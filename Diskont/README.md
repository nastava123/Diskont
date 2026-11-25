# Diskont — Softversko inženjerstvo, Vežbe 7 (rešenje)

Novi projekat **BlazorApp** (Blazor WebAssembly Standalone, .NET 8) je klijent za Web API iz Vežbi 6.
Radi na `https://localhost:7101` i preko `HttpClient`-a šalje zahteve na `https://localhost:7001`.

| Fajl | Sadržaj |
|---|---|
| `Program.cs` | HttpClient sa adresom Web API-ja |
| `Pages/Counter.razor` | prva komponenta: `@page`, `@code`, `@onclick` |
| `Pages/Categories.razor` | prikaz, dodavanje, izmena i brisanje kategorija |
| `Pages/Products.razor` | lista proizvoda i forma za dodavanje, brisanje |
| `Models/ErrorResponse.cs` | poruka greške iz Web API-ja (`{ status, message }`) |

BlazorApp referencira samo projekat **Entities**.

## Pokretanje

1. Baza `DiskontDB` sa skriptama iz foldera `Database`.
2. Desni klik na rešenje › **Configure Startup Projects** › **Multiple startup projects**:
   `WebApi` i `BlazorApp` na **Start**.
3. **F5** → Swagger na `https://localhost:7001/swagger`, aplikacija na `https://localhost:7101`.

Ako se podaci ne učitavaju: F12 › Console. *blocked by CORS policy* znači da BlazorApp ne radi
na portu 7101; *Failed to fetch* znači da WebApi nije pokrenut.
