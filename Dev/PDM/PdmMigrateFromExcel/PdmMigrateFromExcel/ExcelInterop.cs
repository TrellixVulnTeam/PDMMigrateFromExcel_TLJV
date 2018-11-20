using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace PdmMigrateFromExcel
{
    class ExcelInterop
    {
        //string wbPath = "C:\Users\jevans\Desktop\Harrison\harrisonDrawingMigration.xlsx";
        public List<PartData> GetPartData(string wbPath)
        {
            Excel.Application excelApp = null;
            Excel.Workbooks books = null;
            Excel.Workbook wb = null;
            Excel.Worksheet ws = null;
            Excel.Range rng = null;
            List<PartData> partsData = new List<PartData>();

            // Open spreadsheet
            try
            {
                excelApp = new Excel.Application();
                books = excelApp.Workbooks;
                books.Open(wbPath);
                excelApp.Visible = false;
                wb = excelApp.ActiveWorkbook;
                ws = wb.Sheets["Import"];
                rng = ws.Range["A1:K5000"];
                




                // Loop through spreadsheet


                int i = 2;
                string filePath = rng[i, 1].Value();
                while (!string.IsNullOrWhiteSpace(filePath))
                {
                    PartData part = new PartData();
                    part.LocalPath = rng[i, 1].Value;
                    part.DestFolderName = rng[i, 3].Value;
                    part.Number = rng[i, 4].Value;
                    part.PartNumbers = rng[i, 5].Value;
                    part.Revision = rng[i, 6].Value;
                    part.Title = rng[i, 8].Value;
                    part.Material = rng[i, 9].Value;
                    part.DocType = rng[i, 10].Value;
                    part.DrawnBy = rng[i, 11].Value;


                    partsData.Add(part);
                    i++;
                    filePath = rng[i, 1].Value();
                }
                wb.Close();
                excelApp.Quit();

                return partsData;
            }
            finally
            {
                if (rng != null) Marshal.ReleaseComObject(rng);
                if (ws != null) Marshal.ReleaseComObject(ws);
                if (wb != null) Marshal.ReleaseComObject(wb);
                if (books != null) Marshal.ReleaseComObject(books);
                if (excelApp != null) Marshal.ReleaseComObject(excelApp);
            }
        }
    }
}

