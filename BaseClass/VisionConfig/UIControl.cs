using System;
using System.Collections.Generic;
using System.IO;
using NovaVision.BaseClass.Collection;
using NovaVision.UserControlLibrary;

namespace NovaVision.BaseClass.VisionConfig
{
    public class UIControl
    {
        public MyDictionaryEx<ImageDisplay> mImageDisplays;

        public AppConfig mAppConfig;

        private string Path;

        public UIControl()
        {
            mAppConfig = new AppConfig();
            mImageDisplays = new MyDictionaryEx<ImageDisplay>();
            RegisterEvent();
        }

        public UIControl(string path)
        {
            Path = path;
            if (File.Exists(path))
            {
                try
                {
                    mAppConfig = (AppConfig)XmlHelp.ReadXML(path, typeof(AppConfig));
                }
                catch (Exception ex)
                {
                    LogUtil.LogError("解析本地" + path + "文件失败，异常信息：" + ex.Message);
                }
                mImageDisplays = new MyDictionaryEx<ImageDisplay>();
                InitDisplay();
            }
            else
            {
                mAppConfig = new AppConfig();
                mImageDisplays = new MyDictionaryEx<ImageDisplay>();
            }
            RegisterEvent();
        }

        public void InitDisplay()
        {
            mImageDisplays.Clear();
            string[] array = new string[1] { "图_1" };
            if (mAppConfig.DS_2D_Names != null)
            {
                array = mAppConfig.DS_2D_Names.Split('|');
            }
            for (int j = 0; j < mAppConfig.DS_2D_Column * mAppConfig.DS_2D_Row; j++)
            {
                ImageDisplay imageDisplay = new ImageDisplay();
                imageDisplay.Name = "2D_" + (j + 1);
                if (array.Length == mAppConfig.DS_2D_Column * mAppConfig.DS_2D_Row)
                {
                    imageDisplay.DisplayName = array[j];
                }
                else
                {
                    imageDisplay.DisplayName = "图_" + (j + 1);
                }
                mImageDisplays.Add(imageDisplay.Name, imageDisplay);
            }
            array = new string[1] { "图_1" };
            if (mAppConfig.DS_3D_Names != null)
            {
                array = mAppConfig.DS_3D_Names.Split('|');
            }
            for (int i = 0; i < mAppConfig.DS_3D_Column * mAppConfig.DS_3D_Row; i++)
            {
                ImageDisplay imageDisplay2 = new ImageDisplay();
                imageDisplay2.Name = "3D_" + (i + 1);
                if (array.Length == mAppConfig.DS_3D_Column * mAppConfig.DS_3D_Row)
                {
                    imageDisplay2.DisplayName = array[i];
                }
                else
                {
                    imageDisplay2.DisplayName = "图_" + (i + 1);
                }
                mImageDisplays.Add(imageDisplay2.Name, imageDisplay2);
            }
        }

        public bool SaveAppConfig()
        {
            return XmlHelp.WriteXML(mAppConfig, Path, typeof(AppConfig));
        }

        private void RegisterEvent()
        {
            mAppConfig.Changed += AppConfig_Changed;
        }

        private void AppConfig_Changed(object sender, ChangeEventArg e)
        {
            List<string> oldValue = mImageDisplays.GetKeys();
            InitDisplay();
        }

        public void ResumeLayout()
        {
            mAppConfig.ResumeLayout();
            SaveAppConfig();
        }
    }
}
