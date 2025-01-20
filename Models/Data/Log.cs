namespace OverlayImageForWindows.Models.Data
{
    internal class Log
    {
        public Log(string message)
        {
            LogSystem.Log(message);
        }
    }
}
