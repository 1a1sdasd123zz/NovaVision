using System.ComponentModel;
using System.Windows.Forms;

namespace NovaVision.UserControlLibrary
{
    public partial class TextBoxEx : TextBox
    {
        public TextBoxEx()
        {
            InitializeComponent();
        }

        public TextBoxEx(IContainer container)
        {
            container.Add(this);
            InitializeComponent();
        }
    }
}
