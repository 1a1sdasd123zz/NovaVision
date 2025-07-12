using System;
using System.Collections.Generic;
using NovaVision.BaseClass.Collection;

namespace NovaVision.BaseClass.VisionConfig
{
    [Serializable]
    public class StationConfig : MyDictionaryEx<InspectionConfig>
    {
        public List<string> Keys
        {
            get
            {
                return base.GetKeys();
            }
        }

        public List<InspectionConfig> Values
        {
            get
            {
                return base.GetValues();
            }
        }
    }
}
