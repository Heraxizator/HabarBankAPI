<div align="center">

# 🏦 HabarBank API

**Современный REST API для банковских операций на ASP.NET Core**

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat&logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-336791?style=flat&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=flat&logo=docker&logoColor=white)](https://www.docker.com/)
[![EF Core](https://img.shields.io/badge/EF%20Core-9.0-512BD4?style=flat)](https://docs.microsoft.com/ef/core/)
[![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-85EA2D?style=flat&logo=swagger&logoColor=black)](https://swagger.io/)

*Модульная архитектура • Миграции EF Core • Автоматическая документация • Docker-ready*

[Быстрый старт](#-быстрый-старт) • [Документация](#-документация) • [API Endpoints](#-api-endpoints) • [Архитектура](#-архитектура)

</div>

---

## 📋 Содержание

- [О проекте](#-о-проекте)
- [Основные возможности](#-основные-возможности)
- [Технологический стек](#-технологический-стек)
- [Быстрый старт](#-быстрый-старт)
- [Архитектура](#-архитектура)
- [База данных](#-база-данных)
- [API Endpoints](#-api-endpoints)
- [Конфигурация](#-конфигурация)
- [Разработка](#-разработка)
- [Docker](#-docker)
- [Миграции](#-миграции)
- [Тестирование](#-тестирование)
- [Документация](#-документация)
- [Лицензия](#-лицензия)

---

## 🎯 О проекте

**HabarBank API** — это современное банковское backend-приложение, построенное на ASP.NET Core 8.0 с использованием чистой архитектуры и модульного подхода. Проект демонстрирует лучшие практики разработки enterprise-решений:

- 🏗️ **Модульная архитектура** с разделением ответственности
- 🔄 **EF Core Migrations** для управления схемой базы данных
- 🐘 **PostgreSQL** в качестве основной СУБД
- 🐳 **Docker Compose** для быстрого развертывания
- 📚 **Swagger UI** для интерактивной документации API
- 🔐 **Cookie Authentication** для безопасной авторизации
- ✅ **Unit-тесты** для обеспечения качества кода

### Основные бизнес-сценарии

- Управление пользователями и их ролями
- Создание и управление банковскими картами
- Операции переводов между картами
- Работа с различными валютами и курсами
- Система авторизации через токены доступа

---

## ✨ Основные возможности

### 🏛️ Модульная архитектура

```
📦 HabarBankAPI
 ┣ 📂 Users      → Управление пользователями и ролями
 ┣ 📂 Cards      → Банковские карты и их типы
 ┣ 📂 Operations → Денежные переводы и транзакции
 ┣ 📂 Valutas    → Валюты и курсы обмена
 ┗ 📂 Access     → Авторизация и токены доступа
```

Каждый модуль следует **Clean Architecture**:
- **Domain** — сущности и бизнес-логика
- **Application** — сервисы и DTO
- **Infrastructure** — репозитории и доступ к данным
- **Presentation** — контроллеры и API endpoints

### 🔄 Автоматические миграции

- ✅ Миграции EF Core применяются автоматически при запуске
- ✅ Полная история изменений схемы БД
- ✅ Поддержка rollback и версионирование
- ✅ Foreign Keys и каскадные удаления настроены

### 🛡️ Безопасность

- 🔐 Cookie-based аутентификация
- 🔑 Система access-токенов
- 🚫 Глобальная обработка исключений
- 🗑️ Soft Delete для всех сущностей

### 🐳 Готовность к production

- ✅ Docker Compose для локальной разработки
- ✅ Health checks endpoints
- ✅ Swagger UI для документации
- ✅ Настройка под разные окружения (Development/Production)
- ✅ Volume для персистентности данных PostgreSQL

---

## 🛠️ Технологический стек

### Backend

| Технология | Версия | Назначение |
|------------|--------|------------|
| **ASP.NET Core** | 8.0 | Web API Framework |
| **Entity Framework Core** | 9.0 | ORM и миграции |
| **Npgsql** | 9.0 | PostgreSQL Provider |
| **Swashbuckle** | 6.5 | Swagger/OpenAPI документация |

### База данных

| Технология | Версия | Назначение |
|------------|--------|------------|
| **PostgreSQL** | 16 | Основная СУБД |
| **EF Core Migrations** | 9.0 | Управление схемой |

### Инфраструктура

| Технология | Назначение |
|------------|------------|
| **Docker** | Контейнеризация приложения |
| **Docker Compose** | Оркестрация контейнеров |
| **PowerShell** | Скрипты автоматизации |

---

## 🚀 Быстрый старт

### Предварительные требования

Убедитесь, что у вас установлены:

- ✅ [.NET SDK 8.0+](https://dotnet.microsoft.com/download)
- ✅ [Docker Desktop](https://www.docker.com/products/docker-desktop) (рекомендуется)
- ✅ [PostgreSQL 16+](https://www.postgresql.org/download/) (опционально, если без Docker)
- ✅ [Git](https://git-scm.com/)

### Вариант 1: Docker Compose (рекомендуется) 🐳

```bash
# 1. Клонируйте репозиторий
git clone https://github.com/your-username/HabarBankAPI.git
cd HabarBankAPI

# 2. Создайте начальную миграцию
.\create-migration.ps1 InitialCreate

# 3. Запустите всё одной командой
docker compose up --build
```

**Готово!** 🎉 API доступен на:
- 🌐 Swagger UI: http://localhost:8080/swagger
- 🏥 Health Check: http://localhost:8080/api/health

### Вариант 2: Локальный запуск 💻

```bash
# 1. Убедитесь, что PostgreSQL запущен на localhost:5432

# 2. Восстановите зависимости
dotnet restore

# 3. Создайте миграцию
dotnet ef migrations add InitialCreate --project src\App\App.csproj

# 4. Запустите приложение
dotnet run --project src\App\App.csproj
```

**Готово!** 🎉 API доступен на:
- 🌐 Swagger UI: http://localhost:5090/swagger
- 🏥 Health Check: http://localhost:5090/api/health

---

## 🏗️ Архитектура

### Структура проекта

```
HabarBankAPI/
├── 📁 src/
│   ├── 📁 App/                          # 🚀 Точка входа приложения
│   │   ├── Controllers/                 # Health check и служебные endpoints
│   │   ├── Infrastructure/
│   │   │   └── Data/
│   │   │       └── ApplicationDbContext.cs  # EF Core контекст
│   │   ├── Migrations/                  # 🔄 EF Core миграции
│   │   ├── Program.cs                   # Настройка DI и middleware
│   │   ├── appsettings.json             # Конфигурация
│   │   └── App.csproj
│   │
│   ├── 📁 Common/                       # 🔧 Общая инфраструктура
│   │   ├── Abstracts/                   # Базовые классы и интерфейсы
│   │   │   ├── BaseEntity.cs           # Базовая сущность (Id, DeletedAt)
│   │   │   ├── BaseController.cs       # Базовый контроллер
│   │   │   ├── GenericRepository.cs    # Generic репозиторий
│   │   │   └── IDbContext.cs
│   │   ├── DTOs/                        # Общие DTO
│   │   │   └── ApiResponse.cs          # Унифицированный ответ API
│   │   ├── Exceptions/                  # Кастомные исключения
│   │   ├── Filters/                     # Глобальные фильтры
│   │   │   └── ApiExceptionFilter.cs   # Обработка исключений
│   │   └── Extensions/                  # Утилиты (Hash, Encrypt)
│   │
│   └── 📁 Modules/                      # 📦 Бизнес-модули
│       ├── 👤 User/                     # Модуль пользователей
│       │   ├── Domain/
│       │   │   ├── Entities/
│       │   │   │   └── User.cs
│       │   │   └── Enums/
│       │   │       └── RoleEnum.cs
│       │   ├── Application/
│       │   │   ├── DTOs/
│       │   │   ├── Interfaces/
│       │   │   │   └── IUserService.cs
│       │   │   ├── Services/
│       │   │   │   └── UserService.cs
│       │   │   └── Mappers/
│       │   ├── Infrastructure/
│       │   │   └── Repositories/
│       │   │       └── UserRepository.cs
│       │   └── Presentation/
│       │       └── Controllers/
│       │           └── UserController.cs
│       │
│       ├── 💳 Card/                     # Модуль банковских карт
│       │   └── [та же структура]
│       │
│       ├── 💸 Operation/                # Модуль операций
│       │   └── [та же структура]
│       │
│       ├── 💱 Valuta/                   # Модуль валют
│       │   └── [та же структура]
│       │
│       └── 🔐 Access/                   # Модуль авторизации
│           └── [та же структура]
│
├── 📁 scripts/                          # 🔧 Утилиты
│   ├── create-migration.ps1
│   └── update-database.ps1
│
├── 📄 docker-compose.yml                # 🐳 Docker конфигурация
├── 📄 Dockerfile                        # 🐳 Образ приложения
├── 📄 HabarBankAPI.sln                  # Visual Studio Solution
│
└── 📖 Документация
    ├── README.md                        # Основная документация
    ├── MIGRATIONS.md                    # Руководство по миграциям
    ├── DATABASE_SCHEMA.md               # Схема БД
    └── RUN_ME_FIRST.md                  # Быстрый старт
```

### Принципы архитектуры

#### 🎯 Clean Architecture

Каждый модуль изолирован и следует принципам:
- **Dependency Inversion** — зависимости направлены внутрь
- **Single Responsibility** — один модуль = одна ответственность
- **Separation of Concerns** — разделение по слоям

#### 🔄 Слои модуля

```
┌─────────────────────────────────────────┐
│           Presentation Layer            │  ← Controllers (HTTP endpoints)
├─────────────────────────────────────────┤
│          Application Layer              │  ← Services, DTOs, Interfaces
├─────────────────────────────────────────┤
│            Domain Layer                 │  ← Entities, Enums, Business Logic
├─────────────────────────────────────────┤
│         Infrastructure Layer            │  ← Repositories, Data Access
└─────────────────────────────────────────┘
```

---

## 🗄️ База данных

### Схема базы данных

```sql
┌─────────────┐         ┌──────────────┐
│    users    │────1:N──│    cards     │
└─────────────┘         └──────────────┘
      │                        │
      │                        │
     1:N                      N:1
      │                        │
      ▼                        ▼
┌─────────────┐         ┌──────────────┐
│   tokens    │         │   valutas    │
└─────────────┘         └──────────────┘
                               │
                              1:N
                               │
                               ▼
                        ┌──────────────┐
                        │ valuta_rates │
                        └──────────────┘

        ┌──────────────┐
        │  operations  │  ← cards (sender/recipient)
        └──────────────┘
```

### Основные таблицы

| Таблица | Описание | Ключевые поля |
|---------|----------|---------------|
| **users** | Пользователи системы | user_id, user_name, user_email, role_id |
| **cards** | Банковские карты | card_id, user_id, valuta_id, number, score |
| **operations** | История переводов | operation_id, card_sender_id, card_recipient_id |
| **valutas** | Справочник валют | valuta_id, name, letter_code (USD, EUR, RUB) |
| **valuta_rates** | Курсы валют | valuta_rates_id, valuta_id, valuta_count, rubles_count |
| **tokens** | Access токены | token_id, user_id, token, expired_at |

### Связи и каскады

| Связь | Тип | Правило удаления |
|-------|-----|------------------|
| User → Cards | 1:N | CASCADE (при удалении user удаляются его карты) |
| User → Tokens | 1:N | CASCADE |
| Card → Valuta | N:1 | RESTRICT (нельзя удалить валюту с активными картами) |
| Card → Operations | 1:N | RESTRICT (защита истории транзакций) |
| Valuta → ValutaRates | 1:N | CASCADE |

### Soft Delete

Все таблицы поддерживают **Soft Delete** через поле `deleted_at`:
- `deleted_at IS NULL` — запись активна ✅
- `deleted_at IS NOT NULL` — запись удалена (логически) 🗑️

Это позволяет сохранять историю и восстанавливать данные.

📊 **Подробная схема:** [DATABASE_SCHEMA.md](DATABASE_SCHEMA.md)

---

## 🌐 API Endpoints

### 🔐 Authentication & Access

| Method | Endpoint | Описание |
|--------|----------|----------|
| `POST` | `/api/v1/access/login` | Вход в систему |
| `POST` | `/api/v1/access/register` | Регистрация пользователя |
| `POST` | `/api/v1/access/logout` | Выход из системы |
| `GET` | `/api/v1/access/validate` | Проверка токена |

### 👤 Users

| Method | Endpoint | Описание |
|--------|----------|----------|
| `GET` | `/api/v1/users` | Список пользователей (с пагинацией) |
| `GET` | `/api/v1/users/{id}` | Получить пользователя по ID |
| `POST` | `/api/v1/users` | Создать нового пользователя |
| `PUT` | `/api/v1/users/{id}` | Обновить данные пользователя |
| `DELETE` | `/api/v1/users/{id}` | Удалить пользователя (soft delete) |

### 💳 Cards

| Method | Endpoint | Описание |
|--------|----------|----------|
| `GET` | `/api/v1/cards` | Список карт |
| `GET` | `/api/v1/cards/{id}` | Получить карту по ID |
| `GET` | `/api/v1/cards/user/{userId}` | Карты конкретного пользователя |
| `POST` | `/api/v1/cards` | Создать новую карту |
| `PUT` | `/api/v1/cards/{id}` | Обновить данные карты |
| `DELETE` | `/api/v1/cards/{id}` | Удалить карту (soft delete) |

### 💸 Operations

| Method | Endpoint | Описание |
|--------|----------|----------|
| `GET` | `/api/v1/operations` | История операций |
| `GET` | `/api/v1/operations/{id}` | Получить операцию по ID |
| `GET` | `/api/v1/operations/card/{cardId}` | Операции по карте |
| `POST` | `/api/v1/operations/transfer` | Выполнить перевод между картами |

### 💱 Valutas

| Method | Endpoint | Описание |
|--------|----------|----------|
| `GET` | `/api/v1/valutas` | Список валют |
| `GET` | `/api/v1/valutas/{id}` | Получить валюту по ID |
| `POST` | `/api/v1/valutas` | Добавить новую валюту |
| `GET` | `/api/v1/valutas/rates` | Актуальные курсы валют |
| `POST` | `/api/v1/valutas/rates` | Добавить курс валюты |

### 🏥 Health

| Method | Endpoint | Описание |
|--------|----------|----------|
| `GET` | `/api/health` | Статус работы API |

### 📚 Swagger UI

Интерактивная документация доступна по адресу:
- **Development:** http://localhost:5090/swagger
- **Docker:** http://localhost:8080/swagger

---

## ⚙️ Конфигурация

### Переменные окружения

#### appsettings.json

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=HabarBankDb;Username=postgres;Password=postgres"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

#### Docker Compose

```yaml
environment:
  ASPNETCORE_ENVIRONMENT: Development
  ASPNETCORE_HTTP_PORTS: 8080
  ConnectionStrings__DefaultConnection: "Host=postgres;Port=5432;Database=HabarBankDb;..."
```

### Настройка для разных окружений

Создайте `appsettings.Production.json` для production:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=prod-db.example.com;Port=5432;..."
  },
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Error"
    }
  }
}
```

---

## 👨‍💻 Разработка

### Установка зависимостей

```bash
# Восстановить все NuGet пакеты
dotnet restore

# Сборка решения
dotnet build

# Запуск в режиме разработки
dotnet run --project src/App/App.csproj --launch-profile http
```

### Создание нового модуля

1. Создайте структуру папок:
```
Modules/
└── NewModule/
    ├── Domain/Entities/
    ├── Application/Services/
    ├── Infrastructure/Repositories/
    └── Presentation/Controllers/
```

2. Добавьте сущность в `ApplicationDbContext`
3. Зарегистрируйте сервисы в `Program.cs`
4. Добавьте `ApplicationPart` для контроллеров

### Создание нового endpoint

```csharp
[ApiController]
[Route("api/v1/[controller]")]
public class MyController : BaseController
{
    private readonly IMyService _service;

    public MyController(IMyService service)
    {
        _service = service;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all items")]
    [SwaggerResponse(200, "Success", typeof(ApiResponse<List<MyDto>>))]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(new ApiResponse<List<MyDto>>(result));
    }
}
```

---

## 🐳 Docker

### Сборка образа

```bash
# Собрать образ приложения
docker build -t habarbank-api .

# Запустить контейнер
docker run -p 8080:8080 habarbank-api
```

### Docker Compose команды

```bash
# Запустить все сервисы
docker compose up -d

# Просмотр логов
docker compose logs -f app

# Остановить все сервисы
docker compose down

# Остановить и удалить volumes (очистка БД)
docker compose down -v

# Пересобрать и запустить
docker compose up --build
```

### Структура docker-compose.yml

```yaml
services:
  postgres:
    image: postgres:16
    environment:
      POSTGRES_DB: HabarBankDb
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: postgres
    volumes:
      - postgres_data:/var/lib/postgresql/data
    ports:
      - "5432:5432"

  app:
    build: .
    depends_on:
      - postgres
    ports:
      - "8080:8080"
    environment:
      ConnectionStrings__DefaultConnection: "Host=postgres;..."
```

---

## 🔄 Миграции

### Автоматическое применение

Миграции применяются **автоматически** при запуске приложения благодаря коду в `Program.cs`:

```csharp
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}
```

### Создание миграции

```powershell
# Через PowerShell скрипт (Windows)
.\create-migration.ps1 MigrationName

# Или напрямую через dotnet CLI
dotnet ef migrations add MigrationName --project src\App\App.csproj --output-dir Migrations
```

### Применение миграций вручную

```powershell
# Через PowerShell скрипт
.\update-database.ps1

# Или через dotnet CLI
dotnet ef database update --project src\App\App.csproj
```

### Работа с миграциями

```bash
# Список всех миграций
dotnet ef migrations list --project src\App\App.csproj

# Откат к конкретной миграции
dotnet ef database update PreviousMigration --project src\App\App.csproj

# Откат всех миграций
dotnet ef database update 0 --project src\App\App.csproj

# Удаление последней миграции (если не применена)
dotnet ef migrations remove --project src\App\App.csproj

# Генерация SQL скрипта
dotnet ef migrations script --project src\App\App.csproj --output migrations.sql
```

### Production миграции

Для production рекомендуется:

1. **Отключить автоматическое применение:**
```csharp
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}
```

2. **Применять через CI/CD pipeline:**
```bash
dotnet ef database update --project src\App\App.csproj --connection "<ProductionConnectionString>"
```

3. **Или использовать SQL скрипты:**
```bash
dotnet ef migrations script --project src\App\App.csproj --idempotent --output deploy.sql
```

📖 **Подробное руководство:** [MIGRATIONS.md](MIGRATIONS.md)

---

## 🧪 Тестирование

### Запуск тестов

```bash
# Запустить все тесты
dotnet test

# Запустить с детальным выводом
dotnet test --verbosity detailed

# Запустить тесты конкретного проекта
dotnet test tests/HabarBankAPI.UnitTests/HabarBankAPI.UnitTests.csproj
```

### Структура тестов

```
tests/
└── HabarBankAPI.UnitTests/
    ├── Domain/          # Тесты доменных сущностей
    ├── Application/     # Тесты сервисов
    ├── Infrastructure/  # Тесты репозиториев
    └── Presentation/    # Тесты контроллеров
```

### Пример unit-теста

```csharp
[Fact]
public async Task CreateUser_ValidData_ReturnsUser()
{
    // Arrange
    var service = new UserService(_mockRepository.Object);
    var userDto = new CreateUserDto { Login = "test", Email = "test@test.com" };

    // Act
    var result = await service.CreateAsync(userDto);

    // Assert
    Assert.NotNull(result);
    Assert.Equal("test", result.Login);
}
```

---

## 📚 Документация

### Документы проекта

| Документ | Описание |
|----------|----------|
| [README.md](README.md) | 📘 Основная документация (этот файл) |
| [MIGRATIONS.md](MIGRATIONS.md) | 🔄 Полное руководство по работе с миграциями EF Core |
| [DATABASE_SCHEMA.md](DATABASE_SCHEMA.md) | 📊 Детальная схема БД с ERD диаграммами |
| [RUN_ME_FIRST.md](RUN_ME_FIRST.md) | 🚀 Быстрый старт для первого запуска |
| [QUICK_START_MIGRATIONS.md](QUICK_START_MIGRATIONS.md) | ⚡ Краткая инструкция по миграциям |
| [SETUP_SUMMARY.md](SETUP_SUMMARY.md) | 📝 Резюме настройки проекта |
| [CHECKLIST.md](CHECKLIST.md) | ✅ Чеклист для первого запуска |

### Swagger/OpenAPI

Интерактивная документация API доступна через Swagger UI:
- **Локально:** http://localhost:5090/swagger
- **Docker:** http://localhost:8080/swagger

Swagger предоставляет:
- 📋 Список всех endpoints
- 🧪 Возможность тестирования запросов
- 📄 Схемы DTO и моделей
- ⚠️ Описание ошибок и статус-кодов

---

## 🔧 Полезные команды

### Разработка

```bash
# Восстановить зависимости
dotnet restore

# Сборка проекта
dotnet build

# Запуск в Development режиме
dotnet run --project src/App/App.csproj

# Запуск с профилем HTTPS
dotnet run --project src/App/App.csproj --launch-profile https

# Очистка артефактов сборки
dotnet clean
```

### Docker

```bash
# Запуск всей инфраструктуры
docker compose up -d

# Просмотр логов
docker compose logs -f

# Перезапуск сервиса
docker compose restart app

# Остановка
docker compose down

# Полная очистка (включая volumes)
docker compose down -v
```

### Миграции

```bash
# Создать миграцию
.\create-migration.ps1 <Name>

# Применить миграции
.\update-database.ps1

# Список миграций
dotnet ef migrations list --project src\App\App.csproj

# Откат миграции
dotnet ef database update <PreviousMigration> --project src\App\App.csproj
```

### База данных

```bash
# Подключение к PostgreSQL в Docker
docker exec -it <container_id> psql -U postgres -d HabarBankDb

# Просмотр таблиц
\dt

# Просмотр структуры таблицы
\d users

# Выход
\q
```

---

## 🤝 Участие в разработке

Мы приветствуем любой вклад в проект! 

### Как внести свой вклад

1. **Fork** репозиторий
2. Создайте **feature branch** (`git checkout -b feature/AmazingFeature`)
3. **Commit** изменения (`git commit -m 'Add some AmazingFeature'`)
4. **Push** в branch (`git push origin feature/AmazingFeature`)
5. Откройте **Pull Request**

### Правила разработки

- ✅ Следуйте существующему стилю кода
- ✅ Пишите unit-тесты для новой функциональности
- ✅ Обновляйте документацию при необходимости
- ✅ Убедитесь, что все тесты проходят
- ✅ Используйте осмысленные commit-сообщения

---

## 📄 Лицензия

Проект распространяется под лицензией **MIT License**.

```
MIT License

Copyright (c) 2024 HabarBank API

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction...
```

См. файл [LICENSE](LICENSE) для подробностей.

---

## 📞 Контакты и поддержка

- 📧 **Email:** support@habarbank.example
- 🐛 **Issues:** [GitHub Issues](https://github.com/your-username/HabarBankAPI/issues)
- 💬 **Discussions:** [GitHub Discussions](https://github.com/your-username/HabarBankAPI/discussions)

---

## 🙏 Благодарности

Проект построен с использованием:

- [ASP.NET Core](https://docs.microsoft.com/aspnet/core) — Web Framework
- [Entity Framework Core](https://docs.microsoft.com/ef/core) — ORM
- [PostgreSQL](https://www.postgresql.org/) — Database
- [Npgsql](https://www.npgsql.org/) — PostgreSQL Provider
- [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) — Swagger/OpenAPI
- [Docker](https://www.docker.com/) — Containerization

---

<div align="center">

**⭐ Если проект был полезен, поставьте звезду на GitHub! ⭐**

Made with ❤️ by HabarBank Team

[⬆ Вернуться к началу](#-habarbank-api)

</div>
