using System;
using System.Reflection;
using System.Windows.Forms;

namespace ProgToUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            UpdaterLib.ProgrammUpdater updater = new UpdaterLib.ProgrammUpdater();

            if (updater.GenerateUpdateXml(new string[] { "ProgToUpdate.exe" })) Environment.Exit(0);

            updater.clientCheck(@"https://dl.dropboxusercontent.com/u/8399817", "ProgToUpdate.exe");

            Text += " " + Assembly.GetEntryAssembly().GetName().Version.ToString();
        }
    }
}
