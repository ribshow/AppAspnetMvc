using MongoDB.Driver;

namespace AppAspnetMvc.Models
{
    public class ContextMongoDb
    {
        public static string? ConnectionString {  get; set; }

        public static string? DatabaseName { get; set; }

        private IMongoDatabase _database { get; }

        public ContextMongoDb()
        {
            try
            {
                // criando uma variável que irá receber nossa string de conexão
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

                // instanciando a conexão
                var mongoClient = new MongoClient(settings);
                _database = mongoClient.GetDatabase(DatabaseName);

            }
            catch(Exception e)
            {
                throw new Exception($"Não foi possível estabelecer a conexão, {e.Message}");
            }
        }

        // falando com qual coleção a gente irá trabalhar
        public IMongoCollection<Product> Products => _database.GetCollection<Product>("Product");

    }
}
