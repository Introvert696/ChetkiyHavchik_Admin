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
using System.Windows.Shapes;

namespace Четкий_Хавчик_Админ
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        //обьявление массивов
        private List<Item> itemsArr = new List<Item>();
        private List<Order> ordersArr = new List<Order>();


        public AdminPanel()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            //обнавление массивов при запуске
            updateArr();
        }
        private void addArraysToViews()
        {
            //очистка листвьюшек
            orderList.Items.Clear();

            //добавление в листвьишки
            foreach (Order order in ordersArr)
            {
                orderList.Items.Add(order);
            }
        }
        //обновление всех массивов и запись их в listView
        private void updateArr()
        {
            //получение массивов
            itemsArr = workWithAPI.getAllItem();
            List<Order> rawOrdersArr = workWithAPI.getAllOrders(MainWindow.token);
            ordersArr = normalOrderList(rawOrdersArr);
            //добавление всех массивов в лист вью
            addArraysToViews();
        }
        private List<Order> normalOrderList(List<Order> rawOrderArr)
        {
            foreach (Order rawOrder in rawOrderArr)
            {
                foreach(Item item in itemsArr)
                {
                    if(rawOrder.Order_Purchases == item.Item_Id.ToString())
                    {
                        rawOrder.Order_Purchases = item.Item_Name;
                    }
                }
            }
            return rawOrderArr;
            
        }

        private void updateOrderListBtn_Click(object sender, RoutedEventArgs e)
        {
            updateArr();
        }
    }
}
