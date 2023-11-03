using CarWorkshop.Domain.Entities;
using CarWorkshop.Domain.Interfaces;
using CarWorkshop.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CarWorkshop.Infrastructure.Repositories;

public class CarWorkshopServiceRepository : ICarWorkshopServiceRepository
{
    private readonly CarWorkshopDbContext _carWorkshopDbContext;

    public CarWorkshopServiceRepository(CarWorkshopDbContext carWorkshopDbContext)
    {
        _carWorkshopDbContext = carWorkshopDbContext;
    }

    public async Task Create(CarWorkshopService carWorkshopService)
    {
        _carWorkshopDbContext.Services.Add(carWorkshopService);
        await _carWorkshopDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string encodedName) => await _carWorkshopDbContext
        .Services
        .Where(s => s.CarWorkshop.EncodedName == encodedName)
        .ToListAsync();
}