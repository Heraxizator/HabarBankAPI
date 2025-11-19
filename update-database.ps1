# PowerShell скрипт для применения EF Core миграций
Write-Host "Применение миграций к базе данных..." -ForegroundColor Green
Write-Host ""

$projectPath = "src\App\App.csproj"

# Проверка существования проекта
if (-not (Test-Path $projectPath)) {
    Write-Host "Ошибка: Проект не найден по пути $projectPath" -ForegroundColor Red
    exit 1
}

# Применение миграций
Write-Host "Выполнение команды: dotnet ef database update --project $projectPath" -ForegroundColor Yellow
dotnet ef database update --project $projectPath

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✅ Миграции успешно применены!" -ForegroundColor Green
    Write-Host ""
    Write-Host "База данных обновлена и готова к работе" -ForegroundColor Cyan
} else {
    Write-Host ""
    Write-Host "❌ Ошибка при применении миграций" -ForegroundColor Red
    Write-Host ""
    Write-Host "Возможные причины:" -ForegroundColor Yellow
    Write-Host "  - База данных недоступна (проверьте строку подключения в appsettings.json)" -ForegroundColor White
    Write-Host "  - PostgreSQL не запущен (запустите docker compose up -d для локальной БД)" -ForegroundColor White
    exit 1
}

