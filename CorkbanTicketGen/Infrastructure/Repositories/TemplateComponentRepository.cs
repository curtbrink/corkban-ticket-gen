using CorkbanTicketGen.Entities;
using CorkbanTicketGen.Infrastructure.Sqlite;

namespace CorkbanTicketGen.Infrastructure.Repositories;

public class TemplateComponentRepository(SqliteContext dbContext) : BaseRepository<TemplateComponentEntity>(dbContext)
{
    
}