using System.IO;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace OverlayImageForWindows.Models.TG
{
    public static class TelegramDownloader
    {
        public async static void DownloadImage(Message msg)
        {
            var fileId = msg.Photo.Last().FileId;
            var file = await Bot.client.GetFile(fileId);
            using (var saveImageStream = new FileStream(FileSystem.ImagePath + "TgFileName.png".GetNextName(FileSystem.ImagePath), FileMode.Create))
            {
                await Bot.client.DownloadFile(file.FilePath, saveImageStream);
            }
        }

        public async static void DownloadVideo(Message msg)
        {
            var fileId = msg.Video.FileId;
            var file = await Bot.client.GetFile(fileId);
            using (var saveImageStream = new FileStream(FileSystem.VideoPath + "TgFileName.mp4".GetNextName(FileSystem.VideoPath), FileMode.Create))
            {
                await Bot.client.DownloadFile(file.FilePath, saveImageStream);
                FileSystem.CreateVideo(saveImageStream.Name);
            }
        }
    }
}
