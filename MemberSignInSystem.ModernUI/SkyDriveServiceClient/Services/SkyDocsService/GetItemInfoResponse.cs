using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public class GetItemInfoResponse : OperationResponse
    {
        /// <summary>
        /// Gets or sets the item view URL.
        /// </summary>
        /// <value>
        /// The item view URL.
        /// </value>
        public string ItemViewUrl { get; set; }

        /// <summary>
        /// Gets or sets the itemweb URL.
        /// </summary>
        /// <value>
        /// The itemweb URL.
        /// </value>
        public string ItemWebUrl { get; set; }

        /// <summary>
        /// Gets or sets the library.
        /// </summary>
        /// <value>
        /// The library.
        /// </value>
        public Library Library { get; set; }

        /// <summary>
        /// Gets or sets the signed in user.
        /// </summary>
        /// <value>
        /// The signed in user.
        /// </value>
        public string SignedInUser { get; set; }
    }
}
