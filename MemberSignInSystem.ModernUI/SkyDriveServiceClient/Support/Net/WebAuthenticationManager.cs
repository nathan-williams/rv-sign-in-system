using System.Collections.Generic;
using System.Net;

namespace HgCo.WindowsLive.SkyDrive.Support.Net
{
    /// <summary>
    /// Represents the Authentication Manager that manages the authentication modules 
    /// called during HttpWebClient authentication process.
    /// </summary>
    /// <remarks>Custom AuthenticationManager is required, 
    /// otherwise Authorization token cannot be saved in HttpWebSession.</remarks>
    public static class WebAuthenticationManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly List<IAuthenticationModule> lAuthenticationModule = new List<IAuthenticationModule>();

        /// <summary>
        /// Gets the registered modules.
        /// </summary>
        public static IEnumerable<IAuthenticationModule> RegisteredModules
        {
            get
            {
                lock (lAuthenticationModule)
                {
                    return lAuthenticationModule.ToArray();
                }
            }
        }

        /// <summary>
        /// Registers the specified authentication module.
        /// </summary>
        /// <param name="authenticationModule">The authentication module.</param>
        public static void Register(IAuthenticationModule authenticationModule)
        {
            if (authenticationModule != null)
            {
                lock (lAuthenticationModule)
                {
                    if (!lAuthenticationModule.Contains(authenticationModule))
                    {
                        lAuthenticationModule.Add(authenticationModule);
                    }
                }
            }
        }

        /// <summary>
        /// Calls each registered authentication module to find the first module 
        /// that can respond to the authentication request.
        /// </summary>
        /// <param name="challenge">The challenge.</param>
        /// <param name="request">The request.</param>
        /// <param name="credentials">The credentials.</param>
        /// <returns></returns>
        public static Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials)
        {
            foreach (var authenticationModule in RegisteredModules)
            {
                var authorization = authenticationModule.Authenticate(challenge, request, credentials);
                if (authorization != null)
                {
                    return authorization;
                }
            }

            return null;
        }
    }
}
