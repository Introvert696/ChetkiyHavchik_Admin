using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Четкий_Хавчик_Админ
{
    class workWithAPI
    {
        public static bool checkToken(string token)
        {
            string url = "http://localhost/token";
            try {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST"; //метод отправки POST
                                         //данные для отправки, разделяются &
                string data = "token=" + token;
                //преобразуем данные в массив байтов
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                //устанавливаем тип содержимого - параметр ContentType
                request.ContentType = "application/x-www-form-urlencoded";
                //устанавливаем заголовок Content-Lenght запроса
                request.ContentLength = byteArray.Length;

                //записываем данные в поток запроса
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                string responseText = "";

                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                if (responseText == "true")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex){
                MessageBox.Show(ex.Message.ToString());
                return false;
            }
            
        }
        // Получаем все предметы с БД
        public static List<Item> getAllItem()
        {
            string url = "http://localhost/items";
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET"; //метод отправки POST

                string responseText = "";

                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                List<Item> itemArr = stringItemToStringArray(responseText);
                return itemArr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }
            
        }
        // Получаем все заказы с бд
        public static List<Order> getAllOrders(string token)
        {
            string url = "http://localhost/order";
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "POST"; //метод отправки POST
                                         //данные для отправки, разделяются &
                string data = "token=" + token;
                //преобразуем данные в массив байтов
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
                //устанавливаем тип содержимого - параметр ContentType
                request.ContentType = "application/x-www-form-urlencoded";
                //устанавливаем заголовок Content-Lenght запроса
                request.ContentLength = byteArray.Length;

                //записываем данные в поток запроса
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                string responseText = "";

                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        responseText = reader.ReadToEnd();
                    }
                }
                
                List<Order> orderArr = stringOrderToStringArray(responseText);
                int i = 0;
                return orderArr;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return null;
            }

        }
        // Преобразовывает строку в лист обьекта Item
        private static List<Item> stringItemToStringArray(string rawString)
        {
            //Foo json  = JsonConvert.DeserializeObject<Foo>(str)
            rawString = rawString.Remove(0, 1);
            rawString = rawString.Remove(rawString.Length - 1);
            List<String> stringArr = rawString.Split("}").ToList<String>();
            stringArr.RemoveAt(stringArr.Count-1);

            
            List<Item> itemArr = new List<Item>();
            foreach(string str in stringArr)
            {
                string strTemp =str+ "}";
                if (strTemp[0] == ',')
                {
                    strTemp = strTemp.Remove(0, 1);
                }
                Item rawItem = JsonConvert.DeserializeObject<Item>(strTemp);
                itemArr.Add(rawItem);
            }
            
            return itemArr;
        }



        //==============================================================================//
        //                            Методы помощники                                  //
        //==============================================================================//

        // Преобразовывает строку в лист обьекта Order  
        private static List<Order> stringOrderToStringArray(string rawString)
        {
            //Foo json  = JsonConvert.DeserializeObject<Foo>(str)
            rawString = rawString.Remove(0, 1);
            rawString = rawString.Remove(rawString.Length - 1);
            List<String> stringArr = rawString.Split("}").ToList<String>();
            stringArr.RemoveAt(stringArr.Count - 1);


            List<Order> orderArr = new List<Order>();
            foreach (string str in stringArr)
            {
                string strTemp = str + "}";
                if (strTemp[0] == ',')
                {
                    strTemp = strTemp.Remove(0, 1);
                }
                Order rawItem = JsonConvert.DeserializeObject<Order>(strTemp);
                orderArr.Add(rawItem);
            }
            
            return orderArr;
        }
    }
}
