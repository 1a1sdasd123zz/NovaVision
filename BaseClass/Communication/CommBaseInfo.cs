using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace NovaVision.BaseClass.Communication
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    [XmlRoot("root", IsNullable = false)]
    public class CommBaseInfo
    {
        [XmlElement("CommCards")]
        public List<CommConfigData> CardList { get; set; }

        public List<string> SnList
        {
            get
            {
                if (CardList != null && CardList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CommConfigData item in CardList)
                    {
                        tempList.Add(item.SerialNum);
                    }
                    return tempList;
                }
                return null;
            }
        }

        public List<string> SnList_CC24
        {
            get
            {
                if (CardList != null && CardList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CommConfigData item in CardList)
                    {
                        if (item.CommCategory == "CC24")
                        {
                            tempList.Add(item.SerialNum);
                        }
                    }
                    return tempList;
                }
                return null;
            }
        }

        public List<string> SnList_Tcp
        {
            get
            {
                if (CardList != null && CardList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CommConfigData item in CardList)
                    {
                        if (item.CommCategory == "Tcp")
                        {
                            tempList.Add(item.SerialNum);
                        }
                    }
                    return tempList;
                }
                return null;
            }
        }

        public List<string> SnList_S7
        {
            get
            {
                if (CardList != null && CardList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CommConfigData item in CardList)
                    {
                        if (item.CommCategory == "S7")
                        {
                            tempList.Add(item.SerialNum);
                        }
                    }
                    return tempList;
                }
                return null;
            }
        }

        public List<string> SnList_Ethernet
        {
            get
            {
                if (CardList != null && CardList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CommConfigData item in CardList)
                    {
                        if (item.CommCategory == "Ethernet")
                        {
                            tempList.Add(item.SerialNum);
                        }
                    }
                    return tempList;
                }
                return null;
            }
        }

        public List<string> SnList_MC
        {
            get
            {
                if (CardList != null && CardList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CommConfigData item in CardList)
                    {
                        if (item.CommCategory == "MC")
                        {
                            tempList.Add(item.SerialNum);
                        }
                    }
                    return tempList;
                }
                return null;
            }
        }

        public List<string> SnList_ModbusTcp
        {
            get
            {
                if (CardList != null && CardList.Count > 0)
                {
                    List<string> tempList = new List<string>();
                    foreach (CommConfigData item in CardList)
                    {
                        if (item.CommCategory == "ModbusTcp")
                        {
                            tempList.Add(item.SerialNum);
                        }
                    }
                    return tempList;
                }
                return null;
            }
        }

        public event DelCardEventHandler DelCardSetting;

        public CommConfigData Query(string Sn)
        {
            if (CardList != null && CardList.Count > 0)
            {
                foreach (CommConfigData item in CardList)
                {
                    if (item.SerialNum == Sn)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public bool Add(CommConfigData ccd)
        {
            if (SnList != null && SnList.Contains(ccd.SerialNum))
            {
                return false;
            }
            CardList.Add(ccd);
            return true;
        }

        public bool Delete(string Sn)
        {
            if (SnList != null && SnList.Contains(Sn) && CardList.Remove(Query(Sn)))
            {
                if (this.DelCardSetting != null)
                {
                    this.DelCardSetting(this, new DelEventCardArgs(Sn));
                }
                return true;
            }
            return false;
        }

        public bool Modify(CommConfigData ccd)
        {
            foreach (CommConfigData item in CardList)
            {
                CommConfigData CCDItem = item;
                if (CCDItem.SerialNum == ccd.SerialNum)
                {
                    CCDItem = ccd;
                    return true;
                }
            }
            return false;
        }

        public int GetTcpMaxIndex()
        {
            List<string> tcpSnList = SnList_Tcp;
            if (tcpSnList == null)
            {
                return -1;
            }
            List<int> indexs = new List<int>();
            foreach (string item in tcpSnList)
            {
                indexs.Add(Convert.ToInt32(item.Substring(4)));
            }
            indexs.Sort();
            if (indexs.Count > 0)
            {
                return indexs[indexs.Count - 1];
            }
            return -1;
        }

        public int GetS7MaxIndex()
        {
            List<string> slaveSnList = SnList_S7;
            if (slaveSnList == null)
            {
                return -1;
            }
            List<int> indexs = new List<int>();
            foreach (string item in slaveSnList)
            {
                indexs.Add(Convert.ToInt32(item.Substring(9)));
            }
            indexs.Sort();
            if (indexs.Count > 0)
            {
                return indexs[indexs.Count - 1];
            }
            return -1;
        }

        public int GetEthernetMaxIndex()
        {
            List<string> slaveSnList = SnList_Ethernet;
            if (slaveSnList == null)
            {
                return -1;
            }
            List<int> indexs = new List<int>();
            foreach (string item in slaveSnList)
            {
                indexs.Add(Convert.ToInt32(item.Substring(15)));
            }
            indexs.Sort();
            if (indexs.Count > 0)
            {
                return indexs[indexs.Count - 1];
            }
            return -1;
        }
    }
}
