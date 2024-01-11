using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class Customer : IRentalCustomer, IOverlappable
    {
        //This variable will save the current vehicle which will be used in Overlaps method
        private Vehicle? curVehicle;

        //Implementation of contacted methods of IRentalCustomer
        //To make a reservation for a Vehicle
        public bool AddReservation(string number, Schedule wantedSchedule)
        {
            //boolean value which will be returned from the method
            bool reserved = false;
            try {
                curVehicle = RentalServiceDB.vehiclePool[number];
                //check whether the vehicle exists in the vehicle pool and whether the wanted schedule overlaps with a existing schedule
                if (!Overlaps(wantedSchedule) && curVehicle != null) {
                    //Calculates the total rent (daily rent * number of days)
                    double total = curVehicle.GetDailyRent() * wantedSchedule.GetDropOffDate().Subtract(wantedSchedule.GetPickUpDate()).TotalDays;
                    //set the total rent to the schedule
                    wantedSchedule.SetTotalRent(total);
                    //Add schedule to the current vehicle
                    curVehicle.GetSchedules().Add(wantedSchedule);
                    //set boolean value to true
                    reserved = true;
                } else {
                    //display a message if schedules overlap
                    Console.WriteLine("Wanted schedule overlaps with a existing schedule");
                }
            }
            //Any exception will be handled here
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return reserved;
        }

        //To modify the start and/or end date of an existing reservation for the vehicle identified by registration number.
        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            //boolean value which will be returned from the method
            bool changed = false;
            try {
                //check whether the vehicle exists in the vehicle pool
                curVehicle = RentalServiceDB.vehiclePool[number];
                if (curVehicle != null) {
                    //delete the old schedule first. This makes it easier to identify overlapable schedules
                    DeleteReservation(number, oldSchedule);
                    bool valid = AddReservation(number, newSchedule);

                    //If adding new schedule fails, old schedule will be assigned again
                    if (!valid) {
                        AddReservation(number, oldSchedule);
                    }

                    //set boolean value to true
                    changed = true;
                }
            }
            //Any exception will be handled here
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return changed;
        }

        //To delete an existing reservation for a vehicle identified by number on a given schedule
        public bool DeleteReservation(string number, Schedule schedule)
        {
            //boolean value which will be returned from the method
            bool deleted = false;
            int index = 0;
            try {
                //check whether the vehicle exists in the vehicle pool
                curVehicle = RentalServiceDB.vehiclePool[number];
                if (curVehicle != null) {  
                    //Save schedules in a list
                    List<Schedule> schedules = curVehicle.GetSchedules();

                    //iterate through the list
                    for (global::System.Int32 i = 0; i < schedules.Count; i++)
                    {
                        //identify the index of the schedule
                        if (schedules[i].Equals(schedule)) {
                            index = i;
                            //break from the loop as soon as a match is found
                            break;
                        }
                    }
                    //Remove the schedule using index 
                    schedules.RemoveAt(index);

                    //set boolean value to true
                    deleted = true;
                } else {
                    //Display a message if vehicle does not exist in the vehicle pool
                    Console.WriteLine("Vehicle does not exist");
                }
            }
            //Any exception will be handled here
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return deleted;
        }

        //To list the information of vehicles of a given type that are available on a specific wantedSchedule
        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            try {
                //boolean value which will be returned from the method
                bool available = false;

                //Save schedules in a list
                List<Vehicle> vehicles = new List<Vehicle>(RentalServiceDB.vehiclePool.Values);
                //Iterate through the list
                foreach (Vehicle vehicle in vehicles) {
                    //Filter the requested type of vehicles
                    if (vehicle.GetVehicleType().Equals(type)) {
                        curVehicle = vehicle;
                        //Filter the vehicles which does not overlap with requested schedule
                        if (!Overlaps(wantedSchedule)) {
                            Console.WriteLine(vehicle.ToString());
                            available = true;
                        }
                    }
                }

                //Display a message if there are no vehicles available for the given schedule
                if (!available) {
                    Console.WriteLine("There are no vehicles available for the given schedule");
                }

            }
            //Any exception will be handled here
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }           

        }

        //implementation of the Overlaps method of IOverlappable interface
        public bool Overlaps(Schedule newSchedule)
        {
            //curVehicle is initialized at this points. That prevents the requirement of Iterating though all vehicles.
            //Iterate all schedules of the curVehicle
            foreach (Schedule schedule in curVehicle.GetSchedules()) {
                // this CompareTo() method is in DateTime class returns -1, 0, 1 according to the values we pass to two parameters
                //-1 = 1 object1 is is later than object2 , 0 = both objects are at same time,  1 = object1 is earlier than object2

                int value = newSchedule.GetPickUpDate().CompareTo(schedule.GetPickUpDate());
                //new schedule's pickup date is similar to existing schedule's pickup date
                if (value == 0) {
                    return true;

                //new schedule's pickup date is after the existing schedule's pickup date 
                //new schedule's pickup date is before the existing schedule's drop off date
                } else if (value > 0 && newSchedule.GetPickUpDate().CompareTo(schedule.GetDropOffDate()) <= 0) {
                    return true;

                //new schedule's pickup date is before the existing schedule's pickup date 
                //new schedule's drop off date is after the existing schedule's pickup date
                } else if (value < 0 && newSchedule.GetDropOffDate().CompareTo(schedule.GetPickUpDate()) >= 0) {
                    return true;
                }
            }
            return false;
        }
    }
}
