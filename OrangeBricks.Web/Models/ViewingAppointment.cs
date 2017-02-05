using System;
using System.ComponentModel.DataAnnotations;

namespace OrangeBricks.Web.Models
{
    public class ViewingAppointment
    {
        [Key]
        public int Id { get; set; }

        public int PropertyId { get; set; }

        public DateTime ViewingAppointmentDateTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string BuyerId { get; set; }

        public Statuses Status { get; set; }

    }
}