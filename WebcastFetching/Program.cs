using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WebcastFetching
{
    class Program
    {
        static void Main(string[] args)
        {
            string DestPath = String.Empty, 
                m3u8Src = String.Empty;
            bool WaitToExit = false;

            switch (args.Count())
            {
                case 0:
                    string input;
                    bool vaildflag;
                    WaitToExit = true;
                    do
                    {
                        Console.WriteLine("Enter m3u8 web link:");
                        input = Console.ReadLine().TrimEnd('\n');
                    } while (!SrcHdl.IsVaildm3u8(input));
                    m3u8Src = input;
                    do
                    {
                        Console.WriteLine("Download to:");
                        input = Console.ReadLine().TrimEnd('\n');
                        vaildflag = true;
                        if (input == String.Empty)
                            input = Environment.CurrentDirectory + '\\' + SrcHdl.GetLinkFileNamem3u8(m3u8Src);
                        else if ((input.Last() == '\\') || (input.Last() == '/'))    // Is directory
                        {
                            if (Directory.Exists(input))
                                input += SrcHdl.GetLinkFileNamem3u8(m3u8Src);
                            else
                            {
                                Console.WriteLine("Invaild path.");
                                vaildflag = false;
                                continue;
                            }
                        }
                        else
                        {
                            if (!(input.Contains('\\') || input.Contains('/')))
                            {
                                input = Environment.CurrentDirectory + '\\' + input;
                            }
                            else if (!Directory.Exists(input.Substring(0, input.LastIndexOf('\\'))))
                            {
                                Console.WriteLine("Invaild path.");
                                vaildflag = false;
                                continue;
                            }
                        }
                    } while (!vaildflag);
                    DestPath = input;
                    break;

                case 1:
                    if (SrcHdl.IsVaildm3u8(args[0]))
                        m3u8Src = args[0];
                    else
                    {
                        Console.WriteLine("Invaild link.");
                        Console.WriteLine("Usage:\twebcasfetching [Url_to_m3u8] [Download_destination]");
                        Environment.Exit(0);
                    }
                    DestPath = Environment.CurrentDirectory + '\\' + SrcHdl.GetLinkFileNamem3u8(m3u8Src);
                    break;

                case 2:
                    if (SrcHdl.IsVaildm3u8(args[0]))
                        m3u8Src = args[0];
                    else
                    {
                        Console.WriteLine("Invaild link.");
                        Console.WriteLine("Usage:\twebcasfetching [Url_to_m3u8] [Download_destination]");
                        Environment.Exit(0);
                    }
                    if ((args[1].Last() == '\\') || (args[1].Last() == '/'))    // Is directory
                    {
                        if (Directory.Exists(args[1]))
                            DestPath = args[1] + SrcHdl.GetLinkFileNamem3u8(m3u8Src);
                        else
                        {
                            Console.WriteLine("Invaild path.");
                            Console.WriteLine("Usage:\twebcasfetching [Url_to_m3u8] [Download_destination]");
                            Environment.Exit(0);
                        }
                    }
                    else
                    {
                        if (!(args[1].Contains('\\') || args[1].Contains('/')))
                        {
                            DestPath = Environment.CurrentDirectory + '\\' + args[1];
                        }
                        else if (!Directory.Exists(args[1].Substring(0, args[1].LastIndexOf('\\'))))
                        {
                            Console.WriteLine("Invaild path.");
                            Console.WriteLine("Usage:\twebcastfetching [Url_to_m3u8] [Download_destination]");
                            Environment.Exit(0);
                        }
                    }
                    break;

                default:
                    Console.WriteLine("Usage:\twebcastfetching [Url_to_m3u8] [Download_destination]");
                    Environment.Exit(0);
                    break;
            }

            UrlHdl PlayerPage = new UrlHdl(m3u8Src);
            string[] Urls;
            FileStream Dest = new FileStream(DestPath, FileMode.Append);
            if (Dest.CanWrite)
            Console.WriteLine("File ready: " + DestPath);

            PlayerPage.HttpGetText();
            M3U8Hdl m3u8hdl = new M3U8Hdl(PlayerPage.Url.Substring(0, PlayerPage.Url.LastIndexOf('/') + 1), PlayerPage.Result);
            Urls = m3u8hdl.ExecRes();

            for (int i = 0; i < Urls.Count(); i++)
            {
                Console.Write("Downloading video segment {0:D}...", i + 1);
                UrlHdl.HttpGetRaw(Urls[i]).CopyTo(Dest);
                Dest.Flush();
                Console.WriteLine(" Done.");
            }
            Dest.Dispose();
            Console.WriteLine("Finished.");
            
            if (WaitToExit)
                Console.ReadKey();
        }
    }
}
