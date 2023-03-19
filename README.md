# Project Description

This is a project for a Content Management System (CMS) called CMSPlus. It consists of multiple projects, including:

- **CMSPlus.Application:** This project contains the application layer of the system, which provides the business logic and acts as a mediator between the presentation layer and the data layer. It contains services for handling blogs, topics, comments, files, and email, as well as a configurator for configuring the system.
- **CMSPlus.Domain:** This project contains the domain layer of the system, which provides the entities and interfaces for the system. It contains entities for blogs, topics, and comments, as well as interfaces for repositories and services.
- **CMSPlus.Infrastructure:** This project contains the data layer of the system, which provides the implementation of the repositories and the database context. It contains repositories for blogs, topics, and comments, as well as a migration service for managing database migrations.
- **CMSPlus.Presentation:** This project contains the presentation layer of the system, which provides the user interface and handles user input. It contains controllers for handling accounts, blogs, topics, roles, and users, as well as models and validators for input validation.

The project uses ASP.NET Core, C#, Entity Framework Core, Evolve for migrations, FluentApi and FluentValidation and is structured according to the Clean Architecture pattern. It also uses AutoMapper for mapping between entities and models, and uses custom policies for authorization based on user permissions.
