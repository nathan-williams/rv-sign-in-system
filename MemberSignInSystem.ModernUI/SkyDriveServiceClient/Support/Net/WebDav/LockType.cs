using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class LockType
    {
        /// <summary>Gets the code.</summary>
        [XmlIgnore]
        public abstract LockTypeCode Code { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class WriteLockType : LockType
    {
        /// <summary>Gets the code.</summary>
        [XmlIgnore]
        public override LockTypeCode Code
        {
            get { return LockTypeCode.Write; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TransactionLockType : LockType
    {
        /// <summary>Gets the code.</summary>
        [XmlIgnore]
        public override LockTypeCode Code
        {
            get { return LockTypeCode.Transaction; }
        }

        /// <summary>Gets or sets the location.</summary>
        /// <value>The location.</value>
        [XmlElement(ElementName = "local", Type = typeof(LocalTransactionLockTypeLocation))]
        public TransactionLockTypeLocation Location { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum TransactionLockTypeLocationCode
    {
        /// <summary>
        /// 
        /// </summary>
        Local,
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class TransactionLockTypeLocation
    {
        /// <summary>Gets the code.</summary>
        public abstract TransactionLockTypeLocationCode Code { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LocalTransactionLockTypeLocation : TransactionLockTypeLocation
    {
        /// <summary>Gets the code.</summary>
        public override TransactionLockTypeLocationCode Code
        {
            get { return TransactionLockTypeLocationCode.Local; }
        }
    }
}
