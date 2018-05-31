using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Circles
{
    class FileUtil
    {
        public static void PutInWordFile(string[] data)
        {

            Microsoft.Office.Interop.Word.Application objWord = new Microsoft.Office.Interop.Word.Application();
            objWord.Visible = true;
            var doc = objWord.Documents.Add();
            var text = doc.Range();
            for (int i = 0; i < data.Length; i++)
            {
                text.Text += data[i];
            }

        }


        public static void PutInExcelFile(List<string[]>data)
        {
            excel(data);
        }

        private static void excel(List<string[]> data)
        {
            Microsoft.Office.Interop.Excel.Application ObjExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook ObjWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet ObjWorkSheet;
            ObjWorkBook = ObjExcel.Workbooks.Add(System.Reflection.Missing.Value);
            ObjWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)ObjWorkBook.Sheets[1];
            for(int listvar = 0; listvar < data.Count; listvar++)
            {
                string [] temp = data[listvar];
                for (int i = 0; i < temp.Length; i++)
                {
                    ObjWorkSheet.Cells[listvar+1, i+2] = temp[i];
                }
            }

            ObjExcel.Visible = true;
            ObjExcel.UserControl = true;
        }

    }
}
