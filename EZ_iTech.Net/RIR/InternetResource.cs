using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace EZ_iTech.Net {

    public class InternetResource {
        //private data.Records.Where(R => R.Type == ipType).Select(R => R).ToList(); ;
        private List<InternetResource_Record> _ASN = null;
        private List<InternetResource_Record> _IPV4 = null;
        private List<InternetResource_Record> _IPV6 = null;

        public InternetResource_FileHeader FileHeader { get; set; }
        public List<InternetResource_Summary> Summaryes { get; set; }
        public List<InternetResource_Record> Records { get; set; }

        /// <summary>
        /// 自治系统号
        /// Autonomous System Number
        /// </summary>
        public List<InternetResource_Record> ASN {
            get {
                if (null == _ASN) {
                    _ASN = new List<InternetResource_Record>();
                    if (null != Records && Records.Count > 0) {
                        _ASN = Records.Where(R => R.Type == "asn").Select(R => R).ToList(); ;
                    }
                }

                return _ASN;
            }
        }

        /// <summary>
        /// IPV4
        /// </summary>
        public List<InternetResource_Record> IPV4 {
            get {
                if (null == _IPV4) {
                    _IPV4 = new List<InternetResource_Record>();
                    if (null != Records && Records.Count > 0) {
                        _IPV4 = Records.Where(R => R.Type == "ipv4").Select(R => R).ToList(); ;
                    }
                }

                return _IPV4;
            }
        }

        /// <summary>
        /// IPV6
        /// </summary>
        public List<InternetResource_Record> IPV6 {
            get {
                if (null == _IPV6) {
                    _IPV6 = new List<InternetResource_Record>();
                    if (null != Records && Records.Count > 0) {
                        _IPV6 = Records.Where(R => R.Type == "ipv6").Select(R => R).ToList(); ;
                    }
                }
                return _IPV6;
            }
        }

        /// <summary>
        /// Parse form plaintext
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public void Parse(string content) {
            if (!string.IsNullOrEmpty(content)) {
                var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (null != lines && lines.Length > 0) {
                    Summaryes = new List<InternetResource_Summary>();
                    Records = new List<InternetResource_Record>();
                    for (int i = 0, j = 0; i < lines.Length; i++) {
                        var curLine = lines[i];
                        /* the line start with # is comment and skip*/
                        if (curLine.StartsWith("#"))
                            continue;

                        if (j++ == 0)
                            FileHeader = InternetResource_FileHeader.Parse(curLine);

                        if (curLine.EndsWith("summary")) {
                            Summaryes.Add(InternetResource_Summary.Parse(curLine));
                        }
                        else {
                            Records.Add(InternetResource_Record.Parse(curLine));
                        }
                    }
                }

            }
        }
    }

    public static class InternetResource_Ext {

        //public List<InternetResource_Record>
    }
}
