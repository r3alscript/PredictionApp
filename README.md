# PredictionApp

A web API built with **ASP.NET Core** using **Clean Architecture**, **Domain-Driven Design (DDD)**, and **Event-Driven Design (EDD)** principles.  
The application provides **random predictions** and **motivational messages** with database storage (SQlite), event handling, validation, DTO mapping, and caching via **Memcached**.

---

## Project Overview

This project was created as part of software architecture and backend development practice to demonstrate key enterprise patterns in .NET:

- **Clean Architecture** structure  
- **DDD (Domain-Driven Design)** models and layers  
- **Event-Driven Design (EDD)** with domain events and handlers  
- **Dependency Injection (DI)** as part of DIP
- **SOLID** principle compliance  
- **Repository & Unit of Work** patterns  
- **AutoMapper** for DTO conversion  
- **FluentValidation** for data validation  
- **Memcached** caching for performance optimization  

---

## Implemented Features

### PZ-1 / LB-1 — Clean Architecture Setup
- Project divided into layers:  
  `PredictionApp` (Presentation) → `PredictionApp.Domain` → `PredictionApp.Infrastructure`
- Domain models: `Prediction`, `Motivation`
- Database context: `PredictionAppDbContext` with EF Core (SQLite)

### PZ-2 / LB-2 — Domain-Driven Design (DDD)
- Domain layer contains **entities**, **interfaces**, and **services**
- Business logic implemented through `PredictionService` and `MotivationService`
- Each service depends only on abstractions (`IUnitOfWork`, `IRepository<T>`)

### PZ-3 / LB-3 — Event-Driven Design
- Implemented events:  
  `PredictionCreatedEvent`, `MotivationCreatedEvent`
- Each event triggers a corresponding event handler through `IEventHandler<T>`

### PZ-4 / LB-4 — SOLID & Dependency Injection
- Registered all services in `Program.cs` with lifetimes:
  - `Singleton` – utilitie `IRandomProvider`  
  - `Scoped` – database context, services and repositories  
  - `Transient` – utilitie `IDateTimeProvider`
- Refactored services to follow **Single Responsibility** and **Dependency Inversion**

### PZ-5 / LB-5 — Repository & Unit of Work
- Generic `Repository<T>` with methods:  
  `GetById`, `GetAll`, `Add`, `Remove`
- Coordinated by `UnitOfWork` with repositories:  
  `Predictions`, `Motivations`
- Controllers access data only through `IUnitOfWork`

### PZ-6 / LB-6 — DTO & AutoMapper
- Introduced DTO classes: `PredictionDto`, `MotivationDto`
- Configured AutoMapper profiles to map between Entities and DTOs

### PZ-7 / LB-7 — FluentValidation
- Input data validation implemented using **FluentValidation**
- Registered validators for each entity via

### PZ-8 / LB-8 — Caching (Memcached)
- Integrated **Memcached** through `EnyimMemcachedCore`
- Cached **entire lists** of predictions and motivations for 5 minutes  
- Each API call returns a **random** item from cached data  
- Greatly reduces database load while maintaining randomness

---

## How to Run the Project

### 1. Requirements
- **.NET SDK 8.0+**
- **Docker Desktop** *(for Memcached)*

### 2. Clone and Build
```bash
git clone https://github.com/r3alscript/PredictionApp.git
cd PredictionApp
dotnet build
```

### 3. Run Memcached

#### Using Docker (recommended)
```bash
docker run -d --name memcached -p 11211:11211 memcached:latest
```

### 4. Run the API
```bash
dotnet run
```
- Or use coderunner

