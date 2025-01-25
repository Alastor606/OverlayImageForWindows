using MediaToolkit.Model;
using MediaToolkit;
using System;
using System.IO;

namespace OverlayImageForWindows.Models.Data
{
    internal class OverlayVideo
    {
        public string FullName { get; set; }
        public string ThumNailPath { get; set; }
        
        public OverlayVideo(string fullName, string thumbNail)
        {
            FullName = fullName;
            ThumNailPath = thumbNail;
        }

        public OverlayVideo(string fullName)
        {
            FullName = fullName;
        }

        public void CreateThumbNail()
        {
            string thumbnailPath = Path.Combine(FileSystem.ThumnailPath, $"{FullName.GetFileName2()}-thumbnail.png");
            if (File.Exists(thumbnailPath))
            {
                ThumNailPath = thumbnailPath;
                return;
            }

            var inputFile = new MediaFile { Filename = FullName };
            var outputFile = new MediaFile { Filename = thumbnailPath };

            using (var engine = new Engine())
            {
                engine.GetThumbnail(inputFile, outputFile, new MediaToolkit.Options.ConversionOptions { Seek = TimeSpan.FromSeconds(1) });
            }
            ThumNailPath = $"{FullName.GetFileName2()}-thumbnail.png";
        }
    }
}
