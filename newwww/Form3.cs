using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace newwww
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=WIN-6Q9786A8ST6\\SQLEXPRESS;Initial Catalog=telebeballar;Integrated Security=true");

        private void verileri_goster(string veri)
        {
            SqlDataAdapter da = new SqlDataAdapter(veri, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void goster(string verilen)
        {
            Form1 fr = new Form1();

            using (SqlDataAdapter a = new SqlDataAdapter("SELECT * FROM Qruplar  ", conn))
            {
                // fill a data table
                var t = new DataTable();
                a.Fill(t);

                // Bind the table to the list box
                fr.listBox1.DisplayMember = "qrup_ad";
                fr.listBox1.ValueMember = "qrup_adi";
                fr.listBox1.DataSource = t;


            }
        }
       
      
        private void Form3_Load(object sender, EventArgs e)
        {
         
            //button5.Visible = false;
            groupBox1.Text = "";
           textBox1.Visible = false;
            this.dataGridView1.AllowUserToAddRows = false;

            WindowState = FormWindowState.Maximized;

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            this.Location = Screen.PrimaryScreen.WorkingArea.Location;

            dataGridView1.Height = 400;
            verileri_goster("select * from telebeler where fenn_id='" + textBox1.Text + "'");
            dataGridView1.Columns["fenn_id"].Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
                if (textBox2.Text != "")
                {
                   
                    SqlCommand cmd = new SqlCommand("insert into telebeler(sıra_No,fenn_id,ad,soyad,ata_adı,kollokium1,kollokium2,kollokium3,sərbəst_is,davamiyyət,imtahan_bal,yekun_bal) values (@sıra_Nosu,@fenn_idsi,@adi,@soyadi,@ata_adıi,@kollokium1i,@kollokium2i,@kollokium3i,@sərbəst_isi,@davamiyyəti,@imtahan_bali,@yekun_bali)", conn);
                    cmd.Parameters.AddWithValue("@sıra_Nosu", textBox2.Text);
                    cmd.Parameters.AddWithValue("@fenn_idsi",Convert.ToInt32( textBox1.Text));
                    cmd.Parameters.AddWithValue("@adi", textBox3.Text);

                    cmd.Parameters.AddWithValue("@soyadi", textBox4.Text);
                    cmd.Parameters.AddWithValue("@ata_adıi", textBox5.Text);
                    cmd.Parameters.AddWithValue("@kollokium1i", textBox6.Text);
                    cmd.Parameters.AddWithValue("@kollokium2i", textBox7.Text);
                    cmd.Parameters.AddWithValue("@kollokium3i", textBox8.Text);
                    cmd.Parameters.AddWithValue("@sərbəst_isi", textBox9.Text);
                    cmd.Parameters.AddWithValue("@davamiyyəti", textBox10.Text);
                    cmd.Parameters.AddWithValue("@imtahan_bali", textBox11.Text);
                    cmd.Parameters.AddWithValue("@yekun_bali", textBox12.Text);


                    cmd.ExecuteNonQuery();
                    verileri_goster("select * from telebeler where fenn_id='" + textBox1.Text + "'");
                   


                }
                else
                {
                    MessageBox.Show("sıra_No mütləq daxil etməlisiz!!!");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("sıra nömrəsini tam ədəd daxil etməlisiniz!!!");
            }
            conn.Close();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
               
               
            
               
               
               
       
           

               

            }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int x = dataGridView1.SelectedCells[0].RowIndex;
            string sıra_No = dataGridView1.Rows[x].Cells[0].Value.ToString();
            string fenn_id = dataGridView1.Rows[x].Cells[1].Value.ToString();
            string ad = dataGridView1.Rows[x].Cells[2].Value.ToString();
            string soyad = dataGridView1.Rows[x].Cells[3].Value.ToString();
            string ata_adı = dataGridView1.Rows[x].Cells[4].Value.ToString();
            string kollokium1 = dataGridView1.Rows[x].Cells[5].Value.ToString();
            string kollokium2 = dataGridView1.Rows[x].Cells[6].Value.ToString();
            string kollokium3 = dataGridView1.Rows[x].Cells[7].Value.ToString();
            string sərbəst_is = dataGridView1.Rows[x].Cells[8].Value.ToString();
            string davamiyyət = dataGridView1.Rows[x].Cells[9].Value.ToString();
            string imtahan_bal = dataGridView1.Rows[x].Cells[10].Value.ToString();
            string yekun_bal = dataGridView1.Rows[x].Cells[11].Value.ToString();
            textBox2.Text= sıra_No;                                             
             textBox3.Text = ad;
             textBox4.Text = soyad;
            textBox5.Text = ata_adı;
             textBox6.Text = kollokium1;
             textBox7.Text = kollokium2;
             textBox8.Text = kollokium3;
             textBox9.Text = sərbəst_is;
            textBox10.Text = davamiyyət;
             textBox11.Text = imtahan_bal;
             textBox12.Text = yekun_bal;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            try
            {
               
                SqlCommand cmd = new SqlCommand("delete from telebeler where sıra_No=@sıra_Nosu", conn);
                cmd.Parameters.AddWithValue("@sıra_Nosu", textBox13.Text);
                cmd.ExecuteNonQuery();
                verileri_goster("select * from telebeler where fenn_id='" + textBox1.Text + "'");
                textBox13.Text = "";
                
            }
            catch (Exception)
            {

                MessageBox.Show("silinəcək qrupun sıra nömrəsini düzgün daxil edin!!!");
            } conn.Close();
                
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("update  telebeler set fenn_id='" + textBox1.Text + "', ad='" + textBox3.Text + "',soyad='" + textBox4.Text + "',ata_adı='" + textBox5.Text + "',kollokium1='" + textBox6.Text + "',kollokium2='" + textBox7.Text + "',kollokium3='" + textBox8.Text + "',sərbəst_is='" + textBox9.Text + "',davamiyyət='" + textBox10.Text + "',imtahan_bal='" + textBox11.Text + "',yekun_bal='" + textBox12.Text + "' where sıra_No= '" + textBox2.Text + "'", conn);
                // cmd.Parameters.AddWithValue("@idsi", textBox12.Text);
                cmd.ExecuteNonQuery();
                verileri_goster("select * from telebeler where fenn_id='" + textBox1.Text + "'");
            }
            catch (Exception)
            {

                MessageBox.Show("məlumatlar sıraNo-ya görə dəyişdirilir.Ona görədə sıra nömrəsini dəyismək olmaz");
            }
         
            conn.Close();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
               
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 fr = new Form1();
            goster("select * from Qruplar");
            fr.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
            
   }
        
        

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);

            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));
            e.Graphics.DrawImage(bm, 0, 0);
     
        }
    

       
        }
    }

