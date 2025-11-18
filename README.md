# HabarBank API

Многоуровневый ASP.NET Core проект для банковского backend‑приложения. Решение построено по модульной архитектуре (Users, Cards, Operations, Access) с общей библиотекой `Common`, базовым REST‑API (`src/App`) и поддержкой контейнеризации, PostgreSQL и авто‑документации через Swagger UI.

## Содержание

- [Основные возможности](#основные-возможности)
- [Архитектура и структура](#архитектура-и-структура)
- [Быстрый старт](#быстрый-старт)
- [Настройки окружения](#настройки-окружения)
- [Работа с базой данных](#работа-с-базой-данных)
- [API и документация](#api-и-документация)
- [Тесты](#тесты)
- [Полезные команды](#полезные-команды)
- [Лицензия](#лицензия)

## Основные возможности

- **Модульность** — отдельные проекты для Users, Cards, Operations и Access с общими DTO и базовой инфраструктурой.
- **PostgreSQL** — EF Core + Npgsql, миграции и автосоздание схемы при запуске.
- **Swagger UI** — автоматическая документация и тестирование REST‑эндпоинтов.
- **Глобальная обработка ошибок** — `ApiExceptionFilter` возвращает унифицированный `ApiResponse` при непредвиденных исключениях.
- **Cookie‑аутентификация** — базовая настройка ASP.NET Core Cookies для защищённых сценариев.
- **Контейнеризация** — docker-compose поднимает API и внутренний PostgreSQL одной командой.
- **Unit‑тесты** — проект `HabarBankAPI.UnitTests` для доменных, инфраструктурных и web‑слоёв.

## Архитектура и структура

```
HabarBankAPI/
├── src/
│   ├── App/                     # Точка входа ASP.NET Core (Program, DI, Swagger, Dockerfile)
│   ├── Common/                  # Базовые абстракции, DTO, фильтры, расширения
│   ├── Modules/
│   │   ├── Access/              # Авторизация и выдача access-token’ов
│   │   ├── Card/                # Карты, их типы и балансы
│   │   ├── Operation/           # Денежные операции и трансферы
│   │   └── User/                # Пользователи, роли и CRUD
│   └── ...
├── docker-compose.yml           # App + PostgreSQL
├── HabarBankAPI.sln
└── README.md
```

Каждый модуль организован по слоям Application/Domain/Infrastructure/Presentation и доступен из `App` через `ApplicationPart`. Общие сущности наследуются от `Common.Abstracts.BaseEntity`, инфраструктура использует `Common.Infrastructure.Repositories.GenericRepository`.

## Быстрый старт

### Предварительные требования

- .NET SDK 8.0+
- Docker (Desktop / CLI) с Compose v2
- PostgreSQL 16+ (если запускаете БД вне Docker)

### Локальный запуск (без Docker)

```bash
dotnet restore
dotnet build
dotnet run --project src/App/App.csproj
```

Приложение стартует на `http://localhost:5090` (профиль `http`). Swagger UI: `http://localhost:5090/swagger`.

### Запуск через Docker Compose

```bash
docker compose up --build
```

Откроет два контейнера:

- `postgres` — PostgreSQL 16, база `HabarBankDb`
- `app` — ASP.NET Core API (порт `8080`)

Swagger UI доступен по `http://localhost:8080/swagger`. Остановить и удалить контейнеры: `docker compose down`. Для очистки данных добавьте `-v`.

## Настройки окружения

В `src/App/appsettings.json` определена строка подключения по умолчанию (локальный PostgreSQL). Для разных окружений:

- Переопределяйте секцию `ConnectionStrings:DefaultConnection` в `appsettings.Development.json`, `appsettings.Production.json` и т.д.
- В Docker Compose используется переменная `ConnectionStrings__DefaultConnection=Host=postgres;...`, связывающаяся с сервисом `postgres`.
- Дополнительные переменные `ASPNETCORE_ENVIRONMENT`, `ASPNETCORE_HTTP_PORTS` и др. можно задавать через `.env` или CI/CD.

## Работа с базой данных

- Используется EF Core с `ApplicationDbContext` (настройки в `App.Infrastructure.Data`).
- Схема создаётся автоматически благодаря `Database.EnsureCreated()` — для продакшена рекомендуется подключить миграции (`dotnet ef migrations add` / `dotnet ef database update`).
- В `docker-compose.yml` добавлен volume `postgres_data`, чтобы данные БД сохранялись между перезапусками.

## API и документация

- Все публичные контроллеры используют атрибуты Swagger (`SwaggerResponse`, `SwaggerSchema`) и наследуются от `BaseController`.
- Глобальные ответы завёрнуты в `ApiResponse`/`ApiResponse<T>` для единообразия.
- Swagger UI подключается только в `Development`, но его можно включить и в других окружениях при необходимости.
- Основные маршруты:
  - `api/v1/users` — управление пользователями
  - `api/v1/cards` — операции с картами
  - `api/v1/operations` — денежные переводы
  - `api/v1/access` — авторизация/идентификация

## Тесты

Unit‑тесты расположены в `HabarBankAPI.UnitTests`. Запуск:

```bash
dotnet test HabarBankAPI.UnitTests/HabarBankAPI.UnitTests.csproj
```

Покрываются сервисы приложений, доменные сущности и инфраструктурные компоненты.

## Полезные команды

```bash
# Восстановить пакеты
dotnet restore

# Сборка решения
dotnet build

# Запуск API (HTTP-профиль)
dotnet run --project src/App/App.csproj --launch-profile http

# Поднять Docker инфраструктуру
docker compose up --build

# Добавить EF Core миграцию
dotnet ef migrations add <Name> --project src/App/App.csproj
```

## Лицензия

Проект распространяется под MIT License (при необходимости скорректируйте под свои требования). Contributions welcome! Если у вас есть идеи по развитию API, открывайте issue или pull request.

