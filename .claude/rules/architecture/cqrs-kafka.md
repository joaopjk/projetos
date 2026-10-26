---
paths:
  - "microservices-cqrs-kafka-api-netcore/**"
---

# Arquitetura — microservices-cqrs-kafka-api-netcore

CQRS com event sourcing, dividido em `Post.Cmd.*` (escrita) e `Post.Query.*` (leitura). Abstrações compartilhadas em `CQRS.Core`, contratos de evento em `Post.Common`.

- Comandos passam por um `CommandDispatcher` próprio; handlers são registrados manualmente em `Post.Cmd.Api/Program.cs` (`dispatcher.RegisterHandler<TCommand>(...)`). **Comando novo exige registro ali.**
- `AggregateRoot` aplica eventos por reflection: cada tipo de evento precisa de um método público `Apply(TEvent)` no agregado (`PostAggregate`), senão o replay lança exceção. **Evento novo exige `Apply` correspondente.**
- `EventStore` faz concorrência otimista via `expectedVersion`, persiste no Mongo e depois publica cada evento no Kafka. Eventos persistidos são imutáveis: não altere a forma de eventos existentes; crie um novo tipo.
- O lado Query consome os eventos para o SQL Server via EF Core (`EnsureCreated`, lazy-loading proxies).
- As duas APIs ainda têm controllers `WeatherForecast` de template; os endpoints reais não foram implementados.
