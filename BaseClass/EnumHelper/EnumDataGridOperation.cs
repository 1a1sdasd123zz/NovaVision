using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaVision.BaseClass.EnumHelper
{
  public enum EnumDataGridOperation
  {
    [Description("添加")]
    添加,
    [Description("删除")]
    删除,
    [Description("修改名称")]
    修改名称,
    [Description("修改算法类型")]
    修改算法类型,
    [Description("修改上限")]
    修改上限,
    [Description("修改下限")]
    修改下限,
    [Description("启用")]
    启用,
    [Description("修改注释")]
    修改注释
  }
}
