using System;
using System.Xml.Serialization;
using HgCo.WindowsLive.SkyDrive.Support.Net.Soap;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class OperationRequest : SoapWebRequest
    {
        /// <summary>
        /// Gets or sets the service info.
        /// </summary>
        /// <value>The service info.</value>
        [XmlElement(ElementName = "BaseRequest")]
        public ServiceInfo Info { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationRequest"/> class.
        /// </summary>
        public OperationRequest()
        {
            Info = new ServiceInfo
            {
                ClientAppId = "SkyDrive Service Client",
                Market = "en-US",
                SkyDocsServiceVersion = "v1.0"
            };
        }
    }
}
