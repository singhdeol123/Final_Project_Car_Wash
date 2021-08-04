using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project_Car_Wash.Models
{
    public class Booking
    {
        // Booking ID
        public int ID { get; set; }

        //Booking Date
        public DateTime Date { get; set; }

        // Service Id
        public int ServiceId { get; set; }

        // customer Id
        public int CustomerId { get; set; }

        //Services link
        public Services Services { get; set; }
        //Customer link
        public Customer Customer { get; set; }

    }
}
