using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Controllers
{
    public class ZoneC : Base
    {
        public ZoneC(string connectionString) : base(connectionString) { }

        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select ZoneId, StorageId, Capacity, Fullness from Zone";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("ZoneId: {0}", rdr.GetValue(0));
                    Console.WriteLine("StorageId: {0}", rdr.GetValue(1));
                    Console.WriteLine("Capacity: {0}", rdr.GetValue(2));
                    Console.WriteLine("Fullness: {0}", rdr.GetValue(3));
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
            string sqlInsert = "Insert into Zone (StorageId, Capacity, Fullness) VALUES(@StorageId, @Capacity, @Fullness)";

            int Storage_id = 0;
            string Capacity = null;
            string Fullness = null;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Zone properties:");

                Console.WriteLine("StorageId:");
                correct = Int32.TryParse(Console.ReadLine(), out Storage_id);
                if (correct == false)
                {
                    Console.WriteLine("StorageId must be a number!");
                    Console.ReadLine();
                }


                Console.WriteLine("Capacity:");
                Capacity = Console.ReadLine();
                if (Capacity.Length > 40)
                {
                    correct = false;
                    Console.WriteLine("Length of Capacity > 40. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Fullness:");
                Fullness = Console.ReadLine();
                if (Fullness.Length > 40)
                {
                    correct = false;
                    Console.WriteLine("Length of Fullness > 40. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                correct = true;
            } while (correct == false);


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("StorageId", Storage_id);
            cmd.Parameters.AddWithValue("Capacity", Capacity);
            cmd.Parameters.AddWithValue("Fullness", Fullness);
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
            base.Delete("delete from Zone where ZoneId = ");
        }
        public override void Update()
        {
            base.Update("Update Zone ");
        }
        
        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into Zone(StorageId, Capacity, Fullness) (select storage.StorageId"
                + ", "
                + base.sqlRandomString
                + ", "
                + base.sqlRandomString
                + " from generate_series(1, 1000000), storage limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }
    }
}