namespace Tracker.Gui.Controls
{
    partial class Scatter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSpeedUp = new System.Windows.Forms.Button();
            this.buttonPlayPause = new System.Windows.Forms.Button();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.buttonSlowDown = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButtonMysectionOnly = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.radioButtonMysectionOnly, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.buttonSpeedUp, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonPlayPause, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buttonSlowDown, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.trackBar1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.radioButton1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 41F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(649, 660);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonSpeedUp
            // 
            this.buttonSpeedUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSpeedUp.Location = new System.Drawing.Point(455, 548);
            this.buttonSpeedUp.Name = "buttonSpeedUp";
            this.buttonSpeedUp.Size = new System.Drawing.Size(104, 48);
            this.buttonSpeedUp.TabIndex = 4;
            this.buttonSpeedUp.Text = "Speed +++";
            this.buttonSpeedUp.UseVisualStyleBackColor = true;
            // 
            // buttonPlayPause
            // 
            this.buttonPlayPause.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonPlayPause.Location = new System.Drawing.Point(252, 548);
            this.buttonPlayPause.Name = "buttonPlayPause";
            this.buttonPlayPause.Size = new System.Drawing.Size(104, 48);
            this.buttonPlayPause.TabIndex = 3;
            this.buttonPlayPause.Text = "PLAY";
            this.buttonPlayPause.UseVisualStyleBackColor = true;
            // 
            // zedGraphControl1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.zedGraphControl1, 3);
            this.zedGraphControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 3);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(603, 539);
            this.zedGraphControl1.TabIndex = 0;
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.SetColumnSpan(this.trackBar1, 3);
            this.trackBar1.Location = new System.Drawing.Point(3, 643);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(603, 14);
            this.trackBar1.TabIndex = 1;
            // 
            // buttonSlowDown
            // 
            this.buttonSlowDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSlowDown.Location = new System.Drawing.Point(49, 548);
            this.buttonSlowDown.Name = "buttonSlowDown";
            this.buttonSlowDown.Size = new System.Drawing.Size(104, 48);
            this.buttonSlowDown.TabIndex = 2;
            this.buttonSlowDown.Text = "Speed ---";
            this.buttonSlowDown.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(206, 602);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(125, 17);
            this.radioButton1.TabIndex = 5;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "All Boats In the Race";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButtonMysectionOnly
            // 
            this.radioButtonMysectionOnly.AutoSize = true;
            this.radioButtonMysectionOnly.Location = new System.Drawing.Point(3, 602);
            this.radioButtonMysectionOnly.Name = "radioButtonMysectionOnly";
            this.radioButtonMysectionOnly.Size = new System.Drawing.Size(102, 17);
            this.radioButtonMysectionOnly.TabIndex = 6;
            this.radioButtonMysectionOnly.Text = "My Section Only";
            this.radioButtonMysectionOnly.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(612, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(31, 539);
            this.panel1.TabIndex = 7;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Scatter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Scatter";
            this.Size = new System.Drawing.Size(649, 660);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button buttonSpeedUp;
        private System.Windows.Forms.Button buttonPlayPause;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button buttonSlowDown;
        private System.Windows.Forms.RadioButton radioButtonMysectionOnly;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel panel1;
    }
}
