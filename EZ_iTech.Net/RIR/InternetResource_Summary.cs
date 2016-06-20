using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZ_iTech.Net {

    /// <summary>
    /// registry|*|type|*|count|summary
    /// </summary>
    public sealed class InternetResource_Summary {

        /// <summary>
        /// as for records (see up);
        /// </summary>
        public string Registry { get; set; }

        /// <summary>
        /// as for records (defined up);
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// sum of the number of record lines of this 
        /// type in	the file.
        /// </summary>
        public string Count { get; set; }

        /// <summary>
        /// the ASCII string 'summary' (to distinguish 
        /// the record line);
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// format : registry|*|type|*|count|summary
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        internal static InternetResource_Summary Parse(string content) {
            if (!string.IsNullOrEmpty(content)) {
                var items = content.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (null != items && items.Length > 0) {
                    return new InternetResource_Summary() {
                        Registry = items[0],
                        Type = items[2],
                        Count = items[4],
                        Summary = items[5]
                    };
                }
            }

            return null;
        }
    }
}
