using System;
using YellowbrickV8.Entities;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Tracker.Data
{
    internal static class RaceExtensions
    {

        internal static bool AppendRaceXmlNode(this Race race, XmlNode containerNode)
        {
            bool success = true;
            try
            {
                XmlNode configNode = containerNode.SelectSingleNode("race");
                if (configNode == null)
                {
                    configNode = containerNode.OwnerDocument.CreateNode(XmlNodeType.Element, "race", containerNode.OwnerDocument.NamespaceURI);
                    containerNode.AppendChild(configNode);
                }
                configNode.InnerXml = race.SerializeToXmlString();
            }
            catch
            {
                success = false;
            }

            return success;
        }

        internal static string SerializeToXmlString(this Race race)
        {
            XmlSerializer mySerializer = null;
            StringWriter xout = new StringWriter();
            try
            {
                var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true
                };
                using (var writer = XmlWriter.Create(xout, settings))
                {
                    var xmlSerializer = new XmlSerializer(race.GetType());
                    xmlSerializer.Serialize(writer, race);
                }
                return xout.ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (xout != null)
                    xout.Close();
                if (mySerializer != null)
                    mySerializer = null;
            }
        }

        internal static Race DeserializeRaceXmlNode(XmlNode pluginNode)
        {
            Race returned = null;

            MemoryStream stm = null;
            XmlSerializer mySerializer = null;
            try
            {
                stm = new MemoryStream();
                StreamWriter stw = new StreamWriter(stm);
                stw.Write(pluginNode.SelectSingleNode("race").InnerXml);
                stw.Flush();
                stm.Position = 0;
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                mySerializer = new XmlSerializer(typeof(Race));
                // Call the Deserialize method and cast to the object type.
                returned = (Race)mySerializer.Deserialize(stm);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (stm != null)
                    stm.Close();
                if (mySerializer != null)
                    mySerializer = null;
            }

            return returned;
        }

    }
}
