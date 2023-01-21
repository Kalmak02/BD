using BD.Controllers;
using System;

namespace BD
{
    class Prog
    {
        static void Main(string[] args)
        {
            String connectionString = "Host=localhost;Username=postgres;Password=super_KEKL;Database=Maintenance of the warehouse";

            int table = 0;
            int action = 0;
            do
            {
                table = FirstMenu();
                if (table == 0)
                {
                    return;
                }

                Base controller = null;


                switch (table)
                {
                    case 1:
                        action = SecondMenu("Storage");
                        controller = new StorageC(connectionString);
                        break;
                    case 2:
                        action = SecondMenu("Zone");
                        controller = new ZoneC(connectionString);
                        break;
                    case 3:
                        action = SecondMenu("Stuff");
                        controller = new StuffC(connectionString);
                        break;
                    case 4:
                        action = SecondMenu("Zone_Employee");
                        controller = new Zone_EmployeeC(connectionString);
                        break;
                    case 5:
                        action = SecondMenu("Employee");
                        controller = new EmployeeC(connectionString);
                        break;
                    case 6:
                        action = SecondMenu("Employee_Locker");
                        controller = new Employee_LockerC(connectionString);
                        break;
                    case 7:
                        action = SecondMenu("Locker");
                        controller = new LockerC(connectionString);
                        break;
                }


                switch (action)
                {
                    case 1:
                        controller.Create();
                        break;
                    case 2:
                        controller.Read();
                        break;
                    case 3:
                        controller.Update();
                        break;
                    case 4:
                        controller.Delete();
                        break;
                    case 5:
                        controller.Find();
                        break;
                    case 6:
                        controller.Generate();
                        break;
                }



            } while (true);
        }

        public static int FirstMenu()
        {
            var choice = 0;
            var correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose table you want to work with or 0 to exit::");
                Console.WriteLine("1.Storage");
                Console.WriteLine("2.Zone");
                Console.WriteLine("3.Stuff");
                Console.WriteLine("4.Zone_Employee");
                Console.WriteLine("5.Employee");
                Console.WriteLine("6.Employee_Locker");
                Console.WriteLine("7.Locker");
                correct = Int32.TryParse(Console.ReadLine(), out choice);
            } while (choice < 0 || choice > 7 || correct == false);


            return choice;
        }

        public static int SecondMenu(string tableToChange)
        {
            var choice = 0;
            var correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose what you want to do with '" + tableToChange + "' table or 0 to exit:");
                Console.WriteLine("1.Create");
                Console.WriteLine("2.Read");
                Console.WriteLine("3.Update");
                Console.WriteLine("4.Delete");
                Console.WriteLine("5.Find");
                Console.WriteLine("6.Generate");
                correct = Int32.TryParse(Console.ReadLine(), out choice);
            } while (choice < 0 || choice > 6 || correct == false);


            return choice;
        }
    }
}
