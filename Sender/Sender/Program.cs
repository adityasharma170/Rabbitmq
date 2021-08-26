using RabbitMQ.Client;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Sender
{
    class Program
    {
        static void Main(string[] args)
        {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                channel.ExchangeDeclare(exchange: "direct_logs" , type: "direct");
                var routingKey = "info" ;
                string file = @"C:\Users\adityas\Desktop\Conversion.txt";
                string[] lines = File.ReadAllLines(file);
                    foreach (string ln in lines)
                    {
                    var body = Encoding.UTF8.GetBytes(ln);
                    channel.BasicPublish(exchange: "direct_logs",
                                      routingKey: routingKey,
                                      basicProperties: null,
                                      body: body);
                    Console.WriteLine(ln);
                        Thread.Sleep(1000);
                    }

            }
        }
    }
}
