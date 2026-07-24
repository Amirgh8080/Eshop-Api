# Shop API — Marketplace Backend (ASP.NET Core)

A multi-seller marketplace REST API built with **ASP.NET Core**, applying **Domain-Driven Design (tactical patterns)**, **CQRS**, and **Clean Architecture**.

> Originally started as a deep-dive into advanced software design with ASP.NET Core; since extended and maintained as a reference implementation of DDD + CQRS in a realistic e-commerce domain.

## ✨ What this project demonstrates

- **Domain-Driven Design** — the domain is modeled as aggregates (`UserAgg`, `ProductAgg`, `OrderAgg`, `SellerAgg`, `CategoryAgg`, `CommentAgg`) with encapsulated business rules and invariants enforced inside the aggregate roots.
- **CQRS with MediatR** — commands (write side) and queries (read side) are fully separated: `Shop.Application` handles commands, `Shop.Query` serves optimized read models.
- **Clean Architecture** — dependencies point inward; the domain has zero infrastructure references:

```
Shop.Api                 → HTTP endpoints, auth, rate limiting, Swagger
Shop.Presentation.Facade → thin orchestration layer between API and app core
Shop.Application         → command handlers, validation, business workflows
Shop.Query               → read-side handlers and DTOs (CQRS read stack)
Shop.Domain              → aggregates, entities, value objects, domain services
Shop.Infrastructure      → EF Core persistence, external concerns
Common.*                 → shared kernel (domain base classes, query/app abstractions, caching helpers)
```

## 🧰 Tech stack

| Concern | Choice |
|---|---|
| Framework | ASP.NET Core (.NET 8 LTS) Web API |
| Data access | Entity Framework Core + SQL Server |
| Messaging (in-process) | MediatR (commands/queries) |
| Caching | Redis (distributed cache) |
| Auth | JWT Bearer authentication + role-based authorization |
| API protection | AspNetCoreRateLimit (IP rate limiting) |
| Mapping | AutoMapper |
| Docs | Swagger / OpenAPI |
| Device detection | UAParser (login/session tracking) |

## 🔌 API surface

14 controllers covering the full marketplace flow: **Auth** (JWT login/refresh), **Users & Roles**, **Sellers**, **Products & Categories**, **Orders & Transactions**, **Shipping methods**, **Comments**, plus CMS-style **Banners/Sliders**.

## 🚀 Getting started

```bash
git clone https://github.com/Amirgh8080/Eshop-Api.git
cd Eshop-Api

# configure your connection strings in Shop/Shop.Api/appsettings.json
#   - SQL Server
#   - Redis

dotnet restore
dotnet ef database update --project Shop/Shop.Infrastructure --startup-project Shop/Shop.Api
dotnet run --project Shop/Shop.Api
```

Swagger UI is available at `https://localhost:<port>/swagger`.

### JWT signing key

`appsettings.json` ships with `JwtConfig:SignInKey` set to the placeholder
`REPLACE-WITH-YOUR-OWN-SIGNING-KEY` — the API won't issue trustworthy tokens
until you supply your own. Set a real value locally via user secrets (never
commit it):

```bash
cd Shop/Shop.Api
dotnet user-secrets init
dotnet user-secrets set "JwtConfig:SignInKey" "<a long random string>"
```

Or via an environment variable (e.g. in a deployment): `JwtConfig__SignInKey`
(double underscore, per ASP.NET Core's configuration binding convention).

## 🗺️ Roadmap

- [x] Upgrade to .NET 8 LTS
- [ ] Integration tests (Testcontainers: SQL Server + Redis)
- [ ] Docker Compose for one-command local setup
- [ ] Outbox pattern for reliable domain events

## 📄 License

MIT
