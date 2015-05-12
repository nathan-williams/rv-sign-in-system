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
    public class CreateSubFolderItemRequest : XmlWebRequest
    {
        /// <summary>Gets or sets the title.</summary>
        /// <value>The title.</value>
        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public string Title { get; set; }

        /// <summary>Gets or sets the type.</summary>
        /// <value>The type.</value>
        [XmlElement(ElementName = "type", Namespace = "http://api.live.com/schemas")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [resolve name conflict].
        /// </summary>
        /// <value><c>true</c> if [resolve name conflict]; otherwise, <c>false</c>.</value>
        [XmlElement(ElementName = "resolveNameConflict", Namespace = "http://api.live.com/schemas")]
        public bool ResolveNameConflict { get; set; }
    }
}
