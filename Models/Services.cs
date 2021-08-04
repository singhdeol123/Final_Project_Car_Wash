using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project_Car_Wash.Models
{
    public class Services
    {
        // service id
        public int Id { get; set;}
        
        // detail of services display here
        public string TypeofServices { get; set; }

        // Price of service provider
        public int Price { get; set; }
    }
}
