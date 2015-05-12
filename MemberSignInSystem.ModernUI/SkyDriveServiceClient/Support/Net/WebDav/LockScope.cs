using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class LockScope
    {
        /// <summary>Gets the code.</summary>
        [XmlIgnore]
        public abstract LockScopeCode Code { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ExclusiveLockScope : LockScope
    {
        /// <summary>Gets the code.</summary>
        [XmlIgnore]
        public override LockScopeCode Code
        {
            get { return LockScopeCode.Exclusive; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SharedLockScope : LockScope
    {
        /// <summary>Gets the code.</summary>
        [XmlIgnore]
        public override LockScopeCode Code
        {
            get { return LockScopeCode.Shared; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class LocalLockScope : LockScope
    {
        /// <summary>Gets the code.</summary>
        [XmlIgnore]
        public override LockScopeCode Code
        {
            get { return LockScopeCode.Local; }
        }
    }
}
