using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.Models;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly MoviesContext _context;
        private readonly IMapper _mapper;

        public ActorsController(MoviesContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActorViewModel>>> GetActors()
        {
            var actors = await _context.Actors.ToListAsync();
            return Ok(_mapper.Map<IEnumerable<Actor>, IEnumerable<ActorViewModel>>(actors));
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ActorViewModel>> GetActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);


            if (actor == null)
            {
                return NotFound();
            }

            return _mapper.Map<Actor, ActorViewModel>(actor);
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor(int id, EditActorViewModel viewModel)
        {
            var actor = _mapper.Map<EditActorViewModel, Actor>(viewModel);
            actor.Id = id;

            _context.Entry(actor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Actors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ActorViewModel>> PostActor(ActorViewModel viewModel)
        {
            var actor = _mapper.Map<ActorViewModel, Actor>(viewModel);

            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActor", new { id = actor.Id }, actor);
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ActorViewModel>> DeleteActor(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }

            var viewModel = _mapper.Map<Actor, ActorViewModel>(actor);

            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();

            return viewModel;
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
