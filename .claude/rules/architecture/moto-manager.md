---
paths:
  - "api-moto-manager/**"
---

# Arquitetura — api-moto-manager

Arquitetura limpa em camadas: `Api` → `Application` → `Domain` ← `Infrastructure`. `Domain` não referencia nenhuma outra camada.

- **Injeção de dependência** fica toda em `MotoManager.IoC`, como extensões de `IServiceCollection` (`AddApplicationServices`, `AddInfrastructure`, `AddCache`, `AddAutoMapper`, `AddWorkerDependencyInjection`). Novos serviços/repositórios são registrados ali, nunca no `Program.cs`.
- **Result pattern**: serviços da Application retornam `Result<T>` (`Application/ResultBase/Results.cs`) em vez de lançar exceção. Controllers mapeiam `Success` → 2xx e falha → `BadRequest(result.Message)`. Mensagens de erro vêm de `Application/Resources/Resource.resx` — não escreva strings de erro inline.
- **Correlation ID**: `ApiMiddleware` garante o header `X-Correlation-Id` e captura exceções não tratadas. Serviços obtêm correlation/user id via `IHeaderService` e logam por `Shared/Logger/BaseLogger<T>`, passando `correlationId`, `userId` e `methodName`.
- **Validação** mora nos value objects do Domain (`CnhValueObject`, `CnpjValueObject`, `PlateValueObject`). Strings de entrada passam por `Shared/Security/StringSanitizer`; dados sensíveis em log passam por `DataMasking`.
- **Criação assíncrona de moto**: `POST /motos` não grava no Mongo — `MotocycleService` publica `MotocycleCreatedEvent` no RabbitMQ. Dois workers consomem:
  - `MotoManager.Worker.Rental/` (projeto/namespace na verdade é `MotoManager.Worker.MotocycleCreate`) persiste a moto.
  - `MotoManager.Worker.ShowYear/` consome a mesma routing key e só loga quando o ano é 2024.
  Mudanças no contrato do evento afetam os dois workers.
- **Cache**: `DegradationCacheManager` empilha cache em memória → Redis → fonte de dados.
- **Seed**: planos de locação são criados na inicialização da API (`Seeds/RentalPlansSeed.cs`).
- Classes de plumbing de DI/worker são marcadas com `[ExcludeFromCodeCoverage]`.
