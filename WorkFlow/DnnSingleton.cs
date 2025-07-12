using DLInferenceLib.HalconOperate;
using System;
using NovaVision.BaseClass;

namespace NovaVision.WorkFlow;

public sealed class DnnSingleton
{
    public readonly DnnInferenceInfo FlyFrontDnnInference1 = new();
    public readonly DnnInferenceInfo FlyFrontDnnInference2 = new();


    // 静态字段直接初始化（在类加载时触发）

    // 静态属性提供全局访问点
    public static DnnSingleton Instance { get; } = new();

    private DnnSingleton()
    {
        Init();
    }

    private void Init()
    {
        FlyFrontDnnInference1.Initial(AppDomain.CurrentDomain.BaseDirectory + "Project\\Model\\飞拍前\\");
        FlyFrontDnnInference2.Initial(AppDomain.CurrentDomain.BaseDirectory + "Project\\Model\\飞拍前\\");
        if (!FlyFrontDnnInference1.IsInitialSuccese || !FlyFrontDnnInference2.IsInitialSuccese)
        {
            LogUtil.LogError("加载深度学习模型失败！");
        }
    }
}