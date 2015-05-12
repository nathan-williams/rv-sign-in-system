using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Author
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        /// <summary>Gets or sets the photo URL.</summary>
        /// <value>The photo URL.</value>
        [XmlElement(ElementName = "uri")]
        public string PhotoUrl { get; set; }

        /// <summary>Gets or sets the small photo URL.</summary>
        /// <value>The small photo URL.</value>
        [XmlElement(ElementName = "uriSmall", Namespace = "http://api.live.com/schemas")]
        public string SmallPhotoUrl { get; set; }

        /// <summary>Gets or sets the CID.</summary>
        /// <value>The CID.</value>
        [XmlElement(ElementName = "cid", Namespace = "http://api.live.com/schemas")]
        public long CID { get; set; }

        /// <summary>Gets or sets the menu codes.</summary>
        /// <value>The menu codes.</value>
        [XmlElement(ElementName = "menuCodes", Namespace = "http://api.live.com/schemas")]
        public string MenuCodes { get; set; }
    }
}
