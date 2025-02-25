# BookStore# BookStore

## Overview
The `BookStore` project is designed to manage book inventory and notify suppliers when stock is low. It includes services for handling low stock books, notifying suppliers, and managing book data.

## Project Structure
- **BookStoreServer.Service**: Contains the services for handling low stock books and notifying suppliers.
- **BookStoreServer.Api.Entities**: Contains the data transfer objects (DTOs) for the project.
- **BookStoreServer.Repository.Interfaces**: Contains the repository interfaces for accessing book data.

## Key Components
### HandleLowStockBooks
This service runs in the background and periodically checks for low stock books. It notifies suppliers when a book's stock is low.

#### Key Methods:
- `ExecuteAsync(CancellationToken stoppingToken)`: Starts the service and periodically checks for low stock books.
- `CheckLowStockBooks()`: Checks the repository for books that are low in stock and adds them to a concurrent bag.
- `NotifySuppliers()`: Notifies suppliers about low stock books using the `IStoreHub` interface.

### SuppliersClientService
This service manages the connection to the suppliers and sends messages to the server.

#### Key Methods:
- `StartAsync()`: Starts the connection to the suppliers.
- `SendMessageToServer(string message)`: Sends a message to the server.

### BookDTO
This data transfer object represents a book in the system.

#### Key Properties:
- `Id`: The unique identifier for the book.
- `Title`: The title of the book.
- `Description`: A description of the book.
- `PublishedDate`: The date the book was published.
- `Price`: The price of the book.
- `Amount`: The amount of the book in stock.

### IStoreHub
This interface defines the method for sending notifications to suppliers.

#### Key Methods:
- `SendSuppliersNotification(string title)`: Sends a notification to suppliers.

## Getting Started
1. Clone the repository.
2. Open the solution in Visual Studio 2022.
3. Build the solution to restore the dependencies.
4. Run the project.

## Dependencies
- .NET 6.0
- Microsoft.Extensions.Hosting
- Microsoft.Extensions.DependencyInjection
- Microsoft.Extensions.Logging
- Microsoft.AspNetCore.SignalR

## Contributing
Contributions are welcome! Please submit a pull request or open an issue to discuss your changes.

## License
This project is licensed under the MIT License.
