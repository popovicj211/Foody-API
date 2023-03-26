using Domain.Entities;
using EFDataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace EFDataAccess
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
        public DbSet<DishEntity> Dishes { get; set; }
        public DbSet<DishCommentEntity> DishComments { get; set; }
        public DbSet<IngredientEntity> Ingredients { get; set; }
        public DbSet<DishIngredientEntity> DishIngredients { get; set; }
        public DbSet<DishTypeEntity> DishTypes { get; set; }
        public DbSet<DishTypeDishEntity> DishTypeDishes { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }
      
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-BP2V8EI\SQLJPMR5502;Initial Catalog=FoodyAPI_DB;Persist Security Info=True;TrustServerCertificate=True;User ID=sa;Password=Pa92kB79hL");
        //}
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ContactEntityConfiguration());
            modelBuilder.ApplyConfiguration(new CommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DishEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DishCommentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new IngredientEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DishIngredientEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DishTypeEntityConfiguration());
            modelBuilder.ApplyConfiguration(new DishTypeDishEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
        }
    }
}
