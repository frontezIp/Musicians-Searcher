using Musicians.Domain.Models;
using Musicians.Infrastructure.Models;

namespace Musicians.Infrastructure.Seed
{
    public static class DataToSeed
    {
        public static IEnumerable<Genre> GetGenres() 
            => new List<Genre> 
            { 
                new Genre()
                {
                    Id = new Guid("a696c640-19ac-4985-97e7-f1bac0a1ce43"),
                    Name = "Rock"
                },
                
                new Genre()
                {
                    Id= new Guid("6ff5f902-28bd-4195-be30-a5e468bec1fa"),
                    Name = "Rap"
                }
            };

        public static IEnumerable<Skill> GetSkills()
            => new List<Skill>
            {
                new Skill()
                {
                    Id = new Guid("3c3fa874-1de7-488f-b69d-449f5b57c2f3"),
                    Name = "Songwriter"
                },

                new Skill()
                {
                    Id = new Guid("fb24781e-b6ee-41c7-8e31-3c4f217e1379"),
                    Name = "Singer"
                }
            };

        public static IEnumerable<Musician> GetMusicians()
            => new List<Musician>
            {
                new Musician()
                {
                    Age = 18,
                    FavouriteGenres =new List<Genre>(){GetGenres().ToList()[0]},
                    Biography = "Good third",
                    CreatedAt = DateTime.Now,
                    FullName = "Hello World",
                    Goal = "Trying to find some good singers",
                    Id = new Guid("61482e7a-0d75-45a4-8bf9-09ca281ec83f"),
                    Location = "Kazakhstan, Astana",
                    Sex = Shared.Enums.SexTypes.Female,
                    Skills =  new List<Skill>(){GetSkills().ToList()[1]},
                    Username = "Unox103"
                },
                new Musician()
                {
                    Age = 20,
                    FavouriteGenres = new List<Genre>{GetGenres().ToList()[0] },
                    Biography = "Good one",
                    CreatedAt = DateTime.Now,
                    FriendsCount = 1,
                    Friends = new List<Guid>{new Guid("504892f8-e78c-417a-98c8-5c5e49c813c7") },
                    FullName = "Hello World",
                    Goal = "Trying to find some good musicians",
                    Id = new Guid("da6c2ddc-4265-4580-a0e9-e696f2df573c"),
                    Location = "Belarus, Gomel",
                    Sex = Shared.Enums.SexTypes.Male,
                    Skills = GetSkills().ToList(),
                    Username = "Unox404"
                },
                new Musician()
                {
                     Age = 20,
                    FavouriteGenres = GetGenres().ToList(),
                    Biography = "Good second",
                    CreatedAt = DateTime.Now,
                    SubscribersCount = 1,
                    Subscribers = new List<Guid>{new Guid("da6c2ddc-4265-4580-a0e9-e696f2df573c")},
                    FullName = "Hello World",
                    Goal = "Trying to find some good artists",
                    Id = new Guid("fd6ebca2-d3a2-4779-8022-6010d648e367"),
                    Location = "Belarus, Gomel",
                    Sex = Shared.Enums.SexTypes.Male,
                    Skills = new List<Skill>(){GetSkills().ToList()[0]},
                    Username = "Maxim123"
                },

                new Musician()
                {
                    Age = 22,
                    FavouriteGenres =new List<Genre>(){GetGenres().ToList()[1]},
                    Biography = "Good third",
                    CreatedAt = DateTime.Now,
                    FriendsCount = 1,
                    Friends = new List<Guid>{new Guid("da6c2ddc-4265-4580-a0e9-e696f2df573c")},
                    FullName = "Hello World",
                    Goal = "Trying to find some good singers",
                    Id = new Guid("504892f8-e78c-417a-98c8-5c5e49c813c7"),
                    Location = "Belarus, Minsk",
                    Sex = Shared.Enums.SexTypes.Female,
                    Skills =  new List<Skill>(){GetSkills().ToList()[1]},
                    Username = "Unox404"
                },                               
            };
    }
}
