using System;
using System.Drawing;
using System.Windows.Forms;

namespace NovaVision.UserControlLibrary.CommCtrl
{
    public partial class CommCtrl : UserControl
    {
        public CommCtrl()
        {
            InitializeComponent();
            Txt_Rename = new TextBox();
        }

        private void ChangePosition(Control ctrl, Control ctrl_Change)
        {
            ctrl_Change.Location = new Point(ctrl.Location.X, ctrl.Location.Y + ctrl.Height);
        }

        private void tsBtn_Record_Click(object sender, EventArgs e)
        {
            if (this.BtnRecordClick != null)
            {
                this.BtnRecordClick(this, e);
            }
        }

        private void tsBtn_Add_Click(object sender, EventArgs e)
        {
            if (this.BtnAddClick != null)
            {
                this.BtnAddClick(this, e);
            }
        }

        private void tsBtn_Delete_Click(object sender, EventArgs e)
        {
            if (this.BtnDeleteClick != null)
            {
                this.BtnDeleteClick(this, e);
            }
        }

        private void tsBtn_Up_Click(object sender, EventArgs e)
        {
            if (this.BtnMoveUpClick != null)
            {
                this.BtnMoveUpClick(this, e);
            }
        }

        private void tsBtn_Down_Click(object sender, EventArgs e)
        {
            if (this.BtnMoveDownClick != null)
            {
                this.BtnMoveDownClick(this, e);
            }
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (this.BtnSaveClick != null)
            {
                this.BtnSaveClick(this, e);
            }
        }

        private void listBox_Names_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectIndexChanged != null)
            {
                this.SelectIndexChanged(this, e);
            }
        }

        private void tsBtn_NewLine_In_Click(object sender, EventArgs e)
        {
            if (this.BtnNewLineIn_Click != null)
            {
                this.BtnNewLineIn_Click(sender, e);
            }
        }

        private void tsBtn_DeleteLine_In_Click(object sender, EventArgs e)
        {
            if (this.BtnDeleteLineIn_Click != null)
            {
                this.BtnDeleteLineIn_Click(sender, e);
            }
        }

        private void tsBtn_NewLine_Out_Click(object sender, EventArgs e)
        {
            if (this.BtnNewLineOut_Click != null)
            {
                this.BtnNewLineOut_Click(sender, e);
            }
        }

        private void tsBtn_DeleteLine_Out_Click(object sender, EventArgs e)
        {
            if (this.BtnDeleteLineOut_Click != null)
            {
                this.BtnDeleteLineOut_Click(sender, e);
            }
        }

        private void tsBtn_Up_In_Click(object sender, EventArgs e)
        {
            if (this.BtnUpLineIn_Click != null)
            {
                this.BtnUpLineIn_Click(sender, e);
            }
        }

        private void tsBtn_Down_In_Click(object sender, EventArgs e)
        {
            if (this.BtnDownLineIn_Click != null)
            {
                this.BtnDownLineIn_Click(sender, e);
            }
        }

        private void tsBtn_Up_Out_Click(object sender, EventArgs e)
        {
            if (this.BtnUpLineOut_Click != null)
            {
                this.BtnUpLineOut_Click(sender, e);
            }
        }

        private void tsBtn_Down_Out_Click(object sender, EventArgs e)
        {
            if (this.BtnDownLineOut_Click != null)
            {
                this.BtnDownLineOut_Click(sender, e);
            }
        }

        private void ConfigCtrl_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ConfigCtrl.ListBoxNames.SelectedIndex >= 0 && !ConfigCtrl.ListBoxNames.Controls.ContainsKey("textBox_Rename"))
            {
                Txt_Rename.Name = "textBox_Rename";
                Txt_Rename.Visible = true;
                ConfigCtrl.ListBoxNames.Controls.Add(Txt_Rename);
                Rectangle vRectangle = ConfigCtrl.ListBoxNames.GetItemRectangle(ConfigCtrl.ListBoxNames.SelectedIndex);
                Txt_Rename.BorderStyle = BorderStyle.FixedSingle;
                Txt_Rename.Text = ConfigCtrl.ListBoxNames.Items[ConfigCtrl.ListBoxNames.SelectedIndex].ToString();
                Txt_Rename.BringToFront();
                Txt_Rename.Bounds = vRectangle;
                Txt_Rename.Focus();
                Txt_Rename.KeyPress += TxtRename_KeyPress;
                Txt_Rename.LostFocus += Txt_Rename_LostFocus;
            }
        }

        private void Txt_Rename_LostFocus(object sender, EventArgs e)
        {
            TextBox textBox1 = ConfigCtrl.ListBoxNames.Controls["textBox_Rename"] as TextBox;
            textBox1.KeyPress -= TxtRename_KeyPress;
            textBox1.LostFocus -= Txt_Rename_LostFocus;
            ConfigCtrl.ListBoxNames.Controls.RemoveByKey("textBox_Rename");
        }

        private void TxtRename_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox1 = ConfigCtrl.ListBoxNames.Controls["textBox_Rename"] as TextBox;
            if (e.KeyChar == '\r')
            {
                textBox1.LostFocus -= Txt_Rename_LostFocus;
                if (this.ToolStripMenuItem_Click != null)
                {
                    this.ToolStripMenuItem_Click(textBox1, e);
                }
                textBox1.KeyPress -= TxtRename_KeyPress;
                ConfigCtrl.ListBoxNames.Controls.RemoveByKey("textBox_Rename");
            }
        }
    }
}
