using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public class GetProductInfoRequest : OperationRequest
    {
        /// <summary>
        /// Gets or sets the SOAP action.
        /// </summary>
        /// <value>The SOAP action.</value>
        public override string SoapAction
        {
            get { return "GetProductInfo"; }
        }
    }
}
