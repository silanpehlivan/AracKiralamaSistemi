//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using AracK1.BusinessLogic; // OdemeIslemleri sınıfına erişim için

////Bu, geliştirici Esranur Avcı tarafından yazılmış, OdemeIslemleri sınıfı için bir birim testi örneğidir.
//namespace TestProject1
//{
//    [TestClass]
//    public class OdemeTestleri
//    {
//        // Testlerde kullanacağımız "beklenen" (DB'den geldiğini varsaydığımız) kart bilgileri
//        // Gerçek bir senaryoda bu değerler setup metodunda ayarlanabilir veya mocklanabilir.
//        private const string BEKLENEN_KART_NO = "4444555566667777";
//        private const string BEKLENEN_CVV = "321";

//        [TestMethod]
//        public void GecersizKartBilgisiTesti()
//        {
//            // Geçersiz bir kart numarası ve CVV veriyoruz, ancak beklenen doğru kart bilgileri ile karşılaştırılıyor.
//            var sonuc = OdemeIslemleri.KartDogrula("0000000000000000", "123", BEKLENEN_KART_NO, BEKLENEN_CVV);

//            // Assert.IsFalse: Sonucun false olmasını bekliyoruz çünkü kart bilgileri eşleşmiyor.
//            Assert.IsFalse(sonuc, "Geçersiz kart bilgisi false döndürmeliydi.");
//        }

//        [TestMethod]
//        public void GecerliKartBilgisiTesti()
//        {
//            // Geçerli kart numarası ve CVV veriyoruz, ve bu bilgiler beklenen doğru kart bilgileri ile eşleşiyor.
//            var sonuc = OdemeIslemleri.KartDogrula(BEKLENEN_KART_NO, BEKLENEN_CVV, BEKLENEN_KART_NO, BEKLENEN_CVV);

//            // Assert.IsTrue: Sonucun true olmasını bekliyoruz çünkü kart bilgileri eşleşiyor.
//            Assert.IsTrue(sonuc, "Geçerli kart bilgisi true döndürmeliydi.");
//        }
//    }
//}

