using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.Soap
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class SoapWebClient
    {
        #region Constants

        /// <summary>
        /// 
        /// </summary>
        private static readonly Regex SoapResponseRegex = new Regex("^(?i:<(\\w+:)?Envelope[^>]*>\\s*<(\\w+:)?Body>\\s*(?<Response>.+)\\s*</(\\w+:)?Body>\\s*</(\\w+:)?Envelope>)$");

        #endregion

        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private readonly HttpWebClient wcHttp;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the type of decompression that is used.
        /// </summary>
        /// <value>The type of decompression that is used. The default value is GZip.</value>
        public DecompressionMethods AutomaticDecompression { get; set; }
        
        /// <summary>
        /// Gets or sets authentication information for the SOAP Web client.
        /// </summary>
        /// <value>
        /// An <see cref="ICredentials"/> that contains the authentication credentials 
        /// associated with the SOAP Web Client. The default is null.
        /// </value>
        public ICredentials Credentials 
        { 
            get { return wcHttp.Credentials; }
            set { wcHttp.Credentials = value; } 
        }

        /// <summary>
        /// Gets or sets proxy information for <see cref="SoapWebClient"/>.
        /// </summary>
        /// <value>The <see cref="IWebProxy"/> object to use to proxy the <see cref="SoapWebClient"/></value>
        public IWebProxy Proxy
        {
            get { return wcHttp.Proxy; }
            set { wcHttp.Proxy = value; }
        }

        /// <summary>
        /// Gets the session.
        /// </summary>
        public SoapWebSession Session { get; private set; }

        /// <summary>
        /// Gets or sets the time-out value in milliseconds for HTTP requests.
        /// </summary>
        /// <value>The number of milliseconds to wait before a request times out. The default is 100,000 milliseconds (100 seconds).</value>
        public int Timeout
        {
            get { return wcHttp.Timeout; }
            set { wcHttp.Timeout = value; }
        }

        /// <summary>
        /// Gets or sets the URI of the SOAP Web service the client is requesting.
        /// </summary>
        /// <value>The URI of the SOAP Web service.</value>
        public Uri Uri { get; set; }

        /// <summary>
        /// Gets the namespaces.
        /// </summary>
        public XmlSerializerNamespaces Namespaces { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SoapWebClient"/> class.
        /// </summary>
        protected SoapWebClient() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SoapWebClient"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        protected SoapWebClient(SoapWebSession session)
        {
            Session = session != null ? session : new SoapWebSession();

            wcHttp = new HttpWebClient(Session.HttpSession)
            {
                Accept = "text/xml",
                AllowAutoRedirect = true,
                ContentType = "text/xml; charset=utf-8",
                AutomaticDecompression = AutomaticDecompression,
            };
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        protected virtual TResponse SendRequest<TResponse>(SoapWebRequest request)
            where TResponse : SoapWebResponse
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            wcHttp.Headers["SOAPAction"] = request.SoapAction;

            var strRequest = SerializeRequest(request, Namespaces);
            var strResponse = wcHttp.UploadString(Uri, strRequest);
            var response = DeserializeResponse<TResponse>(strResponse);
            return response;
        }

        /// <summary>
        /// Serializes the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns></returns>
        private static string SerializeRequest(SoapWebRequest request, XmlSerializerNamespaces namespaces)
        {
            var sb = new StringBuilder();
            using (var sw = new Utf8StringWriter(sb))
            using (var xw = new XmlTextWriter(sw))
            {
                xw.WriteStartElement("s", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
                xw.WriteStartElement("s", "Body", null);

                var xs = new XmlSerializer(request.GetType());
                xs.Serialize(xw, request, namespaces);

                xw.WriteEndElement();
                xw.WriteEndElement();
            }

            return sb.ToString();
        }

        /// <summary>
        /// Deserializes the response.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="strResponse">The response in string.</param>
        /// <returns>The response.</returns>
        private static TResponse DeserializeResponse<TResponse>(string strResponse)
            where TResponse : SoapWebResponse
        {
            var response = default(TResponse);

            if (RegexHelper.IsMatch(SoapResponseRegex, strResponse))
            {
                var matchSoapResponse = RegexHelper.Match(SoapResponseRegex, strResponse);
                var sb = new StringBuilder(matchSoapResponse.Groups["Response"].Value);
                sb.Insert(0, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                using (var sr = new StringReader(sb.ToString()))
                {
                    var xs = new XmlSerializer(typeof(TResponse));
                    response = (TResponse)xs.Deserialize(sr);
                }
            }

            return response;
        }

        #endregion
    }
}
