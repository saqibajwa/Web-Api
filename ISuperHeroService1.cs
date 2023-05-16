namespace WebWithAPI.Client.Services.SuperHeroService
{
    public interface ISuperHeroService1
    {
        List<Comic> Comics { get; set; }
        List<SuperHero> Heroes { get; set; }

        Task CreateHero(SuperHero hero);
        Task DeleteHero(int id);
        Task GetComics();
        Task<SuperHero> GetSingleHero(int id);
        Task GetSuperHeros();
        Task UpdateHero(SuperHero hero);
    }
}