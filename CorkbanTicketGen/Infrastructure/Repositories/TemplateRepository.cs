using CorkbanTicketGen.Entities;
using CorkbanTicketGen.Infrastructure.Sqlite;

namespace CorkbanTicketGen.Infrastructure.Repositories;

public class TemplateRepository(SqliteContext dbContext) : BaseRepository<TemplateEntity>(dbContext)
{
    
}