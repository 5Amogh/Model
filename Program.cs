// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Model.Data;
using Model.Models;

namespace Model
{

    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            DataContextEF entityFramework = new DataContextEF(config);
            // DateTime rightnow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            // Console.WriteLine(rightnow);
            
            Computer myComputer = new Computer()
            {
                Motherboard = "HP",
                VideoCard = "GEFORCE RTX",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 84734.13m
            };

            string sqlInsert = @"INSERT INTO TutorialAppSchema.Computer(
                 Motherboard ,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
             ) VALUES ('" + myComputer.Motherboard
              + "', '" + myComputer.HasWifi
              + "','" + myComputer.HasLTE
              + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
               + "','" + myComputer.Price
                + "','" + myComputer.VideoCard
             + "')";

            // Console.WriteLine(sqlInsert);
        //    int result = dapper.ExecuteSqlWithRowCount(sqlInsert);
            bool result = dapper.ExecuteSql(sqlInsert);
        //    Console.WriteLine(result);
            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();
            string sqlSelect = @"SELECT 
                Computer.Motherboard ,
                Computer.HasWifi,
                Computer.HasLTE,
                Computer.ReleaseDate,
                Computer.Price,
                Computer.VideoCard
                FROM TutorialAppSchema.Computer";

            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            foreach(Computer singleComputer in computers){
                Console.WriteLine(singleComputer.Motherboard);
            }

            IEnumerable<Computer>? computersEf = entityFramework.Computer?.ToList<Computer>();
            if(computersEf != null)
            {
                foreach(Computer singleComputer in computersEf){
                    Console.WriteLine(" From EF Query'"+singleComputer.Motherboard +"'");
                }
            }
 

            // myComputer.HasLTE = true;
            // Console.WriteLine(myComputer.Motherboard);
            // Console.WriteLine(myComputer.CPUCores);
            // Console.WriteLine(myComputer.ReleaseDate);
            // Console.WriteLine(myComputer.HasLTE);
        }
    }
}