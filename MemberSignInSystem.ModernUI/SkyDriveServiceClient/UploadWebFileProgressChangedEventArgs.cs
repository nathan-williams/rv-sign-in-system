using System;

namespace HgCo.WindowsLive.SkyDrive
{
    /// <summary>
    /// Provides data for the <see cref="E:UploadValuesProgressChanged"/> event of a <see cref="SkyDriveServiceClient"/>.
    /// </summary>
    public class UploadWebFileProgressChangedEventArgs : EventArgs
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
        /// Gets the uploading task progress percentage.
        /// </summary>
        /// <value>The uploading task progress percentage.</value>
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
        /// Initializes a new instance of the <see cref="UploadWebFileProgressChangedEventArgs"/> class.
        /// </summary>
        /// <param name="bytesSent">The number of bytes sent.</param>
        /// <param name="totalBytesToSend">The total number of bytes to send.</param>
        internal UploadWebFileProgressChangedEventArgs(long bytesSent, long totalBytesToSend)
        {
            BytesSent = bytesSent;
            TotalBytesToSend = totalBytesToSend;
        }

        #endregion
    }
}
