# Clube da Leitura Web

**Trabalho 03 - [Academia do Programador](https://www.academiadoprogramador.net/inicio) 2026**

"Gustavo tem uma coleção grande de revistas em quadrinhos. Por isso, resolveu emprestar para os amigos. Assim foi criado o Clube da Leitura.

Mas para não perder nenhuma revista, seu pai contratou os alunos da Academia do Programador para fazer uma aplicação web que cadastra as revistas e controla os empréstimos."

![Demonstração](https://i.imgur.com/cbzkJb5.gif)

## Funcionalidades

- Cadastro, edição, exclusão e visualização de caixas, revistas e amigos
- Controle de empréstimos:
  - Registro de novos empréstimos
  - Registro de devoluções
  - Visualização de empréstimos abertos e concluídos

## Persistência de Dados

Os dados são armazenados em arquivo JSON no caminho:

`%LocalAppData%/ClubeDaLeituraWeb/dadosSalvos.json`

## Como Executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

### Passos

1. Abra a pasta do repositório.
2. Restaure e compile (opcional):

```bash
dotnet build ClubeDaLeituraWeb.slnx
```

3. Execute a aplicação:

```bash
dotnet run --project ClubeDaLeituraWeb.WebApp
```
