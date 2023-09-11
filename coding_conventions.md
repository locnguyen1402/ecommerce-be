# Coding Conventions

## Service

### Structure

```
SERVICE_NAME
    ├── SERVICE_NAME.API
    │   ├── Application
    │   │   ├── Commands
    │   │   │   ├── <CommandName>Command.cs
    │   │   │   └── <CommandName>CommandHandler.cs
    │   │   ├── Queries
    │   │   │   └── <QueryName>Query.cs
    │   │   ├── Requests
    │   │   │   └── <RequestName>Request.cs
    │   │   ├── Responses
    │   │   │   └── <ResponseName>Response.cs
    │   │   └── Validations
    │   │       └── <ValidatorName>Validator.cs
    │   └── Controllers
    │       └── <ControllerName>Controller.cs
    ├── SERVICE_NAME.Domain
    │   └── AggregatesModel
    └── SERVICE_NAME.Infrastructure
        ├── EntityConfigurations
        └── Repositories
```

### Notes

#### SERVICE_NAME.API

This is a dotnet project

- Application folder (centralized business code block)

  - Commands - apply mediator pattern (can be used like request body) [preference](https://github.com/jbogard/MediatR)
  - Queries - used for GET apis
  - Requests - present for request body for POST, PUT,... apis
  - Responses - DTO model for api response
  - Validations - [preference](https://docs.fluentvalidation.net)

#### SERVICE_NAME.Domain

This is a dotnet package like data layer to communicate between service and database or between services

- AggregatesModels folder (contains main models in service such as OrderDetail in ordering service, Product in product service,...)

#### SERVICE_NAME.Infrastructure

This is a dotnet package

- EntityConfigurations folder

  - [preference](https://learn.microsoft.com/en-us/ef/ef6/modeling/code-first/fluent/relationships)

### Naming

- namespace <SERVICE_NAME>.<folder_1>.<folder_2>...
