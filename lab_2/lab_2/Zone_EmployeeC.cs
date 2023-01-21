using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Controllers
{
    public class Zone_EmployeeC : Base
    {
        public Zone_EmployeeC(string connectionString) : base(connectionString) { }
        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select ZoneId, EmployeeId from Zone_Employee";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("ZoneId: {0}", rdr.GetValue(0));
                    Console.WriteLine("EmployeeId: {0}", rdr.GetValue(1));
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
            string sqlInsert = "Insert into Zone_Employee (ZoneId, EmployeeId) VALUES(@ZoneId, @EmployeeId)";

            int ZoneId = 0;
            int EmployeeId = 0;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Zone_Employee properties:");
                Console.WriteLine("ZoneId:");
                correct = Int32.TryParse(Console.ReadLine(), out ZoneId);
                if (correct == false)
                {
                    Console.WriteLine("ZoneId must be a number!");
                    Console.ReadLine();
                }

                Console.WriteLine("EmployeeId:");
                correct = Int32.TryParse(Console.ReadLine(), out EmployeeId);
                if (correct == false)
                {
                    Console.WriteLine("EmployeeId must be a number!");
                    Console.ReadLine();
                }


                correct = true;
            } while (correct == false);


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("ZoneId", ZoneId);
            cmd.Parameters.AddWithValue("EmployeeId", EmployeeId);
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
            base.Delete("delete from Zone_Employee where EmployeeId = ");
        }
        public override void Update()
        {
            base.Update("Update Zone_Employee ");
        }

        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into Zone_Employee(ZoneId, EmployeeId) (select Zone.ZoneId, Employee.EmployeeId"
                + " from Zone, Employee limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}
