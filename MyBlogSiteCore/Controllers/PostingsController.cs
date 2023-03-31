using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBlogSiteCore.Data;
using MyBlogSiteCore.Models;
using MyBlogSiteCore.Services.Interfaces;

namespace MyBlogSiteCore.Controllers
{
    public class PostingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPostings _postings;

        public PostingsController(ApplicationDbContext context, IPostings postings)
        {
            _context = context;
            _postings = postings;
        }

        // GET: Postings
        public async Task<IActionResult> Index()
        {
              return _context.Postings != null ? 
                          View(await _context.Postings.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Postings'  is null.");
        }

        // GET: Postings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Postings == null)
            {
                return NotFound();
            }

            var posting = await _context.Postings
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posting == null)
            {
                return NotFound();
            }

            return View(posting);
        }

        // GET: Postings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Postings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Description,PostingDate,Content")] Posting posting)
        {
            if (ModelState.IsValid)
            {
                await _postings.AddAsync(posting);

                return RedirectToAction(nameof(Index));
            }
            return View(posting);
        }

        // GET: Postings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var model = await _postings.GetAsync(id);

            return View(model);
        }

        // POST: Postings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Description,PostingDate,Content")] Posting posting)
        {
            if (id != posting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //_context.Update(posting);
                    //await _context.SaveChangesAsync();
                    await _postings.EditAsync(posting);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostingExists(posting.Id))
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
            return View(posting);
        }

        // GET: Postings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Postings == null)
            {
                return NotFound();
            }

            //var posting = await _context.Postings
            //    .FirstOrDefaultAsync(m => m.Id == id);

            var posting = await _postings.GetAsync(id);
            if (posting == null)
            {
                return NotFound();
            }

            return View(posting);
        }

        // POST: Postings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Postings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Postings'  is null.");
            }
            var posting = await _context.Postings.FindAsync(id);
            if (posting != null)
            {
                _context.Postings.Remove(posting);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostingExists(int id)
        {
          return (_context.Postings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
