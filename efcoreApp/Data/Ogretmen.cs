using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogretmen
    {
        [Key]
        [Display(Name = "ID")]
        public int OgretmenId { get; set; }
        public string? Ad { get; set; }
        public string? Soyad { get; set; }

        public string AdSoyad { 
            get
            {
               return this.Ad + " " + this.Soyad;
            }
         }
        public string? Mail { get; set; }
        public string? Telefon { get; set; }

        [DataType(DataType.Date)]//Date Time saati de verdiği için biz sadece bu komutla tarihi alıyoruz. Saati istemiyoruz.  
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Başlama Tarihi")]
        public DateTime BaslamaTarihi { get; set; }
        public ICollection<Kurs> Kurslar { get; set; } = new List<Kurs>(); // Bir öğretmenin birçok kursu olabilir
    }
}