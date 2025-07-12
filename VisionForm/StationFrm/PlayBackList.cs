using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.VisionConfig;
using NovaVision.WorkFlow;

namespace NovaVision.VisionForm.StationFrm
{
    public class PlayBackList
    {
        public string Code = "";

        public string RecordPath = "";

        public string SavePicPath = "";

        public bool IsSavePic = false;

        public bool IsSaveResult = false;

        public MyCollectionBase<PlayBack> PlayBackInfo = new MyCollectionBase<PlayBack>();

        public SpotCheckConfig mSpotCheckConfig = new SpotCheckConfig();

        [XmlIgnore]
        public WorkFlow.TaskFlow mTaskFlow;

        [XmlIgnore]
        public JobData mJobData;

        public static PlayBackList Deserialzer(string filePath)
        {
            PlayBackList mPlayBackList;
            if (File.Exists(filePath + "PlayBack.xml"))
            {
                mPlayBackList = (PlayBackList)XmlHelp.ReadXML(filePath + "PlayBack.xml", typeof(PlayBackList));
            }
            else
            {
                mPlayBackList = new PlayBackList();
            }
            return mPlayBackList;
        }

        public void Dispatch(string[] fileNames)
        {
            try
            {
                for (int i = 0; i < this.PlayBackInfo.Count; i++)
                {
                    this.PlayBackInfo[i].ImagePaths.Clear();
                    for (int j = 0; j < fileNames.Length; j++)
                    {
                        bool flag = fileNames[j].Contains(this.PlayBackInfo[i].StationName) && fileNames[j].Contains(this.PlayBackInfo[i].InspectName);
                        if (flag)
                        {
                            this.PlayBackInfo[i].ImagePaths.Add(fileNames[j]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("图片文件路径读取错误：" + ex.Message);
            }
        }

        public void ExcuteProucedureOnce(string resultImagePath, bool isSaveImage, bool isSaveResultData)
        {
            try
            {
                if (mTaskFlow.IsStationFullConfig)
                {
                    MessageBox.Show("流程启动失败，工站配置不完整！");
                }
                else
                {
                    for (int i = 0; i < this.PlayBackInfo.Count; i++)
                    {
                        string station = this.PlayBackInfo[i].StationName;
                        string inspect = this.PlayBackInfo[i].InspectName;
                        int stationIndex = -1;
                        int inspectIndex = -1;
                        int num = -1;
                        bool IsExsit = this.FindStaionInspectIndex(station, inspect, out stationIndex, out inspectIndex, out num);
                        bool flag2 = IsExsit;
                        if (flag2)
                        {
                            string stationName = this.mJobData.mStations[stationIndex][inspectIndex].Station;
                            string inspectionName = this.mJobData.mStations[stationIndex][inspectIndex].Inspect;
                            string commTable = this.mJobData.mStations[stationIndex][inspectIndex].CommunicationTable;
                            string triggerPoint = this.mJobData.mStations[stationIndex][inspectIndex].TriggerPoint;
                            string codePoint = this.mJobData.mStations[stationIndex][inspectIndex].CodePoint;
                            string triggerNum = this.mJobData.mStations[stationIndex][inspectIndex].TriggerNum;
                            string inspectType = this.mJobData.mStations[stationIndex][inspectIndex].InspectType.ToString();
                            string key = string.Concat(new string[]
                            {
                                commTable,
                                ",",
                                triggerPoint,
                                ",",
                                triggerNum,
                                ",",
                                inspectType
                            });
                            string keyCode = stationName + "," + inspectionName;
                            Type t = MyTypeConvert.GetType(this.mJobData.mCommData.Dic[commTable].Outputs[triggerPoint].Type);
                            //this.mTaskFlow.TriggerPoint_Cache[key] = Convert.ChangeType(triggerNum, t);
                            //this.mTaskFlow.CodePoint_Cache[keyCode] = this.Code;
                            //if (isSaveImage)
                            //{
                            //    this.mTaskFlow.ManualPicPath = resultImagePath;
                            //}
                            //else
                            //{
                            //    this.mTaskFlow.ManualPicPath = "";
                            //}
                            bool flag3 = this.PlayBackInfo[i].ExcuteMode == "图片回放" && this.mJobData.mStations[stationIndex][inspectIndex].InspectType == InspectType.Normal && !this.PlayBackInfo[i].IsIgnore;
                            if (flag3)
                            {
                                for (int j = 0; j < this.PlayBackInfo[i].ImagePaths.Count; j++)
                                {
                                    bool flag4 = !string.IsNullOrEmpty(this.PlayBackInfo[i].ImagePaths[j]);
                                    if (flag4)
                                    {
                                        ICogImage image;
                                        bool flag5 = this.ReadPic(this.PlayBackInfo[i].ImagePaths[j], out image);
                                        if (flag5)
                                        {
                                            ImageInfo imageInfo = default(ImageInfo);
                                            imageInfo.CogImage = image;
                                            imageInfo.Index = j + 1;
                                            //imageInfo.StationID = stationIndex;
                                            //imageInfo.InspectID = inspectIndex;
                                            //imageInfo.Num = num;
                                            this.mTaskFlow.ImageQueue.Enqueue(imageInfo);
                                            LogUtil.Log(string.Format("回放工位[{0}],检测[{1}],图片 Index={2} 进入队列", station, inspect, imageInfo.Index));
                                        }
                                        else
                                        {
                                            LogUtil.LogError(string.Concat(new string[]
                                            {
                                                "回放工位[",
                                                station,
                                                "],检测[",
                                                inspect,
                                                "],路径 ",
                                                this.PlayBackInfo[i].ImagePaths[j],
                                                " 不存在"
                                            }));
                                        }
                                    }
                                    else
                                    {
                                        LogUtil.LogError(string.Concat(new string[]
                                        {
                                            "回放工位[",
                                            station,
                                            "],检测[",
                                            inspect,
                                            "],路径为空"
                                        }));
                                    }
                                }
                                bool flag6 = this.PlayBackInfo[i].ImagePaths.Count == 0;
                                if (flag6)
                                {
                                    LogUtil.LogError(string.Concat(new string[]
                                    {
                                        "回放工位[",
                                        station,
                                        "],检测[",
                                        inspect,
                                        "],未分配读图路径"
                                    }));
                                }
                            }
                            else
                            {
                                bool flag7 = this.PlayBackInfo[i].ExcuteMode == "图片回放" && (this.mJobData.mStations[stationIndex][inspectIndex].InspectType == InspectType.Summary || this.mJobData.mStations[stationIndex][inspectIndex].InspectType == InspectType.StageSummary) && !this.PlayBackInfo[i].IsIgnore;
                                if (flag7)
                                {
                                    ImageInfo imageInfo2 = default(ImageInfo);
                                    imageInfo2.CogImage = null;
                                    imageInfo2.Index = 1;
                                    //imageInfo2.StationID = stationIndex;
                                    //imageInfo2.InspectID = inspectIndex;
                                    //imageInfo2.Num = num;
                                    this.mTaskFlow.ImageQueue.Enqueue(imageInfo2);
                                    LogUtil.Log(string.Format("回放工位[{0}],检测[{1}],图片 Index={2} 进入队列", station, inspect, imageInfo2.Index));
                                }
                                else
                                {
                                    bool flag8 = this.PlayBackInfo[i].ExcuteMode == "单次执行" && !this.PlayBackInfo[i].IsIgnore;
                                    if (flag8)
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.LogError("单次回放报错：" + ex.Message);
            }
        }

        public bool FindStaionInspectIndex(string stationName, string inspectName, out int staionIndex, out int inspectIndex, out int num)
        {
            staionIndex = -1;
            inspectIndex = -1;
            num = -1;
            for (int i = 0; i < this.mJobData.mStations.Count; i++)
            {
                for (int j = 0; j < this.mJobData.mStations[i].Count; j++)
                {
                    num++;
                    bool flag = this.mJobData.mStations[i][j].Station == stationName && this.mJobData.mStations[i][j].Inspect == inspectName;
                    if (flag)
                    {
                        staionIndex = i;
                        inspectIndex = j;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ReadPic(string fileName, out ICogImage image)
        {
            bool result;
            if (File.Exists(fileName))
            {
                CogImageFileTool imageFileTool = new CogImageFileTool();
                imageFileTool.Operator.Open(fileName, CogImageFileModeConstants.Read);
                imageFileTool.Run();
                image = imageFileTool.OutputImage;
                result = true;
            }
            else
            {
                image = null;
                result = false;
            }
            return result;
        }
    }
}
