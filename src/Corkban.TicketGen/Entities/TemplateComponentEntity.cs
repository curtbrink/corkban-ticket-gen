using Corkban.TicketGen.Enums;

namespace Corkban.TicketGen.Entities;

public class TemplateComponentEntity : BaseEntity
{
    public required string TemplateId { get; set; }
    
    public ComponentLocation Location { get; set; }
    
    public ComponentType Type { get; set; }
    
    public string? TextData { get; set; }
    
    public string? TextStyle { get; set; }
    
    public byte[]? ImageData { get; set; }
}