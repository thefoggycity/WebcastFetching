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
            //Uri uri = new Uri("https://ivle.nus.edu.sg:443/v1/Media/Student/Multimedia.aspx?CourseID=4ec0ae0f-0745-4308-b350-9bc27d941c48&ChannelID=5de9c813-3644-4162-95e7-e14a278d8946");
            //Console.WriteLine(uri);
            //Console.WriteLine(uri.IsDefaultPort);
            //Console.ReadKey();

            UrlHdl PlayerPage = new UrlHdl("https://vod.nus.edu.sg/hls-vod/ivle/users/13342cd6-97ce-4859-8185-5fe7b296ca56/TRAILER_0923_NO_INTERVIEW_NO_BG.mp4.m3u8");
            string DestPath = Environment.CurrentDirectory + "\\trailer.mp4";
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
            
            Console.ReadKey();
        }
    }
}
