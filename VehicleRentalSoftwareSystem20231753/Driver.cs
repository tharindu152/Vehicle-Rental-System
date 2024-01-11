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
        //Attributes of the driver
        private string name;
        private string surname;
        private DateTime dateOfBirth;
        private string licenseNumber;

        //No argument constructor
        public Driver()
        {

        }

        //All argument constructor
        public Driver(string name, string surname, DateTime dateOfBirth, string licenseNumber)
        {
            this.name = name;
            this.surname = surname;
            this.dateOfBirth = dateOfBirth;
            this.licenseNumber = licenseNumber;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetSurname(string surname)
        {
            this.surname = surname;
        }

        public string GetSurname()
        {
            return this.surname;
        }

        public void SetDateOfBirth(DateTime dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
        }

        public DateTime GetDateOfBirth()
        {
            return this.dateOfBirth;
        }

        public void SetLicenseNumber(string licenseNumber)
        {
            this.licenseNumber = licenseNumber;
        }

        public string GetLicenseNumber()
        {
            return this.licenseNumber;
        }

    }
}
