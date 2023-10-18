using Hash.DAL.Context;

Thread[] threads = new Thread[4];

for (int i = 0; i < threads.Count(); i++)
{
    Console.WriteLine(i);
    //Console.WriteLine(threads.Count());
    var db = new ApplicationDbContextFactory().CreateDbContext(new string[] { });

    ThreadStart threadStart = delegate
    {
        Hash.Processor.RabbitMQ.HashListener(db);
    };

    threads[i] = new Thread(threadStart);
    threads[i].Start();
}
Console.ReadLine();