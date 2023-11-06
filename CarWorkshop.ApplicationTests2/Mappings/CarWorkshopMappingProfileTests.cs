using AutoMapper;
using CarWorkshop.Application.AplicationUser;
using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.Mappings;
using CarWorkshop.Domain.Entities;
using FluentAssertions;
using Moq;
using Xunit;

namespace CarWorkshop.Application.Test.Mappings
{

    public class CarWorkshopMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapCarWorkshopDtoToCarWorkshop()
        {
            //arrange
            var userContextMock = new Mock<IUserContext>();
            userContextMock
                .Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Moderator" }));

            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var carWorkshopDto = new CarWorkshopDto()
            {
                City = "City",
                PhoneNumber = "12345678",
                PostalCode = "12345",
                Street = "Street"
            };

            //act

            var result = mapper.Map<Domain.Entities.CarWorkshop>(carWorkshopDto);

            //asert

            result.Should().NotBeNull();

            result.ContactDetails.Should().NotBeNull();
            result.ContactDetails.City.Should().Be(carWorkshopDto.City);
            result.ContactDetails.PhoneNumber.Should().Be(carWorkshopDto.PhoneNumber);
            result.ContactDetails.PostalCode.Should().Be(carWorkshopDto.PostalCode);
            result.ContactDetails.Street.Should().Be(carWorkshopDto.Street);

        }

        [Fact()]
        public void MappingProfile_ShouldMapCarWorkshopToCarWorkshopDto()
        {
            //arrange
            var userContextMock = new Mock<IUserContext>();
            userContextMock
                .Setup(c => c.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var configuration = new MapperConfiguration(cfg =>
                cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1",
                ContactDetails = new CarWorkshopContacDetails()
                {
                    City = "City",
                    PhoneNumber = "12345678",
                    PostalCode = "12345",
                    Street = "Street"
                }

            };

            //act

            var result = mapper.Map<CarWorkshopDto>(carWorkshop);

            //asert

            result.Should().NotBeNull();

            result.IsEditable.Should().BeTrue();
            
            result.City.Should().Be(carWorkshop.ContactDetails.City);
            result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
            result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
            result.Street.Should().Be(carWorkshop.ContactDetails.Street);

        }
    }
}