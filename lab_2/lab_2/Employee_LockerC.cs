using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Controllers
{
    public class Employee_LockerC : Base
    {


        public Employee_LockerC(string connectionString) : base(connectionString) { }
        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select EmployeeID, LockerId from Employee_Locker";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("EmployeeID: {0}", rdr.GetValue(0));
                    Console.WriteLine("LockerId: {0}", rdr.GetValue(1));
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sqlConnection.Close();
            }


            Console.ReadLine();
        }
        public override void Create()
        {
            string sqlInsert = "Insert into Employee_Locker (EmployeeID, LockerId) VALUES(@EmployeeID, @LockerId)";

            int EmployeeID = 0;
            int LockerId = 0;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Employee_Locker properties:");
                Console.WriteLine("EmployeeID:");
                correct = Int32.TryParse(Console.ReadLine(), out EmployeeID);
                if (correct == false)
                {
                    Console.WriteLine("EmployeeID must be a number!");
                    Console.ReadLine();
                }

                Console.WriteLine("LockerId:");
                correct = Int32.TryParse(Console.ReadLine(), out LockerId);
                if (correct == false)
                {
                    Console.WriteLine("LockerId must be a number!");
                    Console.ReadLine();
                }


                correct = true;
            } while (correct == false);


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("EmployeeID", EmployeeID);
            cmd.Parameters.AddWithValue("LockerId", LockerId);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public override void Delete()
        {
            base.Delete("delete from Employee_Locker where EmployeeID = ");
        }
        public override void Update()
        {
            base.Update("Update Employee_Locker ");
        }

        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into Employee_Locker(EmployeeId, LockerId) (select Employee.EmployeeId, Locker.LockerId"
                + " from Employee, Locker limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}