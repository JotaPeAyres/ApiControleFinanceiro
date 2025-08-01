using ControleFinanceiro.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControleFinanceiro.Data.Tests.Helpers;

public abstract class DataTestBase : IDisposable
{
    protected readonly ApiDbContext Context;
    protected readonly ServiceProvider ServiceProvider;
    private bool _disposed = false;

    protected DataTestBase()
    {
        var services = new ServiceCollection();
        
        // Configure in-memory database without automatic seeding
        services.AddDbContext<ApiDbContext>(options =>
        {
            options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                   .EnableSensitiveDataLogging()
                   .ConfigureWarnings(x => x.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning));
        });

        ServiceProvider = services.BuildServiceProvider();
        Context = ServiceProvider.GetRequiredService<ApiDbContext>();
        
        // Ensure clean database for each test - but skip seeding
        Context.Database.EnsureCreated();
    }

    protected void ClearDatabase()
    {
        if (Context.Transacoes.Any())
        {
            Context.Transacoes.RemoveRange(Context.Transacoes);
            Context.SaveChanges();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            Context?.Dispose();
            ServiceProvider?.Dispose();
            _disposed = true;
        }
    }
}
