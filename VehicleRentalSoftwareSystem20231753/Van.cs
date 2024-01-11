using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    //Van is inherited from the abstract class Vehicle
    internal class Van : Vehicle
    {
        //No argument constructor
        public Van()
        {
            
        }

        //All argument constructor
        public Van(Type type, string registrationNumber, string make, string model, double dailyRent, List<Schedule> schedules) : base(type, registrationNumber, make, model, dailyRent, schedules)
        {

        }
    }
}
