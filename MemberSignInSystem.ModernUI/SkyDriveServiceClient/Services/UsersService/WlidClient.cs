using System;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using HgCo.WindowsLive.SkyDrive.Support;
using HgCo.WindowsLive.SkyDrive.Support.Net;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// Represents the Windows Live Identity (WLID) authentication module.
    /// </summary>
    public class WlidClient : PassportClient
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Regex WlidAuthenticationRegex = new Regex("(?i:\\s*WLID(?<Version>\\d+(\\.\\d+)?)\\s*(?<Parameters>.*))");

        /// <summary>
        /// 
        /// </summary>
        private static readonly Regex AuthorizationTokenRegex = new Regex("t=(?<Token>.+)");

        /// <summary>
        /// Gets the authentication type provided by this authentication module.
        /// </summary>
        /// <returns>A string indicating the authentication type provided by this authentication module.</returns>
        public override string AuthenticationType { get { return "WLID"; } }

        /// <summary>
        /// Returns an instance of the <see cref="T:System.Net.Authorization"/> class in respose to an authentication challenge from a server.
        /// </summary>
        /// <param name="challenge">The authentication challenge sent by the server.</param>
        /// <param name="request">The <see cref="T:System.Net.WebRequest"/> instance associated with the challenge.</param>
        /// <param name="credentials">The credentials associated with the challenge.</param>
        /// <returns>
        /// An <see cref="T:System.Net.Authorization"/> instance containing the authorization message for the request, or null if the challenge cannot be handled.
        /// </returns>
        public override Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials)
        {
            Authorization authorization = null;

            if (RegexHelper.IsMatch(WlidAuthenticationRegex, challenge))
            {
                challenge = String.Format(
                    CultureInfo.InvariantCulture,
                    "Passport1.4 ct={0},rver=6.1.6206.0,wp=MBI,lc=1033,id=250206",
                    UnixDateTimeHelper.Parse(DateTime.Now));

                authorization = base.Authenticate(challenge, request, credentials);

                if (authorization != null && authorization.Complete &&
                    RegexHelper.IsMatch(AuthorizationTokenRegex, authorization.Message))
                {
                    var matchToken = RegexHelper.Match(AuthorizationTokenRegex, authorization.Message);
                    var token = String.Format(
                        CultureInfo.InvariantCulture,
                        "WLID1.0 t={0}",
                        matchToken.Groups["Token"].Value);

                    authorization = new Authorization(token, true)
                    {
                        ProtectionRealm = new[] { request.RequestUri.Authority }
                    };
                }
            }
            return authorization;
        }
    }
}
