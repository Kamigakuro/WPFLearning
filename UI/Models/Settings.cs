using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using UI.ViewModels;

namespace UI.Models
{
    public static class Settings
    {
        public static List<string> Themes = new List<string>();


        private static bool _scanSubDir;
        private static bool _scanZips;
        private static bool _createRepCopy;
        private static bool _scanOnRun;
        private static bool _attentAlienFilesFromZip;
        private static bool _badReplays;
        private static bool _lowSizeReplays;
        private static bool _isDark;
        public static string Version;
        public static bool CheckUpdates;
        public static string UpdateURL;
        public static int MaxPathHistory;
        private static int _lowsizevalue;

        private static string _tempFolder;
        private static string _reservingfolder;

        private static string _badColor;
        private static string _lowSizeColor;
        private static string _theme;

        public static List<ColumnInfo> _columns;
        public static ObservableCollection<string> Pathes;

        public static bool ScanSubDir
        {
            get => _scanSubDir;
            set { if (_scanSubDir == value) return;
                _scanSubDir = value; 
            }
        }
        public static bool ScanZips
        {
            get => _scanZips;
            set {
                if (_scanZips == value) return;
                _scanZips = value;
            }
        }
        public static bool CreateReplayCopy
        {
            get => _createRepCopy;
            set {
                if (_createRepCopy == value) return;
                _createRepCopy = value;
            }
        }
        public static bool ScanOnRun
        {
            get => _scanOnRun;
            set {
                if (_scanOnRun == value) return;
                _scanOnRun = value;
            }
        }
        public static bool AttentAlienFiles
        {
            get => _attentAlienFilesFromZip;
            set {
                if (_attentAlienFilesFromZip == value) return;
                _attentAlienFilesFromZip = value;
            }
        }
        public static bool CheckBadReplays
        {
            get => _badReplays;
            set {
                if (_badReplays == value) return;
                _badReplays = value;
            }
        }
        public static bool CheckLowSizeReplays
        {
            get => _lowSizeReplays;
            set {
                if (_lowSizeReplays == value) return;
                _lowSizeReplays = value;
            }
        }
        public static bool IsDark
        {
            get => _isDark;
            set {
                if (_isDark == value) return;
                _isDark = value;
            }
        }
        public static string TempFolder
        {
            get => _tempFolder;
            set
            {
                if (_tempFolder == value) return;
                _tempFolder = value;
            }
        }
        public static string Reservingfolder
        {
            get => _reservingfolder;
            set
            {
                if (_reservingfolder == value) return;
                _reservingfolder = value;
            }
        }
        public static string BadColor
        {
            get => _badColor;
            set {
                if (_badColor == value) return;
                _badColor = value;
            }
        }
        public static string LowSizeColor
        {
            get => _lowSizeColor;
            set
            {
                if (_lowSizeColor == value) return;
                _lowSizeColor = value;
            }
        }
        public static string Theme
        {
            get => _theme;
            set
            {
                if (_theme == value) return;
                _theme = value;
            }
        }

        public static int LowSizeValue
        {
            get => _lowsizevalue;
            set
            {
                if (_lowsizevalue == value) return;
                _lowsizevalue = value;
            }
        }
        
        public static SnackManager snack = new SnackManager();
        public static InfoPanelVM panel = new InfoPanelVM();

        public static bool LoadSettings()
        {
            _columns = new List<ColumnInfo>();
            Pathes = new ObservableCollection<string>();
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load("settings.xml");
            System.Xml.Linq.XNode node = doc.Root.FirstNode;
            while (node != null)
            {
                if (node.NodeType == System.Xml.XmlNodeType.Element)
                {
                    System.Xml.Linq.XElement el = (System.Xml.Linq.XElement)node;
                    foreach (System.Xml.Linq.XElement element in el.Elements())
                    {
                        if (element.Name == "Version") Version = element.Value;
                        else if (element.Name == "CheckUpdate") CheckUpdates = Convert.ToBoolean(element.Value);
                        else if (element.Name == "UpdateURL") UpdateURL = element.Value;
                        else if (element.Name == "MaxPathHistory") MaxPathHistory = Convert.ToInt32(element.Value);
                        else if (element.Name == "ScanSubDirectories")
                        {
                            Boolean.TryParse(element.Value, out bool tmp);
                            ScanSubDir = tmp;
                        }
                        else if (element.Name == "ScanZips")
                        {
                            Boolean.TryParse(element.Value, out bool tmp);
                            ScanZips = tmp;
                        }
                        else if (element.Name == "TempFolder") TempFolder = element.Value;
                        else if (element.Name == "ReservingReplays")
                        {
                            Boolean.TryParse(element.Value, out bool tmp);
                            CreateReplayCopy = tmp;
                        }
                        else if (element.Name == "ReservingFolder") Reservingfolder = element.Value;
                        else if (element.Name == "ScanOnRun")
                        {
                            Boolean.TryParse(element.Value, out bool tmp);
                            ScanOnRun = tmp;
                        }
                        else if (element.Name == "AttentAlienFilesInZip")
                        {
                            Boolean.TryParse(element.Value, out bool tmp);
                            AttentAlienFiles = tmp;
                        }
                        else if (element.Name == "MarkBadReplays")
                        {
                            Boolean.TryParse(element.Value, out bool tmp);
                            CheckBadReplays = tmp;
                        }
                        else if (element.Name == "BadReplayColor") BadColor = element.Value;
                        else if (element.Name == "MarkLowSizeReplays")
                        {
                            Boolean.TryParse(element.Value, out bool tmp);
                            CheckLowSizeReplays = tmp;
                        }
                        else if (element.Name == "LowReplaysColor") LowSizeColor = element.Value;
                        else if (element.Name == "LowReplaysValue") LowSizeValue = Convert.ToInt32(element.Value);
                        else if (element.Name == "IsDark")
                        {
                            Boolean.TryParse(element.Value, out bool tmp);
                            IsDark = tmp;
                        }
                        else if (element.Name == "Theme") Theme = element.Value;
                        else if (element.Name == "Column")
                        {
                            ColumnInfo column = new ColumnInfo();
                            foreach (var attrib in element.Attributes())
                            {
                                if (attrib.Name == "Size") column.Size = Convert.ToInt32(attrib.Value);
                                else if (attrib.Name == "Name") column.Name = attrib.Value;
                                else if (attrib.Name == "Position") column.Position = Convert.ToInt32(attrib.Value);
                            }
                            _columns.Add(column);
                        }

                        else if (element.Name == "Path")
                            foreach (var attrib in element.Attributes()) if (attrib.Name == "x") Pathes.Add(attrib.Value);
                    }
                }
                node = node.NextNode;
            }
            
            return true;
        }
        public static bool SaveSettings()
        {
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load("settings.xml");
            System.Xml.Linq.XNode node = doc.Root.FirstNode;
            while (node != null)
            {
                if (node.NodeType == System.Xml.XmlNodeType.Element)
                {
                    XElement el = (System.Xml.Linq.XElement)node;
                    int z = 0;
                    foreach (System.Xml.Linq.XElement element in el.Elements())
                    {
                        if (element.Name == "Version") element.Value = Version;
                        else if (element.Name == "CheckUpdate") element.Value = CheckUpdates.ToString();
                        else if (element.Name == "UpdateURL") element.Value = UpdateURL = element.Value;
                        else if (element.Name == "MaxPathHistory") element.Value = MaxPathHistory.ToString();
                        else if (element.Name == "ScanSubDirectories") element.Value = ScanSubDir.ToString();
                        else if (element.Name == "ScanZips") element.Value = ScanZips.ToString();
                        else if (element.Name == "TempFolder") element.Value = TempFolder;
                        else if (element.Name == "ReservingReplays")element.Value = CreateReplayCopy.ToString();
                        else if (element.Name == "ReservingFolder")element.Value = Reservingfolder;
                        else if (element.Name == "ScanOnRun")element.Value = ScanOnRun.ToString();
                        else if (element.Name == "AttentAlienFilesInZip")element.Value = AttentAlienFiles.ToString();
                        else if (element.Name == "MarkBadReplays") element.Value = CheckBadReplays.ToString(); 
                        else if (element.Name == "BadReplayColor") element.Value = BadColor;
                        else if (element.Name == "MarkLowSizeReplays") element.Value = CheckLowSizeReplays.ToString(); 
                        else if (element.Name == "LowReplaysColor") element.Value = LowSizeColor;
                        else if (element.Name == "LowReplaysValue") element.Value = LowSizeValue.ToString();
                        else if (element.Name == "IsDark") element.Value = IsDark.ToString();                       
                        else if (element.Name == "Theme") element.Value = Theme;
                        else if (element.Name == "Column")
                        {
                            var name = element.Attribute("Name");
                            var column = _columns.Where(c => c.Name == name?.Value);
                            foreach (var attrib in element.Attributes())
                            {
                                if (attrib.Name == "Size") attrib.Value = column?.First().Size.ToString();
                                else if (attrib.Name == "Position") attrib.Value = column?.First().Position.ToString();
                            }
                        }
                        
                    }
                    if (el.Name == "History")
                    {
                        el.RemoveAll();
                        foreach (var path in Pathes)
                            el.Add(new XElement("Path", new XAttribute("x", path)));
                    }
                }
                node = node.NextNode;
            }
            doc.Save("settings.xml");
            return true;
        }
        public static bool CreateSettings()
        {
            XmlTextWriter textWritter = new XmlTextWriter(@"settings.xml", Encoding.UTF8);
            textWritter.WriteStartDocument();//writer
            textWritter.WriteStartElement("root");//root
            textWritter.WriteEndElement();//root
            textWritter.Close();//writer

            if (File.Exists("settings.xml"))
            {
                XmlDocument document = new XmlDocument();
                document.Load("settings.xml");
                XmlNode element = document.CreateElement("MainPreferences");
                document.DocumentElement?.AppendChild(element);

                XmlNode subElementz = document.CreateElement("Version");
                subElementz.InnerText = System.Windows.Forms.Application.ProductVersion;
                element.AppendChild(subElementz);
                subElementz = document.CreateElement("CheckUpdate");
                subElementz.InnerText = "False";
                element.AppendChild(subElementz);
                subElementz = document.CreateElement("UpdateURL");
                subElementz.InnerText = "http://google.com";
                element.AppendChild(subElementz);
                subElementz = document.CreateElement("MaxPathHistory");
                subElementz.InnerText = "5";
                element.AppendChild(subElementz);


                element = document.CreateElement("ScanPreferences");
                document.DocumentElement?.AppendChild(element);

                XmlNode subElement1 = document.CreateElement("ScanSubDirectories");
                subElement1.InnerText = "False";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("ScanZips");
                subElement1.InnerText = "False";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("TempFolder");
                subElement1.InnerText = "tmp";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("ReservingReplays");
                subElement1.InnerText = "False";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("ReservingFolder");
                subElement1.InnerText = " ";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("ScanOnRun");
                subElement1.InnerText = "False";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("AttentAlienFilesInZip");
                subElement1.InnerText = "False";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("MarkBadReplays");
                subElement1.InnerText = "True";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("BadReplayColor");
                subElement1.InnerText = "#FF0000";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("MarkLowSizeReplays");
                subElement1.InnerText = "True";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("LowReplaysColor");
                subElement1.InnerText = "#FFAA00";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("LowReplaysValue");
                subElement1.InnerText = "500";
                element.AppendChild(subElement1);


                element = document.CreateElement("ColumnsPreferences");
                document.DocumentElement?.AppendChild(element);
                string[] columns =
                {
                    "Size", "Map", "Status", "Player", "Vehicle", "Version", "DateTime", "Duration", "Respawn",
                    "Winner", "BattleType"
                };
                for (int i = 0; i < columns.Length; i++)
                {
                    subElement1 = document.CreateElement("Column");
                    XmlAttribute attribute = document.CreateAttribute("Name");
                    attribute.Value = columns[i];
                    subElement1.Attributes?.Append(attribute);
                    attribute = document.CreateAttribute("Size");
                    attribute.Value = "22";
                    subElement1.Attributes?.Append(attribute);
                    attribute = document.CreateAttribute("Position");
                    attribute.Value = i.ToString();
                    subElement1.Attributes?.Append(attribute);

                    element.AppendChild(subElement1);
                }

                element = document.CreateElement("History");
                document.DocumentElement?.AppendChild(element);


                element = document.CreateElement("DesingPreferences");
                document.DocumentElement?.AppendChild(element);

                subElement1 = document.CreateElement("IsDark");
                subElement1.InnerText = "False";
                element.AppendChild(subElement1);
                subElement1 = document.CreateElement("Theme");
                subElement1.InnerText = "Amber";
                element.AppendChild(subElement1);

                document.Save("settings.xml");
            }
            return true;
        }

    }
}
