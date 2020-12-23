using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MoviesApp.Data;
using MoviesApp.DTOs;
using MoviesApp.Models;

namespace MoviesApp.Services
{
    public class ActorService : IActorService
    {
        private readonly IMapper _mapper;
        private readonly MoviesContext _context;

        public ActorService(IMapper mapper, MoviesContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public IEnumerable<ActorDto> GetAll()
        {
            return _mapper.Map<IEnumerable<ActorDto>>(_context.Actors.ToList());
        }

        public ActorDto GetById(int id)
        {
            return _mapper.Map<Actor, ActorDto>(_context.Actors.Find(id));
        }

        public void Update(ActorDto actorDto)
        {
            var actor = _context.Actors.Find(actorDto.Id);

            try
            {
                _context.Actors.Update(_mapper.Map<Actor>(actorDto));
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
        }

        public ActorDto Add(ActorDto actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);
            _context.Actors.Add(actor);
            _context.SaveChanges();
            actorDto.Id = actor.Id;
            return actorDto;
        }

        public void Delete(int id)
        {
            var actor = _context.Actors.Find(id);
            
            if (actor != null)
            {
                _context.Actors.Remove(actor);
                _context.SaveChanges();
            }
        }
    }
}