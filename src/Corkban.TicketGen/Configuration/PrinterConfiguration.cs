namespace Corkban.TicketGen.Configuration;

public class PrinterConfiguration
{
    public const string SectionName = "Printer";
    
    public required string SecretKey { get; init; }
}