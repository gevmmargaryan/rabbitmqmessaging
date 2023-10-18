using Hash.DAL.Context;
using Hash.Model.ViewModels;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Hash.Processor
{
    public class RabbitMQ
    {
        public static void HashListener(ApplicationDbContext db)
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(Hash.Model.Constants.RabbitMQ.Queue, false, false, true, null);

                //this is for test, to understand that 4 threads are waiting
                Console.WriteLine("Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    var hashInsertModel = JsonSerializer.Deserialize<HashInsertModel>(message);

                    if (hashInsertModel != null)
                    {
                        Console.WriteLine(message);
                        db.Hash.AddAsync(new DAL.Entities.Hash
                        {
                            Date = hashInsertModel.Date,
                            Sha1 = hashInsertModel.Sha1
                        });
                        db.SaveChangesAsync();
                    }

                };
                channel.BasicConsume(Hash.Model.Constants.RabbitMQ.Queue, true, consumer);

                Console.ReadLine();
            }
            catch(Exception ex)
            {
                //Log
            }
        }
    }
}
