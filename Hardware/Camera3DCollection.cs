using System.Collections.Generic;
using NovaVision.Hardware._006_SDK_Keyence3DTool;
using NovaVision.Hardware._007_SDK_LMI3DTool;
using NovaVision.Hardware._011_SDK_SSZN3DTool;

namespace NovaVision.Hardware
{
    public class Camera3DCollection
    {
        public Dictionary<string, Camera3DBase> _3DCameras = new Dictionary<string, Camera3DBase>();

        private List<string> listKeys = new List<string>();

        private int keyCount = 1;

        public List<string> ListKeys
        {
            get
            {
                listKeys.Clear();
                listKeys.AddRange(_3DCameras.Keys);
                List<string> tempList = new List<string>();
                foreach (string item in listKeys)
                {
                    tempList.Add(item);
                }
                return tempList;
            }
        }

        public int Count => _3DCameras.Count;

        public Camera3DBase this[string key]
        {
            get
            {
                if (_3DCameras.ContainsKey(key))
                {
                    return _3DCameras[key];
                }
                return null;
            }
            set
            {
                _3DCameras[key] = TypeCheck(value);
            }
        }

        public Camera3DBase this[int index]
        {
            get
            {
                listKeys.Clear();
                listKeys.AddRange(_3DCameras.Keys);
                if (index < listKeys.Count)
                {
                    return _3DCameras[listKeys[index]];
                }
                return null;
            }
            set
            {
                listKeys.Clear();
                listKeys.AddRange(_3DCameras.Keys);
                _3DCameras[listKeys[index]] = TypeCheck(value);
            }
        }

        public void Add(string key, Camera3DBase value)
        {
            _3DCameras.Add(key, TypeCheck(value));
        }

        public void Add(Camera3DBase value)
        {
            string key = "3DCam" + keyCount;
            while (_3DCameras.ContainsKey(key))
            {
                keyCount++;
                key = "3DCam" + keyCount;
            }
            _3DCameras.Add(key, TypeCheck(value));
        }

        public bool Remove(string key)
        {
            return _3DCameras.Remove(key);
        }

        public bool Remove(int index)
        {
            listKeys.Clear();
            listKeys.AddRange(_3DCameras.Keys);
            if (index < listKeys.Count)
            {
                return _3DCameras.Remove(listKeys[index]);
            }
            return false;
        }

        public void Clear()
        {
            _3DCameras.Clear();
        }

        public TValue TypeCheck<TValue>(TValue value)
        {
            if (value != null)
            {
                if (value.GetType().Equals(typeof(Camera_Keyence3D)))
                {
                    return value;
                }
                if (value.GetType().Equals(typeof(Camera_LMI3D)))
                {
                    return value;
                }
                //if (value.GetType().Equals(typeof(Camera_LVM3D)))
                //{
                //    return value;
                //}
                if (value.GetType().Equals(typeof(Camera_SSZN)))
                {
                    return value;
                }
                //if (value.GetType().Equals(typeof(Camera_CognexDS3D)))
                //{
                //    return value;
                //}
            }
            return default(TValue);
        }
    }
}
