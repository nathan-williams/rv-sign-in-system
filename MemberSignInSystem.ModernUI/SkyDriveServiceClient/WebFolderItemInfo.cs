using System;
using System.Globalization;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// Provides webfolderitem content specific data. This is an <c>abstract</c> class.
    /// </summary>
    [Serializable]
    [XmlInclude(typeof(WebDocumentInfo))]
    [XmlInclude(typeof(WebFavoriteInfo))]
    [XmlInclude(typeof(WebPhotoInfo))]
    [XmlInclude(typeof(WebFileInfo))]
    [XmlInclude(typeof(WebFolderInfo))]
    public abstract class WebFolderItemInfo : ICloneable
    {
        #region Fields
        /// <summary>
        /// The delimiter character used in path URL.
        /// </summary>
        public const char PathUrlSegmentDelimiter = '/';

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the resource id.
        /// </summary>
        /// <value>The resource id.</value>
        public string ResourceId { get; set; }

        /// <summary>
        /// Gets or sets the ItemType.
        /// </summary>
        /// <value>The ItemType.</value>
        public WebFolderItemType ItemType { get; set; }

        /// <summary>
        /// Gets or sets the CID of the item's owner.
        /// </summary>
        /// <value>The CID of the item's owner.</value>
        public string OwnerCid { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the ShareType.
        /// </summary>
        /// <value>The ShareType.</value>
        public WebFolderItemShareType ShareType { get; set; }

        /// <summary>
        /// Gets or sets the size in bytes.
        /// </summary>
        /// <value>The size.</value>
        public long Size { get; set; }

        /// <summary>
        /// Gets or sets the date when item was added.
        /// </summary>
        /// <value>The date when item was added.</value>
        public DateTime? DateAdded { get; set; }

        /// <summary>
        /// Gets or sets the date when item was modified.
        /// </summary>
        /// <value>The date when item was modified.</value>
        public DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the WebDAV host.
        /// </summary>
        /// <value>
        /// The WebDAV host.
        /// </value>
        public string WebDavHost { get; set; }

        ///// <summary>
        ///// Gets or sets the path URL.
        ///// </summary>
        ///// <value>The path URL.</value>
        //public string PathUrl { get; set; }

        ///// <summary>
        ///// Gets or sets the URL to view the item.
        ///// </summary>
        ///// <value>The view URL.</value>
        //public string ViewUrl { get; set; }

        ///// <summary>
        ///// Gets or sets the URL to download the item.
        ///// </summary>
        ///// <value>The download URL.</value>
        ///// <remarks></remarks>
        //public string DownloadUrl { get; set; }

        ///// <summary>
        ///// Gets or sets the webfolderitemicon.
        ///// </summary>
        ///// <value>The webfolderitemicon.</value>
        //public WebFolderItemIconInfo WebIcon { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
        /// </returns>
        public override string ToString()
        {
            if (String.IsNullOrEmpty(Name))
                return ShareType != WebFolderItemShareType.None ?
                    String.Format(CultureInfo.InvariantCulture, "{0} ({1})", Path, ShareType) : Path;
            else
                return ShareType != WebFolderItemShareType.None ?
                    String.Format(CultureInfo.InvariantCulture, "{0} ({1})", Name, ShareType) : Name;
        }

        /// <summary>
        /// Gets an array containing the path segments that make up the specified path URL.
        /// </summary>
        /// <param name="pathUrl">The path URL.</param>
        /// <returns>The segments of the path URL.</returns>
        public static string[] GetPathUrlSegments(string pathUrl)
        {
            string[] pathUrlSegments = null;
            if (!String.IsNullOrEmpty(pathUrl))
                pathUrlSegments = pathUrl.StartsWith(PathUrlSegmentDelimiter.ToString(), StringComparison.OrdinalIgnoreCase) ? 
                    pathUrl.Substring(1).Split(PathUrlSegmentDelimiter) :
                    pathUrl.Split(PathUrlSegmentDelimiter);
            return pathUrlSegments;
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public abstract object Clone();

        /// <summary>
        /// Creates a new object of T that is a copy of the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the new object, it has to be derived from <see cref="WebFolderItemInfo" />.</typeparam>
        /// <returns>A new object of T that is a copy of this instance.</returns>
        protected virtual T Clone<T>() where T : WebFolderItemInfo, new()
        {
            T webFolderItemNew = new T
            {
                OwnerCid = OwnerCid,
                //CreatorUrl = CreatorUrl,
                DateAdded = DateAdded,
                DateModified = DateModified,
                Description = Description,
                Name = Name,
                Path = Path,
                ShareType = ShareType,
                Size = Size,
                WebDavHost = WebDavHost,
                //ViewUrl = ViewUrl,
                //WebIcon = WebIcon
            };

            return webFolderItemNew;
        }

        #endregion

    }
}
