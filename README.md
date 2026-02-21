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

## Configurando o ambiente

O arquivo `appsettings.json` não é versionado por conter informações sensíveis. Você precisará criá-lo manualmente.

**1. Acesse a pasta da API:**

```bash
cd ConectaAtende.API
```

**2. Crie o arquivo `appsettings.json` com base no exemplo disponível:**

```bash
cp appsettings.Example.json appsettings.json
```

> Caso não tenha o arquivo de exemplo, crie o `appsettings.json` manualmente com a estrutura abaixo.

**3. Preencha as configurações:**

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

## Restaurando as dependências

Na raiz da solução, execute:

```bash
dotnet restore
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

## Contribuindo

1. Crie uma branch a partir da `main`:
   ```bash
   git checkout -b feature/minha-feature
   ```
2. Faça suas alterações e commit:
   ```bash
   git commit -m "feat: descrição da feature"
   ```
3. Abra um Pull Request para revisão.
