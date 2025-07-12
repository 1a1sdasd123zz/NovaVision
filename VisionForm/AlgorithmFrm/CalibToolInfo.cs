using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Cognex.VisionPro;
using Cognex.VisionPro.CalibFix;
using Cognex.VisionPro.ToolBlock;
using NovaVision.BaseClass;
using NovaVision.BaseClass.VisionConfig;

namespace NovaVision.VisionForm.AlgorithmFrm
{
    public class CalibToolInfo
    {
        public string CameraName { get; set; }
        public bool IsUseCalibCheckborad { get; set; }
        public CogCalibCheckerboardTool mCheckboardTool;
        public CogCalibNPointToNPointTool mNPointToNPointTool;
        public CogToolBlock mCalibToolBlock;

        static string[] ToolNames = new string[] { "棋盘格标定.vpp", "九点标定.vpp", "标定流程.vpp" };

        public CalibToolInfo(string path)
        {
            mCheckboardTool = new CogCalibCheckerboardTool();
            mNPointToNPointTool = new CogCalibNPointToNPointTool();
            mCalibToolBlock = (CogToolBlock)CogSerializer.LoadObjectFromFile(path);
            //mCalibToolBlock.Inputs.Add(new CogToolBlockTerminal("Image", typeof(Cognex.VisionPro.ICogImage)));
            //mCalibToolBlock.Outputs.Add(new CogToolBlockTerminal("CircleList", typeof(List<CogCircle>)));
        }
        public CalibToolInfo(CogCalibCheckerboardTool boardTool, CogCalibNPointToNPointTool npointTool, CogToolBlock tb)
        {
            mCheckboardTool = boardTool;
            mNPointToNPointTool = npointTool;
            mCalibToolBlock = tb;
        }

        public static void LoadTool(string path, string key, out CogCalibCheckerboardTool boardTool, out CogCalibNPointToNPointTool npointTool, out CogToolBlock tb)
        {
            boardTool = new CogCalibCheckerboardTool();
            npointTool = new CogCalibNPointToNPointTool();
            tb = new CogToolBlock();
            try
            {
                string loadFilePath = path + key + "\\" + ToolNames[0];
                boardTool = CogSerializer.LoadObjectFromFile(loadFilePath) as CogCalibCheckerboardTool;
                if (boardTool == null)
                {
                    Console.WriteLine("无法加载指定的CogCalibCheckerboardTool工具");
                }
                loadFilePath = path + key + "\\" + ToolNames[1];
                npointTool = CogSerializer.LoadObjectFromFile(loadFilePath) as CogCalibNPointToNPointTool;
                if (npointTool == null)
                {
                    Console.WriteLine("无法加载指定的CogCalibNPointToNPointTool工具");
                }
                loadFilePath = path + key + "\\" + ToolNames[2];
                tb = CogSerializer.LoadObjectFromFile(loadFilePath) as CogToolBlock;
                if (tb == null)
                {
                    Console.WriteLine("无法加载指定的ToolBlock工具");
                }
            }
            catch (Exception e) { LogUtil.LogError("加载标定工具异常" + e.ToString()); }
        }
        public bool SaveTool(string path)
        {
            try
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                string saveFilePath = path + ToolNames[0];
                CogSerializer.SaveObjectToFile(mCheckboardTool, saveFilePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                saveFilePath = path + ToolNames[1];
                CogSerializer.SaveObjectToFile(mNPointToNPointTool, saveFilePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                saveFilePath = path + ToolNames[2];
                CogSerializer.SaveObjectToFile(mCalibToolBlock, saveFilePath, typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                return true;
            }
            catch (Exception ex)
            {
                LogUtil.LogError("保存标定文件失败!" + ex.ToString());
                return false;
            }
        }
    }
}
