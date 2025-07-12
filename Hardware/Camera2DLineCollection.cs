using System.Collections.Generic;
using NovaVision.Hardware.C_2DGigeLineScan.Hikrobot;
using NovaVision.Hardware.C_2DGigeLineScan.SDK_IKapLineScanTool;
using NovaVision.Hardware.DalsaTool;

namespace NovaVision.Hardware
{
    public class Camera2DLineCollection
    {
        public Dictionary<string, CameraLine2DBase> _2DLineCameras = new Dictionary<string, CameraLine2DBase>();

        private List<string> listKeys = new List<string>();

        private int keyCount = 1;
        public List<string> ListKeys
        {
            get
            {
                this.listKeys.Clear();
                this.listKeys.AddRange(this._2DLineCameras.Keys);
                List<string> tempList = new List<string>();
                foreach (string item in this.listKeys)
                {
                    tempList.Add(item);
                }
                return tempList;
            }
        }

        public int Count
        {
            get
            {
                return this._2DLineCameras.Count;
            }
        }

        public CameraLine2DBase this[string key]
        {
            get
            {
                bool flag = this._2DLineCameras.ContainsKey(key);
                CameraLine2DBase result;
                if (flag)
                {
                    result = this._2DLineCameras[key];
                }
                else
                {
                    result = null;
                }
                return result;
            }
            set
            {
                this._2DLineCameras[key] = this.TypeCheck<CameraLine2DBase>(value);
            }
        }

        public CameraLine2DBase this[int index]
        {
            get
            {
                this.listKeys.Clear();
                this.listKeys.AddRange(this._2DLineCameras.Keys);
                bool flag = index < this.listKeys.Count;
                CameraLine2DBase result;
                if (flag)
                {
                    result = this._2DLineCameras[this.listKeys[index]];
                }
                else
                {
                    result = null;
                }
                return result;
            }
            set
            {
                this.listKeys.Clear();
                this.listKeys.AddRange(this._2DLineCameras.Keys);
                this._2DLineCameras[this.listKeys[index]] = this.TypeCheck<CameraLine2DBase>(value);
            }
        }

        public void Add(string key, CameraLine2DBase value)
        {
            this._2DLineCameras.Add(key, this.TypeCheck<CameraLine2DBase>(value));
        }

        public void Add(CameraLine2DBase value)
        {
            string key = "2DLineCam" + this.keyCount.ToString();
            while (this._2DLineCameras.ContainsKey(key))
            {
                this.keyCount++;
                key = "2DLineCam" + this.keyCount.ToString();
            }
            this._2DLineCameras.Add(key, this.TypeCheck<CameraLine2DBase>(value));
        }

        public bool Remove(string key)
        {
            return this._2DLineCameras.Remove(key);
        }

        public bool Remove(int index)
        {
            this.listKeys.Clear();
            this.listKeys.AddRange(this._2DLineCameras.Keys);
            bool flag = index < this.listKeys.Count;
            return flag && this._2DLineCameras.Remove(this.listKeys[index]);
        }

        public void Clear()
        {
            this._2DLineCameras.Clear();
        }

        public TValue TypeCheck<TValue>(TValue value)
        {
            bool flag = value != null;
            if (flag)
            {
                bool flag2 = value.GetType().Equals(typeof(Camera_Dalsa2D));
                if (flag2)
                {
                    return value;
                }
                bool flag3 = value.GetType().Equals(typeof(Camera_HIKLineScanGige));
                if (flag3)
                {
                    return value;
                }
                bool flag4 = value.GetType().Equals(typeof(Camera_IKapLineScan));
                if (flag4)
                {
                    return value;
                }
            }
            return default(TValue);
        }
    }
}
