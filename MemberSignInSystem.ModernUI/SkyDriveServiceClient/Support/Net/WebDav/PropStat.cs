using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PropStat
    {
        /// <summary>Gets or sets the prop.</summary>
        /// <value>The prop.</value>
        [XmlElement(ElementName = "prop")]
        public Prop Prop { get; set; }

        /// <summary>Gets or sets the status.</summary>
        /// <value>The status.</value>
        [XmlElement(ElementName = "status")]
        public string Status { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [XmlElement(ElementName = "responsedescription")]
        public string Description { get; set; }
    }
}
