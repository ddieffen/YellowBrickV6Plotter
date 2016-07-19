using System.Xml;
using System.Drawing;

namespace Tracker
{
    public static class Tools
    {
        public static XmlAttribute CreateAttr(this XmlDocument xmlDoc, string name, object value)
        {
            XmlAttribute attribute = xmlDoc.CreateAttribute(name);
            if (value == null)
                attribute.Value = "";
            else
                attribute.Value = value.ToString();
            return attribute;
        }
       
        public static XmlNode CreateNode(this XmlDocument xmlDoc, string name, params XmlNode[] children)
        {
            XmlNode node = xmlDoc.CreateNode(XmlNodeType.Element, name, xmlDoc.NamespaceURI);
            foreach (XmlNode child in children)
            {
                if (child is XmlAttribute)
                    node.Attributes.Append(child as XmlAttribute);
                else
                    node.AppendChild(child);
            }
            return node;
        }

        const int RGBMAX = 255;
        public static Color InvertMeAColour(Color ColourToInvert)
        {
            return Color.FromArgb(RGBMAX - ColourToInvert.R,
              RGBMAX - ColourToInvert.G, RGBMAX - ColourToInvert.B);
        }
    }
}
