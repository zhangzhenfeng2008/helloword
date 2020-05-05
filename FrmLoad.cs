using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MysqlClient
{
    public partial class FrmLoad : Form
    {
        SocketClient.Connections conn;
        ListView lv;
        AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        BackgroundWorker worker;
        public FrmLoad(SocketClient.Connections _conn,ListView lsciew)
        {
            InitializeComponent();
            conn = _conn;
            lv = lsciew;
            worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.ProgressChanged += worker_ProgressChanged;
            worker.WorkerReportsProgress = true;


        }

        void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.label1.Text = "下載文件："+e.UserState.ToString();
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("下載完成");
            this.Close();
        }
        string CurrentFileName = string.Empty;

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (string s in FileName)
            {
                conn.GetFile(s);
                CurrentFileName = s;
                worker.ReportProgress(0, s);
                autoResetEvent.WaitOne();
            }
            
        }
        List<string> FileName;
        private void FrmLoad_Load(object sender, EventArgs e)
        {
            //SocketClient.Conn.FileServer.close();
            conn.OnReceive += conn_OnReceive;
            conn.CFilesCompleted += conn_CFilesCompleted;
            FileName = new List<string>();
            foreach (ListViewItem item in lv.SelectedItems)
            {
                FileName.Add(item.Text);
            }
            worker.RunWorkerAsync();
        }

        void conn_CFilesCompleted(string FileName)
        {
            if (CurrentFileName == FileName)
            {
                autoResetEvent.Set();
            }
        }
        string fullPath = Path.Combine(Application.StartupPath, "LoadFile");

        void conn_OnReceive(object sender, SocketClient.Handler.CommReceiveEventArgs e)
        {
            //throw new NotImplementedException();

            if (e.MethodId == "GetFile")
            {
                SocketClient.Model.FileModel model= SocketClient.Command.CommAnalysis.getFileModel(e.Message);
                string FilePath = Path.Combine(fullPath, model.FileName);
                FileStream MyFileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write);
                MyFileStream.Write(model.FileByte, 0, model.FileByte.Length);
                MyFileStream.Close();
                if (CurrentFileName == model.FileName)
                {
                    autoResetEvent.Set();
                }
            }
        }

        private void FrmLoad_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.OnReceive -= conn_OnReceive;
            conn.CFilesCompleted -= conn_CFilesCompleted;
          
        }
    }
}
