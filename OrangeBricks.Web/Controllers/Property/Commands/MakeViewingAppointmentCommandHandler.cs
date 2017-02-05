using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OrangeBricks.Web.Models;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeViewingAppointmentCommandHandler
    {
        private IOrangeBricksContext _context;

        public MakeViewingAppointmentCommandHandler(IOrangeBricksContext _context)
        {
            this._context = _context;
        }

        public void Handle(MakeViewingAppointmentCommand command)
        {
            var property = _context.Properties.FirstOrDefault();

            if (property.ViewingAppointment == null)
            {
                property.ViewingAppointment = new List<ViewingAppointment>();
            }

            var viewingAppointmentDateTime = new ViewingAppointment
            {
                ViewingAppointmentDateTime = command.ViewingAppointmentDateTime,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                BuyerId = command.BuyerId,
                Status = Statuses.Pending
            };

            property.ViewingAppointment.Add(viewingAppointmentDateTime);

            _context.SaveChanges();
        }
    }
}