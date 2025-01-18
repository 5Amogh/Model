// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Model.Data;
using Model.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Model
{

    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            DataContextDapper dapper = new DataContextDapper(config);

            string computerJson = File.ReadAllText("Computers.json");

            JsonSerializerOptions jsonOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            // By using Sytem.text.json
            // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(computerJson, jsonOptions);
            //  By using Newtonsoft.json package  
            IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computerJson);


            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string computersCopyNewtonSoft = JsonConvert.SerializeObject(computers,settings);

            File.WriteAllText("computersCopyNewtonSoft.json",computersCopyNewtonSoft);

            string computersCopySystem = System.Text.Json.JsonSerializer.Serialize(computers,jsonOptions);

            File.WriteAllText("computersCopySystem.json",computersCopySystem);

            if(computers != null)
            {
                foreach(Computer computer in computers)
                {

                    string? releaseDate = computer.ReleaseDate.HasValue ? computer.ReleaseDate.Value.ToString("yyyy-MM-dd") :  null;
                    string sqlInsert = @"INSERT INTO TutorialAppSchema.Computer(
                                        Motherboard ,
                                        HasWifi,
                                        HasLTE,
                                        ReleaseDate,
                                        Price,
                                        VideoCard
                                    ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                                    + "', '" + computer.HasWifi
                                    + "','" + computer.HasLTE
                                    + "','" + releaseDate
                                    + "','" + computer.Price
                                    + "','" + EscapeSingleQuote(computer.VideoCard)
                                    + "')";                
                
                                dapper.ExecuteSql(sqlInsert);

                }

            }
            // Computer myComputer = new Computer()
            // {
            //     Motherboard = "HP",
            //     VideoCard = "GEFORCE RTX",
            //     HasWifi = true,
            //     HasLTE = false,
            //     ReleaseDate = DateTime.Now,
            //     Price = 84734.13m
            // };

            // string sqlInsert = @"INSERT INTO TutorialAppSchema.Computer(
            //     Motherboard ,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            //  ) VALUES ('" + myComputer.Motherboard
            //   + "', '" + myComputer.HasWifi
            //   + "','" + myComputer.HasLTE
            //   + "','" + myComputer.ReleaseDate.ToString("yyyy-MM-dd")
            //    + "','" + myComputer.Price
            //     + "','" + myComputer.VideoCard
            //  + "')";
            //Overwrites the text previously created
            // File.WriteAllText("log.txt","\n" + sqlInsert + "\n"); 

            // //Appends the text to the previous log
            // using StreamWriter openFile = new ("log.txt",append:true);

            // openFile.WriteLine("\n" + sqlInsert + "\n");

            // openFile.Close();

            // string logFileText = File.ReadAllText("log.txt");

            // Console.WriteLine(logFileText);
        }

        static string EscapeSingleQuote(string input)
        {
            string output;

            output = input.Replace("'","''");
            return output;
        }
    }
}