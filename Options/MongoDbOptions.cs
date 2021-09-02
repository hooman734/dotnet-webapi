namespace Catalog.Options
{
    class MongoDbOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}