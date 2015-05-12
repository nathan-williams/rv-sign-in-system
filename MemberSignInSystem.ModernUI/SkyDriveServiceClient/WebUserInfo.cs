using System;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WebUserInfo
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the picture URL.
        /// </summary>
        /// <value>
        /// The picture URL.
        /// </value>
        public string PictureUrl { get; set; }

        /// <summary>
        /// Gets or sets the small picture URL.
        /// </summary>
        /// <value>
        /// The small picture URL.
        /// </value>
        public string SmallPictureUrl { get; set; }
    }
}
