using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ResourceTypeContainer
    {
        /// <summary>Gets or sets the type of the resource.</summary>
        /// <value>The type of the resource.</value>
        [XmlElement(ElementName = "collection", Type = typeof(CollectionResourceType))]
        public ResourceType ResourceType { get; set; }
    }
}
