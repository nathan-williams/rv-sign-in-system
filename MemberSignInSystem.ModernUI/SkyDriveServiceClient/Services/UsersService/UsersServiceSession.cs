using System;
using HgCo.WindowsLive.SkyDrive.Support.Net;

namespace HgCo.WindowsLive.SkyDrive.Services.UsersService
{
    /// <summary>
    /// 
    /// </summary>
    public class UsersServiceSession : ICloneable
    {
        /// <summary>
        /// Gets the HTTP session.
        /// </summary>
        public HttpWebSession HttpSession { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UsersServiceSession"/> class.
        /// </summary>
        public UsersServiceSession()
        {
            HttpSession = new HttpWebSession();
        }

        #region ICloneable Members

        /// <summary>
        /// Creates a new UsersServiceSession object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new UsersServiceSession object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            var session = new UsersServiceSession();
            session.HttpSession = (HttpWebSession)HttpSession.Clone();
            return session;
        }

        #endregion
    }
}
