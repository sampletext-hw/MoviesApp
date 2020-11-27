using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers
{
    public class ActorsController : Controller
    {
        private readonly MoviesContext _context;
        private readonly ILogger<ActorsController> _logger;

        public ActorsController(MoviesContext context, ILogger<ActorsController> logger)
        {
            _context = context;

            _logger = logger;
        }

        // GET: Actors
        public IActionResult Index()
        {
            return View(_context.Actors.Select(a => new ActorViewModel
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname,
                Birthdate = a.Birthdate
            }).ToList());
        }

        // GET: Actors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _context.Actors.Where(a => a.Id == id).Select(a => new ActorViewModel()
            {
                Id = a.Id,
                Name = a.Name,
                Surname = a.Surname,
                Birthdate = a.Birthdate
            }).FirstOrDefault();


            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Surname,Birthdate")] InputActorViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(new Actor
                {
                    Name = inputModel.Name,
                    Surname = inputModel.Surname,
                    Birthdate = inputModel.Birthdate
                });
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }

        // GET: Actors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _context.Actors.Where(a => a.Id == id).Select(a => new EditActorViewModel()
            {
                Name = a.Name,
                Surname = a.Surname,
                Birthdate = a.Birthdate
            }).FirstOrDefault();

            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Surname,Birthdate")] EditActorViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var actor = new Actor
                    {
                        Id = id,
                        Name = editModel.Name,
                        Surname = editModel.Surname,
                        Birthdate = editModel.Birthdate
                    };

                    _context.Update(actor);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(id))
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

            return View(editModel);
        }

        // GET: Actors/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _context.Actors.Where(a => a.Id == id).Select(a => new DeleteActorViewModel
            {
                Name = a.Name,
                Surname = a.Surname,
                Birthdate = a.Birthdate
            }).FirstOrDefault();

            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var actor = _context.Actors.Find(id);
            _context.Actors.Remove(actor);
            _context.SaveChanges();
            _logger.LogError($"Actor with id {actor.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}