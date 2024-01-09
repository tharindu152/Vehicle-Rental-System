using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class Driver
    {
        private string name;
        private string surname;
        private DateTime dateOfBirth;
        private string licenseNumber;

        public Driver()
        {

        }

        public Driver(string name, string surname, DateTime dateOfBirth, string licenseNumber)
        {
            this.name = name;
            this.surname = surname;
            this.dateOfBirth = dateOfBirth;
            this.licenseNumber = licenseNumber;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public string getName()
        {
            return this.name;
        }

        public void setSurname(string surname)
        {
            this.surname = surname;
        }

        public string getSurname()
        {
            return this.surname;
        }

        public void setDateOfBirth(DateTime dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
        }

        public DateTime getDateOfBirth()
        {
            return this.dateOfBirth;
        }

        public void setLicenseNumber(string licenseNumber)
        {
            this.licenseNumber = licenseNumber;
        }

        public string getLicenseNumber()
        {
            return this.licenseNumber;
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
