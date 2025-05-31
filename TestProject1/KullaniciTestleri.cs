//using Microsoft.VisualStudio.TestTools.UnitTesting; // MSTest için gerekli
//using AracK1.BusinessLogic; // Oluşturduğumuz KullaniciIslemleri sınıfına erişim için

//Bu, geliştirici Şilan Pehlivan tarafından yazılmış, Kullanıcıİşlemleri sınıfı için bir birim testi örneğidir.
//namespace TestProject1
//{
//    [TestClass] // Bu sınıfın bir test sınıfı olduğunu belirtir
//    public class KullaniciTestleri
//    {
//        [TestMethod] // Bu metodun bir test metodu olduğunu belirtir
//        public void GecersizKullaniciGirisTesti()
//        {
//            // KullaniciIslemleri.GirisYap metodunu geçersiz e-posta ve şifre ile çağır
//            var sonuc = KullaniciIslemleri.GirisYap("yanlis@eposta.com", "1234");

//            // Assert.IsNull: Sonucun null olup olmadığını kontrol eder.
//            // Başarısız bir girişin null döndürmesi beklenir.
//            Assert.IsNull(sonuc, "Geçersiz kullanıcı girişi null döndürmeliydi.");
//        }

//        [TestMethod]
//        public void GecerliKullaniciGirisTesti()
//        {
//            // KullaniciIslemleri.GirisYap metodunu geçerli bir e-posta ve şifre ile çağır
//            // Buradaki "dogru@eposta.com" ve "1234" değerleri, veritabanınızda gerçekten var olan
//            // geçerli bir kullanıcıya ait olmalıdır, aksi takdirde bu test başarısız olur.
//            var sonuc = KullaniciIslemleri.GirisYap("dogru@eposta.com", "1234");

//            // Assert.IsNotNull: Sonucun null olup olmadığını kontrol eder.
//            // Başarılı bir girişin bir KullaniciID döndürmesi beklenir (null olmamalı).
//            Assert.IsNotNull(sonuc, "Geçerli kullanıcı girişi null döndürmemeliydi.");

//            // Opsiyonel: Eğer kullanıcı ID'sinin belirli bir değer olduğunu biliyorsanız, bunu da kontrol edebilirsiniz.
//            // Assert.AreEqual(beklenenKullaniciId, sonuc, "Kullanıcı ID eşleşmiyor.");
//        }
//    }
//}
