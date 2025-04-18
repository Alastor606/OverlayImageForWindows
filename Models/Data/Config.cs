using System;
using System.Numerics;


namespace OverlayImageForWindows.Models.Data
{
    internal class Config
    {
        public string ImagePath { get; set; }
        public Vector2 ScreenSize { get; set; }
        public bool IsVideo { get; set; }
        public float ImageOpacity { get; set; } = 1;
        public float VideoVolume { get; set; } = 0;
    }
}
