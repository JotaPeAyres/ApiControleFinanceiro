using ControleFinanceiro.Bussiness.Models;
using ControleFinanceiro.Data.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;

namespace ControleFinanceiro.Data.Context;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Transacao> Transacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transacao>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(t => t.Valor)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
            entity.Property(t => t.Tipo)
                .IsRequired();
            entity.Property(t => t.Data)
                .IsRequired();
            entity.Property(t => t.Ativo)
                .IsRequired()
                .HasDefaultValue(true);
            entity.Property(t => t.Inclusao)
                .IsRequired();
            entity.Property(t => t.Alteracao)
                .IsRequired(false);
        });
        
        base.OnModelCreating(modelBuilder);
        
        // Seed initial data
        modelBuilder.SeedData();
    }
}
