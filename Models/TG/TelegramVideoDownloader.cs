using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace OverlayImageForWindows.Models.TG
{
    public static class TelegramVideoDownloader
    {
        public static async Task DownloadVideo(string videoUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                var videoBytes = await client.GetByteArrayAsync(videoUrl);
                File.WriteAllBytes(FileSystem.VideoPath + "TgFileName.mp4", videoBytes);
            }
        }

        public static async Task<string> GetVideoUrlFromPinterest(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                htmlDoc.LoadHtml(response);
                
                var videoNode = htmlDoc.DocumentNode.SelectSingleNode("//div[contains(@class, 'iCM XiG L4E')]");
                return videoNode?.GetAttributeValue("src", null);
            }
        }
    }
}
