using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class Schedule : IComparable<Schedule>
    {
        //Attributes of a schedule
        private DateTime pickUpDate;        
        private DateTime dropOffDate;
        private Driver driver;
        private double totalRent;

        //No argument constructor
        public Schedule()
        {       

        }

        //Constructor without total rent
        public Schedule(DateTime pickupDate, DateTime dropOffDate, Driver driver)
        {
            this.pickUpDate = pickupDate;
            this.dropOffDate = dropOffDate;
            this.driver = driver;
        }

        //All argument constructor
        public Schedule(DateTime pickupDate, DateTime dropOffDate, Driver driver, double totalRent)
        {
            this.pickUpDate = pickupDate;
            this.dropOffDate = dropOffDate;
            this.driver = driver;
            this.totalRent = totalRent;
        }

        public void SetPickUpDate(DateTime pickUpDate)
        {
            this.pickUpDate = pickUpDate;
        }

        public DateTime GetPickUpDate()
        {
            return pickUpDate;
        }

        public void SetDropOffDate(DateTime dropOffDate)
        {
            this.dropOffDate = dropOffDate;
        }

        public DateTime GetDropOffDate()
        {
            return dropOffDate;
        }

        public void SetDriver(Driver driver)
        {
            this.driver = driver;
        }

        public Driver GetDriver()
        {
            return driver;
        }

        public void SetTotalRent(double totalRent)
        {
            this.totalRent = totalRent;
        }

        public double GetTotalRent()
        {
            return totalRent;
        }

        // ToString() method of Object class overridden
        public override string ToString()
        {
            return $"From {this.pickUpDate.ToShortDateString()} to {this.dropOffDate.ToShortDateString()} Driven by {driver.GetName()} {driver.GetSurname()}, Total Rent: {totalRent}";
        }



        //CompareTo() method of IComparable interface implemented
        //The bookings will be sorted in chronological order according to the pickup date
        public int CompareTo(Schedule? other)
        {
            return this.pickUpDate.CompareTo(other?.GetPickUpDate());
        }

        // Equals() method of Object class overridden to compare schedules only based on pickup date and drop off date
        public override bool Equals(object? obj)
        {
            return obj is Schedule schedule &&
                   pickUpDate == schedule.pickUpDate &&
                   dropOffDate == schedule.dropOffDate;
        }

    }
}
