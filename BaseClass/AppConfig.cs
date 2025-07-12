using System.Collections.Generic;
using NovaVision.BaseClass.Collection;
using WeifenLuo.WinFormsUI.Docking;

namespace NovaVision.BaseClass
{
    public class AppConfig : IChangedEvent
    {
        public event ChangeEventHandler Changing;

        public event ChangeEventHandler Changed;

        public DockState DS_Frm_Data = DockState.DockRight;

        public DockState DS_Frm_Statistics = DockState.DockRight;

        public DockState DS_Frm_Log = DockState.DockBottom;

        public DockState DS_Frm_Error = DockState.DockBottom;

        public DockState DS_Frm_2D = DockState.Document;

        public DockState DS_Frm_3D = DockState.Document;

        public bool DS_Frm_2D_Dsiplay = false;

        public bool DS_Frm_3D_Dsiplay = false;

        public double DS_DockLeftPortion = 0.2;

        public double DS_DockRighttPortion = 0.2;

        public double DS_DockTopPortion = 0.2;

        public double DS_DockBottomPortion = 0.2;

        public int DigitalNumReserve = 2;

        private int _ds_2D_Row = 0;

        private int _ds_2D_Column = 0;

        private int _ds_3D_Row;

        private int _ds_3D_Column = 0;

        public double DS_3D_Size = 0.4;

        public string DS_2D_Names = "";


        public string DS_3D_Names = "";

        public List<float> Rows_2D_Height = new List<float>();

        public List<float> Cols_2D_Width = new List<float>();

        public List<float> Rows_3D_Height = new List<float>();

        public List<float> Cols_3D_Width = new List<float>();

        public int DS_2D_Row
        {
            get
            {
                return _ds_2D_Row;
            }
            set
            {
                if (_ds_2D_Row != value)
                {
                    int oldValue = _ds_2D_Row;
                    OnChanging(oldValue, value);
                    _ds_2D_Row = value;
                    OnChanged(oldValue, value);
                }
            }
        }

        public int DS_2D_Column
        {
            get
            {
                return _ds_2D_Column;
            }
            set
            {
                if (_ds_2D_Column != value)
                {
                    int oldValue = _ds_2D_Column;
                    OnChanging(oldValue, value);
                    _ds_2D_Column = value;
                    OnChanged(oldValue, value);
                }
            }
        }

        public int DS_3D_Row
        {
            get
            {
                return _ds_3D_Row;
            }
            set
            {
                if (_ds_3D_Row != value)
                {
                    int oldValue = _ds_3D_Row;
                    OnChanging(oldValue, value);
                    _ds_3D_Row = value;
                    OnChanged(oldValue, value);
                }
            }
        }

        public int DS_3D_Column
        {
            get
            {
                return _ds_3D_Column;
            }
            set
            {
                if (_ds_3D_Column != value)
                {
                    int oldValue = _ds_3D_Column;
                    OnChanging(oldValue, value);
                    _ds_3D_Column = value;
                    OnChanged(oldValue, value);
                }
            }
        }


        private void OnChanging(object oldValue, object newValue)
        {
            if (this.Changing != null)
            {
                this.Changing(this, new ChangeEventArg(oldValue, newValue));
            }
        }

        private void OnChanged(object oldValue, object newValue)
        {
            if (this.Changed != null)
            {
                this.Changed(this, new ChangeEventArg(oldValue, newValue));
            }
        }

        public void ResumeLayout()
        {
            DS_Frm_Data = DockState.DockRight;
            DS_Frm_Statistics = DockState.DockRight;
            DS_Frm_Log = DockState.DockBottom;
            DS_Frm_Error = DockState.DockBottom;
            DS_Frm_2D = DockState.Document;
            DS_Frm_3D = DockState.Document;
        }
    }
}
