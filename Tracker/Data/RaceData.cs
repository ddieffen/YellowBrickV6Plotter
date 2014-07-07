using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowbrickV6.Entities;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Tracker.Data
{
    internal class RaceData : Race
    {

        internal bool AppendConfigurationNode(XmlNode containerNode)
        {
            bool success = true;
            try
            {
                XmlNode configNode = containerNode.SelectSingleNode("Race");
                if (configNode == null)
                {
                    configNode = containerNode.OwnerDocument.CreateNode(XmlNodeType.Element, "Race", containerNode.OwnerDocument.NamespaceURI);
                    containerNode.AppendChild(configNode);
                }
                configNode.InnerXml = this.SerializeToXmlString();
            }
            catch
            {
                success = false;
            }

            return success;
        }

        internal string SerializeToXmlString()
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
                    var xmlSerializer = new XmlSerializer(this.GetType());
                    xmlSerializer.Serialize(writer, this);
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

        internal static RaceData DeserializeConfigurationNode(XmlNode pluginNode)
        {
            RaceData returned = null;

            MemoryStream stm = null;
            XmlSerializer mySerializer = null;
            try
            {
                stm = new MemoryStream();
                StreamWriter stw = new StreamWriter(stm);
                stw.Write(pluginNode.SelectSingleNode("Configuration").InnerXml);
                stw.Flush();
                stm.Position = 0;
                // Construct an instance of the XmlSerializer with the type
                // of object that is being deserialized.
                mySerializer = new XmlSerializer(typeof(RaceData));
                // Call the Deserialize method and cast to the object type.
                returned = (RaceData)mySerializer.Deserialize(stm);
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
