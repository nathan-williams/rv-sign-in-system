using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Response
    {
        /// <summary>Gets or sets the URL.</summary>
        /// <value>The URL.</value>
        [XmlElement(ElementName = "href")]
        public string[] Urls { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        /// <summary>Gets or sets the prop stat.</summary>
        /// <value>The prop stat.</value>
        [XmlElement(ElementName = "propstat")]
        public PropStat[] PropStats { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [XmlElement(ElementName = "responsedescription")]
        public string Description { get; set; }
    }
}
