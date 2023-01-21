using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Controllers
{
    public class StuffC : Base
    {
        public StuffC(string connectionString) : base(connectionString) { }

        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select StuffId, ZoneId, Type, Name, Price from Stuff";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("StuffId: {0}", rdr.GetValue(0));
                    Console.WriteLine("ZoneId: {0}", rdr.GetValue(1));
                    Console.WriteLine("Type: {0}", rdr.GetValue(2));
                    Console.WriteLine("Name: {0}", rdr.GetValue(3));
                    Console.WriteLine("Price: {0}", rdr.GetValue(4));
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
            string sqlInsert = "Insert into Stuff (ZoneId, Type, Name, Price) VALUES(@ZoneId, @Type, @Name, @Price)";

            int Zone_id = 0;
            string Type = null;
            string Name = null;
            int Price = 0;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Stuff properties:");

                Console.WriteLine("ZoneId:");
                correct = Int32.TryParse(Console.ReadLine(), out Zone_id);
                if (correct == false)
                {
                    Console.WriteLine("ZoneId must be a number!");
                    Console.ReadLine();
                }


                Console.WriteLine("Type:");
                Type = Console.ReadLine();
                if (Type.Length > 40)
                {
                    correct = false;
                    Console.WriteLine("Length of Type > 40. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Name:");
                Name = Console.ReadLine();
                if (Name.Length > 40)
                {
                    correct = false;
                    Console.WriteLine("Length of Name > 40. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Price:");
                correct = Int32.TryParse(Console.ReadLine(), out Price);
                if (correct == false)
                {
                    Console.WriteLine("Price must be a number!");
                    Console.ReadLine();
                }

                correct = true;
            } while (correct == false);


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("ZoneId", Zone_id);
            cmd.Parameters.AddWithValue("Type", Type);
            cmd.Parameters.AddWithValue("Name", Name);
            cmd.Parameters.AddWithValue("Price", Price);
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
            base.Delete("delete from Stuff where StuffId = ");
        }
        public override void Update()
        {
            base.Update("Update Stuff ");
        }

        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into Stuff(ZoneId, Type, Name, Price) (select zone.ZoneId"
                + ", "
                + base.sqlRandomString
                + ", "
                + base.sqlRandomString
                + ", "
                + base.sqlRandomInteger
                + " from generate_series(1, 1000000), zone limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}