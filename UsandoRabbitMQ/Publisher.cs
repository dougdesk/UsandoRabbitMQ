using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsandoRabbitMQ
{
    public static class Publisher
    {
        public static void PublisherIN()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "saudacao_1",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

                    var message = "Bem vindo ao RabbitMQ do Douglas";

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                        routingKey: "saudacao_1",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine($"[x] Enviada: {message}");
                }

            }            
        }
    }    
}
