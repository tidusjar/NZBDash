using System.Xml.Serialization;

namespace NZBDash.Api.Models
{

    [XmlRoot(ElementName = "Server")]
    public class Server
    {
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        [XmlAttribute(AttributeName = "host")]
        public string Host { get; set; }
        [XmlAttribute(AttributeName = "address")]
        public string Address { get; set; }
        [XmlAttribute(AttributeName = "port")]
        public string Port { get; set; }
        [XmlAttribute(AttributeName = "machineIdentifier")]
        public string MachineIdentifier { get; set; }
        [XmlAttribute(AttributeName = "version")]
        public string Version { get; set; }
    }

    [XmlRoot(ElementName = "MediaContainer")]
    public class PlexServers
    {
        [XmlElement(ElementName = "Server")]
        public Server Server { get; set; }
        [XmlAttribute(AttributeName = "size")]
        public string Size { get; set; }
    }
}
