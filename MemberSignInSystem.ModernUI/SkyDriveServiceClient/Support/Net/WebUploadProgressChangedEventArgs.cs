using System;

namespace HgCo.WindowsLive.SkyDrive.Support.Net
{
    /// <summary>
    /// Provides data for the <see cref="E:WebUploadProgressChanged"/> event of a <see cref="HttpWebClient"/>.
    /// </summary>
    public class WebUploadProgressChangedEventArgs : EventArgs
    {
        #region Properties

        /// <summary>
        /// Gets the number of bytes sent.
        /// </summary>
        /// <value>The number of bytes sent.</value>
        public long BytesSent { get; protected set; }

        /// <summary>
        /// Gets the total number of bytes to send.
        /// </summary>
        /// <value>The total number of bytes to send.</value>
        public long TotalBytesToSend { get; protected set; }

        /// <summary>
        /// Gets the upload operation progress percentage.
        /// </summary>
        /// <value>The upload operation progress percentage.</value>
        public int ProgressPercentage 
        {
            get
            {
                if (TotalBytesToSend > 0)
                    return (int)((BytesSent / (decimal)TotalBytesToSend) * 100);
                else return 0;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebUploadProgressChangedEventArgs"/> class.
        /// </summary>
        /// <param name="bytesSent">The number of bytes sent.</param>
        /// <param name="totalBytesToSend">The total number of bytes to send.</param>
        public WebUploadProgressChangedEventArgs(long bytesSent, long totalBytesToSend)
        {
            BytesSent = bytesSent;
            TotalBytesToSend = totalBytesToSend;
        }

        #endregion
    }
}
