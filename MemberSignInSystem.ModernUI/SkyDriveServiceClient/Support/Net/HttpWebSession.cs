using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;

namespace HgCo.WindowsLive.SkyDrive.Support.Net
{
    /// <summary>
    /// Provides methods for handling HTTP web session information.
    /// </summary>
    public class HttpWebSession : ICloneable
    {
        #region Fields

        /// <summary>
        /// 
        /// </summary>
        private object syncObject;

        /// <summary>
        /// The list of cookies.
        /// </summary>
        private readonly List<Cookie> lCookie;

        /// <summary>
        /// The list of authorizations.
        /// </summary>
        private readonly List<Authorization> lAuthorization;

        /// <summary>
        /// The list of proxy authorizations.
        /// </summary>
        private readonly List<Authorization> lProxyAuthorization;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the cookies.
        /// </summary>
        public Cookie[] Cookies
        {
            get { return lCookie.ToArray(); }
        }

        /// <summary>
        /// Gets the authorizations.
        /// </summary>
        public Authorization[] Authorizations
        {
            get { return lAuthorization.ToArray(); }
        }

        /// <summary>
        /// Gets the proxy authorizations.
        /// </summary>
        public Authorization[] ProxyAuthorizations
        {
            get { return lProxyAuthorization.ToArray(); }
        }

        #endregion

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpWebSession"/> class.
        /// </summary>
        public HttpWebSession()
        {
            syncObject = new object();
            lCookie = new List<Cookie>();
            lAuthorization = new List<Authorization>();
            lProxyAuthorization = new List<Authorization>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies session information on the given web request.
        /// </summary>
        /// <param name="webRequest">The web request.</param>
        public void Apply(WebRequest webRequest)
        {
            if (webRequest != null)
            {
                lock (syncObject)
                {
                    if (lCookie.Count > 0)
                    {
                        var lCookieHeader = new List<string>();
                        foreach (var myCookie in lCookie)
                        {
                            if (webRequest.RequestUri.Authority.EndsWith(
                                    myCookie.Domain, StringComparison.OrdinalIgnoreCase) &&
                                myCookie.Expires >= DateTime.Now)
                            {
                                if (!String.IsNullOrEmpty(myCookie.Name))
                                {
                                    lCookieHeader.Add(String.Format(
                                        CultureInfo.InvariantCulture,
                                        "{0}={1}",
                                        myCookie.Name,
                                        myCookie.Value));
                                }
                                else
                                {
                                    lCookieHeader.Add(myCookie.Value);
                                }
                            }
                        }

                        var cookieHeader = String.Join("; ", lCookieHeader.ToArray());
                        if (!String.IsNullOrEmpty(cookieHeader))
                        {
                            webRequest.Headers[HttpRequestHeader.Cookie] = cookieHeader;
                        }
                    }

                    var authorization = lAuthorization
                        .FirstOrDefault(auth =>
                            auth.ProtectionRealm != null &&
                            auth.ProtectionRealm
                                .FirstOrDefault(realm => webRequest.RequestUri.Authority.EndsWith(realm)) != null);
                    if (authorization != null)
                    {
                        webRequest.Headers[HttpRequestHeader.Authorization] = authorization.Message;
                    }

                    authorization = lProxyAuthorization
                        .FirstOrDefault(auth =>
                            auth.ProtectionRealm != null &&
                            auth.ProtectionRealm
                                .FirstOrDefault(realm => webRequest.RequestUri.Authority.EndsWith(realm)) != null);
                    if (authorization != null)
                    {
                        webRequest.Headers[HttpRequestHeader.ProxyAuthorization] = authorization.Message;
                    }
                }
            }
        }

        /// <summary>
        /// Reads session information out of the specified web response.
        /// </summary>
        /// <param name="webResponse">The web response.</param>
        public void Read(WebResponse webResponse)
        {
            var cookies = WebResponseHelper.ParseCookies(webResponse);
            foreach (var cookie in cookies)
            {
                AddCookie(cookie);
            }
        }

        /// <summary>
        /// Adds the authorization.
        /// </summary>
        /// <param name="authorization">The authorization.</param>
        public void AddAuthorization(Authorization authorization)
        {
            lock (syncObject)
            {
                lAuthorization.Add(authorization);
            }
        }

        /// <summary>
        /// Adds the proxy authorization.
        /// </summary>
        /// <param name="authorization">The authorization.</param>
        public void AddProxyAuthorization(Authorization authorization)
        {
            lock (syncObject)
            {
                lProxyAuthorization.Add(authorization);
            }
        }

        /// <summary>
        /// Adds a cookie to the session.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        /// <param name="domain">The domain.</param>
        public void AddCookie(string name, string value, string domain)
        {
            Cookie cookie = !String.IsNullOrEmpty(name) ?
                new Cookie
                {
                    Name = name,
                    Value = value,
                    Domain = domain,
                    Expired = false,
                    Expires = DateTime.MaxValue
                } : 
                new Cookie
                {
                    Value = value,
                    Domain = domain,
                    Expired = false,
                    Expires = DateTime.MaxValue
                };
            AddCookie(cookie);
        }

        /// <summary>
        /// Adds a cookie to the session.
        /// </summary>
        /// <param name="cookie">The cookie to add.</param>
        public void AddCookie(Cookie cookie)
        {
            if (cookie != null)
            {
                lock (syncObject)
                {
                    Cookie cookieFound = null;
                    foreach (var myCookie in lCookie)
                    {
                        if (myCookie.Name == cookie.Name &&
                            myCookie.Domain == cookie.Domain /*&& 
                            myCookie.Path == cookie.Path*/
                                                          )
                        {
                            cookieFound = myCookie;
                            break;
                        }
                    }
                    
                    if (cookieFound != null)
                    {
                        lCookie.Remove(cookieFound);
                    }
                    lCookie.Add(cookie);
                }
            }
        }

        /// <summary>
        /// Resets the session.
        /// </summary>
        public void Reset()
        {
            lock (syncObject)
            {
                lCookie.Clear();
                lAuthorization.Clear();
                lProxyAuthorization.Clear();
            }
        }

        #endregion

        #region ICloneable Members

        /// <summary>
        /// Creates a new HttpWebSession object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new HttpWebSession object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            lock (syncObject)
            {
                var session = new HttpWebSession();
                session.lCookie.AddRange(lCookie);
                session.lAuthorization.AddRange(lAuthorization);
                session.lProxyAuthorization.AddRange(lProxyAuthorization);
                return session;
            }
        }

        #endregion
    }
}
