﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventoTec.Web.Models;
using EventoTec.Web.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace EventoTec.Web.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly DataDbContext _context;

        public EventsController(DataDbContext context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var dataDbContext = _context.events.Include(a => a.Category).Include(a => a.City).Include(a => a.Client);
            return View(await dataDbContext.ToListAsync());
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.events
                .Include(a => a.Category)
                .Include(a => a.City)
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            var username = User.Identity.Name;
            var userid = _context.Clients.Where(a => a.User.Email == username).FirstOrDefault();
            ViewBag.ClientId = userid.Id;
            ViewData["CityId"] = new SelectList(_context.cities,"Id","Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories,"Id","Name");
            return View();
        }

        public IActionResult CreateEvent()
        {
            ViewBag.ClientId = _context.Clients.Include(u => u.User).ToList();
            ViewData["CityId"] = new SelectList(_context.cities, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEvent(Event @event)
        {
            _context.Add(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", @event.CategoryId);
            ViewData["CityId"] = new SelectList(_context.cities, "Id", "Name", @event.CityId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", @event.ClientId);
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", @event.CategoryId);
            ViewData["CityId"] = new SelectList(_context.cities, "Id", "Name", @event.CityId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", @event.ClientId);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,EvenDate,Description,Picture,People,Duration,CityId,ClientId,CategoryId")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Description", @event.CategoryId);
            ViewData["CityId"] = new SelectList(_context.cities, "Id", "Name", @event.CityId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "Id", "Id", @event.ClientId);
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.events
                .Include(a => a.Category)
                .Include(a => a.City)
                .Include(a => a.Client)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.events.FindAsync(id);
            _context.events.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.events.Any(e => e.Id == id);
        }
    }
}
