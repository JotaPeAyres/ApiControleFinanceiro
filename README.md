# 💰 API Controle Financeiro

> Sistema completo de controle financeiro pessoal com API REST em .NET 8, notificações automáticas e dashboard interativo.

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-8.0-green.svg)](https://docs.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-red.svg)](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

## 📋 Índice

- [Sobre o Projeto](#-sobre-o-projeto)
- [Funcionalidades](#-funcionalidades)
- [Arquitetura](#-arquitetura)
- [Tecnologias](#-tecnologias)
- [Pré-requisitos](#-pré-requisitos)
- [Instalação](#-instalação)
- [Configuração](#-configuração)
- [Execução](#-execução)
- [API Endpoints](#-api-endpoints)
- [Testes](#-testes)
- [Estrutura do Projeto](#-estrutura-do-projeto)
- [Contribuição](#-contribuição)
- [Licença](#-licença)

## 🎯 Sobre o Projeto

O **API Controle Financeiro** é um sistema robusto para gerenciamento de finanças pessoais, desenvolvido com .NET 8 e seguindo as melhores práticas de arquitetura limpa. O sistema oferece:

- **API REST** completa para gerenciamento de transações financeiras
- **Notificações automáticas** por email quando o saldo fica negativo
- **Cálculos automáticos** de fluxo de caixa e relatórios financeiros
- **Arquitetura em camadas** com separação clara de responsabilidades
- **Testes automatizados** com cobertura essencial das funcionalidades

## ✨ Funcionalidades

### 💳 Gestão de Transações
- ✅ Cadastro de receitas e despesas
- ✅ Consulta por período específico
- ✅ Atualização e exclusão de transações
- ✅ Validação automática de dados

### 📊 Fluxo de Caixa
- ✅ Cálculo automático do saldo atual
- ✅ Relatório consolidado por período
- ✅ Histórico de saldos diários
- ✅ Identificação de dias com saldo negativo

### 🔔 Notificações
- ✅ Monitoramento automático do saldo
- ✅ Alertas por email quando saldo negativo
- ✅ Serviço em background (BackgroundService)
- ✅ Configuração flexível de SMTP

### 🌐 API e Integração
- ✅ API REST com Swagger/OpenAPI
- ✅ CORS configurado para frontend
- ✅ Respostas padronizadas com tratamento de erro
- ✅ Suporte a múltiplos ambientes

## 🏗️ Arquitetura

O projeto segue os princípios da **Arquitetura Limpa** e **DDD (Domain-Driven Design)**:

```
┌─────────────────────────────────────────────────────────────┐
│                    🌐 API Layer                             │
│  Controllers, Middlewares, Extensions, Configuration        │
├─────────────────────────────────────────────────────────────┤
│                   💼 Business Layer                         │
│     Services, Models, ViewModels, Validations              │
├─────────────────────────────────────────────────────────────┤
│                    💾 Data Layer                            │
│    Repositories, DbContext, Migrations, Configurations     │
└─────────────────────────────────────────────────────────────┘
```

### Camadas do Sistema:

- **🌐 ControleFinanceiro.Api**: Controllers, configurações, middlewares
- **💼 ControleFinanceiro.Business**: Lógica de negócio, serviços, modelos
- **💾 ControleFinanceiro.Data**: Acesso a dados, repositórios, Entity Framework

## 🛠️ Tecnologias

### Backend
- **[.NET 8.0](https://dotnet.microsoft.com/)** - Framework principal
- **[ASP.NET Core](https://docs.microsoft.com/aspnet/core/)** - Web API
- **[Entity Framework Core 8.0](https://docs.microsoft.com/ef/)** - ORM
- **[SQL Server LocalDB](https://docs.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)** - Banco de dados
- **[AutoMapper](https://automapper.org/)** - Mapeamento de objetos
- **[FluentValidation](https://fluentvalidation.net/)** - Validações
- **[Serilog](https://serilog.net/)** - Logging estruturado

### Testes
- **[xUnit](https://xunit.net/)** - Framework de testes
- **[Moq](https://github.com/moq/moq4)** - Mocking
- **[FluentAssertions](https://fluentassertions.com/)** - Assertions expressivas

### Ferramentas
- **[Swagger/OpenAPI](https://swagger.io/)** - Documentação da API
- **[Docker](https://www.docker.com/)** - Containerização
- **[Visual Studio 2022](https://visualstudio.microsoft.com/)** - IDE

## 📋 Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- **[.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)** ou superior
- **[SQL Server LocalDB](https://docs.microsoft.com/sql/database-engine/configure-windows/sql-server-express-localdb)** (incluído no Visual Studio)
- **[Visual Studio 2022](https://visualstudio.microsoft.com/)** ou **[VS Code](https://code.visualstudio.com/)**
- **[Git](https://git-scm.com/)** para controle de versão

### Verificar Instalação
```bash
# Verificar versão do .NET
dotnet --version

# Verificar SQL Server LocalDB
sqllocaldb info
```

## 🚀 Instalação

### 1. Clonar o Repositório
```bash
git clone https://github.com/seu-usuario/ApiControleFinanceiro.git
cd ApiControleFinanceiro
```

### 2. Restaurar Dependências
```bash
# Restaurar pacotes NuGet
dotnet restore

# Ou usando Visual Studio: Build > Restore NuGet Packages
```

### 3. Configurar Banco de Dados
```bash
# Navegar para o projeto API
cd src/ControleFinanceiro.Api

# Aplicar migrações (automático na primeira execução)
dotnet ef database update
```

## ⚙️ Configuração

### 1. Configurar Banco de Dados

Edite o arquivo `src/ControleFinanceiro.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ControleFinanceiroDb;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 2. Configurar Email (Opcional)

Para receber notificações de saldo negativo:

```json
{
  "EmailSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "Username": "seu-email@gmail.com",
    "Password": "sua-senha-de-app",
    "FromEmail": "seu-email@gmail.com",
    "FromName": "Sistema de Controle Financeiro",
    "EnableSsl": true
  },
  "EmailEnvio": "destinatario@email.com"
}
```

> **⚠️ Segurança**: Use **User Secrets** para credenciais em produção:
> ```bash
> dotnet user-secrets set "EmailSettings:Password" "sua-senha"
> ```

### 3. Configurar CORS

Para integração com frontend:

```json
{
  "Cors": {
    "Origins": [
      "http://localhost:3000",
      "http://localhost:9000",
      "https://seu-frontend.com"
    ]
  }
}
```

## ▶️ Execução

### Opção 1: Visual Studio
1. Abra o arquivo `ControleFinanceiro.sln`
2. Defina `ControleFinanceiro.Api` como projeto de inicialização
3. Pressione `F5` ou clique em "Start"

### Opção 2: Linha de Comando
```bash
# Navegar para o projeto API
cd src/ControleFinanceiro.Api

# Executar a aplicação
dotnet run

# Ou em modo watch (reinicia automaticamente)
dotnet watch run
```

### Opção 3: Docker
```bash
# Construir a imagem
docker build -t controle-financeiro-api .

# Executar o container
docker run -p 8080:80 controle-financeiro-api
```

### 🌐 Acessar a Aplicação

Após iniciar, a API estará disponível em:
- **HTTP**: http://localhost:5000
- **HTTPS**: https://localhost:5001
- **Swagger UI**: https://localhost:5001/swagger

## 📚 API Endpoints

### 💳 Transações

| Método | Endpoint | Descrição |
|--------|----------|----------|
| `GET` | `/api/transacoes` | Lista todas as transações |
| `GET` | `/api/transacoes/{id}` | Busca transação por ID |
| `POST` | `/api/transacoes` | Cria nova transação |
| `DELETE` | `/api/transacoes/{id}` | Remove transação |

### 📊 Fluxo de Caixa

| Método | Endpoint | Descrição |
|--------|----------|----------|
| `GET` | `/api/fluxocaixa/saldo-atual` | Saldo atual |
| `GET` | `/api/fluxocaixa/saldo-diario` | Saldos diários por período |
| `GET` | `/api/fluxocaixa/resumo-financeiro` | Resumo consolidado |

### 📝 Exemplos de Uso

#### Criar Transação
```bash
curl -X POST "https://localhost:5001/api/transacoes" \
  -H "Content-Type: application/json" \
  -d '{
    "descricao": "Salário",
    "tipo": 1,
    "valor": 5000.00,
    "data": "2024-01-15T00:00:00"
  }'
```

#### Consultar Saldo Atual
```bash
curl -X GET "https://localhost:5001/api/fluxocaixa/saldo-atual"
```

## 🧪 Testes

O projeto inclui **20 testes essenciais** cobrindo as funcionalidades críticas:

### Executar Todos os Testes
```bash
# Executar todos os testes
dotnet test

# Com relatório detalhado
dotnet test --logger "console;verbosity=detailed"

# Com cobertura de código
dotnet test --collect:"XPlat Code Coverage"
```

### Executar Testes por Projeto
```bash
# Testes de negócio
cd tests/ControleFinanceiro.Bussiness.Tests
dotnet test

# Testes da API
cd tests/ControleFinanceiro.Api.Tests
dotnet test
```

### 📊 Cobertura de Testes

- **✅ Models**: Criação e validação de entidades
- **✅ Services**: Lógica de negócio (CRUD + cálculos)
- **✅ Controllers**: Endpoints da API
- **✅ Integration**: Comunicação entre componentes

## 📁 Estrutura do Projeto

```
ApiControleFinanceiro/
├── 📁 src/
│   ├── 📁 ControleFinanceiro.Api/          # 🌐 Camada de API
│   │   ├── 📁 Controllers/                 # Controllers REST
│   │   ├── 📁 Configuration/               # Configurações DI
│   │   ├── 📁 Extensions/                  # Extensões e helpers
│   │   ├── 📄 Program.cs                   # Ponto de entrada
│   │   └── 📄 appsettings.json            # Configurações
│   │
│   ├── 📁 ControleFinanceiro.Bussiness/    # 💼 Camada de Negócio
│   │   ├── 📁 Models/                      # Entidades de domínio
│   │   ├── 📁 Services/                    # Lógica de negócio
│   │   ├── 📁 ViewModels/                  # DTOs e ViewModels
│   │   ├── 📁 Interfaces/                  # Contratos
│   │   └── 📁 Notifications/               # Sistema de notificações
│   │
│   └── 📁 ControleFinanceiro.Data/         # 💾 Camada de Dados
│       ├── 📁 Context/                     # DbContext
│       ├── 📁 Repositories/                # Repositórios
│       ├── 📁 Mappings/                    # Configurações EF
│       └── 📁 Migrations/                  # Migrações do banco
│
├── 📁 tests/
│   ├── 📁 ControleFinanceiro.Api.Tests/    # 🧪 Testes da API
│   └── 📁 ControleFinanceiro.Bussiness.Tests/ # 🧪 Testes de negócio
│
├── 📄 ControleFinanceiro.sln               # Solution file
├── 📄 README.md                            # Este arquivo
├── 📄 .gitignore                           # Arquivos ignorados
└── 📄 Dockerfile                           # Container Docker
```

## 🔧 Principais Componentes

### 🎯 Services (Serviços)
- **TransacaoService**: CRUD de transações com validações
- **FluxoCaixaService**: Cálculos financeiros e relatórios
- **EmailSender**: Envio de emails de notificação
- **FluxoCaixaNotificationService**: Monitoramento em background

### 📊 Models (Modelos)
- **Transacao**: Entidade principal (receitas/despesas)
- **Entity**: Classe base com auditoria
- **ViewModels**: DTOs para API (Add, Update, Saldo, etc.)

### 🗄️ Repositories (Repositórios)
- **Repository<T>**: Repositório genérico base
- **TransacaoRepository**: Operações específicas de transações

## 🚀 Próximos Passos

### Melhorias Planejadas
- [ ] **Cache Redis** para otimização de performance
- [ ] **Health Checks** para monitoramento
- [ ] **Logs estruturados** com Serilog
- [ ] **Autenticação JWT** para segurança
- [ ] **Paginação** nas consultas
- [ ] **Relatórios PDF** exportáveis
- [ ] **Dashboard** em tempo real
- [ ] **Deploy automatizado** com GitHub Actions

## 🤝 Contribuição

Contribuições são bem-vindas! Para contribuir:

1. **Fork** o projeto
2. Crie uma **branch** para sua feature (`git checkout -b feature/AmazingFeature`)
3. **Commit** suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. **Push** para a branch (`git push origin feature/AmazingFeature`)
5. Abra um **Pull Request**

### 📋 Diretrizes
- Siga os padrões de código existentes
- Adicione testes para novas funcionalidades
- Atualize a documentação quando necessário
- Use commits semânticos (feat, fix, docs, etc.)

## 📞 Suporte

Se você encontrar problemas ou tiver dúvidas:

- 🐛 **Issues**: [GitHub Issues](https://github.com/seu-usuario/ApiControleFinanceiro/issues)
- 📧 **Email**: seu-email@exemplo.com
- 💬 **Discussões**: [GitHub Discussions](https://github.com/seu-usuario/ApiControleFinanceiro/discussions)

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

<div align="center">

**⭐ Se este projeto foi útil para você, considere dar uma estrela!**

*Desenvolvido com ❤️ usando .NET 8*

</div>