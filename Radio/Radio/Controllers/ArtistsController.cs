using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Radio;
using System.Text.RegularExpressions;
using Radio.ViewModels;
using Radio.ViewModelss;
using Radio.Models;
using Microsoft.AspNetCore.Authorization;

namespace Radio.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly radiostationContext _context;

        public ArtistsController(radiostationContext context)
        {
            _context = context;
        }

        // GET: Artists

        [Authorize(Roles = "admin,user")]
        public IActionResult Index(string NameS, int? GroupId, string Name, string Description,int page = 1, SortState sortOrder = SortState.GroupIdAcs)
        {
            int pageSize = 10;

            IQueryable<Artists> source = _context.Artists.Include(a => a.Group);

            ViewData["NameSSort"] = sortOrder == SortState.NameSAcs ? SortState.NameSDesc : SortState.NameSAcs;
            ViewData["GroupIdSort"] = sortOrder == SortState.GroupIdAcs ? SortState.GroupIdDesc : SortState.GroupIdAcs;


            if (NameS != null)
            {
                source = source.Where(x => x.NameS == NameS);
            }
            if (GroupId != null)
            {
                source = source.Where(x => x.GroupId == GroupId);
            }


            switch (sortOrder)
            {
                case SortState.NameSAcs:
                    source = source.OrderBy(x => x.NameS);
                    break;
                case SortState.NameSDesc:
                    source = source.OrderByDescending(x => x.NameS);
                    break;
                case SortState.GroupIdAcs:
                    source = source.OrderBy(x => x.GroupId);
                    break;
                case SortState.GroupIdDesc:
                    source = source.OrderByDescending(x => x.GroupId);
                    break;
            }


            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);

            IndexViewModels ivm = new IndexViewModels
            {
                PageViewModel = pageView,
                FilterView = new FilterView(NameS,GroupId,Name,Description),
                Artists = items
            };
            return View(ivm);

        }


                [Authorize(Roles = "admin,user")]
        // GET: Artists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artists = await _context.Artists
                .Include(a => a.Group)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (artists == null)
            {
                return NotFound();
            }

            return View(artists);
        }
        [Authorize(Roles = "admin")]
        // GET: Artists/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id");
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: Artists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameS,GroupId")] Artists artists)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artists);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", artists.GroupId);
            return View(artists);
        }
        [Authorize(Roles = "admin")]
        // GET: Artists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artists = await _context.Artists.SingleOrDefaultAsync(m => m.Id == id);
            if (artists == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", artists.GroupId);
            return View(artists);
        }
        [Authorize(Roles = "admin")]
        // POST: Artists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameS,GroupId")] Artists artists)
        {
            if (id != artists.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artists);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtistsExists(artists.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", artists.GroupId);
            return View(artists);
        }
        [Authorize(Roles = "admin")]
        // GET: Artists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artists = await _context.Artists
                .Include(a => a.Group)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (artists == null)
            {
                return NotFound();
            }

            return View(artists);
        }
        [Authorize(Roles = "admin")]
        // POST: Artists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artists = await _context.Artists.SingleOrDefaultAsync(m => m.Id == id);
            _context.Artists.Remove(artists);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtistsExists(int id)
        {
            return _context.Artists.Any(e => e.Id == id);
        }
    }
}
