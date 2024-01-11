using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    //Electric Car is inherited from the abstract class Vehicle
    internal class ElectricCar : Vehicle
    {
        //Electric car has its unique attribute battery percentage
        private int batteryPercentage;

        //No argument constructor
        public ElectricCar()
        {

        }
        
        //All argument constructor
        public ElectricCar(Type type, string registrationNumber, string make, string model, double dailyRent, List<Schedule> schedules, int batteryPercentage) : base(type, registrationNumber, make, model, dailyRent, schedules)
        {
            
            this.batteryPercentage = batteryPercentage;
        }
       
    }
}
