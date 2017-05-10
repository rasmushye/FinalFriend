using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HelpfulFriend.Data;
using HelpfulFriend.Models;

namespace HelpfulFriend.Controllers
{
    public class JobOffersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobOffersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: JobOffers
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        
        {
            ViewData["CurrentSort"] = sortOrder;
            var joboffers = from m in _context.JobOffers
                         select m;

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            
            if (!String.IsNullOrEmpty(searchString))
            {
                joboffers = joboffers.Where(s => s.JobTitle.Contains(searchString));
            }

            int pageSize = 5;
            return View(await PaginatedList<JobOffers>.CreateAsync(joboffers.AsNoTracking(), page ?? 1, pageSize));
            //return View(await joboffers.ToListAsync());
        }


        // GET: JobOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffers = await _context.JobOffers
                .SingleOrDefaultAsync(m => m.ID == id);
            if (jobOffers == null)
            {
                return NotFound();
            }

            return View(jobOffers);
        }

        // GET: JobOffers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobOffers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JobTitle,Location,JobDate,Price,EstTime,Category")] JobOffers jobOffers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobOffers);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(jobOffers);
        }

        // GET: JobOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffers = await _context.JobOffers.SingleOrDefaultAsync(m => m.ID == id);
            if (jobOffers == null)
            {
                return NotFound();
            }
            return View(jobOffers);
        }



        // POST: JobOffers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JobTitle,Location,JobDate,Price,EstTime,Category")] JobOffers jobOffers)
        {
            if (id != jobOffers.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobOffers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobOffersExists(jobOffers.ID))
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
            return View(jobOffers);
        }

        // GET: JobOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobOffers = await _context.JobOffers
                .SingleOrDefaultAsync(m => m.ID == id);
            if (jobOffers == null)
            {
                return NotFound();
            }

            return View(jobOffers);
        }

        // POST: JobOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobOffers = await _context.JobOffers.SingleOrDefaultAsync(m => m.ID == id);
            _context.JobOffers.Remove(jobOffers);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool JobOffersExists(int id)
        {
            return _context.JobOffers.Any(e => e.ID == id);
        }
    }
}
