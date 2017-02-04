using NUnit.Framework;
using NSubstitute;
using OrangeBricks.Web.Controllers.Property.Commands;
using OrangeBricks.Web.Models;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using OrangeBricks.Web.Tests.Controllers.Property.Builders;

namespace OrangeBricks.Web.Tests.Controllers.Property.Commands
{
    [TestFixture]
    public class MakeOfferCommandHandlerTest
    {
        private MakeOfferCommandHandler _handler;
        private IOrangeBricksContext _context;


        [SetUp]
        public void SetUp()
        {
            _context = Substitute.For<IOrangeBricksContext>();

            //Mock data can be refactored into a seprate file - as it's shared in places
            var properties = new List<Models.Property>{
                new Models.Property{ Id = 1, StreetName = "Smith Street", Description = "", IsListedForSale = true },
                new Models.Property{ Id = 2, StreetName = "Jones Street", Description = "", IsListedForSale = true}
            };

            var mockSet = Substitute.For<IDbSet<Models.Property>>()
                .Initialize(properties.AsQueryable());
            _context.Properties.Returns(mockSet);

            _context.Offers.Returns(Substitute.For<IDbSet<Offer>>());
            _handler = new MakeOfferCommandHandler(_context);
        }

        [Test]
        public void HandlerShouldHaveOffer()
        {
            // Arrange           
            var command = new MakeOfferCommand
            {
                PropertyId = 1,
                Offer = 123456
            };

            // Act
            _handler.Handle(command);

            // Assert
            var offer = _context.Properties.First().Offers.First();
            Assert.AreEqual(123456, offer.Amount);

        }

        [Test]
        public void HandlerShouldAddOfferWithBuyerId()
        {
            // Arrange           
            var command = new MakeOfferCommand
            {
                PropertyId = 1,
                Offer = 123456,
                BuyerId = "22"
            };

            // Act
            _handler.Handle(command);

            // Assert
            var offerBuyerId = _context.Properties.First().Offers.First().BuyerId;
            Assert.AreEqual("22", offerBuyerId);

        }


    }
}
