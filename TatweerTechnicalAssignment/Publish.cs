using System;
using System.Text;
using RabbitMQ.Client;

namespace TatweerTechnicalAssignment
{
    class Publish
    {
        public static void AlertForSpeed(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = "amqp://admin:admin@tatweer.fortiddns.com:15672/vhost";
            
            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "Alerts", type: "fanout");
                channel.QueueDeclare(queue: "Alerts");
                channel.QueueBind(queue: "Alerts", exchange: "Alerts", routingKey: "");

                string message = Traffic.GetMessage(args);
                byte[] body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "Alerts", 
                                    routingKey: "Alerts", 
                                    basicProperties: null, 
                                    body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        public static void AlertForAccident(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = "amqp://admin:admin@tatweer.fortiddns.com:15672/vhost";

            using (IConnection connection = factory.CreateConnection())
            using (IModel channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "Accidents", type: "fanout");
                channel.QueueDeclare(queue: "Accidents");
                channel.QueueBind(queue: "Accidents", exchange: "Accidents", routingKey: "");

                string message = Traffic.GetMessage(args);
                byte[] body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "Accidents",
                                    routingKey: "Accidents",
                                    basicProperties: null,
                                    body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

    }
}
