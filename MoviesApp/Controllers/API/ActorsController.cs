using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.DTOs;
using MoviesApp.Models;
using MoviesApp.Services;
using MoviesApp.ViewModels;

namespace MoviesApp.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorService _actorService;

        public ActorsController(IActorService actorService)
        {
            _actorService = actorService;
        }

        // GET: api/Actors
        [HttpGet]
        public ActionResult<IEnumerable<ActorDto>> GetActors()
        {
            var actorDtos = _actorService.GetAll();
            return Ok(actorDtos);
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public ActionResult<ActorDto> GetActor(int id)
        {
            var actor = _actorService.GetById(id);


            if (actor == null)
            {
                return NotFound();
            }

            return actor;
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public IActionResult PutActor(int id, ActorDto dto)
        {
            dto.Id = id;
            _actorService.Update(dto);

            return NoContent();
        }

        // POST: api/Actors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public ActionResult<ActorDto> PostActor(ActorDto dto)
        {
            _actorService.Add(dto);

            return CreatedAtAction("GetActor", new { id = dto.Id }, dto);
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public ActionResult DeleteActor(int id)
        {
            _actorService.Delete(id);

            return NoContent();
        }
    }
}
