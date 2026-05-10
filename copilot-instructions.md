# Project Architecture

* ASP.NET Core Clean Architecture
* CQRS with MediatR
* FluentValidation
* Result Pattern
* Localization
* Repository Pattern

# Rules

* Endpoints must be thin
* Business logic only in handlers
* Use async/await
* Use cancellation token
* Use DTOs
* Validators for all commands
* Follow existing folder structure
* Return Result<T>
* Use localization messages
* Use pagination pattern from existing modules
* Use LocalizedDto for localized fields
* Do not overwrite old FK value when null in update
* Prevent duplicate localized names
* Use XML comments for Swagger documentation
