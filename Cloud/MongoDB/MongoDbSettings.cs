namespace MongoDB;

public class MongoDbSettings //klasse for at hente settings fra appsettings.json
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}