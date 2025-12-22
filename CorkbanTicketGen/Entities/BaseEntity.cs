namespace CorkbanTicketGen.Entities;

public class BaseEntity
{
    public required string Id { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }
    
    public DateTime DeletedAt { get; set; }
}