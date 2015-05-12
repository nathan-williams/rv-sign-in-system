using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlType(Namespace = "http://schemas.microsoft.com/clouddocuments")]
    public class ProductInfo
    {
        /// <summary>
        /// Gets or sets the home page URL.
        /// </summary>
        /// <value>
        /// The home page URL.
        /// </value>
        public string HomePageUrl { get; set; }

        /// <summary>
        /// Gets or sets the learn more URL.
        /// </summary>
        /// <value>
        /// The learn more URL.
        /// </value>
        public string LearnMoreUrl { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the service disabled error message.
        /// </summary>
        /// <value>
        /// The service disabled error message.
        /// </value>
        public string ServiceDisabledErrorMessage { get; set; }

        /// <summary>
        /// Gets or sets the short name of the product.
        /// </summary>
        /// <value>
        /// The short name of the product.
        /// </value>
        public string ShortProductName { get; set; }

        /// <summary>
        /// Gets or sets the sign in message.
        /// </summary>
        /// <value>
        /// The sign in message.
        /// </value>
        public string SignInMessage { get; set; }

        /// <summary>
        /// Gets or sets the sign up message.
        /// </summary>
        /// <value>
        /// The sign up message.
        /// </value>
        public string SignUpMessage { get; set; }

        /// <summary>
        /// Gets or sets the sign up URL.
        /// </summary>
        /// <value>
        /// The sign up URL.
        /// </value>
        public string SignUpUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is SOAP enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is SOAP enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsSoapEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is sync enabled.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is sync enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsSyncEnabled { get; set; }
    }
}
