using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KutahyaEmlakYonetimSistemi
{
    public partial class GayrimenkulYonetimi : Form
    {
        private readonly string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";
        private Dictionary<string, int> ilcelerDictionary = new Dictionary<string, int>();

        public GayrimenkulYonetimi()
        {
            InitializeComponent();
        }

        private void GayrimenkulYonetimi_Load(object sender, EventArgs e)
        {
            LoadCalisanlar();
            LoadIlceler();
            LoadGayrimenkuller();
            InitializeComboBoxValues();
        }

        private void LoadGayrimenkuller()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM Gayrimenkuller";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
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

        private void LoadCalisanlar()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Ad FROM Calisanlar";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            comboBox4.Items.Clear();
                            while (reader.Read())
                            {
                                comboBox4.Items.Add(reader["Ad"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void LoadIlceler()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sorgu = "SELECT IlceID, IlceAdi FROM Ilceler";
                    using (SqlCommand komut = new SqlCommand(sorgu, connection))
                    {
                        using (SqlDataReader reader = komut.ExecuteReader())
                        {
                            comboBox5.Items.Clear();
                            ilcelerDictionary.Clear();
                            while (reader.Read())
                            {
                                int ilceID = Convert.ToInt32(reader["IlceID"]);
                                string ilceAdi = reader["IlceAdi"].ToString();

                                comboBox5.Items.Add(ilceAdi);
                                ilcelerDictionary[ilceAdi] = ilceID;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void LoadMahalleler(int ilceID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT MahalleAdi FROM Mahalleler WHERE IlceID = @IlceID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IlceID", ilceID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            comboBox2.Items.Clear();
                            while (reader.Read())
                            {
                                comboBox2.Items.Add(reader["MahalleAdi"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void InitializeComboBoxValues()
        {
            comboBox1.Items.AddRange(new string[] { "Satılık", "Kiralık" });
            comboBox3.Items.AddRange(new string[] { "1+0", "1+1", "2+1", "3+1", "4+1" });
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedItem != null && ilcelerDictionary.TryGetValue(comboBox5.SelectedItem.ToString(), out int ilceID))
            {
                LoadMahalleler(ilceID);
            }
        }

        private bool ValidateInputs(out int buyukluk, out decimal fiyat)
        {
            buyukluk = 0;
            fiyat = 0;

            // Büyüklük doğrulama
            if (string.IsNullOrWhiteSpace(textBox3.Text) || !int.TryParse(textBox3.Text.Trim(), out buyukluk))
            {
                // Buraya ekleyin
                MessageBox.Show($"Girilen büyüklük değeri: '{textBox3.Text}'");

                MessageBox.Show("Geçerli bir büyüklük değeri girin.");
                return false;
            }

            // Fiyat doğrulama
            if (string.IsNullOrWhiteSpace(textBox4.Text) || !decimal.TryParse(textBox4.Text.Trim(), out fiyat))
            {
                MessageBox.Show("Geçerli bir fiyat değeri girin.");
                return false;
            }

            // Diğer kontroller
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Durum seçilmelidir (Satılık veya Kiralık).");
                return false;
            }

            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Oda sayısı seçilmelidir.");
                return false;
            }

            if (comboBox4.SelectedItem == null)
            {
                MessageBox.Show("Emlakçı bilgisi boş bırakılamaz.");
                return false;
            }

            if (comboBox5.SelectedItem == null || comboBox2.SelectedItem == null)
            {
                MessageBox.Show("İlçe ve mahalle seçimi yapılmalıdır.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Adres boş bırakılamaz.");
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs(out int buyukluk, out decimal fiyat)) return;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Gayrimenkuller (MahalleID, IlceID, Adres, Fiyat, Durum, Buyukluk, OdaSayisi, Emlakci) " +
                                   "VALUES (@MahalleID, @IlceID, @Adres, @Fiyat, @Durum, @Buyukluk, @OdaSayisi, @Emlakci)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MahalleID", comboBox2.SelectedIndex + 1);
                        command.Parameters.AddWithValue("@IlceID", ilcelerDictionary[comboBox5.SelectedItem.ToString()]);
                        command.Parameters.AddWithValue("@Adres", textBox1.Text.Trim());
                        command.Parameters.AddWithValue("@Fiyat", fiyat);
                        command.Parameters.AddWithValue("@Durum", comboBox1.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Buyukluk", buyukluk);
                        command.Parameters.AddWithValue("@OdaSayisi", comboBox3.SelectedItem.ToString());
                        command.Parameters.AddWithValue("@Emlakci", comboBox4.SelectedItem.ToString());

                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Gayrimenkul başarıyla eklendi.");
                LoadGayrimenkuller();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Silme işlemi
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int gayrimenkulID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["GayrimenkulID"].Value);

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM Gayrimenkuller WHERE GayrimenkulID = @GayrimenkulID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@GayrimenkulID", gayrimenkulID);
                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Gayrimenkul başarıyla silindi.");
                    LoadGayrimenkuller();
                }
                else
                {
                    MessageBox.Show("Lütfen silmek istediğiniz gayrimenkulü seçin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Güncelleme işlemi
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int gayrimenkulID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["GayrimenkulID"].Value);

                    if (!ValidateInputs(out int buyukluk, out decimal fiyat)) return;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Gayrimenkuller " +
                                       "SET MahalleID = @MahalleID, IlceID = @IlceID, Adres = @Adres, Fiyat = @Fiyat, Durum = @Durum, Buyukluk = @Buyukluk, OdaSayisi = @OdaSayisi, Emlakci = @Emlakci " +
                                       "WHERE GayrimenkulID = @GayrimenkulID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MahalleID", comboBox2.SelectedIndex + 1);
                            command.Parameters.AddWithValue("@IlceID", ilcelerDictionary[comboBox5.SelectedItem.ToString()]);
                            command.Parameters.AddWithValue("@Adres", textBox1.Text.Trim());
                            command.Parameters.AddWithValue("@Fiyat", fiyat);
                            command.Parameters.AddWithValue("@Durum", comboBox1.SelectedItem.ToString());
                            command.Parameters.AddWithValue("@Buyukluk", buyukluk);
                            command.Parameters.AddWithValue("@OdaSayisi", comboBox3.SelectedItem.ToString());
                            command.Parameters.AddWithValue("@Emlakci", comboBox4.SelectedItem.ToString());
                            command.Parameters.AddWithValue("@GayrimenkulID", gayrimenkulID);

                            command.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Gayrimenkul başarıyla güncellendi.");
                    LoadGayrimenkuller();
                }
                else
                {
                    MessageBox.Show("Lütfen güncellemek istediğiniz gayrimenkulü seçin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
