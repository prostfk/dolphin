using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Circles
{
    class FileUtil
    {
        public static void PutInWordFile(string resource, string[] data)
        {
            // Создаём объект документа
            Word.Document doc = null;
            try
            {
                // Создаём объект приложения
                Word.Application app = new Word.Application();
                // Путь до шаблона документа
                string source = resource;
                // Открываем
                doc = app.Documents.Open(source);
                doc.Activate();

                int i = 0;
                string text = DateTime.Now.ToString("dd MMMM yyyy | HH:mm:ss |->  ");
                foreach (string a in data)
                {

                    text += a;
                }
                Word.Range rng = doc.Range(0, 1);
                text += "\n";
                rng.Text = text;


                // Закрываем документ
                doc.Close();
                doc = null;
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, то
                // закрываем документ и выводим информацию
                doc.Close();
                doc = null;
                Console.WriteLine("Во время выполнения произошла ошибка!");
                Console.ReadLine();
            }
        }


        public static void PutInExcelFile(string path, string []data)
        {
            Microsoft.Office.Interop.Excel.Application ex = new Microsoft.Office.Interop.Excel.Application();
            //Отобразить Excel
            ex.Visible = false;
            //Количество листов в рабочей книге
            ex.SheetsInNewWorkbook = 2;
            //Добавить рабочую книгу
            Microsoft.Office.Interop.Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);
            //Отключить отображение окон с сообщениями
            ex.DisplayAlerts = false;
            //Получаем первый лист документа (счет начинается с 1)
            Microsoft.Office.Interop.Excel.Worksheet sheet = (Microsoft.Office.Interop.Excel.Worksheet)ex.Worksheets.get_Item(1);
            //Название листа (вкладки снизу)
            sheet.Name = "Лист 1";
            //Пример заполнения ячеек
            for (int i = 1; i <= data.Length; i++)
            {
                    sheet.Cells[1, i] = String.Format(data[i-1]);
            }
        }

    }
}
