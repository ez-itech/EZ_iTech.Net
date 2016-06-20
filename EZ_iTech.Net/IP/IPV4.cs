using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using EZ_iTech.Net.RIR;

namespace EZ_iTech.Net {
    public class IPV4 : IPBase {
        public override uint GetSubnetMask(uint cidr) {
            uint subnetMask = 0u;
            for (int i = 0; i < cidr; i++) {
                subnetMask = subnetMask >> 1 | 0x80000000;
            }

            return subnetMask;
        }

        public bool LessThan(string ip1, string ip2) {
            return ip1.ToInteger() < ip2.ToInteger();
        }

        public bool GreaterThan(string ip1, string ip2) {
            return ip1.ToInteger() < ip2.ToInteger();
        }

        public uint IpBefore(string ip) {
            return ip.ToInteger() - 1;
        }

        public uint IpAfter(string ip) {
            return ip.ToInteger() + 1;
        }

        /* Find the smallest possible subnet mask which can contain a given number of IPs
        *  e.g. 512 IPs can fit in a /23, but 513 IPs need a /22
        */
        public uint SmallestCidr(uint count) {
            uint smallest = 1;
            for (var i = 32u; i > 0; i--) {
                smallest = (count <= Math.Pow(2, i)) ? i : smallest;
            }
            return 32 - smallest;
        }

        /* Find out how many IPs are contained within a given IP range
        *  e.g. 192.168.0.0 to 192.168.0.255 returns 256
        */
        public uint RangeSize(string ip1, string ip2) {
            return (uint)Math.Abs((long)ip1.ToInteger() - (long)ip2.ToInteger()) + 1u;
        }

        /* returns a subnet mask  */
        public uint GenerateMask(uint cidr) {
            uint mask = 0u;
            for (int i = 0; i < cidr; i++) {
                mask = mask >> 1 | 0x80000000;
            }

            return mask;
        }

        /* return the subnet address given a host address and a subnet bit count */
        public uint GenerateSubnet(string ip, uint cidr) {
            return ip.ToInteger() & GetSubnetMask(cidr);
        }


        /* return the highest (broadcast) address in the subnet given a host address and
        a subnet bit count */
        public uint GenerateMaxSubnet(string ip, uint cidr) {
            return ip.ToInteger() | ~GetSubnetMask(cidr);
        }

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

        public string[] Subnet(string startIp, string endIp) {
            uint size = RangeSize(startIp, endIp);
            uint minCidr = CalcMinCidr(size);
            for (uint cidr = minCidr; cidr <= 32; cidr++) {
                uint minSubnet = GenerateSubnet(startIp, cidr);
                uint maxSubnet = GenerateMaxSubnet(startIp, cidr);
                string minIp = minSubnet.ToIp();
                string maxIp = maxSubnet.ToIp();

                Debug.WriteLine("From:" + minIp + ";To:" + maxIp + ";CIDR:" + cidr);

                if (startIp == minSubnet.ToIp() && endIp == maxSubnet.ToIp())
                    return new string[] { };


            }


            return null;
        }
    }
}
