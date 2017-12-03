using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Radio;
using Radio.ViewModels;
using Radio.ViewModelss;
using Radio.Models;
using Microsoft.AspNetCore.Authorization;

namespace Radio.Controllers
{
    public class GanrsController : Controller
    {
        private readonly radiostationContext _context;

        public GanrsController(radiostationContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin,user")]
        public IActionResult Index(string NameS, int? GroupId, string Name, string Description, int page = 1, SortState sortOrder = SortState.NameAcs)
        {
            int pageSize = 10;

            IQueryable<Ganrs> source = _context.Ganrs;

            ViewData["NameSort"] = sortOrder == SortState.NameAcs ? SortState.NameDesc : SortState.NameAcs;
            ViewData["DescriptionSort"] = sortOrder == SortState.DescriptionAcs ? SortState.DescriptionDesc : SortState.DescriptionAcs;


            if (Name != null)
            {
                source = source.Where(x => x.Name == Name);
            }
            if (Description != null)
            {
                source = source.Where(x => x.Description == Description);
            }


            switch (sortOrder)
            {
                case SortState.NameAcs:
                    source = source.OrderBy(x => x.Name);
                    break;
                case SortState.NameDesc:
                    source = source.OrderByDescending(x => x.Name);
                    break;
                case SortState.DescriptionAcs:
                    source = source.OrderBy(x => x.Description);
                    break;
                case SortState.DescriptionDesc:
                    source = source.OrderByDescending(x => x.Description);
                    break;
            }


            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);

            IndexViewModels ivm = new IndexViewModels
            {
                PageViewModel = pageView,
                FilterView = new FilterView(NameS, GroupId, Name, Description),
                Ganrs = items
            };
            return View(ivm);

        }
        [Authorize(Roles = "admin,user")]
        // GET: Ganrs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ganrs = await _context.Ganrs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ganrs == null)
            {
                return NotFound();
            }

            return View(ganrs);
        }

        // GET: Ganrs/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: Ganrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Ganrs ganrs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ganrs);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ganrs);
        }
        [Authorize(Roles = "admin")]
        // GET: Ganrs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ganrs = await _context.Ganrs.SingleOrDefaultAsync(m => m.Id == id);
            if (ganrs == null)
            {
                return NotFound();
            }
            return View(ganrs);
        }
        [Authorize(Roles = "admin")]
        // POST: Ganrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Ganrs ganrs)
        {
            if (id != ganrs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ganrs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GanrsExists(ganrs.Id))
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
            return View(ganrs);
        }
        [Authorize(Roles = "admin")]
        // GET: Ganrs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ganrs = await _context.Ganrs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ganrs == null)
            {
                return NotFound();
            }

            return View(ganrs);
        }
        [Authorize(Roles = "admin")]
        // POST: Ganrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ganrs = await _context.Ganrs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Ganrs.Remove(ganrs);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GanrsExists(int id)
        {
            return _context.Ganrs.Any(e => e.Id == id);
        }
    }
}
