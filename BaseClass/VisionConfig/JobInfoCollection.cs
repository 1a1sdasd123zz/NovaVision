using NovaVision.BaseClass.Collection;

namespace NovaVision.BaseClass.VisionConfig
{
    public class JobInfoCollection
    {
        public int CurrentID;
        public string CurrentName;
        public MyDictionaryEx<JobInfo> JobInfos = new();
        public int GetNameIndex(string name)
        {
            int index = -1;
            for (int i = 0; i < JobInfos.Count; i++)
            {
                if (JobInfos[i].Name == name)
                {
                    return i;
                }
            }
            return index;
        }

        public int GetIDIndex(int id)
        {
            int index = -1;
            for (int i = 0; i < JobInfos.Count; i++)
            {
                if (JobInfos[i].ID == id)
                {
                    return i;
                }
            }
            return index;
        }

    }
}
