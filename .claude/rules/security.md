# Segurança

Vale para todos os projetos.

- **Segredos**: nunca grave credenciais, tokens ou connection strings reais em código, `appsettings*.json` ou testes. Configuração vem de variáveis de ambiente (padrão já usado no MotoManager e no CQRS). As credenciais do `docker-compose.yml` são só de desenvolvimento local. Não leia nem exiba arquivos `.env`.
- **Entrada**: toda entrada externa é validada na fronteira (value objects, validators FluentValidation, Bean Validation). No MotoManager, strings passam por `StringSanitizer`.
- **Dados sensíveis em log**: CNH, CNPJ, documentos e dados pessoais são mascarados (`DataMasking` no MotoManager). Nunca logue senha, token ou payload completo com dados pessoais.
- **Banco**: só consultas parametrizadas, LINQ ou filtros tipados do driver. Proibido montar SQL ou filtros Mongo concatenando entrada do usuário.
- **Erros**: respostas HTTP não expõem stack trace, mensagens internas nem detalhes de infraestrutura — use o mecanismo de erro do projeto (`ApiMiddleware`, `ExceptionFilters`, `Result<T>`).
- **Uploads (MinIO)**: valide tipo e tamanho do arquivo e gere o nome do objeto no servidor; não use o nome enviado pelo cliente como chave.
- **Dependências**: não adicione pacotes sem necessidade; prefira versões estáveis e mantidas.
