using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MoviesApp.ViewModels;

namespace MoviesApp.Filters
{
    public class StringMinLength4 : ValidationAttribute
    {
        public StringMinLength4()
        {
        }

        public string GetErrorMessage() =>
            $"Actor must have at least 4 chars name and surname";


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var str = (string)value;
            if (str.Length < 4)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
    }
}