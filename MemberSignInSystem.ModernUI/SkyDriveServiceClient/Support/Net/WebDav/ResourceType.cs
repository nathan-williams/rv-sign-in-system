using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ResourceType
    {
        /// <summary>Gets the code.</summary>
        [XmlIgnore]
        public abstract ResourceTypeCode Code { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class CollectionResourceType : ResourceType
    {
        /// <summary>Gets the code.</summary>
        public override ResourceTypeCode Code
        {
            get { return ResourceTypeCode.Collection; }
        }
    }
}
