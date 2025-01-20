namespace OverlayImageForWindows.Models.Data
{
    internal class ImageCast
    {
        public string Image1 { get; set; }
        public string Image2 { get; set; }

        public ImageCast(string image1, string image2)
        {
            Image1 = image1;
            Image2 = image2;
        }
    }
}
