using System;
using System.Xml.Serialization;
using HgCo.WindowsLive.SkyDrive.Support.Net;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
    public class CreateRootFolderRequest : XmlWebRequest
    {
        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [XmlElement(ElementName = "type", Namespace = "http://api.live.com/schemas")]
        public string Type { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        [XmlElement(ElementName = "category", Namespace = "http://api.live.com/schemas")]
        public string Category { get; set; }

        /// <summary>Gets or sets the sharing level.</summary>
        /// <value>The sharing level.</value>
        [XmlElement(ElementName = "sharingLevel", Namespace = "http://api.live.com/schemas")]
        public SharingLevel SharingLevel { get; set; }
    }
}
