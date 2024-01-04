using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vox.Controllers;
using Vox.Models;
using Vox.Services.SportEventsService;

namespace Vox.UnitTest
{
    public class SportEventControllerTest
    {
        SportEventsController _controller;
        SportEventService _service;

        private readonly ILogger<SportEventService> _logger;
        private readonly ILogger<SportEventsController> _loggerController;

        private readonly DBContext _dbContext;

        public SportEventControllerTest()
        {
            _service = new SportEventService(_logger, _dbContext);
            _controller = new SportEventsController(_loggerController, _service);

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
