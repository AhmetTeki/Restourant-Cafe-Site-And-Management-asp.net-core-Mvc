# Restourant-Cafe-Site-And-Management-asp.net-core-Mvc
Asp.Net Core MVC kullanarak bir restourant yönetim sitesi yaptım. Bu sitede hesap oluşturarak menü, galeri, hakkımızda gibi şeyler ekleyip müşterilerin rezervasyonlarını görebilirsiniz. Ayrıca müşteri olarak yorum yapma ve rezervasyon oluşturma işlemleri yapabilirsiniz. Daha sonra kayıt olup admin sayfasından rezervasyonları ve yorumları görebilirsiniz
1-)PRORAMIN DOĞRU ÇALIŞABİLMESİ İÇİN AŞAĞIDAKİ ADIMLARI SIRASI İLE UYGULAYINIZ
2-)CAFE KLOSÖRÜNÜN ALTINA appsettings.json ADINDA BİR DOSYA EKLEYİN. ARDINDAN BU DOSYANIN İÇİNE AŞAĞIDAKİ KODLARI EKLEYİN


{
  "ConnectionStrings": {
    "DefaultConnection": "YOUR DB Path"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}


4-)ARDINDAN YOUR DB PATH YAZAN YERE KENDİ DATABASE YOLUNUZU YAZINIZ
5-)ARDINDAN TERMİNALİ AÇARAK MİGRATİON OLUŞTURUN VE GÜNCELLEYİN
6-)PROJEYİ KULLANABİLİRSİNİZ
