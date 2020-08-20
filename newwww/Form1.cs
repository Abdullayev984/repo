using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.Diagnostics;

namespace newwww
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
         SqlConnection myConnection = new SqlConnection("Data Source=WIN-6Q9786A8ST6\\SQLEXPRESS;Initial Catalog=telebeballar;Integrated Security=true");
         private void goster(string verilen)
         {

             using (SqlDataAdapter a = new SqlDataAdapter("SELECT * FROM Qruplar  ", myConnection))
             {
                 // fill a data table
                 var t = new DataTable();
                 a.Fill(t);

                 // Bind the table to the list box
                 listBox1.DisplayMember = "qrup_ad";
                 listBox1.ValueMember = "qrup_adi";
                 listBox1.DataSource = t;


             }
         }
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text != "") //textBox1.Text
            {
                //  listBoxControl1.Items.Add(textEdit1.Text);
                // }
                //  SqlConnection sc = new SqlConnection();

                // sc.ConnectionString = ("Data Source=NEMET-PC\\SQLEXPRESS;Initial Catalog=mellim;Integrated Security=true");
                myConnection.Open();
                SqlCommand com = new SqlCommand();
                com.Connection = myConnection;
                com.CommandText = ("INSERT INTO  Qruplar (qrup_ad) VALUES ('" + textBox1.Text.Trim() + "')");
                com.ExecuteNonQuery();

                myConnection.Close();
                textBox1.Text = "";
                goster("select * from  Qruplar");

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           textBox2.Visible = false;
            textBox3.Visible = false;
            WindowState = FormWindowState.Maximized;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            goster("select * from Qruplar");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
                DialogResult result2 = MessageBox.Show("Bu qrupu sildikde qrupa aid bütün melumatlar silinəcək!!! Silmək istəyirsənsə Yes düyməsinə,silmək istəmirsənsə No düyməsinə basın",
   "Important Query",
   MessageBoxButtons.YesNo,
   MessageBoxIcon.Question);
                //}
                if (result2 == DialogResult.Yes)
                {
                    myConnection.Open();
                    SqlCommand cmd = new SqlCommand("delete from  Qruplar where  id=@idsi  ", myConnection);
                    cmd.Parameters.AddWithValue("@idsi", Convert.ToInt64(textBox2.Text));//textBox2

                    cmd.ExecuteNonQuery();
                    goster("select * from  Qruplar");
                    myConnection.Close();
                }
            }
            else
                MessageBox.Show("Zəhmət olmasa qutudan siləcəyiniz qrupu seçin!!!");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {

                string str = "server = WIN-6Q9786A8ST6\\SQLEXPRESS;Database=telebeballar;UID=sa;Password=0708593611;";
                SqlConnection con = new SqlConnection(str);
                string query = "select * from  Qruplar where qrup_ad = '" + listBox1.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dbr;

                try
                {
                    con.Open();
                    dbr = cmd.ExecuteReader();
                    while (dbr.Read())
                    {
                        string qrup_adi = (string)dbr["qrup_ad"].ToString();
                        string idsi = (string)dbr["id"].ToString();
                        textBox3.Text = qrup_adi;
                        textBox2.Text = idsi;

                        Form2 frmm = new Form2();
                        frmm.textBox3.Text = textBox2.Text;
                        frmm.label1.Text = textBox3.Text;
                        frmm.label3.Text = textBox3.Text;
                        frmm.Show();
                        this.Hide();

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error");
                }


            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            string str = "server =WIN-6Q9786A8ST6\\SQLEXPRESS ;Database=telebeballar;UID=sa;Password=0708593611;";
            SqlConnection con = new SqlConnection(str);
            string query = "select * from Qruplar where qrup_ad = '" + listBox1.Text + "' ";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dbr;

            try
            {
                con.Open();
                dbr = cmd.ExecuteReader();
                while (dbr.Read())
                {
                    string qrup_adi = (string)dbr["qrup_ad"].ToString();
                    string idsi = (string)dbr["id"].ToString();
                    textBox3.Text = qrup_adi;
                    textBox2.Text = idsi;



                }
            }
            catch (Exception)
            {
                MessageBox.Show("Eror");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
