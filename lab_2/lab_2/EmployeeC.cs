using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Controllers
{
    public class EmployeeC : Base
    {
        public EmployeeC(string connectionString) : base(connectionString) { }
        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select EmployeeId, Full_name, Salary from Employee";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("EmployeeId: {0}", rdr.GetValue(0));
                    Console.WriteLine("Full_name: {0}", rdr.GetValue(1));
                    Console.WriteLine("Salary: {0}", rdr.GetValue(2));
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
            string sqlInsert = "Insert into Employee(Full_name, Salary) VALUES(@Full_name, @Salary)";

            string Full_name = null;
            int Salary = 0;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter patient properties:");
                Console.WriteLine("Full_name:");
                Full_name = Console.ReadLine();
                if (Full_name.Length > 40)
                {
                    correct = false;
                    Console.WriteLine("Length of Full_name > 40. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Salary:");
                correct = Int32.TryParse(Console.ReadLine(), out Salary);
                if (correct == false)
                {
                    Console.WriteLine("Salary must be a number!");
                    Console.ReadLine();
                }
                correct = true;
            } while (correct == false);


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("Full_name", Full_name);
            cmd.Parameters.AddWithValue("Salary", Salary);
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
            base.Delete("delete from Employee where EmployeeID = ");
        }
        public override void Update()
        {
            base.Update("Update Employee ");
        }

        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into Employee(Full_name, Salary) (select "
                + base.sqlRandomString
                + ", "
                + base.sqlRandomString
                + " from generate_series(1, 1000000)  limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}