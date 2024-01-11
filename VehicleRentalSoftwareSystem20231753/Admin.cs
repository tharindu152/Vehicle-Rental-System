using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class Admin : IRentalManager
    {
        //Implementation of contacted methods of IRentalManager
        //To add a new vehicle into the rental system
        public bool AddVehicle(Vehicle v)
        {
            //boolean value which will be returned by the method
            bool added = false;            

            try 
            {
                //Validation to ensure that the system does not allow duplicate vehicle entries
                if (RentalServiceDB.vehiclePool.ContainsKey(v.GetRegistrationNumber())) {
                    Console.WriteLine("Vehicle already exists in the Vehicle Pool. Please check the registration number");
                    added = false;                    
                } else if(RentalServiceDB.allocatedParkingSlots >= RentalServiceDB.MAX_PARKING_SLOTS) {
                    //Display a message if all parking slots are occupied
                    Console.WriteLine("\nCan not add the vehicle. All parking slots are occupied");
                    added = false;
                } else if (RentalServiceDB.allocatedParkingSlots < RentalServiceDB.MAX_PARKING_SLOTS) {  
                    //Add vehicle to vehicle pool dictionary
                    RentalServiceDB.vehiclePool.Add(v.GetRegistrationNumber(), v);    
                    //increase the allocated parking slot count by 1
                    RentalServiceDB.allocatedParkingSlots++;

                    //Message with the number of remaining parking lots will be displayed after a successful add operation
                    Console.WriteLine($"\nVehicle added successfully.\nNumber of remaining parking slots are: {RentalServiceDB.MAX_PARKING_SLOTS - RentalServiceDB.allocatedParkingSlots}");
                    added = true;
                } 

            }
            //Any exception will be handled here
            catch (Exception ex) 
            {
                added = false;
                Console.WriteLine(ex.Message);
            }

            return added;
        }

        //To delete a vehicle identified by number from the system
        public bool DeleteVehicle(string number)
        {
            //boolean value which will be returned by the method
            bool removed = false;

            try 
            {
                //Check whether there is a vehicle with given registration number
                if (RentalServiceDB.vehiclePool.ContainsKey(number)) {
                    // Vehicle information will be saved first to display a message with deleted vehicle information
                    string vehicleInfo = RentalServiceDB.vehiclePool[number].ToString();
                    //Remove vehicle from vehicle pool dictionary
                    RentalServiceDB.vehiclePool.Remove(number);
                    //decrease the allocated parking slot count by 1
                    RentalServiceDB.allocatedParkingSlots--;
                    //Display details of deleted vehicle
                    Console.WriteLine($"\nDetails of the deleted vehicle:\n{vehicleInfo}");

                    //Display number of remaining parking slots after a successful delete operation
                    Console.WriteLine($"\nNumber of available parking slots are: {RentalServiceDB.MAX_PARKING_SLOTS - RentalServiceDB.allocatedParkingSlots}");
                    removed = true;

                // Check whether the vehicle pool is empty
                } else if (RentalServiceDB.vehiclePool.Keys.Count == 0) {
                    Console.WriteLine("No Vehicles available in the vehicle pool.");
                    removed = false;
                } else {
                    Console.WriteLine("Can not find a vehicle from given registration number. Registration number format is Eg: V-001");
                    removed = false;
                }
            }
            //Any exception will be handled here
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return removed;
        }

        //To print a list of the vehicles in the system.
        public void ListVehicles()
        {
            //Check whether the vehicle pool is empty
            if (RentalServiceDB.vehiclePool.Count == 0) {
                Console.WriteLine("\nNo vehicles available in the vehicle pool");
                return;
            }

            //Save values of vehiclePool dictionary in a list
            List<Vehicle> tempVehiclePool = new List<Vehicle>(RentalServiceDB.vehiclePool.Values);

            //Iterate through the list and print vehicle details
            for (int i = 0; i < tempVehiclePool.Count; i++) 
            {
                Console.Write($"{i + 1})");
                Console.WriteLine(tempVehiclePool[i].ToString());
            }

            //Finally print the remaining parking slots
            Console.WriteLine("\tNumber of parking slots remaining: " + (RentalServiceDB.MAX_PARKING_SLOTS - RentalServiceDB.allocatedParkingSlots));
        }

        // To print a list of the vehicles ordered alphabetically according to the vehicle Make
        public void ListOrderedVehicles()
        {
            //Check whether the vehicle pool is empty
            if (RentalServiceDB.vehiclePool.Count == 0) {
                Console.WriteLine("\nNo vehicles available in the vehicle pool");
                return;
            }

            //Save values of vehiclePool dictionary in a list
            List<Vehicle> vehicleList = new List<Vehicle>(RentalServiceDB.vehiclePool.Values);
            //Sort the list according to the CompareTo() method of Vehicle class
            vehicleList.Sort();

            //Iterate through the list and print vehicle details
            for (int i = 0; i < vehicleList.Count; i++)
            {
                Console.Write($"{i + 1})");
                Console.WriteLine(vehicleList[i].ToString());
            }

            //Finally print the remaining parking slots
            Console.WriteLine("\tNumber of parking slots remaining: " + (RentalServiceDB.MAX_PARKING_SLOTS - RentalServiceDB.allocatedParkingSlots));
        }

        public void GenerateReport(string fileName)
        {
            try {
                //Create a absolute path using file name to save the report in specified location
                string location = $"C:\\Users\\ASUS\\Desktop\\{fileName}.txt";
                
                //Create a file stream and enable read write access
                FileStream fileStream = new FileStream(location, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                
                //Create a write stream
                StreamWriter stream = new StreamWriter(fileStream);

                //Save values of vehiclePool dictionary in a list
                List<Vehicle> vehicleList = new List<Vehicle>(RentalServiceDB.vehiclePool.Values);
                //Sort the list according to the CompareTo() method of Vehicle class
                vehicleList.Sort();

                //Write the report heading
                stream.Write("***Currently Available Vehicles and their Bookings***\n\n");
                
                //Write vehicle details
                for (int i = 0; i < vehicleList.Count; i++) 
                {
                    stream.Write($"{i + 1})");
                    stream.Write(vehicleList[i].ToString() + "\n");
                }
                //Finally write the remaining parking slots
                stream.Write("\tNumber of parking slots remaining: " + (RentalServiceDB.MAX_PARKING_SLOTS - RentalServiceDB.allocatedParkingSlots));
                
                //close the write stream
                stream.Dispose();

                //Display a message with report name and saved location
                Console.WriteLine($"\nReport is created with filename {fileName}.txt at {location}");
            }
            //Any exception will be handled here
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
