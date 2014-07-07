using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tracker.Data;

namespace Tracker
{
    public partial class Parameters : UserControl
    {
        #region delegates
        public delegate void Updating();
        public event Updating UpdatingEvent;
        #endregion

        #region constructors
        public Parameters()
        {
            InitializeComponent();

            this.textBoxRoot.Text = Tracker.Properties.Settings.Default.ServerName;
        }
        #endregion

        #region methods
        private void buttonApply_Click(object sender, EventArgs e)
        {
            bool closing = true;

            if (Tracker.Properties.Settings.Default.ServerName != this.textBoxRoot.Text)
            {
                DialogResult res = MessageBox.Show("Modifying the server root location require to restart the application\r\n otherwise data from the previous and new race will be merged", "Warning", MessageBoxButtons.OKCancel);
                if (res == DialogResult.OK)
                    Tracker.Properties.Settings.Default.ServerName = this.textBoxRoot.Text;
                else
                    closing = false;
            }

            Tracker.Properties.Settings.Default.Save();

            if (this.UpdatingEvent != null)
                this.UpdatingEvent();

            if (this.Parent is Form && closing)
            {
                (this.Parent as Form).Close();

                this.Dispose();
            }
        }
        #endregion

    }
}
