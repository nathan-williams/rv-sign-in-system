using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    public class LockTypeContainer
    {
        /// <summary>Gets or sets the lock scope.</summary>
        /// <value>The lock scope.</value>
        [XmlElement(ElementName = "write", Type = typeof(WriteLockType))]
        [XmlElement(ElementName = "transaction", Type = typeof(TransactionLockType))]
        public LockType LockType { get; set; }
    }
}
