using CarWorkshop.Domain.Entities;

namespace CarWorkshop.Domain.Interfaces;

public interface ICarWorkshopServiceRepository
{
    public Task Create(CarWorkshopService carWorkshopService);
    public Task<IEnumerable<CarWorkshopService>> GetAllByEncodedName(string requestEncodedName);
}