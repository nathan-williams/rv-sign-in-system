using System;
using System.IO;
using System.Net;
using System.Web;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.WebDav
{
    /// <summary>
    /// 
    /// </summary>
    public class WebDavWebClient
    {
        #region Properties

        /// <summary>
        /// Gets or sets authentication information for the WebDAV Web client.
        /// </summary>
        /// <value>
        /// An <see cref="ICredentials"/> that contains the authentication credentials 
        /// associated with the WebDAV Web Client. The default is null.
        /// </value>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets proxy information for <see cref="WebDavWebClient"/>.
        /// </summary>
        /// <value>The <see cref="IWebProxy"/> object to use to proxy the <see cref="WebDavWebClient"/></value>
        public IWebProxy Proxy { get; set; }
        
        /// <summary>
        /// Gets the session.
        /// </summary>
        public HttpWebSession Session { get; private set; }

        /// <summary>
        /// Gets or sets the time-out value in milliseconds for HTTP requests.
        /// </summary>
        /// <value>The number of milliseconds to wait before a request times out. The default is 100,000 milliseconds (100 seconds).</value>
        public int Timeout { get; set; }
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDavWebClient"/> class.
        /// </summary>
        public WebDavWebClient() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebDavWebClient"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public WebDavWebClient(HttpWebSession session)
        {
            Session = session != null ?  session : new HttpWebSession();
        }

        /// <summary>Gets the resource infos.</summary>
        /// <param name="address">The address.</param>
        /// <param name="depthLevel">The depth level.</param>
        /// <returns></returns>
        public GetResourceInfosResponse GetResourceInfos(Uri address, string depthLevel)
        {
            var wcXml = CreateXmlWebClient();
            wcXml.Headers["Depth"] = depthLevel;
            var response = wcXml.SendRequest<GetResourceInfosResponse>(
                WebDavMethods.PROPFIND,
                address);
            return response;
        }

        /// <summary>Creates the collection resource.</summary>
        /// <param name="addressParent">The address parent.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string CreateCollectionResource(Uri addressParent, string name)
        {
            var uriNewCollectionResource = new Uri(String.Format(
                "{0}/{1}",
                addressParent,
                HttpUtility.HtmlEncode(name)));
            
            var wcXml = CreateXmlWebClient();
            wcXml.SendRequest(
                WebDavMethods.MKCOL,
                uriNewCollectionResource,
                null);
            
            return uriNewCollectionResource.AbsoluteUri;
        }

        /// <summary>Creates the resource.</summary>
        /// <param name="addressParent">The address parent.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public string CreateResource(Uri addressParent, string name)
        {
            var uriNewResource = new Uri(String.Format(
                "{0}/{1}",
                addressParent,
                HttpUtility.HtmlEncode(name)));

            var wcHttp = CreateHttpWebClient();
            wcHttp.UploadString(
                WebRequestMethods.Http.Put,
                uriNewResource,
                null);

            return uriNewResource.AbsoluteUri;
        }

        /// <summary>Uploads the resource.</summary>
        /// <param name="address">The address.</param>
        /// <param name="data">The data.</param>
        public void UploadResource(Uri address, Stream data)
        {
            var wcHttp = CreateHttpWebClient();
            wcHttp.UploadStream(
                WebRequestMethods.Http.Put,
                address,
                data);
        }

        /// <summary>
        /// Downloads the resource.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <returns></returns>
        public Stream DownloadResource(Uri address)
        {
            var wcXml = CreateXmlWebClient();
            wcXml.Accept = "*/*";
            var stream = wcXml.DownloadStream(address);
            return stream;
        }

        /// <summary>Moves the resource.</summary>
        /// <param name="addressOld">The address old.</param>
        /// <param name="addressNew">The address new.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public void MoveResource(Uri addressOld, Uri addressNew, bool overwrite)
        {
            var wcXml = CreateXmlWebClient();
            wcXml.Headers["Destination"] = addressNew.AbsoluteUri;
            wcXml.Headers["Overwrite"] = overwrite ? "T" : "F";
            wcXml.SendRequest(
                WebDavMethods.MOVE,
                addressOld,
                null);
        }

        /// <summary>Copies the resource.</summary>
        /// <param name="addressOld">The address old.</param>
        /// <param name="addressNew">The address new.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public void CopyResource(Uri addressOld, Uri addressNew, bool overwrite)
        {
            var wcXml = CreateXmlWebClient();
            wcXml.Headers["Destination"] = addressNew.AbsoluteUri;
            wcXml.Headers["Overwrite"] = overwrite ? "T" : "F";
            wcXml.SendRequest(
                WebDavMethods.COPY,
                addressOld,
                null);
        }

        /// <summary>Deletes the resource.</summary>
        /// <param name="address">The address.</param>
        public void DeleteResource(Uri address)
        {
            var wcXml = CreateXmlWebClient();
            wcXml.SendRequest(
                WebDavMethods.DELETE,
                address,
                null);
        }

        /// <summary>Locks the resource.</summary>
        /// <param name="address">The address.</param>
        public void LockResource(Uri address)
        {
        }

        /// <summary>Creates the XML web client.</summary>
        /// <returns></returns>
        private XmlWebClient CreateXmlWebClient()
        {
            var wcXml = new XmlWebClient(Session);
            wcXml.Accept = String.Empty;
            wcXml.AllowAutoRedirect = true;
            wcXml.ContentType = "application/xml; charset=utf-8";
            wcXml.Credentials = Credentials;
            wcXml.Proxy = Proxy;
            wcXml.Timeout = Timeout;
            return wcXml;
        }

        /// <summary>Creates the HTTP web client.</summary>
        /// <returns></returns>
        private HttpWebClient CreateHttpWebClient()
        {
            var wcHttp = new HttpWebClient(Session);
            wcHttp.Accept = String.Empty;
            wcHttp.AllowAutoRedirect = false;
            wcHttp.ContentType = "application/octet-stream; charset=utf-8";
            wcHttp.Credentials = Credentials;
            wcHttp.Proxy = Proxy;
            wcHttp.Timeout = Timeout;
            return wcHttp;
        }
    }
}
