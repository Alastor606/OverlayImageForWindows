using System.Collections.Generic;
using System.Windows.Documents;

namespace OverlayImageForWindows.Models.Data.TelegramData
{
    public class TgUserInfo
    {
        public long TelegramId { get; set; }
        public bool InBlackList { get; set; } = false;
        public string Title { get; set; }
    }

    public static class TgUserInfoExtensions
    {
        public static List<TgUserInfo> ToInfo(this List<TelegramUser> users)
        {
            if(users == null || users.Count == 0) return new List<TgUserInfo>();
            var result = new List<TgUserInfo>();
            foreach (var item in users) result.Add(new TgUserInfo()
            {
                TelegramId = item.TelegramId,
                InBlackList = item.InBlackList,
                Title = "U_"+item.TelegramId
            });
            return result;
        }
    }
}
