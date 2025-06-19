


![image](https://github.com/user-attachments/assets/0de64906-1e71-46db-8256-7daba75d4029)
















E-Ticaret Web Sitesi Projesi Raporu 
1. GİRİŞ VE KULLANILAN TEKNOLOJİLER 
Bu projede, ASP.NET Core MVC mimarisi kullanılarak Bootstrap, HTML, CSS ve JavaScript 
ile entegre çalışan, veri tabanı destekli dinamik bir e-ticaret web sitesi geliştirilmiştir. Web 
sitesi, kullanıcı dostu bir arayüz sunarak ürünlerin listelenmesini, detaylarının 
görüntülenmesini ve sepete eklenmesini sağlamaktadır. 
Proje kapsamında aşağıdaki teknolojiler kullanılmıştır: 
HTML/CSS: Temel sayfa yapıları ve stil tasarımı 
Bootstrap: Stilistik arayüzler ve UI bileşenleri 
JavaScript: Dinamik etkileşimler (butonlar, uyarılar vb.) 
ASP.NET Core MVC (C#): Sayfa yönlendirme, modelleme ve controller yapısı 
Entity Framework Core + SQLite: Veri işlemleri ve kalıcı veri saklama 
2. SİTE YAPISI VE SAYFALAR 
Web sitesi minimum 5 sayfa içerecek şekilde tasarlanmıştır: 
Ana Sayfa : 
Tanıtım amaçlı görseller içermektedir. 
Ürün resimleri, fiyat bilgisi , açıklaması ve sepete ekleme butonu bulunmaktadır. 
Ürünler(Kategori) Sayfası : 
Tüm ürünler veri tabanından dinamik olarak listelenmektedir. 
Ürün resimleri, fiyat bilgisi , açıklaması ve sepete ekleme butonu bulunmaktadır. 
Kullanıcı Kategorilere Göre istediği  ürünleri görüntüleyebilir. 
Sepet Sayfası): 
Kullanıcının sepete eklediği ürünler burada listelenir. 
Sepet işlemleri gerçekleştirilir (sil, toplam fiyat hesaplama vb.). 
Ödeme vb işlemler gerçekleştirilir. 
Ödeme gerçekleştiyse kullanıcıya bildirilir. 
Kayıt olma Sayfası:Kullanıcı email ve kullanıcı adı bilgileri ile kayıt işlemini gerçekleştirir. Her 
kullanıcı benzersiz email tanımlamalıdır. 
Giriş Sayfası:Kullanıcı giriş işlemini gerçekleştirir. 
Çıkış (Logout) Sayfası:Kullanıcı çıkış işlemini gerçekleştirir. 
Profil Sayfası:Kullanıcı adres isim,soyisim,telefon numarası vb bilgilerini düzenleyebilmekte 
ve geçmiş siparişlerini görüntüleyip siparişlerini yönetebilmektedir. 
3. VERİ TABANI VE MVC YAPISI 
Proje SQLite veritabanı kullanılarak geliştirilmiştir. Veriler MiniEcommerce.db dosyasında 
saklanmaktadır. Aşağıda, kullanılan temel tablolar yer almaktadır: 
Products: Ürün bilgileri (ID, Ad, Açıklama, Fiyat, ResimYolu) 
Categories: Ürün kategorileri 
Cart: Sepete eklenen ürünler 
Migrationlarla gerekli güncellemeler yapılmıştır. 
Dinamik olarak sepete eklenen ürünler veritabanında görüntülenmektedir.Eğer sipariş 
onaylandıysa sipariş tarihi(orderdate) ve siparişin durumu görüntülenebilmekte. 
Model-View-Controller Kullanımı: 
Model (Models klasörü): 
Product.cs, Category.cs gibi sınıflar, veri yapısını temsil eder. 
Entity Framework ile veritabanına bağlanma işlemleri yapılır. 
View (Views klasörü): 
Razor syntax (örneğin: @model Product) kullanılarak sayfa içerikleri dinamikleştirilmiştir. 
Bootstrap ile şık ve responsive tasarım yapılmıştır. 
Controller (Controllers klasörü): 
ProductController.cs, CartController.cs gibi sınıflar, gelen istekleri işler ve doğru görünümü 
döndürür. 
Örneğin: ProductController içindeki Index metodu ürünleri veritabanından çekerek View'a 
aktarır. 
Layout.cshtml ile genel tasarım oluşturulmuştur.  
Program.cs ise bu e-ticaret sitesinin temel yapı taşıdır. Gerekli servisleri (Session , route vb) 
yükler, veritabanını hazırlar, hata yönetimini belirler ve kullanıcıdan gelen istekleri doğru yere 
yönlendirir.
