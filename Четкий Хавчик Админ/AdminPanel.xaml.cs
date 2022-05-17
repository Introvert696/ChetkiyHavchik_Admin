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
            itemsList.Items.Clear();

            //добавление в листвью заказы
            foreach (Order order in ordersArr)
            {
                if(order.Order_State == 0)
                {
                    orderList.Items.Add(order);
                }
            }
            //добавление в листвью предметы
            foreach (Item item in itemsArr)
            {
                itemsList.Items.Add(item);
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
                foreach (Item item in itemsArr)
                {
                    if (rawOrder.Order_Purchases == item.Item_Id.ToString())
                    {
                        rawOrder.Order_Purchases = item.Item_Name;
                    }
                }
            }
            return rawOrderArr;

        }
        //вставка значений в поля во вкладке "Заказы"
        private void setOrderValToTextBox(Order order)
        {
            idOrderTextBox.Text = order.Order_id.ToString();
            fioOrderTextBox.Text = order.Order_FIO;
            numOrderTextBox.Text = order.Order_Number;
            emailOrderTextBox.Text = order.Order_Email;
            addressOrderTextBox.Text = order.Order_Address;
            purchesesOrderTextBox.Text = order.Order_Purchases;
            priceOrderTextBox.Text = order.Order_Price.ToString();
            commentOrderTextBox.Text = order.Order_Comment;
            dateOrderTextBox.Text = order.Order_Date;

        }
        //вставка значений в поля во вкладке "Предметы"
        private void setItemValToTextBox(Item item)
        {
            idItemTextBox.Text = item.Item_Id.ToString();
            nameItemTextBox.Text = item.Item_Name;
            deskItemTextBox.Text = item.Item_Desk;
            pictItemTextBox.Text = item.Item_Pict;
            priceItemTextBox.Text = item.Item_Price.ToString();
        }
        //получение значений с textbox предметов
        private string[] getArrWithItemProp()
        {
            string[] properites = new string[5];
            properites[0] = nameItemTextBox.Text;
            properites[1] = deskItemTextBox.Text;
            properites[2] = pictItemTextBox.Text;
            properites[3] = priceItemTextBox.Text;
            properites[4] = idItemTextBox.Text;
            return properites;
        }
        //очистка значений в полях во вкладке "Заказы"
        private void clearOrderTextBox()
        {
            idOrderTextBox.Text = "";
            fioOrderTextBox.Text = "";
            numOrderTextBox.Text = "";
            emailOrderTextBox.Text = "";
            addressOrderTextBox.Text = "";            
            purchesesOrderTextBox.Text = "";
            priceOrderTextBox.Text = "";
            commentOrderTextBox.Text = "";
            dateOrderTextBox.Text = "";
        }
        //очистка значений в полях во вкладке предметы
        private void clearItemsTextBox()
        {
            idItemTextBox.Text = "";
            nameItemTextBox.Text = "";
            deskItemTextBox.Text = "";
            priceItemTextBox.Text = "";
            pictItemTextBox.Text = "";
        }

        private void updateOrderListBtn_Click(object sender, RoutedEventArgs e)
        {
            updateArr();
        }

        

        private void doneOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                workWithAPI.doneOrder(Convert.ToInt32(idOrderTextBox.Text), MainWindow.token);
                updateArr();
                clearOrderTextBox();
            }
            catch
            {
                MessageBox.Show("Не выбран заказ");
                updateArr();
            }
        }
       
        
        private void updateItemBtn_Click(object sender, RoutedEventArgs e)
        {
            updateArr();
        }
        private void orderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Order selectedOrder = orderList.SelectedItem as Order;
                setOrderValToTextBox(selectedOrder);
            }
            catch
            {
                //обработка "фантомного" изменения
            }
        }
        private void itemsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Item seletionItem = itemsList.SelectedItem as Item;
                setItemValToTextBox(seletionItem);
            }
            catch
            {
                //обработка "фантомного" изменения
            }

        }

        private void addItemBtn_Click(object sender, RoutedEventArgs e)
        {
            //пытаемся добавить элемент в массив
            try
            {
                //получаем значения всех полей
                string[] itemsProps = getArrWithItemProp();
                //создаем предмет в БД
                workWithAPI.createItem(MainWindow.token, itemsProps[0], itemsProps[1], itemsProps[2], itemsProps[3]);
                //выводим что предмет создан
                MessageBox.Show("Предмет создан");
                //очистка полей
                clearItemsTextBox();
                //обновление листвью
                updateArr();
            }
            catch
            {

            }
        }

        private void clearItemBtn_Click(object sender, RoutedEventArgs e)
        {
            clearItemsTextBox();
        }

        private void deleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                workWithAPI.deleteItem(MainWindow.token, Convert.ToInt32(idItemTextBox.Text));
                updateArr();
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                updateArr();
            }
        }

        private void updateItemBtn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //получаем значения всех полей
                string[] itemsProps = getArrWithItemProp();
                //создаем предмет в БД
                workWithAPI.updateItem(MainWindow.token,itemsProps[4], itemsProps[0], itemsProps[1], itemsProps[2], itemsProps[3]);
                //выводим что предмет изменен
                MessageBox.Show("Предмет изменен");
                //очистка полей
                clearItemsTextBox();
                //обновление листвью
                updateArr();
            }
            catch
            {

            }
        }

        private void deleteOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //получаем id заказа
                int orderId = Convert.ToInt32(idOrderTextBox.Text);
                //оправляем запрос на удаление
                workWithAPI.deleteOrder(orderId, MainWindow.token);
                //выводим что заказ удален
                MessageBox.Show("Заказ удален");
                //очистка полей
                clearItemsTextBox();
                //обновление листвью
                updateArr();
            }
            catch
            {

            }
        }
    }
}
