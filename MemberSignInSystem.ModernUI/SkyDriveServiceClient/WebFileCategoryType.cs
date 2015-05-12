namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// Specifies the category type of a webfile.
    /// </summary>
    public enum WebFileCategoryType
    {
        /// <summary>
        /// The specified webfile's category type cannot be determined.
        /// </summary>
        None,
        /// <summary>
        /// The specified webfile is a document.
        /// </summary>
        Document,
        /// <summary>
        /// The specified webfile is a favorite.
        /// </summary>
        Favorite,
        /// <summary>
        /// The specified webfile is a photo.
        /// </summary>
        Photo,
        /// <summary>
        /// The specified webfile is a video.
        /// </summary>
        Video,
    }
}