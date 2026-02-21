# ConectaAtende

Reescrita de Sistema Legado — ConectaAtende API (.NET 8)

---

## Pré-requisitos

Antes de começar, certifique-se de ter instalado:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) (ou SQL Server Express)
- [Git](https://git-scm.com/)
- Um editor como [Visual Studio](https://visualstudio.microsoft.com/)

---

## Clonando o repositório

```bash
git clone https://github.com/i4nzz/ConectaAtende.API.RicardoFugencio.git
cd ConectaAtende
```

---


> Caso não tenha o arquivo de exemplo, crie o `appsettings.json` manualmente com a estrutura abaixo.

**. Preencha as configurações:**

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=ConectaAtende;User Id=seu_usuario;Password=sua_senha;TrustServerCertificate=True"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

---

## Aplicando as migrations

```bash
dotnet ef database update --project ConectaAtende.Infra --startup-project ConectaAtende.API
```

> Caso não tenha o EF Tools instalado:
> ```bash
> dotnet tool install --global dotnet-ef
> ```

---

## Executando o projeto

```bash
dotnet run --project ConectaAtende.API
```

A API estará disponível em:

```
http://localhost:5000
https://localhost:5001
```

A documentação Swagger pode ser acessada em:

```
https://localhost:5001/swagger
```

---

## Estrutura do projeto

```
ConectaAtende/
├── ConectaAtende.API          # Camada de apresentação (Controllers, configuração)
├── ConectaAtende.Application  # Casos de uso, DTOs, interfaces e serviços
├── ConectaAtende.Domain       # Entidades e regras de negócio
├── ConectaAtende.Infra        # Acesso a dados, repositórios, migrations
├── ConectaAtende.Benchmarks   # Testes de performance com BenchmarkDotNet
└── ConectaAtende.sln
```

---

## Executando os testes

```bash
dotnet test
```

---

