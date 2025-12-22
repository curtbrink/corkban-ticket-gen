namespace Corkban.TicketGen.Configuration;

public class DataConfiguration
{
    public const string SectionName = "Data";
    
    public required string FilePath { get; init; }
}