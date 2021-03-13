# Command and Query Responsibility Segregation (CQRS) Demo

> Code for this post - https://carlpaton.github.io/2020/08/cqrs/

Quoted from docs.microsoft.com - This pattern isn't recommended when:

- The domain or the business rules are simple.
- A simple CRUD-style user interface and data access operations are sufficient.

Consider applying CQRS to limited sections of your system where it will be most valuable.

This demo code is however simple CRUD for `Todo` items as this was the simplest way to demonstrate the `Command` / `Queries`. Most of the code is based on the work done by [Jonathan James Williams](https://github.com/jonathanjameswilliams26/CQRSInDotnetCore) - check out his sweet [YouTube video CQRS using C# and MediatR](https://www.youtube.com/watch?v=mdzEKGlH0_Q)

## Setup Swagger

Swagger is a UI (User Interface) for your API, I installed and configured as follows

1. Install [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)

2. Configure services container

```c#
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new   { Title = "CqrsDemo.Api", Version = "v1" });
});
```

3. Configure request pipeline

```c#
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
});
```

4. Update `Properties/launchSettings.json` profiles `launchUrl` property to be set to the swagger route.

```c#
 "launchUrl": "swagger",
```

## References

- https://github.com/jonathanjameswilliams26/CQRSInDotnetCore
- https://www.sqlitetutorial.net/
- https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle
- https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice