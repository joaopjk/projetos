---
name: revisao-codigo
description: Revisa as alterações pendentes (diff do working tree, de um commit ou de uma branch) em um dos projetos do repositório, apontando bugs, violações de arquitetura, problemas de segurança e lacunas de teste. Use quando o usuário pedir revisão, review ou "dá uma olhada no que mudei".
---

# Revisão de código

## 1. Delimitar o escopo
- Sem argumento: `git diff` + `git diff --staged` (e arquivos novos de `git status`). Com argumento: commit, intervalo ou branch informado.
- Identifique **qual(is) projeto(s)** o diff toca. Cada um tem arquitetura própria — leia as regras correspondentes em `.claude/rules/architecture/` antes de opinar.
- Lembre que `rpg-netcore/Rpg.Domain.Test/` não aparece no `git status` (gitignore `*.test`); se o diff toca o RPG, confira essa pasta manualmente.

## 2. Ler com contexto
Para cada arquivo alterado, leia o arquivo inteiro (não só o hunk) e os chamadores/implementações relevantes. Não comente o que não leu.

## 3. Verificar, em ordem de prioridade
1. **Correção**: lógica errada, null não tratado, async sem `await`, `.Result`/`.Wait()`, condição de corrida, exceção engolida.
2. **Arquitetura do projeto**: dependência apontando para fora (Domain → Infrastructure/adapters), registro de DI fora do `IoC` (MotoManager), comando sem `RegisterHandler` ou evento sem `Apply` (CQRS), mudança de modelo sem migration (hexagonal), contrato de evento RabbitMQ alterado sem ajustar os dois workers (MotoManager).
3. **Segurança** (`.claude/rules/security.md`): segredo no código, entrada não validada, dado sensível em log sem máscara, query concatenada, detalhe interno na resposta HTTP.
4. **Testes**: comportamento novo/corrigido sem teste no projeto que tem suíte; teste removido ou ignorado.
5. **Estilo**: só o que divergir do padrão do próprio projeto. Não peça para "harmonizar" com outros projetos.

## 4. Validar
Rode o build e os testes do projeto afetado (ou delegue ao subagente `executor-testes`). Reporte o resultado real; se não foi possível rodar (ex.: Docker ausente), diga.

## 5. Relatório
Liste os achados do mais grave para o menos grave, cada um com `arquivo:linha`, o problema, um cenário concreto em que ele se manifesta e a correção sugerida. Separe **bloqueantes** de **sugestões**. Se não houver problemas relevantes, diga isso claramente — não invente achados para preencher. Não altere código, a menos que o usuário peça.
