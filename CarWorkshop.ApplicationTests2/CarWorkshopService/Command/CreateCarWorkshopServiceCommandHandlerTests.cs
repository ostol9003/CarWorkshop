using AutoMapper;
using CarWorkshop.Application.AplicationUser;
using CarWorkshop.Application.CarWorkshopService.Command;
using CarWorkshop.Domain.Interfaces;
using Moq;
using Xunit;

namespace CarWorkshop.Application.Test.CarWorkshopService.Command
{
   
    public class CreateCarWorkshopServiceCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_CreateCarWorkshopService_WhenUserIsAuthorized()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new List<string>() { "Admin", "User" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
                .ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
            var mapperMock = new Mock<IMapper>();

            var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopRepositoryMock.Object, mapperMock.Object, userContextMock.Object, carWorkshopServiceRepositoryMock.Object);

            //act

            await handler.Handle(command, CancellationToken.None);

            carWorkshopServiceRepositoryMock.Verify(m=> m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()),Times.Once);
        }


        [Fact()]
        public async Task Handle_CreateCarWorkshopService_WhenUserIsModerator()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new List<string>() { "Moderator", "User" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
                .ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
            var mapperMock = new Mock<IMapper>();

            var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopRepositoryMock.Object, mapperMock.Object, userContextMock.Object, carWorkshopServiceRepositoryMock.Object);

            //act

            await handler.Handle(command, CancellationToken.None);

            carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Once);
        }


        [Fact()]
        public async Task Handle_CreateCarWorkshopService_WhenUserIsUnauthorized()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("2", "test@test.com", new List<string>() {  "User" }));

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
                .ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
            var mapperMock = new Mock<IMapper>();

            var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopRepositoryMock.Object, mapperMock.Object, userContextMock.Object, carWorkshopServiceRepositoryMock.Object);

            //act

            await handler.Handle(command, CancellationToken.None);

            carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);
        }

        [Fact()]
        public async Task Handle_CreateCarWorkshopService_WhenUserIsUnauthenticated()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1"
            };

            var command = new CreateCarWorkshopServiceCommand()
            {
                Cost = "100 PLN",
                Description = "Service description",
                CarWorkshopEncodedName = "workshop1"
            };

            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(c => c.GetCurrentUser())
                .Returns((CurrentUser?) null);

            var carWorkshopRepositoryMock = new Mock<ICarWorkshopRepository>();
            carWorkshopRepositoryMock.Setup(c => c.GetByEncodedName(command.CarWorkshopEncodedName))
                .ReturnsAsync(carWorkshop);

            var carWorkshopServiceRepositoryMock = new Mock<ICarWorkshopServiceRepository>();
            var mapperMock = new Mock<IMapper>();

            var handler = new CreateCarWorkshopServiceCommandHandler(carWorkshopRepositoryMock.Object, mapperMock.Object, userContextMock.Object, carWorkshopServiceRepositoryMock.Object);

            //act

            await handler.Handle(command, CancellationToken.None);

            carWorkshopServiceRepositoryMock.Verify(m => m.Create(It.IsAny<Domain.Entities.CarWorkshopService>()), Times.Never);
        }
    }
}