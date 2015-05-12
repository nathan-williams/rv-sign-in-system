using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;

namespace HgCo.WindowsLive.SkyDrive.Support.Net
{
    /// <summary>
    /// Represents the Passport authentication module.
    /// </summary>
    public class PassportClient : IAuthenticationModule
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly Regex PassportAuthenticationRegex = new Regex("(?i:\\s*Passport(?<Version>\\d+(\\.\\d+)?)\\s*(?<Parameters>.*))");

        #region IAuthenticationModule Members

        /// <summary>
        /// Gets the authentication type provided by this authentication module.
        /// </summary>
        /// <returns>A string indicating the authentication type provided by this authentication module.</returns>
        public virtual string AuthenticationType { get { return "Passport"; } }

        /// <summary>
        /// Gets a value indicating whether the authentication module supports preauthentication.
        /// </summary>
        /// <returns><c>true</c> if the authorization module supports preauthentication; otherwise <c>false</c>.</returns>
        public virtual bool CanPreAuthenticate { get { return false; } }

        /// <summary>
        /// Returns an instance of the <see cref="T:System.Net.Authorization"/> class in respose to an authentication challenge from a server.
        /// </summary>
        /// <param name="challenge">The authentication challenge sent by the server.</param>
        /// <param name="request">The <see cref="T:System.Net.WebRequest"/> instance associated with the challenge.</param>
        /// <param name="credentials">The credentials associated with the challenge.</param>
        /// <returns>
        /// An <see cref="T:System.Net.Authorization"/> instance containing the authorization message for the request, or null if the challenge cannot be handled.
        /// </returns>
        public virtual Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials)
        {
            Authorization authorization = null;
            if (RegexHelper.IsMatch(PassportAuthenticationRegex, challenge))
            {
                var credential = credentials.GetCredential(request.RequestUri, AuthenticationType);
                var matchPassportAuthentication = RegexHelper
                    .Match(PassportAuthenticationRegex, challenge);

                var webRequest1 = (HttpWebRequest)HttpWebRequest.Create("https://nexus.passport.com/rdr/pprdr.asp");
                webRequest1.AllowAutoRedirect = false;
                webRequest1.Method = WebRequestMethods.Http.Get;
                webRequest1.Proxy = null;

                Dictionary<string, string> dicPassportParameter;
                string challengeResponse;
                using (var webResponse1 = (HttpWebResponse)webRequest1.GetResponse())
                {
                    var passportUrls = webResponse1.Headers["PassportURLs"];
                    dicPassportParameter = ParsePassportParameters(passportUrls);

                    challengeResponse = String.Format(
                        "Passport{0} sign-in={1},pwd={2},OrgVerb={3},OrgUrl={4},{5}",
                        matchPassportAuthentication.Groups["Version"].Value,
                        HttpUtility.UrlEncode(credential.UserName),
                        HttpUtility.UrlEncode(credential.Password),
                        request.Method,
                        request.RequestUri,
                        matchPassportAuthentication.Groups["Parameters"].Value);
                }

                var webRequest2 = (HttpWebRequest)HttpWebRequest.Create(dicPassportParameter["DALogin"]);
                webRequest2.AllowAutoRedirect = false;
                webRequest2.Method = WebRequestMethods.Http.Get;
                webRequest2.Headers[HttpRequestHeader.Authorization] = challengeResponse;
                webRequest2.Proxy = null;

                using (var webResponse2 = (HttpWebResponse)webRequest2.GetResponse())
                {
                    var authenticationInfo = webResponse2.Headers["Authentication-Info"];
                    matchPassportAuthentication = RegexHelper
                        .Match(PassportAuthenticationRegex, authenticationInfo);
                    dicPassportParameter = ParsePassportParameters(matchPassportAuthentication.Groups["Parameters"].Value);

                    var token = String.Format(
                        "Passport{0} from-PP={1}",
                        matchPassportAuthentication.Groups["Version"].Value,
                        dicPassportParameter["from-PP"]);

                    authorization = new Authorization(token, true)
                    {
                        ProtectionRealm = new[] { request.RequestUri.Authority }
                    };
                }
            }
            return authorization;
        }

        /// <summary>
        /// Returns an instance of the <see cref="T:System.Net.Authorization"/> class for an authentication request to a server.
        /// </summary>
        /// <param name="request">The <see cref="T:System.Net.WebRequest"/> instance associated with the authentication request.</param>
        /// <param name="credentials">The credentials associated with the authentication request.</param>
        /// <returns>
        /// An <see cref="T:System.Net.Authorization"/> instance containing the authorization message for the request.
        /// </returns>
        public virtual Authorization PreAuthenticate(WebRequest request, ICredentials credentials)
        {
            return null;
        }

        #endregion

        /// <summary>
        /// Parses the passport parameters.
        /// </summary>
        /// <param name="passportParameters">The passport parameters.</param>
        /// <returns></returns>
        private static Dictionary<string, string> ParsePassportParameters(string passportParameters)
        {
            var dic = new Dictionary<string, string>();
            if (!String.IsNullOrEmpty(passportParameters))
            {
                var parameters = passportParameters.Split(',');
                foreach (var parameter in parameters)
                {
                    var pairs = parameter.Split('=');
                    var key = pairs.Length > 0 ?
                        HttpUtility.UrlDecode(pairs[0]) : String.Empty;
                    var value = pairs.Length > 1 ?
                        HttpUtility.UrlDecode(String.Join("=", pairs, 1, pairs.Length - 1)) : String.Empty;

                    if (key.Length > 0)
                    {
                        if (key.Equals("DALogin") &&
                            !value.StartsWith("http", StringComparison.OrdinalIgnoreCase))
                        {
                            value = String.Concat("https://", value);
                        }
                        dic[key] = value;
                    }
                }
            }
            return dic;
        }
    }
}
