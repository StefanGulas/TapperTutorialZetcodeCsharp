using Dapper;
using System;
using System.Data.SqlClient;
using System.Linq;

namespace TapperTutorialZetcodeCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var cs = @"Server=.\SQLLOCALDB;Database=SampleDB;Trusted_Connection=True;";

            using var con = new SqlConnection(cs);
            con.Open();

            var version = con.ExecuteScalar<string>("SELECT @@VERSION");

            Console.WriteLine(version);
            Console.ReadKey();

            var cars = con.Query<Car>("SELECT * FROM cars").ToList();

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }


            int noOfRows = con.Execute("DELETE FROM dbo.[cars] WHERE [id] >= 5 AND [id] <= 7");
            Console.WriteLine("'DELETE' table affected rows: " + noOfRows);

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }

            int noOfRowsInsert = con.Execute("INSERT INTO dbo.[cars] VALUES ('BMW', 35400), ('VW',12000)");
            Console.WriteLine("'INSERT' table affected rows: " + noOfRowsInsert);

            var cars2 = con.Query<Car>("SELECT * FROM cars WHERE Id=@Id", 
                new { id = 3 });


            foreach (var car in cars2)
            {
                Console.WriteLine(car);
            }

            var continueQuery = true;

            while (continueQuery)
            {

                Console.WriteLine("What SQL Query?");
                var cwQuery = Console.ReadLine();

                var cars3 = con.Query<Car>(cwQuery).ToList();

                foreach (var car in cars3)
                {
                    Console.WriteLine(car);
                }

                Console.WriteLine("Continue (1) for yes");

                if ((Console.ReadLine().ToString()) != "1")
                {
                    continueQuery = false;
                }
            }





        }

        class Car
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public override string ToString()
            {
                return $"{Id} {Name} {Price}";
            }
        }




    }
}
