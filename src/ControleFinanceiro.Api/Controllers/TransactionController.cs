using ControleFinanceiro.Bussiness.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace ControleFinanceiro.Api.Controllers;

public class TransactionController : MainController
{
    public TransactionController(INotifier notifier) : base(notifier) 
    {
        
    }
}
