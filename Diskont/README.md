# Diskont — Softversko inženjerstvo, Vežbe 6 (rešenje)

Novi projekat **WebApi** (ASP.NET Core, .NET 8) izlaže servise iz BusinessLayer-a preko HTTP-a.

## Struktura WebApi projekta

| Fajl | Sadržaj |
|---|---|
| `Program.cs` | DI registracija repozitorijuma i servisa, Swagger, obrada grešaka, CORS |
| `Controllers/CategoriesController.cs` | CRUD nad kategorijama |
| `Controllers/ProductsController.cs` | CRUD, `?idCategory=`, `/available` |
| `Controllers/UsersController.cs` | lista i registracija (bez lozinki), istorija porudžbina |
| `Controllers/CartController.cs` | korpa: `/api/users/{idUser}/cart` |
| `Errors/BusinessExceptionHandler.cs` | `ArgumentException` → 400, `InvalidOperationException` → 409, ostalo → 500 |
| `Dto/` | `UserDto`, `RegisterUserRequest`, `AddToCartRequest`, `CartDto` |
| `WebApi.http` | svi zahtevi za testiranje |

## Pokretanje

1. Baza `DiskontDB` sa skriptama `Database/01_tabele.sql`, `02_podaci.sql`, `03_korpa.sql`.
2. Startni projekat **WebApi**, F5 → otvara se `https://localhost:7001/swagger`.
3. Ili: otvoriti `WebApi/WebApi.http` i kliknuti **Send request** iznad zahteva.
