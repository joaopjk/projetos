---
name: analisar-bug
description: Investiga um bug, erro, exceção ou comportamento inesperado em um dos projetos do repositório até achar a causa raiz, e propõe ou aplica a correção com teste de regressão. Use quando o usuário relatar erro, stack trace, falha de teste ou "não está funcionando".
---

# Análise de bug

## 1. Coletar evidências
- Sintoma exato: mensagem de erro, stack trace, request/payload, saída esperada vs. obtida, e em qual projeto.
- Se faltar informação essencial para reproduzir, peça ao usuário em vez de supor.

## 2. Descartar causas de ambiente primeiro
Muitos "bugs" aqui são de configuração:
- **Variável de ambiente ausente** — MotoManager e CQRS leem tudo de env vars; ausência gera `NullReferenceException`/`FormatException` na inicialização (ver seção Configuração do `CLAUDE.md`).
- **Infra não rodando** — Mongo/Redis/RabbitMQ/MinIO (MotoManager), Kafka/Zookeeper + Mongo + SQL Server (CQRS), SQL Server em `127.0.0.1,1433` (hexagonal), Docker para Testcontainers.
- **Arquivo invisível ao git** — `Rpg.Domain.Test/` ignorado pela regra `*.test`.

## 3. Reproduzir
Reproduza de forma mínima: um teste que falha (preferível), uma chamada HTTP ou um comando. Registre como reproduziu.

## 4. Encontrar a causa raiz
- Siga o fluxo real do dado a partir do ponto de entrada, lendo o código — não adivinhe pelo nome dos métodos.
- Atenção aos fluxos indiretos: no MotoManager, `POST /motos` só publica evento — a persistência acontece no worker `MotoManager.Worker.Rental` (namespace `MotoManager.Worker.MotocycleCreate`); no CQRS, falha de replay costuma ser `Apply(TEvent)` ausente no agregado ou conflito de `expectedVersion`.
- Use `git log -p`/`git blame` no trecho suspeito quando ajudar a entender a intenção.
- Distinga causa raiz de sintoma: explique **por que** o defeito acontece.

## 5. Corrigir
- Correção mínima na causa raiz, sem refatorações oportunistas.
- Adicione teste de regressão que falha antes e passa depois (quando o projeto tem suíte).
- Procure o mesmo padrão defeituoso em outros pontos do mesmo projeto e mencione-os.

## 6. Relatar
Causa raiz, evidência (como reproduziu), correção aplicada (`arquivo:linha`), resultado real dos testes e riscos remanescentes. Se não aplicou a correção, apresente-a como proposta.
