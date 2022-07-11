using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using anime_list.Data;

namespace anime_list.Controllers
{
    public class AnimeListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnimeListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AnimeLists
        public async Task<IActionResult> Index()
        {
              return _context.AnimeList != null ? 
                          View(await _context.AnimeList.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AnimeList'  is null.");
        }

        // GET: AnimeLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimeList == null)
            {
                return NotFound();
            }

            var animeList = await _context.AnimeList
                .FirstOrDefaultAsync(m => m.AnimeListId == id);
            if (animeList == null)
            {
                return NotFound();
            }

            //getting rating 
            var rating = await _context.UserRating.FirstOrDefaultAsync(m => m.AnimeListId == id);
                
            if(rating == null) 
            {

                var animeWithZeroRating = new AnimeWithRating()
                {
                    AnimeListId = animeList.AnimeListId,
                    Name = animeList.Name,
                    Description = animeList.Description,
                    ScreenTime = animeList.ScreenTime,
                    Image = animeList.Image,
                    AnimeRating = 0


                };

                return View(animeWithZeroRating);
            }

            var animeWithRating = new AnimeWithRating()
            {
                AnimeListId = animeList.AnimeListId,
                Name = animeList.Name,
                Description = animeList.Description,
                ScreenTime = animeList.ScreenTime,
                Image = animeList.Image,
                AnimeRating = rating.AnimeRating

                
            };
            //
            return View(animeWithRating);
            // return View(animeList);
        }

        // GET: AnimeLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnimeLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnimeListId,Name,Description,ScreenTime")] AnimeList animeList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animeList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animeList);
        }

        // GET: AnimeLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnimeList == null)
            {
                return NotFound();
            }

            var animeList = await _context.AnimeList.FindAsync(id);
            if (animeList == null)
            {
                return NotFound();
            }
            return View(animeList);
        }

        // POST: AnimeLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnimeListId,Name,Description,ScreenTime")] AnimeList animeList)
        {
            if (id != animeList.AnimeListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animeList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeListExists(animeList.AnimeListId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(animeList);
        }

        // GET: AnimeLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnimeList == null)
            {
                return NotFound();
            }

            var animeList = await _context.AnimeList
                .FirstOrDefaultAsync(m => m.AnimeListId == id);
            if (animeList == null)
            {
                return NotFound();
            }

            return View(animeList);
        }

        // POST: AnimeLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnimeList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AnimeList'  is null.");
            }
            var animeList = await _context.AnimeList.FindAsync(id);
            if (animeList != null)
            {
                _context.AnimeList.Remove(animeList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeListExists(int id)
        {
          return (_context.AnimeList?.Any(e => e.AnimeListId == id)).GetValueOrDefault();
        }
    }
}
