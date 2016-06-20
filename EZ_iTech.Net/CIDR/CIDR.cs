using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZ_iTech.Net.CIDR {

    /// <summary>
    /// CIDR
    /// What is cidr : http://www.wirelesstek.com/cidr.htm
    /// </summary>
    public class Cidr {

        /// <summary>
        /// The start ip
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// The end ip
        /// </summary>
        public string End { get; set; }

        /// <summary>
        /// 0.0.0.0/32
        /// </summary>
        public uint Value { get; set; }
         
    }
}
