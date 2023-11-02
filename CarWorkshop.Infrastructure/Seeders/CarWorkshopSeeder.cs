using CarWorkshop.Infrastructure.Persistance;

namespace CarWorkshop.Infrastructure.Seeders
{
    public class CarWorkshopSeeder
    {
        private readonly CarWorkshopDbContext _dbContext;

        public CarWorkshopSeeder(CarWorkshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if(await _dbContext.Database.CanConnectAsync()) 
            {
                if(!_dbContext.CarWorkshops.Any()) 
                {
                    var mazdaAso = new Domain.Entities.CarWorkshop()
                    {
                        Name="Mazda ASO",
                        Description = "Autoryzowany serwis Mazda",
                        About = "Autoryzowany serwis Mazda",
                        ContactDetails = new()
                        {
                            City = "Szczecin",
                            Street = "Wojska Polskiego 2",
                            PostalCode = "70-777",
                            PhoneNumber = "+48413234"
                        }
                    };
                    mazdaAso.EncodeName();

                    _dbContext.CarWorkshops.Add(mazdaAso);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

    }
}
