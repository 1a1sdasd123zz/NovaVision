using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NovaVision.VisionForm.CarameFrm
{
    public partial class ACQfifoTool : Form
    {
        public ACQfifoTool(Cognex.VisionPro.ICogAcqFifo acq)
        {
            InitializeComponent();
            cogAcqFifoCtlV21.Subject = acq;
        }
    }
}
