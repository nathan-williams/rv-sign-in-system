using System;
using System.Net;
using HgCo.WindowsLive.SkyDrive.Services.UsersService;
using HgCo.WindowsLive.SkyDrive.Support.Net;
using HgCo.WindowsLive.SkyDrive.Support.Net.Soap;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SkyDriveSession
    {
        /// <summary>Gets or sets the credentials.</summary>
        /// <value>The credentials.</value>
        public ICredentials Credentials { get; set; }

        /// <summary>
        /// Gets or sets the Windows Live Identifier (CID).
        /// </summary>
        /// <value>The CID.</value>
        public string Cid { get; set; }

        /// <summary>
        /// Gets the sky docs service session.
        /// </summary>
        public SoapWebSession SkyDocsServiceSession { get; private set; }

        /// <summary>
        /// Gets the users service session.
        /// </summary>
        public UsersServiceSession UsersServiceSession { get; private set; }

        /// <summary>
        /// Gets the WebDAV web client session.
        /// </summary>
        public HttpWebSession WebDavWebClientSession { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SkyDriveSession"/> class.
        /// </summary>
        public SkyDriveSession()
        {
            SkyDocsServiceSession = new SoapWebSession();
            UsersServiceSession = new UsersServiceSession();
            WebDavWebClientSession = new HttpWebSession();
        }
    }
}
