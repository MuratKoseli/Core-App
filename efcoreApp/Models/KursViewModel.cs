using System.ComponentModel.DataAnnotations;

using efcoreApp.Data;

namespace efcoreApp.Models
{
    public class KursViewModel
    {
        [Display(Name = "Kurs ID")]
        public int KursId { get; set; }

        [Display(Name = "Kurs Adı")]

        [Required]
        public string? Baslik { get; set; }
        public int OgretmenId { get; set; }

         public ICollection<KursKayit> KursKayitleri { get; set; } = new List<KursKayit>();


    }
}