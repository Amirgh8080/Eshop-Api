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

### Option A: Docker Compose (fastest)

```bash
git clone https://github.com/Amirgh8080/Eshop-Api.git
cd Eshop-Api
docker compose up --build
```

Spins up the API, SQL Server, and Redis together. Swagger UI is available at
`http://localhost:8080/swagger`. The API container connects to the other two
via SQL/Redis auth (no Windows Integrated Security), configured through
environment variables in `docker-compose.yml` — override `SQL_SA_PASSWORD` if
you don't want the default dev password. Note: EF Core migrations aren't
applied automatically; run the `dotnet ef database update` command below
(pointed at `localhost,1433`) before exercising endpoints that hit the database.

### Option B: Local .NET + your own SQL Server / Redis

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

## 🧪 Testing

```bash
dotnet test tests/Shop.Api.IntegrationTests
```

Integration tests spin up real, ephemeral SQL Server and Redis containers via
[Testcontainers](https://dotnet.testcontainers.org/) (needs Docker running),
apply EF Core migrations, then exercise the app through
`WebApplicationFactory`: an auth flow (register → login → JWT issuance, plus
a wrong-password rejection case) and a full CRUD flow on the shipping-method
endpoint (create → read → update → delete).

## 🗺️ Roadmap

- [x] Upgrade to .NET 8 LTS
- [x] Integration tests (Testcontainers: SQL Server + Redis)
- [x] Docker Compose for one-command local setup
- [ ] Outbox pattern for reliable domain events

## 📄 License

MIT
