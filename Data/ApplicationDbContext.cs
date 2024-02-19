using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Bookify.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the IdentityUserLogin entity
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<Category>()
                .Property(m => m.LastUpdatedOn)
                .IsRequired(false);
            modelBuilder.Entity<BookCategory>().HasKey(e => new { e.CategoryId, e.BookId });

            modelBuilder.Entity<IdentityUser>().ToTable("Users", "security");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "security");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("User Roless", "security");

            modelBuilder.Entity<Nationality>().HasData(
                new Nationality { Id = 1, Name = "Afghan" },
                new Nationality { Id = 2, Name = "Albanian" },
                new Nationality { Id = 3, Name = "Algerian" },
                new Nationality { Id = 4, Name = "American" },
                new Nationality { Id = 5, Name = "Andorran" },
                new Nationality { Id = 6, Name = "Angolan" },
                new Nationality { Id = 7, Name = "Antiguans" },
                new Nationality { Id = 8, Name = "Argentinean" },
                new Nationality { Id = 9, Name = "Armenian" },
                new Nationality { Id = 10, Name = "Australian" },
                new Nationality { Id = 11, Name = "Austrian" },
                new Nationality { Id = 12, Name = "Azerbaijani" },
                new Nationality { Id = 13, Name = "Bahamian" },
                new Nationality { Id = 14, Name = "Bahraini" },
                new Nationality { Id = 15, Name = "Bangladeshi" },
                new Nationality { Id = 16, Name = "Barbadian" },
                new Nationality { Id = 17, Name = "Barbudans" },
                new Nationality { Id = 18, Name = "Batswana" },
                new Nationality { Id = 19, Name = "Belarusian" },
                new Nationality { Id = 20, Name = "Belgian" },
                new Nationality { Id = 21, Name = "Belizean" },
                new Nationality { Id = 22, Name = "Beninese" },
                new Nationality { Id = 23, Name = "Bhutanese" },
                new Nationality { Id = 24, Name = "Bolivian" },
                new Nationality { Id = 25, Name = "Bosnian" },
                new Nationality { Id = 26, Name = "Brazilian" },
                new Nationality { Id = 27, Name = "British" },
                new Nationality { Id = 28, Name = "Bruneian" },
                new Nationality { Id = 29, Name = "Bulgarian" },
                new Nationality { Id = 30, Name = "Burkinabe" },
                new Nationality { Id = 31, Name = "Burmese" },
                new Nationality { Id = 32, Name = "Burundian" },
                new Nationality { Id = 33, Name = "Cambodian" },
                new Nationality { Id = 34, Name = "Cameroonian" },
                new Nationality { Id = 35, Name = "Canadian" },
                new Nationality { Id = 36, Name = "Cape Verdean" },
                new Nationality { Id = 37, Name = "Central African" },
                new Nationality { Id = 38, Name = "Chadian" },
                new Nationality { Id = 39, Name = "Chilean" },
                new Nationality { Id = 40, Name = "Chinese" },
                new Nationality { Id = 41, Name = "Colombian" },
                new Nationality { Id = 42, Name = "Comoran" },
                new Nationality { Id = 43, Name = "Congolese" },
                new Nationality { Id = 44, Name = "Costa Rican" },
                new Nationality { Id = 45, Name = "Croatian" },
                new Nationality { Id = 46, Name = "Cuban" },
                new Nationality { Id = 47, Name = "Cypriot" },
                new Nationality { Id = 48, Name = "Czech" },
                new Nationality { Id = 49, Name = "Danish" },
                new Nationality { Id = 50, Name = "Djibouti" },
                new Nationality { Id = 51, Name = "Dominican" },
                new Nationality { Id = 52, Name = "Dutch" },
                new Nationality { Id = 53, Name = "East Timorese" },
                new Nationality { Id = 54, Name = "Ecuadorean" },
                new Nationality { Id = 55, Name = "Egyptian" }
                );
        }
        public DbSet<Author> authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }

    }
}