using AracK1.Data;
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
//using AracK1.BusinessLogic; // KullaniciService sınıfınızın bulunduğu namespace'i ekleyin


namespace AracK1.Forms
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            ////TEST1: ŞİLAN PEHLİVAN
            //// KullaniciIslemleri sınıfındaki GirisYap metodunu çağırıyoruz
            //int? kullaniciId = KullaniciIslemleri.GirisYap(txtEmail.Text, txtSifre.Text);

            //if (kullaniciId != null)
            //{
            //    // Giriş başarılıysa ana formu aç
            //    FrmMain main = new FrmMain(kullaniciId.Value); // .Value ile nullable int'ten int değerini alıyoruz
            //    main.Show();
            //    this.Hide();
            //}
            //else
            //{
            //    // Giriş başarısızsa hata mesajı göster
            //    MessageBox.Show("Geçersiz kullanıcı adı veya şifre.");
            //}
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open(); // Bağlantıyı burada açın

                    string query = "SELECT KullaniciID FROM Kullanicilar WHERE Email = @Email AND Sifre = @Sifre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@Sifre", txtSifre.Text);

                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        int kullaniciId = Convert.ToInt32(result);
                        FrmMain main = new FrmMain(kullaniciId);
                        main.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz kullanıcı adı veya şifre.");
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }

        }

        private void lblKayitOl_Click(object sender, EventArgs e)
        {
            FrmRegister register = new FrmRegister();
            register.ShowDialog();
        }

        private void btnAdminGiris_Click(object sender, EventArgs e)
        {
            FrmAdminLogin adminLogin = new FrmAdminLogin();
            adminLogin.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Arka plan resmi eklenmiş PictureBox'ı en arkaya gönder
           
        }

        private void btnGiris_MouseEnter(object sender, EventArgs e)
        {
            if (btnGiris.BackColor == Color.FromArgb(159, 170, 196))
            {
                btnGiris.BackColor = Color.FromArgb(75, 81, 106);
            }
            else
            {
                btnGiris.BackColor = Color.FromArgb(159, 170, 196);
            }
        }

        private void btnGiris_MouseLeave(object sender, EventArgs e)
        {
            if (btnGiris.BackColor == Color.FromArgb(75, 81, 106))
            {
                btnGiris.BackColor = Color.FromArgb(159, 170, 196);
            }
            else
            {
                btnGiris.BackColor = Color.FromArgb(75, 81, 106);
            }
        }

        private void btnAdminGiris_MouseEnter(object sender, EventArgs e)
        {

            if (btnAdminGiris.BackColor == Color.FromArgb(159, 170, 196))
            {
                btnAdminGiris.BackColor = Color.FromArgb(75, 81, 106);
            }
            else
            {
                btnAdminGiris.BackColor = Color.FromArgb(159, 170, 196);
            }
        }

        private void btnAdminGiris_MouseLeave(object sender, EventArgs e)
        {
            if (btnAdminGiris.BackColor == Color.FromArgb(75, 81, 106))
            {
                btnAdminGiris.BackColor = Color.FromArgb(159, 170, 196);
            }
            else
            {
                btnAdminGiris.BackColor = Color.FromArgb(75, 81, 106);
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtSifre.PasswordChar = '●'; // Veya '*' da olabilir
        }
    }
}
