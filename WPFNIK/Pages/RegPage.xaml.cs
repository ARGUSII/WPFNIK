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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void ButtomBack_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void NewReg(object sender, RoutedEventArgs e)
        {
            bool number = false; // цифра

            if (TBLogin.Text.Length > 0) // проверяем логин
            {
                if (Pass.Password.Length > 0) // проверяем пароль
                {
                    if (PassPov.Password.Length > 0) // проверяем второй пароль
                    {
                        if (TBFIO.Text.Length > 0) // проверяем фио
                        {
                            if (Pass.Password.Length >= 6)
                            {
                                for (int i = 0; i < Pass.Password.Length; i++) // перебираем символы
                                {
                                    if (Pass.Password[i] >= '0' && Pass.Password[i] <= '9') number = true; // если цифры
                                }
                                if (!number)
                                {
                                    MessageBox.Show("Добавьте хотя бы одну цифру"); // выводим сообщение
                                }
                                else if (Pass.Password == PassPov.Password) // проверка на совпадение паролей
                                {
                                    PCpartsEntities db = new PCpartsEntities();
                                    USER userObject = new USER
                                    {
                                        FIO = TBFIO.Text,
                                        Login = TBLogin.Text,
                                        Password = Pass.Password,
                                        Role = Role.Text
                                    };
                                    db.USER.Add(userObject);
                                    db.SaveChanges();
                                    MessageBox.Show("Пользователь создан");
                                }
                                else
                                {
                                    MessageBox.Show("Пароли должны совпадать");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Пароль должен содержать 6 или более символов");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Укажите ФИО");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Повторите пароль");
                    }
                }
                else
                {
                    MessageBox.Show("Укажите пароль");
                }
            }
            else
            {
                MessageBox.Show("Укажите логин");
            }
        }

        private void Role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
