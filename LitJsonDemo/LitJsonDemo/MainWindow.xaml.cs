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
        private int FlagXP = 0x00000001;
        private int FlagTicket = 0x00000010;

        public MainWindow()
        {
            InitializeComponent();
        }

        public bool IsDropXP(AwardType _awardtype, GameType _gametype)
        {
            return ((DropDictionary_[_awardtype][_gametype.GetType().ToString()] & FlagXP) != 0);
        }

        public bool IsDropTicket(AwardType _awardtype, GameType _gametype)
        {
            return ((DropDictionary_[_awardtype][_gametype.GetType().ToString()] & FlagTicket) != 0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OpenJsonFile();
            ParseJsonFile();
            Test();
        }

        private void Test()
        {
            GameType[] gametypes = {new TeamDeathType(), new CaptureType(), new HotPotatoType()};
            foreach (AwardType type in Enum.GetValues(typeof(AwardType)))
            {
                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(" {0}, {1}, Ticket: {2}, XP:{3}", type.ToString(),
                                                                       gametypes[i],
                                                                       IsDropTicket(type, gametypes[i]),
                                                                       IsDropXP(type, gametypes[i]));
                }
                    
            }
        }

        private void ParseJsonFile()
        {
            JsonData jsondata = JsonMapper.ToObject(content_);

            var lines = jsondata["DropConfig"];
            for (int i = 0; i < lines.Count; i++)
            { 
                var line = lines[i];
                string sevent = line["Event"].ToString();
                AwardType type = (AwardType)Enum.Parse(typeof(AwardType), sevent);

                Dictionary<string, int> dropInfo = new Dictionary<string, int>();
                dropInfo.Add("TeamDeathType", GetFlag(line["TeamDeathType"].ToString() ));
                dropInfo.Add("CaptureType", GetFlag(line["CaptureType"].ToString()));
                dropInfo.Add("HotPotatoType", GetFlag(line["HotPotatoType"].ToString()));

                DropDictionary_.Add(type, dropInfo);
                //Console.WriteLine("--- {0} ---", sdrop);
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

        private int GetFlag(string _rawflags)
        {
            string[] flags = _rawflags.Split(new char[] { '&' });
            int iFlag = 0;
            if (flags.Contains("T"))
                iFlag |= FlagTicket;
            if (flags.Contains("X"))
                iFlag |= FlagXP;
            return iFlag;
        }
    }
}
