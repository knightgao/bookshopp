using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 书店管理系统
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }
        private static string userName;
        public static string UserName
        {
            get { return MainForm.userName; }
            set { MainForm.userName = value; }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OPToolStripStatusLabel1.Text = "操作员：" + userName;
        }

        private void 图书添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

    }
}
