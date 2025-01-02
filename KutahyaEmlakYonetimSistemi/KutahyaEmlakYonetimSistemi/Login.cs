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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Kullanıcı adı ve şifreyi al
            string KullaniciAdi = textBox1.Text; 
            string Sifre = textBox2.Text; 

            // Veritabanı bağlantı dizesi
            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            // SQL bağlantısını oluştur
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    // Bağlantıyı aç
                    connection.Open();

                    // Kullanıcıyı doğrulamak için SQL sorgusu
                    string query = "SELECT COUNT(1) FROM Kullaniciler WHERE KullaniciAdi = @KullaniciAdi AND Sifre = @Sifre";

                    // Komutu hazırlayın
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametreleri ekle
                        command.Parameters.AddWithValue("@KullaniciAdi", KullaniciAdi);
                        command.Parameters.AddWithValue("@Sifre", Sifre);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        // Kullanıcı adı ve şifre doğruysa giriş yap
                        if (count == 1)
                        {
                            MessageBox.Show("Giriş başarılı! Sisteme hoş geldiniz.");
                            KutahyaEmlakYonetimSistemi kutahyaEmlakYonetimSistemi = new KutahyaEmlakYonetimSistemi();
                            kutahyaEmlakYonetimSistemi.Show();
                        }
                        else
                        {
                            MessageBox.Show("Hatalı kullanıcı adı veya şifre.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Hata mesajı göster
                    MessageBox.Show($"Bağlantı hatası: {ex.Message}");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Kütahya Emlak Yönetim Sistemine Hoşgeldiniz.");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Kullanıcı ekleme kodu
            string kullaniciAdi = textBox1.Text;
            string sifre = textBox2.Text;

            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO Kullaniciler (KullaniciAdi, Sifre) VALUES (@KullaniciAdi, @Sifre)";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        command.Parameters.AddWithValue("@Sifre", sifre);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kullanıcı başarıyla eklendi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Kullanıcı silme kodu
            string kullaniciAdi = textBox1.Text;

            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "DELETE FROM Kullaniciler WHERE KullaniciAdi = @KullaniciAdi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@KullaniciAdi", kullaniciAdi);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kullanıcı başarıyla silindi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Kullanıcı güncelleme kodu
            string eskiKullaniciAdi = textBox1.Text; 
            string yeniSifre = textBox2.Text; 

            string connectionString = "Data Source=GURBEYPC\\MSSQLSERVER01;Initial Catalog=KutahyaEmlakYonetimSistemi;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Kullaniciler SET Sifre = @YeniSifre WHERE KullaniciAdi = @EskiKullaniciAdi";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EskiKullaniciAdi", eskiKullaniciAdi);
                        command.Parameters.AddWithValue("@YeniSifre", yeniSifre);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Kullanıcı başarıyla güncellendi.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}");
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
