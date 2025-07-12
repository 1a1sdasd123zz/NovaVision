using System.Windows.Forms;

namespace NovaVision.VisionForm.LoginFrm
{
    partial class Frm_UserManage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgv_List = new System.Windows.Forms.DataGridView();
            this.btn_delete = new System.Windows.Forms.Button();
            this.btn_create = new System.Windows.Forms.Button();
            this.btn_Init = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)this.dgv_List).BeginInit();
            base.SuspendLayout();
            this.dgv_List.AllowUserToAddRows = false;
            this.dgv_List.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_List.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgv_List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 9f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_List.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgv_List.Location = new System.Drawing.Point(0, 1);
            this.dgv_List.Margin = new System.Windows.Forms.Padding(10);
            this.dgv_List.MultiSelect = false;
            this.dgv_List.Name = "dgv_List";
            this.dgv_List.ReadOnly = true;
            this.dgv_List.RowHeadersVisible = false;
            this.dgv_List.RowTemplate.Height = 23;
            this.dgv_List.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_List.ShowEditingIcon = false;
            this.dgv_List.Size = new System.Drawing.Size(444, 207);
            this.dgv_List.TabIndex = 0;
            this.btn_delete.Location = new System.Drawing.Point(356, 230);
            this.btn_delete.Name = "btn_delete";
            this.btn_delete.Size = new System.Drawing.Size(75, 23);
            this.btn_delete.TabIndex = 1;
            this.btn_delete.Text = "删除";
            this.btn_delete.UseVisualStyleBackColor = true;
            this.btn_delete.Click += new System.EventHandler(btn_delete_Click);
            this.btn_create.Location = new System.Drawing.Point(269, 230);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(75, 23);
            this.btn_create.TabIndex = 1;
            this.btn_create.Text = "注册";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(btn_create_Click);
            this.btn_Init.Location = new System.Drawing.Point(182, 230);
            this.btn_Init.Name = "btn_Init";
            this.btn_Init.Size = new System.Drawing.Size(75, 23);
            this.btn_Init.TabIndex = 1;
            this.btn_Init.Text = "初始化";
            this.btn_Init.UseVisualStyleBackColor = true;
            this.btn_Init.Click += new System.EventHandler(btn_Init_Click);
            base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new System.Drawing.Size(444, 265);
            base.Controls.Add(this.btn_Init);
            base.Controls.Add(this.btn_create);
            base.Controls.Add(this.btn_delete);
            base.Controls.Add(this.dgv_List);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "Frm_UserManage";
            base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "用户管理";
            base.FormClosed += new System.Windows.Forms.FormClosedEventHandler(Frm_UserManage_FormClosed);
            ((System.ComponentModel.ISupportInitialize)this.dgv_List).EndInit();
            base.ResumeLayout(false);
        }

        #endregion


        private DataGridView dgv_List;

        private Button btn_delete;

        private Button btn_create;

        private Button btn_Init;
    }
}