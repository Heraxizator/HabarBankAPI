# PowerShell скрипт для создания EF Core миграции
param(
    [Parameter(Mandatory=$false)]
    [string]$MigrationName = "InitialCreate"
)

Write-Host "Создание миграции: $MigrationName" -ForegroundColor Green
Write-Host ""

$projectPath = "src\App\App.csproj"
$outputDir = "Migrations"

# Проверка существования проекта
if (-not (Test-Path $projectPath)) {
    Write-Host "Ошибка: Проект не найден по пути $projectPath" -ForegroundColor Red
    exit 1
}

# Создание миграции
Write-Host "Выполнение команды: dotnet ef migrations add $MigrationName --project $projectPath --output-dir $outputDir" -ForegroundColor Yellow
dotnet ef migrations add $MigrationName --project $projectPath --output-dir $outputDir

if ($LASTEXITCODE -eq 0) {
    Write-Host ""
    Write-Host "✅ Миграция '$MigrationName' успешно создана!" -ForegroundColor Green
    Write-Host ""
    Write-Host "Следующие шаги:" -ForegroundColor Cyan
    Write-Host "  1. Проверьте созданные файлы в папке src\App\Migrations" -ForegroundColor White
    Write-Host "  2. Примените миграцию: .\update-database.ps1" -ForegroundColor White
    Write-Host "     или просто запустите приложение (миграции применяются автоматически)" -ForegroundColor White
} else {
    Write-Host ""
    Write-Host "❌ Ошибка при создании миграции" -ForegroundColor Red
    exit 1
}

