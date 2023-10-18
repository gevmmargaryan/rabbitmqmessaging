using Hash.Model.ViewModels;
using RabbitMQ.Client;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Hash.Helper
{
    public class RabbitMQ
    {
        public static bool GenerateHashes(int count)
        {
            Task.Run(() =>
            {
                try
                {
                    using var sha1 = SHA1.Create();

                    for (var i = 0; i < count; i++)
                    {
                        var hash = new HashModel
                        {
                            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(i % 10)),
                            Sha1 = Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(Random.Shared.Next(-20, 55).ToString())))
                        };

                        SendToQueue(hash);
                    }
                }
                catch (Exception ex)
                {
                    //Log
                }
            });

            return true;
        }

        private static void SendToQueue(HashModel hashModel)
        {
            try
            {
                //Host name should be configurable
                var factory = new ConnectionFactory { HostName = "localhost" };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(Hash.Model.Constants.RabbitMQ.Queue, false, false, true, null);

                string message = JsonSerializer.Serialize(hashModel);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(string.Empty, Hash.Model.Constants.RabbitMQ.Queue, null, body);
            }
            catch(Exception ex)
            {
                //Log
            }
        }
    }
}