using Microsoft.EntityFrameworkCore;
using MinimalApi.Db;
using Testcontainers.MySql;
using Xunit;

namespace Test.Integration.Connection;

public class DatabaseConnection : IAsyncLifetime
{
    private readonly MySqlContainer _container = new MySqlBuilder()
        .WithImage("mysql:8.0")
        .WithPassword("password")
        .WithDatabase("test_db")
        .Build();

    public DbContexto Context { get; set; }
    
    
    public virtual async Task InitializeAsync()
    {
        await _container.StartAsync();
        
        var options = new DbContextOptionsBuilder<DbContexto>()
            .UseMySql(_container.GetConnectionString(), 
                ServerVersion.AutoDetect(_container.GetConnectionString()))
            .Options;
        
        Context = new DbContexto(options);
        await Context.Database.MigrateAsync();
    }
    
    public virtual async Task DisposeAsync()
    {
        await Context.DisposeAsync();
        await _container.DisposeAsync();
    }
}