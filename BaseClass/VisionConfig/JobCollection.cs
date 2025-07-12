using System;
using System.IO;
using System.Windows.Forms;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Helper;

namespace NovaVision.BaseClass.VisionConfig
{

    public delegate void JobChanged(int id, string name);

    public class JobCollection
    {
        // 单例实例
        private static readonly Lazy<JobCollection> _instance =
            new Lazy<JobCollection>(() => new JobCollection());
        public static JobCollection Instance => _instance.Value;

        public MyDictionaryEx<JobData> Jobs = new MyDictionaryEx<JobData>();
        public JobInfoCollection JobInfoColl = new JobInfoCollection();

        public string JobInfoPath;
        public string ProjectPath = AppDomain.CurrentDomain.BaseDirectory + "Project\\";

        public int CurrentID
        {
            get
            {
                return JobInfoColl.CurrentID;
            }
            set
            {
                if (value >= 0)
                {
                    JobInfoColl.CurrentID = value;
                }
            }
        }

        public string CurrentName
        {
            get
            {
                return JobInfoColl.CurrentName;
            }
            set
            {
                JobInfoColl.CurrentName = value;
            }
        }

        public int Count => JobInfoColl.JobInfos.Count;

        public int LoadCount => Jobs.Count;

        public event JobChanged JobChangingEvent;

        public event JobChanged JobChangedEvent;

        // 添加获取Explain的方法
        public string GetCurrentExplain()
        {
            return JobInfoColl.JobInfos[CurrentName].Explain;
        }


        public JobCollection()
        {
            JobInfoPath = ProjectPath + "JobInfos.xml\\";
            InitJobInfos();
            AnalysisJobInfos();
            if (JobInfoColl.CurrentName != null)
            {
                CurrentName = JobInfoColl.CurrentName;
            }

            if (JobInfoColl.CurrentID != -1)
            {
                CurrentID = JobInfoColl.CurrentID;
            }
            else
            {
                CurrentID = -1;
                LogUtil.LogError("加载型号失败！");
            }
        }

        private void InitJobInfos()
        {
            if (Directory.Exists(ProjectPath))
            {
                if (File.Exists(ProjectPath + "JobInfos.xml"))
                {
                    try
                    {
                        JobInfoColl = (JobInfoCollection)XmlHelper.ReadXML(ProjectPath + "JobInfos.xml", typeof(JobInfoCollection));
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogError("解析本地" + ProjectPath + "JobInfos.xml文件失败,异常信息：" + ex.Message);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(ProjectPath);
            }
        }

        public bool SaveJobInfos()
        {
            return XmlHelper.WriteXML(JobInfoColl, ProjectPath + "JobInfos.xml", typeof(JobInfoCollection));
        }

        public void AnalysisJobInfos()
        {
            int index = JobInfoColl.GetIDIndex(CurrentID);
            if (index >= 0)
            {
                JobInfoColl.JobInfos[index].IsLoaded = true;
                if (!Jobs.ContainsKey(JobInfoColl.JobInfos[index].Name))
                {
                    SystemConfigData systemConfigData = new SystemConfigData(JobInfoColl.JobInfos[index].ID);
                    LoadSystemConfigData(ref systemConfigData);
                    JobData job = new JobData(JobInfoColl.JobInfos[index].ID, JobInfoColl.JobInfos[index].Name, systemConfigData);
                    Jobs.Add(JobInfoColl.JobInfos[index].Name, job);
                    Console.WriteLine($"添加型号：{JobInfoColl.JobInfos[index].ID}！");
                    LogUtil.Log($"添加型号：{JobInfoColl.JobInfos[index].ID}！");
                }
            }
            else
            {
                LogUtil.Log($"当前型号：{JobInfoColl.CurrentID}不存在！");
            }
        }

        public bool ChangeJob(int id, ProgressBar progressBar)
        {
            if (id == JobInfoColl.CurrentID)
            {
                LogUtil.Log($"切换型号{id}，与现有型号相同，直接返回！");
                return true;
            }
            if (JobInfoColl.JobInfos.Count > 0)
            {
                int index = JobInfoColl.GetIDIndex(id);
                if (index >= 0)
                {
                    // 释放之前的型号
                    if (!string.IsNullOrEmpty(CurrentName))
                    {
                        Jobs[CurrentName].UnRegisterEvents();
                        Jobs.Remove(CurrentName);
                    }

                    CurrentID = id;
                    CurrentName = JobInfoColl.JobInfos[index].Name;
                    if (this.JobChangingEvent != null)
                    {
                        this.JobChangingEvent(CurrentID, CurrentName);
                        LogUtil.Log($"正在切换型号：{id}！");
                    }
                    JobInfoColl.JobInfos[index].IsLoaded = true;

                    if (!Jobs.ContainsKey(JobInfoColl.JobInfos[index].Name))
                    {
                        SystemConfigData systemConfigData = new SystemConfigData(JobInfoColl.JobInfos[index].ID);
                        LoadSystemConfigData(ref systemConfigData);
                        JobData job = new JobData(JobInfoColl.JobInfos[index].ID, JobInfoColl.JobInfos[index].Name, systemConfigData);
                        Jobs.Add(JobInfoColl.JobInfos[index].Name, job);
                        LogUtil.Log($"切换型号不在已加载型号中，添加型号：{id}！");
                    }

                    // 模拟加载进度
                    progressBar.Minimum = 0;
                    progressBar.Maximum = 100;
                    progressBar.Value = 0;

                    // 加载硬件
                    progressBar.Value = 30;
                    Jobs[CurrentName].InitHardWare();
                    progressBar.Value = 60;

                    // 注册事件
                    Jobs[CurrentName].RegisterEvents();
                    progressBar.Value = 90;

                    SaveJobInfos();
                    progressBar.Value = 100;

                    if (this.JobChangedEvent != null)
                    {
                        this.JobChangedEvent(JobInfoColl.CurrentID, JobInfoColl.CurrentName);
                        LogUtil.Log($"型号：{id}切换完成！");
                    }
                    return true;
                }
                LogUtil.Log($"当前型号：{id}不存在！");
                return false;
            }
            return false;
        }


        public void LoadSystemConfigData(ref SystemConfigData systemConfigData)
        {
            if (Directory.Exists(systemConfigData.JobPath))
            {
                if (File.Exists(systemConfigData.JobPath + systemConfigData.Name))
                {
                    try
                    {
                        SystemConfigData temp = (SystemConfigData)XmlHelp.ReadXML(systemConfigData.JobPath + systemConfigData.Name, typeof(SystemConfigData));
                        temp.CompareSetValue(systemConfigData);
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogError("解析本地" + systemConfigData.JobPath + systemConfigData.Name + "文件失败，异常信息：" + ex.Message);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(systemConfigData.JobPath);
            }
        }
    }
}
