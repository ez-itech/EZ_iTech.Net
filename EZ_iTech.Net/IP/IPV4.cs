using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using EZ_iTech.Net.RIR;

namespace EZ_iTech.Net {
    public class IPV4 : IPBase {

        /// <summary>
        /// returns a subnet mask
        /// </summary>
        /// <param name="cidr">CIDR</param>
        /// <returns></returns>
        public override uint GetSubnetMask(uint cidr) {
            uint subnetMask = 0u;
            for (int i = 0; i < cidr; i++) {
                subnetMask = subnetMask >> 1 | 0x80000000;
            }

            return subnetMask;
        }

        /// <summary>
        /// returns a subnet mask
        /// </summary>
        /// <param name="cidr">CIDR</param>
        /// <returns></returns>
        public uint GetSubnetMaskFast(uint cidr) {
            return (uint)(-1 << (32 - (int)cidr));
        }

        public bool LessThan(string ip1, string ip2) {
            return ip1.ToInteger() < ip2.ToInteger();
        }

        public bool GreaterThan(string ip1, string ip2) {
            return ip1.ToInteger() > ip2.ToInteger();
        }

        public uint IpBefore(string ip) {
            return ip.ToInteger() - 1;
        }

        public uint IpAfter(string ip) {
            return ip.ToInteger() + 1;
        }


        /* Find out how many IPs are contained within a given IP range
        *  e.g. 192.168.0.0 to 192.168.0.255 returns 256
        */
        public uint RangeSize(string ip1, string ip2) {
            return (uint)Math.Abs((long)ip1.ToInteger() - (long)ip2.ToInteger()) + 1u;
        }

        /* return the subnet address given a host address and a subnet bit count */
        public uint GenerateSubnetByCidr(string ip, uint cidr) {
            return ip.ToInteger() & GetSubnetMask(cidr);
        }

        public uint GenSubnetByMask(string ip, uint mask) {
            return ip.ToInteger() & mask;
        }

        /* return the highest (broadcast) address in the subnet given a host address and
        a subnet bit count */
        public uint GenMaxSubnetByCidr(string ip, uint cidr) {
            return (uint)((long)ip.ToInteger() | ~GetSubnetMask(cidr));
        }

        public uint GenMaxSubnetByMask(string ip, uint mask) {
            return (uint)((long)ip.ToInteger() | ~mask);
        }

        /// <summary>
        /// Find the smallest possible subnet mask which can contain a given number of IPs
        /// e.g. 512 IPs can fit in a /23, but 513 IPs need a /22
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public uint CalcMinCidr(uint size) {
            uint smallest = 32;
            for (uint i = 32; i > 0; i--) {
                var curCapiticy = Math.Pow(2, i);
                if (curCapiticy < size)
                    break;

                smallest = curCapiticy >= size ? i : smallest;
            }

            return 32 - smallest;
        }

        public void Subnet(string startIp, string endIp, ref string[] result) {
            uint size = RangeSize(startIp, endIp);
            uint minCidr = CalcMinCidr(size);
            uint cidr = minCidr;

            string minIp = "";
            string maxIp = "";
            string maskIp = "";

            uint minSubnet = 0u;
            uint maxSubnet = 0u;
            uint mask = 0u;

            for (; cidr <= 32; cidr++) {
                mask = GetSubnetMask(cidr);
                minSubnet = GenSubnetByMask(startIp, mask);
                maxSubnet = GenMaxSubnetByMask(startIp, mask);
                maskIp = mask.ToIp();
                minIp = minSubnet.ToIp();
                maxIp = maxSubnet.ToIp();

                Debug.WriteLine("From:" + minIp + ";To:" + maxIp + ";CIDR:" + cidr + ";MASK:" + maskIp);

                if (startIp == minIp && endIp == maxIp)
                    result = result.Concat(new string[] { string.Format("{0}/{1}", startIp, cidr) }).ToArray();

                if (startIp == minIp && GreaterThan(endIp, maxIp))
                    break;
                if (LessThan(startIp, minIp) && endIp == maxIp)
                    break;
                if (LessThan(startIp, minIp) && GreaterThan(endIp, maxIp))
                    break;
            }

            if (startIp != minIp)
                Subnet(startIp, IpBefore(minIp).ToIp(), ref result);

            result = result.Concat(new string[] { string.Format("{0}/{1}", startIp, cidr) }).ToArray();

            if (endIp != maxIp)
                Subnet(IpAfter(maxIp).ToIp(), endIp, ref result);
        }
    }
}
