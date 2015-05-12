using System;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// Provides webfile content specific data.
    /// </summary>
    [Serializable]
    public abstract class WebFileInfo : WebFolderItemInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets ContentType.
        /// </summary>
        /// <value>The ContentType.</value>
        public WebFileCategoryType CategoryType { get; set; }

        /// <summary>
        /// Gets or sets ContentType.
        /// </summary>
        /// <value>The ContentType.</value>
        public string ContentType { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebFileInfo"/> class.
        /// </summary>
        public WebFileInfo()
        {
            ItemType = WebFolderItemType.File;
        }
        
        #endregion

        #region Methods

        /// <summary>
        /// Creates a new object of T that is a copy of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the new object, it has to be derived from <see cref="WebFolderItemInfo"/>.</typeparam>
        /// <returns>A new object of T that is a copy of this instance.</returns>
        protected override T Clone<T>()
        {
            T webFolderItemNew = base.Clone<T>();
            WebFileInfo webFileNew = webFolderItemNew as WebFileInfo;
            if (webFileNew != null)
            {
                webFileNew.ContentType = ContentType;
            }
            return webFolderItemNew;
        }

        ///// <summary>
        ///// Creates a webfile instance.
        ///// </summary>
        ///// <param name="fileName">The local name of the file (with or without local path information).</param>
        ///// <param name="webFolderParent">The parent webfolder.</param>
        ///// <returns>The webfile represents that file.</returns>
        //public static WebFileInfo CreateWebFileInstance(string fileName, WebFolderInfo webFolderParent)
        //{
        //    var fiFile = new System.IO.FileInfo(fileName);
        //    var webFile = new WebFileInfo
        //    {
        //        Name = System.IO.Path.GetFileNameWithoutExtension(fiFile.Name),
        //        ShareType = webFolderParent.ShareType,
        //        PathUrl = String.Concat(webFolderParent.PathUrl, PathUrlSegmentDelimiter, fiFile.Name),

        //        Extension = fiFile.Extension,
        //    };
        //    return webFile;
        //}

        #endregion
    }
}
