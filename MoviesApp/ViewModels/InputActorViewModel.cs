using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.ViewModels
{
    public class InputActorViewModel
    {
        [StringMinLength4]
        public string Name { get; set; }

        [StringMinLength4]
        public string Surname { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }
    }
}