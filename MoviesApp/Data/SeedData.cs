using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Models;

namespace MoviesApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MoviesContext>>()))
            {
                // Look for any movies.
                if (context.Movies.Any()) return; // DB has been seeded

                var actors = new[]
                {
                    new Actor
                    {
                        Name = "Antonio",
                        Surname = "Banderos",
                        Birthdate = DateTime.Now.Date
                    },
                    new Actor
                    {
                        Name = "Alan",
                        Surname = "Rickman",
                        Birthdate = DateTime.Now.Date
                    },
                    new Actor
                    {
                        Name = "Benedict",
                        Surname = "Cumberbatch",
                        Birthdate = DateTime.Now.Date
                    },
                    new Actor
                    {
                        Name = "Daniel",
                        Surname = "Radcliffe",
                        Birthdate = DateTime.Now.Date
                    },
                    new Actor
                    {
                        Name = "Leonardo",
                        Surname = "DiCaprio",
                        Birthdate = DateTime.Now.Date
                    },
                    new Actor
                    {
                        Name = "Ryan",
                        Surname = "Gosling",
                        Birthdate = DateTime.Now.Date
                    },
                };

                context.Actors.AddRange(actors);

                var movies = new[]
                {
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },
                    new Movie
                    {
                        Title = "Rio Bravo",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Western",
                        Price = 3.99M
                    }
                };

                context.Movies.AddRange(movies);

                context.MovieActors.AddRange(
                    new MovieActor
                    {
                        Actor = actors[0], Movie = movies[0]
                    }, 
                    new MovieActor
                    {
                        Actor = actors[1], Movie = movies[0]
                    }, 
                    new MovieActor
                    {
                        Actor = actors[3], Movie = movies[0]
                    }, 
                    new MovieActor
                    {
                        Actor = actors[5], Movie = movies[0]
                    },
                    new MovieActor
                    {
                        Actor = actors[0], Movie = movies[1]
                    },
                    new MovieActor
                    {
                        Actor = actors[1], Movie = movies[1]
                    },
                    new MovieActor
                    {
                        Actor = actors[1], Movie = movies[2]
                    },
                    new MovieActor
                    {
                        Actor = actors[2], Movie = movies[2]
                    },
                    new MovieActor
                    {
                        Actor = actors[3], Movie = movies[2]
                    },
                    new MovieActor
                    {
                        Actor = actors[1], Movie = movies[3]
                    },
                    new MovieActor
                    {
                        Actor = actors[3], Movie = movies[3]
                    },
                    new MovieActor
                    {
                        Actor = actors[4], Movie = movies[3]
                    },
                    new MovieActor
                    {
                        Actor = actors[5], Movie = movies[3]
                    }
                );

                context.SaveChanges();
            }
        }
    }
}