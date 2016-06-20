using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZ_iTech.Net.RIR;

namespace EZ_iTech.Net {
    public abstract class IPBase {
        public static uint BaseMask = "255.255.255.255".ToInteger();
        public static uint BaseCidr = 32;

        public abstract uint GetSubnetMask(uint cidr);
    }
}
