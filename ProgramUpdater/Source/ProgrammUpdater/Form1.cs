using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using UpdaterLib;

namespace ProgrammUpdater
{
    public partial class Form1 : Form
    {
        int filesDownloaded = 0;
        string appName;
        string remoteDownloadPath;
        List<UpdateAssemblyInfo> assemblyList;
        ProxySettings m_proxySettings;
        UpdaterLib.ProgrammUpdater m_updater;

        public Form1()
        {
            m_proxySettings = new ProxySettings(true, "10.10.10.100", 3128, "spirit", "54475");
            InitializeComponent();
            Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();

            if(Environment.GetCommandLineArgs().Count() != 3)
            {
                MessageBox.Show("Неправильно количествое параметров командной строки");
                Environment.Exit(0);
            }
            else
            {
                appName = Environment.GetCommandLineArgs().ToList()[2];
                remoteDownloadPath = Environment.GetCommandLineArgs().ToList()[1];
            }

            assemblyList = new List<UpdateAssemblyInfo>();

            if (!UpdateAssemblyInfo.loadFromXml("update.dat", ref assemblyList, m_proxySettings))
            {
                ui_updateButton.Enabled = false;
                ThreadSafe.WriteLog(ui_textLog, "Файл update.dat не найден!");
            }

            if (assemblyList.Count == 0)
            {
                ui_updateButton.Enabled = false;
                ThreadSafe.WriteLog(ui_textLog, "Нет обновлений");
            }
            else
            {
                ThreadSafe.WriteLog(ui_textLog, "Доступны для обновления новые файлы:");
                for(int i = 0 ; i < assemblyList.Count; i++)
                {
                    ThreadSafe.WriteLog(ui_textLog, assemblyList[i].name);
                }
                ThreadSafe.WriteLog(ui_textLog, String.Format("Всего файлов: {0} ", assemblyList.Count.ToString()));
            }

            m_updater = new UpdaterLib.ProgrammUpdater();
            m_updater.onMessagereceive += (_text => ThreadSafe.WriteLog(ui_textLog, _text));
            m_updater.m_proxySettings = new ProxySettings(true, "10.10.10.100", 3128, "spirit", "54475");
        }

        void client_DownloadFileCompleted()
        {
            string procName = appName.Substring(0, appName.LastIndexOf("."));
            while (Process.GetProcessesByName(procName).Count() > 0)
            {
                if (MessageBox.Show("Закройте программу " + appName + " для продолжения!", "Update", MessageBoxButtons.RetryCancel) == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
            }

            ThreadSafe.SetStatusLabel(ui_statusLabel, (++filesDownloaded).ToString() + "/" + assemblyList.Count.ToString());

            ThreadSafe.WriteLog(ui_textLog, "Файлы загружены!");

            for (int i = 0; i < assemblyList.Count; i++)
            {
                string md5 = UpdaterLib.ProgrammUpdater.ComputeMD5Checksum("updateTemp/" + assemblyList[i].name);
                if (md5 != assemblyList[i].md5)
                {
                    ThreadSafe.WriteLog(ui_textLog, "MD5 не совпадает: " + assemblyList[i].md5 + " " + md5 + " файл: " + assemblyList[i].name);
                    return;
                }
            }

            try
            {
                for (int i = 0; i < assemblyList.Count; i++)
                {
                    if (File.Exists(assemblyList[i].name)) File.Delete(assemblyList[i].name);

                    File.Move("updateTemp/" + assemblyList[i].name, assemblyList[i].name);
                }

                File.Delete("update.dat");

                ThreadSafe.WriteLog(ui_textLog, "Файлы обновлены!");

                if (MessageBox.Show("Запустить основную программу?", "Updater", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(appName);
                    Environment.Exit(0);
                }
            }
            catch (Exception e1)
            {
                ThreadSafe.WriteLog(ui_textLog, e1.Message);
            }
        }

        private void ui_updateButton_Click(object sender, EventArgs e)
        {
            Thread myThread = new Thread(delegate()
            {
                filesDownloaded = 0;

                if (!m_updater.DownloadFiles(remoteDownloadPath, assemblyList))
                {
                    ThreadSafe.WriteLog(ui_textLog, "Files downloading error, try again later");
                } else{
                    client_DownloadFileCompleted();
                }
                
            });
            myThread.IsBackground = true;
            myThread.Start();

            ui_updateButton.Enabled = false;
        }
    }
}
