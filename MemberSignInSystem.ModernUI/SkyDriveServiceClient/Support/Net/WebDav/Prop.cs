using System;
using System.Xml;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Prop
    {
        /// <summary>Gets or sets the display name.</summary>
        /// <value>The display name.</value>
        [XmlElement(ElementName = "displayname")]
        public string DisplayName { get; set; }

        /// <summary>Gets or sets the lock discovery.</summary>
        /// <value>The lock discovery.</value>
        [XmlElement(ElementName = "lockdiscovery")]
        public string LockDiscovery { get; set; }

        /// <summary>Gets or sets the supported lock.</summary>
        /// <value>The supported lock.</value>
        [XmlElement(ElementName = "supportedlock")]
        public SupportedLock SupportedLock { get; set; }

        /// <summary>Gets or sets the is folder.</summary>
        /// <value>The is folder.</value>
        [XmlElement(ElementName = "isFolder")]
        public string IsFolder { get; set; }

        /// <summary>Gets or sets the is collection.</summary>
        /// <value>The is collection.</value>
        [XmlElement(ElementName = "iscollection")]
        public string IsCollection { get; set; }

        /// <summary>Gets or sets the is hidden.</summary>
        /// <value>The is hidden.</value>
        [XmlElement(ElementName = "ishidden")]
        public string IsHidden { get; set; }

        /// <summary>Gets or sets the type of the content.</summary>
        /// <value>The type of the content.</value>
        [XmlElement(ElementName = "getcontenttype")]
        public string ContentType { get; set; }

        /// <summary>Gets or sets the length of the content.</summary>
        /// <value>The length of the content.</value>
        [XmlElement(ElementName = "getcontentlength")]
        public int ContentLength { get; set; }

        /// <summary>Gets or sets the creation date.</summary>
        /// <value>The creation date.</value>
        [XmlElement(ElementName = "creationdate")]
        public DateTime CreationDate { get; set; }

        /// <summary>Gets or sets the modified date.</summary>
        /// <value>The modified date.</value>
        [XmlElement(ElementName = "getlastmodified")]
        public string ModifiedDate { get; set; }

        /// <summary>Gets or sets the win32 last modified time.</summary>
        /// <value>The win32 last modified time.</value>
        [XmlElement(ElementName = "Win32LastModifiedTime", Namespace = "urn:schemas-microsoft-com:")]
        public string Win32LastModifiedTime { get; set; }

        /// <summary>Gets or sets the win32 creation time.</summary>
        /// <value>The win32 creation time.</value>
        [XmlElement(ElementName = "Win32CreationTime", Namespace = "urn:schemas-microsoft-com:")]
        public string Win32CreationTime { get; set; }
        
        /// <summary>Gets or sets the win32 last access time.</summary>
        /// <value>The win32 last access time.</value>
        [XmlElement(ElementName = "Win32LastAccessTime", Namespace = "urn:schemas-microsoft-com:")]
        public string Win32LastAccessTime { get; set; }

        /// <summary>Gets or sets the type of the resource.</summary>
        /// <value>The type of the resource.</value>
        [XmlElement(ElementName = "resourcetype")]
        public ResourceTypeContainer ResourceTypeContainer { get; set; }

        /// <summary>Gets or sets the authoritative directory.</summary>
        /// <value>The authoritative directory.</value>
        [XmlElement(ElementName = "authoritative-directory", Namespace = "http://schemas.microsoft.com/repl/")]
        public string AuthoritativeDirectory { get; set; }

        /// <summary>Gets or sets the prog id.</summary>
        /// <value>The prog id.</value>
        [XmlElement(ElementName = "progid", Namespace = "http://schemas.microsoft.com/clouddocuments")]
        public string ProgId { get; set; }
    }
}
