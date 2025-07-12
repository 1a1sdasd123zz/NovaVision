using System;
using System.Collections.Generic;
using NovaVision.BaseClass.Communication.TCP;

namespace NovaVision.BaseClass.Communication
{
    public class CommunicationOperator
    {
        public static CommCollection commCollection = new CommCollection();

        public static Dictionary<string, MyTcpClient> clientDic;

        public static Dictionary<string, MyTcpServer> serverDic;

        //public static Dictionary<string, S7_SlaveStation> slaveDic;

        //public static Dictionary<string, Ethernet_SlaveStation> EthernetDic;
        //public static void EnumCards()
        //{
        //    CC24_Comm.EnumCards();
        //    CommunicationOperator.cardDic = CC24_Comm.D_cardList;
        //}

        public static int OpenComm(CommConfigData mcommConfigData)
        {
            int result;
            bool flag3 = mcommConfigData.SerialNum.Contains("Tcp-");
            if (flag3)
            {
                bool flag4 = mcommConfigData.RoleName == "Server";
                if (flag4)
                {
                    MyTcpServer.CreateNewServer(mcommConfigData.LocalIp, Convert.ToInt32(mcommConfigData.LocalPort), 3, mcommConfigData.SerialNum, mcommConfigData.HBStr, mcommConfigData.HBFlag);
                    MyTcpServer.GetServerInstance(mcommConfigData.SerialNum).Start();
                }
                else
                {
                    MyTcpClient.CreateNewClient(mcommConfigData.LocalIp, Convert.ToInt32(mcommConfigData.LocalPort), mcommConfigData.RemoteIp, Convert.ToInt32(mcommConfigData.RemotePort), mcommConfigData.SerialNum, mcommConfigData.HBStr, mcommConfigData.HBFlag);
                    MyTcpClient.GetClientInstance(mcommConfigData.SerialNum).Connect();
                }
                result = 0;
            }
            else
            {
                result = -1;
            }
            //else
            //{
            //    bool flag5 = mcommConfigData.SerialNum.Contains("S7-Slave-");
            //    if (flag5)
            //    {
            //        S7_SlaveStation.CreateNewSlave(mcommConfigData.RemoteIp, Convert.ToInt32(mcommConfigData.RemotePort), (byte)mcommConfigData.Rack, (byte)mcommConfigData.Slot, mcommConfigData.ControlDBNum, mcommConfigData.StatusDBNum, mcommConfigData.SerialNum);
            //        S7_SlaveStation.GetSlaveInstance(mcommConfigData.SerialNum).Connect();
            //        result = 0;
            //    }
            //    else
            //    {
            //        bool flag6 = mcommConfigData.SerialNum.Contains("Ethernet-Slave-");
            //        if (flag6)
            //        {
            //            Ethernet_SlaveStation.CreateNewSlave(mcommConfigData.RemoteIp, Convert.ToInt32(mcommConfigData.RemotePort), mcommConfigData.SerialNum);
            //            Ethernet_SlaveStation.GetSlaveInstance(mcommConfigData.SerialNum).Connect();
            //            result = 0;
            //        }
            //        else
            //        {
            //            result = -1;
            //        }
            //    }
            //}

            return result;
        }

        public static IFlowState FindComm(CommConfigData mcommConfigData)
        {
            bool flag = CommunicationOperator.commCollection._commDic.ContainsKey(mcommConfigData.SerialNum);
            IFlowState result;
            if (flag)
            {
                result = CommunicationOperator.commCollection._commDic[mcommConfigData.SerialNum];
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}
