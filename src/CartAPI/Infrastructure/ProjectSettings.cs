namespace CartAPI.Infrastructure;

public class ProjectSettings : IProjectSettings
{
    public DataBaseSettings DataBaseSettings { get; set; }

    public string ApiGatewayUrl { get; set; }
}

public class DataBaseSettings
{
    public string CartCollectionName { get; set; }
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}

public interface IProjectSettings
{
    public DataBaseSettings DataBaseSettings { get; set; }
    
    public string ApiGatewayUrl { get; set; }
}