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
    public partial class addbook : Form
    {
        private SqlConnection conn;
        private SqlCommand commad;
        
       // private SqlDataAdapter sda;
        DataTable in_DetailTable = new DataTable();
        public addbook()
        {
            InitializeComponent();
            conn = new SqlConnection(@"server=./;databse=bookshop;uid=sa;pwd=123@abc");
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            conn.Open();
            string sql = "select count(*) from book_info where 图书名称 = '" + text图书姓名box.Text + "'and 作者 = '" + text作者Box.Text + "' and ISBN = '" + textISBNBox.Text + "' and 版次 = '" + text版次Box.Text + "' and 出版社 = '" + text出版社Box.Text + "' and 出版时间 ='" + com出版时间Box.Text + "' and 定价 = '" + text定价Box.Text + "' and 本次 = '" + text本次Box.Text + "'and 简介 = '" + text简介Box.Text + "'";
            commad = new SqlCommand(sql, conn);
            commad.ExecuteNonQuery();
            
            conn.Close();
            

        }
    }
}
