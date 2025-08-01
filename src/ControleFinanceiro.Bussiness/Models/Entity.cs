namespace ControleFinanceiro.Bussiness.Models;

public class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public bool Ativo { get; set; } = true;
    public DateTime Inclusao { get; protected set; } = DateTime.UtcNow;
    public DateTime? Alteracao { get; protected set; }

    protected void SetUpdatedAt()
    {
        Alteracao = DateTime.UtcNow;
    }
}
