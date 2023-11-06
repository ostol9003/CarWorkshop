﻿using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace CarWorkshop.MVCTests.Controllers
{


    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HomeControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task About_ReturnsViewWithRenderModel()
        {
            //arrange
            var client = _factory.CreateClient();

            //act

            var response = await client.GetAsync("/Home/About");

            //assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1>CarWorkshop application</h1>")
                .And.Contain("<div class=\"alert alert-primary\">Some description</div>")
                .And.Contain("<li>car</li>")
                .And.Contain("<li>app</li>")
                .And.Contain("<li>free</li>");
        }
    }
}