using System;
using System.Diagnostics;
using System.IO.Ports;
using System.Threading;

namespace NovaVision.Hardware.C_2DGigeLineScan.SDK_IKapLineScanTool
{
    internal class SerialPortControl
    {
        private SerialPort m_port = new SerialPort();

        private bool m_bOpenPort = false;

        private object m_locker = new object();

        private Stopwatch m_watch = new Stopwatch();

        public bool OpenPort(string portName, int bautRate)
        {
            if (IsOpen())
            {
                return true;
            }
            m_bOpenPort = false;
            Monitor.Enter(m_locker);
            m_port.PortName = portName;
            m_port.BaudRate = bautRate;
            m_port.StopBits = StopBits.One;
            m_port.DataBits = 8;
            m_port.Parity = Parity.None;
            m_port.ReadTimeout = 1000;
            m_port.WriteTimeout = 1000;
            try
            {
                m_port.Open();
                m_bOpenPort = true;
            }
            catch (Exception)
            {
                m_bOpenPort = false;
            }
            Monitor.Exit(m_locker);
            return m_bOpenPort;
        }

        public bool IsOpen()
        {
            return m_bOpenPort;
        }

        public bool ClosePort()
        {
            if (!IsOpen())
            {
                return true;
            }
            Monitor.Enter(m_locker);
            try
            {
                m_port.Close();
                m_bOpenPort = false;
            }
            catch (Exception)
            {
                m_bOpenPort = true;
            }
            Monitor.Exit(m_locker);
            return m_bOpenPort;
        }

        public bool WriteDataToPort(string strData, int nTimeOut)
        {
            bool bReturn = false;
            bReturn = WriteDataToPort(strData);
            if (!bReturn)
            {
                return bReturn;
            }
            string strReceive = ReadDataFromPort(nTimeOut);
            return strReceive.Contains(">Ok") ? true : false;
        }

        public bool WriteDataToPort(string strData)
        {
            bool bReturn = false;
            strData += "\r";
            Monitor.Enter(m_locker);
            try
            {
                m_port.Write(strData);
                bReturn = true;
            }
            catch (Exception)
            {
                bReturn = false;
            }
            Monitor.Exit(m_locker);
            return bReturn;
        }

        public string ReadDataFromPort(int nTimeout)
        {
            string strReadData = "";
            m_watch.Restart();
            TimeSpan ts = m_watch.Elapsed;
            Monitor.Enter(m_locker);
            while (ts.TotalMilliseconds <= (double)nTimeout && IsOpen())
            {
                strReadData += m_port.ReadExisting();
                if (strReadData.Contains(">Ok\r") || strReadData.Contains(">128\r") || strReadData.Contains(">130\r") || strReadData.Contains(">131\r") || strReadData.Contains(">132\r") || strReadData.Contains(">133\r"))
                {
                    break;
                }
            }
            Monitor.Exit(m_locker);
            m_watch.Stop();
            return strReadData;
        }
    }
}
