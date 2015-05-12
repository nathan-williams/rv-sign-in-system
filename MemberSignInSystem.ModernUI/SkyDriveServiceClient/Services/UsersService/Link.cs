using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Link
    {
        /// <summary>Gets or sets the rel.</summary>
        /// <value>The rel.</value>
        [XmlAttribute(AttributeName = "rel")]
        public string Rel { get; set; }

        /// <summary>Gets or sets the URL.</summary>
        /// <value>The URL.</value>
        [XmlAttribute(AttributeName = "href")]
        public string Url { get; set; }
    }
}
