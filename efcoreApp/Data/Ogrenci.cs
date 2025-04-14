using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        [Key]
        [Display(Name = "Öğrenci ID")]
        public int OgrenciId { get; set; }

        [Display(Name = "Öğrenci Adı")]
        public string? OgrenciAd { get; set; }

        [Display(Name = "Öğrenci Soyadı")]
        public string? OgrenciSoyad { get; set; }

        [Display(Name ="Ad Soyad")]
        public string AdSoyad { 
            get
            {
                return this.OgrenciAd + " " + this.OgrenciSoyad;

            } 
            }
            // KursKayit kısmında öğrencinin ad soyadının birlikte gözükmesi için ekledik.

        public string? Mail { get; set; }
        public string? Telefon { get; set; }

        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
        //[[[Her bir öğrencinin katılmış olduğu kurs kayıtları için. Yani her bir öğrencinin katılabileceği birden fazla kurs kayıtları olduğu için bu şekilde bir liste tipinde (ICollection bir liste tipidir) KursKayit tipinde kayıtları alıyoruz. 
        // Dolayısıyla biz her öğrenciye ulaştığımız zaman bize o öğrencinin ait olduğu kurs kayıt bilgisini de getirir
        // new List<KursKayit>(); null alma durumundan dolayı yazdık bunu (araştır)]]]
    }
}