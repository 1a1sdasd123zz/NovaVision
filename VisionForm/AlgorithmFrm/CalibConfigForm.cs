using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using NovaVision.BaseClass;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.Module;
using NovaVision.BaseClass.VisionConfig;

namespace NovaVision.VisionForm.AlgorithmFrm
{
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public partial class CalibConfigForm : Form
    {
        private readonly JobData _mJobData;

        private object _mDgvChangingObject;
        private readonly string _mPath;

        private readonly List<string> _mDeleteKeys = [];

        private readonly MyDictionaryEx<CalibToolInfo> _mCalibTools = new();
        private readonly MyDictionaryEx<CalibInfo> _mCalibParams = new();

        private readonly string[] _mColumnNames = ["Setting", "Save"];

        public CalibConfigForm(JobData jobData)
        {
            _mJobData = jobData;
            _mPath = _mJobData.mSystemConfigData.CalibMoudlePath;
            DictionaryCopy(_mJobData.mCalibTools, _mCalibTools);
            DictionaryCopy(_mJobData.mCalibParams, _mCalibParams);
            InitializeComponent();
        }
        private void 九点标定_Load(object sender, EventArgs e)
        {
            InitDgv();
            UpdateDgv();

        }
        private void InitDgv()
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;

            // 添加名称列
            dgv.Columns.Add("Name", "名称");

            // 添加按钮列数组
            DataGridViewColumn[] btns = [new DataGridViewButtonColumn(), new DataGridViewButtonColumn()];
            dgv.Columns.AddRange(btns);

            // 设置各按钮列的HeaderText和Name属性
            string[] headerTexts = ["标定流程配置", "保存"];

            for (int i = 0; i < headerTexts.Length; i++)
            {
                dgv.Columns[i + 1].HeaderText = headerTexts[i];
                dgv.Columns[i + 1].Name = _mColumnNames[i];
            }

            // 添加注释列
            dgv.Columns.Add("Explain", "注释");

            // 设置各列通用属性（宽度、排序模式），除了第0列单独设置只读属性
            int[] widths = [200, 150, 100, 400];
            DataGridViewColumnSortMode sortMode = DataGridViewColumnSortMode.NotSortable;
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].Width = widths[i];
                dgv.Columns[i].SortMode = sortMode;
                if (i != 0)
                {
                    dgv.Columns[i].ReadOnly = false;
                }
            }

            // 创建并应用列标题样式，使标题居中显示
            var headerCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            for (var i = 0; i < dgv.Columns.Count; i++)
            {
                dgv.Columns[i].HeaderCell.Style = headerCellStyle;
            }
        }
        private void UpdateDgv()
        {
            (this._mCalibParams.GetKeys()?.ToList() ?? []).ForEach(key =>
            {
                try
                {
                    AddNewRow(key, this._mCalibParams[key].Explain);
                }
                catch (IndexOutOfRangeException ex)
                {
                    LogUtil.LogError($"标定配置键{key}不存在，异常信息: {ex}");
                }
            });
        }

        private void AddNewRow(string name, string explain = "")
        {
            try
            {
                // 创建按钮列数据并直接初始化赋值
                DataGridViewCell[] value =
                [
                    new DataGridViewButtonCell { Value = "打开标定流程配置界面" },
                    new DataGridViewButtonCell { Value = "保存标定文件" }
                ];

                // 创建一个新行
                var newRow = new DataGridViewRow();
                // 为第一列（索引为0）添加文本数据，使用当前时间生成唯一名称
                newRow.Cells.Add(new DataGridViewTextBoxCell
                {
                    Value = name
                });
                // 为第二列（索引为1）添加按钮列数据
                newRow.Cells.AddRange(value);
                // 添加最后一列（索引为4）文本数据并初始化为空
                newRow.Cells.Add(new DataGridViewTextBoxCell { Value = explain });

                // 将新行添加到DataGridView
                dgv.Rows.Add(newRow);
            }
            catch (Exception ex)
            {
                LogUtil.LogError("标定配置像素坐标dgv添加行异常!" + ex);
                MessageBox.Show(@"添加一行异常", "", MessageBoxButtons.OK);
            }
        }
        private void OpenCalibSettingFrm(string key)
        {
            try
            {
                if (!this._mCalibTools.ContainsKey(key)) return;
                var calibSettingFrm = new CalibProcessForm(this._mJobData, this._mCalibTools[key], key);
                calibSettingFrm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"打开标定配置窗口失败!{ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void SaveCalibToFile(string key)
        {
            try
            {
                if (_mCalibTools.ContainsKey(key))
                {
                    string path = _mPath + key + "\\";
                    _mCalibTools[key].SaveTool(path);
                    _mJobData.mCalibTools.TryAdd(key, _mCalibTools[key]);
                    _mJobData.mCalibParams.TryAdd(key, _mCalibParams[key]);
                    MessageBox.Show(@"保存标定文件成功", "", MessageBoxButtons.OK);
                    LogUtil.Log($"保存{key}标定文件成功");
                }
                else
                {
                    MessageBox.Show(@"保存标定文件失败", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    LogUtil.LogError($"标定工具字典不存在键值为{key}的对象");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"保存标定文件失败!{ex.Message}", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogUtil.LogError("保存标定文件失败!" + ex);
            }
        }

        //删除文件夹
        private void DeleteFolder()
        {
            try
            {
                foreach (var item in _mDeleteKeys)
                {
                    string path = _mPath + item + "\\";
                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                }

            }
            catch (Exception ex)
            {
                LogUtil.LogError("删除文件夹失败!" + ex);
            }
        }

        private void DictionaryCopy<T>(MyDictionaryEx<T> source, MyDictionaryEx<T> newDic)
        {
            foreach (var item in source.GetKeys())
            {
                newDic.Add(item, source[item]);
            }
        }

        private void tsBtn_Add_Click(object sender, EventArgs e)
        {
            string name = $"标定{DateTime.Now.ToString("yyMMddHHmmssff")}";
            if (!this._mCalibParams.TryAdd(name, new CalibInfo()) || !this._mCalibTools.TryAdd(name, new CalibToolInfo(_mPath + "标定流程.vpp")))
            {
                MessageBox.Show(@"已存在相同配置名，添加失败!", "", MessageBoxButtons.OK);
                return;
            }
            AddNewRow(name);
        }
        private void tsBtn_Delete_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count <= 0)
            {
                return;
            }
            try
            {
                string key = dgv.Rows[dgv.CurrentCell.RowIndex].Cells[0].Value.ToString();
                if (this._mCalibParams.ContainsKey(key))
                {
                    this._mCalibParams.Remove(key);
                }
                dgv.Rows.RemoveAt(dgv.CurrentCell.RowIndex);
                _mDeleteKeys.Add(key);
            }
            catch { MessageBox.Show(@"移除数据异常!", "", MessageBoxButtons.OK); }
        }
        private void tsBtn_Up_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count <= 0)
            {
                return;
            }

            int selectedRowIndex = dgv.CurrentCell.RowIndex;
            if (selectedRowIndex > 0)
            {
                // 交换选中行和上一行的数据
                DataGridViewRow selectedRow = dgv.Rows[selectedRowIndex];
                DataGridViewRow aboveRow = dgv.Rows[selectedRowIndex - 1];
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    object temp = selectedRow.Cells[i].Value;
                    selectedRow.Cells[i].Value = aboveRow.Cells[i].Value;
                    aboveRow.Cells[i].Value = temp;
                }
                // 重新选中交换后的行
                dgv.CurrentCell = dgv.Rows[selectedRowIndex - 1].Cells[0];

            }
        }
        private void tsBtn_Down_Click(object sender, EventArgs e)
        {
            if (dgv.Rows.Count <= 0)
            {
                return;
            }

            int selectedRowIndex = dgv.CurrentCell.RowIndex;
            if (selectedRowIndex < dgv.Rows.Count - 1)
            {
                // 交换选中行和下一行的数据
                DataGridViewRow selectedRow = dgv.Rows[selectedRowIndex];
                DataGridViewRow belowRow = dgv.Rows[selectedRowIndex + 1];
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    object temp = selectedRow.Cells[i].Value;
                    selectedRow.Cells[i].Value = belowRow.Cells[i].Value;
                    belowRow.Cells[i].Value = temp;
                }
                // 重新选中交换后的行
                dgv.CurrentCell = dgv.Rows[selectedRowIndex + 1].Cells[0];
            }
        }

        private void btn_SaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                _mJobData.mCalibTools.Clear();
                _mJobData.mCalibParams.Clear();
                DictionaryCopy(_mCalibTools,_mJobData.mCalibTools);
                DictionaryCopy(_mCalibParams, _mJobData.mCalibParams);
                XmlHelp.WriteXML(_mJobData.mCalibParams, _mJobData.CalibParamsFilePath, typeof(MyDictionaryEx<CalibInfo>));
                DeleteFolder();
                MessageBox.Show(@"保存配置文件成功", "", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"保存配置文件成功!", "", MessageBoxButtons.OK);
                LogUtil.Log("保存配置文件成功!" + ex);
            }
        }

        private void dgv_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            var dgv1 = sender as DataGridView;
            var column = dgv1?.CurrentCell.OwningColumn;
            if (column is DataGridViewTextBoxColumn)
            {
                _mDgvChangingObject = dgv1.CurrentCell.Value;
            }
        }
        private void dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            using var gridView = sender as DataGridView;
            if (gridView == null) throw new ArgumentNullException(nameof(gridView));
            var column = gridView.CurrentCell.OwningColumn;
            if (column is not DataGridViewTextBoxColumn) return;
            var cur = gridView.CurrentCell.Value;
            if (cur == _mDgvChangingObject)
            {
                return;
            }

            var str = "";
            if (cur != null)
            {
                str = cur.ToString();
            }

            switch (column.Name)
            {
                case "Name":
                    {
                        if (cur == null)
                        {
                            gridView.CurrentCell.Value = _mDgvChangingObject;
                            MessageBox.Show(@"名称不能为空,请重新输入", "", MessageBoxButtons.OK);
                            return;
                        }

                        this._mCalibParams.Replace(_mDgvChangingObject.ToString(), str);
                        break;
                    }
                case "Explain":
                    {
                        var row = gridView.CurrentCell.RowIndex;
                        var key = gridView.Rows[row].Cells[0].Value.ToString();
                        if (this._mCalibParams.ContainsKey(key))
                        {
                            this._mCalibParams[key].Explain = str;
                        }
                        else
                        {
                            MessageBox.Show(@"字典中不存在修改的行，请检查配置文件!");
                        }

                        break;
                    }
            }
        }
        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv1 = sender as DataGridView;
            if (dgv1 == null || e.ColumnIndex < 0 || e.RowIndex < 0) return;
            var column = dgv1.Columns[e.ColumnIndex];
            if (column is not DataGridViewButtonColumn btnColumn) return;
            var row = dgv1.Rows[e.RowIndex];
            var key = row.Cells[0].Value.ToString();

            var btnColumnName = btnColumn.Name;
            switch (btnColumnName)
            {
                case "Setting":
                    {
                        OpenCalibSettingFrm(key);
                        break;
                    }
                case "Save":
                    {
                        SaveCalibToFile(key);
                        break;
                    }
            }
        }
    }
}
