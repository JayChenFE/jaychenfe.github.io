using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("app.json", true, true).Build();

            Console.WriteLine(configuration["factoryName"]);
        }
    }
}
