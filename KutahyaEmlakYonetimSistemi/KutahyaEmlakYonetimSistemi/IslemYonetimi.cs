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
    public partial class IslemYonetimi : Form
    {
        private string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";


        public IslemYonetimi()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void RefreshDataGrid()
        {
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Islemler";
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

        private void IslemYonetimi_Load(object sender, EventArgs e)
        {
            RefreshDataGrid();
            LoadComboBoxes();
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Islemler";
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
        private void LoadComboBoxes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Gayrimenkul ID
                    string query1 = "SELECT GayrimenkulID FROM Gayrimenkuller";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query1, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        comboBox1.DataSource = dataTable;
                        comboBox1.DisplayMember = "GayrimenkulID";
                        comboBox1.ValueMember = "GayrimenkulID";
                    }

                    // Müşteri ID
                    string query2 = "SELECT MusteriID FROM Musteriler";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query2, connection))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        comboBox3.DataSource = dataTable;
                        comboBox3.DisplayMember = "MusteriID";
                        comboBox3.ValueMember = "MusteriID";
                    }

                    // İşlem Türü
                    comboBox2.Items.Add("Satın Alma");
                    comboBox2.Items.Add("Kiralama");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Ekle butonu
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Islemler (MusteriID, GayrimenkulID, IslemTarihi, IslemTuru) VALUES (@MusteriID, @GayrimenkulID, @IslemTarihi, @IslemTuru)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MusteriID", comboBox3.SelectedValue);
                        command.Parameters.AddWithValue("@GayrimenkulID", comboBox1.SelectedValue);
                        command.Parameters.AddWithValue("@IslemTarihi", dateTimePicker1.Value);
                        command.Parameters.AddWithValue("@IslemTuru", comboBox2.SelectedItem.ToString());

                        command.ExecuteNonQuery();
                        MessageBox.Show("İşlem başarıyla eklendi.");
                        RefreshDataGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Sil butonu
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int islemID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IslemID"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM Islemler WHERE IslemID = @IslemID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IslemID", islemID);
                            command.ExecuteNonQuery();
                            MessageBox.Show("İşlem başarıyla silindi.");
                            RefreshDataGrid();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen silmek istediğiniz işlemi seçin.");
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            // Güncelle butonu
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int islemID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["IslemID"].Value);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "UPDATE Islemler SET MusteriID = @MusteriID, GayrimenkulID = @GayrimenkulID, IslemTarihi = @IslemTarihi, IslemTuru = @IslemTuru WHERE IslemID = @IslemID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MusteriID", comboBox3.SelectedValue);
                            command.Parameters.AddWithValue("@GayrimenkulID", comboBox1.SelectedValue);
                            command.Parameters.AddWithValue("@IslemTarihi", dateTimePicker1.Value);
                            command.Parameters.AddWithValue("@IslemTuru", comboBox2.SelectedItem.ToString());
                            command.Parameters.AddWithValue("@IslemID", islemID);

                            command.ExecuteNonQuery();
                            MessageBox.Show("İşlem başarıyla güncellendi.");
                            RefreshDataGrid();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Hata: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen güncellemek istediğiniz işlemi seçin.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gayrimenkul ıd
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //müşteri ıd
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //satılık mı kiralık mı
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //işlem tarihi
        }
    }
}
