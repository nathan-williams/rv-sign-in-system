using System.IO;

namespace HgCo.WindowsLive.SkyDrive.Support.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpWebMessagePart
    {
        /// <summary>
        /// Gets or sets the MIME version.
        /// </summary>
        /// <value>The MIME version.</value>
        public string MimeVersion { get; set; }

        /// <summary>
        /// Gets or sets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public string ContentType { get; set; }
        
        /// <summary>
        /// Gets or sets the content disposition.
        /// </summary>
        /// <value>The content disposition.</value>
        public string ContentDisposition { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public byte[] Content { get; set; }

        /// <summary>
        /// Gets or sets the content stream.
        /// </summary>
        /// <value>The content stream.</value>
        public Stream ContentStream { get; set; }
    }
}
