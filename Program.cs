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
            //Overwrites the text previously created
            File.WriteAllText("log.txt","\n" + sqlInsert + "\n"); 

            //Appends the text to the previous log
            using StreamWriter openFile = new ("log.txt",append:true);

            openFile.WriteLine("\n" + sqlInsert + "\n");

            openFile.Close();

            string logFileText = File.ReadAllText("log.txt");

            Console.WriteLine(logFileText);
        }
    }
}