using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public class GetProductInfoResponse : OperationResponse
    {
        /// <summary>
        /// Gets or sets the product info.
        /// </summary>
        /// <value>
        /// The product info.
        /// </value>
        public ProductInfo ProductInfo { get; set; }
    }
}
