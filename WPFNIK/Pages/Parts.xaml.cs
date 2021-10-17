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
    /// Логика взаимодействия для Parts.xaml
    /// </summary>
    public partial class Parts : Page
    {
        public Parts()
        {
            InitializeComponent();
            DataGridPC.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
        }

        private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPCPage());
        }

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {

        }
        //Обновление таблицы с данными об автомобилях при каждой перезагрузке станицы
        private void CarPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                PCpartsEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridPC.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
            }
        }
    }
}
