using System;
using System.Drawing;
using System.Windows.Forms;

namespace NovaVision.UserControlLibrary
{
    public partial class ConfigCtrl : UserControl
    {

        public ConfigCtrl()
        {
            InitializeComponent();
            ToolStrip_Config.AutoSize = false;
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

        private void ConfigCtrl_Resize(object sender, EventArgs e)
        {
            if (base.Height - btn_Save.Height - ToolStrip_Config.Height >= 0)
            {
                ListBoxHeight = base.Height - btn_Save.Height - ToolStrip_Config.Height;
            }
        }

        private void ToolStripMenuItemRename_Click(object sender, EventArgs e)
        {
            if (this.ToolStripMenuItem_Click != null)
            {
                this.ToolStripMenuItem_Click(this, e);
            }
        }
    }
}
