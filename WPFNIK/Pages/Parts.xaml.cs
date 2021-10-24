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
            NavigationService.Navigate(new AddPCPage(null));
        }

        private void ButtonDel_OnClick(object sender, RoutedEventArgs e)
        {
            var pcForRemoving = DataGridPC.SelectedItems.Cast<PC>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить записи в количестве {pcForRemoving.Count()} элементов?", "Внимание", 
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PCpartsEntities.GetContext().PC.RemoveRange(pcForRemoving);
                    PCpartsEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно удалены!");
                    DataGridPC.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPCPage((sender as Button).DataContext as PC));
        }
        //Обновление таблицы с данными об автомобилях при каждой перезагрузке станицы
        private void PCPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                PCpartsEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                DataGridPC.ItemsSource = PCpartsEntities.GetContext().PC.ToList();
            }
        }
    }
}
