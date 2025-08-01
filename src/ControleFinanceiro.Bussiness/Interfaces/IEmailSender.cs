using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleFinanceiro.Bussiness.Interfaces;

public interface IEmailSender
{
    Task SendEmailAsync(string email, string assunto, string corpo);
}
