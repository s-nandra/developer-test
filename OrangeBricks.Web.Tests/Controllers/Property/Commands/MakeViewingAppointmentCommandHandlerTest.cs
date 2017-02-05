using System;
using NSubstitute;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using OrangeBricks.Web.Tests.Controllers.Property.Builders;
using NUnit.Framework;
using OrangeBricks.Web.Controllers.Property.ViewModels;



namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class MakeViewingAppointmentCommandHandlerTest
    {

        private MakeViewingAppointmentCommandHandler _handler;
        private IOrangeBricksContext _context;

        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();

            //Mock data can be refactored into a seprate file - as it's shared in places
            var properties = new List<Models.Property>{
                new Models.Property{ Id = 1, StreetName = "Smith Street", Description = "", IsListedForSale = true}
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
               .Initialize(properties.AsQueryable());

            _context.Properties.Returns(mockSet);

            _context.Offers.Returns(Substitute.For<IDbSet<Offer>>());
            _handler = new MakeViewingAppointmentCommandHandler(_context);


        }

        [Test]
        public void HandlerShouldHaveViewingAppountmentDateTime()
        {
            // Arrange    
            var dateTime = Convert.ToDateTime("04/02/2017 14:50:50.42");
   
            var command = new MakeViewingAppointmentCommand()
            {
                PropertyId = 1,
                ViewingAppointmentDateTime = dateTime,
                BuyerId = "abc123"
            };

            // Act
            _handler.Handle(command);

            // Assert
            var va = _context.Properties.First().ViewingAppointment.First();
            Assert.AreEqual("04/02/2017", va.ViewingAppointmentDateTime.Date.ToString("dd/MM/yyyy"));

            Assert.AreEqual("14:50", va.ViewingAppointmentDateTime.TimeOfDay.ToString("hh\\:mm"));



        }

    }
}
