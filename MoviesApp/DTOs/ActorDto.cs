using System;

namespace MoviesApp.DTOs
{
    public class ActorDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthdate { get; set; }
    }
}