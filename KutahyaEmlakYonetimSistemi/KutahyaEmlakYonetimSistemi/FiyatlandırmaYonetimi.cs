using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KutahyaEmlakYonetimSistemi
{
    public partial class FiyatlandirmaYonetimi : Form
    {
        public FiyatlandirmaYonetimi()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Satılık mı kiralık mı seçimi yapılıyor
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Nakit mi kredi kartı mı seçimi yapılıyor
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Hesapla butonu
            try
            {
                // Ödeme tutarını ve komisyon oranını al
                decimal odemeTutari = Convert.ToDecimal(textBox1.Text);
                decimal komisyonOrani = Convert.ToDecimal(textBox2.Text); // Girilen komisyon oranı

                decimal sonuc = 0;

                // Satılık veya Kiralık seçimine göre işlem yap
                if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null)
                {
                    string islemTuru = comboBox1.SelectedItem.ToString(); // Satılık veya Kiralık
                    string odemeTuru = comboBox2.SelectedItem.ToString(); // Nakit veya Kredi Kartı

                    if (islemTuru == "Satılık")
                    {
                        // Satıcıdan ve alıcıdan %2'şer komisyon alınır
                        sonuc = (odemeTutari * (2 + 2) / 100) * komisyonOrani / 100;
                    }
                    else if (islemTuru == "Kiralık")
                    {
                        // Kiracıdan komisyon oranına göre hesap yapılır
                        sonuc = odemeTutari * komisyonOrani / 100;
                    }
                    else
                    {
                        MessageBox.Show("Lütfen geçerli bir işlem türü seçin!");
                        return;
                    }

                    // Ödeme türüne göre işlem yap
                    if (odemeTuru == "Kredi Kartı")
                    {
                        // Kredi Kartı seçilirse %20 KDV eklenir
                        decimal kdv = sonuc * 20 / 100;
                        sonuc += kdv;
                    }

                    // Hesaplanan sonucu TextBox'a yazdır
                    textBox3.Text = sonuc.ToString("C2"); // Para birimi formatında
                }
                else
                {
                    MessageBox.Show("Lütfen işlem ve ödeme türünü seçin!");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Lütfen geçerli bir sayı girin!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

        }

        private void OdemeYonetimi_Load(object sender, EventArgs e)
        {
            

        }


        private void button2_Click(object sender, EventArgs e)
        {
            // Kapat butonu
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Tutar alanına girilen değer
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Komisyon oranı
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Sonuç alanı
        }

        private void FiyatlandirmaYonetimi_Load_1(object sender, EventArgs e)
        {
            comboBox1.Items.Add("Satılık");
            comboBox1.Items.Add("Kiralık");
            comboBox2.Items.Add("Nakit");
            comboBox2.Items.Add("Kredi Kartı");
        }
    }
}
