using System;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Communication.CommData;

namespace NovaVision.VisionForm.CommunicationFrm
{
    public partial class ValueEditForm : Form
    {
        private Comm_Element mComm_Element;
        public ValueEditForm(Comm_Element comm_Element)
        {
            this.InitializeComponent();
            this.mComm_Element = comm_Element;
            this.InitDgv(this.mComm_Element);
            this.Tip.Text = this.mComm_Element.Name;
        }

        private void InitDgv(Comm_Element element)
        {
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToOrderColumns = false;
            this.dgv.RowHeadersVisible = false;
            this.dgv.Columns.Add("Type", "类型");
            this.dgv.Columns.Add("Value", "值");
            this.dgv.Columns[1].ValueType = MyTypeConvert.GetType(element.Type);
            this.dgv.Rows.Clear();
            for (int i = 0; i < element.SettingValues.Count; i++)
            {
                this.dgv.Rows.Add();
                this.dgv[0, i].Value = element.Type;
                this.dgv[1, i].Value = element.SettingValues[i];
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            this.mComm_Element.SettingValues.Clear();
            int i = 0;
            while (i < this.dgv.Rows.Count)
            {
                bool flag = this.dgv[1, i].Value == null;
                if (flag)
                {
                    MessageBox.Show(@"内容不能为空,请将所有空内容填满！");
                    this.mComm_Element.SettingValues.Clear();
                }
                else
                {
                    object o = Convert.ChangeType(this.dgv[1, i].Value, MyTypeConvert.GetType(this.mComm_Element.Type));
                    bool flag2 = this.mComm_Element.SettingValues.Contains(o);
                    if (!flag2)
                    {
                        this.mComm_Element.SettingValues.Add(o);
                        i++;
                        continue;
                    }
                    MessageBox.Show(@"有重复内容,请检查后再保存！");
                    this.mComm_Element.SettingValues.Clear();
                }
                return;
            }
            base.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void tsBtn_NewLine_In_Click(object sender, EventArgs e)
        {
            this.dgv.Rows.Add();
            int count = this.dgv.Rows.Count;
            this.dgv[0, count - 1].Value = this.mComm_Element.Type;
        }

        private void tsBtn_DeleteLine_In_Click(object sender, EventArgs e)
        {
            int index = this.dgv.CurrentCell.RowIndex;
            this.dgv.Rows.RemoveAt(index);
        }
    }
}
