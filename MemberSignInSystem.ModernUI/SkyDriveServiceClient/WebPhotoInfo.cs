using System;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class WebPhotoInfo : WebFileInfo
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebPhotoInfo"/> class.
        /// </summary>
        public WebPhotoInfo()
            : base()
        {
            CategoryType = WebFileCategoryType.Photo;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public override object Clone()
        {
            return Clone<WebPhotoInfo>();
        }

        /// <summary>
        /// Creates a new object of T that is a copy of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the new object, it has to be derived from <see cref="WebFolderItemInfo"/>.</typeparam>
        /// <returns>A new object of T that is a copy of this instance.</returns>
        protected override T Clone<T>()
        {
            var webFolderItemNew = base.Clone<T>();
            var webPhotoNew = webFolderItemNew as WebPhotoInfo;
            if (webPhotoNew != null)
            {
            }
            return webFolderItemNew;
        }

        #endregion
    }
}
