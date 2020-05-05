using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Ex = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace excelWork.Fun
{
    public class excel
    {
        /// <summary>
        /// 简单操作Excel文件
        /// </summary>
        /// <param name="excelPath">excel 文件路径</param>
        /// <returns></returns>
        /// 

        private string exportFileName = string.Empty;
        private string _ModelFile = string.Empty;
        private string ExcelDir = Path.Combine(Application.StartupPath, "export");
        private string Extension = string.Empty;

        public string exportDir
        {
            get { return this.ExcelDir; }
        }
        public excel(string ModelFile)
        {
            if (File.Exists(ModelFile))
            {
                _ModelFile = ModelFile;
            }
            exportFileName = DateTime.Now.ToString("yyyyMMddHHmmss");
            ExcelDir = Path.Combine(ExcelDir, exportFileName);
            Directory.CreateDirectory(ExcelDir);
            Extension = Path.GetExtension(_ModelFile);
        }

        public bool exportExcel(Model model)
        {
            string excelPath=Path.Combine(ExcelDir,model.Name+model.Age.ToString()+Extension);
            File.Copy(_ModelFile, excelPath, true);
            return ExcelOp(excelPath, model);
        }

        public bool ExcelOp(string excelPath, Model model)
        {
            bool b = true;
            string ExcelFilePath = excelPath.Trim();
            Ex.Application app = new Ex.Application();
            Ex.Workbook wb = null;
            app.Visible = false;//设置调用引用的 Excel文件是否可见
            app.DisplayAlerts = false;
            wb = app.Workbooks.Open(ExcelFilePath);
            Ex.Worksheet ws = (Ex.Worksheet)wb.Worksheets[1]; //索引从1开始 //(Excel.Worksheet)wb.Worksheets["SheetName"];
            try
            {
                ws.Cells[3, 2] = model.Name;
                ws.Cells[3, 4] = model.Sex;
                ws.Cells[3, 6] = model.chushengdi;
                ws.Cells[4, 2] = model.BirthDay;
                ws.Cells[4, 4] = model.Age.ToString();
                ws.Cells[4, 6] = model.xueli;
                ws.Cells[5, 2] = model.hunyin;
                ws.Cells[5, 4] = model.CardID;

                ws.Cells[6, 2] = model.shengao;
                ws.Cells[6, 4] = model.tizhong;
                ws.Cells[6, 6] = model.phoneNub;


                ws.Cells[7, 2] = model.yuyan;
                ws.Cells[7, 4] = model.zuocainengli;

                ws.Cells[8, 2] = model.fuwutechang;
                ws.Cells[8, 4] = model.qita;

                ws.Cells[11, 1] = model.times;
                ws.Cells[11, 2] = model.Company;
                ws.Cells[11, 6] = model.position;
                ws.Cells[14, 1] = model.ziwopinjia;

            }
            catch (Exception ex) {
              //  XtraMessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                b = false;
            }
            finally
            {

                wb.Save();
                wb.Close();
                app.Quit();
            }

            return b;
        }


    }
}
