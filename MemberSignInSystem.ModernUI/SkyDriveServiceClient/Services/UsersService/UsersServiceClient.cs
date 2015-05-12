using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using HgCo.WindowsLive.SkyDrive.Support.Net;
using HgCo.WindowsLive.SkyDrive.Support.Net.WebDav;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersServiceClient
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly WlidClient WlidClient = new WlidClient();

        /// <summary>
        /// Occurs when an upload operation successfully transfers some or all of the data.
        /// </summary>
        /// <remarks>
        /// This event is raised each time upload values make progress.
        /// This event is raised when uploads are started by calling UploadFile method.
        /// </remarks>
        public event EventHandler<WebUploadProgressChangedEventArgs> UploadProgressChanged;

        /// <summary>
        /// Gets or sets authentication information for the SOAP Web client.
        /// </summary>
        /// <value>
        /// An <see cref="ICredentials"/> that contains the authentication credentials 
        /// associated with the SOAP Web Client. The default is null.
        /// </value>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets proxy information for <see cref="UsersServiceClient"/>.
        /// </summary>
        /// <value>The <see cref="IWebProxy"/> object to use to proxy the <see cref="UsersServiceClient"/></value>
        public IWebProxy Proxy { get; set; }
        
        /// <summary>
        /// Gets the session.
        /// </summary>
        public UsersServiceSession Session { get; private set; }

        /// <summary>
        /// Gets or sets the time-out value in milliseconds for HTTP requests.
        /// </summary>
        /// <value>The number of milliseconds to wait before a request times out. The default is 100,000 milliseconds (100 seconds).</value>
        public int Timeout { get; set; }

        /// <summary>
        /// Initializes the <see cref="UsersServiceClient"/> class.
        /// </summary>
        static UsersServiceClient()
        {
            // Authentication module can be (re-)registered any times
            WebAuthenticationManager.Register(WlidClient);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersServiceClient"/> class.
        /// </summary>
        public UsersServiceClient() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersServiceClient"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public UsersServiceClient(UsersServiceSession session)
        {
            Session = session != null ? session : new UsersServiceSession();
        }

        /// <summary>
        /// Logs on.
        /// </summary>
        /// <param name="cid">The cid.</param>
        public void LogOn(string cid)
        {
            Authorization authorization = null;
            try
            {
                const string challenge = "WLID1.0";
                authorization = WlidClient.Authenticate(
                    challenge,
                    (HttpWebRequest)WebRequest.Create(GetRootUri(cid)),
                    Credentials);
            }
            catch (Exception ex)
            {
                throw new LogOnFailedException(ex.Message, ex);
            }

            if (authorization != null && authorization.Complete)
            {
                Session.HttpSession.AddAuthorization(authorization);
            }
            else
            {
                throw new LogOnFailedException();
            }
        }

        /// <summary>
        /// Lists the root folder items.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <returns>The response.</returns>
        public ListFolderItemsResponse ListRootFolderItems(string cid)
        {
            var uri = GetRootUri(cid);
            var wcXml = CreateXmlWebClient();
            var response = wcXml.SendRequest<ListFolderItemsResponse>(uri);
            return response;
        }

        /// <summary>
        /// Lists the sub folder items.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="parentResourceId">The parent resource id.</param>
        /// <returns></returns>
        public ListFolderItemsResponse ListSubFolderItems(string cid, string parentResourceId)
        {
            var uri = GetFolderUri(cid, parentResourceId);
            var wcXml = CreateXmlWebClient();
            var response = wcXml.SendRequest<ListFolderItemsResponse>(uri);
            return response;
        }

        ///// <summary>
        ///// Gets the folder.
        ///// </summary>
        ///// <param name="cid">The cid.</param>
        ///// <param name="resourceId">The resource id.</param>
        ///// <returns></returns>
        //public FolderItemResponse GetFolder(string cid, string resourceId)
        //{
        //    var uri = GetFolderUri(cid, resourceId);
        //    var wcXml = CreateXmlWebClient();
        //    var response = wcXml.SendRequest<FolderItemResponse>(uri);
        //    return response;
        //}

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="resourceId">The resource id.</param>
        /// <returns></returns>
        public FolderItemResponse GetFile(string cid, string resourceId)
        {
            var uri = GetFileUri(cid, resourceId);
            var wcXml = CreateXmlWebClient();
            var response = wcXml.SendRequest<FolderItemResponse>(uri);
            return response;
        }

        /// <summary>
        /// Creates the root folder.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="name">The name.</param>
        /// <param name="category">The category.</param>
        /// <param name="sharingLevel">The sharing level.</param>
        /// <returns></returns>
        public FolderItemResponse CreateRootFolder(string cid, string name, string category, SharingLevel sharingLevel)
        {
            var uri = GetRootUri(cid);
            var request = new CreateRootFolderRequest
            {
                Title = name,
                Type = "Library",
                Category = category,
                SharingLevel = sharingLevel
            };
            var wcXml = CreateXmlWebClient();
            var response = wcXml.SendRequest<FolderItemResponse>(
                WebRequestMethods.Http.Post,
                uri,
                request);
            return response;
        }

        /// <summary>
        /// Creates the sub folder.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="parentResourceId">The parent resource id.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public FolderItemResponse CreateSubFolder(string cid, string parentResourceId, string name)
        {
            var uri = GetFolderUri(cid, parentResourceId);
            var request = new CreateSubFolderItemRequest
            {
                Title = name,
                Type = "Folder",
                ResolveNameConflict = true
            };
            var wcXml = CreateXmlWebClient();
            var response = wcXml.SendRequest<FolderItemResponse>(
                WebRequestMethods.Http.Post,
                uri,
                request);
            return response;
        }

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="resourceId">The resource id.</param>
        public void DeleteFolder(string cid, string resourceId)
        {
            var uri = GetFolderUri(cid, resourceId);
            var wcXml = CreateXmlWebClient();
            wcXml.UploadString(
                WebDavMethods.DELETE,
                uri,
                String.Empty);
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="resourceId">The resource id.</param>
        public void DeleteFile(string cid, string resourceId)
        {
            var uri = GetFileUri(cid, resourceId);
            var wcXml = CreateXmlWebClient();
            wcXml.UploadString(
                WebDavMethods.DELETE,
                uri,
                String.Empty);
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="parentResourceId">The parent resource id.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        public FolderItemResponse UploadFile(string cid, string parentResourceId, string fileName)
        {
            var uri = GetFolderUri(cid, parentResourceId);
            var wcXml = CreateXmlWebClient();
            wcXml.ContentType = "multipart/related; type=\"application/atom+xml\"";
            
            var fileContentType = MimeHelper.GetContentType(fileName);
            var folderItemType = String.Empty;
            if (fileContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
                folderItemType = "Photo";
            else if (".url".Equals(Path.GetExtension(fileName), StringComparison.OrdinalIgnoreCase))
                folderItemType = "Favorite";
            else folderItemType = "Document";

            var strResponse = wcXml.UploadMultipartEncoded(
                WebRequestMethods.Http.Post,
                uri,
                new[]
                {
                    new HttpWebMessagePart
                    {
                        MimeVersion = "1.0",
                        ContentType = "application/atom+xml",
                        Content = Encoding.UTF8.GetBytes(XmlWebClient.SerializeRequest(
                            new CreateSubFolderItemRequest
                            {
                                Title = Path.GetFileName(fileName),
                                Type = folderItemType,
                                ResolveNameConflict = true
                            },
                            wcXml.Namespaces)),
                    },
                    new HttpWebMessagePart
                    {
                        MimeVersion = "1.0",
                        ContentType = fileContentType,
                        ContentStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    },
                });
            var response = XmlWebClient.DeserializeResponse<FolderItemResponse>(strResponse);
            return response;
        }

        /// <summary>
        /// Renames the folder.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="newName">The new name.</param>
        /// <returns></returns>
        public FolderItemResponse RenameFolder(string cid, string resourceId, string newName)
        {
            var uri = GetFolderUri(cid, resourceId);
            var responseListSubFolderItems = ListSubFolderItems(cid, resourceId);
            var request = new RenameFolderItemRequest
            {
                Id = resourceId,
                Title = newName,
                Type = responseListSubFolderItems.Type
            };
            var wcXml = CreateXmlWebClient();
            var response = wcXml.SendRequest<FolderItemResponse>(
                WebRequestMethods.Http.Put,
                uri,
                request);
            return response;
        }

        /// <summary>
        /// Renames the file.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="resourceId">The resource id.</param>
        /// <param name="newName">The new name.</param>
        /// <returns></returns>
        public FolderItemResponse RenameFile(string cid, string resourceId, string newName)
        {
            var uri = GetFileUri(cid, resourceId);

            var folderItem = GetFile(cid, resourceId);
            var request = new RenameFolderItemRequest
            {
                Id = resourceId,
                Title = newName,
                Type = folderItem.Type
            };
            var wcXml = CreateXmlWebClient();
            var response = wcXml.SendRequest<FolderItemResponse>(
                WebRequestMethods.Http.Put,
                uri,
                request);
            return response;
        }
        
        /// <summary>Gets the root folder item URI.</summary>
        /// <param name="cid">The cid.</param>
        /// <returns></returns>
        private Uri GetRootUri(string cid)
        {
            var decCid = Int64.Parse(cid, NumberStyles.HexNumber);
            var uri = new Uri(String.Format(
                CultureInfo.InvariantCulture,
                "http://cid-{0}.users.api.live.net/Users({1})/Files",
                cid,
                decCid));
            return uri;
        }

        /// <summary>
        /// Gets the sub folder item URI.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="parentResourceId">The parent resource id.</param>
        /// <returns></returns>
        private Uri GetFolderUri(string cid, string parentResourceId)
        {
            var uri = new Uri(String.Format(
                CultureInfo.InvariantCulture,
                "{0}/Folders('{1}')",
                GetRootUri(cid),
                parentResourceId));
            return uri;
        }

        /// <summary>
        /// Gets the file URI.
        /// </summary>
        /// <param name="cid">The cid.</param>
        /// <param name="resourceId">The resource id.</param>
        /// <returns></returns>
        private Uri GetFileUri(string cid, string resourceId)
        {
            var uri = new Uri(String.Format(
                CultureInfo.InvariantCulture,
                "{0}/Files('{1}')",
                GetRootUri(cid),
                resourceId));
            return uri;
        }

        /// <summary>Creates the XML web client.</summary>
        /// <returns></returns>
        private XmlWebClient CreateXmlWebClient()
        {
            var wcXml = new XmlWebClient(Session.HttpSession);
            wcXml.Accept = "application/atom+xml";
            wcXml.AllowAutoRedirect = true;
            wcXml.ContentType = "application/atom+xml";
            if (Credentials != null)
            {
                wcXml.Credentials = Credentials;
                wcXml.Headers["AppId"] = "1140851978";
            }
            wcXml.Namespaces.Add("live", "http://api.live.com/schemas");
            wcXml.Proxy = Proxy;
            wcXml.Timeout = Timeout;
            //wcXml.UserAgent = "Windows Live COMA API 4.0";
            wcXml.UploadProgressChanged += UploadProgressChanged;
            return wcXml;
        }
    }
}
