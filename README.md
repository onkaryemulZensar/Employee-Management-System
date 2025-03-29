# Employee Management System

This repository contains the source code and database schema for an Employee Management System. The system is designed to manage employees, departments, and managerial relationships within an organization.

## Table of Contents

- [Database Schema](#database-schema)
- [Getting Started](#getting-started)

## Database Schema

The database schema includes the following tables:

1. **Employee**: Stores details about employees.
2. **Department**: Stores department information.
3. **Manager**: Represents management relationships where an employee can have multiple managers.
4. **EmployeeDepartment**: Represents a many-to-many relationship between employees and departments.

## Getting Started

1. Update the connection string in `Program.cs` to match your SQL Server configuration.
2. Execute the SQL script `employeeManagementSystem.sql` to create the database schema.
