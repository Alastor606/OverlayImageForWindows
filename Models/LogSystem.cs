using System;
using System.IO;

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
