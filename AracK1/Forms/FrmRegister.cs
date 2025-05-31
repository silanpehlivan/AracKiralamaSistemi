using AracK1.Data;
using AracK1.Models;
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

namespace AracK1.Forms
{
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }
        private int kullaniciId; // Kullanıcı ID'sini saklamak için sınıf düzeyinde değişken
        private string kullaniciKartNumarasi;
        private string kullaniciCVV;

        private void btnKaydol_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                string query = "INSERT INTO Kullanicilar (Isim, Soyisim, Email, Sifre, KartNumarasi, CVV) VALUES (@Isim, @Soyisim, @Email, @Sifre, @Kart, @CVV)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Isim", txtIsim.Text);
                cmd.Parameters.AddWithValue("@Soyisim", txtSoyisim.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Sifre", txtSifre.Text);
                cmd.Parameters.AddWithValue("@Kart", txtKartNo.Text);
                cmd.Parameters.AddWithValue("@CVV", txtCVV.Text);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();

                    // Kayıt başarılı olduktan sonra KullaniciID'yi al
                    query = "SELECT KullaniciID, KartNumarasi, CVV FROM Kullanicilar WHERE Email = @Email";
                    cmd = new SqlCommand(query, conn); // Aynı bağlantıyı kullan
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text); // Kaydedilen email ile sorgula

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            kullaniciId = (int)dr["KullaniciID"];
                            kullaniciKartNumarasi = dr["KartNumarasi"].ToString();
                            kullaniciCVV = dr["CVV"].ToString();
                        }
                    }

                    MessageBox.Show("Kayıt başarılı!");
                    this.Close(); // Giriş ekranına dönebilir
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
        }

        private void txtSifre_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnKaydol_MouseEnter(object sender, EventArgs e)
        {
            if (btnKaydol.BackColor == Color.FromArgb(160, 197, 225))
            {
                btnKaydol.BackColor = Color.FromArgb(200, 205, 219);

            }
            else
            {
                btnKaydol.BackColor = Color.FromArgb(160, 197, 225);
            }
        }

        private void btnKaydol_MouseLeave(object sender, EventArgs e)
        {
            if (btnKaydol.BackColor == Color.FromArgb(200, 205, 219))
            {
                btnKaydol.BackColor = Color.FromArgb(160, 197, 225);
            }
            else
            {
                btnKaydol.BackColor = Color.FromArgb(200, 205, 219);
            }
        }
    }
}

