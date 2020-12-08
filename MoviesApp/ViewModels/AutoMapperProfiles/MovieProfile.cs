﻿using AutoMapper;
using MoviesApp.Models;

namespace MoviesApp.ViewModels.AutoMapperProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, InputMovieViewModel>().ReverseMap();
            CreateMap<Movie, DeleteMovieViewModel>();
            CreateMap<Movie, EditMovieViewModel>().ReverseMap();
            CreateMap<Movie, MovieViewModel>();
        }
    }
}