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
```
## Execute o Docker
Abra o CMD na pasta que foi Clonada
```
cd ConectaAtende
```
Docker compose up --build
---
A API estará disponível em:
```
http://localhost:5005/swagger/index.html
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



