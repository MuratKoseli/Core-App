using efcoreApp.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace efcoreApp.Controllers
{
    public class OgrenciController : Controller
    {

        private readonly DataContext _context;
        // DataContext isimli bir sınıfın örneği (instance) olan _context değişkeni tanımlanıyor.
        // readonly anahtar kelimesi, bu değişkenin sadece sınıfın kurucusu (constructor) içinde veya tanımlandığı anda atanabileceği anlamına gelir.

        public OgrenciController(DataContext context)
        {
            _context = context;
        }

        // Bu, bir constructor (kurucu metot). Controller sınıfının bir örneği oluşturulduğunda çağrılır.
        // DataContext türünde bir parametre alır ve bu parametre _context değişkenine atanır.
        // DI (Dependency Injection) kullanılarak uygulamanın DataContext örneği buraya enjekte ediliyor.
        // Bu, Controller'da veritabanı işlemlerini gerçekleştirebilmek için kullanılan bir veritabanı bağlamıdır.



        public async Task<IActionResult> Index()
        {
            var ogrenci = await _context.Ogrenciler.ToListAsync();
            return View(ogrenci);

            // Öğrencileri listeleme işlemi yapıyor
            // Tek satırda şu şekilde de yazılabilir:  return View(await _context.Ogrenciler.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Ogrenci model)
        {
            _context.Ogrenciler.Add(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            // await: işlemin tamamlanmasını beklerken uygulamanın diğer işler yapmasına izin verir, bu da performansı artırır ve uygulamanın yanıt verebilirliğini korur.
        }


        [HttpGet]
        // Öğrenci bilgilerini Edit sayfasına taşımamız için yazdığımız komut.
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var ogr = await _context
                            .Ogrenciler
                            .Include(o => o.KursKayitlari)
                            .ThenInclude(o => o.Kurs)
                            .FirstOrDefaultAsync(o=>o.OgrenciId == id);
                            // Ogrencilerdeki ICollection<KursKayit> olarak belirttiğimiz listeleme ile KursKayitlari'na oradan da ThenInclude diyerek Kurs'a giderek Kurs içindeki proplara ulaşıyoruz

            // var ogr = await _context.Ogrenciler.FirstOrDefaultAsync(ogrenciler => ogrenciler.OgrenciId==id);
            // Ogrencilerdeki id yukarıdaki yani (int? id) id ye eşitse ogr ye atar

            if (ogr == null)
            {
                return NotFound();
            }
            return View(ogr);
        }
        // Geri dönecek değerler null olabileceğinden if komutuyla hata mesajı veriyoruz. Mesela bir id girilmezse birinci kısım eğer girilen id veritabanında yoksa ikinci kısım çalışır. ilgili id ile veritabanında bir kayıt olmayabilir o yüzden if(ogr == null) bu komutu yazıyoruz.
        // FindAsync() methodu doğrudan id ye göre arama işlemi yapabiliyor. Ama FirstOrDefaultAsync() methodu sadece id ye göre değil farklı kriterlere göre de arama yapılabilir. Mesela; ad, soyad, mail gibi.

        [HttpPost]
        [ValidateAntiForgeryToken]  // bu bir güvenlik önlemidir.(araştır)

        // Bu yöntem, bir güncelleme (edit/update) işlemini gerçekleştirmek için kullanılır.
        public async Task<IActionResult> Edit(int id, Ogrenci model)
        {
            if (id != model.OgrenciId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogrenciler.Any(o => o.OgrenciId == model.OgrenciId))
                    {
                        return NotFound();
                    }
                    // Any herhangi bir demek yani formdan(modelden) gönderdiğimiz id ile veritabanında herhangi bir kayıt var mı yok mu. Başına ünlem koyduğumuz zaman şu demek; eğer herhangi bir kayıt yoksa bana return değerini geriye gönderir.  
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        // form içinden bilgilerimizin gitmesi için "Ogrenci model" ekledik.
        // Ogrenci model: Kullanıcıdan alınan yeni verileri taşır. Genellikle bir formdan gelen veriler bu model üzerinden controller'a gönderilir. model, Ogrenci sınıfının bir örneğidir ve düzenlenmiş verileri içerir.
        // 1- Gelen id ile güncellenmek istenen veriyi veritabanından getirirsiniz
        // 2- model ile gelen yeni bilgileri, veritabanından gelen nesneye uygularsınız
        // 3- Veritabanında değişiklikleri kaydedersiniz


        [HttpGet]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ogr = await _context.Ogrenciler.FindAsync(id);

            if (ogr == null)
            {
                return NotFound();
            }

            return View(ogr);
        }

        [HttpPost]

        public async Task<IActionResult> Delete([FromForm]int id)
        {
            var ogr = await _context.Ogrenciler.FindAsync(id);
            if(ogr==null)
            {
                return NotFound(); 
            }
            _context.Ogrenciler.Remove(ogr);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // [FromForm]int id: Form içerisinden name=id olan id yi aldığımızı belirtmemiz için kullandık. BU MODEL BINDING KONUSUDUR İNCELE!!!!
    }
}