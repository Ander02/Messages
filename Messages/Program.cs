using RabbitMQ.Client;
using System;

namespace Sender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo!");
            Console.WriteLine("Digite suas mensagens ou exit para sair");

            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var modelChanel = connection.CreateModel())
                {
                    var queueName = "server_queue";
                    modelChanel.QueueDeclare(queue: queueName,
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);
                    var properties = modelChanel.CreateBasicProperties();

                    var exchangeName = ReadLineWithMessage("Informe a exchange que você gostaria de entrar: ");
                    modelChanel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

                    while (true)
                    {
                        var message = ReadLineWithMessage("> ");

                        if (message.Equals("exit")) break;

                        modelChanel.BasicPublish(exchangeName, "", properties, message.GetBytes());
                    }
                }
            }
        }
        public static string ReadLineWithMessage(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}

