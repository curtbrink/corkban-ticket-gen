using Corkban.TicketGen.Configuration;
using Corkban.TicketGen.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Corkban.TicketGen.Infrastructure.Sqlite;

public sealed class SqliteContext(IOptions<DataConfiguration> dataConfig) : DbContext
{
    public DbSet<TemplateEntity> Templates { get; set; }
    public DbSet<TemplateComponentEntity> TemplateComponents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
        {
            return;
        }
        
        // make sure the file exists first
        var filePath = dataConfig.Value.FilePath;
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }
            
        var connectionString = $"DataSource={filePath}";
        optionsBuilder.UseSqlite(connectionString);
    }
}