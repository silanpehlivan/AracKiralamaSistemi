🚗 Araç Kiralama Yönetim Sistemi (AracK1)

Bu proje, yazılım mühendisliği disiplinleri temel alınarak geliştirilmiş, uçtan uca araç kiralama süreçlerini yöneten masaüstü tabanlı bir otomasyon sistemidir. Sistem, akademik standartlara uygun olarak V-Modeli (Doğrulama ve Onaylama) yaklaşımı ile tasarlanmış ve geliştirilmiştir.

---

🎯 Projenin Amacı

Bu projenin temel amacı, araç kiralama işletmelerinin operasyonel süreçlerini daha verimli, düzenli ve dijital hale getirmektir. Bu kapsamda:

- Gereksinim Analizi: Kullanıcı ve işletme ihtiyaçlarının detaylı şekilde belirlenmesi  
- Sistem Tasarımı: Modüler yapı ve veritabanı ilişkilerinin planlanması  
- V-Modeli Uygulaması: Her geliştirme aşamasının test süreçleriyle doğrulanması (Unit & Integration Testing)  
- Kullanıcı Deneyimi: Hem müşteri hem de yönetici için kullanıcı dostu arayüz tasarımı  

---

📚 Temel Özellikler

## 👤 Kullanıcı Modülü

- Rezervasyon Yönetimi: Araçların tarih bazlı müsaitlik kontrolü ve kiralama işlemleri  
- Ödeme Entegrasyonu: Güvenli ödeme simülasyonu ve kart doğrulama sistemi  
- Geri Bildirim Sistemi: Kiralanan araçlara yorum ve puan verme özelliği  

---

## 🔐 Yönetici (Admin) Modülü

- Filo Yönetimi: Araç ekleme, silme ve güncelleme işlemleri  
- Kayıt Yönetimi: Kullanıcı ve sistem verilerinin kontrolü  
- Operasyon Takibi: Kiralama istatistikleri ve performans analizleri  

---

⚙️ Teknik Detaylar

| Özellik | Açıklama |
|----------|----------|
| Dil | C# |
| Framework | .NET Framework (WinForms) |
| Metodoloji | V-Modeli SDLC |
| Veritabanı | Microsoft SQL Server |
| Test | MSTest (Unit & Integration Testing) |
| Paradigma | Nesne Yönelimli Programlama (OOP) |

---

💻 Implementasyon Detayları

Proje sadece bir yazılım değil, aynı zamanda bir mühendislik yaklaşımı ile geliştirilmiştir. Katmanlı mimari sayesinde iş mantığı ve kullanıcı arayüzü birbirinden ayrılmıştır.

### 📌 Örnek Test Metodu

```csharp
[TestMethod]
public void KartDogrula_GecerliBilgiler_TrueDonmeli()
{
    // Arrange
    string kartNo = "1234567812345678";
    string cvv = "123";

    // Act
    bool sonuc = OdemeIslemleri.KartDogrula(
        kartNo,
        cvv,
        "1234567812345678",
        "123"
    );

    // Assert
    Assert.IsTrue(sonuc);
}
```

---

Uygulama içerisinde tüm kritik işlemler unit testler ile doğrulanmakta ve dinamik kaynak yönetimi ile sistem performansı artırılmaktadır.

---

🚀 Kurulum ve Çalıştırma

1. Projeyi indirip bir klasöre çıkarın  
2. `AracKiralamaSistemi.sln` dosyasını Visual Studio ile açın  
3. `AracK1/Data/SqlHelper.cs` dosyasındaki connection string’i düzenleyin  
4. Veritabanını oluşturun (aracKiralamaSistemi)  
5. Projeyi derleyip F5 ile çalıştırın  

---

📂 Proje Yapısı

```
AracKiralamaSistemi-master/
├── AracK1/               # Ana uygulama (WinForms)
│   ├── BusinessLogic/    # İş mantığı katmanı
│   ├── Data/             # Veritabanı erişimi
│   ├── Forms/            # Arayüz ekranları
│   ├── Models/           # Veri modelleri
│   └── Resources/        # Görseller
├── TestProject1/         # Unit test projesi
└── AracKiralamaSistemi.sln
```

---

📜 Lisans

Bu proje MIT Lisansı kapsamında lisanslanmıştır. Detaylar LICENSE dosyasında yer almaktadır.

---

👩‍💻 Yazar

Şilan Pehlivan
