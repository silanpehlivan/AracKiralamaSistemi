using AracK1.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AracK1.Forms
{
    public partial class FrmYorumlar : Form
    {
        private int kullaniciId;
        private int aracId;

        // Parametreli Constructor (Ödeme sayfasından geçiş için)
        public FrmYorumlar(int kullaniciId, int aracId)
        {
            InitializeComponent();
            this.kullaniciId = kullaniciId;
            this.aracId = aracId; // Gelen araç ID'sini atayın
        }

        // Parametresiz Constructor (Tasarım zamanı veya başka kullanımlar için)

        public FrmYorumlar()
        {
            InitializeComponent();

        }

        private void FrmYorumlar_Load(object sender, EventArgs e)
        {
            if (kullaniciId > 0)
            {
                if (aracId > 0)
                {
                    YorumlariYukle(aracId);
                    TumAraclariYukle(aracId); // sadece kiralananlar değil, tüm araçlar
                }
                else
                {
                    TumAraclariYukle(); // sadece kiralananlar değil, tüm araçlar
                }
            }
            else
            {
                MessageBox.Show("Kullanıcı bilgisi alınamadı.");
            }
        }
        private void TumAraclariYukle(int? seciliAracId = null)
        {
            try
            {
                using (SqlConnection conn = SqlHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(@"
                SELECT MIN(AracID) AS AracID, (Marka + ' - ' + Model) AS Model
                FROM Araclar
                GROUP BY Marka, Model", conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbAraclar.DataSource = null;
                    cmbAraclar.DataSource = dt;
                    cmbAraclar.DisplayMember = "Model";
                    cmbAraclar.ValueMember = "AracID";
                    cmbAraclar.DropDownStyle = ComboBoxStyle.DropDownList;

                    if (seciliAracId.HasValue)
                    {
                        cmbAraclar.SelectedValue = seciliAracId.Value;
                    }
                    else if (dt.Rows.Count > 0)
                    {
                        cmbAraclar.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Araçlar yüklenirken hata oluştu: " + ex.Message);
            }
        }


        private void btnYorumYap_Click(object sender, EventArgs e)
        {
            int yorumYapilacakAracId = 0;

            if (aracId > 0)
            {
                yorumYapilacakAracId = aracId; // Ödeme sayfasından gelindiyse direkt aracId'yi kullan
            }
            else if (cmbAraclar.SelectedIndex != -1)
            {
                yorumYapilacakAracId = Convert.ToInt32(cmbAraclar.SelectedValue); // ComboBox'tan seçilen aracı kullan
            }
            else
            {
                MessageBox.Show("Lütfen bir araç seçiniz.");
                return;
            }

            int puan = (int)numPuan.Value;
            string yorum = txtYorum.Text;

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open(); // Bağlantıyı burada açın

                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Yorumlar (KullaniciID, AracID, Puan, Yorum) VALUES (@Kid, @Aid, @Puan, @Yorum)", conn);

                cmd.Parameters.AddWithValue("@Kid", kullaniciId);
                cmd.Parameters.AddWithValue("@Aid", yorumYapilacakAracId);
                cmd.Parameters.AddWithValue("@Puan", puan);
                cmd.Parameters.AddWithValue("@Yorum", yorum);

                cmd.ExecuteNonQuery();
                MessageBox.Show("Yorumunuz kaydedildi.");

                YorumlariYukle(yorumYapilacakAracId);

                // 1 saniye sonra projeyi kapatmak için Timer başlat
                Timer timer = new Timer();
                timer.Interval = 4000; // 4000 milisaniye = 4 saniye
                timer.Tick += (s, args) =>
                {
                    timer.Stop();         // Timer'ı durdur
                    Application.Exit();   // Uygulamayı kapat
                };
                timer.Start();

            }
        }

        private void YorumlariYukle(int aracId)
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open(); // Bağlantıyı burada açın

                
                SqlDataAdapter da = new SqlDataAdapter(@"
                SELECT 
                    K.Isim + ' ' + K.Soyisim AS Kullanici, 
                    (A.Marka + ' - ' + A.Model) AS Arac, 
                    Y.Puan, 
                    Y.Yorum, 
                    Y.Tarih
                FROM Yorumlar Y
                INNER JOIN Kullanicilar K ON Y.KullaniciID = K.KullaniciID
                INNER JOIN Araclar A ON Y.AracID = A.AracID
                WHERE Y.AracID = @Aid
                ORDER BY Y.Tarih DESC", conn);


                da.SelectCommand.Parameters.AddWithValue("@Aid", aracId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvYorumlar.DataSource = dt;

                SqlCommand ortalamaCmd = new SqlCommand("SELECT AVG(CAST(Puan AS FLOAT)) FROM Yorumlar WHERE AracID = @Aid", conn);
                ortalamaCmd.Parameters.AddWithValue("@Aid", aracId);

                object ortalama = ortalamaCmd.ExecuteScalar();
                lblOrtalama.Text = ortalama != DBNull.Value ? $"Ortalama Puan: {Math.Round(Convert.ToDouble(ortalama), 1)}" : "Henüz puan yok";
            }
        }

        private void txtYorum_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnYorumYap_MouseEnter(object sender, EventArgs e)
        {
            if (btnYorumYap.BackColor == Color.FromArgb(24, 137, 239))
            {
                btnYorumYap.BackColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                btnYorumYap.BackColor = Color.FromArgb(24, 137, 239);
            }
        }

        private void btnYorumYap_MouseLeave(object sender, EventArgs e)
        {
            if (btnYorumYap.BackColor == Color.FromArgb(255, 255, 255))
            {
                btnYorumYap.BackColor = Color.FromArgb(24, 137, 239);
            }
            else
            {
                btnYorumYap.BackColor = Color.FromArgb(255, 255, 255);
            }
        }
    }

}
