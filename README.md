URL Shortener API

This is the backend part of the URL Shortener project, built with .NET 8 using ASP.NET Core Web API.
It handles the creation of short URLs, stores user information, and provides a REST API for the frontend.

Technologies

.NET 8 / ASP.NET Core Web API

Entity Framework Core (SQL Server)

Identity (users and authentication)

xUnit (unit testing)

Running the API

Open the project in Visual Studio or JetBrains Rider.

Make sure the appsettings.json file contains the correct connection string to your database.

Run the project with:

dotnet run --project UrlShortener.Api


The API will be available at:

https://localhost:7235

Main Endpoints

GET /api/shorturls — get all short URLs.

POST /api/shorturls — create a new short URL.

GET /api/shorturls/{shortCode} — get the original URL by short code
