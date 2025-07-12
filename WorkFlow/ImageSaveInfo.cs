using NovaVision.BaseClass;

namespace NovaVision.WorkFlow
{
    public struct ImageSaveInfo
    {
        public bool IsOKorNG;

        public string SaveFilePath;

        public string ImageName;

        public ImageType ImageType;
        public ImageType ImageTypeTool;
        public ImageType ImageTypeRemote;
        public ImageType ImageTypeToolRemote;
 
        public bool IsSaveImageLocally;
        public bool IsUploadImageToRemoteDisk;
        public bool IsUploadResImageToRemoteDisk;
    }
}
