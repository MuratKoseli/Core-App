using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Kurs
    {
        [Display(Name = "Kurs ID")]
        public int KursId { get; set; }

        [Display(Name = "Kurs Adı")]
        public string? Baslik { get; set; }
        public int OgretmenId { get; set; }
        public Ogretmen Ogretmen { get; set; } = null!; // data klasörü altına yeni bir Ogretmen.cs açtık ve bu satırla birlikte her kursa bir öğretmen atadık. Her bir kurs için sadece tek bir öğretmen ataması var.

        public ICollection<KursKayit> KursKayitleri { get; set; } = new List<KursKayit>();

    }
}