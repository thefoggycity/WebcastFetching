using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebcastFetching
{
    class M3U8Hdl
    {
        string Addr;    // End with slash
        string Content;

        public M3U8Hdl(string ParentalAddress, string FileContent)
        {
            Addr = ParentalAddress;
            Content = FileContent;
        }

        public string[] ExecRes()
        {
            List<string> AddrLst = new List<string>();
            AddrLst.Clear();
            char[] RET = { '\x0D', '\x0A' };

            /*
            for (int i = 0; i < Content.Count(); i++)
            {
                string rec = String.Empty;

                if (Content.ElementAt(i) == '#')
                {
                    do { i++; }
                    while ((Content.ElementAt(i - 1) != 0x0D) || (Content.ElementAt(i) != 0x0A));
                }
                else
                {
                    while ((Content.ElementAt(i) != 0x0D) || (Content.ElementAt(i + 1) != 0x0A))
                    {
                        rec += Content.ElementAt(i);
                        i++;
                    }
                    i++;
                    AddrLst.Add(Addr + rec);
                }
            }*/
            string[] AddrTmp = Content.Split(RET, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < AddrTmp.Count(); i++)
                if (AddrTmp[i].First() != '#')
                    AddrLst.Add(Addr + AddrTmp[i]);
            string[] Addrs = AddrLst.ToArray();
            return Addrs;
        }
    }
}
