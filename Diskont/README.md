# Diskont — Softversko inženjerstvo, Vežbe 7 (rešenje)

Novi projekat **BlazorApp** (Blazor WebAssembly Standalone, .NET 8) je klijent za Web API iz Vežbi 6.
Radi na `https://localhost:7101`, a podatke čita i menja preko `https://localhost:7001`.

## Struktura BlazorApp projekta

| Fajl | Sadržaj |
|---|---|
| `Program.cs` | HttpClient sa adresom Web API-ja, registracija klijentskih servisa |
| `wwwroot/appsettings.json` | `ApiBaseUrl` |
| `Services/CategoryApi.cs`, `Services/ProductApi.cs` | jedino mesto koje zna adrese `/api/...` |
| `Services/ApiResponse.cs`, `Services/ApiException.cs` | odgovor 4xx/5xx → `ApiException` sa porukom iz BusinessLayer-a |
| `Pages/Categories.razor` | CRUD nad kategorijama na jednoj stranici |
| `Pages/Products.razor` | lista proizvoda sa filterom po kategoriji, brisanje |
| `Pages/ProductEdit.razor` | `/products/new` i `/products/edit/{Id}`: forma za dodavanje i izmenu |
| `Layout/NavMenu.razor` | meni: Početna, Kategorije, Proizvodi |

BlazorApp referencira samo projekat **Entities**.

## Pokretanje

1. Baza `DiskontDB` sa skriptama `Database/01_tabele.sql`, `02_podaci.sql`, `03_korpa.sql`.
2. Desni klik na rešenje › **Configure Startup Projects** › **Multiple startup projects**:
   `WebApi` i `BlazorApp` na **Start** (WebApi prvi).
3. **F5** → Swagger na `https://localhost:7001/swagger` i aplikacija na `https://localhost:7101`.

Ako se podaci ne učitavaju: F12 › Console. Poruka *blocked by CORS policy* znači da BlazorApp
ne radi na portu 7101 ili da u `WebApi/Program.cs` nedostaje `app.UseCors("Frontend")`.
