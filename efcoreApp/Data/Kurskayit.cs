using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }

        public int OgrenciId { get; set; }
        public Ogrenci Ogrenci { get; set; } = null!; // Join işlemi için yaptık. (detaylı araştır) navigation property


        public int KursId { get; set; }
        public Kurs Kurs { get; set; } = null!; // Join işlemi için yaptık. (detaylı araştır) navigation property


        public DateTime KayitTarihi { get; set; }

        // 1 numaralı ıd deki kursa 3 numaralı ıd deki öğrenci 5 numaralı ıd deki derse kayıt olacak
        // Many To Many ilişkisi olacak
    }
}