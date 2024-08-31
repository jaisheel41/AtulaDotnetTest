# AtulaDotnetTest

## Features

- **Product Management**: Add, edit, and delete products with associated categories.
- **Categories**: Categorize products into categories such as Tables, Chairs, and Sofas.
- **User Authentication**: Secure login system with email and password validation.
- **DataTables Integration**: Use of DataTables for dynamic product listings with search and pagination.
- **Validation**: Client-side validation using jQuery and server-side validation using FluentValidation.
- **Notifications**: Toastr notifications for CRUD operations.
- It also includes user authentication and role-based authorization using ASP.NET Identity.

## Technologies Used

- **ASP.NET Core MVC**
- **Entity Framework Core**
- **ASP.NET Identity**
- **MySQL**
- **DataTables**
- **jQuery**
- **Toastr**
- **FluentValidation**

## Prerequisites

- **.NET 6.0 SDK** or later
- **MySQL** installed and running
- **Visual Studio 2022** or later
- **Git** for version control

## Setup Instructions

1. **Clone the Repository**:
   ```sh
   git clone https://github.com/yourusername/AtulaDotnetTest.git
   cd AtulaDotnetTest

2. **Setup Database**:

Create a MySQL database named atuladotnettestdb.
Update the connection string in appsettings.json with your MySQL credentials.

3. **Apply Migrations**:
Update-Database

4. **Run the Application**

5. **Access the Application**

## Database Seeding
The application seeds the database with the following initial data:

### Categories:
Table
Chair
Sofa

### Products:
SKUA: Lorem Table (Category: Table)
SKUB: Ipsum Table (Category: Table)
SKUC: Dolor Table (Category: Table)
SKUD: Sit Chair (Category: Chair)
SKUE: Amet Chair (Category: Chair)
SKUF: Consectetur Chair (Category: Chair)
SKUG: Adipiscing Sofa (Category: Sofa)
SKUH: Elit Sofa (Category: Sofa)
SKUI: Mauris Sofa (Category: Sofa)

### Admin User:
Email: admin@example.com
Password: Admin@123
Role: Admin

### To Test the Application
Login:

Use the seeded admin credentials: admin@example.com / Admin@123.

Manage Products:
Create, edit, and delete products.
Manage Categories:

Assign categories to products.
Register New Users:

Use the "Register" link on the login page to create new users.


