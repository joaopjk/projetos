---
name: implementar-feature
description: Implementa uma funcionalidade nova em um dos projetos do repositório seguindo a arquitetura daquele projeto, com testes e verificação por build. Use quando o usuário pedir para criar endpoint, caso de uso, comando, entidade ou qualquer funcionalidade nova.
---

# Implementação de funcionalidade

## 1. Entender o pedido
- Identifique o projeto alvo. Se o pedido for ambíguo quanto ao projeto ou ao comportamento esperado, pergunte antes de codar.
- Leia `.claude/rules/architecture/<projeto>.md` e encontre uma funcionalidade **parecida já existente** no mesmo projeto para usar como molde.

## 2. Planejar
Liste os arquivos a criar/alterar, camada por camada, de dentro para fora. Para mudanças que tocam várias camadas, mostre o plano ao usuário antes de implementar.

Checklist por projeto:
- **MotoManager**: value object/entidade no Domain → interface de repositório → implementação na Infrastructure → serviço na Application retornando `Result<T>` → mensagens em `Resource.resx` → registro no `MotoManager.IoC` → controller na Api → logs via `BaseLogger<T>` com correlation id.
- **CQRS/Kafka**: comando + handler → `RegisterHandler` em `Post.Cmd.Api/Program.cs` → evento em `Post.Common` → `Apply(TEvent)` no `PostAggregate` → handler de evento no lado Query.
- **Hexagonal**: entidade/porta no `Domain` → porta e serviço na `Application` → repositório no `Adapters.SQL` (+ migration se mudar o modelo) → endpoint na `BookingService.Api`.
- **MyRecipeBook**: request/response em `Communication` → use case + validator FluentValidation → exceção da hierarquia `MyRecipeBookException` para erros → controller.
- **produtosapi**: model → repository → controller.

## 3. Implementar
- Siga o estilo do projeto (`.claude/rules/code-style.md`) e as regras de segurança (`.claude/rules/security.md`).
- Não refatore código não relacionado nem atualize pacotes/framework.

## 4. Testar
- Se o projeto tem suíte de testes, escreva testes do comportamento novo no framework do projeto (`.claude/rules/testing.md`): caminho feliz e principais falhas de validação.
- Rode build + testes do projeto (ou use o subagente `executor-testes`).

## 5. Entregar
Resuma o que foi criado/alterado, como exercitar a funcionalidade (endpoint, payload de exemplo, variáveis de ambiente necessárias) e o resultado real do build/testes. Aponte o que ficou de fora ou pendente.
