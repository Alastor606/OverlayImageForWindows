using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayImageForWindows.Models
{
    internal static class LogSystem
    {
        public static void Log(string message)
        {
            File.AppendAllText(FileSystem.LogPath, $"[{DateTime.Now.ToString("dd.MM.yy.HH:mm:ss")}]   {message}\n");
        }
    }
}
