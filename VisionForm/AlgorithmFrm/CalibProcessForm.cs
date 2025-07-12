using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Cognex.VisionPro;
using Cognex.VisionPro.ImageProcessing;
using Cognex.VisionPro.ToolBlock;
using HslCommunication.BasicFramework;
using NovaVision.BaseClass;
using NovaVision.BaseClass.VisionConfig;
using NovaVision.Hardware;
using Excel = Microsoft.Office.Interop.Excel;
using ICogGraphic = Cognex.VisionPro.ICogGraphic;
using ICogImage = Cognex.VisionPro.ICogImage;

namespace NovaVision.VisionForm.AlgorithmFrm
{
    public partial class CalibProcessForm : Form
    {
        private readonly string _mKey;
        private string _mCameraName;
        private readonly JobData _mJobData;
        private CalibToolInfo _mTools;
        private static int _mRowIndex;
        private ICogImage _mCogImage;

        private double[,] _mPhysicalArray;


        private List<CogCircle> _mCogCircles;

        public CalibProcessForm(JobData mJobData, CalibToolInfo tools, string key)
        {
            InitializeComponent();
            _mKey = key;
            this._mJobData = mJobData;
            _mTools = tools;
            this.AddSn();
        }

        private void CalibProcessForm_Load(object sender, System.EventArgs e)
        {


            InitCtrol();
            InitDgv();


        }

        private void AddSn()
        {
            this.cmb_CameraName.Items.Clear();
            if (_mJobData.CamConfigNames == null) return;
            var items = cmb_CameraName.Items;
            object[] items2 = _mJobData.CamConfigNames.ToArray();
            items.AddRange(items2);
            this.cmb_CameraName.SelectedItem = _mJobData.CamConfigNames.Contains(_mKey) ? _mKey : items[0];
        }

        private void InitCtrol()
        {
            cb_IsUseCheckboard.Checked = _mTools.IsUseCalibCheckborad;
            cogToolBlockEditV21.Subject = _mTools.mCalibToolBlock;
            cogCalibCheckerboardEditV21.Subject = _mTools.mCheckboardTool;
            cogCalibNPointToNPointEditV21.Subject = _mTools.mNPointToNPointTool;
            cogRecordDisplay1.Show();
            cogToolBlockEditV21.Hide();
            cogCalibCheckerboardEditV21.Hide();
            cogCalibNPointToNPointEditV21.Hide();
            gb_ToolShowName.Text = "图像显示";
        }

        private void InitDgv()
        {
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToOrderColumns = false;
            dgv.RowHeadersVisible = false;

            // 添加名称列
            dgv.Columns.Add("Name", "序号");
            // 添加名称列
            dgv.Columns.Add("PosX", "X");
            // 添加名称列
            dgv.Columns.Add("PosY", "Y");


            // 设置各列通用属性（宽度、排序模式），除了第0列单独设置只读属性
            int[] widths = [100, 100, 100];
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

            dgv.Columns[0].Frozen = true;

            // 初始化时禁用清除数据按钮，因为还没有加载数据
            btnClearData.Enabled = false;
            dataGridView.AllowUserToAddRows = false;
        }

        private void UpdateDgvPixel()
        {
            try
            {

            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void SelectDgvCell(double x, double y)
        {

        }

        private void CameraSoftOnce()
        {
            try
            {
                string SN = _mJobData.mCameraData[_mCameraName].CamSN;
                CameraOperator.camera2DCollection[SN].UpdateImage = UpdateUiImage;

                CameraOperator.camera2DCollection[SN].SetCameraSetting(_mJobData.mCameraData[_mCameraName]);
                if (_mJobData.mCameraData[_mCameraName].SettingParams.TriggerMode == 0)
                {
                    CameraOperator.camera2DCollection[SN].SoftwareTriggerOnce();
                }
            }
            catch { LogUtil.LogError("执行拍照流程异常!"); }
        }

        private void UpdateUiImage(ImageData imageData)
        {
            _mCogImage = imageData.CogImage;
            Invoke(new Action(() =>
                {
                    InitCtrol();
                    this.cogRecordDisplay1.Image = _mCogImage;
                    this.cogRecordDisplay1.Fit(true);
                }
            ));
        }

        private void ModifyTools()
        {
            _mTools.mCalibToolBlock = this.cogToolBlockEditV21.Subject;
            _mTools.mCheckboardTool = this.cogCalibCheckerboardEditV21.Subject;
            _mTools.mNPointToNPointTool = this.cogCalibNPointToNPointEditV21.Subject;
        }


        private void UpdateDataArray()
        {
            try
            {
                var targetColumn1 = dataGridView.Columns
                    .Cast<DataGridViewColumn>()
                    .FirstOrDefault(column => column.Name.ToLower() == "x" || column.HeaderText.ToLower() == "x");
                var targetColumn2 = dataGridView.Columns
                    .Cast<DataGridViewColumn>()
                    .FirstOrDefault(column => column.Name.ToLower() == "y" || column.HeaderText.ToLower() == "y");
                if (targetColumn1 != null && targetColumn2 != null)
                {
                    const int col = 2;
                    _mPhysicalArray = new double[dataGridView.RowCount, col];
                    // 用于存储获取到的列数据
                    for (var colIndex = 0; colIndex < col; colIndex++)
                    {
                        for (var rowIndex = 0; rowIndex < dataGridView.Rows.Count; rowIndex++)
                        {
                            // 获取当前行中目标列的单元格值
                            var value = dataGridView.Rows[rowIndex].Cells[targetColumn1.Index].Value;
                            if (value != null)
                            {
                                var str = value.ToString();
                                if (double.TryParse(str, out var dbValue))
                                {
                                    _mPhysicalArray[rowIndex, colIndex] = dbValue;
                                }
                                else
                                {
                                    MessageBox.Show(@"数据格式错误,请检查表格数据!", "", MessageBoxButtons.OK);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show(@"坐标存在空值，请检查表格数据!", "", MessageBoxButtons.OK);
                                return;
                            }
                        }
                    }
                }
                else
                {
                    LogUtil.LogError("未找到指定X、Y坐标列");
                }
            }
            catch { MessageBox.Show(@"提取数据时出现异常!", "", MessageBoxButtons.OK); }
        }


        private void btn_SoftOnce_Click(object sender, EventArgs e)
        {
            CameraSoftOnce();
        }

        private void cmb_CameraName_SelectedValueChanged(object sender, EventArgs e)
        {
            _mCameraName = this.cmb_CameraName.SelectedItem.ToString();
            LogUtil.Log($"标定流程配置{_mKey},切换到相机名称：" + _mCameraName);
        }

        private void btn_OpenCheckboardTool_Click(object sender, EventArgs e)
        {
            if (cogCalibCheckerboardEditV21.Visible)
            {
                this.gb_ToolShowName.Text = "图像显示";
                this.cogCalibCheckerboardEditV21.Hide();
                this.cogRecordDisplay1.Show();
            }
            else
            {
                this.gb_ToolShowName.Text = "棋盘格标定工具";
                this.cogCalibCheckerboardEditV21.Show();
                this.cogRecordDisplay1.Hide();
            }
        }

        private void btn_OpenNpointTool_Click(object sender, EventArgs e)
        {
            if (cogCalibNPointToNPointEditV21.Visible)
            {
                this.gb_ToolShowName.Text = "图像显示";
                this.cogCalibNPointToNPointEditV21.Hide();
                this.cogRecordDisplay1.Show();
            }
            else
            {
                this.gb_ToolShowName.Text = "九点标定工具";
                this.cogCalibNPointToNPointEditV21.Show();
                this.cogRecordDisplay1.Hide();
            }

        }

        private void btn_OpenProcess_Click(object sender, EventArgs e)
        {
            if (this.cogToolBlockEditV21.Visible)
            {
                this.gb_ToolShowName.Text = "图像显示";
                this.cogToolBlockEditV21.Hide();
                this.cogRecordDisplay1.Show();
            }
            else
            {
                this.gb_ToolShowName.Text = "九点标定流程工具";
                this.cogToolBlockEditV21.Show();
                this.cogRecordDisplay1.Hide();
            }
        }

        private void btn_OpenDisplayShow_Click(object sender, EventArgs e)
        {
            this.gb_ToolShowName.Text = "图像显示";
            InitCtrol();
        }

        private void cb_IsUseCheckboard_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_IsUseCheckboard.Checked)
            {
                _mTools.IsUseCalibCheckborad = true;
            }
            else
            {
                _mTools.IsUseCalibCheckborad = false;
            }
        }

        private void CalibProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("是否关闭界面？未应用的修改将不会更新", "提示", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btn_RunProcess_Click(object sender, EventArgs e)
        {
            if (_mCogImage != null)
            {
                try
                {
                    InitCtrol();
                    CogImage8Grey img = new CogImage8Grey();
                    if (_mCogImage is CogImage24PlanarColor)
                    {
                        CogImageConvertTool tool = new CogImageConvertTool();
                        tool.InputImage = _mCogImage;
                        tool.Run();
                        img = (CogImage8Grey)tool.OutputImage;
                    }
                    else if (_mCogImage is CogImage8Grey)
                    {
                        img = (CogImage8Grey)_mCogImage;
                    }

                    if (this.cb_IsUseCheckboard.Checked)
                    {
                        this._mTools.mCheckboardTool.InputImage = img;
                        this._mTools.mCheckboardTool.Run();
                        if (this._mTools.mCheckboardTool.RunStatus.Result == CogToolResultConstants.Accept)
                        {
                            img = (CogImage8Grey)this._mTools.mCheckboardTool.OutputImage;
                        }
                    }

                    CogToolBlock tb = this.cogToolBlockEditV21.Subject;
                    tb.Inputs[0].Value = img;
                    tb.Run();
                    _mCogCircles = (List<CogCircle>)tb.Outputs[0].Value;

                    ICogRecord lastRecord = tb.CreateLastRunRecord().SubRecords.Count > 0
                        ? tb.CreateLastRunRecord().SubRecords[1]
                        : tb.CreateLastRunRecord();
                    CogGraphicCollection graphic = new CogGraphicCollection();
                    int i = 1;
                    dgv.Rows.Clear();
                    _mCogCircles.ForEach(item =>
                    {
                        dgv.Rows.Add(i++, Math.Round(item.CenterX, 3), Math.Round(item.CenterY, 3));
                        graphic.Add(item);
                    });
                    foreach (ICogGraphic g in graphic)
                    {
                        tb.AddGraphicToRunRecord(g, lastRecord, lastRecord.RecordKey, "");
                    }

                    this.cogRecordDisplay1.Record = lastRecord;
                    this.cogRecordDisplay1.Fit();

                }
                catch (Exception exception)
                {
                    LogUtil.LogError(exception.ToString());
                }
            }
            else
            {
                MessageBox.Show("图像为空，请先采集或加载本地图像!", "", MessageBoxButtons.OK);
            }
        }

        private void btnLoadExcel_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = @"Excel文件|*.xlsx;*.xls";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                    Excel.Application excelApp = new Excel.Application();
                    Excel.Workbook excelWorkbook = null;
                    Excel.Worksheet excelWorksheet = null;
                    Excel.Range excelRange = null;

                    try
                    {
                        excelWorkbook = excelApp.Workbooks.Open(filePath);
                        excelWorksheet = (Excel.Worksheet)excelWorkbook.Sheets[1];
                        excelRange = excelWorksheet.UsedRange;

                        // 将Excel数据复制到DataTable
                        DataTable dataTable = new DataTable();
                        for (int i = 1; i <= excelRange.Columns.Count; i++)
                        {
                            object cellValue = excelRange.Cells[1, i].Value;
                            string columnName = cellValue != null ? cellValue.ToString() : $"";
                            dataTable.Columns.Add(columnName);

                        }

                        // 添加数据行
                        for (int i = 2; i <= excelRange.Rows.Count; i++)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            for (int j = 1; j <= excelRange.Columns.Count; j++)
                            {
                                dataRow[j - 1] = excelRange.Cells[i, j].Value;
                            }
                            dataTable.Rows.Add(dataRow);
                        }

                        // 将DataTable数据绑定到DataGridView
                        dataGridView.DataSource = dataTable;
                        dataGridView.Columns[0].ReadOnly = true;
                        // 禁用DataGridView列排序
                        for (int i = 0; i < dataGridView.Columns.Count; i++)
                        {
                            dataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        }
                        btnClearData.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        LogUtil.LogError($"读取Excel文件出现异常: {ex.Message}");
                        MessageBox.Show($@"读取Excel文件出现异常: {ex.Message}");
                        throw; // 可以选择重新抛出异常，让上层调用者进一步处理
                    }
                    finally
                    {
                        // 释放资源，先判断对象是否不为null再进行释放操作，避免空引用异常
                        if (excelRange != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelRange);
                        }
                        if (excelWorksheet != null)
                        {
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorksheet);
                        }
                        if (excelWorkbook != null)
                        {
                            excelWorkbook.Close(false);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelWorkbook);
                        }

                        excelApp.Quit();
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

                        // 强制进行垃圾回收，虽然不一定能立即回收资源，但有助于尽快释放内存
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"操作出现错误: {ex.Message}");
            }
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            // 清除DataGridView中的数据
            dataGridView.DataSource = null;
            _mPhysicalArray = null;
            // 禁用清除数据按钮
            btnClearData.Enabled = false;
        }

        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            UpdateDataArray();
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            UpdateDataArray();
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否应用当前修改？(当前程序生效，长期保存请在配置界面保存到配置文件)", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ModifyTools();
            }
        }

        private void cogRecordDisplay1_Click(object sender, EventArgs e)
        {
            ICogGraphicInteractive graphic = cogRecordDisplay1.Selection[0];

            if (graphic is CogCircle)
            {
                try
                {
                    CogCircle circle = graphic as CogCircle;
                    foreach (DataGridViewRow row in dgv.Rows)
                    {
                        if (Math.Round(Convert.ToDouble(row.Cells[1].Value), 3) == Math.Round(circle.CenterX, 3) &&
                            Math.Round(Convert.ToDouble(row.Cells[2].Value), 3) == Math.Round(circle.CenterY, 3))
                        {
                            row.Selected = true;
                        }
                        else
                        {
                            row.Selected = false;
                        }
                    }
                }
                catch (Exception exception)
                {
                    throw exception; 
                }
            }
        }

    }
}
