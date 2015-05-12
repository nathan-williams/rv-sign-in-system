using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MediaThumbnail
    {
        /// <summary>Gets or sets the URL.</summary>
        /// <value>The URL.</value>
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }

        /// <summary>Gets or sets the partner URL.</summary>
        /// <value>The partner URL.</value>
        [XmlAttribute(AttributeName = "partnerUrl", Namespace = "http://api.live.com/schemas")]
        public string PartnerUrl { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        [XmlAttribute(AttributeName = "width")]
        public int Width { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        [XmlAttribute(AttributeName = "height")]
        public int Height { get; set; }

        /// <summary>Gets or sets the width of the max.</summary>
        /// <value>The width of the max.</value>
        [XmlAttribute(AttributeName = "maxWidth", Namespace = "http://api.live.com/schemas")]
        public int MaxWidth { get; set; }

        /// <summary>Gets or sets the height of the max.</summary>
        /// <value>The height of the max.</value>
        [XmlAttribute(AttributeName = "maxHeight", Namespace = "http://api.live.com/schemas")]
        public int MaxHeight { get; set; }

        /// <summary>Gets or sets the size of the stream.</summary>
        /// <value>The size of the stream.</value>
        [XmlAttribute(AttributeName = "streamSize", Namespace = "http://api.live.com/schemas")]
        public int StreamSize { get; set; }

        /// <summary>Gets or sets the resource id.</summary>
        /// <value>The resource id.</value>
        [XmlAttribute(AttributeName = "resourceId", Namespace = "http://api.live.com/schemas")]
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is cropped.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is cropped; otherwise, <c>false</c>.
        /// </value>
        [XmlAttribute(AttributeName = "isCropped", Namespace = "http://api.live.com/schemas")]
        public bool IsCropped { get; set; }
    }
}
