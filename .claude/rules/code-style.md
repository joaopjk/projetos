---
paths:
  - "**/*.cs"
  - "**/*.java"
---

# Estilo de código

O princípio é **seguir o estilo do projeto em que se está**, não um padrão global. Os projetos divergem entre si e isso é aceitável.

## C#

- Namespaces: `meu-livro-receitas-api-netcore` usa file-scoped (`namespace X;`); os demais usam bloco (`namespace X { }`). Mantenha o do projeto.
- `Nullable` e `ImplicitUsings` estão habilitados na maioria dos `.csproj`; não desabilite e não use `!` para silenciar avisos sem justificativa.
- MotoManager usa primary constructors com campos `_privados` e guarda `?? throw`, e comentários XML `/// <summary>` em membros públicos. Mantenha esse padrão nas classes novas de lá.
- Métodos assíncronos retornam `Task`/`Task<T>`; não use `.Result` nem `.Wait()`.
- Não troque `TargetFramework` nem versões de pacotes NuGet sem pedido explícito.

## Java (produtosapi)

- Pacotes por camada em `com.example.produtosapi` (`controller`, `model`, `repository`).
- Use Lombok como o restante do projeto; injeção por construtor.

## Geral

- Nomes no idioma já usado no projeto/arquivo (português ou inglês) — não traduza identificadores existentes.
- Comentários explicam o "porquê", não repetem o código.
- Sem código morto ou `TODO` órfão em código novo.
