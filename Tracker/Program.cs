﻿using System;
using System.Windows.Forms;

namespace Tracker
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //try
            //{
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            //}
            //catch (Exception e)
            //{
            //    new Reporter(e);
            //}
        }
    }

    class Reporter
    {
        /// <summary>
        /// Function called if an exception has been thrown somewhere in the software
        /// Used to send a mail automatically to the developers in order to signal a bug.
        /// </summary>
        /// <param name="e">The exceptions thrown</param>
        public Reporter(Exception e)
        {
            // Displays the Reporter Form
            ReporterForm reporter = new ReporterForm(e);
            reporter.ShowDialog();
            Application.Exit();
        }
    }
}
