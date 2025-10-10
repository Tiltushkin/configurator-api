# PC Configurator — Backend (ASP.NET Core 8 + Postgres + Redis + JWT)

Готовый бэкенд для сайта «ПК‑конфигуратор». Включает:
- C# / .NET 8 Web API
- PostgreSQL (EF Core, arrays)
- Redis (кэширование умного маршрута компонентов)
- JWT авторизация
- Swagger (с авторизацией через Bearer token)
- Health Checks (`/health`)
- CORS (настройка в `appsettings.json`)

## Быстрый старт (локально)

1) Установите .NET 8 SDK и Docker.
2) Поднимите Postgres и Redis (пример docker-compose ниже).
3) Отредактируйте `appsettings.json` при необходимости.
4) Соберите и запустите проект:
```bash
dotnet restore
dotnet run --project src/PcConfigurator.Api
```
API будет доступно на `http://localhost:5051/swagger`.

### Пример `docker-compose.yml`

```yaml
version: "3.9"
services:
  db:
    image: postgres:16
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: pc_configurator
    ports: ["5432:5432"]
    volumes:
      - dbdata:/var/lib/postgresql/data

  redis:
    image: redis:7
    ports: ["6379:6379"]

volumes:
  dbdata:
```

## Маршруты

### Публичные
- `POST /api/auth/register` — регистрация (возвращает JWT)
- `POST /api/auth/login` — авторизация (возвращает JWT)

### Профили (требуется JWT)
- `GET /api/profiles/me` — мой профиль
- `GET /api/profiles/{userId}` — профиль другого пользователя
- `PUT /api/profiles/me` — изменить отображаемое имя и аватар (только эти поля)

### Сборки (требуется JWT)
- `GET /api/builds?scope=mine|public|all&userId=<guid>`
- `GET /api/builds/{id}`
- `POST /api/builds` — создать
- `PUT /api/builds/{id}` — обновить
- `DELETE /api/builds/{id}` — удалить
- `POST /api/builds/{id}/share?expireDays=30` — сгенерировать публичную ссылку
- `GET /api/builds/shared/{token}` — открыть общедоступную (шаренную) сборку (без JWT)

### Компоненты (умный маршрут, требуется JWT)
- `GET /api/components?type=cpu|gpu|all&search=&limit=50&offset=0&priceMin=&priceMax=&manufacturerCode=&model=&socket=`

Кэширование результата — в Redis на 2 минуты.

## Модель данных
- `User` — Login, PasswordHash (BCrypt), DisplayName, AvatarUrl
- `Cpu`, `Gpu` — поля максимально близкие к ТЗ, массивы хранятся как `text[]` (Npgsql)
- `Build` — принадлежит владельцу, хранит ссылки на CPU/GPU, флаг публичности
- `BuildShare` — токен для публичной ссылки + опциональный срок действия

## Безопасность
- JWT (Issuer/Audience/Key настраиваются в `appsettings.json`).
- В Swagger нажмите **Authorize** и вставьте: `Bearer <токен>`.

## Заметки
- Для демо используется `EnsureCreated()`. Для продакшена подключите миграции EF Core.
- Поля моделей можно расширять без ломки API (добавляйте новые nullable‑свойства).
- CORS политики правятся в `Cors:AllowedOrigins`.

Удачи! 🚀

## Docker / Docker Compose

### 1) Собрать образ API
```bash
# из корня репозитория (где PcConfigurator.sln)
docker build -f src/PcConfigurator.Api/Dockerfile -t pc-configurator-api:latest .
```

### 2) Запуск всего стека через docker-compose (API + Postgres + Redis)
```bash
docker compose up --build
```
API будет доступно на `http://localhost:5051/swagger` (в контейнере сервис слушает `:8080`).

> Конфиги в контейнер подаются через **переменные окружения** (см. `docker-compose.yml`):
> - `ConnectionStrings__Postgres=Host=db;Port=5432;Database=pc_configurator;Username=postgres;Password=postgres`
> - `ConnectionStrings__Redis=redis:6379`
> - `Jwt__Key=...` (замени на длинный секрет)
> - CORS: `Cors__AllowedOrigins__0=http://localhost:5173`, и т.д.
> - `Swagger__Enabled=true`

### Полезно
- Проверка здоровья контейнера: `GET /health` 
- В compose добавлены healthchecks для Postgres/Redis, API стартует после их готовности.
