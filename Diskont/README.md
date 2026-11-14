# Diskont — Softversko inženjerstvo, Vežbe 5 (rešenje)

## Šta je urađeno

**DataLayer** — svi repozitorijumi su kompletni (Add, Update, Delete, Get, GetAll + dodatne metode):

| Repozitorijum | Dodatne metode |
|---|---|
| `ProductRepository` | `GetByCategory` |
| `CategoryRepository` | `GetByName` |
| `UserRepository` | `GetByEmail` |
| `RoleRepository` | — |
| `OrderRepository` | `GetAllByUser`, `GetOrderByUser` (korpa = porudžbina sa `IsCompleted = 0`) |
| `OrderItemRepository` | `GetByOrder`, `GetByOrderAndProduct` |

Metode `Get…` vraćaju **`null`** kada red ne postoji.

**BusinessLayer** — osnovni servisi sa validacijom:
`ProductService`, `CategoryService`, `UserService`, a `OrderService` ima konstruktor i osnovne metode.

## Rešenje Vežbi 5

U `OrderService` su implementirane metode za korpu:
`GetCart`, `GetCartItems`, `AddToCart`, `RemoveFromCart`, `Checkout`, `GetHistory`.

## Pokretanje

1. Nad bazom `DiskontDB` izvršiti `Database/03_korpa.sql` (nova kolona `Orders.IsCompleted`).
   Za novu bazu redom: `01_tabele.sql`, `02_podaci.sql`, `03_korpa.sql`.
2. Otvoriti `Diskont.sln`, startni projekat **TestConsole**, Ctrl+F5.
