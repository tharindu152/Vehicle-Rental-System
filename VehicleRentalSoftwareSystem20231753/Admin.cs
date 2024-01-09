using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class Admin : IRentalManager
    {
        
        public bool AddVehicle(Vehicle v)
        {
            bool added = false;
            

            try 
            {
                if (VehicleDB.vehiclePool.ContainsKey(v.GetRegistrationNumber())) {
                    Console.WriteLine("Vehicle already exists in the Vehicle Pool. Please check the registration number");
                    added = false;                    
                } else if(VehicleDB.allocatedParkingSlots >= VehicleDB.MAX_PARKING_SLOTS) {
                    Console.WriteLine("\nCan not add the vehicle. All parking slots are occupied");
                    added = false;
                } else if (VehicleDB.allocatedParkingSlots < VehicleDB.MAX_PARKING_SLOTS) {                                       
                    VehicleDB.vehiclePool.Add(v.GetRegistrationNumber(), v);                    
                    VehicleDB.allocatedParkingSlots++;
                    Console.WriteLine($"\nVehicle added successfully.\nNumber of remaining parking slots are: {VehicleDB.MAX_PARKING_SLOTS - VehicleDB.allocatedParkingSlots}");
                    added = true;
                } 

            }
            catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);
            }

            return added;
        }

        public bool DeleteVehicle(string number)
        {
            bool removed = false;

            try {
                if (VehicleDB.vehiclePool.ContainsKey(number)) {
                    string vehicleInfo = VehicleDB.vehiclePool[number].ToString();
                    VehicleDB.vehiclePool.Remove(number);
                    VehicleDB.allocatedParkingSlots--;
                    Console.WriteLine($"\nDetails of the deleted vehicle:\n{vehicleInfo}");
                    Console.WriteLine($"\nNumber of available parking slots are: {VehicleDB.MAX_PARKING_SLOTS - VehicleDB.allocatedParkingSlots}");
                    removed = true;
                } else if (VehicleDB.vehiclePool.Keys.Count == 0) {
                    Console.WriteLine("No Vehicles available in the vehicle pool.");
                    removed = false;
                } else {
                    Console.WriteLine("Can not find a vehicle from given registration number. Registration number format is Eg: V-001");
                    removed = false;
                }
            }catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);
            }

            return removed;
        }

        public void ListVehicles()
        {
            if (VehicleDB.vehiclePool.Count == 0) {
                Console.WriteLine("\nNo vehicles available in the vehicle pool");
                return;
            }

            foreach (Vehicle vehicle in VehicleDB.vehiclePool.Values) {
                Console.WriteLine(vehicle.ToString());
            }
        }

        public void ListOrderedVehicles()
        {
            if (VehicleDB.vehiclePool.Count == 0) {
                Console.WriteLine("\nNo vehicles available in the vehicle pool");
                return;
            }

            List<Vehicle> tempVehiclePool = new List<Vehicle>(VehicleDB.vehiclePool.Values);
            tempVehiclePool.Sort();

            foreach (Vehicle vehicle in tempVehiclePool) {
                Console.WriteLine(vehicle.ToString());
            }
        }

        public void GenerateReport(string fileName)
        {
            try {
                string location = $"C:\\Users\\ASUS\\Desktop\\{fileName}.txt";
                
                FileStream fileStream = new FileStream(location, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                
                StreamWriter stream = new StreamWriter(fileStream);
                
                List<Vehicle> vehicleList = new List<Vehicle>(VehicleDB.vehiclePool.Values);
                vehicleList.Sort();

                foreach (Vehicle vehicle in vehicleList) {
                    stream.Write(vehicle.ToString() + "\n");
                }

                stream.Dispose();

                Console.WriteLine($"\nReport is created with filename {fileName}.txt at {location}");
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
