using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Controllers
{
    public class StorageC : Base
    {
        public StorageC(string connectionString) : base(connectionString) { }
        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select StorageId, Area, Owner, Status from Storage";

            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("StorageId: {0}", rdr.GetValue(0));
                    Console.WriteLine("Area: {0}", rdr.GetValue(1));
                    Console.WriteLine("Owner: {0}", rdr.GetValue(2));
                    Console.WriteLine("Status: {0}", rdr.GetValue(3));
                    Console.WriteLine();
                }
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
            string sqlInsert = "Insert into Storage(Area, Owner) VALUES(@Area, @Owner)";

            string Area = null;
            string Owner = null;

            bool correct = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Enter Storage properties:");
                Console.WriteLine("Area:");
                Area = Console.ReadLine();
                if (Area.Length > 40)
                {
                    correct = false;
                    Console.WriteLine("Length of Area > 10. It is wrong.");
                    Console.ReadLine();
                    continue;
                }

                Console.WriteLine("Owner:");
                Owner = Console.ReadLine();
                if (Owner.Length > 40)
                {
                    correct = false;
                    Console.WriteLine("Length of Owner > 40. It is wrong.");
                    Console.ReadLine();
                    continue;
                }


                correct = true;
            } while (correct == false);


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("Area", Area);
            cmd.Parameters.AddWithValue("Owner", Owner);
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
            base.Delete("delete from Storage where StorageId = ");
        }
        public override void Update()
        {
            base.Update("Update Storage ");
        }
        public override void Find()
        {
            base.Find();
        }

        public override void Generate()
        {
            Console.WriteLine("How many records do you want?");
            bool correct = false;
            int recordsAmount;

            correct = Int32.TryParse(Console.ReadLine(), out recordsAmount);

            string sqlGenerate = "insert into Storage(Area, Owner) (select "
                + base.sqlRandomString
                + ", "
                + base.sqlRandomString
                + " from generate_series(1, 1000000)  limit(" + recordsAmount + "))";
            base.Generate(sqlGenerate);
        }


    }
}