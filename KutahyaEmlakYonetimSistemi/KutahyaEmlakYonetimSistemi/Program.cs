using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KutahyaEmlakYonetimSistemi
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Login formunu oluştur ve göster
            Login loginForm = new Login();
            loginForm.ShowDialog(); // Login formunu modally göster

            // Eğer giriş başarılıysa ana formu aç
            if (loginForm.DialogResult == DialogResult.OK)
            {
                KutahyaEmlakYonetimSistemi anaForm = new KutahyaEmlakYonetimSistemi();
                Application.Run(anaForm); // Ana formu çalıştır
            }
        }
    }
}
