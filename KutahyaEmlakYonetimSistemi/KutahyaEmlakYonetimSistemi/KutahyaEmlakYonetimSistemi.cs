using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace KutahyaEmlakYonetimSistemi
{
    public partial class KutahyaEmlakYonetimSistemi : Form
    {
        public KutahyaEmlakYonetimSistemi()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            GayrimenkulYonetimi gayrimenkulYonetimi = new GayrimenkulYonetimi();
            gayrimenkulYonetimi.Show();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusteriYonetimi musteriYonetimi=new MusteriYonetimi();
            musteriYonetimi.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IslemYonetimi ıslemYonetimi = new IslemYonetimi();
            ıslemYonetimi.Show();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FiyatlandirmaYonetimi fiyatlandirmaYonetimi = new FiyatlandirmaYonetimi();
            fiyatlandirmaYonetimi .Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            RaporlamaYonetimi raporlamaYonetimi=new RaporlamaYonetimi ();
            raporlamaYonetimi.Show ();
        }

        private void KutahyaEmlakYonetimSistemi_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CalısanYonetimi calısanYonetimi = new CalısanYonetimi();
            calısanYonetimi.Show();
        }
    }
}