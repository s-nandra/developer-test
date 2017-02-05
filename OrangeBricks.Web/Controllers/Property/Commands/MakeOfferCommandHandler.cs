using System;
using System.Collections.Generic;
using System.Linq;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeOfferCommandHandler
    {
        private readonly IOrangeBricksContext _context;

        public MakeOfferCommandHandler(IOrangeBricksContext context)
        {
            _context = context;
        }

        public void Handle(MakeOfferCommand command)
        {
            //var property = _context.Properties.Find(command.PropertyId);

            var property = _context.Properties.FirstOrDefault();

            if (property.Offers == null)
            {
                property.Offers = new List<Offer>();
            }

            var offer = new Offer
            {
                Amount = command.Offer,
                Status = Statuses.Pending,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                BuyerId = command.BuyerId
            };

            property.Offers.Add(offer);

            _context.SaveChanges();
        }

    }
}