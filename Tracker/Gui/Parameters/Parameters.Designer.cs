namespace Tracker
{
    partial class Parameters
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
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxRoot = new System.Windows.Forms.TextBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(17, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "Root Location :";
            // 
            // textBoxRoot
            // 
            this.textBoxRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRoot.Enabled = false;
            this.textBoxRoot.Location = new System.Drawing.Point(103, 12);
            this.textBoxRoot.Name = "textBoxRoot";
            this.textBoxRoot.Size = new System.Drawing.Size(376, 20);
            this.textBoxRoot.TabIndex = 21;
            this.textBoxRoot.Text = "http://gae.yb.tl/Flash/chicagomack2013/";
            // 
            // buttonApply
            // 
            this.buttonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonApply.Location = new System.Drawing.Point(385, 244);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(94, 30);
            this.buttonApply.TabIndex = 46;
            this.buttonApply.Text = "Apply Changes";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // Parameters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxRoot);
            this.MinimumSize = new System.Drawing.Size(488, 277);
            this.Name = "Parameters";
            this.Size = new System.Drawing.Size(488, 277);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRoot;
        private System.Windows.Forms.Button buttonApply;
    }
}
