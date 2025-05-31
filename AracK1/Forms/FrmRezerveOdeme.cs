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
using AracK1.Models;
using AracK1.Data;
//using AracK1.BusinessLogic;
namespace AracK1.Forms
{
    public partial class FrmRezerveOdeme : Form
    {

        private int kullaniciId;
        private int aracId;
        private string kullaniciKartNumarasi; // Kart numarasını saklamak için
        private string kullaniciCVV;        // CVV'yi saklamak için
        private AracDetay aracDetayBilgisi;
        private AracDetay aracBilgisi;


        // Kayıt formundan kart bilgilerini alacak şekilde güncellenmiş yapıcı metot
        public FrmRezerveOdeme(int kullaniciId, int aracId, AracDetay aracBilgisi, string kartNumarasi, string cvv)
        {
            InitializeComponent();
            this.kullaniciId = kullaniciId;
            this.aracId = aracId;
            this.aracDetayBilgisi = aracBilgisi;
            this.kullaniciKartNumarasi = kartNumarasi; // Kayıt formundan gelen kart no
            this.kullaniciCVV = cvv;                   // Kayıt formundan gelen CVV
        }

        public FrmRezerveOdeme()
        {
            InitializeComponent();
        }

        public FrmRezerveOdeme(int kullaniciId, int aracId, AracDetay aracBilgisi)
        {
            InitializeComponent(); // BU SATIR EKLENDİ
            this.kullaniciId = kullaniciId;
            this.aracId = aracId;
            this.aracBilgisi = aracBilgisi;
        }


        private void btnOdemeYap_Click(object sender, EventArgs e)
        {
            //Test2:Kart Doğrulama İşlemi:Esranur Avcı
            //// OdemeIslemleri sınıfındaki KartDogrula metodunu çağırıyoruz
            //// Kullanıcının girdiği bilgiler (txtKartNo.Text, txtCVV.Text)
            //// ile formun constructor'ında gelen bilgiler (kullaniciKartNumarasi, kullaniciCVV) karşılaştırılacak.
            //bool kartDogru = OdemeIslemleri.KartDogrula(txtKartNo.Text, txtCVV.Text, kullaniciKartNumarasi, kullaniciCVV);

            //if (kartDogru)
            //{
            //    // Ödeme başarılı mesajı ve ilgili işlemler
            //    lblSonuc.Text = "Ödeme başarılı.";
            //    MessageBox.Show(
            //        $"Textbox Kart No: {txtKartNo.Text}\n" +
            //        $"Textbox CVV: {txtCVV.Text}\n" +
            //        $"DB Kart No: {kullaniciKartNumarasi}\n" + // Artık DB'den gelen değil, constructor'dan gelen bilgi
            //        $"DB CVV: {kullaniciCVV}", "Kart Doğrulama", MessageBoxButtons.OK, MessageBoxIcon.Information
            //    );

            //    // Textbox’ları temizle
            //    txtKartNo.Clear();
            //    txtCVV.Clear();

            //    // Kullanıcı mesajı görsün diye bekle (2 saniye)
            //    // Not: Bu kısım UI thread'i bloklayabilir, await/async kullanmak daha iyi.
            //    // Ancak bu örnekte orijinal kodu koruduk.
            //    Task.Delay(1000).ContinueWith(_ =>
            //    {
            //        this.Invoke(new Action(() =>
            //        {
            //            this.Hide(); // Önce formu gizle
            //            // FrmYorumlar'a gerekli parametreleri gönderdiğinizden emin olun
            //            FrmYorumlar yorumlarForm = new FrmYorumlar(kullaniciId, aracId);
            //            yorumlarForm.ShowDialog(); // Yorum formunu aç
            //            this.Close(); // Son olarak kendini kapat
            //        }));
            //    });
            //}
            //else
            //{
            //    // Ödeme başarısız mesajı
            //    lblSonuc.Text = "Geçersiz kart bilgisi.";
            //    MessageBox.Show(
            //        $"Textbox Kart No: {txtKartNo.Text}\n" +
            //        $"Textbox CVV: {txtCVV.Text}\n" +
            //        $"DB Kart No: {kullaniciKartNumarasi}\n" + // Aynı şekilde, constructor'dan gelen bilgi
            //        $"DB CVV: {kullaniciCVV}", "Kart Doğrulama Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Warning
            //    );
            //}



            // Kayıt ekranından alınan kart ve CVV bilgilerini kullan
            string dbKart = kullaniciKartNumarasi;
            string dbCVV = kullaniciCVV;
            bool kartDogru = false;

            using (SqlConnection conn = SqlHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    if (txtKartNo.Text == dbKart && txtCVV.Text == dbCVV)
                    {
                        lblSonuc.Text = "Ödeme başarılı.";
                        kartDogru = true;
                        // Kart bilgileri eşleştiğinde bilgilendirme göster
                        MessageBox.Show(
                            $"Textbox Kart No: {txtKartNo.Text}\n" +
                            $"Textbox CVV: {txtCVV.Text}\n" +
                            $"DB Kart No: {kullaniciKartNumarasi}\n" +
                            $"DB CVV: {kullaniciCVV}", "Kart Doğrulama", MessageBoxButtons.OK, MessageBoxIcon.Information
                        );

                        // Textbox’ları temizle
                        txtKartNo.Clear();
                        txtCVV.Clear();

                        // Kullanıcı mesajı görsün diye bekle (2 saniye)
                        Task.Delay(1000).ContinueWith(_ =>
                        {
                            // UI thread'e geri dönmek için Invoke kullanılır
                            this.Invoke(new Action(() =>
                            {
                                this.Hide(); // Önce formu gizle
                                FrmYorumlar yorumlarForm = new FrmYorumlar(kullaniciId, aracId);
                                yorumlarForm.ShowDialog(); // Yorum formunu aç
                                this.Close(); // Son olarak kendini kapat
                            }));
                        });

                    }

                    else
                    {
                        lblSonuc.Text = "Geçersiz kart bilgisi.";
                        MessageBox.Show(
                            $"Textbox Kart No: {txtKartNo.Text}\n" +
                            $"Textbox CVV: {txtCVV.Text}\n" +
                            $"DB Kart No: {kullaniciKartNumarasi}\n" +
                            $"DB CVV: {kullaniciCVV}", "Kart Doğrulama Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Warning
                        );
                    }
                }
                catch (SqlException ex)
                {
                    lblSonuc.Text = "Veritabanı hatası: " + ex.Message;
                }
                catch (Exception ex)
                {
                    lblSonuc.Text = "Bir hata oluştu: " + ex.Message;
                }

            }
        
        }

        private void FrmRezerveOdeme_Load(object sender, EventArgs e)
        {
            try
            {
                if (aracDetayBilgisi != null)
                {
                    lblAracBilgi.Text = $"Araç: {aracDetayBilgisi.Marka} {aracDetayBilgisi.Model} ({aracDetayBilgisi.Kategori}, {aracDetayBilgisi.Yas} Yaşında)";
                }
                else if (aracBilgisi != null) // Eğer aracDetayBilgisi null ise aracBilgisi'ni kullan
                {
                    lblAracBilgi.Text = $"Araç: {aracBilgisi.Marka} {aracBilgisi.Model} ({aracBilgisi.Kategori}, {aracBilgisi.Yas} Yaşında)";
                }
                else
                {
                    lblAracBilgi.Text = "Araç bilgisi yüklenemedi.";
                }

                // Form yüklendiğinde text box'lara kart ve CVV bilgisini yazdırıyoruz
                txtKartNo.Text = kullaniciKartNumarasi;
                txtCVV.Text = kullaniciCVV;
            }
            catch (Exception ex)
            {
                lblAracBilgi.Text = "Araç bilgisi yüklenirken bir hata oluştu: " + ex.Message;
            }
        }

        private void FrmRezerveOdeme_MouseEnter(object sender, EventArgs e)
        {
            if (btnOdemeYap.BackColor == Color.FromArgb(215, 228, 242))
            {
                btnOdemeYap.BackColor = Color.FromArgb(253, 253, 253);
            }
            else
            {
                btnOdemeYap.BackColor = Color.FromArgb(215, 228, 242);
            }
        }

        private void btnOdemeYap_MouseLeave(object sender, EventArgs e)
        {
            if (btnOdemeYap.BackColor == Color.FromArgb(253,253,253))
            {
                btnOdemeYap.BackColor = Color.FromArgb(215,228,242);
            }
            else
            {
                btnOdemeYap.BackColor = Color.FromArgb(253,253,253);
            }
        }
    }
    
}
