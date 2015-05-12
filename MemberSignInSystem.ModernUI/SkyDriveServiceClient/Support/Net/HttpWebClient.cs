using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace HgCo.WindowsLive.SkyDrive.Support.Net
{
    /// <summary>
    /// Provides methods for sending data to and receiving data from a resource identified by a URI via HTTP, 
    /// while maintaining session (cookies).
    /// </summary>
    public class HttpWebClient
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private static readonly PassportClient PassportClient = new PassportClient();

        #endregion

        #region Events

        /// <summary>
        /// Occurs when an upload operation successfully transfers some or all of the data.
        /// </summary>
        /// <remarks>
        /// This event is raised each time upload values make progress.
        /// This event is raised when uploads are started using any of the following methods:
        /// - UploadString
        /// - UploadData
        /// - UploadStream
        /// - UploadValuesUrlEncoded
        /// - UploadValuesMultipartEncoded
        /// </remarks>
        public event EventHandler<WebUploadProgressChangedEventArgs> UploadProgressChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the Accept HTTP header,
        /// which specifies the MIME types that are acceptable for the response.
        /// </summary>
        /// <value>The value of the Accept HTTP header.</value>
        public string Accept { get; set; }

        /// <summary>
        /// Gets or sets the Accept-Langauge HTTP header, 
        /// which specifies that natural languages that are preferred for the response.
        /// </summary>
        /// <value>The value of the Accept-Langauge HTTP header.</value>
        public string AcceptLanguage { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether web client should follow redirection responses.
        /// </summary>
        /// <value>
        /// <c>true</c> if web client should automatically follow redirection responses from the Internet resource; 
        /// otherwise, <c>false</c>. The default value is true.
        /// </value>
        public bool AllowAutoRedirect { get; set; }

        /// <summary>
        /// Gets or sets the type of decompression that is used.
        /// </summary>
        /// <value>The type of decompression that is used. The default value is GZip.</value>
        public DecompressionMethods AutomaticDecompression { get; set; }

        /// <summary>
        /// Gets or sets the value of the Content-type HTTP header,
        /// which specifies the MIME type of the accompanying body data.
        /// </summary>
        /// <value>The value of the Content-type HTTP header. The default value is null.</value>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets authentication information for the HTTP Web client.
        /// </summary>
        /// <value>
        /// An <see cref="ICredentials"/> that contains the authentication credentials 
        /// associated with the HTTP Web Client. The default is null.
        /// </value>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// Gets the collection of the name/value pairs that make up the HTTP headers.
        /// </summary>
        public WebHeaderCollection Headers { get; private set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to make a persistent connection to the Internet resource.
        /// </summary>
        /// <value>
        /// <c>true</c> if the request to the Internet resource should contain a Connection HTTP header 
        /// with the value Keep-alive; otherwise, <c>false</c>. The default is true.
        /// </value>
        public bool KeepAlive { get; set; }

        /// <summary>
        /// Gets or sets proxy information for <see cref="HttpWebClient"/>.
        /// </summary>
        /// <value>The <see cref="IWebProxy"/> object to use to proxy the <see cref="HttpWebClient"/></value>
        public IWebProxy Proxy { get; set; }

        /// <summary>
        /// Gets or sets the value of the Referer HTTP header,
        /// which specifies the URI of the resource from which the request URI was obtained.
        /// </summary>
        /// <value>The value of the Referer HTTP header. The default value is null.</value>
        public string Referer { get; set; }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>The session.</value>
        public HttpWebSession Session { get; private set; }

        /// <summary>
        /// Gets or sets the time-out value in milliseconds for HTTP requests.
        /// </summary>
        /// <value>The number of milliseconds to wait before a request times out. The default is 100,000 milliseconds (100 seconds).</value>
        public int Timeout { get; set; }

        /// <summary>
        /// Gets or sets the value of User-agent HTTP header,
        /// which specifies information about the client agent.
        /// </summary>
        /// <value>The User-agent HTTP header.</value>
        public string UserAgent { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="HttpWebClient"/> class.
        /// </summary>
        static HttpWebClient()
        {
            // Authentication module can be (re-)registered any times
            WebAuthenticationManager.Register(PassportClient);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebClient"/> class.
        /// </summary>
        public HttpWebClient() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebClient"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public HttpWebClient(HttpWebSession session)
        {
            Session = session != null ? session : new HttpWebSession();

            Accept = "*/*";
            AcceptLanguage = "en-US";
            AllowAutoRedirect = true;
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            Headers = new WebHeaderCollection();
            KeepAlive = true;
            Timeout = 100000;

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.ServerCertificateValidationCallback +=
                new RemoteCertificateValidationCallback(ValidateServerCertificateCallback);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Downloads the requested resource as a string.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>The requested resource.</returns>
        public string DownloadString(Uri address)
        {
            return DownloadString(
                WebRequestMethods.Http.Get,
                address);
        }
        /// <summary>
        /// Downloads the requested resource as a string.
        /// </summary>
        /// <param name="method">The method to use to contact the resource.</param>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>The requested resource.</returns>
        public string DownloadString(string method, Uri address)
        {
            using (var webResponse = SendHttpWebRequest(
                address,
                webRequest => webRequest.Method = method))
            {
                var strResponse = ReadString(webResponse);
                return strResponse;
            }
        }

        /// <summary>
        /// Downloads the requested resource as a byte array.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>The requested resource.</returns>
        public byte[] DownloadData(Uri address)
        {
            return DownloadData(
                WebRequestMethods.Http.Get,
                address);
        }
        /// <summary>
        /// Downloads the requested resource as a byte array.
        /// </summary>
        /// <param name="method">The method to use to contact the resource.</param>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>The requested resource.</returns>
        public byte[] DownloadData(string method, Uri address)
        {
            using (var webResponse = SendHttpWebRequest(
                address,
                webRequest => webRequest.Method = method))
            {
                var dataResponse = ReadData(webResponse);
                return dataResponse;
            }
        }

        /// <summary>
        /// Downloads the requested resource.
        /// </summary>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>The requested resource.</returns>
        public Stream DownloadStream(Uri address)
        {
            return DownloadStream(
                WebRequestMethods.Http.Get,
                address);
        }
        /// <summary>
        /// Downloads the requested resource.
        /// </summary>
        /// <param name="method">The method to use to contact the resource.</param>
        /// <param name="address">The address of the resource to download.</param>
        /// <returns>The requested resource.</returns>
        public Stream DownloadStream(string method, Uri address)
        {
            var webResponse = SendHttpWebRequest(
                address, 
                webRequest => webRequest.Method = method);
            var srResponse = webResponse.GetResponseStream();
            return srResponse;
        }

        /// <summary>
        /// Uploads the specified string to the specified resource, using the POST method.
        /// </summary>
        /// <param name="address">The address of the resource to upload to.</param>
        /// <param name="data">The data to be uploaded.</param>
        /// <returns>The response from the resource.</returns>
        public string UploadString(Uri address, string data)
        {
            return UploadString(
                WebRequestMethods.Http.Post,
                address,
                data);
        }

        /// <summary>
        /// Uploads the specified string to the specified resource.
        /// </summary>
        /// <param name="method">The method to use to contact the resource.</param>
        /// <param name="address">The address of the resource to upload to.</param>
        /// <param name="data">The data to be uploaded.</param>
        /// <returns>The response from the resource.</returns>
        public string UploadString(string method, Uri address, string data)
        {
            var content = Encoding.UTF8.GetBytes(data);
            var dataResponse = UploadData(method, address, content);
            // This is the correct way, as response contains some extra 3 characters at the beginning
            // and Encoding.UTF8.GetString does not remove it, except StreamReader!!
            using (var ms = new MemoryStream(dataResponse))
            using (var sr = new StreamReader(ms))
            {
                var strResponse = sr.ReadToEnd();
                return strResponse;
            }
        }

        /// <summary>
        /// Uploads the specified data to the specified resource.
        /// </summary>
        /// <param name="address">The address of the resource to upload to.</param>
        /// <param name="data">The data to be uploaded.</param>
        /// <returns>The response from the resource.</returns>
        public byte[] UploadData(Uri address, byte[] data)
        {
            return UploadData(WebRequestMethods.Http.Post, address, data);
        }

        /// <summary>
        /// Uploads the specified data to the specified resource.
        /// </summary>
        /// <param name="method">The method to use to contact the resource.</param>
        /// <param name="address">The address of the resource to upload to.</param>
        /// <param name="data">The data to be uploaded.</param>
        /// <returns>The response from the resource.</returns>
        public byte[] UploadData(string method, Uri address, byte[] data)
        {
            using (var webResponse = SendHttpWebRequest(
                address,
                webRequest =>
                {
                    webRequest.Method = method;

                    webRequest.ContentLength = data.Length;

                    var offset = 0;
                    using (var sw = webRequest.GetRequestStream())
                    {
                        var chunk = Math.Min(data.Length - offset, 64 * 1024);
                        sw.Write(data, offset, chunk);
                        offset += chunk;

                        OnUploadProgressChanged(new WebUploadProgressChangedEventArgs(
                            offset,
                            data.Length));
                    }
                }))
            {
                var dataResponse = ReadData(webResponse);
                return dataResponse;
            }
        }

        /// <summary>
        /// Uploads the specified data to the specified resource.
        /// </summary>
        /// <param name="address">The address of the resource to upload to.</param>
        /// <param name="data">The data to be uploaded.</param>
        public void UploadStream(Uri address, Stream data)
        {
            UploadStream(
                WebRequestMethods.Http.Post,
                address,
                data);
        }
        /// <summary>
        /// Uploads the specified data to the specified resource.
        /// </summary>
        /// <param name="method">The method to use to contact the resource.</param>
        /// <param name="address">The address of the resource to upload to.</param>
        /// <param name="data">The data to be uploaded.</param>
        public void UploadStream(string method, Uri address, Stream data)
        {
            using (var webResponse = SendHttpWebRequest(
                address,
                webRequest =>
                {
                    webRequest.Method = method;

                    if (data != null)
                    {
                        var contentLength = webRequest.ContentLength = data.Length;
                        using (var sw = webRequest.GetRequestStream())
                        {
                            long contentLengthSent = 0;

                            var buffer = new byte[64 * 1024];
                            int count = 0;
                            while ((count = data.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                sw.Write(buffer, 0, count);
                                contentLengthSent += count;

                                OnUploadProgressChanged(new WebUploadProgressChangedEventArgs(
                                    contentLengthSent,
                                    contentLength));
                            }
                        }
                    }
                }))
            {
            }
        }

        /// <summary>
        /// Uploads the multipart encoded.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="address">The address.</param>
        /// <param name="messageParts">The message parts.</param>
        /// <returns></returns>
        public string UploadMultipartEncoded(string method, Uri address, HttpWebMessagePart[] messageParts)
        {
            // TODO: It seems that boundary should be an input parameter,
            // because Content-Type cannot be set externally without it! See a bit below!
            var mpBoundary = String.Format(
                CultureInfo.InvariantCulture,
                "---------------------------{0:x}",
                DateTime.Now.Ticks);
            var mpNewLine = Encoding.UTF8.GetBytes("\r\n");
            var mpLastBoundary = Encoding.UTF8.GetBytes(String.Format(
                CultureInfo.InvariantCulture,
                "--{0}--\r\n", mpBoundary));
            var dicMultiPartHeader = new Dictionary<HttpWebMessagePart, byte[]>(messageParts.Length);
            var dicMultiPartBody = new Dictionary<HttpWebMessagePart, byte[]>(messageParts.Length);

            long contentLength = mpLastBoundary.Length;
            foreach (var messagePart in messageParts)
            {
                var mpHeader = GenerateMultipartHeaderBytes(messagePart, mpBoundary);
                dicMultiPartHeader[messagePart] = mpHeader;
                contentLength += mpHeader.Length;
                
                if (messagePart.ContentStream != null)
                {
                    contentLength += messagePart.ContentStream.Length;
                }
                else if (messagePart.Content != null)
                {
                    dicMultiPartBody[messagePart] = messagePart.Content;
                    contentLength += messagePart.Content.Length;
                }
                contentLength += mpNewLine.Length;
            }

            using (var webResponse = SendHttpWebRequest(
                address,
                webRequest =>
                {
                    webRequest.Method = method;
                    webRequest.ContentType = String.Format(
                        CultureInfo.InvariantCulture,
                        "{0}; boundary=\"{1}\"",
                        ContentType,
                        mpBoundary);
                    webRequest.ContentLength = contentLength;

                    using (var sw = webRequest.GetRequestStream())
                    {
                        long contentLengthSent = 0;

                        foreach (var messagePart in messageParts)
                        {
                            var mpHeader = dicMultiPartHeader[messagePart];
                            sw.Write(mpHeader, 0, mpHeader.Length);
                            contentLengthSent += mpHeader.Length;

                            if (messagePart.ContentStream != null)
                            {
                                using (var sr = messagePart.ContentStream)
                                {
                                    var count = 0;
                                    var buffer = new byte[64 * 1024];
                                    while ((count = sr.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        sw.Write(buffer, 0, count);
                                        contentLengthSent += count;

                                        OnUploadProgressChanged(new WebUploadProgressChangedEventArgs(
                                            contentLengthSent,
                                            contentLength));
                                    }
                                    sw.Write(mpNewLine, 0, mpNewLine.Length);
                                    contentLengthSent += mpNewLine.Length;
                                }
                            }
                            else
                            {
                                var mpBody = dicMultiPartBody[messagePart];
                                sw.Write(mpBody, 0, mpBody.Length);
                                sw.Write(mpNewLine, 0, mpNewLine.Length);
                                contentLengthSent += mpBody.Length + mpNewLine.Length;
                            }

                            OnUploadProgressChanged(new WebUploadProgressChangedEventArgs(
                                contentLengthSent,
                                contentLength));
                        }

                        sw.Write(mpLastBoundary, 0, mpLastBoundary.Length);
                        contentLengthSent += mpLastBoundary.Length;

                        OnUploadProgressChanged(new WebUploadProgressChangedEventArgs(
                            contentLengthSent,
                            contentLength));

                        sw.Flush();
                    }
                }))
            {
                var strResponse = ReadString(webResponse);
                return strResponse;
            }
        }

        /// <summary>
        /// Uploads a name/value collection in URL encoded format to a resource with the specified URI.
        /// </summary>
        /// <param name="address">The address of the resource to upload to.</param>
        /// <param name="parameters">The parameters to upload to the resourece.</param>
        /// <returns>The response from the resource.</returns>
        public string UploadFormUrlEncoded(Uri address, NameValueCollection parameters)
        {
            using (var webResponse = SendHttpWebRequest(
                address,
                webRequest =>
                {
                    webRequest.Method = WebRequestMethods.Http.Post;
                    webRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";

                    var sbContent = new StringBuilder();
                    for (int idxParameter = 0; idxParameter < parameters.Count; idxParameter++)
                        if (idxParameter == 0)
                            sbContent.AppendFormat("{0}={1}",
                                HttpUtility.UrlEncodeUnicode(parameters.GetKey(idxParameter)),
                                HttpUtility.UrlEncodeUnicode(parameters[idxParameter]));
                        else sbContent.AppendFormat("&{0}={1}",
                                HttpUtility.UrlEncodeUnicode(parameters.GetKey(idxParameter)),
                                HttpUtility.UrlEncodeUnicode(parameters[idxParameter]));
                    var content = Encoding.UTF8.GetBytes(sbContent.ToString());
                    webRequest.ContentLength = content.Length;

                    using (var sw = webRequest.GetRequestStream())
                    {
                        sw.Write(content, 0, content.Length);
                    }
                }))
            {
                var strResponse = ReadString(webResponse);
                return strResponse;
            }
        }

        /// <summary>
        /// Uploads a name/value collection in Multipart encoded format to a resource with the specified URI.
        /// </summary>
        /// <param name="address">The address of the resource to upload to.</param>
        /// <param name="parameters">The parameters to upload to the resourece.</param>
        /// <returns>The response from the resource.</returns>
        public string UploadFormMultipartEncoded(Uri address, Dictionary<string, object> parameters)
        {
            var mpBoundary = String.Format(
                CultureInfo.InvariantCulture,
                "---------------------------{0:x}",
                DateTime.Now.Ticks);
            var mpNewLine = Encoding.UTF8.GetBytes("\r\n");
            var mpLastBoundary = Encoding.UTF8.GetBytes(String.Format(
                CultureInfo.InvariantCulture,
                "--{0}--\r\n", mpBoundary));
            var dicMultiPartFormHeader = new Dictionary<string, byte[]>(parameters.Count);
            var dicMultiPartFormBody = new Dictionary<string, byte[]>(parameters.Count);

            long contentLength = mpLastBoundary.Length;
            foreach (var parameterKey in parameters.Keys)
            {
                var parameterValue = parameters[parameterKey];
                var fiParameter = parameterValue as FileInfo;
                if (fiParameter != null)
                {
                    var mpFormHeader = GenerateMultiPartFormFieldHeaderBytes(parameterKey, fiParameter.Name, mpBoundary);
                    dicMultiPartFormHeader[parameterKey] = mpFormHeader;
                    contentLength += mpFormHeader != null ? mpFormHeader.Length : 0;
                    contentLength += fiParameter.Length + mpNewLine.Length;
                }
                else
                {
                    var mpFormHeader = GenerateMultiPartFormFieldHeaderBytes(parameterKey, mpBoundary);
                    dicMultiPartFormHeader[parameterKey] = mpFormHeader;

                    var mpFormBodyBytes = GenerateMultiPartFormFieldContentBytes(parameterValue);
                    dicMultiPartFormBody[parameterKey] = mpFormBodyBytes;

                    contentLength += mpFormHeader != null ? mpFormHeader.Length : 0;
                    contentLength += mpFormBodyBytes != null ? mpFormBodyBytes.Length : 0;
                }
            }

            using (var webResponse = SendHttpWebRequest(
                address,
                webRequest =>
                {
                    webRequest.Method = WebRequestMethods.Http.Post;
                    webRequest.ContentType = String.Format(
                        CultureInfo.InvariantCulture,
                        "multipart/form-data; boundary=\"{0}\"",
                        mpBoundary);
                    webRequest.ContentLength = contentLength;

                    using (var sw = webRequest.GetRequestStream())
                    {
                        long contentLengthSent = 0;

                        foreach (var parameterKey in parameters.Keys)
                        {
                            var parameterValue = parameters[parameterKey];
                            var fiParameter = parameterValue as FileInfo;
                            if (fiParameter != null)
                            {
                                var mpFormHeader = dicMultiPartFormHeader[parameterKey];
                                sw.Write(mpFormHeader, 0, mpFormHeader.Length);
                                contentLengthSent += mpFormHeader.Length;

                                using (var fs = fiParameter.OpenRead())
                                {
                                    var count = 0;
                                    var buffer = new byte[64 * 1024];
                                    while ((count = fs.Read(buffer, 0, buffer.Length)) > 0)
                                    {
                                        sw.Write(buffer, 0, count);
                                        contentLengthSent += count;

                                        OnUploadProgressChanged(new WebUploadProgressChangedEventArgs(
                                            contentLengthSent,
                                            contentLength));
                                    }
                                    sw.Write(mpNewLine, 0, mpNewLine.Length);
                                    contentLengthSent += mpNewLine.Length;
                                }
                            }
                            else
                            {
                                var mpFormHeader = dicMultiPartFormHeader[parameterKey];
                                var mpFormBody = dicMultiPartFormBody[parameterKey];

                                sw.Write(mpFormHeader, 0, mpFormHeader.Length);
                                sw.Write(mpFormBody, 0, mpFormBody.Length);
                                contentLengthSent += mpFormHeader.Length + mpFormBody.Length;
                            }

                            OnUploadProgressChanged(new WebUploadProgressChangedEventArgs(
                                contentLengthSent,
                                contentLength));
                        }

                        sw.Write(mpLastBoundary, 0, mpLastBoundary.Length);
                        contentLengthSent += mpLastBoundary.Length;

                        OnUploadProgressChanged(new WebUploadProgressChangedEventArgs(
                            contentLengthSent,
                            contentLength));
                    }
                }))
            {
                var strResponse = ReadString(webResponse);
                return strResponse;
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Raises the <see cref="E:UploadProgressChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="WebUploadProgressChangedEventArgs"/> instance containing the event data.</param>
        protected void OnUploadProgressChanged(WebUploadProgressChangedEventArgs e)
        {
            if (UploadProgressChanged != null)
                UploadProgressChanged(this, e);
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Verifies the remote Secure Sockets Layer (SSL) certificate used for authentication.
        /// </summary>
        /// <param name="sender">An object that contains state information for this validation.</param>
        /// <param name="cert">The certificate used to authenticate the remote party.</param>
        /// <param name="chain">The chain of certificate authorities associated with the remote certificate.</param>
        /// <param name="error">One or more errors associated with the remote certificate.</param>
        /// <returns>A boolean value that determines whether the specified certificate is accepted for authentication.</returns>
        private static bool ValidateServerCertificateCallback(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors error)
        {
            return true;
        }

        private HttpWebResponse SendHttpWebRequest(Uri address, Action<HttpWebRequest> adjustWebRequestCallback)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(address);
            webRequest.Headers = Headers;
            webRequest.Headers[HttpRequestHeader.AcceptLanguage] = AcceptLanguage;

            webRequest.Accept = Accept;
            webRequest.AllowAutoRedirect = false;
            webRequest.AutomaticDecompression = AutomaticDecompression;
            webRequest.ContentType = ContentType;
            webRequest.Credentials = Credentials;
            webRequest.KeepAlive = KeepAlive;
            webRequest.Method = WebRequestMethods.Http.Get;
            webRequest.Proxy = Proxy;
            webRequest.Referer = Referer;
            webRequest.Timeout = Timeout;
            webRequest.UserAgent = UserAgent;

            Session.Apply(webRequest);

            if (adjustWebRequestCallback != null)
            {
                adjustWebRequestCallback(webRequest);
            }

            HttpWebResponse webResponse = null;
            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
            }
            catch (WebException ex)
            {
                webResponse = (HttpWebResponse)ex.Response;
                if (webResponse == null) throw;
                if (webResponse.StatusCode != HttpStatusCode.Unauthorized)
                {
                    webResponse.Close();
                    throw;
                }
            }

            Session.Read(webResponse);

            var authenticationChallenge = 
                webResponse.Headers[HttpResponseHeader.WwwAuthenticate] ??
                webResponse.Headers[HttpResponseHeader.ProxyAuthenticate];
            if (!String.IsNullOrEmpty(authenticationChallenge))
            {
                webResponse.Close();

                var authorization = WebAuthenticationManager.Authenticate(authenticationChallenge, webRequest, Credentials);
                if (authorization != null && authorization.Complete)
                {
                    if (webResponse.Headers[HttpResponseHeader.WwwAuthenticate] != null)
                    {
                        //Headers[HttpRequestHeader.Authorization] = authorization.Message;
                        Session.AddAuthorization(authorization);
                    }
                    else
                    {
                        //Headers[HttpRequestHeader.ProxyAuthorization] = authorization.Message;
                        Session.AddProxyAuthorization(authorization);
                    }
                    webResponse = SendHttpWebRequest(address, adjustWebRequestCallback);
                }
                else throw new OperationFailedException();
            }
            else
            {
                while (AllowAutoRedirect && webResponse.StatusCode == HttpStatusCode.Found)
                {
                    var uriLocationRedirected = new Uri(webResponse.Headers[HttpResponseHeader.Location]);
                    webResponse.Close();

                    webResponse = SendHttpWebRequest(uriLocationRedirected, adjustWebRequestCallback);
                }
            }
            
            return webResponse;
        }

        /// <summary>
        /// Reads the content of a web response as a string.
        /// </summary>
        /// <param name="webResponse">The web response to read.</param>
        /// <returns>The web response's content in string.</returns>
        private static string ReadString(WebResponse webResponse)
        {
            string strResponse = null;
            var encoding = Encoding.UTF8;
            using (var sr = new StreamReader(webResponse.GetResponseStream(), encoding))
            {
                strResponse = sr.ReadToEnd();
            }
            return strResponse;
        }

        /// <summary>
        /// Reads the content of a web response.
        /// </summary>
        /// <param name="webResponse">The web response to read.</param>
        /// <returns>The web response's content.</returns>
        public static byte[] ReadData(WebResponse webResponse)
        {
            using (var ms = new MemoryStream())
            using (var sr = webResponse.GetResponseStream())
            {
                var count = 0;
                var buffer = new byte[64 * 1024];
                while ((count = sr.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, count);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Generates the header bytes of a form field for a multi part request.
        /// </summary>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="multipartBoundary">The boundary of the multi part request.</param>
        /// <returns>The list of bytes representing the form field's header.</returns>
        private static byte[] GenerateMultiPartFormFieldHeaderBytes(string fieldName, string multipartBoundary)
        {
            var sb = new StringBuilder();
            if (!String.IsNullOrEmpty(fieldName))
            {
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "--{0}", multipartBoundary));
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "Content-Disposition: form-data; name=\"{0}\"", fieldName));
                sb.AppendLine();
            }
            var mpFormHeaderString = sb.ToString();
            var mpFormHeaderBytes = Encoding.UTF8.GetBytes(mpFormHeaderString);
            return mpFormHeaderBytes;
        }

        /// <summary>
        /// Generates the header bytes of a form file field for a multi part request.
        /// </summary>
        /// <param name="fieldName">The name of the field.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="multipartBoundary">The boundary of the multi part request.</param>
        /// <returns>The list of bytes representing the form file field's header.</returns>
        private static byte[] GenerateMultiPartFormFieldHeaderBytes(string fieldName, string fileName, string multipartBoundary)
        {
            var sb = new StringBuilder();
            if (!String.IsNullOrEmpty(fieldName))
            {
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "--{0}", multipartBoundary));
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"", fieldName, fileName));
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "Content-Type: {0}", MimeHelper.GetContentType(fileName)));
                sb.AppendLine();
            }
            var mpFormHeaderString = sb.ToString();
            var mpFormHeaderBytes = Encoding.UTF8.GetBytes(mpFormHeaderString);
            return mpFormHeaderBytes;
        }

        /// <summary>
        /// Generates the multipart header bytes.
        /// </summary>
        /// <param name="messagePart">The message part.</param>
        /// <param name="multipartBoundary">The multipart boundary.</param>
        /// <returns></returns>
        private static byte[] GenerateMultipartHeaderBytes(HttpWebMessagePart messagePart, string multipartBoundary)
        {
            var sb = new StringBuilder();
            if (messagePart != null)
            {
                sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "--{0}", multipartBoundary));
                if (!String.IsNullOrEmpty(messagePart.MimeVersion))
                {
                    sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "MIME-Version: {0}", messagePart.MimeVersion));
                }
                if (!String.IsNullOrEmpty(messagePart.ContentDisposition))
                {
                    sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "Content-Disposition: {0}", messagePart.ContentDisposition));
                }
                if (!String.IsNullOrEmpty(messagePart.ContentType))
                {
                    sb.AppendLine(String.Format(CultureInfo.InvariantCulture, "Content-Type: {0}", messagePart.ContentType));
                }
                sb.AppendLine();
            }
            var mpHeaderString = sb.ToString();
            var mpHeaderBytes = Encoding.UTF8.GetBytes(mpHeaderString);
            return mpHeaderBytes;
        }
        
        /// <summary>
        /// Generates the content bytes of a form field for a multi part request.
        /// </summary>
        /// <param name="fieldValue">The value of the field.</param>
        /// <returns>The list of bytes representing the form field's content.</returns>
        private static byte[] GenerateMultiPartFormFieldContentBytes(object fieldValue)
        {
            var mpFormBodyString = String.Concat(fieldValue, "\r\n");
            var mpFormBodyBytes = Encoding.UTF8.GetBytes(mpFormBodyString);
            return mpFormBodyBytes;
        }

        #endregion
    }
}
