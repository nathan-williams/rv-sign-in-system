using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace HgCo.WindowsLive.SkyDrive.Support.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlWebClient : HttpWebClient
    {
        /// <summary>
        /// Gets the namespaces.
        /// </summary>
        public XmlSerializerNamespaces Namespaces { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWebClient"/> class.
        /// </summary>
        public XmlWebClient() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlWebClient"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public XmlWebClient(HttpWebSession session)
            : base(session)
        {
            Accept = "text/xml";
            AllowAutoRedirect = true;
            ContentType = "text/xml; charset=utf-8";
            Namespaces = new XmlSerializerNamespaces();
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="address">The address.</param>
        /// <param name="request">The request.</param>
        public virtual void SendRequest(string method, Uri address, XmlWebRequest request)
        {
            var strRequest = SerializeRequest(request, Namespaces);
            UploadString(method, address, strRequest);
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="address">The address.</param>
        /// <returns>The response.</returns>
        public virtual TResponse SendRequest<TResponse>(Uri address)
            where TResponse : XmlWebResponse
        {
            return SendRequest<TResponse>(WebRequestMethods.Http.Get, address);
        }
        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="method">The method.</param>
        /// <param name="address">The address.</param>
        /// <returns>The response.</returns>
        public virtual TResponse SendRequest<TResponse>(string method, Uri address)
            where TResponse : XmlWebResponse
        {
            var strResponse = DownloadString(method, address);
            var response = DeserializeResponse<TResponse>(strResponse);
            return response;
        }

        /// <summary>
        /// Sends the request.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="method">The method.</param>
        /// <param name="address">The address.</param>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public virtual TResponse SendRequest<TResponse>(string method, Uri address, XmlWebRequest request)
            where TResponse : XmlWebResponse
        {
            var strRequest = SerializeRequest(request, Namespaces);
            var strResponse = UploadString(method, address, strRequest);
            var response = DeserializeResponse<TResponse>(strResponse);
            return response;
        }

        /// <summary>
        /// Serializes the request.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="namespaces">The namespaces.</param>
        /// <returns>The serialized request.</returns>
        public static string SerializeRequest(XmlWebRequest request, XmlSerializerNamespaces namespaces)
        {
            var sb = new StringBuilder();
            
            if (request != null)
            {
                using (var sw = new Utf8StringWriter(sb))
                {
                    var xs = new XmlSerializer(request.GetType());
                    xs.Serialize(sw, request, namespaces);
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Deserializes the response.
        /// </summary>
        /// <typeparam name="TResponse">The type of the response.</typeparam>
        /// <param name="strResponse">The response in string.</param>
        /// <returns>The response.</returns>
        public static TResponse DeserializeResponse<TResponse>(string strResponse)
            where TResponse : XmlWebResponse
        {
            var response = default(TResponse);

            if (!String.IsNullOrEmpty(strResponse))
            {
                using (var sr = new StringReader(strResponse))
                {
                    var xs = new XmlSerializer(typeof(TResponse));
                    response = (TResponse)xs.Deserialize(sr);
                }
            }

            return response;
        }
    }
}
