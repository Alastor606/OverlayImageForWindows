using OverlayImageForWindows.Models.Data;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace OverlayImageForWindows.Models.TG
{
    public static class Bot
    {
        public static TelegramBotClient client;
        public static bool IsConnected { get; private set; }

        public static void Init()
        {
            if (IsConnected) return;
            try
            {
                client = new TelegramBotClient(FileSystem.info.Token);
                client.StartReceiving(GetUpdates, Error);
                IsConnected = true;
            }
            catch (Exception ex)
            {
                IsConnected = false;
                new Log("При попытке запуска бота ошибка - " + ex.Message);
            }
        }

        private static async Task Error(ITelegramBotClient client, Exception exception, HandleErrorSource source, CancellationToken token)
        {
            new Log("Telegram bot exception : " + exception.Message);
            return;
        }

        private static async Task GetUpdates(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var messge = update.Message.Text;
            var user = update.Message.Chat.Id;
            var type = update.Message.Type;
            var name = update.Message.Chat.FirstName +" " + update.Message.Chat.LastName;
            new Log("message");
            if(!FileSystem.info.AcceptTPUFiles && user != FileSystem.info.TelegramID)
            {
                await client.SendMessage(user, "Автор запретил другим пользователям добавлять файлы");
                new Log($"Пользователь {name}(id = {user}) пытался добавить файл.");
                return;
            }
            if(FileSystem.users != null && FileSystem.users.Count > 0)
            {
                var us = FileSystem.users.FirstOrDefault(x => x.TelegramId == user);
                if (us != null && us.InBlackList)
                {
                    await client.SendMessage(user, "Доступ временно ограничен.");
                    new Log($"Пользователь {name}(id = {user}) пытался добавить файл.");
                    return;
                }
            }
            
            if (type != Telegram.Bot.Types.Enums.MessageType.Text && type != Telegram.Bot.Types.Enums.MessageType.Photo && type != Telegram.Bot.Types.Enums.MessageType.Video)
            {
                await client.SendMessage(user, "Неподдерживаемый формат сообщения!");
                new Log("Неподдерживаемый тип сообщения");
                return;
            }
            if (type == Telegram.Bot.Types.Enums.MessageType.Photo)
            {
                var msg = await client.SendMessage(user, "Сейчас скочаю");
                TelegramDownloader.DownloadImage(update.Message);
                var msg1 = await client.SendMessage(user, "Изображение скачано");

                string log = string.Empty;
                if (user != FileSystem.info.TelegramID) log = $"Пользователь {name}(id = {user}) Добавил изображение!";
                else log = $"Вы добавили изображение (id = {user})";

                new Log(log);
                if (user != FileSystem.info.TelegramID) await client.SendMessage(FileSystem.info.TelegramID, log);

                await client.DeleteMessage(user, msg.Id);
                await Task.Delay(3000);
                await client.DeleteMessage(user, msg1.Id);
            }
            else if (type == Telegram.Bot.Types.Enums.MessageType.Video)
            {
                var msg = await client.SendMessage(user, "Сейчас скочаю");
                TelegramDownloader.DownloadVideo(update.Message);
                var msg1 = await client.SendMessage(user, "Видео скачано");

                string log = string.Empty;
                if (user != FileSystem.info.TelegramID) log = $"Пользователь {name}(id = {user}) Добавил видео!";
                else log = $"Вы добавили видео (id = {user})";

                new Log(log);
                if (user != FileSystem.info.TelegramID) await client.SendMessage(FileSystem.info.TelegramID, log);

                await client.DeleteMessage(user, msg.Id);
                await Task.Delay(3000);
                await client.DeleteMessage(user, msg1.Id);
            }
            else if(type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                if (messge == "/start")
                {
                    var us = FileSystem.users.FirstOrDefault(x => x.TelegramId == user);
                    if (us == null)
                    {
                        await client.SendMessage(user, "Чтобы отправить пользователю картинку или изображение просто отправьте его в чат, и ваш ДРУК получит его на свой компьютер.");
                        await client.SendMessage(FileSystem.info.TelegramID, $"Новый пользователь - {name}(id = {user}))");
                        new Log($"Новый пользователь -  {name}(id = {user})");
                        FileSystem.CreateUser(user);
                        return;
                    }
                    await client.SendMessage(user, "Снова здравстсвуйте");
                }
                else if (messge.Contains("pin"))
                {
                    await client.SendMessage(user, "Пока функция не доступна");
                }
                else
                {
                    await client.SendMessage(user, "Каки");
                    new Log($"Пользователь {name}(id = {user}) написал {messge}");
                }
            }
            else new Log("Неподдерживаемый тип сообщения");
        }
    }
}
