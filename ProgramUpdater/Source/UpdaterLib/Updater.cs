using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace UpdaterLib
{
    public class ProgrammUpdater
    {
        public static ProgrammUpdater m_instanse = null;
        
        public delegate void sendMessage(string _text);
        public event sendMessage onMessagereceive;
        public WebClient m_client {get; private set;}
        public ProxySettings m_proxySettings { get; set; }

        public static ProgrammUpdater getInstance()
        {
            if (m_instanse == null) m_instanse = new ProgrammUpdater();
            return m_instanse;
        }
        public static void DeleteInstance()
        {
            m_instanse = null;
        }

        public ProgrammUpdater()
        {
            m_instanse = this;
            onMessagereceive = (_text) => { };
            m_client = new WebClient();
            m_proxySettings = new ProxySettings();
        }

        public bool GenerateUpdateXml(string[] _files)
        {
            string pathToSave = "";
           
            //если в командной строке не указан параметр -update, то ничего не делаем
            if (!Environment.GetCommandLineArgs().Contains("-update")) return false;
            
            //проверяем указан ли в командной строке после параметра -update, путь для сохранения файлов
            for(int i = 0; i < Environment.GetCommandLineArgs().ToList().Count;i++)
            {
                if(Environment.GetCommandLineArgs().ToList()[i] == "-update")
                {
                    if (Environment.GetCommandLineArgs().ToList().Count > i + 1)
                    {
                        pathToSave = Environment.GetCommandLineArgs().ToList()[i + 1];
                        break;
                    }
                    else
                    {
                        onMessagereceive("Неверные аргументы командной строки\n-update <папка для сохранения файлов>");
                        return false;
                    }
                }
            }
            
            List<UpdateAssemblyInfo> assemblyList = new List<UpdateAssemblyInfo>();
            
            //ищем файлы на диске и считаем md5
            for (int i = 0; i < _files.Count(); i++)
            {
                if(!File.Exists(_files[i]))
                {
                    onMessagereceive("Файл не найден: " + _files[i] + "\nОшибка создания Xml!");
                    return false;
                }

                assemblyList.Add(new UpdateAssemblyInfo(_files[i], ComputeMD5Checksum(_files[i])));
            }

            if (!UpdateAssemblyInfo.saveToXml(pathToSave + "/update.xml", assemblyList))
            {
                onMessagereceive("Ошибка сохранения файла: " + pathToSave + "/update.xml");
                return false;
            }

            //копируем файлы в нужную директорию
            for (int i = 0; i < _files.Count(); i++ )
            {
                File.Copy(_files[i], pathToSave + "/" + _files[i], true);
            }

            onMessagereceive("Файлы успешно созданы в " + pathToSave);

            return true;
        }

        public void clientCheck(string _remotePath, string _appName)
        {
            Thread myThread = new Thread(delegate()
                {
                    List<UpdateAssemblyInfo> remoteAssembliesList = new List<UpdateAssemblyInfo>();
                    if (!UpdateAssemblyInfo.loadFromXml(_remotePath + "/update.xml",
                                                        ref remoteAssembliesList,
                                                        m_proxySettings))
                    {
                        return;
                    }

                    List<UpdateAssemblyInfo> listToUpdate = checkForUpdate(remoteAssembliesList);
                    if (listToUpdate.Count > 0)
                    {
                        UpdateAssemblyInfo.saveToXml("update.dat", listToUpdate);
                        if (File.Exists("ProgrammUpdater.exe"))
                        {
                            Process.Start("ProgrammUpdater.exe", _remotePath + " " + _appName);
                        }
                        return;
                    }

                    return;
                });
            myThread.IsBackground = true;
            myThread.Start();
        }

        public bool DownloadFiles(string _remoteUrl, List<UpdateAssemblyInfo> _assembliesList)
        {
            try
            {
                if(m_proxySettings.isProxyEnable)
                {
                    WebProxy wp = new WebProxy(m_proxySettings.proxyServer, m_proxySettings.proxyPort);
                    wp.Credentials = new NetworkCredential(m_proxySettings.proxyUser, m_proxySettings.proxyPassword);
                    m_client.Proxy = wp;
                }
                
                if (!Directory.Exists("updateTemp"))
                {
                    Directory.CreateDirectory("updateTemp");
                }

                onMessagereceive("Начало загрузки файлов");
                
                for (int i = 0; i < _assembliesList.Count; i++)
                {
                    m_client.DownloadFile(new Uri(_remoteUrl + "/" + _assembliesList[i].name), "updateTemp/" + _assembliesList[i].name);
                    onMessagereceive("Файл " + _assembliesList[i].name + " загружен " + (i + 1).ToString() + "/" + _assembliesList.Count.ToString());
                }

                onMessagereceive("Все файлы загружены");
            }
            catch(Exception e)
            {
                onMessagereceive("Ошибка загрузки файла\n" + e.Message);
                return false;
            }
            return true;
        }

        //передаем список всех сборок данного приложения и список сборок доступных на удаленном сервере
        //в ответ получаем список сборок, которые есть на сервере, и нет в данном приложение и те у который версия на сервере больше чем в приложение.
        public List<UpdateAssemblyInfo> checkForUpdate(List<UpdateAssemblyInfo> _remoteAssemblies)
        {
            List<UpdateAssemblyInfo> resultList = new List<UpdateAssemblyInfo>();
            for(int i = 0; i < _remoteAssemblies.Count; i++)
            {
                if(!File.Exists(_remoteAssemblies[i].name))
                {
                    resultList.Add(_remoteAssemblies[i]);
                }
                else
                {
                    if(_remoteAssemblies[i].md5 != ComputeMD5Checksum(_remoteAssemblies[i].name))
                    {
                        resultList.Add(_remoteAssemblies[i]);
                    }
                }
            }

            return resultList;
        }

        public static string ComputeMD5Checksum(string path)
        {
            using (FileStream fs = System.IO.File.OpenRead(path))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSum = md5.ComputeHash(fileData);
                string result = BitConverter.ToString(checkSum).Replace("-", String.Empty).ToLower();
                return result;
            }
        }
    }

    [Serializable]
    public class UpdateAssemblyInfo
    {
        public string name { get; set; }
        public string md5 { get; set; }

        public UpdateAssemblyInfo()
        {
            name = "";
            md5 = "";
        }

        public UpdateAssemblyInfo(string _name, string _md5)
        {
            name = _name;
            md5 = _md5;
        }

        public UpdateAssemblyInfo(UpdateAssemblyInfo other)
        {
            name = other.name;
            md5 = other.md5;
        }

        public static bool loadFromXml(string _path, ref List<UpdateAssemblyInfo> _assemblyList, ProxySettings _proxySettings)
        {
            try
            {
                XmlTextReader reader;
                if (_proxySettings.isProxyEnable)
                {
                    WebProxy wp = new WebProxy(_proxySettings.proxyServer, _proxySettings.proxyPort);
                    wp.Credentials = new NetworkCredential(_proxySettings.proxyUser, _proxySettings.proxyPassword);
                    WebClient wc = new WebClient();
                    wc.Proxy = wp;

                    MemoryStream ms = new MemoryStream(wc.DownloadData(_path));
                    reader = new XmlTextReader(ms);
                }
                else
                {
                    reader = new XmlTextReader(_path);
                }

                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "module":
                                UpdateAssemblyInfo someInfo = new UpdateAssemblyInfo();
                                someInfo.md5 = reader["md5"];
                                someInfo.name = reader["name"];
                                _assemblyList.Add(someInfo);
                                break;
                        }
                    }
                }
                reader.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public static bool saveToXml(string _path, List<UpdateAssemblyInfo> _assemblyList)
        {
            try
            {
                using (XmlTextWriter writer = new XmlTextWriter(_path, Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartDocument();
                    writer.WriteStartElement("modules");
                    for (int i = 0; i < _assemblyList.Count; i++)
                    {
                        writer.WriteStartElement("module");
                        writer.WriteAttributeString("name", _assemblyList[i].name);
                        writer.WriteAttributeString("md5", _assemblyList[i].md5);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch 
            {
                return false;
            }
            return true;
        }
    }

    public class ProxySettings
    {
        public bool isProxyEnable { get; set; }
        public string proxyServer { get; set; }
        public int proxyPort { get;set;}
        public string proxyUser { get; set; }
        public string proxyPassword { get; set; }


        public ProxySettings(bool _isProxyEnable = false,
                                string _proxyServer = "",
                                int _proxyPort = 3128,
                                string _proxyUser = "",
                                string _proxyPassword = "")
        {
            isProxyEnable = _isProxyEnable;
            proxyServer = _proxyServer;
            proxyPort = _proxyPort;
            proxyUser = _proxyUser;
            proxyPassword = _proxyPassword;
        }

        public ProxySettings(ProxySettings other)
        {
            isProxyEnable = other.isProxyEnable;
            proxyServer = other.proxyServer;
            proxyPort = other.proxyPort;
            proxyUser = other.proxyUser;
            proxyPassword = other.proxyPassword;
        }

        public ProxySettings()
        {
            isProxyEnable = false;
            proxyServer = "";
            proxyPort = 3128;
            proxyUser = "";
            proxyPassword = "";
        }
    }
}
