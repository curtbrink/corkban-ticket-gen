using Corkban.TicketGen.Entities;
using Corkban.TicketGen.Infrastructure.Sqlite;

namespace Corkban.TicketGen.Infrastructure.Repositories;

public class TemplateComponentRepository(SqliteContext dbContext) : BaseRepository<TemplateComponentEntity>(dbContext)
{
    
}