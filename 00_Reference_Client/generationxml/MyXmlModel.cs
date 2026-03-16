using System.Xml.Serialization;

namespace generationxml
{
    [XmlRoot("MyXmlModel")]
    public class MyXmlModel
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Code")]
        public string Code { get; set; }
    }

}