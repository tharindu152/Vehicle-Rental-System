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
        private Vehicle curVehicle;
        
        public bool AddReservation(string number, Schedule wantedSchedule)
        {
            bool reserved = false;
            try {
                curVehicle = VehicleDB.vehiclePool[number];
                if (!Overlaps(wantedSchedule) && curVehicle != null) {
                    VehicleDB.vehiclePool[number].GetSchedules().Add(wantedSchedule);
                    reserved = true;
                } else {
                    Console.WriteLine("Wanted schedule overlaps with a existing schedule");
                }
            }catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);
            }
            return reserved;
        }

        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            bool changed = false;
            try {
                curVehicle = VehicleDB.vehiclePool[number];
                if (curVehicle != null) {
                    DeleteReservation(number, oldSchedule);
                    AddReservation(number, newSchedule);
                    changed = true;
                }
            }catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);
            }
            return changed;
        }

        public bool DeleteReservation(string number, Schedule schedule)
        {
            bool deleted = false;
            int index = 0;
            try {
                curVehicle = VehicleDB.vehiclePool[number];
                if (curVehicle != null) {                     
                    List<Schedule> schedules = VehicleDB.vehiclePool[number].GetSchedules();
                    for (global::System.Int32 i = 0; i < schedules.Count; i++)
                    {
                        if (schedules[i].Equals(schedule)) {
                            index = i; break;
                        }
                    }
                    schedules.RemoveAt(index);
                    deleted = true;
                } else {
                    Console.WriteLine("Vehicle does not exist");
                }
            } catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);
            }
            return deleted;
        }

        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            try {
                bool available = false;
                List<Vehicle> vehicles = new List<Vehicle>(VehicleDB.vehiclePool.Values);
                foreach (Vehicle vehicle in vehicles) {
                    if (vehicle.GetVehicleType().Equals(type)) {
                        curVehicle = vehicle;
                        if (!Overlaps(wantedSchedule)) {
                            Console.WriteLine(vehicle.ToString());
                            available = true;
                        }
                    }
                }

                if (!available) {
                    Console.WriteLine("There are no vehicles available for the given schedule");
                }

            }catch(ArgumentException ex) {
                Console.WriteLine(ex.Message);
            }           

        }

        public bool Overlaps(Schedule newSchedule)
        {
            // when calling this method the currentVehicle has been initialized to currentVehicle instance variable. 
            // so that we can get specific vehicle object instead of looping all the vehicles. 
            foreach (Schedule schedule in curVehicle.GetSchedules()) {
                // this CompareTo() method is in DateTime class that return -1, 0, 1 according to the values we pass to two parameters
                //-1 = 1 Objt is is later than 2Obj | 0 = both objects are same | -1 = 1 Obj is earlier than 2 Obj

                int value = newSchedule.GetPickUpDate().CompareTo(schedule.GetPickUpDate());
                if (value == 0) {
                    return true;
                } else if (value > 0 && newSchedule.GetPickUpDate().CompareTo(schedule.GetDropOffDate()) <= 0) {
                    return true;
                } else if (value < 0 && newSchedule.GetDropOffDate().CompareTo(schedule.GetPickUpDate()) >= 0) {
                    return true;
                }
            }
            return false;
        }
    }
}
