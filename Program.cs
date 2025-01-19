// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using AutoMapper;
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

            string computerJson = File.ReadAllText("ComputersSnake.json");

            //Uncooment if Automapper is needed
            // Mapper mapper = new Mapper(new MapperConfiguration((cfg)=> {
            //     cfg.CreateMap<ComputersSnake,Computer>()
            //     .ForMember(destination => destination.ComputerId, options =>
            //     options.MapFrom(source => source.computer_id))
            //     .ForMember(destination => destination.Motherboard, options =>
            //     options.MapFrom(source => source.motherboard))
            //     .ForMember(destination => destination.VideoCard, options =>
            //     options.MapFrom(source => source.video_card))
            //     .ForMember(destination => destination.HasWifi, options =>
            //     options.MapFrom(source => source.has_wifi))
            //     .ForMember(destination => destination.HasLTE, options =>
            //     options.MapFrom(source => source.has_lte))
            //     .ForMember(destination => destination.ReleaseDate, options =>
            //     options.MapFrom(source => source.release_date))
            //      .ForMember(destination => destination.CPUCores, options =>
            //     options.MapFrom(source => source.cpu_cores))
            //     .ForMember(destination => destination.Price, options =>
            //     options.MapFrom(source => source.price));
            // }));

            IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computerJson);
            
            if(computersSystem != null){
                // Need the below commendted snippet if we were to use AutoMapper instead of JSON Property Attribute mapping that we did in computer mapping
                // IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);

                // foreach(Computer computer in computerResult){
                //     Console.WriteLine(computer.Motherboard);
                // }
                foreach(Computer computer in computersSystem){
                    Console.WriteLine(computer.Motherboard);
                }
            }
            // if(computers != null)
            // {
            //     foreach(Computer computer in computers)
            //     {

            //         string? releaseDate = computer.ReleaseDate.HasValue ? computer.ReleaseDate.Value.ToString("yyyy-MM-dd") :  null;
            //         string sqlInsert = @"INSERT INTO TutorialAppSchema.Computer(
            //                             Motherboard ,
            //                             HasWifi,
            //                             HasLTE,
            //                             ReleaseDate,
            //                             Price,
            //                             VideoCard
            //                         ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
            //                         + "', '" + computer.HasWifi
            //                         + "','" + computer.HasLTE
            //                         + "','" + releaseDate
            //                         + "','" + computer.Price
            //                         + "','" + EscapeSingleQuote(computer.VideoCard)
            //                         + "')";                
                
            //                     dapper.ExecuteSql(sqlInsert);

            //     }

            // }
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
        }

        static string EscapeSingleQuote(string input)
        {
            string output;

            output = input.Replace("'","''");
            return output;
        }
    }
}