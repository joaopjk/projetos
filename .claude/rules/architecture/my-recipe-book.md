---
paths:
  - "meu-livro-receitas-api-netcore/**"
---

# Arquitetura — meu-livro-receitas-api-netcore

Estilo orientado a casos de uso:

- `Communication`: contratos de request/response (o que a API expõe).
- `Application/UseCases`: casos de uso + validators FluentValidation, um por caso de uso.
- `Exceptions`: hierarquia `MyRecipeBookException`, traduzida para HTTP em `Api/Filters/ExceptionFilters`. Erros de negócio lançam exceções dessa hierarquia.
- `CultureMiddleware` define a cultura a partir do `Accept-Language`; mensagens ao usuário vêm de resources, não de strings fixas.
- `MyRecipeBookDbContext` usa o schema `myrecipebook`; ainda não há provider de banco configurado.
