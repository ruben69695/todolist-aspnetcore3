namespace Data.Helpers.Mongo
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}