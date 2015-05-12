using System;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WebAccountInfo
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        /// <value>
        /// The name of the product.
        /// </value>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the short name of the product.
        /// </summary>
        /// <value>
        /// The short name of the product.
        /// </value>
        public string ProductShortName { get; set; }

        /// <summary>
        /// Gets or sets the user info.
        /// </summary>
        /// <value>
        /// The user info.
        /// </value>
        public WebUserInfo UserInfo { get; set; }

        /// <summary>
        /// Gets or sets the drive info.
        /// </summary>
        /// <value>
        /// The drive info.
        /// </value>
        public WebDriveInfo DriveInfo { get; set; }
    }
}
