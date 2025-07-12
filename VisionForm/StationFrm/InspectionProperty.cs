using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NovaVision.BaseClass;
using NovaVision.BaseClass.EnumHelper;
using NovaVision.BaseClass.Module;
using NovaVision.BaseClass.VisionConfig;

namespace NovaVision.VisionForm.StationFrm
{
    public class InspectionProperty
    {
        public JobData mJobData;

        public PropertyGridProperty propertyGridProperty;

        private PropertyInfo[] propertyInfos = typeof(InspectionConfig).GetProperties();

        public InspectionProperty(InspectionConfig inspectionConfig, JobData jobData)
        {
            mJobData = jobData;
            InitPropertyGrid(inspectionConfig);
        }

        public void InitPropertyGrid(InspectionConfig inspectionConfig)
        {
            if (propertyGridProperty == null)
            {
                propertyGridProperty = new PropertyGridProperty();
            }
            else
            {
                propertyGridProperty.Clear();
            }
            Property Station = new Property("工位配置", "工位序号", propertyInfos[0].Name, "工位号", inspectionConfig.Station, sReadonly: true, sVisible: true);
            propertyGridProperty.Add(Station);
            Property Inspect = new Property("工位配置", "检测内容", propertyInfos[1].Name, "检测点检测内容", inspectionConfig.Inspect, sReadonly: true, sVisible: true);
            propertyGridProperty.Add(Inspect);
            Property CameraName = new Property("取像配置", "配置名称", propertyInfos[2].Name, "相机自定义别名，用于分定义相机多套参数", inspectionConfig.CameraName, sReadonly: false, sVisible: true);
            CameraName.Converter = new DropDownListConverter(mJobData.CamConfigNames);
            propertyGridProperty.Add(CameraName);
            Property CameraSerialNum = new Property("取像配置", "相机编号", propertyInfos[3].Name, "相机序列号", inspectionConfig.CameraSerialNum, sReadonly: true, sVisible: true);
            propertyGridProperty.Add(CameraSerialNum);
            Property CameraType = new Property("取像配置", "相机类型", propertyInfos[4].Name, "相机类型", inspectionConfig.CameraType, sReadonly: true, sVisible: true);
            propertyGridProperty.Add(CameraType);
            Property CommCodePoint = new Property("取像配置", "条码接收点位", propertyInfos[5].Name, "条码接收点位", inspectionConfig.CodePoint, sReadonly: false, sVisible: true);
            if (inspectionConfig.CommunicationTable != "")
            {
                CommCodePoint.Converter = new DropDownListConverter(mJobData.mCommData.Dic[inspectionConfig.CommunicationTable].Outputs.GetKeys());
            }
            propertyGridProperty.Add(CommCodePoint);
            Property CommTriggerPoint = new Property("取像配置", "相机触发点位", propertyInfos[6].Name, "相机触发点位", inspectionConfig.TriggerPoint, sReadonly: false, sVisible: true);
            if (inspectionConfig.CommunicationTable != "")
            {
                CommTriggerPoint.Converter = new DropDownListConverter(mJobData.mCommData.Dic[inspectionConfig.CommunicationTable].Outputs.GetKeys());
            }
            propertyGridProperty.Add(CommTriggerPoint);
            Property CommTriggerNum = new Property("取像配置", "位置号", propertyInfos[7].Name, "触发相机拍照标志号", inspectionConfig.TriggerNum, sReadonly: false, sVisible: true);
            if (inspectionConfig.CommunicationTable != "" && inspectionConfig.TriggerPoint != "")
            {
                CommTriggerNum.Converter = new DropDownListConverter(mJobData.mCommData.Dic[inspectionConfig.CommunicationTable].Outputs[inspectionConfig.TriggerPoint].SettingValuesToString());
            }
            propertyGridProperty.Add(CommTriggerNum);
            Property IsShowResult = new Property("取像配置", "显示结果存储数据", propertyInfos[8].Name, "是否将触发点位数字作为显示和存储到数据库点位（一般所有检测只设置最后完成的检测为true）", inspectionConfig.IsShowResult, sReadonly: false, sVisible: true);
            propertyGridProperty.Add(IsShowResult);
            Property IsSaveImageLocally = new Property("取像配置", "是否保存图片到本地", propertyInfos[9].Name, "当前流程是否需要保存图片到本地（True-保存，False-不保存），方便选择性保存图片！", inspectionConfig.IsSaveImageLocally, sReadonly: false, sVisible: true);
            propertyGridProperty.Add(IsSaveImageLocally);
            Property IsUploadImageToRemoteDisk = new Property("取像配置", "是否将图片上传到公共网盘", propertyInfos[10].Name, "当前流程是否需要将图片上传到公共网盘（True-上传，False-不上传），方便选择性上传图片！", inspectionConfig.IsUploadImageToRemoteDisk, sReadonly: false, sVisible: true);
            propertyGridProperty.Add(IsUploadImageToRemoteDisk);
            Property IsUploadResImageToRemoteDisk = new Property("取像配置", "是否将结果图上传到公共网盘", propertyInfos[11].Name, "当前流程是否需要将结果图上传到公共网盘（True-上传，False-不上传），方便选择性上传图片！", inspectionConfig.IsUploadResImageToRemoteDisk, sReadonly: false, sVisible: true);
            propertyGridProperty.Add(IsUploadResImageToRemoteDisk);
            Property ExternalTriggerTimes = new Property("取像配置", "外触发次数", propertyInfos[12].Name, "2D相机为外触发时设置次数，软触发次数设为0", inspectionConfig.ExternalTriggerTimes, sReadonly: false, sVisible: true);
            propertyGridProperty.Add(ExternalTriggerTimes);
            Property CommTable = new Property("通讯配置", "主通讯表名", propertyInfos[13].Name, "通讯表名选择", inspectionConfig.CommunicationTable, sReadonly: false, sVisible: true);
            CommTable.Converter = new DropDownListConverter(mJobData.mCommTableList);
            propertyGridProperty.Add(CommTable);
            Property CommSerial = new Property("通讯配置", "主序列号", propertyInfos[14].Name, "序列号", inspectionConfig.CommSerialNum, sReadonly: false, sVisible: true);
            CommSerial.Converter = new DropDownListConverter(mJobData.mCommList);
            propertyGridProperty.Add(CommSerial);
            Property CommTable_A = new Property("通讯配置", "副通讯表名", propertyInfos[15].Name, "通讯表名选择", inspectionConfig.CommunicationTable_A, sReadonly: false, sVisible: true);
            CommTable_A.Converter = new DropDownListConverter(mJobData.mCommTableList);
            propertyGridProperty.Add(CommTable_A);
            Property CommSerial_A = new Property("通讯配置", "副序列号", propertyInfos[16].Name, "序列号", inspectionConfig.CommSerialNum_A, sReadonly: false, sVisible: true);
            CommSerial_A.Converter = new DropDownListConverter(mJobData.mCommList);
            propertyGridProperty.Add(CommSerial_A);
            Property IsIgnoreComm_A = new Property("通讯配置", "是否屏蔽副通讯", propertyInfos[17].Name, "是否屏蔽副通讯", inspectionConfig.IsIgnoreComm_A, sReadonly: false, sVisible: true);
            propertyGridProperty.Add(IsIgnoreComm_A);
            Property InspectType = new Property("算法配置", "算法类型", propertyInfos[18].Name, "算法Normal正常检测,StageSummary阶段性汇总检测，Summary所有汇总检测", inspectionConfig.InspectType, sReadonly: false, sVisible: true);
            propertyGridProperty.Add(InspectType);
            Property Algorithm = new Property("算法配置", "算法编号", propertyInfos[19].Name, "算法编号", inspectionConfig.Algorithm, sReadonly: false, sVisible: true);
            Algorithm.Converter = new DropDownListConverter(Enum.GetNames(typeof(EnumStation)).ToList());
            propertyGridProperty.Add(Algorithm);
            Property ImageDisplay = new Property("显示配置", "图像窗口", propertyInfos[20].Name, "图像显示窗口号", inspectionConfig.ImageDisplay, sReadonly: false, sVisible: true);
            ImageDisplay.Converter = new DropDownListConverter(mJobData.mDisplayList);
            propertyGridProperty.Add(ImageDisplay);
            Property ImageDisplayIndex = new Property("显示配置", "图像显示图层序号", propertyInfos[21].Name, "图像窗口显示的图层序号,默认为0", inspectionConfig.ImageDisplayIndex, sReadonly: false, sVisible: true);
            propertyGridProperty.Add(ImageDisplayIndex);
            Property IsIgnoreInspect = new Property("检测状态", "是否屏蔽", propertyInfos[22].Name, "是否屏蔽当前检测", inspectionConfig.IsIgnore, sReadonly: false, sVisible: true);
            propertyGridProperty.Add(IsIgnoreInspect);
        }

        public void SetProperty(InspectionConfig inspectionConfig)
        {
            for (int i = 0; i < propertyGridProperty.Count; i++)
            {
                propertyGridProperty[i].Value = propertyInfos[i].GetValue(inspectionConfig);
            }
            InitPropertyGrid(inspectionConfig);
        }

        public void SetInspectConfig(ref InspectionConfig inspectionConfig)
        {
            for (int i = 0; i < propertyGridProperty.Count; i++)
            {
                if (propertyInfos[i].GetValue(inspectionConfig) == propertyGridProperty[i].Value)
                {
                    continue;
                }
                if (propertyGridProperty[i].Name == "Algorithm")
                {
                    ReomveBinding(inspectionConfig, DataSource.Algorithm, inspectionConfig.Algorithm);
                }
                if (propertyGridProperty[i].Name == "CommunicationTable")
                {
                    ReomveBinding(inspectionConfig, DataSource.Communication, inspectionConfig.CommunicationTable);
                }
                if (propertyGridProperty[i].Name == "CommunicationTable_A")
                {
                    ReomveBinding(inspectionConfig, DataSource.Communication, inspectionConfig.CommunicationTable_A);
                }
                if (i != 3 && i != 4)
                {
                    propertyInfos[i].SetValue(inspectionConfig, propertyGridProperty[i].Value);
                }
                if (propertyGridProperty[i].Name == "CameraName")
                {
                    if (mJobData.mCameraData.ContainsKey(inspectionConfig.CameraName) && mJobData.mCameraData[inspectionConfig.CameraName].CamSN != null)
                    {
                        inspectionConfig.CameraSerialNum = mJobData.mCameraData[inspectionConfig.CameraName].CamSN;
                    }
                    else if (mJobData.mCameraData_CL.ContainsKey(inspectionConfig.CameraName) && mJobData.mCameraData_CL[inspectionConfig.CameraName]["Serial"].Value.ToString() != null)
                    {
                        inspectionConfig.CameraSerialNum = mJobData.mCameraData_CL[inspectionConfig.CameraName]["Serial"].Value.ToString();
                    }
                    else
                    {
                        inspectionConfig.CameraSerialNum = "";
                    }
                    if (mJobData.mCameraData.ContainsKey(inspectionConfig.CameraName) && mJobData.mCameraData[inspectionConfig.CameraName].CamCategory != null)
                    {
                        inspectionConfig.CameraType = mJobData.mCameraData[inspectionConfig.CameraName].CamCategory;
                    }
                    else if (mJobData.mCameraData_CL.ContainsKey(inspectionConfig.CameraName) && mJobData.mCameraData_CL[inspectionConfig.CameraName]["Category"].Value.ToString() != null)
                    {
                        inspectionConfig.CameraType = mJobData.mCameraData_CL[inspectionConfig.CameraName]["Category"].Value.ToString();
                    }
                    else
                    {
                        inspectionConfig.CameraType = "";
                    }
                }
                if (propertyGridProperty[i].Name == "CommunicationTable")
                {
                    inspectionConfig.TriggerPoint = "";
                    inspectionConfig.TriggerNum = "";
                }
            }
        }

        private void ReomveBinding(InspectionConfig inspectionConfig, DataSource source, string value)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < inspectionConfig.DataBindings.Count; i++)
            {
                if (inspectionConfig.DataBindings[i].D_Source == source || inspectionConfig.DataBindings[i].S_Source == source)
                {
                    list.Add(i);
                }
            }
            int count = 0;
            for (int j = 0; j < list.Count; j++)
            {
                string[] array = inspectionConfig.DataBindings[list[j] - count].DestinationPath.Split('.');
                string[] array2 = inspectionConfig.DataBindings[list[j] - count].SourcePath.Split('.');
                if (array[0] == value || array2[0] == value)
                {
                    inspectionConfig.DataBindings.RemoveAt(list[j] - count);
                    count++;
                }
            }
        }
    }
}
