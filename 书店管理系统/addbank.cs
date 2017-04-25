using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using DataClass; 
namespace 书店管理系统
{
    public partial class addbank : Form
    {
        private SqlConnection conn;
        private SqlCommand commad, commad2;
        private SqlDataReader reader;
        private SqlDataAdapter sda;
        DataTable in_DetailTable = new DataTable();

        private void addbank_load(object sender, EventArgs e)
        {
            //通过查询数据库，生成入库单编号
            commad = new SqlCommand("select MAX(入库单ID) from in_Info", conn);
            conn.Open();
            this.text入库单IDBox.Text = (Convert.ToInt32(commad.ExecuteScalar()) + 1).ToString();
            conn.Close();
            this.text时间box.Text = DateTime.Now.ToString();
            this.check付款Box.CheckState = CheckState.Unchecked;
            //为dataGridView控件和DataTable实例dt添加数据结构
            sda = new SqlDataAdapter(@"select ID,图书ID,折扣,进价,数量,金额 from in_detail where 入库单ID = 0", conn);
            sda.Fill(in_DetailTable);
            this.dataGridView1.DataSource = in_DetailTable;
            dataGridView1.Columns["Column1"].DisplayIndex = 6;
        }

        public addbank()
        {
            InitializeComponent();
            conn = new SqlConnection(@"server=./;databse=bookshop;uid=Sa;pwd=123@abc");

        }

        private void clear()//Todo不懂
        {
            foreach (Control c in this.groupBox1.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
            }
        }

        private void text图书姓名box_TextChanged(object sender, EventArgs e)
        {
            string sql = "select * from book_Info where 图书名称='" + this.text图书姓名box.Text + "'";
            commad = new SqlCommand(sql, conn);
            conn.Open();
            reader = commad.ExecuteReader();
            if (reader.Read())
            {
                this.text作者Box.Text = reader["作者"].ToString();
                this.textISBNBox.Text = reader["ISBN"].ToString();
                this.text版次Box.Text = reader["版次"].ToString();
                this.text出版社Box.Text = reader["出版社"].ToString();
                this.text出版时间Box.Text = reader["出版时间"].ToString();
                this.text定价Box.Text = reader["定价"].ToString();
                this.text图书IDBox.Text = reader["图书ID"].ToString();
            }
            conn.Close();
        }

        private void text折扣Box_TextChanged(object sender, EventArgs e)
        {
            decimal zhekou = 0, dingjia = 0, jinjia = 0;
            try
            {
                dingjia = Convert.ToDecimal(this.text定价Box.Text);
                zhekou = Convert.ToDecimal(this.text折扣Box.Text);
                jinjia = dingjia * zhekou;
                this.text进价Box.Text = jinjia.ToString();
            }
            catch
            { }
        }

        private void text数量Box_TextChanged(object sender, EventArgs e)
        {
            decimal shuliang, jinjia, jinE;

            try
            {
                shuliang = Convert.ToDecimal(this.text数量Box.Text);
                jinjia = Convert.ToDecimal(this.text进价Box.Text);
                jinE = jinjia * shuliang;
                this.text金额Box.Text = jinE.ToString();
            }
            catch
            { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Cindex = e.ColumnIndex;
            if (Cindex == 0)
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //查看图书信息是否被改动过，若改动过，则被当作新书添加到book_Info 中，并取得新的图书ID
            int rows = 0;
            conn.Open();
            string sql = "select count(*) from book_info where 图书名称 = '" + text图书姓名box.Text + "'and 作者 = '" + text作者Box.Text + "' and ISBN = '" + textISBNBox.Text + "' and 版次 = '" + text版次Box.Text + "' and 出版社 = '" + text出版社Box.Text + "' and 出版时间 ='" + text出版时间Box.Text + "' and 定价 = '" + text定价Box.Text + "'";
            commad = new SqlCommand(sql, conn);
            rows = Convert.ToInt32(commad.ExecuteScalar());
            conn.Close();
            if (rows < 1)
            {
                string sql2 = "insert into book_info(图书名称,作者,出版社,ISBN,出版时间,版次,定价) values ('" + text图书姓名box.Text + "','" + text作者Box.Text + "','" + text出版社Box.Text + "','" + textISBNBox.Text + "','" + text出版时间Box.Text + "','" + text版次Box.Text + "','" + text定价Box.Text + "')";
                commad2 = new SqlCommand(sql2, conn);
                conn.Open();
                commad2.ExecuteNonQuery();
                conn.Close();
                commad = new SqlCommand("select MAX(图书ID) from book_Info", conn);
                conn.Open();
                this.text图书IDBox.Text = commad.ExecuteScalar().ToString();
                conn.Close();
            }
            //将图书ID、折扣、进价、数量、金额、等添加到dataTable实例DT中
            DataRow newrow = in_DetailTable.NewRow();
            newrow["图书ID"] = this.text图书IDBox.Text;
            newrow["折扣"] = this.text折扣Box.Text;
            newrow["进价"] = this.text进价Box.Text;
            newrow["数量"] = this.text数量Box.Text;
            newrow["金额"] = this.text金额Box.Text;
            in_DetailTable.Rows.Add(newrow);
            dataGridView1.DataSource = in_DetailTable;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 保存入库单信息到in_Info表
            string sql2 = "insert into in_Info(供应商,填单时间,经办人,付款,付款,入库) values('" + text供应商Box.Text + "','" + text时间box.Text + "','" + text经办人Box.Text + "','" + check付款Box.Checked + "','" + false + "')";
            commad2 = new SqlCommand(sql2, conn);
            conn.Open();
            commad2.ExecuteNonQuery();
            conn.Close();
            //保存入库图书详细信息到in_Detail表
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                string sql = "insert into in_Dtail(入库单ID,图书ID,折扣,进价,数量,金额) values ('" + text入库单IDBox.Text + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "','" + dataGridView1.Rows[i].Cells[4].Value + "','" + dataGridView1.Rows[i].Cells[5].Value + "')";
                commad = new SqlCommand(sql, conn);
                conn.Open();
                commad.ExecuteNonQuery();
                conn.Close();
            }
            //通过dataclass类的ruKuDanID字段传递入库单ID到入库单打印窗体
            //dataclass.ruKuDanID = Convert.ToInt32(this.text入库单IDBox.Text);

        }

        

        


    }
}
