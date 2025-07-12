using System.Collections.Generic;
using NovaVision.BaseClass.Collection;

namespace NovaVision.BaseClass.VisionConfig
{
    public class StationCollection : MyDictionaryEx<StationConfig>
    {
        public List<string> Keys => GetKeys();

        public List<StationConfig> Values => GetValues();

        public int InspectionCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < base.Count; i++)
                {
                    count += base[i].Count;
                }
                return count;
            }
        }
    }
}

