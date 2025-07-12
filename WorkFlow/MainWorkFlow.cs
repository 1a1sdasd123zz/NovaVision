using System;
using System.Collections.Generic;
using System.ComponentModel;
using Cognex.VisionPro;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Helper;
using NovaVision.BaseClass.VisionConfig;
using JobData = NovaVision.BaseClass.VisionConfig.JobData;

namespace NovaVision.WorkFlow;

public class MainWorkFlow
{
    public readonly MyDictionaryEx<TaskFlow> mTasFlows = new ();

    WorkFlowTreeNode mTasFlowTree;
    JobData mJobData;

    public int InitWorkFlow(JobData jobData)
    {
        mJobData = jobData;
        VisionPro_ImageSave.PathDelete = mJobData.mSystemConfigData.PicPath;
        VisionPro_ImageSave.SaveDays = mJobData.mSystemConfigData.SaveDays;
        VisionPro_ImageSave.FlagDelete = mJobData.mSystemConfigData.DeletePic;

        mTasFlowTree = new WorkFlowTreeNode("工位配置",null ,false);
        mTasFlows.Clear();
        for (int i = 0; i < mJobData.mStations.Count; i++)
        {
            WorkFlowTreeNode workstation1Node = new WorkFlowTreeNode("",null,false);
            for (int j = 0; j < mJobData.mStations[i].Count; j++)
            {
                try
                {
                    InspectionConfig inspection = mJobData.mStations[i][j];
                    TaskFlow taskFlow = new TaskFlow(inspection, mJobData);
                    taskFlow.InitTaskFlow();
                    workstation1Node.Name = inspection.Station;
                    WorkFlowTreeNode workflowNode = new WorkFlowTreeNode(inspection.Inspect, taskFlow,true);
                    workstation1Node.Children.Add(workflowNode);
                    mTasFlows.Add(inspection.Inspect,taskFlow);
                }
                catch
                {
                    // ignored
                }
            }
            mTasFlowTree.Children.Add(workstation1Node);
        }

        return 1;
    }


    public void OnLineStateChange(bool state)
    {
        var taskflows = new List<object>();
        PreorderTraversal(mTasFlowTree, ref taskflows);

        foreach (var item in taskflows)
        {
            TaskFlow taskflow = item as TaskFlow;
            if (state)
            {
                taskflow.IsOnLine = true;
                if (taskflow != null) taskflow.StartWorkFlow();
            }
            else
            {
                taskflow.IsOnLine = false;
                if (taskflow != null) taskflow.StopWorkFlow();
            }

            if (taskflow != null) taskflow.IsOnLine = state;
        }
    }

    public void Run(string workstationName, string workflowName, List<ICogImage> Images = null, RunMode runType = RunMode.自动运行流程)
    {
        try
        {
            if (runType == RunMode.自动运行流程)
            {
                FindeNode(mTasFlowTree, workstationName, workflowName, out var o);
                if (o != null)
                {
                    TaskFlow taskflow = o as TaskFlow;
                    taskflow.StartWorkFlow();
                    taskflow.AutoRun();
                }
            }
            else
            {
                FindeNode(mTasFlowTree, workstationName, workflowName, out var o);
                if (o != null)
                {
                    TaskFlow taskflow = o as TaskFlow;
                    taskflow.StartWorkFlow();
                    taskflow.ManualRun(Images);
                }
            }
        }
        catch (Exception e)
        {
            LogUtil.Log($"[{workstationName}][{workflowName}]手动单次执行异常" + e.ToString());
            throw;
        }

    }

    public void ModifyWorkFlow()
    {
        InitWorkFlow(mJobData);
    }

    private void PreorderTraversal(WorkFlowTreeNode node, ref List<object> objects)
    {
        if (node != null)
        {
            if (node.IsWorkFlow)
            {
                objects.Add(node.Data);
            }
            foreach (var child in node.Children)
            {
                PreorderTraversal(child, ref objects);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="node"></param>
    /// <param name="workstationName"></param>工位名
    /// <param name="workflowName"></param>流程名
    /// <param name="o"></param>
    private void FindeNode(WorkFlowTreeNode node,string workstationName,string workflowName,out object o)
    {
        o = null;
        if (node == null)
        {
            return;
        }
        // 先遍历根节点的子节点（工位节点）
        foreach (WorkFlowTreeNode workstationNode in node.Children)
        {
            if (!workstationNode.IsWorkFlow && workstationNode.Name == workstationName)
            {
                // 找到目标工位节点，遍历其流程子节点
                foreach (WorkFlowTreeNode workflowNode in workstationNode.Children)
                {
                    if (workflowNode.IsWorkFlow && workflowNode.Name == workflowName)
                    {
                        o = workflowNode.Data; 
                        break;
                    }
                }
            }
        }
    }
}


public class WorkFlowTreeNode
{
    public string Name { get; set; }
    public object Data { get; set; }
    public bool IsWorkFlow { get; set; }
    public List<WorkFlowTreeNode> Children { get; set; } = new List<WorkFlowTreeNode>();
    public WorkFlowTreeNode(string name,object data, bool isWorkstation)
    {
        Name = name;
        Data = data;
        IsWorkFlow = isWorkstation;
    }
}

public enum RunMode
{
    [Description("自动运行流程")]
    自动运行流程 = 0,
    [Description("手动运行流程")]
    手动运行流程,
}
