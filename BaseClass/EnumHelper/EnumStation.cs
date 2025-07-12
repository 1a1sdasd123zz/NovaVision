using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace NovaVision.BaseClass.EnumHelper;

public enum EnumStation
{
  [Description("正面线扫检测")]
  正面线扫检测 = 0,
  [Description("反面线扫检测")]
  反面线扫检测,
  [Description("飞拍相机1")]
  飞拍相机1,
  [Description("飞拍相机2")]
  飞拍相机2,
  [Description("飞拍相机3")]
  飞拍相机3,
  [Description("飞拍相机4")]
  飞拍相机4
}

