using System;
using System.CodeDom;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Cognex.VisionPro;
using Cognex.VisionPro.Display;
using Cognex.VisionPro.ToolBlock;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Communication;
using NovaVision.BaseClass.EnumHelper;
using NovaVision.BaseClass.Helper;
using NovaVision.BaseClass.VisionConfig;
using NovaVision.Hardware;
using NovaVision.UserControlLibrary;
using ICogImage = Cognex.VisionPro.ICogImage;
using ICogRecord = Cognex.VisionPro.ICogRecord;
using JobData = NovaVision.BaseClass.VisionConfig.JobData;

namespace NovaVision.WorkFlow;

public sealed class TaskFlow
{
    private Stopwatch stp;
    public readonly ConcurrentQueue<ImageInfo> ImageQueue;
    private readonly ConcurrentQueue<ImageRecordInfo> ImageRecordQueue;

    readonly JobData mJobData;
    private readonly InspectionConfig mInspectionConfig;
    private CameraBase mCamera;
    private IFlowState mComm;
    CameraConfigData mCamData;
    CogToolBlock mToolBlock;
    ImageDisplay mDisplay;

    private string WorkFullName;
    string CameraName;
    string CameraSerialNum;
    string CameraType;
    private string TriggerPoint;
    private string TriggerNum;

    private string DisplayKey;
    public int mRecordIndex = 0;
    private int Index;
    public bool IsStationFullConfig;
    bool IsInspectFlowStart;
    public bool IsOnLine;

    private string SaveCode = "";
    private readonly List<bool[][]> FlyResults = new ();


    public TaskFlow(InspectionConfig inspection, JobData jobData)
    {
        stp = new Stopwatch();
        ImageQueue = new ConcurrentQueue<ImageInfo>();
        ImageRecordQueue = new ConcurrentQueue<ImageRecordInfo>();
        mJobData = jobData;
        mInspectionConfig = inspection;
    }

    public void InitTaskFlow()
    {
        try
        {
            WorkFullName = $"[{mInspectionConfig.Station}][{mInspectionConfig.Inspect}]";
            IsStationFullConfig = CheckStationConfig(mInspectionConfig);
            CameraName = mInspectionConfig.CameraName;
            CameraSerialNum = mInspectionConfig.CameraSerialNum;
            TriggerPoint = mInspectionConfig.TriggerPoint;
            TriggerNum = mInspectionConfig.TriggerNum;
            CameraType = mInspectionConfig.CameraType;
            DisplayKey = mInspectionConfig.ImageDisplay;
            mToolBlock = mJobData.mTools[mInspectionConfig.Algorithm];
            mDisplay = mJobData.mUIControl.mImageDisplays[mInspectionConfig.ImageDisplay];
            mCamData = mJobData.mCameraData[CameraName];
            InitCameraParam();
            mComm = CommunicationOperator.commCollection._commDic[mInspectionConfig.CommSerialNum];
        }
        catch (Exception e)
        {
            LogError($"初始化流程{WorkFullName}异常,{e}");
        }
    }

    public bool CheckStationConfig(InspectionConfig inspection)
    {
        string stationName = inspection.Station;
        string inspectName = inspection.Inspect;
        if (inspection.InspectType == InspectType.Summary ||
            inspection.InspectType == InspectType.StageSummary)
        {
            if (string.IsNullOrEmpty(inspection.TriggerPoint))
            {
                LogError("工位" + stationName + "，检测" + inspectName + "：触发点位配置为空");
                return false;
            }

            if (string.IsNullOrEmpty(inspection.TriggerNum))
            {
                LogError("工位" + stationName + "，检测" + inspectName + "：触发位置号为空");
                return false;
            }

            if (string.IsNullOrEmpty(inspection.CommunicationTable))
            {
                LogError("工位" + stationName + "，检测" + inspectName + "：主通讯表名配置为空");
                return false;
            }

            if (string.IsNullOrEmpty(inspection.CommSerialNum))
            {
                LogError("工位" + stationName + "，检测" + inspectName + "：主通讯序列号为空");
                return false;
            }

            if (!inspection.IsIgnoreComm_A)
            {
                if (string.IsNullOrEmpty(inspection.CommunicationTable_A))
                {
                    LogError("工位" + stationName + "，检测" + inspectName + "：副通讯表名配置为空");
                    return false;
                }

                if (string.IsNullOrEmpty(inspection.CommSerialNum_A))
                {
                    LogError("工位" + stationName + "，检测" + inspectName + "：副通讯序列号为空");
                    return false;
                }
            }

            if (string.IsNullOrEmpty(inspection.Algorithm))
            {
                LogError("工位" + stationName + "，检测" + inspectName + "：算法配置为空");
                return false;
            }
        }

        if (string.IsNullOrEmpty(inspection.CameraName))
        {
            LogError("工位" + stationName + "，检测" + inspectName + "：相机参数配置为空");
            return false;
        }

        if (string.IsNullOrEmpty(inspection.CameraSerialNum))
        {
            LogError("工位" + stationName + "，检测" + inspectName + "：相机序列号为空");
            return false;
        }

        if (string.IsNullOrEmpty(inspection.CameraType))
        {
            LogError("工位" + stationName + "，检测" + inspectName + "：相机类型配置为空");
            return false;
        }

        if (string.IsNullOrEmpty(inspection.TriggerPoint))
        {
            LogError("工位" + stationName + "，检测" + inspectName + "：触发点位配置为空");
            return false;
        }

        if (string.IsNullOrEmpty(inspection.TriggerNum))
        {
            LogError("工位" + stationName + "，检测" + inspectName + "：触发位置号为空");
            return false;
        }

        if (string.IsNullOrEmpty(inspection.CommunicationTable))
        {
            LogError("工位" + stationName + "，检测" + inspectName + "：主通讯表名配置为空");
            return false;
        }

        if (string.IsNullOrEmpty(inspection.CommSerialNum))
        {
            LogError("工位" + stationName + "，检测" + inspectName + "：主通讯序列号为空");
            return false;
        }

        if (!inspection.IsIgnoreComm_A)
        {
            if (string.IsNullOrEmpty(inspection.CommunicationTable_A))
            {
                LogError("工位" + stationName + "，检测" + inspectName + "：副通讯表名配置为空");
                return false;
            }

            if (string.IsNullOrEmpty(inspection.CommSerialNum_A))
            {
                LogError("工位" + stationName + "，检测" + inspectName + "：副通讯序列号为空");
                return false;
            }
        }

        if (string.IsNullOrEmpty(inspection.Algorithm))
        {
            LogError("工位" + stationName + "，检测" + inspectName + "：算法配置为空");
            return false;
        }

        return true;
    }

    private void InitCameraParam()
    {
        try
        {
            if (!string.IsNullOrEmpty(CameraType))
            {

                switch (CameraType)
                {
                    case "3D":
                    {
                        mCamera = CameraOperator.camera3DCollection[CameraSerialNum];
                        //Log("(" + CameraSerialNum + ")3D相机设置拍照参数!");
                        //stp.Restart();
                        //CameraOperator.camera3DCollection[CameraSerialNum].Stop_Grab(state: false);
                        //stp.Stop();
                        //Log("(" + CameraSerialNum + ")停止用时：" + stp.ElapsedMilliseconds);
                        //stp.Restart();
                        //CameraOperator.camera3DCollection[CameraSerialNum].SetCameraSetting(mCamData);
                        //stp.Stop();
                        //Log("(" + CameraSerialNum + ")3D相机设置拍照参数用时" + stp.ElapsedMilliseconds);
                        //stp.Restart();
                        //CameraOperator.camera3DCollection[CameraSerialNum].Start_Grab(state: false);
                        //stp.Stop();
                        //Log("(" + CameraSerialNum + ")3D相机开始用时：" + stp.ElapsedMilliseconds);
                        break;
                    }
                    case "2D_LineScan":
                    {
                        mCamera = CameraOperator.camera2DLineCollection[CameraSerialNum];
                        //Log("(" + CameraSerialNum + ")2D线扫相机设置拍照参数!");
                        //stp.Restart();
                        //CameraOperator.camera2DLineCollection[CameraSerialNum].Stop_Grab(state: false);
                        //CameraOperator.camera2DLineCollection[CameraSerialNum].SetCameraSetting(mCamData);
                        //CameraOperator.camera2DLineCollection[CameraSerialNum].Start_Grab(state: false);
                        //stp.Stop();
                        //Log("(" + CameraSerialNum + ")2D线扫相机设置拍照参数用时" + stp.ElapsedMilliseconds);
                        break;
                    }
                    case "2D":
                    {
                        mCamera = CameraOperator.camera2DCollection[CameraSerialNum];
                        //Log("(" + CameraSerialNum + ")2D相机设置拍照参数!");
                        //stp.Restart();
                        ////CameraOperator.camera2DCollection[CameraSerialNum].Stop_Grab(state: false);
                        //CameraOperator.camera2DCollection[CameraSerialNum].SetCameraSetting(mCamData);
                        ////CameraOperator.camera2DCollection[CameraSerialNum].Start_Grab(state: false);
                        //stp.Stop();
                        //Log("(" + CameraSerialNum + ")2D相机设置拍照参数用时" + stp.ElapsedMilliseconds);
                        break;
                    }
                }
            }
        }
        catch (Exception)
        {
            LogError($"{WorkFullName}初始化相机失败");
        }
    }

    private void Log(string msg)
    {
        LogUtil.Log($"{WorkFullName} ,{msg}");
    }

    private void LogError(string msg)
    {
        LogUtil.LogError($"[{WorkFullName}]{msg}");
    }

    private void TaskFlow_Trigger(object sender, string triggerPoint,string triggeNum)
    {
        if (!IsOnLine)
            return;

        if (sender is IFlowState)
        {
            if (triggerPoint == TriggerPoint && triggeNum == TriggerNum)
            {
                Log($"收到触发信号[触发点]{triggerPoint},[触发位置号]{triggeNum}");
                SaveCode = $"\\{DateTime.Now:HH_mm_ss_ff}";
                FlyResults.Clear();
                AutoRun();
            }
        }
        else
        {
            LogError("通讯不是IFlowState类型！");
        }
    }

    public void AutoRun()
    {
        try
        {
            if (!IsStationFullConfig)
            {
                Log($"配置不完整，执行流程失败，请检查配置!");
                return;
            }
            if (!string.IsNullOrEmpty(CameraType))
            {
                if (mInspectionConfig.IsIgnore)
                {
                    Log($"设置为忽略，不执行该流程!");
                    return;
                }
                Index = 0;
                switch (CameraType)
                {
                    case "3D":
                    {
                        Carema3D_Run();
                        break;
                    }
                    case "2D":
                    {
                        Carema2D_Run();
                        break;
                    }
                }
            }
            else
            {
                Log($"相机配置为空,执行流程失败");
            }
        }
        catch (Exception e)
        {
            LogError(e.ToString());
            throw;
        }
    }

    public void ManualRun(List<ICogImage> images)
    {
        StartWorkFlow();
        Index = 0;
        FlyResults.Clear();
        for (int i = 0; i < images.Count; i++)
        {
            ImageInfo imageInfo = default(ImageInfo);
            imageInfo.CogImage = images[i];
            imageInfo.Index = i + 1;
            ImageQueue.Enqueue(imageInfo);
        }
    }

    public void StartWorkFlow()
    {
        if (!IsInspectFlowStart)
        {
            InspectFlowStart();
        }
        
        if (IsOnLine)
        {
            mComm.Trigger -= TaskFlow_Trigger;
            mComm.Trigger += TaskFlow_Trigger;
        }
    }
    public void StopWorkFlow()
    {
        IsInspectFlowStart = false;
        if (mCamera != null)
        {
            mCamera.UpdateImage = null;
            //mCamera.Stop_Grab(state: false);
        }
        if (mComm != null)
        {
            mComm.Trigger -= TaskFlow_Trigger;
        }
    }

    public int Carema2D_Run()
    {
        try
        {
            CameraOperator.camera2DCollection[CameraSerialNum].UpdateImage = delegate(ImageData imageData)
            {
                Index++;
                ImageInfo item = default(ImageInfo);
                item.Index = Index;
                item.CogImage = imageData.CogImage;
                ImageQueue.Enqueue(item);
                Log($"单个2D相机{CameraName}{CameraSerialNum} 拍照完成次数{item.Index}");
            };
            CameraOperator.camera2DCollection[CameraSerialNum].SetCameraSetting(mCamData);
            
            if (mCamData.SettingParams.TriggerMode == 0)
            {
                stp.Restart();
                CameraOperator.camera2DCollection[CameraSerialNum].SoftwareTriggerOnce();
                stp.Stop();
                Log("2D相机" + CameraName + " " + CameraSerialNum + "触发拍照指令执行时间" + stp.ElapsedMilliseconds);
            }

            return 1;
        }
        catch (Exception ex)
        {
            LogError("（2D相机取像）配置" + CameraName + "，序列号" + CameraSerialNum + "：" + ex);
            return -1;
        }
    }

    public void Carema3D_Run()
    {
        try
        {

            stp.Restart();
            CameraOperator.camera3DCollection[CameraSerialNum].Stop_Grab(state: false);
            CameraOperator.camera3DCollection[CameraSerialNum].SetCameraSetting(mCamData);
            CameraOperator.camera3DCollection[CameraSerialNum].Start_Grab(state: false);
            stp.Stop();
            Log($"3D相机{CameraSerialNum} 运行中设置拍照参数用时{stp.ElapsedMilliseconds}");

            CameraOperator.camera3DCollection[CameraSerialNum].UpdateImage = delegate(ImageData imageData)
            {
                Index++;
                ImageInfo item = default(ImageInfo);
                item.Index = Index;
                item.CogImage = imageData.CogImage;
                ImageQueue.Enqueue(item);
                Log($"单个3D相机{CameraName}{CameraSerialNum} 拍照完成次数{Index}");
            };
            if (mCamData.SettingParams.TriggerMode == 1 || mCamData.SettingParams.TriggerMode == 3)
            {
                stp.Restart();
                int ret = CameraOperator.camera3DCollection[CameraSerialNum].SoftTriggerOnce();
                stp.Stop();
                Log("3D相机" + " " + CameraSerialNum + "触发拍照指令执行时间" + stp.ElapsedMilliseconds);
                if (ret == 0)
                {
                    Log("3D相机" + " " + CameraSerialNum + "触发一次拍照指令执行成功!");
                }
                else
                {
                    Log("3D相机" + " " + CameraSerialNum + "触发一次拍照指令执行失败!");
                }
            }
        }
        catch (Exception ex)
        {
            LogError("（3D相机取像）机配置" + "，序列号" + CameraSerialNum + "：" + ex.Message);
        }
    }

    private void RunTool(ImageInfo imageInfo)
    {
        try
        {
            string alg_key = mInspectionConfig.Algorithm;
            var type = alg_key.ToEnum<EnumStation>();
            string sendData = "";

            if (mJobData.mAlgorithmParameter.ContainsKey(alg_key))
            {
                CogToolBlock toolFlow = (CogToolBlock)mToolBlock.Tools["检测流程"];
                foreach (var key in mJobData.mAlgorithmParameter[alg_key].GetKeys())
                {
                    if (mJobData.mAlgorithmParameter[alg_key][key].IsEnable)
                    {
                        if (toolFlow.DisabledTools.Contains(key))
                        {
                            toolFlow.DisabledTools.Remove(toolFlow.Tools[key]);
                        }
                    }
                    else
                    {
                        if (!toolFlow.DisabledTools.Contains(key))
                        {
                            toolFlow.DisabledTools.Add(toolFlow.Tools[key]);
                        }
                    }

                    for (int i = 0; i < toolFlow.Tools.Count; i++)
                    {
                        if (toolFlow.Tools[i].Name == key)
                        {
                            var tool = (CogToolBlock)toolFlow.Tools[i];
                            tool.Inputs["Max"].Value = mJobData.mAlgorithmParameter[alg_key][key].LimitMaxValue;
                            tool.Inputs["Min"].Value = mJobData.mAlgorithmParameter[alg_key][key].LimitMinValue;
                        }
                    }
                }
            }

            if (type == EnumStation.正面线扫检测 || type == EnumStation.反面线扫检测)
            {
                mToolBlock.Inputs["Image"].Value = imageInfo.CogImage;
                mToolBlock.Inputs["Index"].Value = imageInfo.Index;
                mToolBlock.Inputs["Row"].Value = mJobData.mProductInfo.Row;
                mToolBlock.Inputs["Col"].Value = mJobData.mProductInfo.Col;
                mToolBlock.Run();
                if (mToolBlock.RunStatus.Result == CogToolResultConstants.Accept)
                {
                }
                else
                {
                    LogError($"检测算法[{alg_key}],运行出错,错误信息" + mToolBlock.RunStatus.Message);
                }

                var Results = (List<bool>)mToolBlock.Outputs["Results"].Value;
                bool result = true;
                foreach (var item in Results)
                {
                    if (item)
                    {
                        sendData += "1,";
                    }
                    else
                    {
                        sendData += "2,";
                        result = false;
                    }
                }
                switch (type)
                {
                    case EnumStation.正面线扫检测:
                        {
                            sendData = sendData.TrimEnd(',') + "-a";
                            break;
                        }
                    case EnumStation.反面线扫检测:
                        {
                            sendData = sendData.TrimEnd(',') + "-b";

                            break;
                        }
                }
                mComm.SendData(sendData);
                Log("发送结果给客户端");

                try
                {
                    ImageRecordInfo imageRecodInfo = default(ImageRecordInfo);
                    imageRecodInfo.ImageSaveInfo = default(ImageSaveInfo);
                    imageRecodInfo.ImageSaveInfo.IsOKorNG = result;
                    CreateRecordInfo(imageInfo, ref imageRecodInfo);

                    if (DisplayKey != "")
                    {
                        imageRecodInfo.CogRecord = SelectRecord(mToolBlock.CreateLastRunRecord(), mRecordIndex, DisplayKey);
                    }
                    else
                    {
                        Log("未关联显示界面");
                    }

                    ImageRecordQueue.Enqueue(imageRecodInfo);
                }
                catch (Exception e)
                {
                    LogError("结果图像显示异常" + e);
                }
                Log($"检测完成");
            }
            else
            {
                string end = "";
                int FlyRow;
                int index = imageInfo.Index;
                switch (type)
                {
                    case EnumStation.飞拍相机1:
                    {
                        end = "-c";
                        FlyRow = mJobData.mProductInfo.Fly1Row;
                        mToolBlock.Inputs["Row"].Value = FlyRow;
                        mToolBlock.Inputs["Col"].Value = mJobData.mProductInfo.FlyColArray1[index - 1];
                        mToolBlock.Inputs["DnnInference"].Value = DnnSingleton.Instance.FlyFrontDnnInference1;
                        break;
                    }
                    case EnumStation.飞拍相机2:
                    {
                        end = "-d";
                        FlyRow = mJobData.mProductInfo.Fly2Row;
                        mToolBlock.Inputs["Row"].Value = FlyRow;
                        mToolBlock.Inputs["Col"].Value = mJobData.mProductInfo.FlyColArray2[index - 1];
                            mToolBlock.Inputs["DnnInference"].Value = DnnSingleton.Instance.FlyFrontDnnInference2;
                        break;
                    }
                    case EnumStation.飞拍相机3:
                    {
                        end = "-e";
                        for (int i = 0; i < mJobData.mProductInfo.Row * mJobData.mProductInfo.Col; i++)
                        {
                            sendData += "1,";
                        }

                        sendData = sendData.TrimEnd(',') + end;
                        mComm.SendData(sendData);
                        return;
                    }
                    case EnumStation.飞拍相机4:
                    {
                        end = "-f";
                        for (int i = 0; i < mJobData.mProductInfo.Row * mJobData.mProductInfo.Col; i++)
                        {
                            sendData += "1,";
                        }

                        sendData = sendData.TrimEnd(',') + end;
                        mComm.SendData(sendData);
                        return;
                    }
                }

                mToolBlock.Inputs["Image"].Value = imageInfo.CogImage;
                mToolBlock.Inputs["Index"].Value = imageInfo.Index;

                mToolBlock.Run();
                if (mToolBlock.RunStatus.Result != CogToolResultConstants.Accept)
                {
                    LogError($"检测算法[{alg_key}],运行出错,错误信息" + mToolBlock.RunStatus.Message);
                }

                bool result = true;
                var Results = (bool[][])mToolBlock.Outputs["ArrayResult"].Value;
                FlyResults.Add(Results);
                foreach (var item in FlyResults)
                {
                    for (int i = 0; i < item.GetLength(0); i++)
                    {
                        var array = item[i];
                        for (int j = 0; j < array.Length; j++)
                        {
                            if (!array[j])
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }
                int[,] aw2D = new int[mJobData.mProductInfo.Row, mJobData.mProductInfo.Col];

                if (imageInfo.Index == mInspectionConfig.ExternalTriggerTimes)
                {
                    if (alg_key == EnumStation.飞拍相机1.GetDescription())
                    {
                        for (int row = 0; row < FlyResults[0].GetLength(0); row++)
                        {
                            int col = 0;
                            for (int k = FlyResults.Count - 1; k >= 0; k--)
                            {
                                var array2D = FlyResults[k];
                                var array = array2D[row];
                                for (int j = 0; j < array.Length; j++)
                                {
                                    if (array[j])
                                    {
                                        sendData += "1,";
                                        aw2D[row, col] = 1;
                                    }
                                    else
                                    {
                                        sendData += "2,";
                                        aw2D[row, col] = 2;
                                    }
                                    col++;
                                }
                            }
                        }

                        for (int i = mJobData.mProductInfo.Row - mJobData.mProductInfo.Fly1Row;
                             i < mJobData.mProductInfo.Row;
                             i++)
                        {
                            for (int j = 0; j < mJobData.mProductInfo.Col; j++)
                            {
                                sendData += "1,";
                                aw2D[i, j] = 1;
                            }
                        }
                    }
                    else if(alg_key == EnumStation.飞拍相机2.GetDescription())
                    {
                        for (int i = 0; i < mJobData.mProductInfo.Fly1Row; i++)
                        {
                            for (int j = 0; j < mJobData.mProductInfo.Col; j++)
                            {
                                sendData += "1,";
                                aw2D[i, j] = 1;
                            }
                        }

                        int n = mJobData.mProductInfo.Row - mJobData.mProductInfo.Fly1Row;
                        for (int row = 0; row < FlyResults[0].GetLength(0); row++)
                        {
                            int col = 0;
                            for (int k = FlyResults.Count - 1; k >= 0; k--)
                            {
                                var array2D = FlyResults[k];
                                var array = array2D[row];
                                for (int j = 0; j < array.Length; j++)
                                {
                                    if (array[j])
                                    {
                                        sendData += "1,";
                                        aw2D[n, col] = 1;
                                    }
                                    else
                                    {
                                        sendData += "2,";
                                        aw2D[n, col] = 2;
                                    }

                                    col++;
                                }
                            }
                            n++;
                        }
                    }

                    FlyResults.Clear();
                    sendData = sendData.TrimEnd(',') + end;
                    //mComm.SendData(sendData);
                    Log("发送结果给客户端");
                    try
                    {
                        // 文件路径
                        string filePath = $"D:\\结果发送\\{alg_key}.txt"; // 扩展名也可用.txt
                        //---------- 核心修复点：检查并创建目录 ----------//
                        string directory = Path.GetDirectoryName(filePath);
                        if (!Directory.Exists(directory) && !string.IsNullOrEmpty(directory))
                        {
                            Directory.CreateDirectory(directory); // 自动创建缺失的目录
                        }
                        // 使用 StreamWriter 写入文件（覆盖模式）
                        using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                        {
                            for (int row = 0; row < aw2D.GetLength(0); row++)
                            {
                                // 构造当前行的逗号分隔字符串
                                string[] elements = new string[aw2D.GetLength(1)];
                                for (int col = 0; col < aw2D.GetLength(1); col++)
                                {
                                    elements[col] = aw2D[row, col].ToString();
                                }
                                writer.WriteLine(string.Join("", elements)); // 写入行
                            }
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                try
                {
                    ImageRecordInfo imageRecodInfo = default(ImageRecordInfo);
                    imageRecodInfo.ImageSaveInfo = default(ImageSaveInfo);
                    imageRecodInfo.ImageSaveInfo.IsOKorNG = result;
                    CreateRecordInfo(imageInfo, ref imageRecodInfo);

                    if (DisplayKey != "")
                    {
                        imageRecodInfo.CogRecord = SelectRecord(mToolBlock.CreateLastRunRecord(), mRecordIndex, DisplayKey);
                    }
                    else
                    {
                        Log("未关联显示界面");
                    }
                    ImageRecordQueue.Enqueue(imageRecodInfo);
                }
                catch (Exception e)
                {
                    LogError("结果图像显示异常" + e);
                }
                LogUtil.Log("检测完成");
            }
        }
        catch (Exception e)
        {
            LogError("检测流程异常" + e);
        }
    }

    private ICogRecord SelectRecord(ICogRecord cogRecord, int displayIndex, string displayKey)
    {
        if (cogRecord != null && !string.IsNullOrEmpty(displayKey))
        {
            int Records_Count = cogRecord.SubRecords.Count;
            if (Records_Count > 0)
            {
                if (displayIndex >= 0 && displayIndex < Records_Count)
                {
                    return cogRecord.SubRecords[displayIndex];
                }
                return cogRecord.SubRecords[0];
            }
        }
        return null;
    }

    private void InspectFlowStart()
    {
        IsInspectFlowStart = true;
        Task.Factory.StartNew(delegate
        {
            while (IsInspectFlowStart)
            {
                ImageQueue.ToArray();
                
                if (ImageQueue.TryDequeue(out var result))
                {
                    
                    Log($"图像{result.Index}检测线程开始,,线程ID[{Thread.CurrentThread.ManagedThreadId}]");
                    RunTool(result);
                    Log($"图像{result.Index}检测完成");
                }

                Thread.Sleep(10);
            }
        }, TaskCreationOptions.LongRunning);
        Task.Factory.StartNew(delegate
        {
            while (IsInspectFlowStart)
            {
                if (ImageRecordQueue.TryDequeue(out var result))
                {
                    Log("界面图像刷新线程开始！");
                    ShowRecord(result);
                }
                Thread.Sleep(10);
            }
        }, TaskCreationOptions.LongRunning);
    }

    private void CreateRecordInfo(ImageInfo imageInfo, ref ImageRecordInfo imageRecodInfo)
    {
        imageRecodInfo.ImageInfo = imageInfo;
        imageRecodInfo.ImageSaveInfo.SaveFilePath = $"{mInspectionConfig.Station}\\{mInspectionConfig.Inspect}\\";
        imageRecodInfo.ImageSaveInfo.ImageName = $"{mInspectionConfig.Inspect}_{imageInfo.Index}_{DateTime.Now.ToString("HH_mm_ss_ff")}";
        imageRecodInfo.ImageSaveInfo.IsSaveImageLocally = (mInspectionConfig.IsSaveImageLocally && mJobData.mSystemConfigData.SaveRaw);
        imageRecodInfo.ImageSaveInfo.IsUploadImageToRemoteDisk = mInspectionConfig.IsUploadImageToRemoteDisk;
        imageRecodInfo.ImageSaveInfo.IsUploadResImageToRemoteDisk = mInspectionConfig.IsUploadResImageToRemoteDisk;
        imageRecodInfo.ImageSaveInfo.ImageType = mJobData.mSystemConfigData.ImageType;
        imageRecodInfo.ImageSaveInfo.ImageTypeRemote = mJobData.mSystemConfigData.ImageTypeRemote;
        imageRecodInfo.ImageSaveInfo.ImageTypeTool = mJobData.mSystemConfigData.ImageTypeTool;
        imageRecodInfo.ImageSaveInfo.ImageTypeToolRemote = mJobData.mSystemConfigData.ImageTypeToolRemote;

        if (CameraType == "3D")
        {
            imageRecodInfo.ImageSaveInfo.ImageType = ImageType.cdb;
            imageRecodInfo.ImageSaveInfo.ImageTypeRemote = ImageType.cdb;
        }
    }
    private void ShowRecord(ImageRecordInfo imageRecodInfo)
    {
        try
        {
            if (imageRecodInfo.CogRecord != null && mDisplay != null)
            {
                mDisplay.Record = imageRecodInfo.CogRecord;
            }
            else if (imageRecodInfo.CogRecord == null && mDisplay != null)
            {
                mDisplay.CogImage = imageRecodInfo.ImageInfo.CogImage;
            }
            if (mDisplay != null && (imageRecodInfo.CogRecord != null || imageRecodInfo.ImageInfo.CogImage != null))
            {
                imageRecodInfo.ToolImage = mDisplay.RecordDisplay.CreateContentBitmap(CogDisplayContentBitmapConstants.Image);
            }

            Task.Run(() =>
            {
                SaveImage(imageRecodInfo);
            });
        }
        catch (Exception ex)
        {
            LogError("界面显示或存图出错:" + ex.Message);
        }
    }

    private void SaveImage(ImageRecordInfo imageRecodInfo)
    {
        string rootPath = $"{mJobData.mSystemConfigData.PicPath}{DateTime.Now.ToString("yyyy-MM-dd")}\\";
        bool isOKorNG = imageRecodInfo.ImageSaveInfo.IsOKorNG;
        string savepath = imageRecodInfo.ImageSaveInfo.SaveFilePath;
        string imageName = imageRecodInfo.ImageSaveInfo.ImageName;
        string ResultInfo = "";
        ImageType imageType = imageRecodInfo.ImageSaveInfo.ImageType;
        ImageType imageTypeTool = imageRecodInfo.ImageSaveInfo.ImageTypeTool;

        if (mJobData.mSystemConfigData.SaveOKNGGlobal)
        {
            ResultInfo = isOKorNG ? "OK" : "NG";
        }

        if (imageRecodInfo.ImageSaveInfo.IsSaveImageLocally)
        {
            var imageInfo_Raw = new RawImageInfo();
            imageInfo_Raw.SaveFilePath = rootPath +  $"{JobCollection.Instance.GetCurrentExplain()}\\Raw\\" + savepath + "\\" + ResultInfo + SaveCode + "\\";
            imageInfo_Raw.ImageName = imageName;
            imageInfo_Raw.mImageType = imageType;
            imageInfo_Raw.ThumbPercent = mJobData.mSystemConfigData.ThumbPercent;
            imageInfo_Raw.Image = imageRecodInfo.ImageInfo.CogImage;

            VisionPro_ImageSave.SaveRawImageAsync(imageInfo_Raw);
            //LogUtil.Log("[工位]" + mInspectionConfig.Inspect + "，" + "图像入队完成(原图)！");
        }

        if (mJobData.mSystemConfigData.SaveDeal)
        {
            var imageInfo_Tool = new ToolImageInfo();
            imageInfo_Tool.SaveFilePath = rootPath + $"{JobCollection.Instance.GetCurrentExplain()}\\Deal\\" + savepath + ResultInfo + SaveCode + "\\";
            imageInfo_Tool.ImageName = imageName;
            imageInfo_Tool.mImageType = imageTypeTool;
            imageInfo_Tool.ThumbPercent = mJobData.mSystemConfigData.ThumbPercentRes;
            imageInfo_Tool.Image = imageRecodInfo.ToolImage;

            VisionPro_ImageSave.SaveToolImageAsync(imageInfo_Tool, null);
            //LogUtil.Log("[工位]" + mInspectionConfig.Inspect + "，" + "图像入队完成（处理图）！");
        }
    }
}