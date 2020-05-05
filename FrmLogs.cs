using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace MysqlClient
{
    public partial class FrmLogs : Form
    {
        SocketClient.Connections conn;
        public FrmLogs(SocketClient.Connections _conn)
        {
            conn = _conn;
            InitializeComponent();
        }

        private void FrmLogs_Load(object sender, EventArgs e)
        {
            this.maskedTextBox1.Text = DateTime.Now.AddDays(-7).ToString();
            this.maskedTextBox2.Text = DateTime.Now.ToString();
            conn.OnReceive += conn_OnReceive;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            maskedTextBox1.GotFocus += maskedTextBox1_GotFocus;
            maskedTextBox1.MouseUp += maskedTextBox1_MouseUp;
            maskedTextBox1.Tag = false;
            maskedTextBox2.MouseUp += maskedTextBox1_MouseUp;
            maskedTextBox2.GotFocus += maskedTextBox1_GotFocus;
            maskedTextBox2.Tag = false;
			2321321
			timeee
			123
        }

        void maskedTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MaskedTextBox box = (MaskedTextBox)sender;
            //如果鼠标左键操作并且标记存在，则执行全选             
            if (e.Button == MouseButtons.Left && (bool)box.Tag == true)
            {
                box.SelectAll();
            }

            //取消全选标记              
            box.Tag = false;   
        }

        void maskedTextBox1_GotFocus(object sender, EventArgs e)
        {
            MaskedTextBox box = (MaskedTextBox)sender;
            box.Tag = true;
            box.SelectAll();
        }

        void conn_OnReceive(object sender, SocketClient.Handler.CommReceiveEventArgs e)
        {
            if (e.MethodId == "Getlogs")
            {
                List<SocketClient.Model.oplog> mlist = (List<SocketClient.Model.oplog>)e.ObjData;
                this.Invoke(new MethodInvoker(delegate() {
                    this.dataGridView1.DataSource = mlist;
                }));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.maskedTextBox1.Text) || string.IsNullOrEmpty(this.maskedTextBox2.Text)) return;
            conn.Getlogs(this.maskedTextBox1.Text, this.maskedTextBox2.Text);
        }

        private void FrmLogs_FormClosing(object sender, FormClosingEventArgs e)
        {
            conn.OnReceive -= conn_OnReceive;
        }
    }
}
