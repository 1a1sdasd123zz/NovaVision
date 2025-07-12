using System;

namespace NovaVision.BaseClass.Light
{
    [Serializable]
    public class LightInfo
    {
        public string ComName { get; set; }
        public int ControlChannelNum { get; set; }//控制器通道
        public int[] ControlLightChannel { get; set; }//控制通道
        public string Explain { get; set; }//注释
    }
}
