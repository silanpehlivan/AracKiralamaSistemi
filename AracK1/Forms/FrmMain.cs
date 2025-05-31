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
    public partial class FrmMain : Form
    {
        private int kullaniciId;

        public FrmMain(int kullaniciId)
        {
            InitializeComponent();
            this.kullaniciId = kullaniciId;

        }


        private void btnFiltrele_Click(object sender, EventArgs e)
        {
            AraclariYukle(cmbKategori.Text, cmbMarka.Text, cmbYas.Text);
        }

        private void AraclariYukle(string kategori = "", string marka = "", string yas = "")
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                string query = "SELECT * FROM Araclar WHERE Durum = 'Müsait'";
                if (!string.IsNullOrEmpty(kategori))
                    query += " AND Kategori = @Kategori";
                if (!string.IsNullOrEmpty(marka))
                    query += " AND Marka = @Marka";
                if (!string.IsNullOrEmpty(yas))
                    query += " AND Yas <= @Yas";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (!string.IsNullOrEmpty(kategori))
                    cmd.Parameters.AddWithValue("@Kategori", kategori);
                if (!string.IsNullOrEmpty(marka))
                    cmd.Parameters.AddWithValue("@Marka", marka);
                if (!string.IsNullOrEmpty(yas))
                    cmd.Parameters.AddWithValue("@Yas", Convert.ToInt32(yas));

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAraclar.DataSource = dt;

                // Sütun başlıklarını düzenle
                dgvAraclar.Columns["AracID"].HeaderText = "Araç ID";
                dgvAraclar.Columns["Marka"].HeaderText = "Marka";
                dgvAraclar.Columns["Model"].HeaderText = "Model";
                dgvAraclar.Columns["Kategori"].HeaderText = "Kategori";
                dgvAraclar.Columns["Yas"].HeaderText = "Yaş";
            }
        }


        private void btnRezerve_Click(object sender, EventArgs e)
        {
            if (dgvAraclar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen bir araç seçiniz.");
                return;
            }

            int aracId = Convert.ToInt32(dgvAraclar.SelectedRows[0].Cells["AracID"].Value);

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string kontrolQuery = "SELECT Durum, Kategori, Marka, Model, Yas FROM Araclar WHERE AracID = @AracID";
                    SqlCommand kontrolCmd = new SqlCommand(kontrolQuery, conn);
                    kontrolCmd.Parameters.AddWithValue("@AracID", aracId);

                    using (SqlDataReader reader = kontrolCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string durum = reader["Durum"].ToString();

                            if (durum != "Müsait")
                            {
                                lblDurum.Text = "GELİNCE HABER VER";
                                return;
                            }

                            lblDurum.Text = "ARACI REZERVE EDEBİLİRSİNİZ";

                            var aracBilgisi = new AracDetay
                            {
                                Kategori = reader["Kategori"].ToString(),
                                Marka = reader["Marka"].ToString(),
                                Model = reader["Model"].ToString(),
                                Yas = reader["Yas"].ToString()
                            };
                        }

                        else
                        {
                            MessageBox.Show("Seçilen araç bulunamadı.");
                            return;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message);
                    return;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                    return;
                }
            }

            // Bu kısım, veritabanından kart bilgilerini alıp yeni ödeme formuna aktarır
            string kartNumarasi = "";
            string cvv = "";

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT KartNumarasi, CVV FROM Kullanicilar WHERE KullaniciID = @Kid", conn);
                cmd.Parameters.AddWithValue("@Kid", kullaniciId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        kartNumarasi = reader["KartNumarasi"].ToString();
                        cvv = reader["CVV"].ToString();
                    }
                }
            }

            // FrmRezerveOdeme formunu kart bilgileriyle birlikte aç
            var aracDetay = new AracDetay
            {
                Kategori = dgvAraclar.SelectedRows[0].Cells["Kategori"].Value.ToString(),
                Marka = dgvAraclar.SelectedRows[0].Cells["Marka"].Value.ToString(),
                Model = dgvAraclar.SelectedRows[0].Cells["Model"].Value.ToString(),
                Yas = dgvAraclar.SelectedRows[0].Cells["Yas"].Value.ToString()
            };

            FrmRezerveOdeme frmOdeme = new FrmRezerveOdeme(kullaniciId, aracId, aracDetay, kartNumarasi, cvv);
            var result = frmOdeme.ShowDialog();
            if (result == DialogResult.OK)
            {
                using (SqlConnection conn2 = SqlHelper.GetConnection())
                {
                    conn2.Open();
                    SqlCommand kirala = new SqlCommand(
                        "INSERT INTO Kiralamalar (KullaniciID, AracID, Durum) VALUES (@Kid, @Aid, 'Aktif'); " +
                        "UPDATE Araclar SET Durum = 'Kiralandı' WHERE AracID = @Aid", conn2);

                    kirala.Parameters.AddWithValue("@Kid", kullaniciId);
                    kirala.Parameters.AddWithValue("@Aid", aracId);
                    kirala.ExecuteNonQuery();

                    MessageBox.Show("Araç başarıyla kiralandı.");
                    this.Hide(); // FrmMain gizleniyor
                }
            }

            else
            {
                MessageBox.Show("Ödeme başarısız veya iptal edildi.");
            }
            FrmYorumlar yorumForm = new FrmYorumlar(kullaniciId, aracId);
            yorumForm.ShowDialog();

        }
        
        
        private void FrmMain_Load(object sender, EventArgs e)
        {
            // DropDownları doldur
                            cmbKategori.Items.AddRange(new string[] {
                    "Sedan",
                    "SUV",
                    "Hatchback",
                    "Ticari",
                    "Cabrio",
                    "Coupe",
                    "Pickup",
                    "MPV",
                    "Crossover",
                    "Minivan"
                });

            //cmbMarka.Items.AddRange(new string[] { "Toyota", "Renault", "Ford", "BMW", "Mercedes" });
            cmbYas.Items.AddRange(new string[] { "0", "1", "2", "3", "4", "5+" });

            MarkalariYukle();
            AraclariYukle();
        }
        private void MarkalariYukle()
        {
            cmbMarka.Items.Clear();

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Marka FROM Araclar ORDER BY Marka", conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbMarka.Items.Add(reader["Marka"].ToString());
                }
                conn.Close();
            }
        }


        private void btnFiltrele_MouseEnter(object sender, EventArgs e)
        {
            if (btnFiltrele.BackColor == Color.FromArgb(244,247,252))
            {
                btnFiltrele.BackColor = Color.FromArgb(188,143,143);
            }
            else
            {
                btnFiltrele.BackColor = Color.FromArgb(244,247,252);
            }

        }

        private void btnFiltrele_MouseLeave(object sender, EventArgs e)
        {
            if (btnFiltrele.BackColor == Color.FromArgb(188,143,143))
            {
                btnFiltrele.BackColor = Color.FromArgb(244,247,252);
            }
            else
            {
               btnFiltrele.BackColor = Color.FromArgb(188, 143, 143);
            }
        }

        private void btnRezerve_MouseEnter(object sender, EventArgs e)
        {
            if (btnRezerve.BackColor == Color.FromArgb(244, 247, 252))
            {
                btnRezerve.BackColor = Color.FromArgb(188, 143, 143);
            }
            else
            {
                btnRezerve.BackColor = Color.FromArgb(244, 247, 252);
            }
        }

        private void btnRezerve_MouseLeave(object sender, EventArgs e)
        {
            if (btnRezerve.BackColor == Color.FromArgb(188, 143, 143))
            {
                btnRezerve.BackColor = Color.FromArgb(244, 247, 252);
            }
            else
            {
                btnRezerve.BackColor = Color.FromArgb(188, 143, 143);
            }
        }
    }
}
