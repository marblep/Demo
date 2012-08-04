using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using LitJson;

namespace LitJsonDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string content_;

        public enum AwardType
        {
            Kill=0, Assist, DoubleKill, TripleKill, GetSelfFlag,
        }
        private Dictionary<AwardType, Dictionary<string, int>> DropDictionary_ = new Dictionary<AwardType, Dictionary<string, int>>();
        private TeamDeathType teamdeath_ = new TeamDeathType();
        

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OpenJsonFile();
            ParseJsonFile();
        }

        private void ParseJsonFile()
        {
            JsonData jsondata = JsonMapper.ToObject(content_);

            var lines = jsondata["DropConfig"];
            for (int i = 0; i < lines.Count; i++)
            { 
                var line = lines[i];

            }
        }

        private void OpenJsonFile()
        { 
            string path = System.IO.Path.Combine(System.Environment.CurrentDirectory, @"..\..\DropConfig.txt");
            if(File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    content_ = sr.ReadToEnd();
                }
            }
            else
            {
                MessageBox.Show("Can't find the file");
            }
        }
    }
}
