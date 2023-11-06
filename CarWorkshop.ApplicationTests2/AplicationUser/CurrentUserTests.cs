using CarWorkshop.Application.AplicationUser;
using FluentAssertions;
using Xunit;

namespace CarWorkshop.Application.Test.AplicationUser
{
    public class CurrentUserTests
    {


        [Fact()]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", new List<string> { "Admin", "User" });

            //act

            var isInRole = currentUser.IsInRole("Admin");

            //asert

            isInRole.Should().BeTrue();

        }

        [Fact()]
        public void IsInRole_WithNonMatchingRole_ShouldReturnFalse()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", new List<string>{ "Admin", "User" });

            //act

            var isInRole = currentUser.IsInRole("Administrator");

            //asert

            isInRole.Should().BeFalse();

        }
        [Fact()]
        public void IsInRole_WithNonMatchingCaseRole_ShouldReturnFalse()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", new List<string> { "Admin", "User" });

            //act

            var isInRole = currentUser.IsInRole("admin");

            //asert

            isInRole.Should().BeFalse();

        }
    }
}