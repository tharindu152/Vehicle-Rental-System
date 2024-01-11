using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal abstract class Vehicle : IComparable<Vehicle>
    {
        // Common attributes for all Vehicles
        private Type type;
        private string registrationNumber;
        private string make;
        private string model;
        private double dailyRent;
        private List<Schedule> schedules;   // schedules attribute will store reservations for a vehicle.

        // No argument constructor
        public Vehicle()
        {

        }

        // All argument constructor
        public Vehicle(Type type, string registrationNumber, string make, string model, double dailyRent, List<Schedule> schedules)
        {
            this.type = type;
            this.registrationNumber = registrationNumber;
            this.make = make;
            this.model = model;
            this.dailyRent = dailyRent;
            this.schedules = schedules;
        }

        public void SetVehicleType(Type type)
        {
            this.type = type;
        }

        public Type GetVehicleType()
        {
            return type;
        }

        public void SetRegistrationNumber(string registrationNumber)
        {
            this.registrationNumber = registrationNumber;
        }

        public string GetRegistrationNumber()
        {
            return registrationNumber;
        }

        public void SetMake(string make)
        {
            this.make = make;
        }

        public string GetMake()
        {
            return make;
        }

        public void SetModel(string model)
        {
            this.model = model;
        }

        public string GetModel()
        {
            return model;
        }

        public void SetDailyRent(double dailyRent)
        {
            this.dailyRent = dailyRent;
        }

        public double GetDailyRent()
        {
            return dailyRent;
        }

        public void SetSchedules(List<Schedule> schedules)
        {
            this.schedules = schedules;
        }

        public List<Schedule> GetSchedules()
        {
            return schedules;
        }

        // ToString() method of Object class overridden 
        // For each vehicle, information on the registration number, type, make, model, rent, reservations will to be displayed.
        public override string ToString()
        {
            string vehicleInfo = "\tRegistration number: " + registrationNumber.ToString();
            vehicleInfo += "\n\tType: " + type.ToString();
            vehicleInfo += "\n\tMake: " + make.ToString();
            vehicleInfo += "\n\tModel: " + model.ToString();
            vehicleInfo += "\n\tRent per day: LKR " + dailyRent.ToString();
            vehicleInfo += "\n\tBookings: ";
            if (this.schedules.Count > 0) {
                schedules.Sort();
                foreach (Schedule s in schedules) {
                    vehicleInfo += $"\n\t\t{s?.ToString()}";
                }                
            }            
            vehicleInfo += "\n";

            return vehicleInfo;
        }

        // CompareTo() method of IComparable interface implemented
        // To order alphabetically according to the vehicle Make
        public int CompareTo(Vehicle? other)
        {
            return this.make.CompareTo(other?.GetMake());
        }


    }
}
