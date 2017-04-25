using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace 书店管理系统
{
    public partial class book_Info : Form
    {
        private SqlConnection conn;
        private SqlDataAdapter sda;
        DataSet ds = new DataSet();
        public book_Info()
        {
            InitializeComponent();
            conn = new SqlConnection(@"server=./;databse=bookshop;uid=Sa;pwd=123@abc");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sql = "select * from book_Info where 图书名称 = '"+ text图书名称Box.Text+ "'";
            sda = new SqlDataAdapter(sql, conn);
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0].DefaultView;
        }
    }
}
