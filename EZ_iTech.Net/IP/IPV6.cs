using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EZ_iTech.Net.RIR;

namespace EZ_iTech.Net {
    public class IPV6 : IPBase {
        public override uint GetSubnetMask(uint cidr) {
           
            return 0u;
        }
    }
}
