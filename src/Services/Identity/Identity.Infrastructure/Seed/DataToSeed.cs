using Identity.Domain.Models;
using Shared.Constants;

namespace Identity.Infrastructure.Seed
{
    public static class DataToSeed
    {

        public static (User user,string Password) GetAdminUserToAdd()
            =>
                (new User()
                {
                    SecondName = "Dron",
                    Name = "Max",
                    UserName = "Unox",
                    Age = 21,
                    Biography = "Used to laugh",
                    SexTypeId = Shared.Enums.SexTypes.Male,
                    CityId = new Guid("fd19ee26-af1c-4ed5-9737-d78995f66589"),
                }, PasswordConstants.AdminPassword);




        public static IEnumerable<Role> GetRolesToAdd()
            => new List<Role>
            {
                new Role()
                {
                    Name = RoleNamesConstants.AdminRoleName,
                    NormalizedName = RoleNamesConstants.AdminRoleName.ToUpper()
                    
                },
                new Role()
                {
                    Name= RoleNamesConstants.UserRoleName,
                    NormalizedName = RoleNamesConstants.UserRoleName.ToUpper()
                }
            };

        public static IEnumerable<Country> GetCountriesToAdd()
            =>
            new List<Country>()
            {
                new Country()
                {
                    Id = new Guid("0c0eb4c9-1d98-416e-951a-d9db6ad41cb8"),
                    Name = "Belarus"
                },
                new Country()
                {
                    Id = new Guid("1a9c8541-7dcd-491f-af3a-aa272c621ff0"),
                    Name = "Kazakhstan"
                }
            };


        public static IEnumerable<City> GetCitiesToAdd()
            =>
            new List<City>()
            {
                new City()
                {
                    CountryId = new Guid("0c0eb4c9-1d98-416e-951a-d9db6ad41cb8"),
                    Id =new Guid("fd19ee26-af1c-4ed5-9737-d78995f66589"),
                    Name = "Minsk",


                },
                new City()
                {
                    CountryId = new Guid("0c0eb4c9-1d98-416e-951a-d9db6ad41cb8"),
                    Id = new Guid("350dec6b-e0d8-4d3f-8efd-084a09797609"),
                    Name = "Gomel",
                },
                new City()
                {
                    CountryId = new Guid("1a9c8541-7dcd-491f-af3a-aa272c621ff0"),
                    Id = new Guid("f391445f-d9ce-4180-bf71-0de2841b9dbf"),
                    Name = "Almaty"
                },
                new City()
                {
                    CountryId = new Guid("1a9c8541-7dcd-491f-af3a-aa272c621ff0"),
                    Id = new Guid("6606d45d-7513-49e4-a898-3214226e1aa9"),
                    Name = "Astana"
                }
            };
    }
}
