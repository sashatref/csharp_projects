using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace VersionAutoIncrement
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            rcInfo _rc = new rcInfo();;
            
            rcInfo.loadSettings(ref _rc);

            ui_propertyGrid.SelectedObject = _rc;
        }

        private void ui_okButton_Click(object sender, EventArgs e)
        {
            ((rcInfo)ui_propertyGrid.SelectedObject).saveToRc();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ui_saveHFile_Click(object sender, EventArgs e)
        {
            Program.incBuild("buildNumber.h");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            rcInfo.saveSettings((rcInfo)ui_propertyGrid.SelectedObject);
        }
    }

    public enum FileType
    {
      [Description("Dynamic dll")]
      dynamic_dll,
      [Description("Application")]
      application
    }

    [Serializable]
    public class rcInfo
    {
        public rcInfo()
        {
            fileName = "myApp.rc";
            fileDescription = "My application description";
            originalFileName = "myApp.exe";
            productName = "myApp";
            legalCopyright = "My coorporation";
            fileType = FileType.application;
            icoName = "";
        }

        public rcInfo(rcInfo other)
        {
            fileName            = other.fileName;
            fileDescription     = other.fileDescription;
            originalFileName    = other.originalFileName;
            productName         = other.productName;
            legalCopyright      = other.legalCopyright;
            fileType            = other.fileType;
            icoName             = other.icoName;
        }

        [DisplayName("1.File name"), Description("File name to save")]
        public string fileName { get; set; }
        
        [DisplayName("2.Original file name")]
        public string originalFileName { get; set; }
        
        [DisplayName("3.Product name")]
        public string productName { get; set; }
        
        [DisplayName("4.File description")]
        public string fileDescription { get; set; }
        
        [DisplayName("5.Legal copyraight")]
        public string legalCopyright { get; set; }

        [DisplayName("6.App ico")]
        public string icoName { get; set; }

        [DisplayName("7.File type")]
        public FileType fileType { get; set; }

        public void saveToRc()
        {
            string _fileType = "0x1L";
            
            if(fileType == FileType.dynamic_dll)
            {
                _fileType = "0x2L";
            } else if(fileType == FileType.application)
            {
                _fileType = "0x1L";
            }

            string ico = "";

            if(icoName != "")
            {
                ico = "IDI_ICON1 ICON DISCARDABLE \"" + icoName + "\""; 
            }
            
            string textToSave = "#include \"buildNumber.h\"" +
                                "\n" + ico +
                                "\n\n1 VERSIONINFO" +
                                "\nFILEVERSION MAJOR_VERSION,MINOR_VERSION,MAINTENANCE,BUILD" +
                                "\nFILEFLAGSMASK 0x17L" +
                                "\nFILEOS 0x4L" +
                                "\nFILETYPE " + _fileType + 
                                "\nFILESUBTYPE 0x0L" +
                                "\nBEGIN" +
                                    "\n\tBLOCK \"StringFileInfo\"" +
                                    "\n\tBEGIN" +
                                        "\n\t\tBLOCK \"040904b0\"" +
                                        "\n\t\tBEGIN" +
                                            "\n\t\t\tVALUE \"FileDescription\", \"" + fileDescription + "\"" +
                                            "\n\t\t\tVALUE \"LegalCopyright\", \"" + legalCopyright + "\"" +
                                            "\n\t\t\tVALUE \"OriginalFilename\", \"" + originalFileName + "\"" +
                                            "\n\t\t\tVALUE \"ProductName\", \"" + productName + "\"" +
                                            "\n\t\t\tVALUE \"ProductVersion\", PRODUCT_VER" +
                                        "\n\t\tEND" +
                                    "\n\tEND" +
                                    "\n\tBLOCK \"VarFileInfo\"" +
                                    "\n\tBEGIN" +
                                        "\n\t\tVALUE \"Translation\", 0x419, 1252" +
                                    "\n\tEND" +
                                "\nEND";
            File.WriteAllText(fileName, textToSave);
        }

        public void loadFromRc(string fileName)
        {
            string result = File.ReadAllText(fileName);
        }

        public static bool saveSettings(rcInfo _settings)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(rcInfo));
                TextWriter writer = new StreamWriter("lastRc.xml");
                ser.Serialize(writer, _settings);
                writer.Close();
                return true;
            }
            catch { return false; }
        }

        public static bool loadSettings(ref rcInfo _rc)
        {
            try
            {
                XmlSerializer ser = new XmlSerializer(typeof(rcInfo));
                TextReader reader = new StreamReader("lastRc.xml", Encoding.UTF8);
                _rc = (rcInfo)ser.Deserialize(reader);
                reader.Close();
                return true;
            }
            catch { return false; }
        }
    }
}
