using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    //Motorbike is inherited from the abstract class Vehicle
    internal class Motorbike : Vehicle
    {
        //No argument constructor
        public Motorbike()
        {

        }

        //All argument constructor
        public Motorbike(Type type, string registrationNumber, string make, string model, double dailyRent, List<Schedule> schedules) : base(type, registrationNumber, make, model, dailyRent, schedules)
        {
            
        }
    }
}
