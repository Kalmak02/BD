using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace BD.Controllers
{
    public class LockerC : Base
    {
        public LockerC(string connectionString) : base(connectionString) { }
        public override void Read(string whereCondition)
        {
            Console.Clear();

            sqlConnection.Open();

            string sqlSelect = "select LockerId, Condition from Locker";

            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine("LockerId: {0}", rdr.GetValue(0));
                    Console.WriteLine("Condition: {0}", rdr.GetValue(1));
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
            Console.Clear();
            Console.WriteLine("This function is not available in this table");
            System.Threading.Thread.Sleep(3000);
        }

        public override void Delete()
        {
            base.Delete("delete from Locker where LockerId = ");
        }
        public override void Update()
        {
            base.Update("Update Locker ");
        }

        public override void Generate()
        {
            Console.Clear();
            Console.WriteLine("This function is not available in this table");
            System.Threading.Thread.Sleep(3000);
        }
    }
}