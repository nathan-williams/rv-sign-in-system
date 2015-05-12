using System;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MediaContent
    {
        /// <summary>Gets or sets the URL.</summary>
        /// <value>The URL.</value>
        [XmlAttribute(AttributeName = "url")]
        public string Url { get; set; }

        /// <summary>Gets or sets the width.</summary>
        /// <value>The width.</value>
        [XmlAttribute(AttributeName = "width")]
        public int Width { get; set; }

        /// <summary>Gets or sets the height.</summary>
        /// <value>The height.</value>
        [XmlAttribute(AttributeName = "height")]
        public int Height { get; set; }

        /// <summary>Gets or sets the size of the file.</summary>
        /// <value>The size of the file.</value>
        [XmlAttribute(AttributeName = "fileSize")]
        public int FileSize { get; set; }
    }
}
