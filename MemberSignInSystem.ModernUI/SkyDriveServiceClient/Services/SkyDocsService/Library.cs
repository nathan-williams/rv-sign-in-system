using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlType(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    [XmlInclude(typeof(SharedLibrary))]
    public class Library
    {
        /// <summary>
        /// Gets or sets the access level.
        /// </summary>
        /// <value>The access level.</value>
        public AccessLevel AccessLevel { get; set; }

        /// <summary>
        /// Gets or sets the URL used to access the library via WebDAV.
        /// </summary>
        /// <value>the URL used to access the library via WebDAV.</value>
        [XmlElement(ElementName = "DavUrl")]
        public string WebDavUrl { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        [XmlElement(ElementName = "DisplayName")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the sharing level.
        /// </summary>
        /// <value>The sharing level.</value>
        public SharingLevelInfo SharingLevelInfo { get; set; }

        /// <summary>
        /// Gets or sets the URL used to access the library via HTTP or HTTPS.
        /// </summary>
        /// <value>the URL used to access the library via HTTP or HTTPS.</value>
        public string WebUrl { get; set; }
    }
}
