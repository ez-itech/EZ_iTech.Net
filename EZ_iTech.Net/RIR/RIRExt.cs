using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace EZ_iTech.Net.RIR {
    public static class RIRExt {

        /// <summary>
        /// apnic|CN|ipv4|1.0.1.0|256|20110414|allocated
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<InternetResource_Record> IP(this InternetResource data, string ipType = "ipv4") {

            return data.Records.Where(R => R.Type == ipType).Select(R => R).ToList(); ;
        }

       
    }
}
