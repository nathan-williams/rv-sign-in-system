using System;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.Soap
{
    /// <summary>
    /// 
    /// </summary>
    public class SoapWebSession : ICloneable
    {
        /// <summary>
        /// Gets the HTTP session.
        /// </summary>
        public HttpWebSession HttpSession { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SoapWebSession"/> class.
        /// </summary>
        public SoapWebSession()
        {
            HttpSession = new HttpWebSession();
        }

        #region ICloneable Members

        /// <summary>
        /// Creates a new SoapWebSession object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new SoapWebSession object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            var session = new SoapWebSession();
            session.HttpSession = (HttpWebSession)HttpSession.Clone();
            return session;
        }

        #endregion
    }
}
