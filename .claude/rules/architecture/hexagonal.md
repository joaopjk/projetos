---
paths:
  - "microservices-hexagonal-arch/**"
---

# Arquitetura — microservices-hexagonal-arch

Ports & adapters:

- `Domain`: entidades, value objects e interfaces de repositório em `Domain/Ports`.
- `Application`: portas de caso de uso em `Application/Ports`, implementações em `Services`.
- `Adapters.SQL`: `HotelDbContext` (EF Core), repositórios e migrations.
- `BookingService.Api`: adapter de entrada (driving).

Regras:
- `Domain` e `Application` **nunca** referenciam adapters nem EF Core. Dependências apontam para dentro.
- Novo acesso a dados: interface em `Domain/Ports`, implementação em `Adapters.SQL`.
- Mudança de modelo persistido exige migration (comandos no `CLAUDE.md` raiz).
