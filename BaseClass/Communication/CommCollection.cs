using System.Collections.Generic;
using NovaVision.BaseClass.Communication.TCP;

namespace NovaVision.BaseClass.Communication
{
    public class CommCollection
    {
        public Dictionary<string, IFlowState> _commDic = new Dictionary<string, IFlowState>();

        private List<string> listKeys = new List<string>();

        private int keyCount = 1;
        public List<string> ListKeys
        {
            get
            {
                this.listKeys.Clear();
                this.listKeys.AddRange(this._commDic.Keys);
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
                return this._commDic.Count;
            }
        }

        public IFlowState this[string key]
        {
            get
            {
                bool flag = this._commDic.ContainsKey(key);
                IFlowState result;
                if (flag)
                {
                    result = this._commDic[key];
                }
                else
                {
                    result = null;
                }
                return result;
            }
            set
            {
                this._commDic[key] = this.TypeCheck<IFlowState>(value);
            }
        }

        public IFlowState this[int index]
        {
            get
            {
                this.listKeys.Clear();
                this.listKeys.AddRange(this._commDic.Keys);
                bool flag = index < this.listKeys.Count;
                IFlowState result;
                if (flag)
                {
                    result = this._commDic[this.listKeys[index]];
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
                this.listKeys.AddRange(this._commDic.Keys);
                this._commDic[this.listKeys[index]] = this.TypeCheck<IFlowState>(value);
            }
        }

        public void Add(string key, IFlowState value)
        {
            this._commDic.Add(key, this.TypeCheck<IFlowState>(value));
        }

        public void Add(IFlowState value)
        {
            string key = "Comm" + this.keyCount.ToString();
            while (this._commDic.ContainsKey(key))
            {
                this.keyCount++;
                key = "Comm" + this.keyCount.ToString();
            }
            this._commDic.Add(key, this.TypeCheck<IFlowState>(value));
        }

        public bool Remove(string key)
        {
            return this._commDic.Remove(key);
        }

        public bool Remove(int index)
        {
            this.listKeys.Clear();
            this.listKeys.AddRange(this._commDic.Keys);
            bool flag = index < this.listKeys.Count;
            return flag && this._commDic.Remove(this.listKeys[index]);
        }

        public void Clear()
        {
            this._commDic.Clear();
        }

        public TValue TypeCheck<TValue>(TValue value)
        {
            bool flag = value != null;
            if (flag)
            {
                //bool flag2 = value.GetType().Equals(typeof(CC24_Comm));
                //if (flag2)
                //{
                //    return value;
                //}
                bool flag3 = value.GetType().Equals(typeof(MyTcpClient));
                if (flag3)
                {
                    return value;
                }
                bool flag4 = value.GetType().Equals(typeof(MyTcpServer));
                if (flag4)
                {
                    return value;
                }
                //bool flag5 = value.GetType().Equals(typeof(S7_SlaveStation));
                //if (flag5)
                //{
                //    return value;
                //}
                //bool flag6 = value.GetType().Equals(typeof(Ethernet_SlaveStation));
                //if (flag6)
                //{
                //    return value;
                //}
            }
            return default(TValue);
        }
    }
}
