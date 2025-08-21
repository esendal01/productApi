# Product.Api — Backend Task 1

Bu proje .NET 8 (ASP.NET Core Web API) ile geliştirilen, PostgreSQL + EF Core kullanan basit bir ürün yönetim servisidir. Katmanlı mimari (Controller → Service → Repository) ve DTO yapısı uygulanmıştır. Swagger ile test edilebilir.

---

## 📦 Gereksinimler

* .NET SDK 8.x (veya 7/6 da çalışır)
* PostgreSQL 15+ (lokal)
* Entity Framework Core Tools

```bash
# EF CLI
dotnet tool install --global dotnet-ef
```

---

## 🗃️ PostgreSQL Kurulum & Çalıştırma

Aşağıdaki seçeneklerden birini kullan.

### Seçenek A — Windows Installer (Servis)

1. PostgreSQL’i kur (Server + pgAdmin + Command Line Tools seçili).
2. Kurarken port: **5432** (boş değilse 5433), kullanıcı: **postgres**, parola: belirle.
3. `services.msc` → `postgresql-x64-XX` servisini **Running** yap.

### Seçenek B — Elle Başlat (pg\_ctl, kullanıcı klasöründe)

> Windows Türkçe locale sorunu için `--locale=C --encoding=UTF8` kullanılır.

```powershell
# Yönetici PowerShell
cd "C:\Program Files\PostgreSQL\17\bin"
initdb -D "C:\Users\<Kullanıcı>\pgsql\data" -U postgres -W --auth=md5 --locale=C --encoding=UTF8
pg_ctl -D "C:\Users\<Kullanıcı>\pgsql\data" -l "C:\Users\<Kullanıcı>\pgsql\logfile.log" -w start
```

> Not: `psql --version` görünmüyorsa PostgreSQL `bin` klasörünü PATH’e ekle.

### Seçenek C — Docker (en hızlı)

```bash
docker run --name pg -e POSTGRES_PASSWORD=postgres -e POSTGRES_DB=productdb -p 5432:5432 -d postgres:16
```

---

## 🔧 appsettings.json (Connection String)

`Product.Api/appsettings.json` içinde:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=productdb;Username=postgres;Password=postgres"
  }
}
```

> Portu kurduğun porta göre güncelle (5433 vb.).

---

## 🗄️ Veritabanı Migrasyonu

```bash
# proje kökünde veya Product.Api içinde
cd Product.Api
# migration zaten mevcutsa sadece güncelle:
dotnet ef database update
```

> Konsolda "Applying migration '...\_InitialCreate'. Done." görmelisin.

---

## ▶️ Çalıştırma & Swagger

Uygulamayı başlat:

```bash
dotnet run --project Product.Api
```

Konsolda şu satırlar görünür (portlar örnektir):

```
Now listening on: https://localhost:7153
Now listening on: http://localhost:5153
```

Swagger arayüzü **Development** ortamında otomatik açıktır. Aşağıdaki URL’den eriş:

```
https://localhost:7153/swagger
```

> Eğer sadece HTTPS dinleniyorsa `https://…/swagger` kullan. Portları konsoldakiyle eşleştir.

**launchSettings.json (opsiyonel, port sabitleme)**

```json
"applicationUrl": "https://localhost:7153;http://localhost:5153",
"launchUrl": "swagger"
```

---

## 🔌 Endpointler

Temel rota: `api/Product`

* **GET** `/api/Product` → tüm ürünler
* **GET** `/api/Product/{id}` → tek ürün
* **POST** `/api/Product` → ürün oluştur
* **PUT** `/api/Product/{id}` → ürün güncelle
* **DELETE** `/api/Product/{id}` → ürün sil

### Örnek Gövdeler

**POST / PUT JSON**

```json
{
  "name": "Kalem",
  "price": 19.9,
  "description": "HB"
}
```

---

## 🧱 Mimari

* **Domain**: `ProductEntity` (Entities), Repository arayüzleri
* **Infrastructure**: `AppDbContext`, Repository implementasyonları
* **Application**: DTO’lar (`ProductCreateDto`, `ProductUpdateDto`, `ProductReadDto`) ve `IProductService`/`ProductService`
* **API**: `ProductController`, Swagger

---

## ✅ Doğrulama (Validation)

DTO’larda DataAnnotations kullanıldı:

* `Name`: `[Required]`, `StringLength(100)`
* `Price`: `[Range(0.01, …)]`
* `Description`: `StringLength(500)`

`[ApiController]` sayesinde hatalı gövde → **400 Bad Request + ProblemDetails** şeklinde döner.

---

## 🧰 Global Hata Yönetimi (ProblemDetails)

`ExceptionHandlingMiddleware` ile beklenmeyen hatalarda **500** ve tutarlı `application/problem+json` formatı döner.

---

## 🧪 Hızlı Test (cURL)

```bash
# Create
token="" # gerek yoksa boş bırakın
curl -k -X POST https://localhost:7153/api/Product \
 -H "Content-Type: application/json" \
 -d '{"name":"Kalem","price":19.9,"description":"HB"}'

# Get All
curl -k https://localhost:7153/api/Product

# Update (ör. id=1)
curl -k -X PUT https://localhost:7153/api/Product/1 \
 -H "Content-Type: application/json" \
 -d '{"name":"Kalem Pro","price":24.5,"description":"HB-2"}'

# Delete
curl -k -X DELETE https://localhost:7153/api/Product/1
```

---

