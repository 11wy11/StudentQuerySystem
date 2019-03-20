using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;

namespace Whu_Navigation.Forms
{
    public partial class Information : Form
    {
        DataTable dt = new DataTable();  
        private string m_text;
        public Information(string text)
        {
            m_text = text;
           
            InitializeComponent();
        }

        private void Information_Load(object sender, EventArgs e)
        {

            listView1.View = View.Details;
            listView1.Columns.Add("ID", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("姓名", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("性别", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("学院", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("宿舍", 50, HorizontalAlignment.Center);
            listView1.Columns.Add("学号", 100, HorizontalAlignment.Center);
            listView1.Columns.Add("家庭住址", 130, HorizontalAlignment.Center);
            listView1.Items.Clear();
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+System.IO.Directory.GetCurrentDirectory()+"\\WHU_StudentQuery.mdb";
            OleDbConnection connection = new OleDbConnection(connectionString);
            //OleDbConnection conn = new OleDbConnection(mm);
            //MessageBox.Show("ok");
            connection.Open();
            string strsql = "select * from [Information] where [姓名]='"+m_text+"'";//或者like语句  
            //MessageBox.Show(strsql);
           // OleDbCommand cmd = new OleDbCommand(strsql, connecion);
            OleDbCommand odCommand = connection.CreateCommand();
            odCommand.CommandText = strsql;
            OleDbDataReader odrReader = odCommand.ExecuteReader();
            DataRow dr;
            int size = odrReader.FieldCount;
             for (int i = 0; i < size; i++)  
                {  
                    DataColumn dc;  
                    dc = new DataColumn(odrReader.GetName(i));  
                    dt.Columns.Add(dc);
                    //MessageBox.Show(dc.ColumnName);
                }  
                while (odrReader.Read())  
                {  
                    dr = dt.NewRow();  
                    for (int i = 0; i < size; i++)  
                    {  
                        dr[odrReader.GetName(i)] = odrReader[odrReader.GetName(i)].ToString();  
                    }  
                    dt.Rows.Add(dr);  
                }  
              
                ArrayList arr = new ArrayList(); 
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (!arr.Contains(dt.Rows[i]))
                    {
                        arr.Add(dt.Rows[i]);
                    }
                    ListViewItem li = new ListViewItem();
                    li.SubItems[0].Text = dt.Rows[i][0].ToString();
                    li.SubItems.Add(dt.Rows[i][1].ToString());
                    li.SubItems.Add(dt.Rows[i][2].ToString());
                    li.SubItems.Add(dt.Rows[i][3].ToString());
                    li.SubItems.Add(dt.Rows[i][4].ToString());
                    li.SubItems.Add(dt.Rows[i][5].ToString());
                    li.SubItems.Add(dt.Rows[i][6].ToString());
                    listView1.Items.Add(li);
                } 
                //关闭连接
                odrReader.Close();  
                connection.Close();    
  

            //OleDbDataAdapter ad = new OleDbDataAdapter();
            //ad.SelectCommand = cmd;
            //DataTable dt = new DataTable();
            //ad.Fill(dt);
            //ArrayList arr = new ArrayList(); 
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    if (!arr.Contains(dt.Rows[i]))
            //    {
            //        arr.Add(dt.Rows[i]);
            //    }
            //    ListViewItem li = new ListViewItem();
            //    li.SubItems[0].Text = dt.Rows[i][0].ToString();
            //    li.SubItems.Add(dt.Rows[i][1].ToString());
            //    li.SubItems.Add(dt.Rows[i][2].ToString());
            //    li.SubItems.Add(dt.Rows[i][3].ToString());
            //    listView1.Items.Add(li);
            //} 
        }
 

    }
}
