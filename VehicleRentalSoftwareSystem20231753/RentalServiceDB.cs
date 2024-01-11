using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    static class RentalServiceDB
    {
        //This static class will serve as a single source of data for both Admin and Customer operations. 

        public const int MAX_PARKING_SLOTS = 50;    //Maximum number of parking slots as a constant
        public static int allocatedParkingSlots = 0;    //Allocated number of parking slots as editable value 
        public static Dictionary<string, Vehicle> vehiclePool = new();      //Dictionary to store, check availability, update, remove vehicles
        public static List<Driver> drivers = new();     //List to store Driver details

    }
}
