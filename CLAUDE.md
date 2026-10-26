# CLAUDE.md

Instruções gerais para o Claude Code na pasta `projetos`, que reúne todos os projetos de estudo. Regras detalhadas ficam em `.claude/rules/` (as de arquitetura carregam só quando se trabalha no projeto correspondente), procedimentos em `.claude/skills/` e subagentes em `.claude/agents/`.

## O que é este repositório

Uma coleção de **projetos de estudo independentes** (quase todos .NET, um Java), não uma aplicação única. Não existe solution raiz nem build compartilhado: cada pasta tem sua própria solution, runtime e convenções. Sempre aponte comandos para o projeto específico e nunca introduza dependências entre projetos.

| Pasta | Stack | Build | Testes |
|---|---|---|---|
| `api-moto-manager` | .NET 9, MongoDB, Redis, RabbitMQ, MinIO | `MotoManager.sln` | xUnit + Moq (unit), xUnit + Testcontainers (integração) |
| `meu-livro-receitas-api-netcore` | .NET 10, EF Core, FluentValidation | `MyRecipeBook.sln` | nenhum |
| `microservices-cqrs-kafka-api-netcore` | .NET 6, Kafka, MongoDB (event store), SQL Server (leitura) | `microservices-cqrs-kafka-api-netcore.sln` | nenhum |
| `microservices-hexagonal-arch` | .NET 8, EF Core + SQL Server | `HotelBooking.sln` | NUnit (quase todos placeholders) |
| `rpg-netcore` | .NET 6 | `RPG.sln` | xUnit + FluentAssertions (`Rpg.Domain.Test`) |
| `produtosapi` | Java 21, Spring Boot, JPA + H2 em memória, Lombok | `pom.xml` (Maven wrapper) | JUnit (Spring Boot test) |

Identificadores, comentários e mensagens frequentemente estão em português (`Heroi`, `Dado`, `Produto`). A grafia "Motocycle" no MotoManager é intencional e usada de forma consistente — não corrija.

## Comandos

.NET (a partir da pasta do projeto):
```bash
dotnet build <Solution>.sln
dotnet test <Solution>.sln
dotnet test <ProjetoDeTeste>.csproj --filter "FullyQualifiedName~Classe.Metodo"   # um teste
```

MotoManager:
```bash
cd api-moto-manager
docker compose up -d mongo redis minio rabbitmq   # só infraestrutura
docker compose up --build                        # API (porta 5000) + workers + infra
dotnet test MotoManager.Tests/MotoManager.UnitTests.csproj
dotnet test MotoManager.IntegrationTests/MotoManager.IntegrationTests.csproj   # exige Docker (Testcontainers)
```

CQRS/Kafka: `docker compose up -d` em `microservices-cqrs-kafka-api-netcore` sobe só Zookeeper + Kafka; MongoDB e SQL Server precisam ser providos à parte.

Migrations do hexagonal (projeto `Adapters.SQL`, API como startup):
```bash
cd microservices-hexagonal-arch
dotnet ef migrations add <Nome> --project Adapters.SQL --startup-project BookingService.Api
dotnet ef database update --project Adapters.SQL --startup-project BookingService.Api
```

Java (`produtosapi`):
```bash
./mvnw spring-boot:run          # console H2 em /h2-console, dados de data.sql
./mvnw test
./mvnw test -Dtest=Classe#metodo
```

## Fluxo de desenvolvimento

- Antes de alterar, leia o código vizinho e siga o padrão do **próprio projeto** (cada um tem estilo diferente — ver `.claude/rules/code-style.md`).
- Mudanças pequenas e focadas; não refatore além do pedido nem "harmonize" projetos entre si.
- Depois de alterar, rode build e os testes do projeto afetado. Se o projeto não tem testes, diga isso em vez de afirmar que está verificado.
- Commits e branches só quando pedido.

Procedimentos disponíveis: `/revisao-codigo`, `/implementar-feature`, `/analisar-bug`.

## Configuração

Vários projetos leem configuração **direto de variáveis de ambiente**, não do `appsettings.json` (que só tem logging). Variável ausente gera falha de null/parse na inicialização, não um erro claro.

- **MotoManager**: `MotoManager.Domain/Constants/AppConstants.cs` — `MONGO_DATABASE_URL`, `MONGO_DATABASE_NAME`, `REDIS_URL`, `RABBITMQ_HOST/PORT/USER/PASSWORD/EXCHANGENAME/ROUTINGKEY`, `MINIO_ENDPOINT/ACCESSKEY/SECRETKEY/BUCKETNAME`. Valores funcionais em `api-moto-manager/docker-compose.yml`.
- **CQRS lado Cmd**: `MONGO_DB_CONNECTION_STRING`, `DATABASE`, `COLLECTION`, `BOOTSTRAP_SERVER`, `KAFKA_TOPIC`. **Lado Query**: `SQL_CONNECTIONSTRING`.
- **Hexagonal**: connection string do SQL Server fixa em `BookingService.Api/Program.cs` (`127.0.0.1,1433`).

## Armadilhas

- **O `.gitignore` raiz contém `*.test`** (template de Go). No Windows (case-insensitive) isso ignora `rpg-netcore/Rpg.Domain.Test/` inteiro, embora esteja no `RPG.sln`. Arquivos ali não aparecem no `git status`; use `git add -f` ou corrija a regra se precisarem ser commitados.
- O `.gitignore` raiz também ignora `bin/`, `build/`, `target/`, `out/` em qualquer nível, e `.env`.
