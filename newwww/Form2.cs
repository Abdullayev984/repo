using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace newwww
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection youConnection = new SqlConnection("Data Source=WIN-6Q9786A8ST6\\SQLEXPRESS;Initial Catalog=telebeballar;Integrated Security=true");


        private void demonstrate(string verilenler)
        {

            using (SqlDataAdapter a = new SqlDataAdapter("SELECT *  FROM fennler where  id = '" +Convert.ToInt32( textBox3.Text) + "' ", youConnection))
            {
                //




                // fill a data table
                var t = new DataTable();
                a.Fill(t);

                // Bind the table to the list box
                listBox1.DisplayMember = "fenn_adi";
                listBox1.ValueMember = "fenn_adii";
                listBox1.DataSource = t;

            }

        }
        private void Form2_Load(object sender, EventArgs e)
        {
            listBox1.Name = "";
            textBox2.Visible = false;
            textBox3.Visible = false;
            label1.Visible = false;
            WindowState = FormWindowState.Maximized;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            demonstrate("SELECT *  FROM fennler where id = '" + textBox3.Text + "'");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                //  listBoxControl1.Items.Add(textEdit1.Text);
                // }
                //  SqlConnection sc = new SqlConnection();

                // sc.ConnectionString = ("Data Source=NEMET-PC\\SQLEXPRESS;Initial Catalog=mellim;Integrated Security=true");
                youConnection.Open();
                // SqlCommand com = new SqlCommand();
                // com.Connection = youConnection;
                //  com.CommandText = ("INSERT INTO  fennler (id,fenn_ad) VALUES (@idsi,@fenn_adi)",youConnection);

                SqlCommand cmd = new SqlCommand("insert into fennler(id,fenn_adi) values (@idsi,@fenn_adii)", youConnection);
                cmd.Parameters.AddWithValue("@idsi",Convert.ToInt32( textBox3.Text));
                cmd.Parameters.AddWithValue("@fenn_adii", textBox1.Text);

                cmd.ExecuteNonQuery();

                youConnection.Close();
                textBox1.Text = "";
                demonstrate("SELECT *  FROM fennler where id = '" + textBox3.Text + "'");

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.listBox1.SelectedIndex >= 0)
            {
                this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
                DialogResult result2 = MessageBox.Show("Bu fenni sildikde fenne aid bütün melumatlar silinəcək!!! Silmək istəyirsənsə Yes düyməsinə,silmək istəmirsənsə No düyməsinə basın",
   "Important Query",
   MessageBoxButtons.YesNo,
   MessageBoxIcon.Question);
                //}
                if (result2 == DialogResult.Yes)
                {
                    youConnection.Open();
                    SqlCommand cmd = new SqlCommand("delete from  fennler where  fenn_id=@fenn_idsi  ", youConnection);

                    cmd.Parameters.AddWithValue("@fenn_idsi", Convert.ToInt32(textBox2.Text));
                    cmd.ExecuteNonQuery();
                    // demonstrate("select *from fennler ");
                    demonstrate("SELECT *  FROM fennler where id = '" +Convert.ToInt32( textBox3.Text) + "'");
                    youConnection.Close();
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
                SqlConnection youConnection = new SqlConnection(str);
                string query = "select * from fennler where fenn_adi = '" + listBox1.Text + "' ";
                SqlCommand cmd = new SqlCommand(query, youConnection);
                SqlDataReader dbr;

                try
                {
                    youConnection.Open();
                    dbr = cmd.ExecuteReader();
                    while (dbr.Read())
                    {
                        // string fenn_adi = (string)dbr["fenn_ad"].ToString();
                        string fenn_idsi = (string)dbr["fenn_id"].ToString();
                        textBox2.Text = fenn_idsi;
                        Form3 frmmm = new Form3();
                        frmmm.textBox1.Text = textBox2.Text;
                        frmmm.Show();
                        this.Hide();

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Error");
                }


            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {

            string str = "server = WIN-6Q9786A8ST6\\SQLEXPRESS;Database=telebeballar;UID=sa;Password=0708593611;";
            SqlConnection youConnection = new SqlConnection(str);
            string query = "select * from fennler where fenn_adi = '" + listBox1.Text + "' ";
            SqlCommand cmd = new SqlCommand(query, youConnection);
            SqlDataReader dbr;

            try
            {
                youConnection.Open();
                dbr = cmd.ExecuteReader();
                while (dbr.Read())
                {
                    // string fenn_adi = (string)dbr["fenn_ad"].ToString();
                    string fenn_idsi = (string)dbr["fenn_id"].ToString();
                    textBox2.Text = fenn_idsi;


                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            fr.Show();
            this.Hide();

        }
    }
}
