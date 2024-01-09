using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    static class VehicleDB
    {
        public const int MAX_PARKING_SLOTS = 10;
        public static int allocatedParkingSlots = 0;
        public static Dictionary<string, Vehicle> vehiclePool = new();       

    }
}
