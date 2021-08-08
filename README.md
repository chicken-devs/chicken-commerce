# chicken-ecommerce

A playground where the Code Lovers practice their skills.

> **Feedback** with improvements and pull requests from the community will be highly appreciated. Thank you!

# High level context

![](https://raw.githubusercontent.com/minhhuyen93/chicken-commerce/9f7e3b813febaef822a54453a711f53823b26c7a/out/Architech_Document/overall-architecture/ChickenCommerce.svg)

# Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet/6.0): 6.0.100-preview.5.21271.2
- Dev tools:
  - [vscode REST Client](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) extension
  - [vscode PlantUML](https://marketplace.visualstudio.com/items?itemName=jebbs.plantuml) extension

# :hearts: Technical stacks

- ✔️ **[`.NET Core 6`](https://dotnet.microsoft.com/download)** - .NET Framework and .NET Core, including ASP.NET and ASP.NET Core
- ✔️ **[`MVC Versioning API`](https://github.com/microsoft/aspnet-api-versioning)** - Set of libraries which add service API versioning to ASP.NET Web API, OData with ASP.NET Web API, and ASP.NET Core
- ✔️ **[`MongoDb .Net Core Driver`](https://mongodb.github.io/mongo-csharp-driver/2.13/getting_started/installation/)** - The official MongoDB C#/.NET Driver provides asynchronous interaction with MongoDB.
- ✔️ **[`FluentValidation`](https://github.com/FluentValidation/FluentValidation)** - Popular .NET validation library for building strongly-typed validation rules
- ✔️ **[`Swagger & Swagger UI`](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)** - Swagger tools for documenting API's built on ASP.NET Core
- ✔️ **[`serilog`](https://github.com/serilog/serilog)** - Simple .NET logging with fully-structured events

## ✔️ Building blocks

```bash
+---CKE.Host
|   |   CKE.Host.csproj
|   |   .dockerignore
|   |   appsettings.json
|   |   Dockerfile
|   |   CKE.Host.csproj
|   |   Program.cs
|   |
|   \---Properties
|       launchSettings.json
|
+---CKE.Infra
|   |   CKE.Infra.csproj
|   |   AppOptions.cs
|   |   Extensions.cs
|   |
|   +---Auth
|   |   AuthBehavior.cs
|   |   Extensions.cs
|   |   IAuthRequest.cs
|   |   ISecurityContextAccessor.cs
|   |   SecurityContextAccessor.csc
|   |
|   +---Database
|   |   |
|   |   \---MongoDb
|   |      MongoDbOptions.cs
|   |
|   +---Bus
|   |   |   Extensions.cs
|   |   |   IEventBus.cs
|   |   |   EventBusBase.cs
|   |   |
|   |   \---Kafka
|   |       |   KafkaEventBusOptions.cs
|   |       |
|   |       \---Internal
|   |           KafkaEventBus.cs
|   |
|   +---Logging
|   |   Extensions.cs
|   |   LoggingBehavior.cs
|   |   TraceIdEnricher.cs
|   |
|   +---Swagger
|   |   ConfigureSwaggerOptions.cs
|   |   Extentions.cs
|   |   SwaggerDefaultValues.cs
|   |
+---CKE.Shared
|   |   CKE.Shared.csproj
|   |
|   +---Helpers
|   |   DateTimeHelper.cs
|   |   GuidHelper.cs
|   |
|   +---Extensions
|   |   DateTimeExtensions.cs
|   |   GuidExtensions.cs
|   |   StringExtensions.cs
|   |
\---CKE.Modules
    |
    \---Sample
       |   Sample.project.cs
       |
       +---EntryPoints
       |   |
       |   +---Api
       |   |   SampleControllers.cs
       |   |   ViewModels.cs
       |   |
       |   +---gRPC
       |   |   SampleGrpcServices.cs
       |   |   GrpcModels.cs
       |   |
       |   \---Events
       |       SampleEventHandlers.cs
       |       EventModels.cs
       |
       +---Infra
       |   |   SampleRepository.cs
       |   |
       |   \---Events
       |       SampleEvents.cs
       |
       \---Biz
           |   ISampleRepository.cs
           |   SampleService.cs
           |   SampleService.cs
           |   SampleBizModel.cs
           |
           \---Dtos
               SampleCreateDto.cs

```

## ✔️ Implementation (Monolith)

```bash
+---CKE.Host
|   |   CKE.Host.csproj
|   |   .dockerignore
|   |   appsettings.json
|   |   Dockerfile
|   |   CKE.Host.csproj
|   |   Program.cs
|   |
|   +---Properties
|   |    launchSettings.json
|   |
+---CKE.Infra
|   |   CKE.Infra.csproj
|   |   AppOptions.cs
|   |   Extensions.cs
|   |
|   +---Auth
|   |   AuthBehavior.cs
|   |   Extensions.cs
|   |   IAuthRequest.cs
|   |   ISecurityContextAccessor.cs
|   |   SecurityContextAccessor.csc
|   |
|   +---Database
|   |   |
|   |   \---MongoDb
|   |       MongoDbOptions.cs
|   |
|   +---Bus
|   |   |   Extensions.cs
|   |   |   IEventBus.cs
|   |   |   EventBusBase.cs
|   |   |
|   |   \---Kafka
|   |       |   KafkaEventBusOptions.cs
|   |       |
|   |       \---Internal
|   |           KafkaEventBus.cs
|   |
|   +---Logging
|   |   Extensions.cs
|   |   LoggingBehavior.cs
|   |   TraceIdEnricher.cs
|   |
|   +---Swagger
|   |   ConfigureSwaggerOptions.cs
|   |   Extentions.cs
|   |   SwaggerDefaultValues.cs
|   |
+---CKE.Shared
|   |   CKE.Shared.csproj
|   |
|   +---Helpers
|   |   DateTimeHelper.cs
|   |   GuidHelper.cs
|   |
|   +---Extensions
|   |   DateTimeExtensions.cs
|   |   GuidExtensions.cs
|   |   StringExtensions.cs
|   |
\---CKE.Modules
    |
    +---Core.Account
    |   |   Core.Account.project.cs
    |   |
    |   +---EntryPoints
    |   |   |
    |   |   +---Api
    |   |   |   AccountControllers.cs
    |   |   |   ViewModels.cs
    |   |   |
    |   |   +---gRPC
    |   |   |   AccountGrpcServices.cs
    |   |   |   GrpcModels.cs
    |   |   |
    |   |   \---Events
    |   |       AccountEventHandlers.cs
    |   |       EventModels.cs
    |   |
    |   +---Infra
    |   |   |   AccountRepository.cs
    |   |   |
    |   |   \---Events
    |   |       AccountEvents.cs
    |   |
    |   \---Biz
    |       |   IAccountRepository.cs
    |       |   IAccountService.cs
    |       |   AccountService.cs
    |       |   AccountBizModel.cs
    |       |
    |       \---Dtos
    |           AccountRegisterDto.cs
    |
    \---Catalog
        |   |   Catalog.project.cs
        |   |
        |   +---EntryPoints
        |   |   |
        |   |   +---Api
        |   |   |   CatalogControllers.cs
        |   |   |   ViewModels.cs
        |   |   |
        |   |   +---gRPC
        |   |   |   CatalogGrpcServices.cs
        |   |   |   GrpcModels.cs
        |   |   |
        |   |   \---Events
        |   |       CatalogEventHandlers.cs
        |   |       EventModels.cs
        |   |
        |   +---Infra
        |   |   CatalogRepository.cs
        |   |
        |   \---Events
        |       CatalogEvents.cs
        |
        \---Biz
            |   ICatalogRepository.cs
            |   CatalogService.cs
            |   CatalogService.cs
            |   CatalogBizModel.cs
            |
            \---Dtos
                CatalogCreateDto.cs
```

# Works

- **:hammer_and_pick: App Structure**
  - Category: **Base**
  - Contributor: [Viet Pham](https://github.com/vietphamh)
- **:hammer_and_pick: Authentication (Session/ OAuth 2/ OpenID Connect)**
  - Category: **Core Features**
  - Contributor: n/a
- **:hammer_and_pick: Account**
  - Category: **Core Features**
  - Contributor: n/a
- **:hammer_and_pick: Catalog**
  - Category: **Biz Features**
  - Contributor: n/a
- **:hammer_and_pick: Order**
  - Category: **Biz Features**
  - Contributor: n/a
- **:hammer_and_pick: Notification**
  - Category: **Biz Features**
  - Contributor: n/a
- ... TBD

# Get Started

// TBD

# Credits

- https://github.com/thangchung/clean-architecture-dotnet
- https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures
- https://github.com/zkavtaskin/Domain-Driven-Design-Example
- https://github.com/ardalis/CleanArchitecture
- https://github.com/jasontaylordev/CleanArchitecture
- https://github.com/ThreeDotsLabs/wild-workouts-go-ddd-example
- [C4 PlaintUML Model](https://github.com/plantuml-stdlib/C4-PlantUML/blob/master/samples/C4CoreDiagrams.md)
- [Real world PlantUML](https://real-world-plantuml.com)
