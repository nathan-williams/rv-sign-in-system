using System;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// Provides SkyDrive storage secific data.
    /// </summary>
    [Serializable]
    public class WebDriveInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets the total disk space in bytes.
        /// </summary>
        /// <value>
        /// The total disk space in bytes.
        /// </value>
        public long TotalDiskSpace { get; set; }

        /// <summary>
        /// Gets or sets the used disk space in bytes.
        /// </summary>
        /// <value>
        /// The used disk space in bytes.
        /// </value>
        public long UsedDiskSpace { get; set; }

        /// <summary>
        /// Gets the free disk space in bytes.
        /// </summary>
        /// <value>
        /// The free disk space in bytes.
        /// </value>
        public long FreeDiskSpace 
        {
            get { return TotalDiskSpace - UsedDiskSpace; } 
        }

        /// <summary>
        /// Gets or sets the maximum file size.
        /// </summary>
        /// <value>
        /// The maximum file size.
        /// </value>
        public long MaxFileSize { get; set; }

        #endregion
    }
}
