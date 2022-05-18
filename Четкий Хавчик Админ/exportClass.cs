using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace Четкий_Хавчик_Админ
{
    class exportClass
    {
        public static void exportWord(string token)
        {
            List<Order> orderList = workWithAPI.getAllOrders(token);
            int i = 0;

            var application = new Word.Application();
            Word.Document document = application.Documents.Add();
            
            foreach(var order in orderList)
            {
                Word.Paragraph userParagraph = document.Paragraphs.Add();
                Word.Range userRange = userParagraph.Range;
                userRange.Text = order.Order_FIO;
                userParagraph.set_Style("Title");
                userRange.InsertParagraphAfter();

                Word.Paragraph tableParagraph = document.Paragraphs.Add();
                Word.Range tableRange = tableParagraph.Range;
                Word.Table paymentsTable = document.Tables.Add(tableRange, 2, 3);

            }
            application.Visible = true;

        }
    }
}
