using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using NovaVision.BaseClass.EnumHelper;

namespace NovaVision.BaseClass.Algorithm;

[Serializable]
[XmlRoot("AlgorithmParameter")]
public class AlgorithmParameter
{
  public string Name { get; set; }
  public double LimitMaxValue { get; set; }
  public double LimitMinValue { get; set; }
  public bool IsEnable { get; set; }
}