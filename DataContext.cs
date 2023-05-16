namespace WebWithAPI.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comic>().HasData(
                new Comic { Id = 1, Name = "Saqib" },
        new Comic { Id = 2, Name = "Bilal" }
                );
            modelBuilder.Entity<SuperHero>().HasData(
                 new SuperHero
                 {
                     id = 1,
                     FirstName = "Saqib",
                     LastName = "Bajwa",
                     HeroName = "SpiderMan",
                   
                     ComicId = 1,
                 },
        new SuperHero
        {
            id = 2,
            FirstName = "Bilal",
            LastName = "Muzzafar",
            HeroName = "BatMan",
           
            ComicId = 2,
        }
                );
        }


        public DbSet<Comic> Comics { get; set; }
        public DbSet<SuperHero> SuperHeroes { get; set; }

    }
}
