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
    public partial class ExamplEukuGuanLi : Form
    {
        private SqlConnection conn;
        private SqlCommand commad, commad2;
        private SqlDataAdapter sda1, sda2;
        DataTable in_InfoTable = new DataTable();
        DataTable in_DetailTable = new DataTable();

        public ExamplEukuGuanLi()
        {
            InitializeComponent();
            conn = new SqlConnection(@"");

        }

        private void ExamplEukuGuanLi_Load(object sender, EventArgs e)
        {
            this.dateTimePicker1.Value = Convert.ToDateTime("2010-1-1");
            this.dateTimePicker2.Value = DateTime.Now;
            DateTime begin, end;
            begin = this.dateTimePicker1.Value;
            end = this.dateTimePicker2.Value;
            //根据填单时间来查询入库单信息,并显示到in_InfoDataGridView
            string sql = "select * from in_Info where 填单时间 between '" + begin + "'and '" + end + "'";
            sda1 = new SqlDataAdapter(sql, conn);



        }
    }
}
