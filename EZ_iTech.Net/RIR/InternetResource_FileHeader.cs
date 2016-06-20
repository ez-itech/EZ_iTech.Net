using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZ_iTech.Net {

        /// <summary>
        /// The file header consists of the version line and the summary
        /// lines for each type of record.
        /// 
        /// version|registry|serial|records|startdate|enddate|UTCoffset
        /// </summary>
        public sealed class InternetResource_FileHeader {

            /// <summary>
            /// format version number of this file, 
            //  currently 2;
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// as for records and filename (see up);
            /// </summary>
            public string Registry { get; set; }

            /// <summary>
            /// serial number of this file (within the
            /// /// creating RIR series);
            /// </summary>
            public string Serial { get; set; }
            /// <summary>
            /// number of records in file, excluding blank
            /// lines, summary lines, the version line and
            //  comments;
            /// </summary>
            public string Records { get; set; }
            /// <summary>
            /// start date of time period, in yyyymmdd 
            /// format;
            /// </summary>
            public string Startdate { get; set; }
            /// <summary>
            /// end date of period in yyyymmdd format;
            /// </summary>
            public string Enddate { get; set; }
            /// <summary>
            /// offset from UTC (+/- hours) of local RIR
            /// producing file.
            /// </summary>
            public string UTCoffset { get; set; }

            /// <summary>
            /// Format:
            ///     version|registry|serial|records|startdate|enddate|UTCoffset
            /// </summary>
            /// <param name="content"></param>
            /// <returns></returns>
            internal static InternetResource_FileHeader Parse(string content) {
                if (!string.IsNullOrEmpty(content)) {
                    var items = content.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    if (null != items && items.Length > 0) {
                        return new InternetResource_FileHeader() {
                            Version = items[0],
                            Registry = items[1],
                            Serial = items[2],
                            Records = items[3],
                            Startdate = items[4],
                            Enddate = items[5],
                            UTCoffset = items[6]
                        };
                    }

                }

                return null;
            }
        }
}
