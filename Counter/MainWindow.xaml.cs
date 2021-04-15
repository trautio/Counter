using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace Counter
{
    /// <summary>
    /// All the magic
    /// </summary>
    public partial class MainWindow : Window
    {
        private int currentCount;
        private int currentPoints;
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "/counter.txt";

        public MainWindow()
        {
            InitializeComponent();
            Get_Current();
        }

        // When a number is clicked add the number to current
        private void Nmbr_Click(object sender, RoutedEventArgs e)
        {
            string content = (sender as Button).Content.ToString();
            int value = Get_Value(content);
            currentPoints += value;
            currentCount++;
            Writer();
        }

        // Reset to 0
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            currentPoints = 0;
            currentCount = 0;
            Writer();
        }

        // Parser
        private int Get_Value(string nmbr)
        {
            int.TryParse(nmbr, out int v);
            return v;
        }

        //  Get current
        private void Get_Current()
        {           
            if (!File.Exists(path)) Writer();

            string[] lines = File.ReadAllLines(path);
            currentPoints = Get_Value(lines[1].Split(": ")[1]);
            currentCount = Get_Value(lines[0].Split(": ")[1]);
        }

        // Filewriter
        private void Writer()
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.WriteLine("Achievement count: " + currentCount.ToString());
                sw.WriteLine("Achievement points: " + currentPoints.ToString());
            }
        }
    }
}
