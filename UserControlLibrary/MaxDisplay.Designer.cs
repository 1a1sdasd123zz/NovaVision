namespace NovaVision.UserControlLibrary
{
    partial class MaxDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaxDisplay));
            this.cogRecordDisplay = new Cognex.VisionPro.CogRecordDisplay();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // cogRecordDisplay
            // 
            this.cogRecordDisplay.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogRecordDisplay.DoubleTapZoomCycleLength = 2;
            this.cogRecordDisplay.DoubleTapZoomSensitivity = 2.5D;
            this.cogRecordDisplay.Location = new System.Drawing.Point(0, 0);
            this.cogRecordDisplay.Margin = new System.Windows.Forms.Padding(2);
            this.cogRecordDisplay.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay.Name = "cogRecordDisplay";
            this.cogRecordDisplay.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay.OcxState")));
            this.cogRecordDisplay.Size = new System.Drawing.Size(1200, 675);
            this.cogRecordDisplay.TabIndex = 3;
            // 
            // MaxDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 675);
            this.Controls.Add(this.cogRecordDisplay);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MaxDisplay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MaxDisplay";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MaxDisplay_FormClosing);
            this.Shown += new System.EventHandler(this.MaxDisplay_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay;
    }
}