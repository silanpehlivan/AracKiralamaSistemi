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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void btnAracEkle_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open(); // BAĞLANTI BURADA AÇILIYOR

                    SqlCommand cmd = new SqlCommand("INSERT INTO Araclar (Marka, Model, Kategori, Yas, Durum) VALUES (@marka, @model, @kategori, @yas, 'Müsait')", conn);
                    cmd.Parameters.AddWithValue("@marka", txtMarka.Text);
                    cmd.Parameters.AddWithValue("@model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@kategori", cmbKategori.Text);
                    cmd.Parameters.AddWithValue("@yas", numYas.Value);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Araç eklendi.");
                    AraclariYukle();
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

        private void btnAracSil_Click(object sender, EventArgs e)
        {
            if (dgvAraclar.SelectedRows.Count == 0) return;

            int aracId = Convert.ToInt32(dgvAraclar.SelectedRows[0].Cells["AracID"].Value);

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open(); // BAĞLANTI BURADA AÇILIYOR

                    SqlCommand cmd = new SqlCommand("DELETE FROM Araclar WHERE AracID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", aracId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Araç silindi.");
                    AraclariYukle();
                    TemizleAracTextBoxlar(); // Silme işleminden sonra araç textbox ve combobox'larını temizle
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
        private void TemizleAracTextBoxlar()
        {
            // Araç ekleme/düzenleme bölümündeki textbox ve combobox'ları temizler
            txtMarka.Text = string.Empty;
            txtModel.Text = string.Empty;
            cmbKategori.SelectedIndex = -1; // Combobox'ta seçili öğeyi kaldırır
            numYas.Value = numYas.Minimum; // NumericUpDown'ı minimum değerine sıfırlar

            // Formunuzda araç yönetimi ile ilgili başka textbox veya kontroller varsa
            // onları da bu metot içinde temizleyebilirsiniz. Örneğin:
            // txtRenk.Text = string.Empty;
            // txtYakitTipi.Text = string.Empty;
        }

        private void AraclariYukle()
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Araclar", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvAraclar.DataSource = dt;
            }
        }

        private void dgvAraclar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAraclar.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvAraclar.SelectedRows[0];
                if (selectedRow.Cells["Marka"] != null)
                {
                    txtMarka.Text = selectedRow.Cells["Marka"].Value.ToString();
                }
                if (selectedRow.Cells["Model"] != null)
                {
                    txtModel.Text = selectedRow.Cells["Model"].Value.ToString();
                }
                if (selectedRow.Cells["Kategori"] != null)
                {
                    cmbKategori.Text = selectedRow.Cells["Kategori"].Value.ToString();
                }
                if (selectedRow.Cells["Yas"] != null && selectedRow.Cells["Yas"].Value != DBNull.Value)
                {
                    numYas.Value = Convert.ToInt32(selectedRow.Cells["Yas"].Value);
                }
            }
        }


        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void RezerveListele()
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
            SELECT K.Isim + ' ' + K.Soyisim AS Kullanici, A.Marka, A.Model, R.KiralamaTarihi, A.AracID
            FROM Kiralamalar R
            INNER JOIN Kullanicilar K ON R.KullaniciID = K.KullaniciID
            INNER JOIN Araclar A ON R.AracID = A.AracID
            WHERE R.Durum = 'Aktif'", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvRezervasyonlar.DataSource = dt;
            }
        }

        private void dgvRezervasyonlar_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRezervasyonlar.SelectedRows.Count > 0)
            {
                var row = dgvRezervasyonlar.SelectedRows[0];
                txtMarka.Text = row.Cells["Marka"].Value.ToString();
                txtModel.Text = row.Cells["Model"].Value.ToString();

            }
        }

        private void btnIadeEt_Click(object sender, EventArgs e)
        {

            if (dgvIadeListesi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen iade edilecek bir araç seçiniz.");
                return;
            }

            int aracId = Convert.ToInt32(dgvIadeListesi.SelectedRows[0].Cells["AracID"].Value);

            if (dgvIadeListesi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen iade edilecek bir araç seçiniz.");
                return;
            }

            int AracId = Convert.ToInt32(dgvIadeListesi.SelectedRows[0].Cells["AracID"].Value);

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"
                UPDATE Araclar
                SET Durum = 'Müsait'
                WHERE AracID = @Aid;

                UPDATE Kiralamalar
                SET Durum = 'İade Edildi'
                WHERE AracID = @Aid AND Durum = 'Aktif';
            ", conn);

                    cmd.Parameters.AddWithValue("@Aid", aracId);

                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Araç başarıyla iade edildi.");
                    }
                    else
                    {
                        MessageBox.Show("Seçilen araç için iade işlemi yapılamadı. Araç durumu güncel olmayabilir.");
                    }

                    IadeListesiYukle();       // DataGridView'i güncelle
                    TemizleIadeTextBoxlar();  // Formdaki textboxları temizle
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



        private void dgvIadeListesi_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvIadeListesi.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvIadeListesi.SelectedRows[0];

                txtIadeMarka.Text = selectedRow.Cells["Marka"].Value?.ToString() ?? string.Empty;
                txtIadeModel.Text = selectedRow.Cells["Model"].Value?.ToString() ?? string.Empty;
                //txtIadeKullanici.Text = selectedRow.Cells["Kullanici"].Value?.ToString() ?? string.Empty;
            }
        }

        private void TemizleIadeTextBoxlar()
        {
            txtMarka.Text = string.Empty;
            txtModel.Text = string.Empty;
            cmbKategori.SelectedIndex = -1; // Combobox'ı temizle
            numYas.Value = numYas.Minimum;
        }


        private void IadeListesiYukle()
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    SqlDataAdapter da = new SqlDataAdapter(@"
                SELECT 
                    AracID,
                    Marka,
                    Model,
                    Kategori,
                    Yas,
                    Durum
                FROM Araclar
                WHERE Durum = 'Kiralandı'", conn);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvIadeListesi.AutoGenerateColumns = true; // Otomatik kolonlar
                    dgvIadeListesi.DataSource = dt;
                    dgvIadeListesi.ClearSelection();
                    dgvIadeListesi.Refresh();

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Kiralanmış araç bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Veritabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void KategorileriYukle()
        {
            cmbKategori.Items.Clear();
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Kategori FROM Araclar", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbKategori.Items.Add(reader["Kategori"].ToString());
                }
                reader.Close();
            }
            cmbKategori.SelectedIndex = -1; // İlk başta hiçbir kategori seçili olmasın
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
            // Form yüklendiğinde DataGridView olaylarını bağla ve listeleri yükle
            dgvAraclar.SelectionChanged += dgvAraclar_SelectionChanged;
            dgvIadeListesi.SelectionChanged += dgvIadeListesi_SelectionChanged;
            AraclariYukle();
            IadeListesiYukle();// Form yüklenirken listeyi getir
           // dgvIadeListesi.ClearSelection(); // Varsayılan seçimi temizle
            RezerveListele();
            YorumlariListele();
            KategorileriYukle(); // Form yüklendiğinde kategorileri yükle

        }

        // Eğer IadeListesiYukle metodu DataGridView'i dolduruyorsa, bu metotun içinde veya sonrasında
        // ilk satır seçili gelsin isterseniz aşağıdaki gibi bir kod ekleyebilirsiniz:


        private void btnYorumSil_Click(object sender, EventArgs e)
        {

            if (dgvYorumlar.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek bir yorum seçiniz.");
                return;
            }

            int yorumId = Convert.ToInt32(dgvYorumlar.SelectedRows[0].Cells["YorumID"].Value);

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open(); // BAĞLANTI BURADA AÇILIYOR
                    SqlCommand cmd = new SqlCommand("DELETE FROM Yorumlar WHERE YorumID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", yorumId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yorum silindi.");
                    YorumlariListele(); // Güncelle

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

        private void YorumlariListele()
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(@"
            SELECT 
                Y.YorumID,
                K.Isim + ' ' + K.Soyisim AS Kullanici,
                A.Marka + ' ' + A.Model AS Arac,
                Y.Puan,
                Y.Yorum,
                Y.Tarih
            FROM Yorumlar Y
            INNER JOIN Kullanicilar K ON Y.KullaniciID = K.KullaniciID
            INNER JOIN Araclar A ON Y.AracID = A.AracID
            ORDER BY Y.Tarih DESC", conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvYorumlar.DataSource = dt;

                // Gizlemek istersen yorum ID'yi görünmez yapabilirsin:
                if (dgvYorumlar.Columns.Contains("YorumID"))
                    dgvYorumlar.Columns["YorumID"].Visible = false;
            }
        }

        private void dgvAraclar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bu olay şu anda kullanılmıyor.
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                        string query = @"
                SELECT
                    KUL.Isim + ' ' + KUL.Soyisim AS Kullanici,
                    AR.Marka,
                    AR.Model,
                    KIR.KiralamaTarihi,
                    KIR.Durum AS OdemeDurumu,
                    AR.Durum AS AracDurumu
                FROM Araclar AR
                LEFT JOIN Kiralamalar KIR ON AR.AracID = KIR.AracID AND KIR.Durum = 'Aktif'
                LEFT JOIN Kullanicilar KUL ON KIR.KullaniciID = KUL.KullaniciID
                WHERE AR.Durum IN ('Kiralandı', 'Müsait')
                ORDER BY KIR.KiralamaTarihi DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvRezervasyonlar.DataSource = dt;

                        // Kolon başlıklarını kontrol ederek ayarlama
                        if (dgvRezervasyonlar.Columns.Contains("Kullanici"))
                            dgvRezervasyonlar.Columns["Kullanici"].HeaderText = "Kullanıcı";
                        if (dgvRezervasyonlar.Columns.Contains("Marka"))
                            dgvRezervasyonlar.Columns["Marka"].HeaderText = "Marka";
                        if (dgvRezervasyonlar.Columns.Contains("Model"))
                            dgvRezervasyonlar.Columns["Model"].HeaderText = "Model";
                        if (dgvRezervasyonlar.Columns.Contains("KiralamaTarihi"))
                            dgvRezervasyonlar.Columns["KiralamaTarihi"].HeaderText = "Kiralama Tarihi";
                        if (dgvRezervasyonlar.Columns.Contains("OdemeDurumu"))
                            dgvRezervasyonlar.Columns["OdemeDurumu"].HeaderText = "Ödeme Durumu";
                        if (dgvRezervasyonlar.Columns.Contains("AracDurumu"))
                            dgvRezervasyonlar.Columns["AracDurumu"].HeaderText = "Araç Durumu";
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

        private void btnEkle_MouseEnter(object sender, EventArgs e)
        {
            if (btnEkle.BackColor == Color.FromArgb(223, 238, 215))
            {
                btnEkle.BackColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                btnEkle.BackColor = Color.FromArgb(223, 238, 215);
            }
        }

        private void btnEkle_MouseLeave(object sender, EventArgs e)
        {

            if (btnEkle.BackColor == Color.FromArgb(255, 255, 255))
            {
                btnEkle.BackColor = Color.FromArgb(223,238,215);
            }
            else
            {
                btnEkle.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void btnSil_MouseEnter(object sender, EventArgs e)
        {
            if (btnSil.BackColor == Color.FromArgb(223, 238, 215))
            {
                btnSil.BackColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                btnSil.BackColor = Color.FromArgb(223, 238, 215);
            }
        }

        private void btnSil_MouseLeave(object sender, EventArgs e)
        {

            if (btnSil.BackColor == Color.FromArgb(255, 255, 255))
            {
                btnSil.BackColor = Color.FromArgb(223, 238, 215);
            }
            else
            {
                btnSil.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.FromArgb(255, 228, 225))
            {
                button1.BackColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                button1.BackColor = Color.FromArgb(255, 228, 225);
            }
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.FromArgb(255, 228, 225))
            {
                button1.BackColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                button1.BackColor = Color.FromArgb(255, 228, 225);
            }
        }

        private void btnIadeEt_MouseEnter(object sender, EventArgs e)
        {
            if (btnIadeEt.BackColor == Color.FromArgb(244, 219, 202))
            {
                btnIadeEt.BackColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                btnIadeEt.BackColor = Color.FromArgb(244, 219, 202);
            }
        }

        private void btnIadeEt_MouseLeave(object sender, EventArgs e)
        {
            if (btnIadeEt.BackColor == Color.FromArgb(255, 255, 255))
            {
                btnIadeEt.BackColor = Color.FromArgb(244, 219, 202);
            }
            else
            {
                btnIadeEt.BackColor = Color.FromArgb(255, 255, 255);
            }
        }

        private void txtIadeMarka_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnYorumSil_MouseEnter(object sender, EventArgs e)
        {
            if (btnYorumSil.BackColor == Color.FromArgb(222, 199, 241))
            {
                btnYorumSil.BackColor = Color.FromArgb(255, 255, 255);
            }
            else
            {
                btnYorumSil.BackColor = Color.FromArgb(222, 199, 241);
            }
        }

        private void btnYorumSil_MouseLeave(object sender, EventArgs e)
        {
            if (btnYorumSil.BackColor == Color.FromArgb(255,255,255))
            {
                btnYorumSil.BackColor = Color.FromArgb(222,199,241);
            }
            else
            {
                btnYorumSil.BackColor = Color.FromArgb(255,255,255);
            }
        }

        private void dgvRezervasyonlar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvRezervasyonlar.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRezervasyonlar.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvRezervasyonlar.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }

        private void dgvIadeListesi_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }
    }
}
