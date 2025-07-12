using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NovaVision.BaseClass.Algorithm;
using NovaVision.BaseClass.Collection;
using NovaVision.BaseClass.EnumHelper;
using NovaVision.BaseClass.Helper;
using NovaVision.WorkFlow;
using JobData = NovaVision.BaseClass.VisionConfig.JobData;

namespace NovaVision.VisionForm.StationFrm;

public partial class FrmModuleConfigDgv : Form
{
  private readonly JobData mJobData;
  private readonly EnumStation mStationName;
  private readonly MyDictionaryEx<AlgorithmParameter> mDicAlg = new ();

    public FrmModuleConfigDgv(EnumStation stationName, TaskFlow taskFlow, JobData job)
    {
        InitializeComponent();
        mJobData = job;
        mStationName = stationName;
        if (mJobData.mAlgorithmParameter.ContainsKey(mStationName.GetDescription()))
        {
            foreach (var key in mJobData.mAlgorithmParameter[mStationName.GetDescription()].GetKeys())
            {
                mDicAlg.Add(key, mJobData.mAlgorithmParameter[mStationName.GetDescription()][key]);
            }
        }
        else
        {
            mJobData.mAlgorithmParameter.Add(mStationName.GetDescription(),new MyDictionaryEx<AlgorithmParameter>());
        }
        TableParam table = new TableParam
        {
            ColName = new List<string> { "Name", "LimitMinValue", "LimitMaxValue", "IsEnable" },
            HeaderTexts = new List<string> { "名称", "下限", "上限", "是否启用" }
        };
        InitDgv(dgv_Config, table);
        RefreshDataGridView();
        dgv_Config.ShowCellToolTips = true;
    }

  private void InitDgv(DataGridView dgv, TableParam table)
  {
    dgv.AllowUserToAddRows = false;
    dgv.AllowUserToOrderColumns = false;
    dgv.RowHeadersVisible = false;
    dgv.Columns.Add(table.ColName[0], table.HeaderTexts[0]);

    dgv.Columns.Add(table.ColName[1], table.HeaderTexts[1]);
    dgv.Columns.Add(table.ColName[2], table.HeaderTexts[2]);
    DataGridViewCheckBoxColumn cb = new DataGridViewCheckBoxColumn
    {
      Name = table.ColName[3],
      HeaderText = table.HeaderTexts[3],
    };
    dgv.Columns.Add(cb);

    dgv.Columns[0].Width = 200;
    dgv.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
    dgv.Columns[1].Width = 60;
    dgv.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
    dgv.Columns[2].Width = 60;
    dgv.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
    dgv.Columns[3].Width = 60;
    dgv.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
  }

  private void RefreshDataGridView()
  {
    dgv_Config.Rows.Clear(); // 清空现有行
    foreach (var param in mDicAlg.GetValues())
    {
      if (param != null)
      {
        int rowIndex = dgv_Config.Rows.Add();
        DataGridViewRow row = dgv_Config.Rows[rowIndex];
        row.Cells["Name"].Value = param.Name;
        row.Cells["LimitMaxValue"].Value = param.LimitMaxValue;
        row.Cells["LimitMinValue"].Value = param.LimitMinValue;
        row.Cells["IsEnable"].Value = param.IsEnable;
      }
    }
  }

  // 添加单行
  private void AddSingleRow()
  {
    try
    {
      // 获取 DataGridView 中所有现有名称
      var existingNames = new List<string>();
      foreach (DataGridViewRow item in dgv_Config.Rows)
      {
        if (!item.IsNewRow && item.Cells["Name"].Value != null)
        {
          existingNames.Add(item.Cells["Name"].Value.ToString());
        }
      }

      // 生成下一个默认名称
      string newName = GetNextDefaultName(existingNames);
      // 创建参数对象（直接使用枚举值）
      var param = new AlgorithmParameter
      {
        Name = newName,
        LimitMaxValue = 0.0,
        LimitMinValue = 0.0,
        IsEnable = true
      };

      // 添加行到 DataGridView
      int rowIndex = dgv_Config.Rows.Add();
      DataGridViewRow row = dgv_Config.Rows[rowIndex];
      row.Cells["Name"].Value = newName;
      row.Cells["LimitMaxValue"].Value = param.LimitMaxValue;
      row.Cells["LimitMinValue"].Value = param.LimitMinValue;
      row.Cells["IsEnable"].Value = param.IsEnable;
      mDicAlg.Add(newName, param);
    }
    catch (Exception e)
    {
      MessageBox.Show("添加行失败," + e, "", MessageBoxButtons.OK);
    }

  }

  private string GetNextDefaultName(IEnumerable<string> existingNames)
  {
    // 提取所有已使用的数字
    var usedNumbers = new HashSet<int>();
    foreach (var name in existingNames)
    {
      if (name.StartsWith("检测") && name.Length > 2)
      {
        string numberPart = name.Substring(2);
        if (int.TryParse(numberPart, out int number))
        {
          usedNumbers.Add(number);
        }
      }
    }

    // 查找最小的未使用数字
    int nextNumber = 1;
    while (usedNumbers.Contains(nextNumber))
    {
      nextNumber++;
    }

    return $"检测{nextNumber}";
  }



  private void tsbtn_Add_Click(object sender, System.EventArgs e)
  {
    AddSingleRow();
  }

  private void tsbtn_Delete_Click(object sender, System.EventArgs e)
  {
    if (dgv_Config.CurrentRow == null || dgv_Config.CurrentRow.IsNewRow)
    {
      MessageBox.Show("请选择要删除的行！");
      return;
    }

    // 获取选中行的 Name
    string name = dgv_Config.CurrentRow.Cells["Name"].Value?.ToString();

    if (string.IsNullOrEmpty(name))
    {
      MessageBox.Show("无效的行数据！");
      return;
    }

    // 确认删除
    DialogResult result = MessageBox.Show(
      "确定要删除选中的行吗？",
      "删除确认",
      MessageBoxButtons.YesNo,
      MessageBoxIcon.Warning
    );

    if (result == DialogResult.Yes)
    {
      mDicAlg.Remove(name);
      // 从 DataGridView 删除行
      dgv_Config.Rows.Remove(dgv_Config.CurrentRow);
    }
    else
    {
      MessageBox.Show("删除行失败，算法配置中不存在对应名称", "", MessageBoxButtons.OK);
    }
  }


  private void dgv_Config_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
  {
    DataGridViewRow row = dgv_Config.Rows[e.RowIndex];
    if (e.ColumnIndex == dgv_Config.Columns["Name"]!.Index)
    {
      // 保存原始名称到 Tag 属性
      row.Cells["Name"].Tag = row.Cells["Name"].Value?.ToString();
    }
    else if (dgv_Config.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
    {
      row.Cells["Type"].Tag = dgv_Config.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
    }
  }

  private void dgv_Config_CellEndEdit(object sender, DataGridViewCellEventArgs e)
  {
    DataGridViewRow row = dgv_Config.Rows[e.RowIndex];
    if (e.ColumnIndex == dgv_Config.Columns["Name"]!.Index)
    {
      ChangeName(row);
    }
  }

  private void ChangeName(DataGridViewRow row)
  {
    
    string newName = row.Cells["Name"].Value?.ToString().Trim();
    string oldName = row.Cells["Name"].Tag?.ToString();

    // --------------- 验证逻辑 ---------------
    // 检查名称是否为空
    if (string.IsNullOrWhiteSpace(newName))
    {
      MessageBox.Show("名称不能为空！");
      row.Cells["Name"].Value = oldName; // 恢复原值
      return;
    }

    if (newName == oldName)
    {
      return;
    }

    // 收集所有现有名称（字典键 + DataGridView 其他行名称）
    var existingNames = new HashSet<string>(mJobData.mAlgorithmParameter.GetKeys());

    // 检查唯一性
    if (existingNames.Contains(newName))
    {
      MessageBox.Show("已存在相同名称的算法配置！");
      row.Cells["Name"].Value = oldName; // 恢复原值
      return;
    }
    var alg = mDicAlg[oldName];
    alg.Name = newName;
    mDicAlg.Add(newName, alg);
    mDicAlg.Remove(oldName);
    row.Cells["Name"].Value = newName;
  }


  private void btn_Save_Click(object sender, EventArgs e)
  {
    try
    {
      if(DialogResult.Yes != MessageBox.Show("是否保存修改？","",MessageBoxButtons.YesNoCancel))
      {
        return;
      }
      foreach (DataGridViewRow row in dgv_Config.Rows)
      {
        var key = row.Cells["Name"].Value?.ToString();
        if (mDicAlg.ContainsKey(key))
        {
          mDicAlg[key].LimitMaxValue = double.Parse(row.Cells["LimitMaxValue"].Value?.ToString().Trim() ?? string.Empty);
          mDicAlg[key].LimitMinValue = double.Parse(row.Cells["LimitMinValue"].Value?.ToString().Trim() ?? string.Empty);
          mDicAlg[key].IsEnable = (bool) row.Cells["IsEnable"].Value;
        }
      }

      mJobData.mAlgorithmParameter[mStationName.GetDescription()] = mDicAlg;
      XmlHelper.WriteXML(mJobData.mAlgorithmParameter, mJobData.AlgorithmParameterPath, typeof(MyDictionaryEx<MyDictionaryEx<AlgorithmParameter>>));
      MessageBox.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
    catch
    {
      MessageBox.Show($"保存失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
  }
  private void dgv_Config_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Delete)
    {
      tsbtn_Delete_Click(sender, e);
    }

  }

}
