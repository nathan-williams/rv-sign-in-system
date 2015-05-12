using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public class GetWebAccountInfoResponse : OperationResponse
    {
        /// <summary>
        /// Gets or sets the account title.
        /// </summary>
        /// <value>
        /// The account title.
        /// </value>
        public string AccountTitle { get; set; }

        /// <summary>
        /// Gets or sets the libraries.
        /// </summary>
        /// <value>
        /// The libraries.
        /// </value>
        public Library[] Libraries { get; set; }

        /// <summary>
        /// Gets or sets the new library URL.
        /// </summary>
        /// <value>
        /// The new library URL.
        /// </value>
        public string NewLibraryUrl { get; set; }

        /// <summary>
        /// Gets or sets the product info.
        /// </summary>
        /// <value>
        /// The product info.
        /// </value>
        public ProductInfo ProductInfo { get; set; }

        /// <summary>
        /// Gets or sets the signed in user.
        /// </summary>
        /// <value>
        /// The signed in user.
        /// </value>
        public string SignedInUser { get; set; }
    }
}
