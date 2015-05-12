using System;
using System.Xml;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LockEntry
    {
        /// <summary>Gets or sets the lock scope.</summary>
        /// <value>The lock scope.</value>
        [XmlElement(ElementName = "lockscope")]
        public LockScopeContainer LockScopeContainer { get; set; }

        /// <summary>Gets or sets the lock type.</summary>
        /// <value>The lock type.</value>
        [XmlElement(ElementName = "locktype")]
        public LockTypeContainer LockTypeContainer { get; set; }
    }
}
