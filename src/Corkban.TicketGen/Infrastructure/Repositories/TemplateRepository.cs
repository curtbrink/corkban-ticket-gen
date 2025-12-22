using Corkban.TicketGen.Entities;
using Corkban.TicketGen.Infrastructure.Sqlite;

namespace Corkban.TicketGen.Infrastructure.Repositories;

public class TemplateRepository(SqliteContext dbContext) : BaseRepository<TemplateEntity>(dbContext)
{
    
}