using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class ElectricCar : Vehicle
    {

        public ElectricCar()
        {

        }

        public ElectricCar(Type type, string registrationNumber, string make, string model, double dailyRent, List<Schedule> schedules) : base(type, registrationNumber, make, model, dailyRent, schedules)
        {
            
        }
       
    }
}
