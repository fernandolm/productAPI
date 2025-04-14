**Vertical Slice Architecture with MediatR**

**Overview**

This project is a C# solution that implements Vertical Slice Architecture to structure the application and uses MediatR as the mediator pattern to handle API commands and queries. The goal of this architecture is to simplify the development process by focusing on individual features (or "slices") rather than traditional horizontal layers like controllers, services, and repositories.
By adopting Vertical Slice Architecture, each feature is self-contained, making the codebase more modular, maintainable, and scalable.

**Key Features**
- Vertical Slice Architecture: Each feature (or use case) is implemented as an independent slice containing all the necessary components such as request models, handlers, validations, and persistence logic.
- Minimal API: Provides a lightweight approach to defining endpoints directly in code without requiring controllers, making the API concise and easy to maintain.
- MediatR Integration: MediatR is used to handle commands and queries, decoupling the API from the business logic.
- Clean Separation of Concerns: Each slice encapsulates its own logic, reducing cross-dependencies between features.

**Technologies Used**
- .NET 7: For building a high-performance API.
- Minimal API: A streamlined approach to defining web APIs.
- MediatR: To coordinate requests and responses in a decoupled manner.
- Entity Framework Core: For database access and persistence.
- FluentValidation: For request validation within each slice.
- Dependency Injection (DI): Built-in .NET DI container for managing dependencies.

**Project Structure**

The project is organized into feature-based folders instead of traditional layers like "Controllers" or "Services." Each folder represents a vertical slice of functionality.
Each feature (e.g., CreateProduct, GetProducts) has its own folder containing:
- Command/Query classes (CreateProductCommand.cs, GetProductQuery.cs).
- Handlers (CreateProductCommandHandler.cs, GetProductQueryHandler.cs) that process requests using MediatR.
- Validators (CreateProductValidator.cs) for input validation using FluentValidation.

**How It Works**

API Endpoint: A controller receives an HTTP request and forwards it to MediatR.
Command/Query: The request is encapsulated in a command or query object.
Handler: MediatR routes the command/query to its corresponding handler, where the business logic is executed.
Validation: Input validation occurs using FluentValidation before processing the request.

**Example Workflow**
Use Case: Creating a Product
1. The client sends a POST request to /products with product details in the body.
2. The CreateProductCommand class encapsulates the product details.
3. The CreateProductValidator validates the input data (e.g., checking required fields).
4. The CreateProductHandler processes the command by:
4.1. Saving order data to the database using Entity Framework Core.
4.2. Returning a success response.
5. The API sends a success response back to the client.

**Prerequisites**
- .NET 7 SDK installed on your machine.
- SQL Server database for persistence.

**Steps to Run Locally**
1. Clone this repository:
```
git clone https://github.com/fernandolm/productAPI.git
cd productAPI
```
2. Restore dependencies:
```
dotnet restore
```
3. Run the application:
```
dotnet run
```
