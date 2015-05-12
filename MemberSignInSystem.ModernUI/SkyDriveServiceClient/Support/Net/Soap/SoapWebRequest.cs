using System;

namespace HgCo.WindowsLive.SkyDrive.Support.Net.Soap
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public abstract class SoapWebRequest
    {
        /// <summary>
        /// Gets or sets the SOAP action.
        /// </summary>
        /// <value>The SOAP action.</value>
        public abstract string SoapAction { get; }
    }
}
