using FluentAssertions;
using Xunit;

namespace CarWorkshop.Domain.Test.Entities
{

    public class CarWorkshopTests
    {
        [Fact()]
        public void EncodedName_ShouldSetEncodedName()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop();
            carWorkshop.Name = "Test Workshop";
            //act
            carWorkshop.EncodeName();
            // assert
            carWorkshop.EncodedName.Should().Be("test_workshop");
        }
        [Fact]
        public void EncodedName_ShouldThrowException_WhenNameIsNull()
        {
            //arrange
            var carWorkshop = new Domain.Entities.CarWorkshop();
            
            //act
            Action action = () => carWorkshop.EncodeName();

            //arange

            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();
        }
    }
}


