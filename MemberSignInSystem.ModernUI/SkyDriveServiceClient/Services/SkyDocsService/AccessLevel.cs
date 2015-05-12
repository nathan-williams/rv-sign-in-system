using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlType(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public enum AccessLevel
    {
        /// <summary>
        /// 
        /// </summary>
        Read,
        /// <summary>
        /// 
        /// </summary>
        ReadWrite,
        /// <summary>
        /// 
        /// </summary>
        None,
    }
}
