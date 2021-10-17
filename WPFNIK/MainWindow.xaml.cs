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
using WPFNIK.Pages;
using WPFNIK.Properties;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace WPFNIK
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MainFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            if (!(e.Content is Page page)) return;
            this.Title = $"WPFNIK - {page.Title}";
            if (page is AuthPage)
            {
                ButtonBack.Visibility = Visibility.Hidden;
            }
            else
            {
                ButtonBack.Visibility = Visibility.Visible;
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();
        }

        private void Button_OnCalc(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new Calc());
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            string path = "export.txt";
            StreamWriter sw = new StreamWriter(path);
            using (var db = new PCpartsEntities())
            {
                var users = db.USER.AsNoTracking().ToList();
                string IDline = String.Join(": ", db.USER.Select(x => x.ID));
                sw.WriteLine(IDline);
                string FIOline = String.Join(": ", db.USER.Select(x => x.FIO));
                sw.WriteLine(FIOline);
                string Loginline = String.Join(": ", db.USER.Select(x => x.Login));
                sw.WriteLine(Loginline);
                string Passline = String.Join(": ", db.USER.Select(x => x.Password));
                sw.WriteLine(Passline);
                string Roleline = String.Join(": ", db.USER.Select(x => x.Role));
                sw.WriteLine(Roleline);
                sw.Close();
            }
            Process.Start("notepad.exe", path);
        }

        private void Import(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            if (ofd.FileName != "") // проверка на выбор файла
            {
            }
            else MessageBox.Show("Файл для импорта не выбран!");
            StreamReader sr = new StreamReader(ofd.FileName); //открываем файл
            while (!sr.EndOfStream) // перебираем строки, пока они не закончены
            {
                string[] lines = new string[5];// массив данных
                for (int i = 0; i < 5; i++) // читаем 5 строк
                {
                    string line = sr.ReadLine(); // читаем строку
                    string[] data = line.Split(':');
                    line = ""; // обнуляем переменную
                    if (data.Length >= 2) // проверяем на целостность данных
                    {
                        for (int j = 1; j < data.Length; j++) // складываем данные
                        {
                            line += data[j]; // собираем строку
                        }
                    }
                    lines[i] = line; // записываем значения в массив
                }
                // выводим данные
                MessageBox.Show("Данные в файле: \nID: " + lines[0] +
                "\nФИО: " + lines[1] + "\nЛогин: " + lines[2] + "\nПароль: " +
                lines[3] + "\nРоль: " + lines[4]);


            }
        }
    }
}
