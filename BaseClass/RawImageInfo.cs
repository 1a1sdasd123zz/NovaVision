
namespace NovaVision.BaseClass
{
    public class RawImageInfo
    {
        public int ThumbPercent { get; set; }

        public int DiskType { get; set; }

        public string UserName { get; set; }

        public string Pwd { get; set; }

        public string Path { get; set; }//公共盘路径


        public string SaveFilePath { get; set; }
        public string ImageName { get; set; }

        public ImageType mImageType { get; set; }

        public Cognex.VisionPro.ICogImage Image { get; set; }

        public byte[] ImageBuffer { get; set; }
    }
}
