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

namespace Четкий_Хавчик_Админ
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string token = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            token = passTextBox.Text;
            bool resultCheckToken = workWithAPI.checkToken(token);
            if(resultCheckToken == true)
            {
                //загрузка формы работы
                AdminPanel _adminPanel = new AdminPanel();
                _adminPanel.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильный токен!");
            }
        }
    }
}
