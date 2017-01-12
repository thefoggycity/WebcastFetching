using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebcastFetching
{
    static class SrcHdl
    {
        public static bool IsVaildm3u8(string Link)
        {
            return ((Link.Substring(Link.Count() - 4, 4).ToLower() == "m3u8")
                && (Link.Substring(0, 4).ToLower() == "http"));
            //  Link.EndsWith("m3u8",StringComparison.OrdinalIgnoreCase)
        }

        public static string GetLinkFileNamem3u8(string Link)
        {
            return Link.Substring(Link.LastIndexOf('/') + 1, Link.LastIndexOf('.')  - Link.LastIndexOf('/') - 1);
        }
    }
}
