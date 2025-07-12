using NovaVision.VisionForm.MainForm;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace NovaVision.BaseClass;

public class LogUtil
{
    public static TextBox TextBoxInfo;

    public static TextBox TextBoxError;

    public static bool IsIgnore;

    public static string path;

    public static string pathMesLog;

    public static int fileSize;

    public static int lineCount;

    private static ConcurrentQueue<string> msgQueue;

    //public static ConcurrentQueue<MesLogInfo> mesInfoQueue;

    private static object mesLock;

    private static ConcurrentQueue<string> _pendingInfoLogs = new ConcurrentQueue<string>();
    private static ConcurrentQueue<string> _pendingErrorLogs = new ConcurrentQueue<string>();
    private static volatile bool _controlsReady = false;

    static LogUtil()
    {
        IsIgnore = false;
        path = "D:\\log";
        pathMesLog = "D:\\MESLOG\\";
        fileSize = 10485760;
        lineCount = 1500;
        msgQueue = new ConcurrentQueue<string>();
        //mesInfoQueue = new ConcurrentQueue<MesLogInfo>();
        mesLock = new object();
        Thread thread = new Thread((ThreadStart)delegate
        {
            try
            {
                int num = 0;
                while (true)
                {
                    int num2 = 0;
                    List<string> list = new List<string>();
                    string result;
                    while (msgQueue.TryDequeue(out result) && num2++ < 10000)
                    {
                        list.Add(result);
                    }
                    if (list.Count > 0)
                    {
                        WriteFile(list, CreateLogPath());
                        ShowLog(list);
                    }
                    //if (mesInfoQueue.TryDequeue(out var result2))
                    //{
                    //    Save_Log(result2);
                    //}
                    if (num++ > 2000)
                    {
                        num = 0;
                        FileOperator.DeleteFileN(path, 30, flag: false);
                    }
                    Thread.Sleep(200);
                }
            }
            catch
            {
            }
        });
        thread.IsBackground = true;
        thread.Start();
    }

    public static void SetControlsReady()
    {
        _controlsReady = true;
        ProcessCachedLogs();
    }
    private static void ProcessCachedLogs()
    {
        // 处理缓存的信息日志
        ProcessLogQueue(_pendingInfoLogs, isError: false);
        // 处理缓存的错误日志
        ProcessLogQueue(_pendingErrorLogs, isError: true);
    }
    private static void ProcessLogQueue(ConcurrentQueue<string> queue, bool isError)
    {
        const int batchSize = 100; // 每批处理100条避免界面卡顿
        var logsToProcess = new List<string>();

        while (queue.TryDequeue(out var log) && logsToProcess.Count < batchSize)
        {
            logsToProcess.Add(log);
        }

        if (logsToProcess.Count > 0)
        {
            ShowLogs(logsToProcess, isError);
        }
    }
    public static void WriteFile(List<string> list, string path)
    {
        try
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            if (!File.Exists(path))
            {
                using FileStream fileStream = new FileStream(path, FileMode.Create);
                fileStream.Close();
            }
            using FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            try
            {
                list.ForEach(delegate (string item)
                {
                    sw.WriteLine(item);
                });
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                {
                    ((IDisposable)sw).Dispose();
                }
            }
            fs.Close();
        }
        catch (Exception)
        {
        }
    }

    public static void ShowLog(List<string> list)
    {
        if (IsIgnore) return;

        // 分离正常日志和错误日志
        var infoLogs = new List<string>();
        var errorLogs = new List<string>();

        foreach (var item in list)
        {
            if (item.Length >= 24 + 6 && item.Substring(24, 6) == "[Info]")
            {
                infoLogs.Add(item);
            }
            else
            {
                errorLogs.Add(item);
            }
        }

        if (_controlsReady)
        {
            // 直接显示
            ShowLogs(infoLogs, isError: false);
            ShowLogs(errorLogs, isError: true);
        }
        else
        {
            // 加入缓存队列
            foreach (var log in infoLogs) _pendingInfoLogs.Enqueue(log);
            foreach (var log in errorLogs) _pendingErrorLogs.Enqueue(log);
        }
    }

    private static void ShowLogs(List<string> logs, bool isError)
    {
        if (logs.Count == 0) return;

        var targetBox = isError ? TextBoxError : TextBoxInfo;
        if (targetBox == null || targetBox.IsDisposed) return;

        try
        {
            if (targetBox.InvokeRequired)
            {
                targetBox.BeginInvoke((Action)(() =>
                {
                    AppendLogsToControl(logs, targetBox);
                }));
            }
            else
            {
                AppendLogsToControl(logs, targetBox);
            }
        }
        catch (ObjectDisposedException)
        {
            // 忽略已释放的控件
        }
    }
    private static void AppendLogsToControl(List<string> logs, TextBox textBox)
    {
        if (textBox.Lines.Length + logs.Count > lineCount)
        {
            textBox.Clear();
        }

        var sb = new StringBuilder();
        logs.ForEach(s => sb.AppendLine(s));

        // 使用追加模式并限制频率
        textBox.AppendText(sb.ToString());
        textBox.SelectionStart = textBox.Text.Length;
        textBox.ScrollToCaret();
    }

    public static string CreateLogPath()
    {
        int index = 0;
        bool bl = true;
        string logPath;
        do
        {
            index++;
            logPath = Path.Combine(path, "Log" + DateTime.Now.ToString("yyyyMMdd") + ((index == 1) ? "" : ("_" + index)) + ".txt");
            if (File.Exists(logPath))
            {
                FileInfo fileInfo = new FileInfo(logPath);
                if (fileInfo.Length < fileSize)
                {
                    bl = false;
                }
            }
            else
            {
                bl = false;
            }
        }
        while (bl);
        return logPath;
    }

    public static void LogError(string log)
    {
        //if (MonitorInfo.PushEnable)
        //{
        //    MonitorAlarmsInfo monitorAlarmsInfo = new MonitorAlarmsInfo();
        //    monitorAlarmsInfo.DateTime = DateTime.Now;
        //    monitorAlarmsInfo.AlarmMessage = "[以正常数据类型推送的Error]" + log;
        //    MonitorInfo.MonitorAlarmsQueue.Enqueue(monitorAlarmsInfo);
        //    if (log.Contains("采集超时"))
        //    {
        //        monitorAlarmsInfo.DateTime = DateTime.Now;
        //        monitorAlarmsInfo.AlarmMessage = AlarmsErrorTitle.CCD采图异常报警.ToString();
        //        MonitorInfo.MonitorAlarmsQueue.Enqueue(monitorAlarmsInfo);
        //    }
        //}
        string str = string.Format("{0} {1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "[Error] ", log);
        msgQueue.Enqueue(str);
    }

    public static void Log(string log)
    {
        string str = string.Format("{0} {1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), "[Info]  ", log);
        msgQueue.Enqueue(str);
    }

    //public static void Save_Log(MesLogInfo mesLogInfo)
    //{
    //    try
    //    {
    //        string Path_Mess = pathMesLog + "\\" + mesLogInfo.str_MESType;
    //        string sbh = ((mesLogInfo.str_Resource == null) ? "设备号未定义" : mesLogInfo.str_Resource);
    //        string Path_Mess_Name = Path_Mess + "\\" + sbh + "_log_" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day + ".csv";
    //        FileStream fs_Mess = null;
    //        if (!Directory.Exists(Path_Mess))
    //        {
    //            Directory.CreateDirectory(Path_Mess);
    //        }
    //        if (!File.Exists(Path_Mess_Name))
    //        {
    //            fs_Mess = new FileStream(Path_Mess_Name, FileMode.OpenOrCreate);
    //            StreamWriter sb = new StreamWriter(fs_Mess, Encoding.UTF8);
    //            sb.WriteLine("SFC,接口名称,接口调用开始时间,接口调用传参,返回代码,返回信息,耗时（ms）,接口调用返回时间\r\n");
    //            sb.Close();
    //            sb.Dispose();
    //            fs_Mess.Close();
    //        }
    //        StreamWriter sw_Mess = new StreamWriter(Path_Mess_Name, append: true, Encoding.UTF8);
    //        string str_Parameta = mesLogInfo.str_Parameta.Replace(",", ".");
    //        sw_Mess.WriteLine(mesLogInfo.str_SFC + "," + mesLogInfo.str_MESType.Split('_')[0] + "," + mesLogInfo.str_StartTime + "," + str_Parameta + "," + mesLogInfo.str_ReturnCode + "," + mesLogInfo.str_ReturnMess + "," + mesLogInfo.str_UesTime + "," + mesLogInfo.str_EndTime);
    //        sw_Mess.Close();
    //        sw_Mess.Dispose();
    //        sw_Mess = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        LogError("MesLog保存失败，详情见：" + ex.Message);
    //    }
    //}

    public static void Save_Log(string str_MESType, string str_SFC, string str_StartTime, string str_Parameta, string str_EndTime, string str_UesTime, string str_ReturnCode, string str_ReturnMess, string str_Resource = "")
    {
        //try
        //{
        //    MesLogInfo mesLogInfo = new MesLogInfo
        //    {
        //        str_MESType = str_MESType,
        //        str_SFC = str_SFC,
        //        str_StartTime = str_StartTime,
        //        str_Parameta = str_Parameta,
        //        str_EndTime = str_EndTime,
        //        str_UesTime = str_UesTime,
        //        str_ReturnCode = str_ReturnCode,
        //        str_ReturnMess = str_ReturnMess,
        //        str_Resource = str_Resource
        //    };
        //    mesInfoQueue.Enqueue(mesLogInfo);
        //}
        //catch (Exception)
        //{
        //}
    }

    public static string[] CSVstrToArry(string splitStr)
    {
        string newstr = string.Empty;
        List<string> sList = new List<string>();
        bool isSplice = false;
        string[] array = splitStr.Split(',');
        string[] array2 = array;
        foreach (string str in array2)
        {
            if (!string.IsNullOrEmpty(str) && str.IndexOf('"') > -1)
            {
                string firstchar = str.Substring(0, 1);
                string lastchar = string.Empty;
                if (str.Length > 0)
                {
                    lastchar = str.Substring(str.Length - 1, 1);
                }
                if (firstchar.Equals("\"") && !lastchar.Equals("\""))
                {
                    isSplice = true;
                }
                if (lastchar.Equals("\""))
                {
                    newstr = (isSplice ? (newstr + "," + str) : (newstr + str));
                    isSplice = false;
                }
            }
            else if (string.IsNullOrEmpty(newstr))
            {
                newstr += str;
            }
            if (isSplice)
            {
                newstr = ((!string.IsNullOrEmpty(newstr)) ? (newstr + "," + str) : (newstr + str));
                continue;
            }
            sList.Add(newstr.Replace("\"", "").Trim());
            newstr = string.Empty;
        }
        return sList.ToArray();
    }
}