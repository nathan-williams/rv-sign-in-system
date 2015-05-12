using System;
using System.Text.RegularExpressions;
using System.Web;
using HgCo.WindowsLive.SkyDrive.Services.UsersService;

namespace HgCo.WindowsLive.SkyDrive.Support
{
    /// <summary>
    /// Provides methods for handling <see cref="WebFolderItemInfo"/> specific properties and tasks.
    /// </summary>
    internal static class WebFolderItemHelper
    {
        #region Fields
        
        /// <summary>
        /// The regular expression to parse a webfolderitem's path URL from HTML.
        /// </summary>
        private static readonly Regex RegexPathUrl = new Regex("(?i:https?://[^/]+/\\w+.aspx(?<Path>[^?]+))");

        /// <summary>
        /// The URL encoded representation of '+'.
        /// </summary>
        private static readonly string UrlEncodedPlusCharacter = HttpUtility.UrlEncode("+");

        #endregion

        #region Methods

        /// <summary>
        /// Parses the ShareType from UsersService.SharingLevel.
        /// </summary>
        /// <param name="sharingLevel">The sharing level.</param>
        /// <returns>
        /// The parsed ShareType.
        /// </returns>
        public static WebFolderItemShareType ParseShareType(Services.UsersService.SharingLevel sharingLevel)
        {
            WebFolderItemShareType shareType = WebFolderItemShareType.None;
            switch (sharingLevel)
            {
                case Services.UsersService.SharingLevel.Public:
                case Services.UsersService.SharingLevel.PublicShared:
                    shareType = WebFolderItemShareType.Public;
                    break;
                case Services.UsersService.SharingLevel.PublicUnlisted:
                    shareType = WebFolderItemShareType.MyNetwork;
                    break;
                case Services.UsersService.SharingLevel.Shared:
                    shareType = WebFolderItemShareType.PeopleSelected;
                    break;
                case Services.UsersService.SharingLevel.Private:
                    shareType = WebFolderItemShareType.Private;
                    break;
            }
            return shareType;
        }

        /// <summary>
        /// Parses the type.
        /// </summary>
        /// <param name="folderItem">The folder item.</param>
        /// <returns></returns>
        public static WebFolderItemType ParseType(FolderItemResponse folderItem)
        {
            var webFolderItemType =
                folderItem.Type.Equals("Library", StringComparison.OrdinalIgnoreCase) ||
                folderItem.Type.Equals("Folder", StringComparison.OrdinalIgnoreCase) ?
                WebFolderItemType.Folder : WebFolderItemType.File;
            return webFolderItemType;
        }

        ///// <summary>
        ///// Gets the URL for viewing a webfolderitem.
        ///// </summary>
        ///// <param name="cid">The cid.</param>
        ///// <param name="webFolderItem">The webfolderitem to view.</param>
        ///// <returns>
        ///// The URL for viewing a webfolderitem.
        ///// </returns>
        //private static string GetWebFolderItemViewUrl(string cid, WebFolderItemInfo webFolderItem)
        //{
        //    string urlView = null;
        //    if (!String.IsNullOrEmpty(cid) && webFolderItem != null)
        //        urlView = String.Format(
        //            CultureInfo.InvariantCulture,
        //            "{0}://{1}.skydrive.live.com/self.aspx{2}",
        //            webFolderItem.ShareType != WebFolderItemShareType.Public ? "https" : "http",
        //            cid,
        //            webFolderItem.PathUrl);
        //    return urlView;
        //}

        ///// <summary>
        ///// Parses the PathUrl from an URL.
        ///// </summary>
        ///// <param name="url">The URL to be parsed.</param>
        ///// <returns>The parsed PathUrl.</returns>
        //public static string ParsePathUrl(string url)
        //{
        //    string pathUrl = null;
        //    if (!String.IsNullOrEmpty(url))
        //    {
        //        string urlDecoded = HtmlDocumentHelper.DecodeUnicodeString(url);
        //        // Need to replace '+' to its URL encoded representation, 
        //        // otherwise UrlDecode method would decode it to ' '
        //        urlDecoded = urlDecoded.Replace("+", UrlEncodedPlusCharacter);
        //        urlDecoded = HttpUtility.UrlDecode(urlDecoded);
        //        if (RegexHelper.IsMatch(RegexPathUrl, urlDecoded))
        //        {
        //            Match matchPathUrl = RegexHelper.Match(RegexPathUrl, urlDecoded);
        //            pathUrl = matchPathUrl.Groups["Path"].Value.Replace("|", String.Empty);
        //        }
        //        else
        //        {
        //            // In case of some folders (it seems which contain more unicode characters),
        //            // path can be given in the query parameter "path"
        //            var uriDecoded = new Uri(urlDecoded);
        //            var queryParameters = HttpUtility.ParseQueryString(uriDecoded.Query);
        //            var pathQueryParameter = queryParameters["path"];
        //            if (!String.IsNullOrEmpty(pathQueryParameter))
        //            {
        //                pathUrl = pathQueryParameter;
        //            }
        //        }
        //    }
        //    return pathUrl;
        //}

        #endregion
    }
}
