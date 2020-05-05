using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using excelWork.Fun;
using System.Threading;

namespace excelWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string b = UserData.GetGenderByIdCard("440229199002261017");
            //int i=  UserData.GetAgeByIdCard("440229199002261017");
            //UserData data = new UserData();
            //string s = data.GetSF("440229199002261017");
            //string p,y,t;
            //data.GetRegionInfo("440229199002261017", out p, out y, out t);
            //  WebFuns web = new WebFuns();
            // web.GetUserAddress();

            string s=  Guid.NewGuid().ToString().Replace("-", "");
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerReportsProgress = true;
        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = this.progressBar1.Value + 1;
            this.label2.Text = this.progressBar1.Value.ToString();
        }
        UserData us = new UserData();
        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                MessageBox.Show("成功导出");
                string v_OpenFolderPath = e.Result.ToString();
                System.Diagnostics.Process.Start("explorer.exe", v_OpenFolderPath);
            }
            else
            {
                MessageBox.Show("无资料");
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<string[]> cards= us.GetCards();
            this.Invoke((MethodInvoker)delegate {
                this.progressBar1.Maximum = cards.Count;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Value = 0;
            });

            List<Model> ModelList = new List<Model>();
            for (int i = 0; i < cards.Count; i++)
            {
                ModelList.Add(us.GetModel(cards[i][0], cards[i][1]));
                worker.ReportProgress(i + 1);
            }
            if (ModelList.Count > 0)
            {

                this.Invoke((MethodInvoker)delegate
                {
                    this.progressBar1.Maximum = ModelList.Count;
                    this.progressBar1.Minimum = 0;
                    this.progressBar1.Value = 0;
                });
                excel ex = new excel(ModelFile);
                List<ManualResetEvent> manualEvents = new List<ManualResetEvent>();
                for (int i = 0; i < ModelList.Count; i++)
                {
                    AsyObject obj = new AsyObject();
                    obj.model = ModelList[i];
                    obj.ex = ex;
                    ManualResetEvent ma = new ManualResetEvent(false);
                    manualEvents.Add(ma);
                    obj.doevent = ma;
                    ThreadPool.QueueUserWorkItem(AsyncOperation, obj);

                    if (manualEvents.Count >= 60)
                    {
                        WaitHandle.WaitAll(manualEvents.ToArray());
                        manualEvents.Clear();
                    }
                }
                if (manualEvents.Count > 0)
                {
                    WaitHandle.WaitAll(manualEvents.ToArray());
                }

                e.Result = ex.exportDir;
            }
            

        }

        private void AsyncOperation(object state)
        {
            AsyObject obj = (AsyObject)state;
            bool b = obj.ex.exportExcel(obj.model);
            obj.doevent.Set();
            worker.ReportProgress(0);
        }
        private struct AsyObject
        {
            public Model model;
            public excel ex;
            public ManualResetEvent doevent;
        }
        private BackgroundWorker worker = new BackgroundWorker();
        private string ModelFile = string.Empty;
        public List<string> companys = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {

            ModelFile = this.textBox1.Text;
            if (string.IsNullOrEmpty(ModelFile))
            {
                MessageBox.Show("请设置Excel模版");
                return;
            }
            if (worker.IsBusy) return;
            worker.RunWorkerAsync();
            //excel ex = new excel(ModelFile);
            //bool b= ex.exportExcel(mo);
            //if (b)
            //{
            //    MessageBox.Show("可以了");
            //}
           // ex.ExcelOp(@"E:\excel\excelWork\excelWork\bin\Debug\m.xlsx");

        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}
