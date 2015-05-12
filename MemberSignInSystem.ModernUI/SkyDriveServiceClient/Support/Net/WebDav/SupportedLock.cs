using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SupportedLock
    {
        /// <summary>Gets or sets the lock entry.</summary>
        /// <value>The lock entry.</value>
        [XmlElement(ElementName = "lockentry")]
        public LockEntry LockEntry { get; set; }
    }
}
