using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "multistatus", Namespace = "DAV:")]
    public class GetResourceInfosResponse : GeneralResponse
    {
        /// <summary>Gets or sets the response.</summary>
        /// <value>The response.</value>
        [XmlElement(ElementName = "response")]
        public Response[] Responses { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        [XmlElement(ElementName = "responsedescription")]
        public string Description { get; set; }
    }
}
