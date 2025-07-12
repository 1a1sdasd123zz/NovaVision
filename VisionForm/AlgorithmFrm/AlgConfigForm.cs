using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using Cognex.VisionPro.Implementation;
using Cognex.VisionPro.Interop;
using Cognex.VisionPro.ToolBlock;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Module;
using NovaVision.BaseClass.Module.Algorithm;
using NovaVision.BaseClass.VisionConfig;
using Info = NovaVision.BaseClass.Module.Algorithm.Info;

namespace NovaVision.VisionForm.AlgorithmFrm
{
    public partial class AlgConfigForm : Form
    {
        private ModuleData<Terminal, Info> mAlgModuleData;

        private JobData mJobData;

        private Dictionary<OperaterType, string> OperaterLog_Dic = new Dictionary<OperaterType, string>();

        private static AlgConfigForm _instance;

        private bool IsOnLine = false;

        private object obj = new object();

        private string LoadedAlg = "";

        public static AlgConfigForm CreateInstance(JobData jobData, bool isOnline)
        {
            if (_instance == null)
            {
                _instance = new AlgConfigForm(jobData, isOnline);
            }
            return _instance;
        }

        private AlgConfigForm(JobData jobData, bool isOnline)
        {
            mJobData = jobData;
            mAlgModuleData = mJobData.mAlgModuleData;
            IsOnLine = isOnline;
            InitializeComponent();
            InitAlg();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 500.0;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer)sender;
            timer.Enabled = false;
            if (IsOnLine)
            {
                FormCollection m_formList = Application.OpenForms;
                for (int i = 0; i < m_formList.Count; i++)
                {
                    if (!(m_formList[i] is CogScriptEditorV2))
                    {
                        continue;
                    }
                    Invoke((Action)delegate
                    {
                        if (m_formList[i].IsHandleCreated)
                        {
                            m_formList[i].Close();
                        }
                    });
                    MessageBox.Show("视觉系统在线，请勿打开脚本！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    break;
                }
            }
            timer.Enabled = true;
        }

        private void InitAlg()
        {
            if (mAlgModuleData.Dic.Count > 0)
            {
                List<string> keys = mAlgModuleData.Dic.GetKeys();
                if (mAlgModuleData.Dic.Count != 0)
                {
                    configCtrl_Alg.ListBoxNames.Items.Clear();
                    ListBox.ObjectCollection items = configCtrl_Alg.ListBoxNames.Items;
                    object[] items2 = keys.ToArray();
                    items.AddRange(items2);
                    configCtrl_Alg.ListBoxNames.SelectedIndex = -1;
                }
            }
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            if (txt_Name.Text == "")
            {
                MessageBox.Show(@"请输入算法名：");
                return;
            }
            if (!mAlgModuleData.Dic.ContainsKey(txt_Name.Text.Trim()))
            {
                try
                {
                    LoadedAlg = txt_Name.Text.Trim();
                    mAlgModuleData.Dic.Add(txt_Name.Text.Trim(), new InputsOutputs<Terminal, Info>());
                    UnRegisterSubject(cogToolBlockEditV21);
                    if (cogToolBlockEditV21.Subject != null)
                    {
                        UnRegisterEvent(cogToolBlockEditV21.Subject);
                    }
                    CogToolBlock TB = new CogToolBlock();
                    cogToolBlockEditV21.Subject = TB;
                    RegisterSubject(cogToolBlockEditV21);
                    RegisterEvent(cogToolBlockEditV21.Subject);
                    CogToolBlockTerminal Input = new CogToolBlockTerminal("Image", typeof(Cognex.VisionPro.ICogImage));
                    CogToolBlockTerminal Input2 = new CogToolBlockTerminal("Index", typeof(int));
                    CogToolBlockTerminal Output = new CogToolBlockTerminal("Result", typeof(bool));
                    cogToolBlockEditV21.Subject.Inputs.Add(Input);
                    cogToolBlockEditV21.Subject.Inputs.Add(Input2);
                    cogToolBlockEditV21.Subject.Outputs.Add(Output);
                    mJobData.mTools.Add(txt_Name.Text.Trim(), TB);
                    configCtrl_Alg.ListBoxNames.Items.Clear();
                    ListBox.ObjectCollection items = configCtrl_Alg.ListBoxNames.Items;
                    object[] items2 = mAlgModuleData.Dic.GetKeys().ToArray();
                    items.AddRange(items2);
                    configCtrl_Alg.ListBoxNames.SelectedIndex = configCtrl_Alg.ListBoxNames.Items.Count - 1;
                    LogOperater(OperaterType.AddAlg);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("算法文件添加失败" + ex.Message);
                    LogUtil.LogError("算法文件添加失败" + ex.Message);
                    return;
                }
            }
            MessageBox.Show(@"算法名与已有算法重名，请重新输入");
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Control.MousePosition.X, Control.MousePosition.Y);
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            if (configCtrl_Alg.ListBoxNames.SelectedIndex < 0)
            {
                MessageBox.Show(@"请选择要删除算法！");
                return;
            }
            DialogResult dr = MessageBox.Show("是否删除算法：" + configCtrl_Alg.ListBoxNames.SelectedItem.ToString() + " ?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            if (dr == DialogResult.OK && Directory.Exists(mJobData.mSystemConfigData.AlgMoudlePath) && File.Exists(mJobData.mSystemConfigData.AlgMoudlePath + "\\" + configCtrl_Alg.ListBoxNames.SelectedItem.ToString() + ".vpp"))
            {
                File.Delete(mJobData.mSystemConfigData.AlgMoudlePath + "\\" + configCtrl_Alg.ListBoxNames.SelectedItem.ToString() + ".vpp");
                UnRegisterSubject(cogToolBlockEditV21);
                UnRegisterEvent(cogToolBlockEditV21.Subject);
                cogToolBlockEditV21.Subject = null;
                if (mAlgModuleData.Dic.GetKeys().Count > 1)
                {
                    mAlgModuleData.Dic.Remove(configCtrl_Alg.ListBoxNames.SelectedItem.ToString());
                    mJobData.mTools.Remove(configCtrl_Alg.ListBoxNames.SelectedItem.ToString());
                    configCtrl_Alg.ListBoxNames.Items.Clear();
                    ListBox.ObjectCollection items = configCtrl_Alg.ListBoxNames.Items;
                    object[] items2 = mAlgModuleData.Dic.GetKeys().ToArray();
                    items.AddRange(items2);
                    configCtrl_Alg.ListBoxNames.SelectedIndex = configCtrl_Alg.ListBoxNames.Items.Count - 1;
                    mJobData.SaveAllData();
                }
                else
                {
                    mAlgModuleData.Dic.Remove(configCtrl_Alg.ListBoxNames.SelectedItem.ToString());
                    mJobData.mTools.Remove(configCtrl_Alg.ListBoxNames.SelectedItem.ToString());
                    configCtrl_Alg.ListBoxNames.Items.Clear();
                    configCtrl_Alg.ListBoxNames.SelectedIndex = -1;
                    LoadedAlg = "";
                    File.Delete(mJobData.AlgDataFilePath);
                }
            }
        }

        private void ReadInputsOutputs(string key)
        {
            try
            {
                CogToolBlock TB = cogToolBlockEditV21.Subject;
                for (int j = 0; j < cogToolBlockEditV21.Subject.Inputs.Count; j++)
                {
                    mAlgModuleData.Dic[key].Inputs[j].Value.mValue = TB.Inputs[j].Value;
                }
                for (int i = 0; i < cogToolBlockEditV21.Subject.Outputs.Count; i++)
                {
                    mAlgModuleData.Dic[key].Outputs[i].Value.mValue = TB.Outputs[i].Value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"算法输入输出引脚存储出错，请移除算法重新配置");
                LogUtil.LogError("xml文件中输入输出与算法输入输出不匹配," + ex.Message);
            }
        }

        private void WriteInputsOutputs(string key)
        {
            try
            {
                CogToolBlock TB = cogToolBlockEditV21.Subject;
                for (int j = 0; j < TB.Inputs.Count; j++)
                {
                    if (j != 0)
                    {
                        TB.Inputs[j].Value = mAlgModuleData.Dic[key].Inputs[j].Value.mValue;
                    }
                }
                for (int i = 0; i < TB.Outputs.Count; i++)
                {
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"算法输入输出引脚存储出错，请移除算法重新配置");
                LogUtil.LogError("xml文件中输入输出与算法输入输出不匹配," + ex.Message);
            }
        }

        private void btn_Review_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.JPEG,*.jpg,*.jpeg,*.bmp,*.cdb)|*.JPEG;*.jpg;*.jpeg;*.bmp;*.cdb";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                CogImageFileTool imageFileTool = new CogImageFileTool();
                imageFileTool.Operator.Open(fileName, CogImageFileModeConstants.Read);
                imageFileTool.Run();
                if (configCtrl_Alg.ListBoxNames.SelectedIndex >= 0)
                {
                    cogToolBlockEditV21.Subject.Inputs[0].Value = imageFileTool.OutputImage;
                    cogToolBlockEditV21.Subject.Run();
                }
                else
                {
                    MessageBox.Show(@"请选择要回放的算法");
                }
            }
        }

        private void tsBtn_Up_Click(object sender, EventArgs e)
        {
            int index = configCtrl_Alg.ListBoxNames.SelectedIndex;
            if (mAlgModuleData.Dic.MoveUp(index))
            {
                mJobData.mTools.MoveUp(index);
                configCtrl_Alg.ListBoxNames.Items.Clear();
                ListBox.ObjectCollection items = configCtrl_Alg.ListBoxNames.Items;
                object[] items2 = mAlgModuleData.Dic.GetKeys().ToArray();
                items.AddRange(items2);
                configCtrl_Alg.ListBoxNames.SelectedIndex = index - 1;
                mJobData.SaveAllData();
            }
        }

        private void tsBtn_Down_Click(object sender, EventArgs e)
        {
            int index = configCtrl_Alg.ListBoxNames.SelectedIndex;
            if (mAlgModuleData.Dic.MoveDown(configCtrl_Alg.ListBoxNames.SelectedIndex))
            {
                mJobData.mTools.MoveDown(index);
                configCtrl_Alg.ListBoxNames.Items.Clear();
                ListBox.ObjectCollection items = configCtrl_Alg.ListBoxNames.Items;
                object[] items2 = mAlgModuleData.Dic.GetKeys().ToArray();
                items.AddRange(items2);
                configCtrl_Alg.ListBoxNames.SelectedIndex = index + 1;
                mJobData.SaveAllData();
            }
        }

        private void configCtrl_Alg_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (configCtrl_Alg.ListBoxNames.SelectedIndex >= 0)
            {
                TextBox textBox1 = new TextBox();
                textBox1.Name = "textBox1";
                textBox1.Visible = true;
                configCtrl_Alg.ListBoxNames.Controls.Add(textBox1);
                Rectangle vRectangle = configCtrl_Alg.ListBoxNames.GetItemRectangle(configCtrl_Alg.ListBoxNames.SelectedIndex);
                textBox1.BorderStyle = BorderStyle.FixedSingle;
                textBox1.Text = configCtrl_Alg.ListBoxNames.Items[configCtrl_Alg.ListBoxNames.SelectedIndex].ToString();
                textBox1.BringToFront();
                textBox1.Bounds = vRectangle;
                textBox1.Focus();
                textBox1.KeyPress += TextBox1_KeyPress;
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != '\r')
            {
                return;
            }
            TextBox textBox1 = configCtrl_Alg.ListBoxNames.Controls["textBox1"] as TextBox;
            if (!mAlgModuleData.Dic.ContainsKey(textBox1.Text.Trim()))
            {
                int index = configCtrl_Alg.ListBoxNames.SelectedIndex;
                string oldKey = mAlgModuleData.Dic.GetKeys()[index];
                string newKey = textBox1.Text.Trim();
                File.Move(mJobData.mSystemConfigData.AlgMoudlePath + "\\" + configCtrl_Alg.ListBoxNames.SelectedItem.ToString() + ".vpp", mJobData.mSystemConfigData.AlgMoudlePath + "\\" + textBox1.Text.Trim() + ".vpp");
                bool flag1 = mAlgModuleData.Dic.Replace(oldKey, newKey);
                bool flag2 = mJobData.mTools.Replace(oldKey, newKey);
                bool flag3 = mJobData.SaveAllData();
                if (flag1 && flag2 && flag3)
                {
                    LogUtil.Log("算法重命名成功，" + oldKey + "-->" + newKey + "！");
                }
                else
                {
                    LogUtil.Log("算法重命名失败，" + oldKey + "-->" + newKey + "！");
                }
                configCtrl_Alg.ListBoxNames.Controls.RemoveByKey("textBox1");
                configCtrl_Alg.ListBoxNames.Items.Clear();
                ListBox.ObjectCollection items = configCtrl_Alg.ListBoxNames.Items;
                object[] items2 = mAlgModuleData.Dic.GetKeys().ToArray();
                items.AddRange(items2);
                configCtrl_Alg.ListBoxNames.SelectedIndex = index;
            }
            else
            {
                MessageBox.Show(@"算法重名，请重新命名？");
            }
        }

        private void listBox_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (configCtrl_Alg.ListBoxNames.SelectedItem != null && LoadedAlg != configCtrl_Alg.ListBoxNames.SelectedItem.ToString())
            {
                UnRegisterSubject(cogToolBlockEditV21);
                if (cogToolBlockEditV21.Subject != null)
                {
                    UnRegisterEvent(cogToolBlockEditV21.Subject);
                }
                LoadedAlg = configCtrl_Alg.ListBoxNames.SelectedItem.ToString();
                cogToolBlockEditV21.Subject = mJobData.mTools[LoadedAlg];
                RegisterSubject(cogToolBlockEditV21);
                RegisterEvent(cogToolBlockEditV21.Subject);
                //WriteInputsOutputs(configCtrl_Alg.ListBoxNames.SelectedItem.ToString());
                LogOperater(OperaterType.SelectedIndexChanged);
            }
            if (configCtrl_Alg.ListBoxNames.Controls.ContainsKey("textBox1"))
            {
                configCtrl_Alg.ListBoxNames.Controls.RemoveByKey("textBox1");
            }
        }

        private void RegisterSubject(CogToolBlockEditV2 subject)
        {
            subject.SubjectChanging -= CogToolBlockEditV21_SubjectChanging;
            subject.SubjectChanging += CogToolBlockEditV21_SubjectChanging;
            subject.SubjectChanged -= CogToolBlockEditV21_SubjectChanged;
            subject.SubjectChanged += CogToolBlockEditV21_SubjectChanged;
        }

        private void UnRegisterSubject(CogToolBlockEditV2 subject)
        {
            subject.SubjectChanging -= CogToolBlockEditV21_SubjectChanging;
            subject.SubjectChanged -= CogToolBlockEditV21_SubjectChanged;
        }

        private void RegisterEvent(CogToolBlock TB)
        {
            TB.Inputs.TrackedItemNameChanging -= Inputs_TrackedItemNameChanging;
            TB.Inputs.TrackedItemNameChanging += Inputs_TrackedItemNameChanging;
            TB.Outputs.TrackedItemNameChanging -= Outputs_TrackedItemNameChanging;
            TB.Outputs.TrackedItemNameChanging += Outputs_TrackedItemNameChanging;
            TB.Inputs.MovedItem -= Inputs_MovedItem;
            TB.Inputs.MovedItem += Inputs_MovedItem;
            TB.Outputs.MovedItem -= Outputs_MovedItem;
            TB.Outputs.MovedItem += Outputs_MovedItem;
            TB.Inputs.InsertedItem -= Inputs_InsertedItem;
            TB.Inputs.InsertedItem += Inputs_InsertedItem;
            TB.Outputs.InsertedItem -= Outputs_InsertedItem;
            TB.Outputs.InsertedItem += Outputs_InsertedItem;
            TB.Inputs.RemovedItem -= Inputs_RemovedItem;
            TB.Inputs.RemovedItem += Inputs_RemovedItem;
            TB.Outputs.RemovedItem -= Outputs_RemovedItem;
            TB.Outputs.RemovedItem += Outputs_RemovedItem;
        }

        private void UnRegisterEvent(CogToolBlock TB)
        {
            TB.Inputs.TrackedItemNameChanging -= Inputs_TrackedItemNameChanging;
            TB.Outputs.TrackedItemNameChanging -= Outputs_TrackedItemNameChanging;
            TB.Inputs.MovedItem -= Inputs_MovedItem;
            TB.Outputs.MovedItem -= Outputs_MovedItem;
            TB.Inputs.InsertedItem -= Inputs_InsertedItem;
            TB.Outputs.InsertedItem -= Outputs_InsertedItem;
            TB.Inputs.RemovedItem -= Inputs_RemovedItem;
            TB.Outputs.RemovedItem -= Outputs_RemovedItem;
        }

        private void CogToolBlockEditV21_SubjectChanging(object sender, EventArgs e)
        {
            CogToolBlockEditV2 TB_Edit = (CogToolBlockEditV2)sender;
            int i;
            for (i = 0; i < cogToolBlockEditV21.Subject.Inputs.Count; i++)
            {
                cogToolBlockEditV21.Subject.Inputs.RemoveAt(i);
                i--;
            }
            int j;
            for (j = 0; j < cogToolBlockEditV21.Subject.Outputs.Count; j++)
            {
                cogToolBlockEditV21.Subject.Outputs.RemoveAt(j);
                j--;
            }
            UnRegisterEvent(TB_Edit.Subject);
        }

        private void CogToolBlockEditV21_SubjectChanged(object sender, EventArgs e)
        {
            CogToolBlockEditV2 TB_Edit = (CogToolBlockEditV2)sender;
            if (TB_Edit.Subject != null)
            {
                mJobData.mTools[LoadedAlg] = TB_Edit.Subject;
                for (int j = 0; j < TB_Edit.Subject.Inputs.Count; j++)
                {
                    Terminal terminal = new Terminal();
                    terminal.Name = TB_Edit.Subject.Inputs[j].Name;
                    terminal.Type = FormatTypeString(TB_Edit.Subject.Inputs[j].ValueType);
                    terminal.Value.mValue = TB_Edit.Subject.Inputs[j].Value;
                    mAlgModuleData.Dic[LoadedAlg].Inputs.Add(TB_Edit.Subject.Inputs[j].Name, terminal);
                    LogUtil.Log("算法" + LoadedAlg + "输入，增加项" + TB_Edit.Subject.Inputs[j].Name + "成功！");
                }
                for (int i = 0; i < TB_Edit.Subject.Outputs.Count; i++)
                {
                    Terminal terminal2 = new Terminal();
                    terminal2.Name = TB_Edit.Subject.Outputs[i].Name;
                    terminal2.Type = FormatTypeString(TB_Edit.Subject.Outputs[i].ValueType);
                    terminal2.Value.mValue = TB_Edit.Subject.Outputs[i].Value;
                    mAlgModuleData.Dic[LoadedAlg].Outputs.Add(TB_Edit.Subject.Outputs[i].Name, terminal2);
                    LogUtil.Log("算法" + LoadedAlg + "输出，增加项" + TB_Edit.Subject.Outputs[i].Name + "成功！");
                }
                RegisterEvent(TB_Edit.Subject);
                LogOperater(OperaterType.AlgInnerOperater);
            }
        }

        private void Outputs_TrackedItemNameChanging(object sender, CogCancelChangingEventArgs<string> e)
        {
            if (e.Exception == null)
            {
                if (!mAlgModuleData.Dic[LoadedAlg].Outputs.Replace(e.OldValue, e.NewValue))
                {
                    LogUtil.Log("算法" + LoadedAlg + "输出，重名命名" + e.OldValue + "-->" + e.NewValue + "失败！");
                }
                else
                {
                    LogUtil.Log("算法" + LoadedAlg + "输出，重名命名" + e.OldValue + "-->" + e.NewValue + "成功！");
                }
                mAlgModuleData.Dic[LoadedAlg].Outputs[e.NewValue].Name = e.NewValue;
                LogOperater(OperaterType.AlgInnerOperater);
            }
            else
            {
                LogUtil.Log("算法" + LoadedAlg + "输出，重名命名" + e.OldValue + "-->" + e.NewValue + "失败,命名不合法！");
            }
        }

        private void Inputs_TrackedItemNameChanging(object sender, CogCancelChangingEventArgs<string> e)
        {
            if (e.Exception == null)
            {
                if (!mAlgModuleData.Dic[LoadedAlg].Inputs.Replace(e.OldValue, e.NewValue))
                {
                    LogUtil.Log("算法" + LoadedAlg + "输入，重名命名" + e.OldValue + "-->" + e.NewValue + "失败！");
                }
                else
                {
                    LogUtil.Log("算法" + LoadedAlg + "输入，重名命名" + e.OldValue + "-->" + e.NewValue + "成功！");
                }
                mAlgModuleData.Dic[LoadedAlg].Inputs[e.NewValue].Name = e.NewValue;
                LogOperater(OperaterType.AlgInnerOperater);
            }
            else
            {
                LogUtil.Log("算法" + LoadedAlg + "输入，重名命名" + e.OldValue + "-->" + e.NewValue + "失败,命名不合法！");
            }
        }

        private void Outputs_RemovedItem(object sender, CogCollectionRemoveEventArgs e)
        {
            string name = mAlgModuleData.Dic[LoadedAlg].Outputs[e.Index].Name;
            if (!mAlgModuleData.Dic[LoadedAlg].Outputs.Remove(e.Index))
            {
                LogUtil.Log("算法" + LoadedAlg + "输出，删除项" + name + "失败！");
            }
            else
            {
                LogUtil.Log("算法" + LoadedAlg + "输出，删除项" + name + "成功！");
            }
            LogOperater(OperaterType.AlgInnerOperater);
        }

        private void Inputs_RemovedItem(object sender, CogCollectionRemoveEventArgs e)
        {
            string name = mAlgModuleData.Dic[LoadedAlg].Inputs[e.Index].Name;
            if (!mAlgModuleData.Dic[LoadedAlg].Inputs.Remove(e.Index))
            {
                LogUtil.Log("算法" + LoadedAlg + "输入，删除项" + name + "失败！");
            }
            else
            {
                LogUtil.Log("算法" + LoadedAlg + "输入，删除项" + name + "成功！");
            }
            LogOperater(OperaterType.AlgInnerOperater);
        }

        private void Outputs_InsertedItem(object sender, CogCollectionInsertEventArgs e)
        {
            CogToolBlockTerminalCollection mOutputs = (CogToolBlockTerminalCollection)sender;
            Terminal terminal = new Terminal();
            terminal.Name = mOutputs[e.Index].Name;
            terminal.Type = FormatTypeString(mOutputs[e.Index].ValueType);
            terminal.Value.mValue = mOutputs[e.Index].Value;
            if (!mAlgModuleData.Dic[LoadedAlg].Outputs.Add(mOutputs[e.Index].Name, terminal))
            {
                LogUtil.Log("算法" + LoadedAlg + "输出，增加项" + mOutputs[e.Index].Name + "失败！");
            }
            else
            {
                LogUtil.Log("算法" + LoadedAlg + "输出，增加项" + mOutputs[e.Index].Name + "成功！");
            }
            LogOperater(OperaterType.AlgInnerOperater);
        }

        private void Inputs_InsertedItem(object sender, CogCollectionInsertEventArgs e)
        {
            CogToolBlockTerminalCollection mInputs = (CogToolBlockTerminalCollection)sender;
            Terminal terminal = new Terminal();
            terminal.Name = mInputs[e.Index].Name;
            terminal.Type = FormatTypeString(mInputs[e.Index].ValueType);
            terminal.Value.mValue = mInputs[e.Index].Value;
            if (!mAlgModuleData.Dic[LoadedAlg].Inputs.Add(mInputs[e.Index].Name, terminal))
            {
                LogUtil.Log("算法" + LoadedAlg + "输入，增加项" + mInputs[e.Index].Name + "失败！");
            }
            else
            {
                LogUtil.Log("算法" + LoadedAlg + "输入，增加项" + mInputs[e.Index].Name + "成功！");
            }
            LogOperater(OperaterType.AlgInnerOperater);
        }

        private void Outputs_MovedItem(object sender, CogCollectionMoveEventArgs e)
        {
            if (e.FromIndex < e.ToIndex)
            {
                mAlgModuleData.Dic[LoadedAlg].Outputs.MoveDown(e.FromIndex);
            }
            else
            {
                mAlgModuleData.Dic[LoadedAlg].Outputs.MoveUp(e.FromIndex);
            }
            LogOperater(OperaterType.AlgInnerOperater);
        }

        private void Inputs_MovedItem(object sender, CogCollectionMoveEventArgs e)
        {
            if (e.FromIndex < e.ToIndex)
            {
                mAlgModuleData.Dic[LoadedAlg].Inputs.MoveDown(e.FromIndex);
            }
            else
            {
                mAlgModuleData.Dic[LoadedAlg].Inputs.MoveUp(e.FromIndex);
            }
            LogOperater(OperaterType.AlgInnerOperater);
        }

        private void AlgConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnRegisterSubject(cogToolBlockEditV21);
            if (cogToolBlockEditV21.Subject != null)
            {
                UnRegisterEvent(cogToolBlockEditV21.Subject);
                cogToolBlockEditV21.Subject = null;
            }
            LogOperater(OperaterType.FormClosing);
            _instance = null;
        }

        private void LogOperater(OperaterType operaterType)
        {
            if (OperaterType.SaveAlg == operaterType)
            {
                OperaterLog_Dic.Clear();
                return;
            }
            if (operaterType == OperaterType.SelectedIndexChanged || OperaterType.AddAlg == operaterType || OperaterType.FormClosing == operaterType)
            {
                if (OperaterLog_Dic.ContainsKey(OperaterType.AlgInnerOperater))
                {
                    string Alg = OperaterLog_Dic[OperaterType.AlgInnerOperater];
                    if (mAlgModuleData.Dic.ContainsKey(Alg))
                    {
                        mJobData.SaveAllData();
                        CogSerializer.SaveObjectToFile(mJobData.mTools[Alg], mJobData.mSystemConfigData.AlgMoudlePath + "\\" + Alg + ".vpp", typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                    }
                }
                OperaterLog_Dic.Clear();
            }
            if (!OperaterLog_Dic.ContainsKey(operaterType))
            {
                OperaterLog_Dic.Add(operaterType, LoadedAlg);
            }
        }

        private string FormatTypeString(Type t)
        {
            if (t.Name == "List`1")
            {
                return t.Name.Split('`')[0] + "<" + t.GenericTypeArguments[0].Name + ">";
            }
            return t.Name;
        }

        private async void 保存原始vppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsOnLine)
            {
                MessageBox.Show("系统在线，不允许保存工具，请离线保存!", "提示", MessageBoxButtons.OK);
                return;
            }
            int index = configCtrl_Alg.ListBoxNames.SelectedIndex;
            string vppName = "";
            if (configCtrl_Alg.ListBoxNames.SelectedItem != null)
            {
                vppName = configCtrl_Alg.ListBoxNames.SelectedItem.ToString();
            }
            if (index >= 0)
            {
                await Task.Run(delegate
                {
                    lock (obj)
                    {
                        CogSerializer.SaveObjectToFile(cogToolBlockEditV21.Subject, mJobData.mSystemConfigData.AlgMoudlePath + "\\" + vppName + ".vpp", typeof(BinaryFormatter), CogSerializationOptionsConstants.All);
                        ReadInputsOutputs(vppName);
                        mJobData.SaveAllData();
                        LogOperater(OperaterType.SaveAlg);
                    }
                });
                MessageBox.Show("算法：" + vppName + ".vpp保存成功");
            }
            else
            {
                MessageBox.Show(@"请选择一个要保存的算法");
            }
        }

        private async void 保存不带图像与结果vppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsOnLine)
            {
                MessageBox.Show("系统在线，不允许保存工具，请离线保存!", "提示", MessageBoxButtons.OK);
                return;
            }
            int index = configCtrl_Alg.ListBoxNames.SelectedIndex;
            string vppName = "";
            if (configCtrl_Alg.ListBoxNames.SelectedItem != null)
            {
                vppName = configCtrl_Alg.ListBoxNames.SelectedItem.ToString();
            }
            if (index >= 0)
            {
                await Task.Run(delegate
                {
                    lock (obj)
                    {
                        CogSerializer.SaveObjectToFile(cogToolBlockEditV21.Subject, mJobData.mSystemConfigData.AlgMoudlePath + "\\" + vppName + ".vpp", typeof(BinaryFormatter), CogSerializationOptionsConstants.Minimum);
                        //ReadInputsOutputs(vppName);
                        mJobData.SaveAllData();
                        LogOperater(OperaterType.SaveAlg);
                    }
                });
                MessageBox.Show("算法：" + vppName + ".vpp保存成功");
            }
            else
            {
                MessageBox.Show(@"请选择一个要保存的算法");
            }
        }

    }
}
