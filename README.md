# URL Shortener API

Це бекенд частина проєкту **URL Shortener**, написана на **.NET 8** з використанням **ASP.NET Core Web API**.  
Вона відповідає за створення коротких посилань, зберігання інформації про користувачів та забезпечує REST API для фронтенду.

## Технології
 .NET 8 / ASP.NET Core Web API
 Entity Framework Core (SQL Server)
 Identity (користувачі та авторизація)
 xUnit (юніт-тести)

## Запуск
1. Відкрити проект у Visual Studio або JetBrains Rider.
2. Переконатися, що у файлі `appsettings.json` вказано коректне підключення до бази даних.
3. Виконати команду:
   dotnet run --project UrlShortener.Api

API буде доступне за адресою:

https://localhost:7235

Основні ендпоінти:

GET /api/shorturls — отримати всі короткі посилання.

POST /api/shorturls — створити нове коротке посилання.

GET /api/shorturls/{shortCode} — отримати оригінальне посилання за коротким кодом.
