using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class Vehicle : IComparable<Vehicle>
    {
        private Type type;
        private string registrationNumber;
        private string make;
        private string model;
        private double dailyRent;
        private List<Schedule> schedules;

        public Vehicle()
        {

        }

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

        // to string method that overridden from super class
        public override string ToString()
        {
            string vehicleInfo = "Registration number: " + registrationNumber.ToString();
            vehicleInfo += "\nType: " + type.ToString();
            vehicleInfo += "\nMake: " + make.ToString();
            vehicleInfo += "\nModel: " + model.ToString();
            vehicleInfo += "\nRent per day: LKR " + dailyRent.ToString();
            vehicleInfo += "\nSchedule: ";
            if (this.schedules.Count > 0) {
                schedules.Sort();
                foreach (Schedule s in schedules) {
                    vehicleInfo += $"\n\t{s?.ToString()}";
                }                
            }
            vehicleInfo += "\n";

            return vehicleInfo;
        }

        public int CompareTo(Vehicle? other)
        {
            return this.make.CompareTo(other?.GetMake());
        }


    }
}
