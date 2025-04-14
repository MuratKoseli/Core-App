using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Kurs> Kurslar => Set<Kurs>();
        public DbSet<Ogrenci> Ogrenciler => Set<Ogrenci>();
        public DbSet<KursKayit> KursKayitleri => Set<KursKayit>();
        public DbSet<Ogretmen> Ogretmenler => Set<Ogretmen>();

    }

    // Bu class yani DataContext: Veritabanı bağlantısı ve veri işlemleri için kullanılan merkezi sınıftır.
    // Bunların her biri birer prop'tur.
    // DbSet<T>: Veritabanındaki bir tabloyu veya tablo benzeri yapıyı temsil eder. DbSet ile, veritabanındaki tablolar üzerinde sorgular yapabilir, veri ekleyebilir, güncelleyebilir ve silebilirsiniz.
}