# 🧑‍💼 Employee Management System | .NET 8 Blazor WASM & Web API 🚀

A complete **Employee Management System** built with **.NET 8**, featuring **Blazor WebAssembly**, **ASP.NET Web API**, **Entity Framework Core**, and **JWT Authentication**.  
The project demonstrates how to build a **modular, scalable enterprise application** from scratch using clean architecture principles, with full **CRUD**, **PDF export**, **print**, and **authentication** functionality.

---

## 📋 Table of Contents
- [📖 Overview](#-overview)
- [🧱 Project Structure](#-project-structure)
- [⚙️ Tech Stack](#️-tech-stack)
- [🏗️ Architecture Diagram](#️-architecture-diagram)
- [✨ Features](#-features)
- [🚀 Getting Started](#-getting-started)
- [🧪 Testing](#-testing)
- [📸 Screenshots](#-screenshots)
- [🎬 Development Timeline](#-development-timeline)
- [👨‍💻 Author](#-author)

---

## 📖 Overview

This system manages employees, departments, vacations, and sanctions through a clean, role-based interface.  
It supports **secure authentication**, **token-based API communication**, and **data export (PDF & print)** — built using a real-world modular structure that separates client, server, and shared libraries.

---

## 🧱 Project Structure

EmployeeManagementSystem/
│
├── BaseLibrary/ # Shared Data Models (Entities, DTOs, Responses)
│ ├── Entities/
│ ├── DTOs/
│ ├── Responses/
│ └── Class1.cs
│
├── Client/ # Blazor WebAssembly Frontend
│ ├── wwwroot/
│ ├── Helper/
│ ├── Layout/
│ ├── Pages/
│ ├── State/
│ ├── App.razor
│ ├── Program.cs
│ └── libman.json
│
├── ClientLibrary/ # Client-Side Logic (Reusable Classes)
│ ├── Authentication/
│ ├── Constants/
│ ├── Helper/
│ └── Services/
│
├── Server/ # ASP.NET Core Web API
│ ├── Controllers/
│ ├── Authentication/
│ ├── appsettings.json
│ ├── Program.cs
│ └── Server.http
│
└── ServerLibrary/ # Backend Data & Repository Layer
├── Authentication/
├── Data/
├── Helpers/
├── Repositories/
├── Service/
└── Class1.cs

yaml
Copy code

This layered structure follows **Clean Architecture** principles for reusability and maintainability.

---

## ⚙️ Tech Stack

**Frontend**
- Blazor WebAssembly (.NET 8)
- Bootstrap 5 / CSS
- Razor Components & State Management

**Backend**
- ASP.NET Core 8 Web API
- Entity Framework Core 8
- SQL Server Database
- Repository & Service Layer Pattern

**Authentication**
- JWT Authentication & Refresh Token
- Role-Based Authorization

**Other Tools**
- PDF & Print Integration
- Generic Repository + Generic Controller for CRUD

---

## 🏗️ Architecture Diagram

```text
┌────────────────────────────┐
│        Blazor WASM         │
│  (UI / Pages / Services)   │
└──────────┬─────────────────┘
           │ HTTP / JSON
┌──────────▼─────────────────┐
│       Web API Server       │
│ (Controllers / Auth / CRUD)│
└──────────┬─────────────────┘
           │ EF Core ORM
┌──────────▼─────────────────┐
│   ServerLibrary / DB       │
│ (Repositories / Context)   │
└──────────┬─────────────────┘
           │ Shared Models
┌──────────▼─────────────────┐
│       BaseLibrary          │
│ (DTOs / Entities / Helper) │
└────────────────────────────┘
✨ Features
✅ Authentication & Authorization

Register, Login, Logout

Role-based view rendering

JWT & Refresh Token Support

✅ CRUD Operations

Employees

Departments

Sanctions

Vacations

✅ Advanced Functions

Generic Repository Pattern

Generic Controller

Export / Print to PDF

✅ UI

Responsive Bootstrap Layout

Dynamic Navigation Menu

Authentication State Provider

✅ Security

Token-based Secure API Access

CORS Configuration between Client & Server
