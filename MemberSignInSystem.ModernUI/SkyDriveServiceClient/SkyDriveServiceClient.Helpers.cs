using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using HgCo.WindowsLive.SkyDrive.Services.SkyDocsService;
using HgCo.WindowsLive.SkyDrive.Services.UsersService;
using HgCo.WindowsLive.SkyDrive.Support;
using HgCo.WindowsLive.SkyDrive.Support.Net;
using HgCo.WindowsLive.SkyDrive.Support.Net.WebDav;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SkyDriveServiceClient
    {
        private static readonly Regex CidRegex = new Regex("(?i:cid.(?<CID>[0-9a-f]{16}))");
        
        /// <summary>Creates the sky docs service.</summary>
        /// <returns></returns>
        private SkyDocsServiceClient CreateSkyDocsService()
        {
            var srvSkyDocs = new SkyDocsServiceClient(Session.SkyDocsServiceSession)
            {
                Credentials = Session.Credentials,
                Proxy = Proxy,
                Timeout = Timeout,
            };
            return srvSkyDocs;
        }

        /// <summary>Creates the users service client.</summary>
        /// <returns></returns>
        private UsersServiceClient CreateUsersServiceClient()
        {
            var srvUsers = new UsersServiceClient(Session.UsersServiceSession)
            {
                Credentials = Session.Credentials,
                Proxy = Proxy,
                Timeout = Timeout,
            };
            return srvUsers;
        }

        /// <summary>
        /// Creates the web dav web client.
        /// </summary>
        /// <returns></returns>
        private WebDavWebClient CreateWebDavWebClient()
        {
            var wcWebDav = new WebDavWebClient(Session.WebDavWebClientSession)
            {
                Credentials = Session.Credentials,
                Proxy = Proxy,
                Timeout = Timeout,
            };
            return wcWebDav;
        }

        /// <summary>
        /// Creates the HTTP web client.
        /// </summary>
        /// <returns></returns>
        private HttpWebClient CreateHttpWebClient()
        {
            var wcHttp = new HttpWebClient()
            {
                AllowAutoRedirect = true,
                Credentials = Session.Credentials,
                Proxy = Proxy,
                Timeout = Timeout,
            };
            return wcHttp;
        }

        /// <summary>
        /// Picks the web dav host.
        /// </summary>
        /// <returns></returns>
        private string PickWebDavHost()
        {
            if (WebDavHosts.Count == 0)
            {
                throw new Exception("There is no WebDAV host to pick.");
            }
            
            var rnd = new Random(WebDavHosts.Count);
            var webDavHost = WebDavHosts[rnd.Next(WebDavHosts.Count)];
            return webDavHost;
        }

        /// <summary>
        /// Gets the web folder item web dav URL.
        /// </summary>
        /// <param name="webFolderItem">The web folder item.</param>
        /// <returns></returns>
        private Uri GetWebFolderItemWebDavUrl(WebFolderItemInfo webFolderItem)
        {
            var uriWebDav = new Uri(String.Format(
                CultureInfo.InvariantCulture,
                "https://{0}/{1}{2}",
                webFolderItem.WebDavHost ?? PickWebDavHost(),
                Session.Cid,
                EncodeWebFolderItemPath(webFolderItem.Path)));
            return uriWebDav;
        }

        /// <summary>
        /// Encodes the web folder item path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private static string EncodeWebFolderItemPath(string path)
        {
            var sb = new StringBuilder(path);

            //sb.Replace("^", "^L")
            //    .Replace("+", "^M")
            //    .Replace("/.", "/^.")
            //    .Replace("&", "^0")
            //    .Replace("#", "^N")
            //    .Replace("%", "^1")
            //    .Replace("(", "^5")
            //    .Replace(")", "^6")
            //    .Replace("_", "^_")
            //    .Replace("'", "^4")
            //    .Replace(",", "^J")
            //    .Replace("~", "^F");

            return sb.ToString();
        }

        /// <summary>Parses the cid.</summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        private static string ParseCid(GetWebAccountInfoResponse response)
        {
            string cid = String.Empty;
            if (RegexHelper.IsMatch(CidRegex, response.NewLibraryUrl))
            {
                var matchCid = RegexHelper.Match(CidRegex, response.NewLibraryUrl);
                cid = matchCid.Groups["CID"].Value;
            }
            return cid;
        }

        /// <summary>
        /// Parses the web folder item.
        /// </summary>
        /// <param name="folderItem">The folder item.</param>
        /// <param name="webFolderParent">The web folder parent.</param>
        /// <returns></returns>
        private static WebFolderItemInfo ParseWebFolderItem(FolderItemResponse folderItem, WebFolderInfo webFolderParent)
        {
            WebFolderItemInfo webFolderItem = null;

            var webFolderItemType = WebFolderItemHelper.ParseType(folderItem);
            switch (webFolderItemType)
            {
                case WebFolderItemType.Folder:
                    var webFolder = new WebFolderInfo
                    {
                        CategoryType = webFolderParent != null ?
                            webFolderParent.CategoryType : WebFolderHelper.ParseCategoryType(folderItem),
                        IsSpecial = folderItem.IsSpecial,

                        ResourceId = folderItem.ResourceId,
                        OwnerCid = folderItem.OwnerCid.ToString("x16", CultureInfo.InvariantCulture),
                        Name = folderItem.Title,
                        Description = folderItem.Summary,
                        ShareType = WebFolderItemHelper.ParseShareType(folderItem.SharingLevel),
                        Size = folderItem.Size,
                        DateAdded = folderItem.Published,
                        DateModified = folderItem.Updated,
                        Path = String.Format(
                            CultureInfo.InvariantCulture,
                            "{0}{1}{2}",
                            webFolderParent != null ? webFolderParent.Path : String.Empty,
                            WebFolderItemInfo.PathUrlSegmentDelimiter,
                            folderItem.IsSpecial ? "." + folderItem.CanonicalName : folderItem.Title),
                        WebDavHost = webFolderParent != null ? webFolderParent.WebDavHost : null,
                    };
                    webFolderItem = webFolder;
                    break;
                case WebFolderItemType.File:
                    var webFileCategoryType = WebFileHelper.ParseCategoryType(folderItem);
                    switch (webFileCategoryType)
                    {
                        case WebFileCategoryType.Document:
                            var webDocument = new WebDocumentInfo
                            {
                                ContentType = WebFileHelper.ParseContentType(folderItem),

                                ResourceId = folderItem.ResourceId,
                                OwnerCid = folderItem.OwnerCid.ToString("x16", CultureInfo.InvariantCulture),
                                Name = folderItem.Title,
                                Description = folderItem.Summary,
                                ShareType = webFolderParent != null ? webFolderParent.ShareType : WebFolderItemShareType.Private,
                                Size = folderItem.Size,
                                DateAdded = folderItem.Published,
                                DateModified = folderItem.Updated,
                                Path = String.Format(
                                    CultureInfo.InvariantCulture,
                                    "{0}{1}{2}",
                                    webFolderParent != null ? webFolderParent.Path : String.Empty,
                                    WebFolderItemInfo.PathUrlSegmentDelimiter,
                                    folderItem.IsSpecial ? folderItem.CanonicalName : folderItem.Title),
                                WebDavHost = webFolderParent != null ? webFolderParent.WebDavHost : null,
                            };
                            webFolderItem = webDocument;
                            break;
                        case WebFileCategoryType.Favorite:
                            var webFavorite = new WebFavoriteInfo
                            {
                                Url = WebFavoriteHelper.ParseUrl(folderItem),

                                ContentType = WebFileHelper.ParseContentType(folderItem),

                                ResourceId = folderItem.ResourceId,
                                OwnerCid = folderItem.OwnerCid.ToString("x16", CultureInfo.InvariantCulture),
                                Name = folderItem.Title,
                                Description = folderItem.Summary,
                                ShareType = webFolderParent != null ? webFolderParent.ShareType : WebFolderItemShareType.Private,
                                Size = folderItem.Size,
                                DateAdded = folderItem.Published,
                                DateModified = folderItem.Updated,
                                Path = String.Format(
                                    CultureInfo.InvariantCulture,
                                    "{0}{1}{2}",
                                    webFolderParent != null ? webFolderParent.Path : String.Empty,
                                    WebFolderItemInfo.PathUrlSegmentDelimiter,
                                    folderItem.IsSpecial ? folderItem.CanonicalName : folderItem.Title),
                                WebDavHost = webFolderParent != null ? webFolderParent.WebDavHost : null,
                            };
                            webFolderItem = webFavorite;
                            break;
                        case WebFileCategoryType.Photo:
                            var webPhoto = new WebPhotoInfo
                            {
                                ContentType = WebFileHelper.ParseContentType(folderItem),

                                ResourceId = folderItem.ResourceId,
                                OwnerCid = folderItem.OwnerCid.ToString("x16", CultureInfo.InvariantCulture),
                                Name = folderItem.Title,
                                Description = folderItem.Summary,
                                ShareType = webFolderParent != null ? webFolderParent.ShareType : WebFolderItemShareType.Private,
                                Size = folderItem.Size,
                                DateAdded = folderItem.Published,
                                DateModified = folderItem.Updated,
                                Path = String.Format(
                                    CultureInfo.InvariantCulture,
                                    "{0}{1}{2}",
                                    webFolderParent != null ? webFolderParent.Path : String.Empty,
                                    WebFolderItemInfo.PathUrlSegmentDelimiter,
                                    folderItem.IsSpecial ? folderItem.CanonicalName : folderItem.Title),
                                WebDavHost = webFolderParent != null ? webFolderParent.WebDavHost : null,
                            };
                            webFolderItem = webPhoto;
                            break;
                        case WebFileCategoryType.Video:
                            var webVideo = new WebVideoInfo
                            {
                                ContentType = WebFileHelper.ParseContentType(folderItem),

                                ResourceId = folderItem.ResourceId,
                                OwnerCid = folderItem.OwnerCid.ToString("x16", CultureInfo.InvariantCulture),
                                Name = folderItem.Title,
                                Description = folderItem.Summary,
                                ShareType = webFolderParent != null ? webFolderParent.ShareType : WebFolderItemShareType.Private,
                                Size = folderItem.Size,
                                DateAdded = folderItem.Published,
                                DateModified = folderItem.Updated,
                                Path = String.Format(
                                    CultureInfo.InvariantCulture,
                                    "{0}{1}{2}",
                                    webFolderParent != null ? webFolderParent.Path : String.Empty,
                                    WebFolderItemInfo.PathUrlSegmentDelimiter,
                                    folderItem.IsSpecial ? folderItem.CanonicalName : folderItem.Title),
                                WebDavHost = webFolderParent != null ? webFolderParent.WebDavHost : null,
                            };
                            webFolderItem = webVideo;
                            break;
                        default:
                            throw new NotSupportedException(String.Format(
                                "The given web file category type s not supported: {0}", webFileCategoryType));
                    }
                    break;
            }

            return webFolderItem;
        }

    }
}
