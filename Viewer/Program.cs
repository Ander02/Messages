using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;

namespace Viewer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem vindo!");
            Console.WriteLine("Veja as mensagens que estão sendo enviadas");

            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            {
                using (var modelChanel = connection.CreateModel())
                {
                    var exchangeName = ReadLineWithMessage("Informe a exchange que você gostaria de ouvir mensagens: ");
                    modelChanel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

                    var queueName = modelChanel.QueueDeclare().QueueName;
                    modelChanel.QueueBind(queue: queueName,
                                          exchange: exchangeName,
                                          routingKey: "");

                    var consumer = new EventingBasicConsumer(modelChanel);
                    consumer.Received += (model, ev) =>
                    {
                        var message = ev.Body.GetString();
                        Console.WriteLine("-> " + message);
                    };

                    modelChanel.BasicConsume(queue: queueName,
                                             autoAck: true,
                                             consumer: consumer);

                    ReadLineWithMessage("Aperte qualquer tecla para sair\n");
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
