# BookStoreServer

BookStoreServer is a backend server application for managing a book store. It provides functionalities for user authentication, book services, order processing, and real-time notifications.

## Features

- **User Authentication**: Supports JWT-based authentication.
- **Book Management**: Services for managing books in the store.
- **Order Processing**: Handles order creation, updates, and notifications.
- **Real-Time Notifications**: Uses SignalR for real-time updates and notifications.
- **API Documentation**: Integrated with Swagger for API documentation.

## Getting Started

### Prerequisites

- Docker
- Docker Compose

### Installation

1. Clone the repository:  
    git clone https://github.com/your-repo/BookStoreServer.git
    cd BookStoreServer

2. Run the application using Docker Compose:
    docker-compose up --build

### Running the Application
    
1. The server will start at `http://0.0.0.0:5000`.

### API Documentation

- Access the Swagger UI at `http://localhost:5000/swagger`.

## Configuration

The application uses `appsettings.json` for configuration. Key settings include:

- **Jwt**: Settings for JWT authentication.
- **EmailSettings**: Configuration for email services.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License.
