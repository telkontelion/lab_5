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
    public class GroupsController : Controller
    {
        private readonly radiostationContext _context;

        public GroupsController(radiostationContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "admin,user")]
        public IActionResult Index(string NameS, int? GroupId, string Name, string Description, int page = 1, SortState sortOrder = SortState.DescriptionAcs)
        {
            int pageSize = 5;

            IQueryable<Groups> source = _context.Groups;

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
                Groups = items
            };
            return View(ivm);

        }
        [Authorize(Roles = "admin,user")]
        // GET: Groups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .SingleOrDefaultAsync(m => m.Id == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        // GET: Groups/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groups);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groups);
        }
        [Authorize(Roles = "admin")]
        // GET: Groups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);
            if (groups == null)
            {
                return NotFound();
            }
            return View(groups);
        }
        [Authorize(Roles = "admin")]
        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Groups groups)
        {
            if (id != groups.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groups);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupsExists(groups.Id))
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
            return View(groups);
        }
        [Authorize(Roles = "admin")]
        // GET: Groups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .SingleOrDefaultAsync(m => m.Id == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }
        [Authorize(Roles = "admin")]
        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groups = await _context.Groups.SingleOrDefaultAsync(m => m.Id == id);
            _context.Groups.Remove(groups);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupsExists(int id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
