using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class WestminsterRentalVehicle
    {
        private static Customer customer = new Customer();
        private static Admin admin = new Admin();
        
        
        static void Main(string[] args)
        {
            //Five Drivers are created and added to the RentalServiceDB before initializing the app
            RentalServiceDB.drivers.Add(new Driver("David", "Costa", DateTime.Parse("03/24/1975"), "12345"));
            RentalServiceDB.drivers.Add(new Driver("Kamal", "Weththamuni", DateTime.Parse("12/04/1965"), "76445"));
            RentalServiceDB.drivers.Add(new Driver("Kasun", "Sampath", DateTime.Parse("07/14/1995"), "73642"));
            RentalServiceDB.drivers.Add(new Driver("Michel", "Thissera", DateTime.Parse("03/24/1975"), "78902"));
            RentalServiceDB.drivers.Add(new Driver("David", "Costa", DateTime.Parse("08/28/1955"), "62593"));

            //Display console title
            Console.Title = $"Westminster Vehicle Rental Service";

            //Start the application by displaying customer menu
            ShowCustomerMenu();            
        }

        //Display customer menu
        public static void ShowCustomerMenu()
        {
            DateTime pickUpDate;
            DateTime dropOffDate;            
            Schedule schedule;
            Random random = new Random();

            //Options for customer
            CreateTopic("Customer Menu" , ConsoleColor.Green);            
            Console.Write("Options: \n" +
                              "1) List Available Vehicles\n" +
                              "2) Add Reservation\n" +
                              "3) Change Reservation\n" +
                              "4) Delete Reservation\n" +
                              "5) Go to admin menu\n\n" +
                              "Please select an option between 1 to 5: ");     
            
            try {
                int option = Convert.ToInt32(Console.ReadLine());
                //switch to a selected option by the customer
                switch (option) {
                    //To list the information of vehicles of a given type that are available on a specific wantedSchedule
                    case 1:
                        CreateTopic("List Available Vehicles", ConsoleColor.Green);
                        //Get the requested schedule from customer
                        Console.Write("Please enter the pick up date. format:- MM/dd/yyyy : ");
                        pickUpDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the drop off date. format:- MM/dd/yyyy : ");
                        dropOffDate = DateTime.Parse(Console.ReadLine());
                        
                        //Validate the pick up date and drop off date
                        if (pickUpDate < dropOffDate && !(DateTime.Now > pickUpDate)) {
                            
                            Type vehicleType = 0;

                            label1:
                            bool flag = true;
                            while (flag) {
                                flag = false;
                                //Get the requested vehicle type from customer
                                Console.Write("Vehicle types available: \n" +
                                                  "1) Van\n" +
                                                  "2) Car\n" +
                                                  "3) Electric Car\n" +
                                                  "4) Motorbike\n\n" +
                                                  "Please select an option between 1 to 4: ");

                                int type = Convert.ToInt32(Console.ReadLine());

                                switch (type) {
                                    case 1:
                                        vehicleType = Type.VAN;
                                        break;
                                    case 2:
                                        vehicleType = Type.CAR;
                                        break;
                                    case 3:
                                        vehicleType = Type.ELECTRIC_CAR;
                                        break;
                                    case 4:
                                        vehicleType = Type.MOTORBIKE;
                                        break;
                                    default:
                                        Console.WriteLine("\nInvalid Vehicle type. Please select an option between 1 to 4");
                                        //If selecting a vehicle type is failed, code execution will restart from label1
                                        goto label1;
                                }
                            }
                            //Select a driver for the schedule. For testing purposes selection is made randomly. Driver selection algorithm is yet to be implemented.
                            int driver = random.Next(0, 4);
                            //Assign a driver to the schedule
                            schedule = new Schedule(pickUpDate, dropOffDate, RentalServiceDB.drivers[driver]);

                            //List available vehicles for the given schedule
                            customer.ListAvailableVehicles(schedule, vehicleType);
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();

                        //Display a message if pickup date is a past date
                        } else if (DateTime.Now > pickUpDate) {
                            Console.WriteLine("Your pickup date should not be a past date");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                        //Display a message if pick up date is later than drop off date
                        } else if (pickUpDate > dropOffDate) {
                            Console.WriteLine("Your pickup date should be earlier than drop off date");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                        }                         
                        
                        ShowCustomerMenu();                       
                        break;

                    //To make a reservation for a Vehicle
                    case 2:
                        label2:
                        CreateTopic("Add Reservation", ConsoleColor.Green);
                        //Get the requested vehicle registration number from customer
                        Console.Write("Please enter the registration number of the vehicle: ");
                        string registrationNumber = Console.ReadLine();
                        //Get the requested schedule from customer
                        Console.Write("Please enter the pick up date. format:- MM/dd/yyyy: ");
                        pickUpDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the drop off date. format:- MM/dd/yyyy: ");
                        dropOffDate = DateTime.Parse(Console.ReadLine());

                        //Validate the pick up date and drop off date
                        if (pickUpDate < dropOffDate && !(DateTime.Now > pickUpDate)) {
                            //Select a driver for the schedule. For testing purposes driver selection is made randomly. Driver selection algorithm is yet to be implemented.
                            int driver = random.Next(0, 4);
                            //Assign a driver to the schedule
                            schedule = new Schedule(pickUpDate, dropOffDate, RentalServiceDB.drivers[driver]);

                            //Validate whether registration number is null or white spaces
                            if (!string.IsNullOrWhiteSpace(registrationNumber)) {
                                //Add the reservation
                                bool isRented = customer.AddReservation(registrationNumber, schedule);
                                if (isRented) {
                                    Console.WriteLine("The vehicle has been scheduled.");
                                    Console.WriteLine($"The driver assigned to your vehicle is {RentalServiceDB.drivers[driver].GetName()} {RentalServiceDB.drivers[driver].GetSurname()}.");

                                } else {
                                    Console.WriteLine("\nPlease enter to continue...");
                                    Console.ReadLine();
                                    //If adding reservation is failed, code execution will restart from label2
                                    goto label2;
                                }
                            //Display message if registration number is null or white spaces
                            } else {
                                Console.WriteLine("Please enter a valid registration number");
                                Console.WriteLine("\nPlease enter to continue...");
                                Console.ReadLine();
                                goto label2;
                            }
                        //Display a message if pickup date is a past date
                        } else if (DateTime.Now > pickUpDate) {
                            Console.WriteLine("Your pickup date should not be a past date");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                            //If adding reservation is failed, code execution will restart from label2
                            goto label2;

                        //Display a message if pick up date is later than drop off date
                        } else if (pickUpDate > dropOffDate) {
                            Console.WriteLine("Your pickup date should be earlier than drop off date");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                            //If adding reservation is failed, code execution will restart from label2
                            goto label2;
                        }

                        Console.WriteLine("\nPlease enter to continue...");
                        Console.ReadLine();
                        ShowCustomerMenu();
                        break;

                    // To modify the start and/or end date of an existing reservation for the vehicle identified by registration number
                    case 3:
                        label3:
                        CreateTopic("Change Reservation", ConsoleColor.Green);
                        //Get the requested vehicle registration number from customer
                        Console.Write("Please enter the registration number of the vehicle: ");
                        string regNumber = Console.ReadLine();

                        //Get the requested old and new schedules from customer
                        Console.Write("Please enter the old pick up date. format:- MM/dd/yyyy: ");
                        DateTime pickUpDateOld = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the old drop off date. format:- MM/dd/yyyy: ");
                        DateTime dropOffDateOld = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the new pick up date. format:- MM/dd/yyyy: ");
                        DateTime pickUpDateNew = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the new drop off date. format:- MM/dd/yyyy: ");
                        DateTime dropOffDateNew = DateTime.Parse(Console.ReadLine());

                        //Validate the pick up date and drop off date
                        if (pickUpDateOld < dropOffDateOld && pickUpDateNew < dropOffDateNew && !(DateTime.Now > pickUpDateNew)) {
                            //Select a driver for the schedule. For testing purposes selection is made randomly. Driver selection algorithm is yet to be implemented.
                            int driver = random.Next(0, 4);
                            //Assign a driver to the schedules
                            Schedule scheduleOld = new Schedule(pickUpDateOld, dropOffDateOld, RentalServiceDB.drivers[driver]);
                            Schedule scheduleNew = new Schedule(pickUpDateNew, dropOffDateNew, RentalServiceDB.drivers[driver]);

                            //Validate whether registration number is null or white spaces
                            if (!string.IsNullOrWhiteSpace(regNumber)) {
                                //Chane the reservation
                                bool isChanged = customer.ChangeReservation(regNumber, scheduleOld, scheduleNew);
                                if (isChanged) {
                                    Console.WriteLine("The schedule has been updated");
                                    Console.WriteLine($"The new driver assigned to your vehicle is {RentalServiceDB.drivers[driver].GetName()} {RentalServiceDB.drivers[driver].GetSurname()}.");
                                } else {
                                    Console.WriteLine("\nPlease enter to continue...");
                                    Console.ReadLine();
                                    //If changing reservation is failed, code execution will restart from label3
                                    goto label3;
                                }
                            //Display message if registration number is null or white spaces
                            } else {
                                Console.WriteLine("Please enter a valid registration number");
                                Console.WriteLine("\nPlease enter to continue...");
                                Console.ReadLine();
                                //If changing reservation is failed, code execution will restart from label3
                                goto label3;
                            }
                           
                        //Display a message if pickup dates are past dates
                        } else if (DateTime.Now > pickUpDateOld && DateTime.Now > pickUpDateNew) {
                            Console.WriteLine("Your pickup dates should not be a past dates");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                            //If changing reservation is failed, code execution will restart from label3
                            goto label3;

                        //Display a message if pick up dates are later than respective drop off dates
                        } else if (pickUpDateOld > dropOffDateOld && pickUpDateNew > dropOffDateNew) {
                            Console.WriteLine("Your pickup dates should be earlier than respective drop off dates");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                            //If changing reservation is failed, code execution will restart from label3
                            goto label3;
                        }

                        Console.WriteLine("\nPlease enter to continue...");
                        Console.ReadLine();  
                        ShowCustomerMenu();
                        break;

                    // To delete an existing reservation for a vehicle identified by number on a given schedule
                    case 4:
                        label4:
                        CreateTopic("Delete Reservation", ConsoleColor.Green);
                        //Get the requested vehicle registration number from customer
                        Console.Write("Please enter the registration number of the vehicle: ");
                        registrationNumber = Console.ReadLine();
                        //Get the requested schedule from customer
                        Console.Write("Please enter the pick up date. format:- MM/dd/yyyy: ");
                        pickUpDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the drop off date. format:- MM/dd/yyyy: ");
                        dropOffDate = DateTime.Parse(Console.ReadLine());

                        //Validate the pick up date and drop off date
                        if (pickUpDate < dropOffDate && !(DateTime.Now > pickUpDate)) {
                            //Select a driver for the schedule. For testing purposes selection is made randomly. Driver selection algorithm is yet to be implemented.
                            int driver = random.Next(0, 4);
                            //Assign a driver to the schedule
                            schedule = new Schedule(pickUpDate, dropOffDate, RentalServiceDB.drivers[driver]);
                            //Validate whether registration number is null or white spaces
                            if (!string.IsNullOrWhiteSpace(registrationNumber)) {
                                //Delete the reservation
                                bool isDeleted = customer.DeleteReservation(registrationNumber, schedule);
                                if (isDeleted) {
                                    Console.WriteLine("The schedule has been deleted");
                                } else {
                                    Console.WriteLine("\nPlease enter to continue...");
                                    Console.ReadLine();
                                    //If deleting reservation is failed, code execution will restart from label4
                                    goto label4;
                                }
                            } else {
                                Console.WriteLine("Please enter a valid registration number");
                                Console.WriteLine("\nPlease enter to continue...");
                                Console.ReadLine();
                                //If deleting reservation is failed, code execution will restart from label4
                                goto label4;
                            }
                            
                        //Display a message if pickup date is a past date
                        } else if (DateTime.Now > pickUpDate) {
                            Console.WriteLine("Your pickup dates should not be a past dates");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                            goto label4;

                        //Display a message if pick up date is later than drop off date
                        } else if (pickUpDate > dropOffDate) {
                            Console.WriteLine("Your pickup dates should be earlier than respective drop off dates");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                            goto label4;
                        }

                        Console.WriteLine("\nPlease enter to continue...");
                        Console.ReadLine();
                        ShowCustomerMenu();
                        break;

                    // To access the admin menu
                    case 5:
                        ShowAdminMenu();
                        break;

                    default:
                        //If invalid response is selected, customer menu will display again
                        Console.WriteLine("Please select a number from 1 to 5");
                        Console.WriteLine("\nPlease enter to continue...");
                        Console.ReadLine();
                        ShowCustomerMenu();
                        break;
                }
            } 
            //Handles exceptions of the entire menu
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("\nPlease enter to continue...");
                Console.ReadLine();
                ShowCustomerMenu();
            }
        }

        public static void ShowAdminMenu()
        {
            int option = 0;
            bool invalid = true;

            outer:
            //Admin menu options will loop until integer value is entered
            while (invalid) {
                invalid = false;
                try {
                    CreateTopic("Admin Menu", ConsoleColor.Blue);
                    //Options for admin
                    Console.Write("Options: \n" +
                                      "1) Add a vehicle\n" +
                                      "2) Delete a vehicle\n" +
                                      "3) List vehicles\n" +
                                      "4) List sorted vehicles\n" +
                                      "5) Generate vehicle report\n" +
                                      "6) Go to customer menu\n\n" +
                                      "Please select an option between 1 to 6: ");
                    
                    option = Convert.ToInt32(Console.ReadLine());
                //Any exception will be handled here
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message + " Please try again.");
                    invalid = true;
                }
            }
            
            switch (option) {
                // To add a new vehicle into the rental system
                case 1:
                    CreateTopic("Add a vehicle", ConsoleColor.Blue);
                    invalid = true;
                    int type = 0;
                    Vehicle vehicle = null;
                
                    inner:
                    //Vehicle type selection will loop until integer value is entered
                    while (invalid) {
                        invalid = false;
                        try {
                            //Validate whether there are any parking slots remaining to add vehicles
                            if (RentalServiceDB.allocatedParkingSlots >= RentalServiceDB.MAX_PARKING_SLOTS) {
                                //Display a message if all parking slots are occupied
                                Console.WriteLine("\nCan not add the vehicles. All parking slots are occupied");
                                Console.WriteLine("\nPlease enter to continue...");
                                Console.ReadLine();
                                ShowAdminMenu();
                            } else {
                                CreateTopic("Add Vehicle", ConsoleColor.Blue);

                                //Get the requested vehicle type from admin
                                Console.Write("Vehicle Types: \n" +
                                                  "1) Van\n" +
                                                  "2) Car\n" +
                                                  "3) Electric Car\n" +
                                                  "4) Motorbike\n\n" +
                                                  "Please select an option between 1 to 4: ");

                                type = Convert.ToInt32(Console.ReadLine());
                            }

                        //Any exception will be handled here
                        } catch (Exception ex) {
                            Console.WriteLine(ex.Message + " Please try again.");
                            invalid = true;
                        }
                    }

                    invalid = true;
                    while (invalid) {
                        invalid = false;
                        switch (type) {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                vehicle = CreateVehicle(type);                                
                                break;

                            default:
                                invalid = true;
                                Console.WriteLine("Please select an option between 1 to 4");
                                //If invalid vehicle type is selected code execution will restart from label inner
                                goto inner;
                        }
                    }

                    //Check whether the vehicle is created or null
                    if (vehicle != null) {
                        //Add vehicle to the vehicle pool
                        bool created = admin.AddVehicle(vehicle);

                        //if vehicle is not added to the pool properly and there are parking slots remaining.
                        //Restart code execution from label inner
                        if (!created && RentalServiceDB.allocatedParkingSlots < RentalServiceDB.MAX_PARKING_SLOTS) {
                            goto inner;

                        //if vehicle is not added to the pool properly and there are no parking slots remaining.
                        //Restart code execution from label outer
                        } else if(!created && RentalServiceDB.allocatedParkingSlots >= RentalServiceDB.MAX_PARKING_SLOTS) {
                            option = 0;
                            invalid = true;
                            goto outer;
                        }
                    //if vehicle is not created properly, restart code execution from label inner
                    } else {
                        goto inner;
                    }

                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();                   
                    ShowAdminMenu();

                    break;

                // To delete a vehicle identified by number from the system
                case 2:
                    CreateTopic("Delete a Vehicle", ConsoleColor.Blue);
                    //Get the requested vehicle registration number from admin
                    Console.Write("Please enter the registration number of the vehicle to delete: ");
                    try {
                        string registrationNumber = Console.ReadLine();
                        //Validate whether registration number is null or white spaces before deleting the vehicle
                        if (!string.IsNullOrWhiteSpace(registrationNumber)) admin.DeleteVehicle(registrationNumber);
                    //Any exception will be handled here
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();                    
                    ShowAdminMenu();
                    break;

                // To print a list of the vehicles in the system.
                case 3:
                    CreateTopic("List Vehicles", ConsoleColor.Blue);
                    admin.ListVehicles();
                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();
                    ShowAdminMenu();
                    break;

                // To print a list of the vehicles ordered alphabetically according to the vehicle Make
                case 4:
                    CreateTopic("List Sorted Vehicles", ConsoleColor.Blue);
                    admin.ListOrderedVehicles();
                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();
                    ShowAdminMenu();
                    break;

                // To save the current list of vehicles with their complete related information to a text file
                case 5:
                    CreateTopic("Generate Vehicle Report", ConsoleColor.Blue);
                    //Get the requested file name for the report from admin
                    Console.Write("Please enter a file name for the report: ");

                    try {
                        string fileName = Console.ReadLine();
                        //Validate whether filename is null or white spaces before passing it to generate report
                        if (!string.IsNullOrWhiteSpace(fileName)) admin.GenerateReport(fileName);
                    //Any exception will be handled here
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }  
                    
                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();
                    ShowAdminMenu();
                    break;

                // To access the customer menu
                case 6:
                    ShowCustomerMenu();
                    break;

                //If invalid option is selected admin menu will load again
                default:
                    Console.WriteLine("Please select a number from 1 to 6");
                    Console.WriteLine("\nPlease enter to continue...");
                    ShowAdminMenu();
                    break;
            }
        }

        //Helper method to create a Vehicle of any type
        private static Vehicle? CreateVehicle(int type)
        {
            string registrationNumber = "";
            string make = "";
            string model = "";
            int batteryPercentage = 0;
            double rent = 0.0;

            //Get vehicle details from admin
            Console.WriteLine("\nPlease enter following details of the vehicle");

            bool flag = true;
            //loop will be executed until valid inputs are given
            while(flag) {
                flag = false;
                try {
                    Console.Write("Registration number: ");
                    registrationNumber = Console.ReadLine();
                    Console.Write("Make of the vehicle: ");
                    make = Console.ReadLine();
                    Console.Write("Model of the vehicle: ");
                    model = Console.ReadLine();
                    //Battery percentage is requested only for the Electric Vehicle
                    if (type == 3) {
                        Console.Write("Battery percentage: ");
                        batteryPercentage = int.Parse(Console.ReadLine());
                    }
                    Console.Write("Daily rental price: LKR ");
                    rent = double.Parse(Console.ReadLine());
                //Any exceptions will be handled here    
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message + " Please try again.\n");
                    flag = true;
                }
            }                       

            //An empty schedules list will be added to every vehicle when their instance is created
            List<Schedule> schedules = new List<Schedule>();

            //Validate whether the registration number, make, model is null or white spaces
            if (!string.IsNullOrWhiteSpace(registrationNumber) && !string.IsNullOrWhiteSpace(make) && !string.IsNullOrWhiteSpace(model)) {
                switch (type) {
                    case 1:
                        return new Van(Type.VAN, registrationNumber, make, model, rent, schedules);

                    case 2:
                        return new Car(Type.CAR, registrationNumber, make, model, rent, schedules);

                    case 3:
                        return new ElectricCar(Type.ELECTRIC_CAR, registrationNumber, make, model, rent, schedules, batteryPercentage);

                    case 4:
                        return new Motorbike(Type.MOTORBIKE, registrationNumber, make, model, rent, schedules);
                    
                }

            } else {
                Console.WriteLine("\nPlease enter a valid input.");
            }

            //Null values will be returned from the helper method. Those null values will be handled at the main app logic
            return null;

        }

        //Helper method to display a topic
        private static void CreateTopic(string topic, ConsoleColor color)
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.ForegroundColor = color;            
            Console.WriteLine($"- - - {topic} - - -\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}