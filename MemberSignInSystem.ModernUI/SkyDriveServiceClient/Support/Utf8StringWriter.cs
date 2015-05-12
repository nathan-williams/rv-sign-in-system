using System;
using System.IO;
using System.Text;

namespace HgCo.WindowsLive.SkyDrive.Support
{
    /// <summary>
    /// 
    /// </summary>
    public class Utf8StringWriter : StringWriter
    {
        /// <summary>
        /// Gets the <see cref="T:System.Text.Encoding"/> in which the output is written.
        /// </summary>
        /// <returns>
        /// The Encoding in which the output is written.
        ///   </returns>
        public override Encoding Encoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
        /// </summary>
        public Utf8StringWriter() 
            : base() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
        /// </summary>
        /// <param name="formatProvider">An <see cref="T:System.IFormatProvider"/> object that controls formatting.</param>
        public Utf8StringWriter(IFormatProvider formatProvider) 
            : base(formatProvider) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
        /// </summary>
        /// <param name="sb">The sb.</param>
        public Utf8StringWriter(StringBuilder sb) 
            : base(sb) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Utf8StringWriter"/> class.
        /// </summary>
        /// <param name="sb">The StringBuilder to write to.</param>
        /// <param name="formatProvider">An <see cref="T:System.IFormatProvider"/> object that controls formatting.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///   <paramref name="sb"/> is null.
        ///   </exception>
        public Utf8StringWriter(StringBuilder sb, IFormatProvider formatProvider)
            : base(sb, formatProvider) { }
    }
}
