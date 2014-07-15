using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using Tracker.Data;
using System.Net;
using System.ComponentModel;
using Tracker;
using YellowbrickV6;
using YellowbrickV6.Entities;

namespace Tracker
{
    public static class Presenter
    {
        #region delegates
        public delegate void Updating(string message);
        public static event Updating UpdatingEvent;
        #endregion

        #region attributes
        public static Messages messages = new Messages();
        public static long bytesTransfered = 0;
        #endregion

        #region load save
        public static int LoadData(string DataWorkingFolder, string SoftFolder)
        {
            if (new FileInfo(DataWorkingFolder + @"\contour.txt").Exists)
                LoadContour(DataWorkingFolder + @"\contour.txt");

            if (new FileInfo(DataWorkingFolder + @"\data.xml").Exists)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(DataWorkingFolder + @"\data.xml");
                LoadFromSavedData(xmlDoc);
                return 0;
            }

            return -1;
        }

        public static void LoadContour(string filename)
        {
            string line;

            Holder.contours = new List<Contour>();

            // Read the file and display it line by line.
            System.IO.StreamReader file =
               new System.IO.StreamReader(filename);

            Contour currContour = new Contour();
            while ((line = file.ReadLine()) != null)
            {
                if (String.IsNullOrEmpty(line) == false && line[0] != '#')
                {
                    if (line[0] != '>')
                    {
                        string[] split = line.Split('\t');
                        ContourPoint cp = new ContourPoint(Convert.ToDouble(split[1]), Convert.ToDouble(split[0]));
                        currContour.points.Add(cp);
                    }
                    else if(line[0] == '>')
                    {
                        if (currContour.points.Count > 0)
                        {
                            Holder.contours.Add(currContour);
                            currContour = new Contour();
                        }
                    }
                }
            }

            file.Close();
        }

        public static void SaveData(string filename)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateNode("root");
            xmlDoc.AppendChild(rootNode);

            #region serverBase
            rootNode.AppendChild(xmlDoc.CreateNode("sectionChecked", xmlDoc.CreateAttr("ids", Holder.sectionsSelected)));
            rootNode.AppendChild(xmlDoc.CreateNode("teamChecked", xmlDoc.CreateAttr("ids", Holder.teamsSelected)));
            
            #endregion
         
            if(Holder.race != null)
                Holder.race.AppendRaceXmlNode(rootNode);

            xmlDoc.Save(filename);
        }

        public static void LoadFromSavedData(XmlDocument xmlDoc)
        {
            if (xmlDoc != null)
            {
                Holder.dataIsBeeingUpdated = true;

                #region serverBase
                Holder.sectionsSelected = xmlDoc.SelectSingleNode("root/sectionChecked").Attributes["ids"].Value;
                Holder.teamsSelected = xmlDoc.SelectSingleNode("root/teamChecked").Attributes["ids"].Value;
                #endregion

                if (xmlDoc.SelectSingleNode("root") != null)
                    Holder.race = RaceExtensions.DeserializeRaceXmlNode(xmlDoc.SelectSingleNode("root"));

                Holder.dataIsBeeingUpdated = false;
            }
            else
            {
                Presenter.messages.Add(DateTime.Now, "Trying to load data but XmlDocument sent is null");
            }
        }

        #endregion

        #region update from web

        public static int updateRace(string serverName, string raceKey)
        {
            if (UpdatingEvent != null)
                UpdatingEvent("Updating Race...");
            try
            {
                Race race = YBTracker.getRaceInformation(serverName, raceKey);

                if (race != null)
                {
                    if (Holder.race == null)
                    {
                        Holder.race = race;
                        return 0;
                    }
                    Holder.race.title = race.title;

                    if (Holder.race.tags == null)
                        Holder.race.tags = new List<Tag>();
                    foreach (Tag tag in race.tags)
                    {
                        if (!Holder.race.tags.Any(t => t.id == tag.id))
                            Holder.race.tags.Add(tag);
                    }

                    if (Holder.race.teams == null)
                        Holder.race.teams = new List<Team>();
                    foreach (Team team in race.teams)
                    {
                        if (!Holder.race.teams.Any(t => t.id == team.id))
                            Holder.race.teams.Add(team);
                    }

                    if (Holder.race.course == null)
                        Holder.race.course = new Course();

                    Holder.race.course.nodes.Clear();
                    foreach (Node node in race.course.nodes)
                        Holder.race.course.nodes.Add(node);

                    return 0;
                }
                else
                {
                    messages.Add(DateTime.Now, "Failed to update the race");
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        public static int updateLatestPositions(string serverName, string raceKey)
        {
            if (UpdatingEvent != null)
                UpdatingEvent("Updating Latest Positions...");
            try
            {
                List<Team> teams = YBTracker.getNewPositions(serverName, raceKey);

                if (Holder.race != null && Holder.race.teams != null)
                {
                    YBTracker.UpdateTeamsMoments(Holder.race.teams, teams);
                    foreach (Team team in Holder.race.teams)
                        YBTracker.UpdateMomentsSpeedHeading(team);
                    return 0;
                }
                else
                {
                    messages.Add(DateTime.Now, "Failed to update LatestPositions");
                    return -1;
                }
            }
            catch 
            {
                return -1;
            }
        }

        public static int updateAllPositions(string serverName, string raceKey)
        {
            if (UpdatingEvent != null)
                UpdatingEvent("Updating All Positions...");
            try
            {
                List<Team> teams = YBTracker.getAllPositions(serverName, raceKey);

                if (Holder.race.teams != null && Holder.race != null)
                {
                    YBTracker.UpdateTeamsMoments(Holder.race.teams, teams);
                    foreach (Team team in Holder.race.teams)
                        YBTracker.UpdateMomentsSpeedHeading(team);
                    return 0;
                }
                else
                {
                    messages.Add(DateTime.Now, "Failed to update AllPositions");
                    return -1;
                }
            }
            catch
            {
                return -1;
            }
        }

        #endregion

    }
}
