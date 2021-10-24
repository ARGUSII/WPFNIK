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

        public AddPCPage(PC selectedPC)
        {
            InitializeComponent();

            if (selectedPC != null)
                _currentPC = selectedPC;

            DataContext = _currentPC;
        }

        private void ButtonSave_OnClick(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentPC.Processor))
                errors.AppendLine("Укажите процессор!");
            if (string.IsNullOrWhiteSpace(_currentPC.VideoCard))
                errors.AppendLine("Укажите видеокарту!");
            if (string.IsNullOrWhiteSpace(_currentPC.MotherBoard))
                errors.AppendLine("Укажите материнскую плату!");
            if (string.IsNullOrWhiteSpace(_currentPC.RAM))
                errors.AppendLine("Укажите оперативную память!");
            if (string.IsNullOrWhiteSpace(_currentPC.SSD))
                errors.AppendLine("Укажите SSD!");
            if (string.IsNullOrWhiteSpace(_currentPC.HDD))
                errors.AppendLine("Укажите жёсткий диск!");
            if (string.IsNullOrWhiteSpace(_currentPC.Power))
                errors.AppendLine("Укажите блок питания!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentPC.PCID == 0)
            {
                PCpartsEntities.GetContext().PC.Add(_currentPC);
            }
            
            try
            {
                PCpartsEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
