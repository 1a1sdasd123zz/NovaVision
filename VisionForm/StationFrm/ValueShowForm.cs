using System;
using System.Collections;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Module;

namespace NovaVision.VisionForm.StationFrm
{
    public partial class ValueShowForm : Form
    {
        private ElementBase mElementBase;

        private string strType = "";

        private Type elementType;
        public ValueShowForm(ElementBase elementBase)
        {
            this.InitializeComponent();
            this.mElementBase = elementBase;
            this.Tip.Text = this.mElementBase.Name;
            this.elementType = MyTypeConvert.GetType(this.mElementBase.Type);
            this.strType = this.elementType.Name;
            //this.InitDgv(this.mElementBase);
        }

        //public ValueShowForm(string key, List<MesDataInfo> element)
        //{
        //    this.InitializeComponent();
        //    this.Tip.Text = key;
        //    this.InitDgv(element);
        //}

        //private void InitDgv(List<MesDataInfo> element)
        //{
        //    this.dgv.AllowUserToAddRows = false;
        //    this.dgv.AllowUserToOrderColumns = false;
        //    this.dgv.RowHeadersVisible = false;
        //    this.dgv.Columns.Add("Type", "类型");
        //    this.dgv.Columns.Add("Value", "值");
        //    this.dgv.Columns[0].ReadOnly = true;
        //    this.dgv.Columns[1].ReadOnly = true;
        //    this.dgv.Columns[1].ValueType = MyTypeConvert.GetType(element[0].Type);
        //    this.dgv.Rows.Clear();
        //    for (int i = 0; i < element.Count; i++)
        //    {
        //        this.dgv.Rows.Add();
        //        this.dgv[0, i].Value = element[i].Type;
        //        this.dgv[1, i].Value = element[i].Value.mValue;
        //    }
        //}

        private void InitDgv(ElementBase element)
        {
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToOrderColumns = false;
            this.dgv.RowHeadersVisible = false;
            this.dgv.Columns.Add("Type", "类型");
            this.dgv.Columns.Add("Value", "值");
            this.dgv.Columns[0].ReadOnly = true;
            this.dgv.Columns[1].ReadOnly = true;
            this.dgv.Columns[1].ValueType = this.elementType;
            this.dgv.Rows.Clear();
            bool flag = element.Value.mValue != null;
            if (flag)
            {
                IEnumerable items = element.Value.mValue as IEnumerable;
                int i = 0;
                foreach (object item in items)
                {
                    this.dgv.Rows.Add();
                    this.dgv[0, i].Value = this.strType;
                    this.dgv[1, i].Value = item;
                    i++;
                }
            }
        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}
