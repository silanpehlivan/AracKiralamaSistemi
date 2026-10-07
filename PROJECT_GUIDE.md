<div align="center">

# Araç Kiralama Yönetimi

### Araç seçiminden rezervasyona uzanan bir deneyim.

![C#](https://img.shields.io/badge/C%23-2563eb?style=for-the-badge)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-0891b2?style=for-the-badge)
![SQL Server](https://img.shields.io/badge/SQL%20Server-7c3aed?style=for-the-badge)
[![MIT](https://img.shields.io/badge/MIT-16a34a?style=for-the-badge)](LICENSE)

Araç kiralama süreçlerini kullanıcı ve yönetici modülleriyle ele alan, V-Modeli yaklaşımıyla geliştirilmiş masaüstü projesi.

**Rezervasyon ve filo operasyonları**

[Projeyi keşfet](https://github.com/silanpehlivan/AracKiralamaSistemi/tree/master) · [Kurulum ve ayrıntılar](#projeyi-çalıştırmak-ve-incelemek)

</div>

---

## İçeride neler var?

- **01** · Rezervasyon ve tarih bazlı müsaitlik
- **02** · Filo, kullanıcı ve geri bildirim yönetimi
- **03** · Ödeme simülasyonu, raporlama ve MSTest çalışmaları

## Projeyi çalıştırmak ve incelemek

<details>
<summary><strong>Kurulum, kod yapısı ve teknik notları aç</strong></summary>

## Öne Çıkanlar

- Rezervasyon ve tarih bazlı müsaitlik
- Filo, kullanıcı ve geri bildirim yönetimi
- Ödeme simülasyonu, raporlama ve MSTest çalışmaları

## Teknolojiler

C# · Windows Forms · SQL Server

### Teknik yaklaşım

Forms, BusinessLogic, Data ve Models dizinleri kullanıcı etkileşimi, işlem mantığı ve veri erişimini ayırır. MSTest dosyaları kullanıcı ve ödeme akışlarının incelenmesine olanak verir.

### Kodu incelemeye başlayın

- [AracK1.Tests/UnitTest1.cs](AracK1.Tests/UnitTest1.cs)
- [AracK1.Tests1.0/KullaniciTestleri.cs](AracK1.Tests1.0/KullaniciTestleri.cs)
- [AracK1/Program.cs](AracK1/Program.cs)
- [TestProject1/KullaniciTestleri.cs](TestProject1/KullaniciTestleri.cs)

### Kapsam ve sınırlar

Ödeme akışı simülasyondur. Test dosyalarının bulunması güncel çalıştırmanın başarılı olduğunu veya ölçülmüş kapsam oranını göstermez.



Bu proje, yazılım mühendisliği disiplinleri temel alınarak geliştirilmiş, uçtan uca araç kiralama süreçlerini yöneten masaüstü tabanlı bir otomasyon sistemidir. Sistem, akademik standartlara uygun olarak V-Modeli (Doğrulama ve Onaylama) yaklaşımı ile tasarlanmış ve geliştirilmiştir.

---

 Projenin Amacı
---

Bu projenin temel amacı, araç kiralama işletmelerinin operasyonel süreçlerini dijitalleştirerek daha verimli ve yönetilebilir bir yapı oluşturmaktır.

Bu kapsamda:

- Gereksinim Analizi: Kullanıcı ve işletme ihtiyaçlarının detaylı şekilde belirlenmesi  
- Sistem Tasarımı: Modüler yapı ve veritabanı ilişkilerinin planlanması  
- V-Modeli Uygulaması: Geliştirme aşamalarının test süreçleri ile doğrulanması (Unit & Integration Testing)  
- Kullanıcı Deneyimi: Müşteri ve yönetici için modern ve kullanıcı dostu arayüz tasarımı  

---

 Temel Özellikler
---

## Kullanıcı Modülü

- Rezervasyon Yönetimi: Araçların tarih bazlı müsaitlik kontrolü ve kiralama işlemleri  
- Ödeme Entegrasyonu: Güvenli ödeme simülasyonu ve kart doğrulama sistemi  
- Geri Bildirim Sistemi: Kiralanan araçlara yorum ve puan verme özelliği  

---

## Yönetici (Admin) Modülü

- Filo Yönetimi: Araç ekleme, silme ve güncelleme işlemleri  
- Kayıt Yönetimi: Kullanıcı ve sistem verilerinin merkezi kontrolü  
- Operasyon Takibi: Kiralama istatistikleri ve sistem performans analizleri  
- Detaylı Raporlama: En çok kiralanan araçlar ve gelir analizleri  

---

 Teknik Detaylar
---

| Özellik | Açıklama |
|----------|----------|
| Dil | C# |
| Framework | .NET Framework (WinForms) |
| Metodoloji | V-Modeli SDLC |
| Veritabanı | Microsoft SQL Server |
| Test | MSTest (Unit & Integration Testing) |
| Paradigma | Nesne Yönelimli Programlama (OOP) |

---

 Implementasyon Detayları
---

Proje katmanlı mimari ile geliştirilmiş olup iş mantığı, veri erişimi ve arayüz katmanları birbirinden ayrılmıştır.

Depoda kullanıcı ve ödeme işlemleri için test dosyaları bulunur; güncel test sonuçları ayrıca çalıştırılarak doğrulanmalıdır.

---

 Kurulum ve Çalıştırma
---

1. Projeyi indirip klasöre çıkarın  
2. `AracK1.sln` dosyasını Visual Studio ile açın  
3. `SqlHelper.cs` içindeki connection string’i düzenleyin  
4. Veritabanını oluşturun (aracKiralamaSistemi)  
5. Projeyi derleyip F5 ile çalıştırın  

---

 Proje Yapısı
---

```text
AracKiralamaSistemi-master/
├── AracK1/
│   ├── BusinessLogic/
│   ├── Data/
│   ├── Forms/
│   ├── Models/
│   └── Resources/
├── TestProject1/
└── AracK1.sln
```

---




</details>

---

<div align="center">

**© 2024 Şilan PEHLİVAN and Esranur AVCI**

Bu proje MIT lisansı kapsamında sunulmaktadır. Kullanım ve dağıtım koşulları: [LICENSE](LICENSE).

</div>
