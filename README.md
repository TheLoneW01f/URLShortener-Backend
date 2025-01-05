# LinkShortener

LinkShortener is a powerful web application designed to help users convert long, unwieldy URLs into shorter, more manageable links. This project leverages the capabilities of ASP.NET Core and Docker to deliver a robust and scalable solution.

## Features

- **Shorten Long URLs**: Easily convert lengthy URLs into short, easy-to-share links.
- **Redirect to Original URLs**: Seamlessly redirect users to the original URL when they click on the shortened link.
- **Track Clicks**: Monitor the number of clicks each shortened URL receives, providing valuable insights into link performance.

## Prerequisites

Before you begin, ensure you have the following installed on your machine:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)
- [SQL Server](https://www.microsoft.com/en-in/sql-server/sql-server-downloads)

- We have chosen SQL Server (RDBMS) as the preferred data storage solution for several reasons:

    1. **Well Versed in RDBMS**: More hands on experience on RDBMS as compared to NoSQL
        
    2. **ACID Compliance**: SQL Server ensures ACID (Atomicity, Consistency, Isolation, Durability) properties, which are crucial for maintaining data integrity and reliability, especially in a web application where data consistency is paramount.
        
    3. **Indexing**: SQL Server provides powerful indexing capabilities, allowing us to create indexes on multiple columns. This improves query performance, especially for complex queries involving multiple tables and conditions.
        
    4. **Relational Data**: The application requires managing relationships between users, the links they generate, and the clicks on those links. An RDBMS like SQL Server is well-suited for handling such relational data with its robust support for foreign keys and join operations.
        
    5. **Scalability**: With features like partitioning, SQL Server can handle large volumes of data efficiently, making it a scalable solution as the application grows.
        
    6. **Advanced Querying**: SQL Server supports advanced querying capabilities, including stored procedures, views, and functions, which can simplify complex data operations and improve performance.
        
    7. **Security**: SQL Server offers robust security features, including encryption, authentication, and authorization, ensuring that user data is protected.

These features make SQL Server an ideal choice for our application's data storage needs.


# Setup
## Running with Docker Compose

To run the application using Docker Compose, follow these steps:
1. **Add a shared network for the API, UI and Database to communicate internally in docker**
- Open new terminal and run the below command 
    ```sh
    docker network create shared_network
    ```

2. **Build and Run the Containers**:
    ```sh
    docker-compose up -d
    ```

3. **Access the API**:
    The API will be listening on  `http://localhost:8080`.


## To Access the Web UI
1. **Clone the UI repository**
    ```sh
        git clone https://github.com/TheLoneW01f/URLShortener-Frontend.git
        cd URLShortener-Frontend
    ```
2. **Make sure the shared-network already exists by using the below command**
If not exists, it will create a shared network.
    ```
        docker network create shared_network 
    ```
3. **Build and Run the Containers**:
    ```sh
    docker-compose up -d
    ```

This setup uses Docker Compose to orchestrate the LinkShortener application and a SQL Server instance, making it easier to manage and run the entire stack with a single command.


## Additional Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Docker Documentation](https://docs.docker.com/)
