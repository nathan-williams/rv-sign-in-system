using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlType(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public class ServiceInfo
    {
        /// <summary>
        /// Gets or sets the client app id.
        /// </summary>
        /// <value>The client app id.</value>
        public string ClientAppId { get; set; }

        /// <summary>
        /// Gets or sets the market.
        /// </summary>
        /// <value>The market.</value>
        public string Market { get; set; }

        /// <summary>
        /// Gets or sets the sky docs service version.
        /// </summary>
        /// <value>The sky docs service version.</value>
        public string SkyDocsServiceVersion { get; set; }
    }
}
