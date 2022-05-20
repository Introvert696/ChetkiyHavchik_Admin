using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace Четкий_Хавчик_Админ
{
    class exportClass
    {
        private FileInfo _fileInfo;

        public exportClass(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileInfo = new FileInfo(fileName);
            }
            else
            {
                throw new ArgumentException("File not found");
            }
        }
        public static void exportWord(string token)
        {
            /*
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
            */

           
        }

        internal bool Process(Dictionary<string,string> items)
        {
            Word.Application app = null;
            try 
            {
                app = new Word.Application();
                Object file = _fileInfo.FullName;

                Object missing = Type.Missing;

                app.Documents.Open(file);
                
                foreach(var item in items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord:false,
                        MatchWildcards:false,
                        MatchSoundsLike:missing,
                        MatchAllWordForms:false,
                        Forward:true,
                        Wrap:wrap,
                        Format:false,
                        ReplaceWith:missing,Replace:replace);
                }
                Object newFileName = Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString("yyyyMMdd HHmmss ")+ _fileInfo.Name);
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                }
                
            }
        }
    }
}
