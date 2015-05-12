using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Content
    {
        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        /// <summary>Gets or sets the URL.</summary>
        /// <value>The URL.</value>
        [XmlAttribute(AttributeName = "src")]
        public string Url { get; set; }
    }
}
