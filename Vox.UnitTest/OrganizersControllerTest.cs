using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Vox.Controllers;
using Vox.Data.UserData;
using Vox.Models;
using Vox.Services.OrganizersService;

namespace Vox.UnitTest
{
    public class OrganizersControllerTest
    {
        OrganizersController _controller;
        OrganizersService _service;

        private readonly ILogger<OrganizersService> _logger;
        private readonly ILogger<OrganizersController> _loggerController;

        private readonly DBContext _dbContext;

        public OrganizersControllerTest()
        {
            _service = new OrganizersService(_logger, _dbContext);
            _controller = new OrganizersController(_loggerController, _service);

        }

        [Fact]
        public async void ListTest()
        {
            //Assert
            Assert.Null(null);
        }

        [Fact]
        public async void CreateTest()
        {
            //Assert
            Assert.Null(null);
        }

        [Fact]
        public async void UpdateTest()
        {
            //Assert
            Assert.Null(null);
        }

        [Fact]
        public async void DeleteTest()
        {
            //Assert
            Assert.Null(null);
        }

        [Fact]
        public async void DetailTest()
        {
            //Assert
            Assert.Null(null);
        }

    }
}
