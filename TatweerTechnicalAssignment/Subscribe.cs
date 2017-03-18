using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Web;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace TatweerTechnicalAssignment
{
    class Subscribe
    {
        public static void Main(string[] args)
        {
            ConnectionFactory Factory = new ConnectionFactory();
            Factory.Uri = "amqp://admin:admin@tatweer.fortiddns.com:15672/vhost";

            using (IConnection Connection = Factory.CreateConnection())
            {
                using (IModel Channel = Connection.CreateModel())
                {
                    Channel.ExchangeDeclare(exchange: "Counts", type: "fanout");
                    Channel.QueueDeclare(queue: "Counts");
                    Channel.QueueBind(queue: "Counts",
                                      exchange: "Count",
                                      routingKey: "");
                    
                    EventingBasicConsumer Consumer = new EventingBasicConsumer(Channel);
                    Consumer.Received += (model, ea) =>
                    {
                        var Body = ea.Body;
                        var Message = Encoding.UTF8.GetString(Body);

                        Vehicle Vehicle_ = JsonConvert.DeserializeObject<Vehicle>(Message);

                        Traffic.UpdateTracker(Vehicle_);
                        Traffic.CheckForSpeed(Vehicle_);
                        Traffic.CheckForAccident();

                        Channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    };
                    
                    Channel.BasicConsume(queue: "Counts",
                                         noAck: false,
                                         consumer: Consumer);
                }
            }
        }
    }

}
