# E-Commerce-Sv
------
Developed E-commerce site as backend. No frontend applied on this project.

## Technologies Used
-ASP .Net Core MVC
-Entity Framework Core
-SQL

## Features

- **Authentication**: Implemented .NET Identity scaffold authentication with roles (ADMIN and Customer) for site maintenance and user access control.

- **E-commerce Functionality**: Developed essential e-commerce features including a shopping cart, wishlist, and a comment section.

- **Database Connectivity**: Utilized Entity Framework Core as the Object-Relational Mapping (ORM) tool to connect with the database, making data operations efficient and flexible with LINQ.

- **DAL and Repository Pattern**: Implemented a Data Access Layer (DAL) for improved code readability and testability, along with the Repository pattern for structured data access.

- **Unit of Work**: Experimented with the Unit of Work pattern to manage transactions and data operations more effectively.

- **Payment System**: Integrated Stripe API for online payment processing, enhancing the platform's usability for online shopping.

//How to start application
# Clone the repository
git clone https://github.com/vuklalovic194/E-commerce-Sv.git

# Navigate to the project directory
cd your-repo

# Install dependencies (if applicable)
npm install

# Configure environment variables
cp .env.example .env

# Run the application
dotnet run


