using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NovaVision.UserControlLibrary
{
    [DefaultEvent("CheckedChanged")]
    public partial class MyCheckBox : UserControl
    {
        public delegate void CheckedHandle(object sender, EventArgs e);

        private bool isDraw = false;

        private bool isCheck = false;

        private new Color DefaultBackColor;

        private Color CheckBackColor = Color.Gainsboro;

        private Color _CheckColor = Color.Blue;

        private Color _ActiveColor = Color.DodgerBlue;

        private Color _DefaultColor = Color.Blue;

        private bool _Checked = false;

        private bool _isAvtice = false;

        [Category("设置")]
        [Description("需要显示的文本")]
        public string Value
        {
            get
            {
                return Text;
            }
            set
            {
                Text = value;
                isDraw = false;
                Invalidate(new Rectangle(0, 0, base.Width, base.Height));
            }
        }

        [Category("设置")]
        [Description("选项框的颜色")]
        public Color CheckColor
        {
            get
            {
                return _CheckColor;
            }
            set
            {
                _CheckColor = value;
                Invalidate(new Rectangle(0, 0, 14, base.Height));
            }
        }

        [Category("设置")]
        [Description("当鼠标移到选项框时的颜色")]
        public Color ActiveColor
        {
            get
            {
                return _ActiveColor;
            }
            set
            {
                _ActiveColor = value;
            }
        }

        [Category("设置")]
        [Description("选项框的默认颜色")]
        private Color DefaultColor
        {
            get
            {
                return _DefaultColor;
            }
            set
            {
                _DefaultColor = value;
            }
        }

        [Category("设置")]
        [Description("表示是否选中")]
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                Invalidate();
                if (this.CheckedChanged != null)
                {
                    this.CheckedChanged(this, new EventArgs());
                }
            }
        }

        [Category("设置")]
        [Description("是否启用焦点变换")]
        public bool isAvtice
        {
            get
            {
                return _isAvtice;
            }
            set
            {
                _isAvtice = value;
            }
        }

        public event CheckedHandle CheckedChanged;

        public MyCheckBox()
        {
            InitializeComponent();
            DefaultBackColor = BackColor;
            DoubleBuffered = true;
            base.Load += MyCheckBox_Load;
            base.Paint += MyCheckBox_Paint;
            base.EnabledChanged += MyCheckBox_EnabledChanged;
            base.MouseDown += MyCheckBox_MouseDown;
            base.MouseMove += MyCheckBox_MouseMove;
            base.MouseLeave += MyCheckBox_MouseLeave;
            base.MouseUp += MyCheckBox_MouseUp;
            Value = "需要显示的文本";
        }

        private void MyCheckBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SizeF DSize = g.MeasureString("√", new Font(new FontFamily("黑体"), 18f));
            base.Height = (int)DSize.Height;
            Rectangle Outrect = new Rectangle(0, base.Height / 2 - 6, 12, 12);
            Rectangle Inrect = new Rectangle(1, base.Height / 2 - 6 + 1, 10, 10);
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(Outrect);
            g.DrawPath(new Pen(base.Enabled ? CheckColor : Color.Silver), path);
            g.FillPath(new SolidBrush(CheckBackColor), path);
            if (!isDraw)
            {
                SizeF thisSize = g.MeasureString(Text, Font);
                thisSize.Width += 14f;
                base.Width = (int)thisSize.Width;
                g.DrawString(Text, Font, new SolidBrush(ForeColor), new PointF(16f, (float)(base.Height / 2) - thisSize.Height / 2f + 1f));
            }
            if (Checked)
            {
                g.DrawString("√", new Font(new FontFamily("黑体"), 18f), new SolidBrush(base.Enabled ? CheckColor : Color.Silver), new PointF(6f - DSize.Width / 2f, base.Height / 2 - base.Height / 2 - 1));
            }
        }

        private void MyCheckBox_MouseLeave(object sender, EventArgs e)
        {
            CheckColor = DefaultColor;
            Cursor = Cursors.Default;
            Invalidate(new Rectangle(0, 0, 14, base.Height));
        }

        private void MyCheckBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAvtice)
            {
                CheckColor = ActiveColor;
                Invalidate(new Rectangle(0, 0, 14, base.Height));
            }
        }

        private void MyCheckBox_MouseDown(object sender, MouseEventArgs e)
        {
            Checked = !Checked;
            isDraw = false;
            isCheck = !Checked;
            CheckBackColor = Color.GhostWhite;
            Invalidate();
        }

        private void MyCheckBox_MouseUp(object sender, MouseEventArgs e)
        {
            CheckBackColor = Color.Gainsboro;
            Invalidate(new Rectangle(0, 0, 14, base.Height));
        }

        private void MyCheckBox_Load(object sender, EventArgs e)
        {
            DefaultColor = CheckColor;
            Invalidate();
        }

        private void MyCheckBox_EnabledChanged(object sender, EventArgs e)
        {
            if (!base.Enabled)
            {
                isDraw = false;
                Invalidate();
            }
        }
    }
}
