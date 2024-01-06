using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class Schedule
    {
        private DateTime pickupDate;        
        private DateTime dropoffDate;

        public Schedule()
        {                       

        }

        public Schedule(DateTime pickupDate, DateTime dropoffDate)
        {
            this.pickupDate = pickupDate;
            this.dropoffDate = dropoffDate;
        }

        public void SetpickupDate(DateTime pickupDate)
        {
            this.pickupDate = pickupDate;
        }

        public DateTime GetPickupDate()
        {
            return pickupDate;
        }

        public void SetDropoffDate(DateTime dropoffDate)
        {
            this.dropoffDate = dropoffDate;
        }

        public DateTime GetDropoffDate()
        {
            return dropoffDate;
        }


        public override string? ToString()
        {
            return pickupDate.ToShortDateString() + " to " + dropoffDate.ToShortDateString();
        }
    }
}
