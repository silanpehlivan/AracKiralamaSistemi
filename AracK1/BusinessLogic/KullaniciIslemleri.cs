//using System;
//using System.Data.SqlClient;
//using AracK1.Data; // SqlHelper sınıfının bulunduğu namespace

//namespace AracK1.BusinessLogic
//{
//    public static class KullaniciIslemleri
//    {
//        // Kullanıcı girişi yapar ve başarılıysa KullaniciID'yi, başarısızsa null döndürür.
//        public static int? GirisYap(string email, string sifre)
//        {
//            using (SqlConnection conn = SqlHelper.GetConnection())
//            {
//                try
//                {
//                    conn.Open();
//                    string query = "SELECT KullaniciID FROM Kullanicilar WHERE Email = @Email AND Sifre = @Sifre";
//                    SqlCommand cmd = new SqlCommand(query, conn);
//                    cmd.Parameters.AddWithValue("@Email", email);
//                    cmd.Parameters.AddWithValue("@Sifre", sifre);
//                    var result = cmd.ExecuteScalar();

//                    if (result != null)
//                    {
//                        return Convert.ToInt32(result);
//                    }
//                    else
//                    {
//                        return null; // Kullanıcı bulunamadı
//                    }
//                }
//                catch (SqlException ex)
//                {
//                    // Veritabanı ile ilgili özel bir hata yakalandığında
//                    // Hata mesajını veya loglama yapabilirsiniz.
//                    // Birim testi için genellikle null döndürmek yeterlidir.
//                    Console.WriteLine("SQL Hatası: " + ex.Message); // Geliştirme aşamasında konsola yazdırabilirsiniz
//                    return null;
//                }
//                catch (Exception ex)
//                {
//                    // Diğer genel hatalar için
//                    Console.WriteLine("Genel Hata: " + ex.Message); // Geliştirme aşamasında konsola yazdırabilirsiniz
//                    return null;
//                }
//            }
//        }
//    }
//}

