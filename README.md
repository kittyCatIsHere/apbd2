
# Equipment Rental System - APBD Tutorial 2

## Description

A C# console application for managing university
equipment rentals.

## Features

- Register students and employees
- Register laptops, cameras and projectors
- Rent and return equipment
- Check equipment availability
- Enforce student and employee rental limits
- Calculate late return penalties
- Manage equipment maintenance and damage status
- Display active and overdue rentals
- Generate summary reports

## Business Rules

- Students can have up to 2 active rentals.
- Employees can have up to 5 active rentals.
- Rental duration is between 1 and 30 days.
- Unavailable equipment cannot be rented.
- Late returns cost 10 PLN per started day.

## Technologies

- C#
- .NET 10
- Git and GitHub

## How to Run

Clone the repository:

git clone https://github.com/kittyCatIsHere/apbd2.git

Open the project directory:

cd apbd2

Build and run:

dotnet build
dotnet run

## Design Decisions

### Cohesion

Each class has a specific responsibility.
RentalService handles rental operations.
PenaltyCalculator calculates late penalties.
ReportService generates system reports.
EquipmentDisplayService displays equipment information.

### Coupling

The application separates domain models from services.
RentalService operates on the abstract Equipment type
instead of depending on concrete equipment classes.

### SOLID Principles

Single Responsibility Principle:
Different responsibilities are assigned to separate classes.

Open/Closed Principle:
New equipment types can extend the Equipment class
without modifying existing equipment subclasses.

### Inheritance and Polymorphism

Laptop, Camera and Projector inherit from Equipment.
Each type overrides the GetDetails method.

## Testing

The application demonstrates successful rentals,
invalid operations, student and employee rental limits,
on-time returns, late penalties, and rental duration checks.

## Git Workflow

Two feature branches were used:

- feature/equipment-management
- feature/rental-improvements

Both branches were merged into main.
