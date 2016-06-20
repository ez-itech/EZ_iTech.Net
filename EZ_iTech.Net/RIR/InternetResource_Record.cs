using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using EZ_iTech.Net.CIDR;

namespace EZ_iTech.Net {

    /// <summary>
    /// IPv4 address ranges
    /// IPv6 address ranges
    /// AS numbers
    /// delegated info
    /// Doc: https://www.arin.net/announcements/2004/20040108.html
    ///      https://www.arin.net/knowledge/statistics/nro_extended_stats_format.pdf
    /// </summary>
    public sealed class InternetResource_Record {


        /// <summary>
        /// 当前Ip段所述的分配机构 RIR
        /// RIR 区域性互联网注册机构（Regional Internet Registry）
        /// 全球5大RIR
        /// http://www.iana.org/numbers
        /// ----------------------------
        /// AfriNIC非洲地区
        /// ftp://ftp.afrinic.net/pub/stats/afrinic/delegated-afrinic-latest
        /// ----------------------------
        /// APNIC亚太地区
        /// http://ftp.apnic.net/apnic/stats/apnic/delegated-apnic-latest
        /// ----------------------------
        /// ARIN 北美地区
        /// http://ftp.arin.net/pub/stats/arin/delegated-arin-extended-latest
        /// ----------------------------
        /// LACNIC 拉丁美洲和一些加勒比群岛
        /// http://ftp.lacnic.net/pub/stats/lacnic/delegated-lacnic-extended-latest
        /// ----------------------------
        /// RIPE NCC 欧洲、中东和中亚
        /// https://ftp.apnic.net/stats/ripe-ncc/delegated-ripencc-latest
        /// ----------------------------
        /// </summary>
        public string Registry { get; set; }

        /// <summary>
        /// ISO 3166 2-letter code of the organisation to which the allocation or assignment was made.
        /// eg : CN、US
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// Type of Internet number resource represented
        /// in this record.One value from the set of
        /// defined strings:
        ///     {asn,ipv4,ipv6}
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// In the case of records of type 'ipv4' or
        /// 'ipv6' this is the IPv4 or IPv6 'first
        /// 
        /// address' of the	range.
        /// 
        /// In the case of an 16 bit AS number, the
        /// 
        /// format is the integer value in the range:
        /// 	
        /// 0 - 65535
        /// 
        /// 
        /// In the case of a 32 bit ASN, the value is
        /// in the range:
        /// 	
        /// 0 - 4294967296
        /// 
        /// No distinction is drawn between 16 and 32
        /// bit ASN values in the range 0 to 65535.
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// In the case of IPv4 address the count of
        /// 
        /// hosts for this range.This count does not
        /// 
        /// have to represent a CIDR range.
        /// 
        /// In the case of an IPv6 address the value
        /// 
        /// will be the CIDR prefix length from the 
        /// 'first address'	value of <start>.
        /// 
        /// 
        /// In the case of records of type 'asn' the
        /// number is the count of AS from this start
        /// value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Date on this allocation/assignment was made
        /// by the RIR in the format:
        /// 
        /// 
        ///     YYYYMMDD
        /// 
        /// Where the allocation or assignment has been
        /// 
        /// transferred from another registry, this date
        /// represents the date of first assignment or
        /// 
        /// allocation as received in from the original
        /// RIR.
        /// 
        /// It is noted that where records do not show a
        /// date of first assignment, this can take the
        /// 0000/00/00 value.
        /// 
        /// </summary>
        public string Date { get; set; }


        /// <summary>
        /// Type of allocation from the set:
        /// 
        ///         {allocated, assigned}
        /// 
        /// This is the allocation or assignment made by
        /// 
        /// the registry producing the file and not any
        /// 
        /// sub-assignment by other agencies.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// In future, this may include extra data that 
        ///	is yet to be defined.
        /// </summary>
        public string Extensions { get; set; }

        /// <summary>
        /// format ： registry|cc|type|start|value|date|status[|extensions...]
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        internal static InternetResource_Record Parse(string content) {
            if (!string.IsNullOrEmpty(content)) {
                var items = content.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (null != items && items.Length > 0) {
                    return new InternetResource_Record() {
                        Registry = items[0],
                        CC = items[1],
                        Type = items[2],
                        Start = items[3],
                        Value = items[4],
                        Date = items[5],
                        Status = items[6]
                    };


                }

            }

            return null;
        }

        public List<IPAddress> ConvertTo(AddressFamily addressFamily = AddressFamily.Unknown) {
            IPAddress ipAddress = IPAddress.None;
            if (IPAddress.TryParse(Start, out ipAddress)) {

                List<IPAddress> result = new List<IPAddress>() {
                   ipAddress
                };
                switch (addressFamily) {
                    case AddressFamily.Unknown:
                        break;
                    case AddressFamily.InterNetwork:
                        var startValue = Start.ToInteger();
                        var length = 0;
                        if (Int32.TryParse(Value, out length)) {
                            for (uint i = 0; i < length; i++) {
                                result.Add(IPAddress.Parse((startValue + i).ToIp()));
                            }
                        }
                        break;
                    case AddressFamily.InterNetworkV6:
                        break;

                    default:
                        break;
                }

                return result;
            }

            return null;
        }

        public Cidr ConvertToCIDR(AddressFamily addressFamily = AddressFamily.Unknown) {
            IPAddress ipAddress = IPAddress.None;
            if (IPAddress.TryParse(Start, out ipAddress)) {
                switch (addressFamily) {
                    case AddressFamily.Unknown:
                        break;
                    case AddressFamily.InterNetwork:
                        var startValue = Start.ToInteger();
                        var length = 0u;
                        if (uint.TryParse(Value, out length)) {
                            return new Cidr() {
                                Start = Start,
                                End = (startValue + length).ToIp(),
                                Value = length
                            };
                        }
                        break;
                    case AddressFamily.InterNetworkV6:
                        break;

                    default:
                        break;
                }

                return null;
            }

            return null;
        }

    }
}
