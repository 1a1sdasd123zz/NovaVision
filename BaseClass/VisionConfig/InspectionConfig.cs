using System;
using NovaVision.BaseClass.Module;

namespace NovaVision.BaseClass.VisionConfig
{
    [Serializable]
    public class InspectionConfig
    {
        public DataBindingCollection DataBindings = new DataBindingCollection();
        public string Station { get; set; }

        public string Inspect { get; set; }

        public string CameraName { get; set; }

        public string CameraSerialNum { get; set; }


        public string CameraType { get; set; }


        public string CodePoint { get; set; }


        public string TriggerPoint { get; set; }


        public string TriggerNum { get; set; }


        public bool IsShowResult { get; set; }

        public bool IsSaveImageLocally { get; set; }

        public bool IsUploadImageToRemoteDisk { get; set; }


        public bool IsUploadResImageToRemoteDisk { get; set; }


        public int ExternalTriggerTimes { get; set; }


        public string CommunicationTable { get; set; }


        public string CommSerialNum { get; set; }


        public string CommunicationTable_A { get; set; }


        public string CommSerialNum_A { get; set; }


        public bool IsIgnoreComm_A { get; set; }


        public InspectType InspectType { get; set; }

        public string Algorithm { get; set; }

        public string ImageDisplay { get; set; }


        public int ImageDisplayIndex { get; set; }


        public bool IsIgnore { get; set; }

        public InspectionConfig()
        {
            this.Station = "工位1";
            this.Inspect = "检测1";
            this.CameraName = "";
            this.CameraSerialNum = "";
            this.CameraType = "";
            this.CodePoint = "";
            this.TriggerPoint = "";
            this.TriggerNum = "";
            this.IsShowResult = false;
            this.IsSaveImageLocally = true;
            this.IsUploadResImageToRemoteDisk = false;
            this.IsUploadImageToRemoteDisk = false;
            this.ExternalTriggerTimes = 0;
            this.CommunicationTable = "";
            this.CommSerialNum = "";
            this.CommunicationTable_A = "";
            this.CommSerialNum_A = "";
            this.IsIgnoreComm_A = true;
            this.InspectType = InspectType.None;
            this.Algorithm = "";
            this.ImageDisplay = "";
            this.ImageDisplayIndex = 0;
            this.IsIgnore = false;
        }
    }
}
