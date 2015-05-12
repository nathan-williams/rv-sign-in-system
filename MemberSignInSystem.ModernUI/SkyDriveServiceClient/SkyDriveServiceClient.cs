using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using HgCo.WindowsLive.SkyDrive.Services.SkyDocsService;
using HgCo.WindowsLive.SkyDrive.Services.UsersService;
using HgCo.WindowsLive.SkyDrive.Support;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SkyDriveServiceClient
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly List<string> WebDavHosts = new List<string>()
        {
		    "bqvkvy.docs.live.net",
            "9iwlkc.docs.live.net",
            "cmlubp.docs.live.net",
            "mo4vwm.docs.live.net",
            "cbdg9k.docs.live.net",
            "phjcvr.docs.live.net",
            "bceulv.docs.live.net",
            "omd4hq.docs.live.net",
            "lmdvz4.docs.live.net",
            "iaep8q.docs.live.net",
            "twpfax.docs.live.net",
            "bpapni.docs.live.net"
        };

        #region Events

        /// <summary>
        /// Occurs when a webfile upload operation successfully transfers some or all of the data.
        /// </summary>
        /// <remarks>
        /// This event is raised each time a webfile upload make progress.
        /// This event is raised when uploads are started by calling UploadWebFile(string, WebFolderInfo) method.
        /// </remarks>
        public event EventHandler<UploadWebFileProgressChangedEventArgs> UploadWebFileProgressChanged;

        #endregion

        /// <summary>Gets the session.</summary>
        public SkyDriveSession Session { get; private set; }

        /// <summary>
        /// Gets or sets the time-out value in milliseconds.
        /// </summary>
        /// <value>The number of milliseconds to wait before a request times out. The default is 100,000 milliseconds (100 seconds).</value>
        public int Timeout { get; set; }

        /// <summary>
        /// Gets or sets the proxy information used for SkyDrive communication.
        /// </summary>
        /// <value>The <see cref="IWebProxy"/> object to use to proxy the SkyDrive communication.</value>
        public IWebProxy Proxy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkyDriveServiceClient"/> class.
        /// </summary>
        public SkyDriveServiceClient() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkyDriveServiceClient"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public SkyDriveServiceClient(SkyDriveSession session)
        {
            Session = session != null ? session : new SkyDriveSession();
            Timeout = 100000;
        }

        /// <summary>
        /// Logs on to a specified user account.
        /// </summary>
        /// <param name="userName">The name of the user.</param>
        /// <param name="userPassword">The user password.</param>
        public void LogOn(string userName, string userPassword)
        {
            try
            {
                Session.Credentials = new NetworkCredential(userName, userPassword);
                var srvSkyDocs = CreateSkyDocsService();
                var responseWebAccountInfo = srvSkyDocs.GetWebAccountInfo(
                    new GetWebAccountInfoRequest
                    {
                        GetReadWriteLibrariesOnly = false
                    });
                Session.Cid = ParseCid(responseWebAccountInfo);
                
                var srvUsers = CreateUsersServiceClient();
                srvUsers.LogOn(Session.Cid);
            }
            catch (Exception ex)
            {
                throw new LogOnFailedException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Gets the web account info.
        /// </summary>
        /// <returns></returns>
        /// <remarks>This method does not work for unauthenticated users.</remarks>
        public WebAccountInfo GetWebAccountInfo()
        {
            WebAccountInfo webAccountInfo = null;

            try
            {
                var srvSkyDocs = CreateSkyDocsService();
                var responseWebAccountInfo = srvSkyDocs.GetWebAccountInfo(
                    new GetWebAccountInfoRequest
                    {
                        GetReadWriteLibrariesOnly = true
                    });
                var srvUsers = CreateUsersServiceClient();
                var responseListFolderItems = srvUsers.ListRootFolderItems(Session.Cid);

                webAccountInfo = new WebAccountInfo
                {
                    Title = responseWebAccountInfo.AccountTitle,
                    ProductName = responseWebAccountInfo.ProductInfo.ProductName,
                    ProductShortName = responseWebAccountInfo.ProductInfo.ShortProductName,

                    UserInfo = new WebUserInfo
                    {
                        Name = responseWebAccountInfo.SignedInUser,
                        PictureUrl = responseListFolderItems.Author.PhotoUrl,
                        SmallPictureUrl = responseListFolderItems.Author.SmallPhotoUrl,
                    },
                    DriveInfo = new WebDriveInfo
                    {
                        TotalDiskSpace = responseListFolderItems.TotalQuota,
                        UsedDiskSpace = responseListFolderItems.QuotaUsed,
                        MaxFileSize = responseListFolderItems.MaxFileSize,
                    }
                };
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }

            return webAccountInfo;
        }

        /// <summary>
        /// Gets the web drive info.
        /// </summary>
        /// <returns></returns>
        public WebDriveInfo GetWebDriveInfo()
        {
            WebDriveInfo webDriveInfo = null;

            try
            {
                var srvUsers = CreateUsersServiceClient();
                var responseListFolderItems = srvUsers.ListRootFolderItems(Session.Cid);

                webDriveInfo = new WebDriveInfo
                {
                    TotalDiskSpace = responseListFolderItems.TotalQuota,
                    UsedDiskSpace = responseListFolderItems.QuotaUsed,
                    MaxFileSize = responseListFolderItems.MaxFileSize,
                };
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }

            return webDriveInfo;
        }

        /// <summary>
        /// Lists webfolders located in SkyDrive's root.
        /// </summary>
        /// <returns>The list of webfolders in SkyDrive's root.</returns>
        public WebFolderInfo[] ListRootWebFolders()
        {
            var webFolderItems = ListRootWebFolderItems();
            var webFolders = webFolderItems
                .Where(webFolderItem => webFolderItem.ItemType == WebFolderItemType.Folder)
                .Cast<WebFolderInfo>()
                .ToArray();
            return webFolders;
        }

        /// <summary>
        /// Lists webfolderitems located in SkyDrive's root.
        /// </summary>
        /// <returns>The list of webfolderitems in SkyDrive's root.</returns>
        public WebFolderItemInfo[] ListRootWebFolderItems()
        {
            var lWebFolderItem = new List<WebFolderItemInfo>();

            try
            {
                var srvUsers = CreateUsersServiceClient();
                var responseRootFolderItems = srvUsers.ListRootFolderItems(Session.Cid);
                if (responseRootFolderItems.FolderItems != null)
                {
                    lWebFolderItem.AddRange(responseRootFolderItems.FolderItems
                        .Select(folderItem => ParseWebFolderItem(folderItem, null))
                        .Where(webFolderItem => webFolderItem != null));
                }

                if (Session.Credentials != null)
                {
                    var srvSkyDocs = CreateSkyDocsService();
                    var responseWebAccountInfo = srvSkyDocs.GetWebAccountInfo(
                        new GetWebAccountInfoRequest
                        {
                            GetReadWriteLibrariesOnly = false
                        });
                    if (responseWebAccountInfo.Libraries != null)
                    {
                        foreach (var library in responseWebAccountInfo.Libraries)
                        {
                            var uriWebDav = new Uri(library.WebDavUrl);

                            var webFolderItem = lWebFolderItem
                                .FirstOrDefault(wfItem => wfItem.Name.Equals(library.Name, StringComparison.OrdinalIgnoreCase));
                            if (webFolderItem != null)
                            {
                                webFolderItem.WebDavHost = uriWebDav.Authority;
                            }

                            if (!WebDavHosts.Contains(uriWebDav.Authority))
                            {
                                WebDavHosts.Add(uriWebDav.Authority);
                            }
                        }
                    }
                }

                lWebFolderItem
                    .Where(webFolderItem => String.IsNullOrEmpty(webFolderItem.WebDavHost))
                    .ForEach(webFolderItem => webFolderItem.WebDavHost = PickWebDavHost());
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }

            return lWebFolderItem.ToArray();
        }

        /// <summary>
        /// Lists webfolders located in a sub webfolder.
        /// </summary>
        /// <param name="webFolderParent">The webfolder which webfolders are to be listed.</param>
        /// <returns>The list of webfolders in the sub webfolder.</returns>
        public WebFolderInfo[] ListSubWebFolders(WebFolderInfo webFolderParent)
        {
            var webFolderItems = ListSubWebFolderItems(webFolderParent);
            var webFolders = webFolderItems
                .Where(webFolderItem => webFolderItem.ItemType == WebFolderItemType.Folder)
                .Cast<WebFolderInfo>()
                .ToArray();
            return webFolders;
        }

        /// <summary>
        /// Lists webfiles located in a sub webfolder.
        /// </summary>
        /// <param name="webFolderParent">The webfolder which webfiles are to be listed.</param>
        /// <returns>The list of webfiles in the sub webfolder.</returns>
        public WebFileInfo[] ListSubWebFiles(WebFolderInfo webFolderParent)
        {
            var webFolderItems = ListSubWebFolderItems(webFolderParent);
            var webFiles = webFolderItems
                .Where(webFile => webFile.ItemType == WebFolderItemType.File)
                .Cast<WebFileInfo>()
                .ToArray();
            return webFiles;
        }

        /// <summary>
        /// Lists webfolderitems located in a sub webfolder.
        /// </summary>
        /// <param name="webFolderParent">The webfolder of which webfolderitems are to be listed.</param>
        /// <returns>The list of webfolderitems in the sub webfolder.</returns>
        public WebFolderItemInfo[] ListSubWebFolderItems(WebFolderInfo webFolderParent)
        {
            var lWebFolderItem = new List<WebFolderItemInfo>();
            
            try
            {
                var srvUsers = CreateUsersServiceClient();
                var responseSubFolderItems = srvUsers.ListSubFolderItems(
                    Session.Cid, webFolderParent.ResourceId);

                if (responseSubFolderItems.FolderItems != null)
                {
                    foreach (var folderItem in responseSubFolderItems.FolderItems)
                    {
                        var webFolderItem = ParseWebFolderItem(folderItem, webFolderParent);
                        if (webFolderItem != null)
                        {
                            lWebFolderItem.Add(webFolderItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }

            return lWebFolderItem.ToArray();
        }

        /// <summary>
        /// Creates a webfolder in SkyDrive's root.
        /// </summary>
        /// <param name="name">The name of the webfolder.</param>
        /// <param name="categoryType">Type of the category.</param>
        /// <param name="shareType">The ShareType of the webfolder.</param>
        /// <returns></returns>
        public WebFolderInfo CreateRootWebFolder(string name, WebFolderCategoryType categoryType, WebFolderItemShareType shareType)
        {
            try
            {
                var strCategory = String.Empty;
                switch (categoryType)
                {
                    case WebFolderCategoryType.Documents:
                    strCategory = "Document";
                    break;
                    case WebFolderCategoryType.Favorites:
                    case WebFolderCategoryType.Photos:
                        strCategory = categoryType.ToString();
                        break;
                    default:
                        throw new NotSupportedException();
                }

                var sharingLevel = HgCo.WindowsLive.SkyDrive.Services.UsersService.SharingLevel.Private;
                switch (shareType)
                {
                    case WebFolderItemShareType.Public:
                        sharingLevel = HgCo.WindowsLive.SkyDrive.Services.UsersService.SharingLevel.Public;
                        break;
                    case WebFolderItemShareType.MyNetwork:
                        sharingLevel = HgCo.WindowsLive.SkyDrive.Services.UsersService.SharingLevel.PublicUnlisted;
                        break;
                    case WebFolderItemShareType.PeopleSelected:
                        sharingLevel = HgCo.WindowsLive.SkyDrive.Services.UsersService.SharingLevel.Shared;
                        break;
                    case WebFolderItemShareType.Private:
                        sharingLevel = HgCo.WindowsLive.SkyDrive.Services.UsersService.SharingLevel.Private;
                        break;
                    default:
                        throw new NotSupportedException();
                }

                var srvUsers = CreateUsersServiceClient();
                var responseFolderItem = srvUsers.CreateRootFolder(
                    Session.Cid, 
                    name,
                    strCategory, 
                    sharingLevel);
                
                var webFolder = (WebFolderInfo)ParseWebFolderItem(responseFolderItem, null);
                return webFolder;
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Creates a webfolder in a sub webfolder.
        /// </summary>
        /// <param name="name">The name of the webfolder.</param>
        /// <param name="webFolderParent">The webfolder where the new webfolder is to be created.</param>
        /// <returns></returns>
        public WebFolderInfo CreateSubWebFolder(string name, WebFolderInfo webFolderParent)
        {
            try
            {
                var srvUsers = CreateUsersServiceClient();
                var responseFolderItem = srvUsers.CreateSubFolder(
                    Session.Cid,
                    webFolderParent.ResourceId,
                    name);
                
                var webFolder = (WebFolderInfo)ParseWebFolderItem(responseFolderItem, webFolderParent);
                return webFolder;
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes a webfolder.
        /// </summary>
        /// <param name="webFolder">The webfolder to be deleted.</param>
        public void DeleteWebFolder(WebFolderInfo webFolder)
        {
            try
            {
                var srvUsers = CreateUsersServiceClient();
                srvUsers.DeleteFolder(
                    Session.Cid,
                    webFolder.ResourceId);
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Deletes the web file.
        /// </summary>
        /// <param name="webFile">The web file.</param>
        public void DeleteWebFile(WebFileInfo webFile)
        {
            try
            {
                var srvUsers = CreateUsersServiceClient();
                srvUsers.DeleteFile(
                    Session.Cid,
                    webFile.ResourceId);
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Uploads a webfile to the specified webfolder.
        /// </summary>
        /// <param name="fileName">The name of the file (including path) to upload.</param>
        /// <param name="webFolderParent">The webfolder where webfile is to be uploaded.</param>
        /// <returns></returns>
        public WebFileInfo UploadWebFile(string fileName, WebFolderInfo webFolderParent)
        {
            FileInfo fiWebFile = new FileInfo(fileName);
            if (!fiWebFile.Exists)
                throw new FileNotFoundException("File to upload cannot be found!", fiWebFile.FullName);

            try
            {
                var srvUsers = CreateUsersServiceClient();
                srvUsers.UploadProgressChanged += (sender, e) =>
                {
                    OnUploadWebFileProgressChanged(new UploadWebFileProgressChangedEventArgs(
                        e.BytesSent,
                        e.TotalBytesToSend));
                };
                var folderItem = srvUsers.UploadFile(
                    Session.Cid,
                    webFolderParent.ResourceId,
                    fileName);
                var webFile = (WebFileInfo)ParseWebFolderItem(folderItem, webFolderParent);
                return webFile;
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }
            finally
            {
            }
        }

        /// <summary>
        /// Downloads a webfile.
        /// </summary>
        /// <param name="webFile">The webfile to download.</param>
        /// <returns>A readable stream that contains the webfile's content.</returns>
        public Stream DownloadWebFile(WebFileInfo webFile)
        {
            Stream stream = null;

            try
            {
                if (webFile.ShareType == WebFolderItemShareType.Public)
                {
                    var srvUsers = CreateUsersServiceClient();
                    var folderItem = srvUsers.GetFile(Session.Cid, webFile.ResourceId);

                    var uriDownload = new Uri(folderItem.Content.Url);
                    var wcHttp = CreateHttpWebClient();
                    stream = wcHttp.DownloadStream(uriDownload);
                }
                else
                {
                    var wcWebDav = CreateWebDavWebClient();
                    var uriWebDav = GetWebFolderItemWebDavUrl(webFile);
                    stream = wcWebDav.DownloadResource(uriWebDav);
                }
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }

            return stream;
        }

        /// <summary>
        /// Renames a webfolderitem.
        /// </summary>
        /// <param name="webFolderItem">The webfolderitem to be renamed.</param>
        /// <param name="newName">The new name.</param>
        /// <returns></returns>
        public WebFolderItemInfo RenameWebFolderItem(WebFolderItemInfo webFolderItem, string newName)
        {
            try
            {
                var srvUsers = CreateUsersServiceClient();
                
                FolderItemResponse folderItem = null;
                if (webFolderItem.ItemType == WebFolderItemType.Folder)
                {
                    folderItem = srvUsers.RenameFolder(
                        Session.Cid,
                        webFolderItem.ResourceId,
                        newName);
                }
                else
                {
                    folderItem = srvUsers.RenameFile(
                        Session.Cid,
                        webFolderItem.ResourceId,
                        newName);
                }
                
                webFolderItem = ParseWebFolderItem(
                    folderItem,
                    new WebFolderInfo
                    {
                        ShareType = webFolderItem.ShareType,
                        WebDavHost = webFolderItem.WebDavHost
                    });
                return webFolderItem;
            }
            catch (Exception ex)
            {
                throw new OperationFailedException(ex.Message, ex);
            }
        }

        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="E:UploadWebFileProgressChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="UploadWebFileProgressChangedEventArgs"/> instance containing the event data.</param>
        protected void OnUploadWebFileProgressChanged(UploadWebFileProgressChangedEventArgs e)
        {
            if (UploadWebFileProgressChanged != null)
                UploadWebFileProgressChanged(this, e);
        }

        #endregion
    }
}
