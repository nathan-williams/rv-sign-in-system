using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public class GetWebAccountInfoRequest : OperationRequest
    {
        /// <summary>
        /// Gets or sets a value indicating whether [get read write libraries only].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [get read write libraries only]; otherwise, <c>false</c>.
        /// </value>
        public bool GetReadWriteLibrariesOnly { get; set; }

        /// <summary>
        /// Gets or sets the SOAP action.
        /// </summary>
        /// <value>The SOAP action.</value>
        public override string SoapAction
        {
            get { return "GetWebAccountInfo"; }
        }
    }
}
