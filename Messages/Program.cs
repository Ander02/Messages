using RabbitMQ.Client;
using System;
using Utility;

namespace Sender
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var userName = Util.ReadLineWithMessage("Digite seu nome: ");

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

                    var exchangeName = Util.ReadLineWithMessage("Informe a exchange que você gostaria de entrar: ");
                    modelChanel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

                    while (true)
                    {
                        var content = Util.ReadLineWithMessage("> ");

                        if (content.Equals("exit")) break;

                        var message = new Message
                        {
                            Content = content,
                            Date = DateTime.Now,
                            UserName = userName
                        };

                        modelChanel.BasicPublish(exchangeName, "", properties, message.ToJson().GetBytes());
                    }
                }
            }
        }
    }
}
