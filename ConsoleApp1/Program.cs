using System;
using System.Reflection.Emit;
using System.Threading;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp1
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }
        
        public static IConfiguration BuildConfig()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return config.Build();
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Random rnd = new Random();
            Configuration = BuildConfig();

            var team = Configuration["Testing:FootballClubID"];

            var sqlString = Configuration["ConnectionStrings:DefaultDatabase"];
            SqlConnection con = new SqlConnection(sqlString);
            int ran = rnd.Next(100);
            SqlCommand cmd = new SqlCommand($"INSERT INTO TestingTable VALUES('Hello {ran}')", con);
            con.Open();
            cmd.ExecuteReader();
            con.Close();
            Console.WriteLine(team);
            Console.WriteLine("Done");
            
            if (args.Length == 0)
            {
                // Display message to user to provide parameters.
                System.Console.WriteLine("Please enter parameter values.");
                Thread.Sleep(50000);

            }
            else
            {
                Console.WriteLine("Enter here bla bla bla");
                // Loop through array to list args parameters.
                for (int i = 0; i < args.Length; i++)
                {
                    Console.Write(args[i] + Environment.NewLine);

                }
                // Keep the console window open after the program has run.
                // Console.Read();
                Thread.Sleep(50000);
            }
        }
    }
}
