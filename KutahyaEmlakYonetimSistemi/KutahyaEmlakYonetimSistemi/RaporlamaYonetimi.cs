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
    public partial class RaporlamaYonetimi : Form
    {
        public RaporlamaYonetimi()
        {
            InitializeComponent();
        }
        private void RefreshDataGrid()
        {
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Raporlar";
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


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            //2. tarih
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // 1. tarih
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //listele butonu
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Kullanıcıdan alınan tarih aralığı
                    DateTime baslangicTarihi = dateTimePicker1.Value.Date;
                    DateTime bitisTarihi = dateTimePicker2.Value.Date;

                    // Doğru sütun adı olan 'RaporTarihi' kullanılıyor
                    string query = "SELECT * FROM Raporlar WHERE RaporTarihi BETWEEN @BaslangicTarihi AND @BitisTarihi";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@BaslangicTarihi", baslangicTarihi);
                        command.Parameters.AddWithValue("@BitisTarihi", bitisTarihi);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            dataGridView1.DataSource = dataTable;
                        }
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
            this.Close();
        }

        private void RaporlamaYonetimi_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Raporlar";
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
