using System.Windows.Forms;
using NovaVision.BaseClass.Communication;

namespace NovaVision.VisionForm.CommunicationFrm
{
    public class OpenCommConfig
    {
        public static void OpenDiffTypeComm(CommConfigData commConfigData, CommBaseInfo mBaseInfo, string path)
        {
            if (commConfigData == null)
            {
                return;
            }
            if (commConfigData.CommCategory == "Tcp")
            {
                if (CommunicationOperator.commCollection._commDic.ContainsKey(commConfigData.SerialNum))
                {
                    TcpConfigForm _tcpConfigForm = new TcpConfigForm(commConfigData, mBaseInfo, path);
                    _tcpConfigForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("网络中无法创建名称为" + commConfigData.SerialNum + "的TCP对象，请检查IP和端口号是否正确！");
                }
            }
            //else if (commConfigData.CommCategory == "CC24")
            //{
            //    if (CommunicationOperator.commCollection._commDic.ContainsKey(commConfigData.SerialNum))
            //    {
            //        CC24ConfigForm _cc24ConfigForm = new CC24ConfigForm(commConfigData, mBaseInfo, path);
            //        _cc24ConfigForm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("设备中未查找到序列号为" + commConfigData.SerialNum + "的CC24卡或该通讯卡故障！");
            //    }
            //}
            //else if (commConfigData.CommCategory == "S7")
            //{
            //    if (CommunicationOperator.commCollection._commDic.ContainsKey(commConfigData.SerialNum))
            //    {
            //        S7ConfigForm _slaveConfigForm2 = new S7ConfigForm(commConfigData, mBaseInfo, path);
            //        _slaveConfigForm2.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("网络中无法创建名称为" + commConfigData.SerialNum + "的S7-Slave对象，请检查IP和端口号是否正确或者PLC是否正常开启服务！");
            //    }
            //}
            //else if (commConfigData.CommCategory == "Ethernet")
            //{
            //    if (CommunicationOperator.commCollection._commDic.ContainsKey(commConfigData.SerialNum))
            //    {
            //        EthernetConfigForm _slaveConfigForm = new EthernetConfigForm(commConfigData, mBaseInfo, path);
            //        _slaveConfigForm.ShowDialog();
            //    }
            //    else
            //    {
            //        MessageBox.Show("网络中无法创建名称为" + commConfigData.SerialNum + "的Ethernet-Slave对象，请检查IP和端口号是否正确或者PLC是否正常开启服务！");
            //    }
            //}
        }
    }
}
