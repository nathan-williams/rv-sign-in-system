using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
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
        PublicShared,
        /// <summary>
        /// 
        /// </summary>
        PublicUnlisted,
        /// <summary>
        /// 
        /// </summary>
        Shared,
        /// <summary>
        /// 
        /// </summary>
        Private,
    }
}
