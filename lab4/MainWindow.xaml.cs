using System;
using System.Collections.Generic;
using System.IO;
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
using System.Threading;

namespace lab4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Список слов
        /// </summary>
        List<string> listWords = new List<string>();
        private string fileName;
        private string Text;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Content.Items.Clear();
            listWords.Clear();
            fileName = "";
            PathFile.Text = "";
            Text = "";

            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog();
            fileDialog.Filter = "Только текстовые файлы|*.txt";
            if (fileDialog.ShowDialog() == true)
            {
                Stopwatch timeLoading = new Stopwatch();
                timeLoading.Start();

                fileName = fileDialog.FileName;
                PathFile.Text = fileName;

                Text = File.ReadAllText(fileName);

                string[] words = Text.Split(' ', ',', '.', '!', '?');

                foreach (string word in words)
                {
                    if (!listWords.Contains(word))
                    {
                        listWords.Add(word);
                    }
                }

                timeLoading.Stop();
                ElapsedTime.Text = timeLoading.Elapsed.ToString();

                foreach (string word in listWords)
                {
                    Content.Items.Add(word);
                }

            }
        }

        private void FindWordButton_Click(object sender, RoutedEventArgs e)
        {
            FoundWords.Items.Clear();

            if (FindWordField.Text == null)
                return;

            Stopwatch timeSearching = new Stopwatch();
            timeSearching.Start();

            foreach (string word in listWords)
            {
                if (word.Contains(FindWordField.Text))
                {
                    FoundWords.Items.Add(word);
                }
            }

            timeSearching.Stop();
            SearchingTime.Text = timeSearching.Elapsed.ToString();
            FindWordField.Text = "";
        }
    }
}
