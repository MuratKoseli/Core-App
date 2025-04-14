using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;
        public KursController(DataContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(int? id)
        {
            var kurs = await _context.Kurslar.Include(k => k.Ogretmen).ToListAsync();
            return View(kurs);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            // Kurs eklerken aynı anda kursu hangi öğretmenin verdiğini de select ile seçiyoruz ve bunu da bu şekilde Viewbag ile taşıyoruz.
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursViewModel model)
        {
            if (ModelState.IsValid)
            {
                _context.Kurslar.Add(new Kurs() { KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");

                return View(model);
            

        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var krs = await _context
                            .Kurslar
                            .Include(o => o.KursKayitleri)
                            .ThenInclude(o => o.Ogrenci)
                            .Select(o => new KursViewModel
                            {
                                KursId = o.KursId,
                                Baslik = o.Baslik,
                                OgretmenId = o.OgretmenId,
                                KursKayitleri = o.KursKayitleri
                            }) // Bu konuyu iyice araştır(Model kısmına yeni bir model oluşturduk KursViewModel isminde)
                            .FirstOrDefaultAsync(o => o.KursId == id);


            if (krs == null)
            {
                return NotFound();
            }

            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");

            return View(krs);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, KursViewModel model)
        {
            if (id != model.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(new Kurs() { KursId = model.KursId, Baslik = model.Baslik, OgretmenId = model.OgretmenId });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Kurslar.Any(o => o.KursId == model.KursId))
                    {
                        return NotFound();
                    }

                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Ogretmenler = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var krs = await _context.Kurslar.FindAsync(id);

            if (krs == null)
            {
                return NotFound();
            }

            return View(krs);
        }

        [HttpPost]

        public async Task<IActionResult> Delete([FromForm] int id)
        {
            var krs = await _context.Kurslar.FindAsync(id);
            if (krs == null)
            {
                return NotFound();
            }
            _context.Kurslar.Remove(krs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}