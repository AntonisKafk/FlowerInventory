# Flower Inventory System

## Overview
This project is a full-stack web application that allows users to manage flowers and categories in a flower shop. Users can view, add, edit, and delete flowers and categories, as well as search and sort flowers efficiently.

---

## Features Summary

### Database
- SQL Server database with Flower and Category tables.
- One-to-many relationship: each category can have multiple flowers.
### Backend
- Service layer for handling CRUD operations for flowers and categories.
- Exception handling and validation included.
- Unit tests implemented using NUnit.
- Logging for each function, using ILogger

### Frontend
- Razor Pages with responsive Bootstrap layout.
- Home page (Index): displays a list of flowers categories, users can view and manage flowers and categories, with search and sort functionality for flowers page.
- Flower page: view all or category specific flowers, with add, edit, delete and details functionality.
- Manage page (ManageCategory & ManageFlower): two distinct pages for updating/adding a new category/flower.
- Flower Details page: shows name, price, category of selected flower with option for delete and edit.
- Search & Sort: client-side search by flower name and ascending/descending sort.
---

## Setup Instructions
1. Clone repository
2. Run `database_setup.sql` from Database folder
3. Build project and run

## Assumptions and Design Choices
- Used `CategoryId` as a foreign key in the Flowers table to establish a one-to-many relationship between `Categories` and `Flowers`.
- Used `ApplicationDbContext` as the Entity Framework Core context to interact with the SQL Server database.
- Merged Edit and Add pages into one Manage page having functionality for both.
- Used browser's built in dialog for deletion confirmation.
- Search and sort are performed client-side for performance on small data sets.
- Used TempData for navigation purposes.

## Challenges and Notes
- Managing JSON serialization cycles between categories and flowers.
- Managing redirection on deletion inside the details page
- Ensuring category selected preservation when navigating to and from manage, details pages

## Technologies Used
- **Database**: SQL Server (preinstalled with Visual Studio)
- **Backend**: .NET 8 Web App with Razor Pages
- **Frontend**: Javascript, HTML5, Bootstrap 5 for responsive design
- **Testing**: NUnit for unit testing