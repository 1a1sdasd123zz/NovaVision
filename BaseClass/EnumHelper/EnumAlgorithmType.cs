using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaVision.BaseClass.EnumHelper;

public enum EnumAlgorithmType
{
  [Description("斑点分析手动区域")]
  斑点分析手动区域,
  [Description("斑点分析自动区域")]
  斑点分析自动区域,
  [Description("宽度测量")]
  宽度测量,
  [Description("椭圆分析")]
  椭圆分析
}