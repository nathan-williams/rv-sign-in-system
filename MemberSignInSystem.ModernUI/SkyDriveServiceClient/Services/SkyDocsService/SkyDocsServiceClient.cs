using System;
using System.Net;
using HgCo.WindowsLive.SkyDrive.Support.Net.Soap;

namespace HgCo.WindowsLive.SkyDrive.Services.SkyDocsService
{
    /// <summary>
    /// 
    /// </summary>
    public class SkyDocsServiceClient : SoapWebClient
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly Uri SkyDocsServiceUri = new Uri("https://docs.live.net/SkyDocsService.svc");

        /// <summary>
        /// Initializes a new instance of the <see cref="SkyDocsServiceClient"/> class.
        /// </summary>
        public SkyDocsServiceClient() : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkyDocsServiceClient"/> class.
        /// </summary>
        /// <param name="session">The session.</param>
        public SkyDocsServiceClient(SoapWebSession session)
            : base (session)
        {
            Uri = SkyDocsServiceUri;
            // BUG: if compression is used, SkyDocsService sends the response compressed (above the built-in HTTP compression)!
            // Resolution: compression is switched off at all.
            AutomaticDecompression = DecompressionMethods.None;
        }

        /// <summary>
        /// Gets the product info.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public GetProductInfoResponse GetProductInfo(GetProductInfoRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = SendRequest<GetProductInfoResponse>(request);
            return response;
        }

        /// <summary>
        /// Gets the web account info.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public GetWebAccountInfoResponse GetWebAccountInfo(GetWebAccountInfoRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = SendRequest<GetWebAccountInfoResponse>(request);
            return response;
        }

        /// <summary>
        /// Gets the item info.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The response.</returns>
        public GetItemInfoResponse GetItemInfo(GetItemInfoRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            var response = SendRequest<GetItemInfoResponse>(request);
            return response;
        }

    }
}
