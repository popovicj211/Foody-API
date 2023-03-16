using Bogus;
using Domain.Entities;

namespace EFDataAccess.Seeders
{
    public class DatabaseSeeder
    {
        private readonly DBContext _context;

        public DatabaseSeeder(DBContext context) => _context = context;


        public void AddRoles()
        {
            var namesOfRole = new[] { "Admin", "Customer"};

            if (!_context.Roles.Any())
            {
                var rolesFakeData = new Faker<RoleEntity>()
                    .RuleFor(u => u.Name, f => f.PickRandom(namesOfRole));
                   
                var roles = rolesFakeData.Generate(2);

                foreach (var role in roles)
                {
                    if (_context.Roles.Any(u => u.Name == role.Name))
                        continue;

                    _context.Roles.Add(role);
                }

                _context.SaveChanges();
            }
        }

        public void AddUsers()
        {
            if (!_context.Users.Any())
            {
                var testUsers = new Faker<UserEntity>()
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.Username, f => f.Person.UserName)
                    .RuleFor(u => u.Password, f => f.Internet.Password())
                    .RuleFor(u => u.Email, f => f.Person.Email)
                    .RuleFor(u => u.ImagePath, f => f.Internet.Avatar())
                    .RuleFor(u => u.RoleId, f => f.Random.Int(2));
           //     ImagePath

                var users = testUsers.Generate(10);

                foreach (var user in users)
                {
                    if (_context.Users.Any(u => u.Email == user.Email))
                        continue;

                    _context.Users.Add(user);
                }

                _context.SaveChanges();
            }
        }

    }
}
