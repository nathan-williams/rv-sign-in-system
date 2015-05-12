using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlType(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public class SharedLibrary : Library
    {
        /// <summary>
        /// Gets or sets the name of the user who shared the folder.
        /// </summary>
        /// <value>The owner.</value>
        public string Owner { get; set; }
    }
}
