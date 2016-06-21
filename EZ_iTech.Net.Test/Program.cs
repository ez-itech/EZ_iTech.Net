using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using EZ_iTech.ToolKit;
using System.IO;
using System.Net.Sockets;

namespace EZ_iTech.Net.Test {
    class Program {
        static void Main(string[] args) {

            /* "255.255.255.128 */
            IPV4 IP = new IPV4();
            var aa1 = IP.GetSubnetMask(25);
            var aa11 = IP.GetSubnetMaskFast(25);
            var aa2 = aa1.ToIp();

            var aa3 = aa1 & "2.0.254.1".ToInteger();
            var aa4 = aa3.ToIp();

            var aa5 = IP.CalcMinCidr(512);
            var result = new string[0];
            IP.Subnet("192.168.1.1", "192.168.1.12", ref result);

            var a1 = "255.255.255.128";
            var a2 = "255.255.255.255";

            var b1 = a1.ToInteger();
            var b2 = a2.ToInteger();

            var c1 = b1 ^ b2;
            var c2 = Math.Log(c1 + 1, 2);

            var mmmm = "1.0.1.0".ToInteger();
            var nnnn = 16777472u.ToIp();


            var cfg = new HTTPHelper.Config() {
                UseProxy = false,
                ManualSetIp = false

            };
            cfg = null;
            //var content = HTTPHelper.Get("http://ftp.arin.net/pub/stats/apnic/delegated-apnic-latest", 3000, cfg: cfg);
            var content = File.ReadAllText("D:/TMP/delegated-apnic-latest.txt");
            InternetResource res = new InternetResource();
            res.Parse(content);

            var data = res.IPV4;

            //var ipv4 = res.IPV4.Where(I => I.CC == "CN").Select(I => I.ConvertTo(AddressFamily.InterNetwork)).ToList();
            var ipv4 = res.IPV4.Where(I => I.CC == "CN").Select(I => I.ConvertToCIDR(AddressFamily.InterNetwork)).ToList();
        }

        static void TEST1() {

            //var ip = IPAddress.Parse("180.168.27.243");



            //WebClient client = new WebClient();
            //var m = client.DownloadString("http://ftp.arin.net/pub/stats/apnic/delegated-apnic-latest");
            var r = (HttpWebRequest)WebRequest.Create("http://ftp.arin.net/pub/stats/apnic/delegated-apnic-latest");
            r.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.84 Safari/537.36";
            r.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            r.Method = "GET";

            r.Headers["Accept-Encoding"] = "gzip, deflate, sdch";
            r.Headers["Accept-Language"] = "zh-CN,zh;q=0.8,en;q=0.6";
            r.Headers["Cache-Control"] = "max-age=0";
            r.Headers["Upgrade-Insecure-Requests"] = "1";


            var rr = r.GetResponse();
            var rs = rr.GetResponseStream();
            var rsr = new StreamReader(rs);
            var rstr = rsr.ReadToEnd();



            Console.WriteLine(rstr);

            //Console.Read();
        }

        static void TEST2() {
            var ip = IPAddress.Parse("1.0.1.0");

            var mmmm = "1.0.1.0".ToInteger();
            var nnnn = 16777472u.ToIp();

            uint ip2 = 1 << 24 | 0 << 16 | 1 << 8 | 0;

            var a1 = (ip2 >> 24) & 0XFF;
            var a2 = (ip2 >> 16) & 0XFF;
            var a3 = (ip2 >> 8) & 0XFF;
            var a4 = (ip2) & 0XFF;

            var tt = new uint[] { 1, 0, 1, 0 };
            var mm = tt.ToInteger();

            var nn = mm.ToIp();
        }
    }
}
