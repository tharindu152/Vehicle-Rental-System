using System.Security.Cryptography;

namespace VehicleRentalSoftwareSystem20231753
{
    internal class WestminsterRentalVehicle
    {
        private static Customer customer = new Customer();
        private static Admin admin = new Admin();   
        static void Main(string[] args)
        {
            Car toyota = new Car(Type.CAR, "C-001", "Toyota", "Axio", 250.50, new List<Schedule>());
            Van petrol = new Van(Type.VAN, "C-002", "Nissan", "Petrol", 2750.50, new List<Schedule>());
            ElectricCar leaf = new ElectricCar(Type.ELECTRIC_CAR, "C-003", "Nissan", "Leaf", 4550.50, new List<Schedule>());
            Motorbike ct100 = new Motorbike(Type.MOTORBIKE, "C-004", "Bajaj", "CT100", 3250.50, new List<Schedule>());
            Car wagonr = new Car(Type.CAR, "C-005", "Suzuki", "wagonR", 1250.50, new List<Schedule>());
            Van highroof = new Van(Type.VAN, "C-006", "Toyota", "High Roof", 2950.50, new List<Schedule>());
            ElectricCar tesla = new ElectricCar(Type.ELECTRIC_CAR, "C-007", "Tesla", "tesla", 8250.50, new List<Schedule>());
            Motorbike kawasaki = new Motorbike(Type.MOTORBIKE, "C-008", "Suzuki", "Kawasaki", 2750.50, new List<Schedule>());

            VehicleDB.vehiclePool.Add("C-001", toyota);
            VehicleDB.vehiclePool.Add("C-002", petrol);
            VehicleDB.vehiclePool.Add("C-003", leaf);
            VehicleDB.vehiclePool.Add("C-004", ct100);
            VehicleDB.vehiclePool.Add("C-005", wagonr);
            VehicleDB.vehiclePool.Add("C-006", highroof);
            VehicleDB.vehiclePool.Add("C-007", tesla);
            VehicleDB.vehiclePool.Add("C-008", kawasaki);

            leaf.GetSchedules().Add(new Schedule( DateTime.Parse("03/24/2024"), DateTime.Parse("03/26/2024")));
            leaf.GetSchedules().Add(new Schedule(DateTime.Parse("03/20/2024"), DateTime.Parse("03/22/2024")));
            ct100.GetSchedules().Add(new Schedule(DateTime.Parse("03/04/2024"), DateTime.Parse("03/06/2024")));
            wagonr.GetSchedules().Add(new Schedule(DateTime.Parse("04/24/2024"), DateTime.Parse("04/26/2024")));
            wagonr.GetSchedules().Add(new Schedule(DateTime.Parse("04/04/2024"), DateTime.Parse("04/06/2024")));
            tesla.GetSchedules().Add(new Schedule(DateTime.Parse("03/14/2024"), DateTime.Parse("03/16/2024")));

            ShowCustomerMenu();
            /*Console.WriteLine(DateTime.Today);*/
            /*Console.Write("Please enter the pick up date. Eg- 01/09/2024 : ");
            DateTime pickUpDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine(pickUpDate.ToShortDateString());*/


        }

        public static void ShowCustomerMenu()
        {
            DateTime pickUpDate;
            DateTime dropOffDate;            
            Schedule schedule;

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
                switch (option) {
                    
                    case 1:
                                                
                        Console.Write("Please enter the pick up date. format:- MM/dd/yyyy : ");
                        pickUpDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the drop off date. format:- MM/dd/yyyy : ");
                        dropOffDate = DateTime.Parse(Console.ReadLine());
                        
                        if (pickUpDate < dropOffDate && !(DateTime.Now > pickUpDate)) {
                            schedule = new Schedule(pickUpDate, dropOffDate);
                            Console.Write("Vehicle types available: \n" +
                                                  "1) Van\n" +
                                                  "2) Car\n" +
                                                  "3) Electric Car\n" +
                                                  "4) Motorbike\n\n" +
                                                  "Please select an option between 1 to 4: ");

                            int type = Convert.ToInt32(Console.ReadLine());
                            Type vehicleType = 0;
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
                                    Console.WriteLine("Please select the correct vehicle type..");
                                    break;
                            }
                            customer.ListAvailableVehicles(schedule, vehicleType);
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                        } else if (DateTime.Now > pickUpDate) {
                            Console.WriteLine("Your pickup date should not be a past date");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                        } else if (pickUpDate > dropOffDate) {
                            Console.WriteLine("Your pickup date should be earlier than drop off date");
                            Console.WriteLine("\nPlease enter to continue...");
                            Console.ReadLine();
                        }                         
                        
                        ShowCustomerMenu();                       
                        break;
                    
                    case 2:
                        CreateTopic("Rent a Vehicle", ConsoleColor.Green);
                        Console.Write("Please enter the registration number of the vehicle: ");
                        string registrationNumber = Console.ReadLine();
                        Console.Write("Please enter the pick up date. format:- MM/dd/yyyy: ");
                        pickUpDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the drop off date. format:- MM/dd/yyyy: ");
                        dropOffDate = DateTime.Parse(Console.ReadLine());

                        if (pickUpDate < dropOffDate && !(DateTime.Now > pickUpDate)) {
                            schedule = new Schedule(pickUpDate, dropOffDate);
                            if (!string.IsNullOrWhiteSpace(registrationNumber)) {
                                bool isRented = customer.AddReservation(registrationNumber, schedule);
                                if (isRented) {
                                    Console.WriteLine("The vehicle has been scheduled..");
                                    
                                } else {
                                    Console.WriteLine("");
                                }
                            } else {
                                Console.WriteLine("Please enter a valid registration number");                                
                            }
                        } else if (DateTime.Now > pickUpDate) {
                            Console.WriteLine("Your pickup date should not be a past date");                            
                        } else if (pickUpDate > dropOffDate) {
                            Console.WriteLine("Your pickup date should be earlier than drop off date");                            
                        }

                        Console.WriteLine("\nPlease enter to continue...");
                        Console.ReadLine();
                        ShowCustomerMenu();
                        break;
                    
                    case 3:
                        CreateTopic("Change Reservation", ConsoleColor.Green);
                        Console.Write("Please enter the registration number of the vehicle: ");
                        string regNumber = Console.ReadLine();
                        Console.Write("Please enter the old pick up date. format:- MM/dd/yyyy: ");
                        DateTime pickUpDateOld = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the old drop off date. format:- MM/dd/yyyy: ");
                        DateTime dropOffDateOld = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the new pick up date. format:- MM/dd/yyyy: ");
                        DateTime pickUpDateNew = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the new drop off date. format:- MM/dd/yyyy: ");
                        DateTime dropOffDateNew = DateTime.Parse(Console.ReadLine());                

                        if (pickUpDateOld < dropOffDateOld && pickUpDateNew < dropOffDateNew && !(DateTime.Now > pickUpDateNew)) {
                            Schedule scheduleOld = new Schedule(pickUpDateOld, dropOffDateOld);
                            Schedule scheduleNew = new Schedule(pickUpDateNew, dropOffDateNew);
                            if (!string.IsNullOrWhiteSpace(regNumber)) {
                                bool isChanged = customer.ChangeReservation(regNumber, scheduleOld, scheduleNew);
                                if (isChanged) {
                                    Console.WriteLine("The schedule has been updated..");
                                } else {
                                    Console.WriteLine("");
                                }
                            } else {
                                Console.WriteLine("Please enter a valid registration number");
                            }
                        } else if (DateTime.Now > pickUpDateOld && DateTime.Now > pickUpDateNew) {
                            Console.WriteLine("Your pickup dates should not be a past dates");
                        } else if (pickUpDateOld > dropOffDateOld && pickUpDateNew > dropOffDateNew) {
                            Console.WriteLine("Your pickup dates should be earlier than respective drop off dates");
                        }

                        Console.WriteLine("\nPlease enter to continue...");
                        Console.ReadLine();
                        ShowCustomerMenu();
                        break;

                    case 4:
                        CreateTopic("Delete Reservation", ConsoleColor.Green);
                        Console.Write("Please enter the registration number of the vehicle: ");
                        registrationNumber = Console.ReadLine();
                        Console.Write("Please enter the pick up date. format:- MM/dd/yyyy: ");
                        pickUpDate = DateTime.Parse(Console.ReadLine());
                        Console.Write("Please enter the drop off date. format:- MM/dd/yyyy: ");
                        dropOffDate = DateTime.Parse(Console.ReadLine());                    

                        if (pickUpDate < dropOffDate && !(DateTime.Now > pickUpDate)) {
                            schedule = new Schedule(pickUpDate, dropOffDate);
                            if (!string.IsNullOrWhiteSpace(registrationNumber)) {
                                bool isDeleted = customer.DeleteReservation(registrationNumber, schedule);
                                if (isDeleted) {
                                    Console.WriteLine("The schedule has been deleted..");
                                } else {
                                    Console.WriteLine("");
                                }
                            } else {
                                Console.WriteLine("Please enter a valid registration number");
                            }
                        } else if (DateTime.Now > pickUpDate) {
                            Console.WriteLine("Your pickup dates should not be a past dates");
                        } else if (pickUpDate > dropOffDate) {
                            Console.WriteLine("Your pickup dates should be earlier than respective drop off dates");
                        }

                        Console.WriteLine("\nPlease enter to continue...");
                        Console.ReadLine();
                        ShowCustomerMenu();
                        break;

                    case 5:
                        ShowAdminMenu();
                        break;

                    default:
                        Console.WriteLine("Please select a number from 1 to 5");
                        Console.WriteLine("\nPlease enter to continue...");
                        Console.ReadLine();
                        break;
                }
            } catch (Exception e) {
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
            while (invalid) {
                invalid = false;
                try {
                    CreateTopic("Admin Menu");
                    Console.Write("Options: \n" +
                                      "1) Add a vehicle\n" +
                                      "2) Delete a vehicle\n" +
                                      "3) List vehicles\n" +
                                      "4) List sorted vehicles\n" +
                                      "5) Generate vehicle report\n" +
                                      "6) Go to customer menu\n\n" +
                                      "Please select an option between 1 to 6: ");
                    
                    option = Convert.ToInt32(Console.ReadLine());

                } catch (Exception ex) {
                    Console.WriteLine(ex.Message + " Please try again.");
                    invalid = true;
                }
            }
            
            switch (option) {
                case 1:
                    invalid = true;
                    int type = 0;
                    Vehicle vehicle = null;
                
                    inner:
                    while (invalid) {
                        invalid = false;
                        try {
                            if (VehicleDB.allocatedParkingSlots >= VehicleDB.MAX_PARKING_SLOTS) {
                                Console.WriteLine("\nCan not add the vehicles. All parking slots are occupied");
                                Console.WriteLine("\nPlease enter to continue...");
                                Console.ReadLine();
                                ShowAdminMenu();
                            } else {
                                CreateTopic("Add Vehicle");
                                Console.Write("Vehicle Types: \n" +
                                                  "1) Van\n" +
                                                  "2) Car\n" +
                                                  "3) Electric Car\n" +
                                                  "4) Motorbike\n\n" +
                                                  "Please select an option between 1 to 4: ");

                                type = Convert.ToInt32(Console.ReadLine());
                            }                            
                        } catch(Exception ex) {
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
                                goto inner;
                        }
                    }

                    if (vehicle != null) {
                        bool created = admin.AddVehicle(vehicle);
                        if (!created && VehicleDB.allocatedParkingSlots < VehicleDB.MAX_PARKING_SLOTS) {
                            goto inner;
                        }else if(!created && VehicleDB.allocatedParkingSlots >= VehicleDB.MAX_PARKING_SLOTS) {
                            option = 0;
                            invalid = true;
                            goto outer;
                        }

                    } else {
                        goto inner;
                    }

                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();                   
                    ShowAdminMenu();

                    break;

                // getting user input ( registration number) to delete the vehicle
                case 2:
                    CreateTopic("Delete Vehicle");
                    Console.Write("Please enter the registration number of the vehicle to delete: ");
                    try {
                        string registrationNumber = Console.ReadLine();
                        if (registrationNumber != null) admin.DeleteVehicle(registrationNumber);
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();                    
                    ShowAdminMenu();
                    break;

                // this method just list the vehicle by looping the Dictionary
                case 3:
                    CreateTopic("List Vehicles");
                    admin.ListVehicles();
                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();
                    ShowAdminMenu();
                    break;

                // list vehicles according to the make in ascending order
                case 4:
                    CreateTopic("List Ordered Vehicles");
                    admin.ListOrderedVehicles();
                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();
                    ShowAdminMenu();
                    break;

                // generation a report. as user input this method first gets file name from the user
                case 5:
                    CreateTopic("Generate Report");
                    Console.Write("Please enter a file name for the report: ");

                    try {
                        string fileName = Console.ReadLine();
                        if(fileName != null) admin.GenerateReport(fileName);
                    } catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }  
                    
                    Console.WriteLine("\nPlease enter to continue...");
                    Console.ReadLine();
                    ShowAdminMenu();
                    break;

                // go back to the customer menu
                case 6:
                    ShowCustomerMenu();
                    break;

                // if the given user input is wrong to select function. this default method will run
                default:
                    Console.WriteLine("Please select a number from 1 to 6");
                    Console.WriteLine("\nPlease enter to continue...");
                    ShowAdminMenu();
                    break;
            }
        }

        private static Vehicle? CreateVehicle(int type)
        {
            string registrationNumber = "";
            string make = "";
            string model = "";
            double rent = 0.0;

            Console.WriteLine("\nPlease enter following details of the vehicle");

            bool flag = true;
            while(flag) {
                flag = false;
                try {
                    Console.Write("Registration number: ");
                    registrationNumber = Console.ReadLine();
                    Console.Write("Make of the vehicle: ");
                    make = Console.ReadLine();
                    Console.Write("Model of the vehicle: ");
                    model = Console.ReadLine();
                    Console.Write("Daily rental price: LKR ");
                    rent = double.Parse(Console.ReadLine());
                } catch (Exception ex) {
                    Console.WriteLine(ex.Message + " Please try again.\n");
                    flag = true;
                }
            }                       

            List<Schedule> schedules = new List<Schedule>();

            if (!string.IsNullOrWhiteSpace(registrationNumber) && !string.IsNullOrWhiteSpace(make) && !string.IsNullOrWhiteSpace(model)) {
                switch (type) {
                    case 1:
                        return new Van(Type.VAN, registrationNumber, make, model, rent, schedules);

                    case 2:
                        return new Car(Type.CAR, registrationNumber, make, model, rent, schedules);

                    case 3:
                        return new ElectricCar(Type.ELECTRIC_CAR, registrationNumber, make, model, rent, schedules);

                    case 4:
                        return new Motorbike(Type.MOTORBIKE, registrationNumber, make, model, rent, schedules);
                    
                }

            } else {
                Console.WriteLine("\nPlease enter a valid input.");
            }

            return null;

        }

        private static void CreateTopic(string topic)
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Title = $"Westminster Rental Vehicle";
            Console.WriteLine($"- - - {topic} - - -\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void CreateTopic(string topic, ConsoleColor color)
        {
            Console.Clear();
            Console.WriteLine("\x1b[3J");
            Console.ForegroundColor = color;
            Console.Title = $"Westminster Rental Vehicle";
            Console.WriteLine($"- - - {topic} - - -\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}