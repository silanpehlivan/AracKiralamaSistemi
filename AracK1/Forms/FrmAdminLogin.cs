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

namespace AracK1.Forms
{
    public partial class FrmAdminLogin : Form
    {
        public FrmAdminLogin()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open(); // BAĞLANTI BURADA AÇILIYOR

                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Yoneticiler WHERE KullaniciAdi = @kadi AND Sifre = @sifre", conn);
                    cmd.Parameters.AddWithValue("@kadi", txtKullaniciAdi.Text);
                    cmd.Parameters.AddWithValue("@sifre", txtSifre.Text);

                    int result = (int)cmd.ExecuteScalar();

                    if (result > 0)
                    {
                        FrmAdmin adminPanel = new FrmAdmin();
                        adminPanel.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Hatalı giriş!");
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
                finally
                {
                    // Bağlantı using bloğu tarafından otomatik olarak kapatılacak
                }
            }
        }

        private void btnGiris_MouseEnter(object sender, EventArgs e)
        {
            if (btnGiris.BackColor == Color.FromArgb(158, 175, 190))
            {
                btnGiris.BackColor = Color.FromArgb(102, 121, 135);

            }
            else
            {
                btnGiris.BackColor = Color.FromArgb(158, 175, 190);
            }
        }

        private void btnGiris_MouseLeave(object sender, EventArgs e)
        {
            if (btnGiris.BackColor == Color.FromArgb(102, 121, 135))
            {
                btnGiris.BackColor = Color.FromArgb(158, 175, 190);

            }
            else
            {
                btnGiris.BackColor = Color.FromArgb(102, 121, 135);
            }
        }

        private void FrmAdminLogin_Load(object sender, EventArgs e)
        {
            txtSifre.PasswordChar = '●';
        }
    }
}
