using MediaToolkit.Model;
using MediaToolkit;
using System;
using System.IO;

namespace OverlayImageForWindows.Models.Data
{
    internal class OverlayVideo
    {
        public string FullPath { get; set; }
        public string ThumNailPath { get; set; }
        
        public OverlayVideo(string fullPath, string thumbNail)
        {
            FullPath = fullPath;
            ThumNailPath = thumbNail;
        }

        public OverlayVideo(string fullPath)
        {
            FullPath = fullPath;

        }

        public void CreateThumbNail()
        {
            string thumbnailPath = Path.Combine(FileSystem.ThumnailPath, $"{FullPath.GetFileName2()}-thumbnail.png");
            if (File.Exists(thumbnailPath))
            {
                ThumNailPath = thumbnailPath;
                return;
            }

            var inputFile = new MediaFile { Filename = FullPath };
            var outputFile = new MediaFile { Filename = thumbnailPath };

            using (var engine = new Engine())
            {
                engine.GetThumbnail(inputFile, outputFile, new MediaToolkit.Options.ConversionOptions { Seek = TimeSpan.FromSeconds(1) });
            }
        }
    }
}
