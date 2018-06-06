using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using Utility;

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
                    var exchangeName = Util.ReadLineWithMessage("Informe a exchange que você gostaria de ouvir mensagens: ");
                    modelChanel.ExchangeDeclare(exchange: exchangeName, type: "fanout");

                    var queueName = modelChanel.QueueDeclare().QueueName;
                    modelChanel.QueueBind(queue: queueName,
                                          exchange: exchangeName,
                                          routingKey: "");

                    var consumer = new EventingBasicConsumer(modelChanel);
                    consumer.Received += (model, ev) =>
                    {
                        var messageJson = ev.Body.GetString();

                        var message = new Message(messageJson);

                        Console.WriteLine("-> " + message.ToString());
                    };

                    modelChanel.BasicConsume(queue: queueName,
                                             autoAck: true,
                                             consumer: consumer);

                    Util.ReadLineWithMessage("Para sair, pressione qualquer tecla.\n");
                }
            }
        }
    }
}
