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

namespace KutahyaEmlakYonetimSistemi
{
    public partial class MusteriYonetimi : Form
    {
        public MusteriYonetimi()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Ekle
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Musteriler (Ad, Soyad, Telefon, Email, Adres, Tercih) VALUES (@Ad, @Soyad, @Telefon, @Email, @Adres, @Tercih)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Ad", textBox1.Text);
                        command.Parameters.AddWithValue("@Soyad", textBox2.Text);
                        command.Parameters.AddWithValue("@Telefon", textBox3.Text);
                        command.Parameters.AddWithValue("@Email", textBox5.Text);
                        command.Parameters.AddWithValue("@Adres", textBox4.Text);
                        command.Parameters.AddWithValue("@Tercih", comboBox1.SelectedItem?.ToString() ?? "");

                        command.ExecuteNonQuery();
                        MessageBox.Show("Müşteri başarıyla eklendi!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
                finally
                {
                    RefreshDataGrid();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //sil
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        int musteriId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MusteriID"].Value);

                        string query = "DELETE FROM Musteriler WHERE MusteriID = @MusteriID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MusteriID", musteriId);
                            command.ExecuteNonQuery();
                            MessageBox.Show("Müşteri başarıyla silindi!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}");
                    }
                    finally
                    {
                        RefreshDataGrid();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir müşteri seçin!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //güncelle
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        int musteriId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MusteriID"].Value);

                        string query = "UPDATE Musteriler SET Ad = @Ad, Soyad = @Soyad, Telefon = @Telefon, Email = @Email, Adres = @Adres, Tercih = @Tercih WHERE MusteriID = @MusteriID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Ad", textBox1.Text);
                            command.Parameters.AddWithValue("@Soyad", textBox2.Text);
                            command.Parameters.AddWithValue("@Telefon", textBox3.Text);
                            command.Parameters.AddWithValue("@Email", textBox5.Text);
                            command.Parameters.AddWithValue("@Adres", textBox4.Text);
                            command.Parameters.AddWithValue("@Tercih", comboBox1.SelectedItem?.ToString() ?? "");
                            command.Parameters.AddWithValue("@MusteriID", musteriId);

                            command.ExecuteNonQuery();
                            MessageBox.Show("Müşteri başarıyla güncellendi!");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}");
                    }
                    finally
                    {
                        RefreshDataGrid();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellenecek bir müşteri seçin!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //tercih
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //müşteri adı
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //soyadı
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //telefon
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            //adres
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //e-mail
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void MusteriYonetimi_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Kiralık");
            comboBox1.Items.Add("Satılık");
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Musteriler";
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
                    string query = "SELECT * FROM Musteriler";
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

    }
}
