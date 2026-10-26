---
paths:
  - "**/*Test*/**"
  - "**/*Tests/**"
  - "**/src/test/**"
---

# Testes

- Use o framework que o projeto já usa — não misture:
  - MotoManager: xUnit + Moq (unit, `MotoManager.Tests`), xUnit + Testcontainers (integração, `MotoManager.IntegrationTests`).
  - Hexagonal: NUnit (`DomainTests`, `ApplicationTests`, `AdaptersTests`).
  - RPG: xUnit + FluentAssertions (`Rpg.Domain.Test`).
  - produtosapi: JUnit 5 + Spring Boot Test.
- Testes unitários não tocam infraestrutura real: repositórios e clientes externos são mockados.
- Testes de integração do MotoManager sobem containers por classe via `IAsyncLifetime` e precisam do Docker rodando. Se o Docker não estiver disponível, informe — não trate como aprovado.
- Nome do teste descreve cenário e resultado esperado (siga o padrão já existente na classe).
- Um comportamento por teste, estrutura Arrange / Act / Assert.
- Correção de bug vem com um teste que falha antes da correção e passa depois, quando o projeto tem suíte de testes.
- Não apague nem marque como `Skip`/`Ignore` um teste que falha só para a suíte passar.
- `rpg-netcore/Rpg.Domain.Test/` é ignorado pelo git (regra `*.test` do `.gitignore` raiz) — arquivos novos ali precisam de `git add -f`.
