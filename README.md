# BlogEngineNet

This is a solution developed with .NET6, ASP.NET, efCore code first and SQL Server. I tried to apply a clean architecture with 3 project:
- Core: All business logic is here and definition of persistence
- Persistence: Implementation of context interface and all thing entities.
- API: Web application that exposes the endpoints to consume the business logic.

## Getting started

- Cloning code
```
git clone https://github.com/ftinoco/blog-engine-net.git
```

- Build the whole solution in order to install all packages.
- Create database called "BlogEngineDB" in your local instance.
- Change string connection in "BlogEngineNet.API/appsettings.json" with your credentials.
- On Package Manager Console, select the project "BlogEngineNet.Persistence" and run the following command:
```
update-database -verbose
```
![](./docs/images/updating_database.jpg)

## Software requirements

- .NET6 SKD installed.
- A sql instance must be in our local environment or in the cloud.

## Considerations
- The development of this project took me about 16 hours.
- There is a collection of postman in the docs folder, where we can find the way to call the endpoints.

## Availability

The application is available through [BlogEngineNetApi](https://blogenginenetapi.azure-api.net), that's an Azure App Service connected to database in Azure as well. There are two environment files in the docs folder to import, development and production with this url configured.