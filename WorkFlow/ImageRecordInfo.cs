using System.Drawing;

namespace NovaVision.WorkFlow
{
    public struct ImageRecordInfo
    {
        public ImageInfo ImageInfo;

        public Cognex.VisionPro.ICogRecord CogRecord;

        public Image ToolImage;

        public ImageSaveInfo ImageSaveInfo;

    }
}
