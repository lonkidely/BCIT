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
using Parallel;

namespace homework
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> listWords = new List<string>();
        private string fileName;
        private string Text;
        public MainWindow()
        {
            InitializeComponent();
        }
        //Чтение из файла
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

                string[] words = Text.Split(' ', ',', '.', '?', '!', '/', '|', '"', '\n', '\t', '_', '-', '(', ')', '*', '{', '}', '[', ']');

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
        //Поиск слов, для которых заданная строка является подстрокой
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

        //Поиск похожих слов
        private void FindSimilarWords_Click(object sender, RoutedEventArgs e)
        {
            string sampleWord = SampleWord.Text.Trim();
            int maxDistance = Convert.ToInt32(MaxDistText.Text);
            int ThreadCount = Convert.ToInt32(MaxThreadCountText.Text);

            if (!string.IsNullOrWhiteSpace(sampleWord) && listWords.Count > 0)
            {
                //Результирующий список
                List<string> Result = new List<string>();

                //Деление списка на фрагменты для параллельного запуска в потоках
                List<MinMax> arrayDivList = SubArrays.DivideSubArrays(0, listWords.Count, ThreadCount);
                int count = arrayDivList.Count;


                //Количество потоков соответствует количеству фрагментов массива
                Task<List<string>>[] tasks = new Task<List<string>>[count];

                //Запуск потоков
                for (int i = 0; i < count; i++)
                {
                    //Создание временного списка, чтобы потоки не работали параллельно с одной коллекцией
                    List<string> tempTaskList = listWords.GetRange(arrayDivList[i].Min, arrayDivList[i].Max - arrayDivList[i].Min);

                    tasks[i] = new Task<List<string>>(
                        //Метод, который будет выполняться в потоке
                        ArrayThreadTask,
                        //Параметры потока передаются в виде кортежа, чтобы не создавать временный класс
                        new Tuple<List<String>, string,  int>(tempTaskList, sampleWord, maxDistance));
                    //Запуск потока
                    tasks[i].Start();
                }

                //Ожидание завершения всех потоков
                Task.WaitAll(tasks);

                //Объединение результатов полученных из разных потоков
                for (int i = 0; i < count; i++)
                {
                    //Добавление результатов конкретного потока в общий массив результатов
                    Result.AddRange(tasks[i].Result);
                }

                SimilarWordsBox.Items.Clear();
                foreach (string word in Result)
                {
                    SimilarWordsBox.Items.Add(word);
                }

            }
        }

        public static List<string> ArrayThreadTask(object paramObj)
        {
            //Получение параметров
            Tuple<List<string>, string, int> param = (Tuple<List<string>, string, int>)paramObj;
            int listCount = param.Item1.Count;

            //Временный список для результата
            List<string> tempData = new List<string>();

            string word = param.Item2;

            //Перебор нужных элементов в списке данных
            for (int i = 0; i < listCount; i++)
            {
                string temp = param.Item1[i];
                int dist = LevenshteinDistance(word, temp);
                if (dist <= param.Item3)
                {
                    tempData.Add(temp);
                }
            }

            //Возврат массива данных
            return tempData;
        }

        //Поиск расстояния Левенштейна
        public static int LevenshteinDistance(string str1, string str2)
        {
            //Проверка на исключительные случаи

            if ((str1 == null && str2 == null) || (str1 == str2)) return 0;
            if (str1 == null || str2 == null) throw new ArgumentNullException("Одна из строк пустая!\n");

            //Алгоритм Вагнера — Фишера

            int[,] matrix = new int[str1.Length + 1, str2.Length + 1];

            for (int i = 0; i <= str1.Length; i++)
            {
                matrix[i, 0] = i;
            }
            for (int j = 0; j <= str2.Length; j++)
            {
                matrix[0, j] = j;
            }

            for (int i = 1; i <= str1.Length; i++)
            {
                for (int j = 1; j <= str2.Length; j++)
                {
                    int d = 1;
                    if (str1[i - 1] == str2[j - 1]) d = 0;
                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + 1, matrix[i, j - 1] + 1), matrix[i - 1, j - 1] + d);

                }
            }
            return matrix[str1.Length, str2.Length];
        }
    }
}
