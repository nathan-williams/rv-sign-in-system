using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlType(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public enum SharingLevel
    {
        /// <summary>
        /// 
        /// </summary>
        Public,
        /// <summary>
        /// 
        /// </summary>
        Private,
        /// <summary>
        /// 
        /// </summary>
        Shared,
        /// <summary>
        /// 
        /// </summary>
        PublicUnlisted,
    }
}
