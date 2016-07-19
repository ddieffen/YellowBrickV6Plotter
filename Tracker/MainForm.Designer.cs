namespace Tracker
{
    partial class MainForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.messagesLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yellowbrickToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateRaceInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getAllPositionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getLatestPositionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chartPositions1 = new Tracker.Gui.Controls.ChartPositions();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.analytics1 = new Tracker.Gui.Controls.Analytics();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.emailPasting1 = new Tracker.Gui.Controls.EmailPasting();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.TimeAutoSaves = new System.Windows.Forms.Timer(this.components);
            this.timer100ms = new System.Windows.Forms.Timer(this.components);
            this.scatter1 = new Tracker.Gui.Controls.Scatter();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 809);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(914, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(833, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "Status";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(66, 17);
            this.toolStripStatusLabel3.Text = "LastUpdate";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.parametersToolStripMenuItem,
            this.yellowbrickToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(914, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(97, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editParametersToolStripMenuItem,
            this.messagesLogToolStripMenuItem});
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(78, 20);
            this.parametersToolStripMenuItem.Text = "Parameters";
            // 
            // editParametersToolStripMenuItem
            // 
            this.editParametersToolStripMenuItem.Name = "editParametersToolStripMenuItem";
            this.editParametersToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.editParametersToolStripMenuItem.Text = "Edit Parameters";
            this.editParametersToolStripMenuItem.Click += new System.EventHandler(this.editParametersToolStripMenuItem_Click);
            // 
            // messagesLogToolStripMenuItem
            // 
            this.messagesLogToolStripMenuItem.Name = "messagesLogToolStripMenuItem";
            this.messagesLogToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.messagesLogToolStripMenuItem.Text = "Messages Log";
            this.messagesLogToolStripMenuItem.Click += new System.EventHandler(this.messagesLogToolStripMenuItem_Click);
            // 
            // yellowbrickToolStripMenuItem
            // 
            this.yellowbrickToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateRaceInfoToolStripMenuItem,
            this.getAllPositionsToolStripMenuItem,
            this.getLatestPositionsToolStripMenuItem});
            this.yellowbrickToolStripMenuItem.Name = "yellowbrickToolStripMenuItem";
            this.yellowbrickToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.yellowbrickToolStripMenuItem.Text = "Yellowbrick";
            // 
            // updateRaceInfoToolStripMenuItem
            // 
            this.updateRaceInfoToolStripMenuItem.Name = "updateRaceInfoToolStripMenuItem";
            this.updateRaceInfoToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.updateRaceInfoToolStripMenuItem.Text = "Update Race Info";
            this.updateRaceInfoToolStripMenuItem.Click += new System.EventHandler(this.updateRaceInfoToolStripMenuItem_Click);
            // 
            // getAllPositionsToolStripMenuItem
            // 
            this.getAllPositionsToolStripMenuItem.Name = "getAllPositionsToolStripMenuItem";
            this.getAllPositionsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.getAllPositionsToolStripMenuItem.Text = "Get All Positions";
            this.getAllPositionsToolStripMenuItem.Click += new System.EventHandler(this.getAllPositionsToolStripMenuItem_Click);
            // 
            // getLatestPositionsToolStripMenuItem
            // 
            this.getLatestPositionsToolStripMenuItem.Name = "getLatestPositionsToolStripMenuItem";
            this.getLatestPositionsToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.getLatestPositionsToolStripMenuItem.Text = "Get Latest Positions";
            this.getLatestPositionsToolStripMenuItem.Click += new System.EventHandler(this.getLatestPositionsToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(914, 785);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.chartPositions1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(906, 759);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Chart and Positions";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // chartPositions1
            // 
            this.chartPositions1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPositions1.Location = new System.Drawing.Point(3, 3);
            this.chartPositions1.Name = "chartPositions1";
            this.chartPositions1.Size = new System.Drawing.Size(900, 753);
            this.chartPositions1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.analytics1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(906, 759);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Results / Distances";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // analytics1
            // 
            this.analytics1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.analytics1.Location = new System.Drawing.Point(3, 3);
            this.analytics1.Name = "analytics1";
            this.analytics1.Size = new System.Drawing.Size(900, 753);
            this.analytics1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.emailPasting1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(906, 759);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Email Pasting";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // emailPasting1
            // 
            this.emailPasting1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.emailPasting1.Location = new System.Drawing.Point(3, 3);
            this.emailPasting1.Name = "emailPasting1";
            this.emailPasting1.Size = new System.Drawing.Size(900, 753);
            this.emailPasting1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.scatter1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(906, 759);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Scatter";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // TimeAutoSaves
            // 
            this.TimeAutoSaves.Enabled = true;
            this.TimeAutoSaves.Interval = 300000;
            this.TimeAutoSaves.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer100ms
            // 
            this.timer100ms.Enabled = true;
            // 
            // scatter1
            // 
            this.scatter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scatter1.Location = new System.Drawing.Point(3, 3);
            this.scatter1.Name = "scatter1";
            this.scatter1.Size = new System.Drawing.Size(900, 753);
            this.scatter1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 831);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(733, 557);
            this.Name = "MainForm";
            this.Text = "Yellowbrick Data Reader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editParametersToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private Gui.Controls.ChartPositions chartPositions1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Timer TimeAutoSaves;
        private System.Windows.Forms.ToolStripMenuItem messagesLogToolStripMenuItem;
        private Gui.Controls.Analytics analytics1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private Gui.Controls.EmailPasting emailPasting1;
        private System.Windows.Forms.ToolStripMenuItem yellowbrickToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateRaceInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getAllPositionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getLatestPositionsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Timer timer100ms;
        private Gui.Controls.Scatter scatter1;
    }
}

