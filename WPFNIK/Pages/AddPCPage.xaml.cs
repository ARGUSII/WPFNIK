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

namespace WPFNIK.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPCPage.xaml
    /// </summary>
    public partial class AddPCPage : Page
    {
        private PC _currentPC = new PC();

        public AddPCPage()
        {
            InitializeComponent();
            DataContext = _currentPC;
            Process.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
            Video.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
            Mather.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
            Oper.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
            ssd.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
            hdd.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
            BP.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (_currentPC.Processor == null)
                errors.AppendLine("Выберите процессор!");
            if (_currentPC.VideoCard == null)
                errors.AppendLine("Выберите видеокарту!");
            if (_currentPC.MotherBoard == null)
                errors.AppendLine("Выберите материнскую плату!");
            if (_currentPC.RAM == null)
                errors.AppendLine("Выберите оперативную память!");
            if (_currentPC.SSD == null)
                errors.AppendLine("Выберите SSD!");
            if (_currentPC.HDD == null)
                errors.AppendLine("Выберите жёсткий диск!");
            if (_currentPC.Power == null)
                errors.AppendLine("Выберите блок питания!");
            //Проверяем переменную errors на наличие ошибок
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            //Добавляем в объект PC новую запись
            if (_currentPC.PCID == 0)
                PCpartsEntities.GetContext().PC.Add(_currentPC);
            //Делаем попытку записи данных в БД о новом компьютере
            try
            {
                PCpartsEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные успешно сохранены!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
