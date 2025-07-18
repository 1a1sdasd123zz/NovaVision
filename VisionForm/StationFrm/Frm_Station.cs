using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Communication;
using NovaVision.BaseClass.EnumHelper;
using NovaVision.BaseClass.Helper;
using NovaVision.BaseClass.VisionConfig;
using NovaVision.VisionForm.CarameFrm;
using NovaVision.VisionForm.CommunicationFrm;
using NovaVision.WorkFlow;
using JobData = NovaVision.BaseClass.VisionConfig.JobData;
using CogImage8Grey = Cognex.VisionPro.CogImage8Grey;
using ICogImage = Cognex.VisionPro.ICogImage;

namespace NovaVision.VisionForm.StationFrm
{
    public delegate void delNotifyMesState(object sender, bool state);
    public partial class Frm_Station : Form
    {
        //public event delNotifyMesState NotifyMesState;

        private MainWorkFlow mMainWorkFlow;


        private JobData mJobData;

        private InspectionProperty mInspectionProperty;

        private InspectionConfig mInspectionConfig = new InspectionConfig();

        private StationConfig mStationConfig = new StationConfig();

        private StationCollection mStationCollection;

        private Frm_ProcedurePlayBack frm_ProcedurePlayBack;

        private string camConfigKey = "";

        private string mSelectStation = "";

        public Frm_Station(JobData jobData, WorkFlow.MainWorkFlow taskFlow)
        {
            InitializeComponent();
            AddCtrlToGroup();
            mJobData = jobData;
            mMainWorkFlow = taskFlow;
            mStationCollection = mJobData.mStations;
            Init_StationParam();
            InitProducParam();
            RegisterEvent();
            Init_JobChangeInfo();
        }

        private void AddCtrlToGroup()
        {
            Label label = new Label();
            label.Name = "label";
            label.AutoSize = true;
            label.Font = new Font("宋体", 9f, FontStyle.Regular, GraphicsUnit.Point, 134);
            label.BringToFront();
            label.Location = new Point(propertyGrid.Bounds.X + 180, propertyGrid.Bounds.Y + 10);
            label.Text = "";
            tabControl1.TabPages[0].Controls.Add(label);
        }

        private void Init_StationParam()
        {
            if (mStationCollection.Count > 0)
            {
                mInspectionProperty = new InspectionProperty(mInspectionConfig, mJobData);
                treeView_Station.Nodes.Add("工位配置");
                ObjToTreeView.TransObjToTreeView(mStationCollection, treeView_Station.Nodes[0], typeof(InspectionConfig));
                return;
            }
            mInspectionProperty = new InspectionProperty(mInspectionConfig, mJobData);
            propertyGrid.SelectedObject = mInspectionProperty.propertyGridProperty;
            treeView_Station.Nodes.Add("工位配置");
            mStationConfig.Add("检测1", mInspectionConfig);
            mStationCollection.Add("工位1", mStationConfig);
            ObjToTreeView.TransObjToTreeView(mStationCollection, treeView_Station.Nodes[0], typeof(InspectionConfig));

        }

        private void Init_JobChangeInfo()
        {
            ComboBox.ObjectCollection items = cmb_JobChangeCommTable.Items;
            object[] items2 = mJobData.mCommTableList.ToArray();
            items.AddRange(items2);
            ComboBox.ObjectCollection items3 = cmb_JobChangeCommSerial.Items;
            items2 = mJobData.mCommList.ToArray();
            items3.AddRange(items2);
            if (mJobData.mCommTableList.Contains(mJobData.mJobChangeSignal.CommunicationTable))
            {
                cmb_JobChangeCommTable.SelectedItem = mJobData.mJobChangeSignal.CommunicationTable;
            }
            if (mJobData.mCommList.Contains(mJobData.mJobChangeSignal.CommSerialNum))
            {
                cmb_JobChangeCommSerial.SelectedItem = mJobData.mJobChangeSignal.CommSerialNum;
            }
        }

        private void InitProducParam()
        {
            numRow.Value = mJobData.mProductInfo.Row;
            numCol.Value = mJobData.mProductInfo.Col;
            numFlyNum.Value = mJobData.mProductInfo.FlyNum;
            numFly1Row.Value = mJobData.mProductInfo.Fly1Row;
            numFly2Row.Value = mJobData.mProductInfo.Fly2Row;
            numSingleCol.Value = mJobData.mProductInfo.SingleCol;
            cb_IsEnableDp.Checked = mJobData.mProductInfo.IsEnableDp;
            cb_IsReverse1.Checked = mJobData.mProductInfo.IsReverse1;
            cb_IsReverse2.Checked = mJobData.mProductInfo.IsReverse2;
        }


        private void treeView_Station_MouseDown(object sender, MouseEventArgs e)
        {
            if (treeView_Station.Nodes.Count == 0)
            {
                treeView_Station.ContextMenuStrip = CMS_Station;
            }
            else if (treeView_Station.SelectedNode == null)
            {
                treeView_Station.ContextMenuStrip = null;
            }
            else if (e.Button == MouseButtons.Right && treeView_Station.SelectedNode.Level >= 0 && treeView_Station.SelectedNode.Level <= 2)
            {
                Rectangle Rect = treeView_Station.SelectedNode.Bounds;
                bool flag_InX = e.Location.X < Rect.Right && e.Location.X > Rect.Left;
                bool flag_InY = e.Location.Y < Rect.Bottom && e.Location.Y > Rect.Top;
                if (flag_InX && flag_InY)
                {
                    if (treeView_Station.SelectedNode.Level == 0)
                    {
                        CMS_Station.Items[0].Enabled = false;
                        CMS_Station.Items[1].Enabled = false;
                        CMS_Station.Items[2].Enabled = false;
                        CMS_Station.Items[3].Enabled = false;
                        CMS_Station.Items[4].Enabled = false;
                        CMS_Station.Items[5].Enabled = false;
                    }
                    else if (treeView_Station.SelectedNode.Level == 1)
                    {
                        CMS_Station.Items[0].Enabled = false;
                        CMS_Station.Items[1].Enabled = false;
                        CMS_Station.Items[2].Enabled = false;
                        CMS_Station.Items[3].Enabled = false;
                        CMS_Station.Items[4].Enabled = false;
                        CMS_Station.Items[5].Enabled = false;
                    }
                    else if (treeView_Station.SelectedNode.Level == 2)
                    {
                        CMS_Station.Items[0].Enabled = false;
                        CMS_Station.Items[1].Enabled = false;
                        CMS_Station.Items[2].Enabled = false;
                        CMS_Station.Items[3].Enabled = true;
                        CMS_Station.Items[4].Enabled = true;
                        CMS_Station.Items[5].Enabled = true;
                    }
                    treeView_Station.ContextMenuStrip = CMS_Station;
                }
                else
                {
                    treeView_Station.ContextMenuStrip = null;
                }
            }
            else
            {
                treeView_Station.ContextMenuStrip = null;
            }
        }

        private void treeView_Station_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label == null)
            {
                e.CancelEdit = true;
                e.Node.EndEdit(cancel: true);
                return;
            }
            if (ContainNodeText(e.Node, e.Label))
            {
                e.CancelEdit = true;
                e.Node.EndEdit(cancel: true);
                MessageBox.Show(@"节点重名，请重新命名！");
                return;
            }
            if (treeView_Station.SelectedNode.Level == 1)
            {
                mStationCollection.Replace(treeView_Station.SelectedNode.Text, e.Label);
                for (int i = 0; i < mStationCollection[e.Label].Count; i++)
                {
                    mStationCollection[e.Label][i].Station = e.Label;
                }
                if (mStationCollection[e.Label].Count > 0)
                {
                    mInspectionProperty.SetProperty(mStationCollection[e.Label][0]);
                    propertyGrid.SelectedObject = mInspectionProperty.propertyGridProperty;
                }
            }
            else if (treeView_Station.SelectedNode.Level == 2)
            {
                mStationCollection[e.Node.Parent.Text].Replace(treeView_Station.SelectedNode.Text, e.Label);
                mStationCollection[e.Node.Parent.Text][e.Label].Inspect = e.Label;
                mInspectionProperty.SetProperty(mStationCollection[e.Node.Parent.Text][e.Label]);
                propertyGrid.SelectedObject = mInspectionProperty.propertyGridProperty;
            }
            e.Node.EndEdit(cancel: true);
        }

        private void treeView_Station_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView_Station.SelectedNode != null && treeView_Station.SelectedNode.Level == 2)
            {
                string stationKey = treeView_Station.SelectedNode.Parent.Text;
                string inspectionKey = treeView_Station.SelectedNode.Text;
                mSelectStation = treeView_Station.SelectedNode.Text;
                mInspectionConfig = mStationCollection[stationKey][inspectionKey];
                mInspectionProperty.SetProperty(mInspectionConfig);
                propertyGrid.SelectedObject = mInspectionProperty.propertyGridProperty;
                tabControl1.TabPages[0].Controls["label"].Text = stationKey + " -- " + inspectionKey;
                tabControl1.TabPages[0].Controls["label"].BringToFront();
            }
        }


        private void btn_Run_Click(object sender, EventArgs e)
        {
            mMainWorkFlow.Run(treeView_Station.SelectedNode.Parent.Text,treeView_Station.SelectedNode.Text);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            TreeNode node = treeView_Station.SelectedNode;
            AddStaionConfig(AddNodeText(node));
        }

        private void btn_Rename_Click(object sender, EventArgs e)
        {
            treeView_Station.SelectedNode.BeginEdit();
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes != MessageBox.Show("是否删除该节点","",MessageBoxButtons.YesNoCancel))
                return;
            TreeNode parentNode = treeView_Station.SelectedNode.Parent;
            if (treeView_Station.SelectedNode.Level == 1)
            {
                mStationCollection.Remove(treeView_Station.SelectedNode.Text);
            }
            else if (treeView_Station.SelectedNode.Level == 2)
            {
                mStationCollection[parentNode.Text].Remove(treeView_Station.SelectedNode.Text);
                if (mStationCollection[parentNode.Text].Count == 0)
                {
                    mStationCollection.Remove(parentNode.Text);
                }
            }
            treeView_Station.SelectedNode.Remove();
            if (parentNode.Level != 0 && parentNode.Nodes.Count == 0)
            {
                treeView_Station.SelectedNode = parentNode;
                treeView_Station.SelectedNode.Remove();
            }
        }

        private string AddNodeText(TreeNode node)
        {
            string path = "";
            List<string> names = new List<string>();
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                names.Add(node.Nodes[i].Text);
            }
            string text = "";
            text = ((node.Level == 0) ? "工位" : ((node.Level != 1) ? "工位配置" : "检测"));
            int j = 1;
            string key = text + j;
            while (names.Contains(key))
            {
                key = text + j;
                j++;
            }
            if (node.Level == 0)
            {
                node.Nodes.Add(key);
                node.Nodes[node.Nodes.Count - 1].Nodes.Add("检测1");
                path = key + ",检测1,0";
            }
            else if (node.Level == 1)
            {
                node.Nodes.Add(key);
                path = node.Text + "," + key + ",1";
            }
            return path;
        }

        private void AddStaionConfig(string path)
        {
            string[] keys = path.Split(',');
            if (keys[2] == "0")
            {
                mInspectionConfig = new InspectionConfig();
                mInspectionConfig.Station = keys[0];
                mInspectionConfig.Inspect = keys[1];
                mStationConfig = new StationConfig();
                mStationConfig.Add(keys[1], mInspectionConfig);
                mStationCollection.Add(keys[0], mStationConfig);
            }
            else if (keys[2] == "1")
            {
                mInspectionConfig = new InspectionConfig();
                mInspectionConfig.Station = keys[0];
                mInspectionConfig.Inspect = keys[1];
                mStationCollection[keys[0]].Add(keys[1], mInspectionConfig);
            }
        }

        private bool ContainNodeText(TreeNode node, string text)
        {
            TreeNode parentNode = node.Parent;
            List<string> names = new List<string>();
            for (int i = 0; i < parentNode.Nodes.Count; i++)
            {
                names.Add(parentNode.Nodes[i].Text);
            }
            if (names.Contains(text))
            {
                return true;
            }
            return false;
        }

        private void cmb_JobChangeCommTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            mJobData.mJobChangeSignal.CommunicationTable = cmb_JobChangeCommTable.SelectedItem.ToString();
        }

        private void cmb_JobChangeCommSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
            mJobData.mJobChangeSignal.CommSerialNum = cmb_JobChangeCommSerial.SelectedItem.ToString();
        }

        private void propertyGrid_PropertySortChanged(object sender, EventArgs e)
        {
            propertyGrid.PropertySort = PropertySort.Categorized;
        }

        private void propertyGrid_PropertyValueChanged(object sender, PropertyValueChangedEventArgs e)
        {
            string station = (string)mInspectionProperty.propertyGridProperty[0].Value;
            string Inspect = (string)mInspectionProperty.propertyGridProperty[1].Value;
            InspectionConfig inspectionConfig = mStationCollection[station][Inspect];
            mInspectionProperty.SetInspectConfig(ref inspectionConfig);
            mInspectionProperty.InitPropertyGrid(inspectionConfig);
            propertyGrid.SelectedObject = mInspectionProperty.propertyGridProperty;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                string station = (string)((PropertyGridProperty)propertyGrid.SelectedObject)[0].Value;
                string inspection = (string)((PropertyGridProperty)propertyGrid.SelectedObject)[1].Value;
                if (propertyGrid.SelectedGridItem.PropertyDescriptor == null)
                {
                    LogUtil.Log("请不要选择标题");
                    return;
                }
                string name = propertyGrid.SelectedGridItem.PropertyDescriptor.Name;
                string value = propertyGrid.SelectedGridItem.PropertyDescriptor.GetValue(propertyGrid)?.ToString();
                if (name == "Algorithm" && value != "")
                {
                    //Frm_ModuleConfig frm_ModuleConfig = new Frm_ModuleConfig(station, inspection, name, value, mJobData);
                    //MultiLanguage.LoadLanguage(frm_ModuleConfig, MultiLanguage.GetDefaultLanguage());
                    //frm_ModuleConfig.ShowDialog();
                    FrmModuleConfigDgv frm_ModuleConfigDgv =
                        new FrmModuleConfigDgv(mSelectStation.ToEnum<EnumStation>(), mMainWorkFlow.mTasFlows[treeView_Station.SelectedNode.Text], mJobData);
                    MultiLanguage.LoadLanguage(frm_ModuleConfigDgv, MultiLanguage.GetDefaultLanguage());
                    frm_ModuleConfigDgv.ShowDialog();
                }
                else if (name == "CommunicationTable" && value != "")
                {
                    Frm_ModuleConfig frm_ModuleConfig3 = new Frm_ModuleConfig(station, inspection, name, value, mJobData);
                    MultiLanguage.LoadLanguage(frm_ModuleConfig3, MultiLanguage.GetDefaultLanguage());
                    frm_ModuleConfig3.ShowDialog();
                }
                else if (name == "CommunicationTable_A" && value != "")
                {
                    Frm_ModuleConfig frm_ModuleConfig4 = new Frm_ModuleConfig(station, inspection, name, value, mJobData);
                    MultiLanguage.LoadLanguage(frm_ModuleConfig4, MultiLanguage.GetDefaultLanguage());
                    frm_ModuleConfig4.ShowDialog();
                }
                else if (name == "InspectType" && (value == "StageSummary" || value == "Summary"))
                {
                    //Frm_ModuleConfig frm_ModuleConfig2 = new Frm_ModuleConfig(station, inspection, name, mJobData.ID.ToString(), mJobData);
                    //FrmModuleConfigDgv frm_ModuleConfigDgv =
                    //    new FrmModuleConfigDgv(mSelectStation.ToEnum<EnumStation>(), mJobData);
                    //MultiLanguage.LoadLanguage(frm_ModuleConfigDgv, MultiLanguage.GetDefaultLanguage());
                    //frm_ModuleConfigDgv.ShowDialog();
                }
                else if (name == "CameraName")
                {
                    FrmCameraConfig frm_CameraConfig = new FrmCameraConfig(mJobData.mCameraData, mJobData.mCameraInfo, mJobData.mCameraData_CL);
                    MultiLanguage.LoadLanguage(frm_CameraConfig, MultiLanguage.GetDefaultLanguage());
                    frm_CameraConfig.ShowDialog();
                }
                else if (name == "CameraType" && value != "")
                {
                    string camKey = (string)((PropertyGridProperty)propertyGrid.SelectedObject)[2].Value;
                    if (camKey != "" && (value == "2D" || value == "2D_LineScan" || value == "3D"))
                    {
                        OpenCameraConfig.OpenDiffTypeCamera(mJobData.mCameraData[camKey], mJobData.mCameraInfo, mJobData.CameraDeviceInfoPath);
                    }
                    if (camKey != "" && (value == "C_2DLineCL" || value == "C_2DLineGige"))
                    {
                        //OpenCameraConfig.OpenDiffTypeFrameGrabber(mJobData.mCameraData_CL[camKey]);
                        //FrmCameraDahuaCL frm = new FrmCameraDahuaCL(mJobData.mDahuaCameraData_CL,mJobData.DahuaCameraConfigFilePath_CL);
                        //frm.ShowDialog();
                    }
                }
                else if (name == "CommSerialNum" || (name == "CommSerialNum_A" && value != ""))
                {
                    string commKey = "";
                    string commTable = "";
                    if (name == "CommSerialNum")
                    {
                        commTable = (string)((PropertyGridProperty)propertyGrid.SelectedObject)[13].Value;
                        commKey = (string)((PropertyGridProperty)propertyGrid.SelectedObject)[14].Value;
                    }
                    else
                    {
                        commTable = (string)((PropertyGridProperty)propertyGrid.SelectedObject)[15].Value;
                        commKey = (string)((PropertyGridProperty)propertyGrid.SelectedObject)[16].Value;
                    }
                    if (commKey != "" && commTable != "" && CommunicationOperator.commCollection.ListKeys.Contains(commKey))
                    {
                        CommunicationOperator.commCollection[commKey].SetInputsOutputs(mJobData.mCommData.Dic[commTable]);
                        OpenCommConfig.OpenDiffTypeComm(mJobData.mCommBaseInfo.Query(commKey), mJobData.mCommBaseInfo, mJobData.CommDeviceInfoPath);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void RegisterEvent()
        {
            mJobData.mCameraData.InsertedItem += MCameraData_InsertedItem;
            mJobData.mCameraData.RemovingItem += MCameraData_RemovingItem;
            mJobData.mCameraData.RemovedItem += MCameraData_RemovedItem;
            mJobData.mCameraData_CL.InsertedItem += MCameraDataCL_InsertedItem;
            mJobData.mCameraData_CL.RemovingItem += MCameraDataCL_RemovingItem;
            mJobData.mCameraData_CL.RemovedItem += MCameraDataCL_RemovedItem;
        }

        private void UnregisterEvent()
        {
            mJobData.mCameraData.InsertedItem -= MCameraData_InsertedItem;
            mJobData.mCameraData.RemovingItem -= MCameraData_RemovingItem;
            mJobData.mCameraData.RemovedItem -= MCameraData_RemovedItem;
            mJobData.mCameraData_CL.InsertedItem -= MCameraDataCL_InsertedItem;
            mJobData.mCameraData_CL.RemovingItem -= MCameraDataCL_RemovingItem;
            mJobData.mCameraData_CL.RemovedItem -= MCameraDataCL_RemovedItem;
        }

        private void MCameraData_RemovingItem(object sender, CollectionRemoveEventArgs e)
        {
            camConfigKey = mJobData.mCameraData.GetKeys()[e.Index];
        }

        private void MCameraData_RemovedItem(object sender, CollectionRemoveEventArgs e)
        {
            for (int i = 0; i < mStationCollection.Count; i++)
            {
                for (int j = 0; j < mStationCollection[i].Count; j++)
                {
                    if (mStationCollection[i][j].CameraName == camConfigKey)
                    {
                        mStationCollection[i][j].CameraName = "";
                        mStationCollection[i][j].CameraSerialNum = "";
                        mStationCollection[i][j].CameraType = "";
                    }
                }
            }
            string station = (string)mInspectionProperty.propertyGridProperty[0].Value;
            string Inspect = (string)mInspectionProperty.propertyGridProperty[1].Value;
            mInspectionProperty.InitPropertyGrid(mStationCollection[station][Inspect]);
            propertyGrid.SelectedObject = mInspectionProperty.propertyGridProperty;
        }

        private void MCameraData_InsertedItem(object sender, CollectionInsertEventArgs e)
        {
            string station = (string)mInspectionProperty.propertyGridProperty[0].Value;
            string Inspect = (string)mInspectionProperty.propertyGridProperty[1].Value;
            mInspectionProperty.InitPropertyGrid(mStationCollection[station][Inspect]);
            propertyGrid.SelectedObject = mInspectionProperty.propertyGridProperty;
        }

        private void MCameraDataCL_RemovingItem(object sender, CollectionRemoveEventArgs e)
        {
            camConfigKey = mJobData.mCameraData_CL.GetKeys()[e.Index];
        }

        private void MCameraDataCL_RemovedItem(object sender, CollectionRemoveEventArgs e)
        {
            for (int i = 0; i < mStationCollection.Count; i++)
            {
                for (int j = 0; j < mStationCollection[i].Count; j++)
                {
                    if (mStationCollection[i][j].CameraName == camConfigKey)
                    {
                        mStationCollection[i][j].CameraName = "";
                        mStationCollection[i][j].CameraSerialNum = "";
                        mStationCollection[i][j].CameraType = "";
                    }
                }
            }
            string station = (string)mInspectionProperty.propertyGridProperty[0].Value;
            string Inspect = (string)mInspectionProperty.propertyGridProperty[1].Value;
            mInspectionProperty.InitPropertyGrid(mStationCollection[station][Inspect]);
            propertyGrid.SelectedObject = mInspectionProperty.propertyGridProperty;
        }

        private void MCameraDataCL_InsertedItem(object sender, CollectionInsertEventArgs e)
        {
            string station = (string)mInspectionProperty.propertyGridProperty[0].Value;
            string Inspect = (string)mInspectionProperty.propertyGridProperty[1].Value;
            mInspectionProperty.InitPropertyGrid(mStationCollection[station][Inspect]);
            propertyGrid.SelectedObject = mInspectionProperty.propertyGridProperty;
        }

        private void UpdateTriggerPoint()
        {
            for (int i = 0; i < mJobData.mCommData.Dic.Count; i++)
            {
                for (int k = 0; k < mJobData.mCommData.Dic[i].Outputs.Count; k++)
                {
                    mJobData.mCommData.Dic[i].Outputs[k].IsTriggerPoint = false;
                }
            }
            for (int j = 0; j < mStationCollection.Count; j++)
            {
                for (int l = 0; l < mStationCollection[j].Count; l++)
                {
                    string commTable = mStationCollection[j][l].CommunicationTable;
                    string triggerPoint = mStationCollection[j][l].TriggerPoint;
                    if (commTable != "" && triggerPoint != "")
                    {
                        mJobData.mCommData.Dic[commTable].Outputs[triggerPoint].IsTriggerPoint = true;
                    }
                }
            }
        }

        private void btn_SaveParam_Click(object sender, EventArgs e)
        {
            UpdateTriggerPoint();
            if (mJobData.SaveAllData())
            {
                mMainWorkFlow.ModifyWorkFlow();
                MessageBox.Show(@"参数保存成功！");
            }
            else
            {
                MessageBox.Show(@"参数保存失败！");
            }
        }

        private void ProcedurePlayBack_Click(object sender, EventArgs e)
        {
            if (frm_ProcedurePlayBack != null)
            {
                if (frm_ProcedurePlayBack.IsDisposed)
                {
                    frm_ProcedurePlayBack = Frm_ProcedurePlayBack.CreateInstance(mJobData);
                }
                MultiLanguage.LoadLanguage(frm_ProcedurePlayBack, MultiLanguage.GetDefaultLanguage());
                frm_ProcedurePlayBack.Show();
            }
            else
            {
                frm_ProcedurePlayBack = Frm_ProcedurePlayBack.CreateInstance(mJobData);
                MultiLanguage.LoadLanguage(frm_ProcedurePlayBack, MultiLanguage.GetDefaultLanguage());
                frm_ProcedurePlayBack.Show();
            }
        }

        private void Frm_Station_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.NotifyMesState = null;
            UnregisterEvent();
            //if (!File.Exists(mJobData.MesStationFilePath))
            //{
            //    mesStationConfig.Clear();
            //}
        }

        private void tsm_LoadRun_Click(object sender, EventArgs e)
        {
            try
            {
                using OpenFileDialog openFileDialog = new OpenFileDialog();
                // 设置文件过滤器（只允许图片格式）
                openFileDialog.Filter =
                    "图片文件|*.jpg;*.jpeg;*.png;*.bmp;*.gif|所有文件|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // 通过文件流加载（避免锁定文件）
                        using var stream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read);
                        var img = Image.FromStream(stream);
                        Bitmap bmp = new Bitmap(img);
                        List<ICogImage> cogImage = new List<ICogImage>();
                        cogImage.Add(new CogImage8Grey(bmp));
                        mMainWorkFlow.Run(treeView_Station.SelectedNode.Parent.Text, treeView_Station.SelectedNode.Text, cogImage,RunMode.手动运行流程);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"图片加载失败: {ex.Message}", "错误",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception exception)
            {
                
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                var row = (int)numRow.Value;
                var col = (int)numCol.Value;
                var num = (int)numFlyNum.Value;
                var row1 = (int)numFly1Row.Value;
                var row2 = (int)numFly2Row.Value;
                int n = (int)numSingleCol.Value;
                bool t = cb_IsEnableDp.CheckState == CheckState.Checked;
                int[] fr = new int[num];
                for (int i = 0; i < num - 1; i++)
                {
                    fr[i] = n;
                }
                fr[num - 1] = col - (num - 1) * n;

                mJobData.mProductInfo.Row = row;
                mJobData.mProductInfo.Col = col;
                mJobData.mProductInfo.FlyNum = num;
                mJobData.mProductInfo.Fly1Row = row1;
                mJobData.mProductInfo.Fly2Row = row2;
                mJobData.mProductInfo.SingleCol = n;
                mJobData.mProductInfo.IsReverse1 = cb_IsReverse1.Checked;
                mJobData.mProductInfo.IsReverse2 = cb_IsReverse2.Checked;

                if (mJobData.mProductInfo.IsReverse1)
                {
                    mJobData.mProductInfo.FlyColArray1 = fr.Reverse().ToArray();
                }
                else
                {
                    mJobData.mProductInfo.FlyColArray1 = fr;
                }
                if (mJobData.mProductInfo.IsReverse2)
                {
                    mJobData.mProductInfo.FlyColArray2 = fr.Reverse().ToArray();
                }
                else
                {
                    mJobData.mProductInfo.FlyColArray2 = fr;
                }

                mJobData.mProductInfo.IsEnableDp = t;
                XmlHelper.WriteXML(mJobData.mProductInfo, mJobData.ProductParameterPath, typeof(ProductInfo));
                MessageBox.Show("保存成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败" + ex);
            }
        }

        private void tsm_LoadImagesRun_Click(object sender, EventArgs e)
        {
            try
            {
                List<ICogImage> CogImages = new List<ICogImage>();
                List<ICogImage> CogImages2 = new List<ICogImage>();
                using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
                {
                    if (folderDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFolder = folderDialog.SelectedPath;
                        if (!Directory.Exists(selectedFolder))
                        {
                            MessageBox.Show("目录不存在！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // 获取所有支持的图片文件
                        var imageFiles = Directory.GetFiles(selectedFolder, "*.*", SearchOption.TopDirectoryOnly)
                            .Where(file => ImageHelper.SupportedExtensions.Contains(Path.GetExtension(file)));

                        foreach (var file in imageFiles)
                        {
                            // 通过文件流加载（避免锁定文件）
                            using var stream = new FileStream(file, FileMode.Open, FileAccess.Read);
                            var img = Image.FromStream(stream);
                            Bitmap bmp = new Bitmap(img);
                            ICogImage cogImage = new CogImage8Grey(bmp);
                            CogImages.Add(cogImage);
                        }
                        mMainWorkFlow.Run(treeView_Station.SelectedNode.Parent.Text, treeView_Station.SelectedNode.Text, CogImages, RunMode.手动运行流程);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"图片加载失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
