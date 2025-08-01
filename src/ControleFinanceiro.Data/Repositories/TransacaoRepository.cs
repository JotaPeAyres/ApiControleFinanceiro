using ControleFinanceiro.Bussiness.Interfaces;
using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Data.Repositories;

public class TransacaoRepository : Repository<Transacao>, ITransacaoRepository
{
    public TransacaoRepository(ApiDbContext context) : base(context) { }

    public async Task<IEnumerable<Transacao>> ObterPorPeriodo(DateTime? dataInicio, DateTime? dataFim)
    {
        var query = DbSet.AsQueryable();

        if (dataInicio.HasValue)
            query = query.Where(t => t.Data >= dataInicio.Value.Date);

        if (dataFim.HasValue)
            query = query.Where(t => t.Data <= dataFim.Value.Date.AddDays(1).AddTicks(-1));

        return await query
            .OrderBy(t => t.Data)
            .ThenBy(t => t.Descricao)
            .AsNoTracking()
            .ToListAsync();
    }
}
