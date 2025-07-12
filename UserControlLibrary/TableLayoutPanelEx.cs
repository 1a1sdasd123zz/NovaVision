using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NovaVision.UserControlLibrary
{
    public class TableLayoutPanelEx : TableLayoutPanel
    {
        private const int WM_NCHITTEST = 132;

        private const int WM_MOUSEMOVE = 512;

        private const int WM_LBUTTONDOWN = 513;

        private const int WM_LBUTTONUP = 514;

        private const long MK_LBUTTON = 1L;

        private List<int> VBorders = new List<int>();

        private List<int> HBorders = new List<int>();

        private int selColumn = -1;

        private int selRow = -1;

        public new int ColumnCount
        {
            get
            {
                return base.ColumnCount;
            }
            set
            {
                base.ColumnCount = value;
            }
        }

        public new int RowCount
        {
            get
            {
                return base.RowCount;
            }
            set
            {
                base.RowCount = value;
            }
        }

        public event EventHandler RowColSizeChanged;

        public TableLayoutPanelEx(IContainer container)
        {
            container.Add(this);
            DoubleBuffered = true;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
        }

        public void ResetSizeAndSizeTypes()
        {
            for (int c = 0; c <= base.ColumnCount - 1; c++)
            {
                base.ColumnStyles[c].SizeType = SizeType.Percent;
                base.ColumnStyles[c].Width = 100 / base.ColumnCount;
            }
            for (int r = 0; r <= base.RowCount - 1; r++)
            {
                base.RowStyles[r].SizeType = SizeType.Percent;
                base.RowStyles[r].Height = 100 / base.RowCount;
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            try
            {
                if (!(base.Created & !base.Disposing))
                {
                    return;
                }
                if (m.Msg == 132)
                {
                    Point loc = PointToClient(Control.MousePosition);
                    VBorders.Clear();
                    HBorders.Clear();
                    if (ColumnCount > 1)
                    {
                        for (int w = 0; w <= base.ColumnCount - 2; w++)
                        {
                            if (w == 0)
                            {
                                VBorders.Add(GetColumnWidths()[w]);
                            }
                            else
                            {
                                VBorders.Add(VBorders[VBorders.Count - 1] + GetColumnWidths()[w]);
                            }
                        }
                    }
                    if (RowCount > 1)
                    {
                        for (int h = 0; h <= GetRowHeights().Length - 2; h++)
                        {
                            if (h == 0)
                            {
                                HBorders.Add(GetRowHeights()[h]);
                            }
                            else
                            {
                                HBorders.Add(HBorders[HBorders.Count - 1] + GetRowHeights()[h]);
                            }
                        }
                    }
                    bool onV = VBorders.Contains(loc.X) | VBorders.Contains(loc.X - 1) | VBorders.Contains(loc.X + 1);
                    bool onH = HBorders.Contains(loc.Y) | HBorders.Contains(loc.Y - 1) | HBorders.Contains(loc.Y + 1);
                    if (onV && onH)
                    {
                        Cursor = Cursors.SizeAll;
                    }
                    else if (onV)
                    {
                        Cursor = Cursors.VSplit;
                    }
                    else if (onH)
                    {
                        Cursor = Cursors.HSplit;
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                    }
                }
                else if ((m.Msg == 513) & (Cursor != Cursors.Default))
                {
                    Point loc2 = PointToClient(Control.MousePosition);
                    selColumn = -1;
                    selRow = -1;
                    for (int c = 0; c <= VBorders.Count - 1; c++)
                    {
                        if ((VBorders[c] >= loc2.X - 1) & (VBorders[c] <= loc2.X + 1))
                        {
                            selColumn = c;
                            break;
                        }
                    }
                    for (int r = 0; r <= HBorders.Count - 1; r++)
                    {
                        if ((HBorders[r] >= loc2.Y - 1) & (HBorders[r] <= loc2.Y + 1))
                        {
                            selRow = r;
                            break;
                        }
                    }
                }
                else if ((m.Msg == 512) & (m.WParam.ToInt64() == 1))
                {
                    Point loc3 = PointToClient(Control.MousePosition);
                    if (!(Cursor != Cursors.Default))
                    {
                        return;
                    }
                    if ((selRow > -1) & (loc3.Y >= 1) & (loc3.Y <= base.ClientSize.Height - 2))
                    {
                        float rowHeight = 0f;
                        if (base.RowStyles[selRow].SizeType == SizeType.Absolute)
                        {
                            rowHeight = base.RowStyles[selRow].Height;
                        }
                        else
                        {
                            base.RowStyles[selRow].SizeType = SizeType.Percent;
                            rowHeight = base.RowStyles[selRow].Height / 100f * (float)base.ClientSize.Height;
                        }
                        float ref2 = (float)loc3.Y - rowHeight;
                        if (selRow > 0)
                        {
                            ref2 -= (float)HBorders[selRow - 1];
                        }
                        if (rowHeight + ref2 > 0f)
                        {
                            if (RowCount > selRow + 1)
                            {
                                float rowHeight2 = 0f;
                                if (base.RowStyles[selRow + 1].SizeType == SizeType.Absolute)
                                {
                                    rowHeight2 = base.RowStyles[selRow + 1].Height;
                                }
                                else
                                {
                                    base.RowStyles[selRow + 1].SizeType = SizeType.Percent;
                                    rowHeight2 = base.RowStyles[selRow + 1].Height / 100f * (float)base.ClientSize.Height;
                                }
                                if (rowHeight2 - ref2 < 1f)
                                {
                                    return;
                                }
                                rowHeight2 -= ref2;
                                if (base.RowStyles[selRow + 1].SizeType == SizeType.Absolute)
                                {
                                    base.RowStyles[selRow + 1].Height = rowHeight2;
                                }
                                else
                                {
                                    base.RowStyles[selRow + 1].Height = rowHeight2 / (float)base.ClientSize.Height * 100f;
                                }
                            }
                            rowHeight += ref2;
                            if (base.RowStyles[selRow].SizeType == SizeType.Absolute)
                            {
                                base.RowStyles[selRow].Height = rowHeight;
                            }
                            else
                            {
                                base.RowStyles[selRow].Height = rowHeight / (float)base.ClientSize.Height * 100f;
                            }
                        }
                    }
                    if ((selColumn > -1) & (loc3.X >= 1) & (loc3.X <= base.ClientSize.Width - 2))
                    {
                        float colWidth = 0f;
                        if (base.ColumnStyles[selColumn].SizeType == SizeType.Absolute)
                        {
                            colWidth = base.ColumnStyles[selColumn].Width;
                        }
                        else
                        {
                            base.ColumnStyles[selColumn].SizeType = SizeType.Percent;
                            colWidth = base.ColumnStyles[selColumn].Width / 100f * (float)base.ClientSize.Width;
                        }
                        float @ref = (float)loc3.X - colWidth;
                        if (selColumn > 0)
                        {
                            @ref -= (float)VBorders[selColumn - 1];
                        }
                        if (colWidth + @ref > 0f)
                        {
                            if (ColumnCount > selColumn + 1)
                            {
                                float colWidth2 = 0f;
                                if (base.ColumnStyles[selColumn + 1].SizeType == SizeType.Absolute)
                                {
                                    colWidth2 = base.ColumnStyles[selColumn + 1].Width;
                                }
                                else
                                {
                                    base.ColumnStyles[selColumn + 1].SizeType = SizeType.Percent;
                                    colWidth2 = base.ColumnStyles[selColumn + 1].Width / 100f * (float)base.ClientSize.Width;
                                }
                                if (colWidth2 - @ref < 1f)
                                {
                                    return;
                                }
                                colWidth2 -= @ref;
                                if (base.ColumnStyles[selColumn + 1].SizeType == SizeType.Absolute)
                                {
                                    base.ColumnStyles[selColumn + 1].Width = colWidth2;
                                }
                                else
                                {
                                    base.ColumnStyles[selColumn + 1].Width = colWidth2 / (float)base.ClientSize.Width * 100f;
                                }
                            }
                            colWidth += @ref;
                            if (base.ColumnStyles[selColumn].SizeType == SizeType.Absolute)
                            {
                                base.ColumnStyles[selColumn].Width = colWidth;
                            }
                            else
                            {
                                base.ColumnStyles[selColumn].Width = colWidth / (float)base.ClientSize.Width * 100f;
                            }
                        }
                    }
                    if (this.RowColSizeChanged != null)
                    {
                        this.RowColSizeChanged(this, new EventArgs());
                    }
                }
                else if (m.Msg == 514)
                {
                    selColumn = -1;
                    selRow = -1;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
