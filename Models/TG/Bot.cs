using OverlayImageForWindows.Models.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace OverlayImageForWindows.Models.TG
{
    internal static class Bot
    {
        static TelegramBotClient client;
        public static void Init()
        {
            client = new TelegramBotClient(FileSystem.info.Token);
            client.StartReceiving(GetUpdates, Error);
        }

        private static async Task Error(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
        {
            MessageBox.Show("Получена ошибка" + exception.Message);
            new Log("Получена ошибка - " + exception.Message);
        }

        private static async Task GetUpdates(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var messge = update.Message.Text;
            var user = update.Message.Chat.Id;
            var type = update.Message.Type;
            if (type != Telegram.Bot.Types.Enums.MessageType.Text && type != Telegram.Bot.Types.Enums.MessageType.Photo && type != Telegram.Bot.Types.Enums.MessageType.Video)
            {
                await client.SendMessage(user, "что-то пошло не так");
                return;
            }
            if (type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                var msg = await client.SendMessage(user, "Сейчас скочаю");
                var fileId = update.Message.Photo.Last().FileId;
                var file = await client.GetFile(fileId);
                using (var saveImageStream = new FileStream(FileSystem.ImagePath + "TgFileName.png".GetNextName(FileSystem.ImagePath), FileMode.Create))
                {
                    await client.DownloadFile(file.FilePath, saveImageStream);
                }
                var msg1 = await client.SendMessage(user, "Изображение скачано");


                var log = $"Пользователь {update.Message.Chat.FirstName} {update.Message.Chat.LastName}({user}). Добавил изображение!";
                new Log(log);
                if (user != FileSystem.info.TelegramID) await client.SendMessage(FileSystem.info.TelegramID, log);

                await client.DeleteMessage(user, msg.Id);
                await Task.Delay(3000);
                await client.DeleteMessage(user, msg1.Id);
            }
            else if (type == Telegram.Bot.Types.Enums.MessageType.Video)
            {
                var msg = await client.SendMessage(user, "Сейчас скочаю");
                var fileId = update.Message.Video.FileId;
                var file = await client.GetFile(fileId);
                using (var saveImageStream = new FileStream(FileSystem.VideoPath + "TgFileName.mp4".GetNextName(FileSystem.VideoPath), FileMode.Create))
                {
                    await client.DownloadFile(file.FilePath, saveImageStream);
                }

                var log = $"Пользователь {update.Message.Chat.FirstName} {update.Message.Chat.LastName}({user}). Добавил видео!";
                new Log(log);
                if (user != FileSystem.info.TelegramID) await client.SendMessage(FileSystem.info.TelegramID, log);
                await client.DeleteMessage(user, msg.Id);
            }
            else if(type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (messge == "/start")
                {
                    await client.SendMessage(user, "С добрым утрам");
                    new Log($"Новый пользователь -  {update.Message.Chat.FirstName} {update.Message.Chat.LastName}({user})");
                }
                else
                {
                    await client.SendMessage(user, "Каки");
                    new Log($"Пользователь {update.Message.Chat.FirstName} {update.Message.Chat.LastName}({user}) написал {messge}");
                }
            }
            else
            {
                new Log("Неподдерживаемый тип сообщения");
            }
            
        }
    }
}
