using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KutahyaEmlakYonetimSistemi
{
    public partial class CalısanYonetimi : Form
    {
        public CalısanYonetimi()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //emlakçı adı
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //emlakçı soyadı
        }

        private void Telefon_Click(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //emlakçı telefon
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //komisyon oranı
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcıdan alınan bilgiler
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            string telefon = textBox3.Text;
            int komisyonOrani = int.Parse(textBox4.Text);
            string pozisyon = textBox5.Text;

            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Calisanlar (Ad, Soyad, Telefon, Pozisyon, KomisyonOrani) VALUES (@Ad, @Soyad, @Telefon, @Pozisyon, @KomisyonOrani )";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Ad", ad);
                        command.Parameters.AddWithValue("@Soyad", soyad);
                        command.Parameters.AddWithValue("@Telefon", telefon);
                        command.Parameters.AddWithValue("@Pozisyon", pozisyon);
                        command.Parameters.AddWithValue("@KomisyonOrani", komisyonOrani);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Çalışan başarıyla eklendi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
            RefreshDataGrid();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            string telefon = textBox3.Text;
            int komisyonOrani = int.Parse(textBox4.Text);
            string pozisyon = textBox5.Text;

            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Calisanlar WHERE Ad = @Ad AND Soyad = @Soyad AND Telefon = @Telefon AND Pozisyon= @Pozisyon";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Ad", ad);
                        command.Parameters.AddWithValue("@Soyad", soyad);
                        command.Parameters.AddWithValue("@Telefon", telefon);
                        command.Parameters.AddWithValue("@Pozisyon", pozisyon);
                        command.Parameters.AddWithValue("@KomisyonOrani", komisyonOrani);
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Çalışan başarıyla silindi.");
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen çalışan bulunamadı.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
            RefreshDataGrid();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            string ad = textBox1.Text;
            string soyad = textBox2.Text;
            string telefon = textBox3.Text;
            int komisyonOrani = int.Parse(textBox4.Text);
            string pozisyon = textBox5.Text;

            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Calisanlar SET KomisyonOrani = @KomisyonOrani WHERE Ad = @Ad AND Soyad = @Soyad AND Telefon = @Telefon AND Pozisyon= @Pozisyon ";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Ad", ad);
                        command.Parameters.AddWithValue("@Soyad", soyad);
                        command.Parameters.AddWithValue("@Telefon", telefon);
                        command.Parameters.AddWithValue("@Pozisyon", pozisyon);
                        command.Parameters.AddWithValue("@KomisyonOrani", komisyonOrani);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Çalışan başarıyla güncellendi.");
                        }
                        else
                        {
                            MessageBox.Show("Belirtilen çalışan bulunamadı.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
            RefreshDataGrid();
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CalısanYonetimi_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Calisanlar";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }
        private void RefreshDataGrid()
        {
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Calisanlar";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

