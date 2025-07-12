using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaVision.VisionForm.StationFrm;

[Serializable]
public class ProductInfo
{
  public int Row { get; set; }
  public int Col { get; set; }
  public int FlyNum { get; set; }
  public int Fly1Row { get; set; }
  public int Fly2Row { get; set; }
  public int SingleCol { get; set; }
  public bool IsReverse1 { get; set; }
  public bool IsReverse2 { get; set; }
  public int[] FlyColArray1 { get; set; }
  public int[] FlyColArray2 { get; set; }

  public bool IsEnableDp { get; set; }
}