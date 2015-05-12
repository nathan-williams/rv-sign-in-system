using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    public class LockScopeContainer
    {
        /// <summary>Gets or sets the lock scope.</summary>
        /// <value>The lock scope.</value>
        [XmlElement(ElementName = "exclusive", Type = typeof(ExclusiveLockScope))]
        [XmlElement(ElementName = "shared", Type = typeof(SharedLockScope))]
        [XmlElement(ElementName = "local", Type = typeof(LocalLockScope))]
        public LockScope LockScope { get; set; }
    }
}
