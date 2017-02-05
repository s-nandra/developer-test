using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrangeBricks.Web.Controllers.Property.Commands
{
    public class MakeViewingAppointmentCommand
    {
        public int PropertyId { get; set; }
        public DateTime ViewingAppointmentDateTime { get; set; }
        public string BuyerId { get; set; }
    }
}