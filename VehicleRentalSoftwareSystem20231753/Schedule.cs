using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class Schedule : IComparable<Schedule>
    {
        private DateTime pickUpDate;        
        private DateTime dropOffDate;

        public Schedule()
        {       

        }

        public Schedule(DateTime pickupDate, DateTime dropOffDate)
        {
            this.pickUpDate = pickupDate;
            this.dropOffDate = dropOffDate;
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


        public override string ToString()
        {
            return $"From {this.pickUpDate.ToShortDateString()} to {this.dropOffDate.ToShortDateString()}";
        }

        public int CompareTo(Schedule? other)
        {
            return this.pickUpDate.CompareTo(other?.GetPickUpDate());
        }
    }
}
