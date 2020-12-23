using System.Collections.Generic;
using MoviesApp.DTOs;

namespace MoviesApp.Services
{
    public interface IActorService
    {
        IEnumerable<ActorDto> GetAll();
        ActorDto GetById(int id);
        void Update(ActorDto actorDto);
        ActorDto Add(ActorDto actorDto);
        void Delete(int id);
    }
}