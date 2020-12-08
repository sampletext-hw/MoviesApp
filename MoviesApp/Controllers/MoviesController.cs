using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MoviesContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;


        public MoviesController(MoviesContext context, ILogger<HomeController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: Movies
        [HttpGet]
        public IActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(_context.Movies.ToList()));
        }

        // GET: Movies
        [HttpGet]
        public IActionResult ByActor(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<Movie> query = _context.Movies.Include(m => m.Actors).Where(m => m.Actors.Any(a => a.ActorId == id));

            var movieViewModels = _mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(query.ToList());

            return View(nameof(Index), movieViewModels);
        }

        // GET: Movies/Details/5
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<Movie, MovieViewModel>(_context.Movies.FirstOrDefault(m => m.Id == id));
            
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Movies/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,ReleaseDate,Genre,Price")]
            InputMovieViewModel inputModel)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<InputMovieViewModel,Movie>(inputModel);
                _context.Add(movie);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(inputModel);
        }

        [HttpGet]
        // GET: Movies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editModel = _mapper.Map<Movie, EditMovieViewModel > (_context.Movies.FirstOrDefault(m => m.Id == id));
            
            if (editModel == null)
            {
                return NotFound();
            }

            return View(editModel);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Title,ReleaseDate,Genre,Price")]
            EditMovieViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var movie = _mapper.Map<EditMovieViewModel, Movie>(editModel);
                    movie.Id = id;

                    _context.Update(movie);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (!MovieExists(id))
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

        [HttpGet]
        // GET: Movies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteModel = _mapper.Map<Movie, DeleteMovieViewModel>(_context.Movies.FirstOrDefault(m => m.Id == id));
            
            if (deleteModel == null)
            {
                return NotFound();
            }

            return View(deleteModel);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = _context.Movies.Find(id);
            _context.Movies.Remove(movie);
            _context.SaveChanges();
            _logger.LogError($"Movie with id {movie.Id} has been deleted!");
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}