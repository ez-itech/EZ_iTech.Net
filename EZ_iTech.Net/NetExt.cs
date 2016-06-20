using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZ_iTech.Net {
    public static class NetExt {

        /// <summary>
        /// apnic|CN|ipv4|1.0.1.0|256|20110414|allocated
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<InternetResource_Record> IP(this InternetResource data, string ipType = "ipv4") {

            return data.Records.Where(R => R.Type == ipType).Select(R => R).ToList(); ;
        }

        /// <summary>
        /// IP to integer
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static uint ToInteger(this string ip) {
            if (!string.IsNullOrEmpty(ip))
                return ToInteger(ip.Split('.').Select(I => UInt32.Parse(I)).ToArray());

            return 0u;
        }

        /// <summary>
        /// IP to integer
        /// </summary>
        /// <param name="ips"></param>
        /// <returns></returns>
        public static uint ToInteger(this uint[] ips) {
            if (null != ips && ips.Length > 0)
                return ips[0] << 24 | ips[1] << 16 | ips[2] << 8 | ips[3];

            return 0u;
        }

        /// <summary>
        /// IP to string format
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string ToIp(this uint ip) {
            var ips = ToIPs(ip);
            if (null != ips && ips.Length > 0)
                return string.Format("{0}.{1}.{2}.{3}", ips[0], ips[1], ips[2], ips[3]);

            ///* ToIPs(ip)*/.Select(I => I.ToString()).Aggregate((i, j) => i.ToString() + "." + j.ToString());
            return string.Empty;
        }

        /// <summary>
        /// 0000 0000 0000 1111
        /// >>24
        /// >>16
        /// >>8
        /// >>0
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static uint[] ToIPs(this uint ip) {
            uint[] ips = new uint[4];
            ips[0] = ip >> 24 & 0XFF;
            ips[1] = ip >> 16 & 0XFF;
            ips[2] = ip >> 8 & 0XFF;
            ips[3] = ip & 0XFF;

            return ips;
        }
    }
}
