# TodoList - Task Management System

A full-stack .NET solution demonstrating clean architecture and professional software engineering practices through three distinct applications: Console UI, RESTful Web API, and Razor Pages web client.

[![.NET](https://img.shields.io/badge/-.NET%208.0-blueviolet?logo=dotnet)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-blue.svg)](LICENSE)

---

## 🚀 Quick Start

### Prerequisites
- .NET 8.0 SDK or higher
- Visual Studio 2022 or VS Code (optional)
- Modern web browser (for Web Client)

### Running the Applications

**Console Application:**
```bash
cd TodoList
dotnet run
```

**Web API:**
```bash
cd ToDoList.WebApi
dotnet run
# API available at: https://localhost:7126
# Swagger UI: https://localhost:7126/swagger
```

**Web Client:**
```bash
# Ensure Web API is running first
cd ToDoList.Client
dotnet run
# Web app available at: https://localhost:7266
```

---

## 📚 Documentation

### User Guides
- **[Console Application User Guide](docs/CONSOLE_APP_USER_GUIDE.md)** - Complete guide for the CLI interface
- **[Web Application User Guide](docs/WEB_APP_USER_GUIDE.md)** - End-user documentation for the web client
- **[Web API Documentation](docs/WEB_API_DOCUMENTATION.md)** - RESTful API reference with Swagger integration

### Quick Links
- [Architecture Overview](#architecture)
- [Features](#features)
- [Project Structure](#project-structure)
- [Testing](#testing)
- [Contributing](#contributing)

---

## ✨ Features

### Core Functionality
- ✅ **CRUD Operations** - Create, Read, Update, Delete tasks
- ✅ **Progress Tracking** - Cumulative progress with visual indicators
- ✅ **Category Management** - Organize tasks by categories
- ✅ **Task Protection** - 50% rule prevents modification of advanced tasks
- ✅ **Auto-Completion** - Tasks marked complete at 100% progress

### Technical Features
- 🏗️ **Clean Architecture** - 4-tier layered design
- 🔌 **RESTful API** - OpenAPI/Swagger documentation
- 🌐 **Web Client** - Responsive Razor Pages with AJAX
- 💻 **Console UI** - Interactive CLI with logging
- 🧪 **Unit Tests** - Comprehensive test coverage
- 📝 **Structured Logging** - Serilog integration
- 🔄 **CORS Support** - Cross-origin resource sharing

---

## 🏗️ Architecture

### Layered Design

```
┌─────────────────────────────────────────────────────────┐
│         1. Distributed Services Layer                   │
│  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐   │
│  │   Console    │  │   Web API    │  │  Web Client  │   │
│  │     App      │  │  (REST API)  │  │ (Razor Pages)│   │
│  └──────────────┘  └──────────────┘  └──────────────┘   │
└─────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────┐
│              2. Application Layer                       │
│  ┌──────────────────────────────────────────────────┐   │
│  │  Application Services & DTOs                     │   │
│  │  (TodoListApplicationService, DTOs, Mappers)     │   │
│  └──────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────┘
                          ↓
┌─────────────────────────────────────────────────────────┐
│               3. Domain Layer                           │
│  ┌──────────────────────────────────────────────────┐   │
│  │  Aggregates, Entities, Value Objects, Events     │   │
│  │  (TodoList, TodoItem, Category, Repository Iface)│   │
│  └──────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────┘
                          ↑ (Dependency Inversion)
┌─────────────────────────────────────────────────────────┐
│                4. Infrastructure Layer                  │
│  ┌──────────────────────────────────────────────────┐   │
│  │  Repository Implementations                      │   │
│  │  (InMemoryTodoListRepository)                    │   │
│  └──────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────┘
```

### Design Principles
- **SOLID Principles** - Interface-based design, dependency inversion
- **Domain-Driven Design** - Separate domain models from DTOs
- **Repository Pattern** - Data access abstraction
- **Dependency Injection** - Loose coupling throughout
- **Separation of Concerns** - Clear layer boundaries

---

## 📁 Project Structure

```
TodoList/
├── docs/                              # 📚 Documentation
│   ├── CONSOLE_APP_USER_GUIDE.md
│   ├── WEB_API_DOCUMENTATION.md
│   └── WEB_APP_USER_GUIDE.md
│
├── TodoList/                          # 💻 Console Application
│   ├── Program.cs
│   └── Setup/
│
├── ToDoList.WebApi/                   # 🌐 REST API
│   ├── Controllers/
│   │   └── ToDoListController.cs
│   ├── Program.cs
│   └── Registrations/
│
├── ToDoList.Client/                   # 🖥️ Web Client (Razor Pages)
│   ├── Pages/
│   │   └── Index.cshtml
│   ├── wwwroot/
│   │   └── js/site.js
│   └── Program.cs
│
├── ToDoList.Application/              # 📦 Application Layer
│   ├── Services/
│   │   └── TodoListApplicationService.cs
│   ├── DTOs/
│   │   ├── TodoItemDto.cs
│   │   └── ProgressionDto.cs
│   ├── Mappers/
│   │   └── TodoItemMapper.cs
│   └── Models/
│       ├── CreateRequest.cs
│       └── ...
│
├── ToDoList.Domain/                   # 🔷 Domain Layer (Core)
│   ├── Aggregates/
│   │   └── TodoListAggregate/
│   │       ├── TodoList.cs
│   │       └── TodoItem.cs
│   ├── ValueObjects/
│   │   ├── TodoItemId.cs
│   │   ├── Category.cs
│   │   ├── Progression.cs
│   │   └── ProgressionHistory.cs
│   ├── Events/
│   │   └── ...
│   ├── Exceptions/
│   │   └── ...
│   └── Repositories/
│       └── ITodoListRepository.cs
│
├── ToDoList.Infrastructure/           # 🏗️ Infrastructure Layer
│   └── Persistence/
│       └── InMemory/
│           └── InMemoryTodoListRepository.cs
│
├── ToDoList.Tests/                    # 🧪 Unit Tests
│   ├── Domain/
│   │   ├── TodoListTests.cs
│   │   └── CategoryTests.cs
│   └── Application/
│       └── TodoListApplicationServiceTests.cs
│
└── README.md                          # 📖 This file
```

---

## 🎯 Key Business Rules

### 50% Progress Protection Rule
Tasks with more than 50% accumulated progress are **protected** from modification:
- ❌ **Cannot Update** - Description changes are blocked
- ❌ **Cannot Delete** - Task removal is prevented

**Rationale:** Prevents accidental modification of substantially complete work.

### Cumulative Progress Tracking
Progress entries are **additive**:
```
Initial: 0%
Register 25% → Total: 25%
Register 30% → Total: 55%
Register 45% → Total: 100% ✅ (Auto-marked as Completed)
```

### Category Validation
- Categories must exist in the system
- Categories are case-sensitive
- Default categories: `Work`, `Personal`, `Shopping`, `Health`

---

## 🧪 Testing

### Running Tests
```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test ToDoList.Domain.Test

# Run with coverage
dotnet test /p:CollectCoverage=true
```

### Test Projects
- **ToDoList.Domain.Test** - Domain layer unit tests

---

## 🛠️ Technology Stack

### Backend
- **Framework**: .NET 8.0
- **API**: ASP.NET Core Web API
- **Logging**: Serilog
- **Documentation**: Swagger/OpenAPI
- **DI Container**: Microsoft.Extensions.DependencyInjection

### Frontend
- **Framework**: ASP.NET Core Razor Pages
- **JavaScript**: Vanilla JS with Fetch API
- **Styling**: Bootstrap 5
- **Communication**: RESTful API calls

### Architecture
- **Pattern**: Clean Architecture / Onion Architecture
- **Data Access**: Repository Pattern
- **Storage**: In-Memory (development)

---

## 🌟 Highlights

### Architectural Excellence
✅ **Clean Architecture** - Proper separation of concerns across 4 layers  
✅ **SOLID Principles** - Interface-driven design with dependency inversion  
✅ **Domain-Driven Design** - Business logic encapsulated in domain services  
✅ **Testability** - Comprehensive unit test coverage  

### Professional Practices
✅ **RESTful API Design** - Proper HTTP verbs and status codes  
✅ **API Documentation** - Swagger/OpenAPI integration  
✅ **Structured Logging** - Serilog with file rotation  
✅ **Error Handling** - Graceful error management throughout  

### Full-Stack Capabilities
✅ **Multiple UIs** - Console, Web API, Web Client  
✅ **Responsive Design** - Modern web interface  
✅ **Real-time Updates** - AJAX-driven SPA-like experience  
✅ **Cross-Platform** - Runs on Windows, Linux, macOS  

### Code Quality
✅ **Consistent Naming** - Clear, descriptive identifiers  
✅ **Documentation** - Comprehensive user guides  
✅ **Modularity** - Reusable components and services  
✅ **Scalability** - Ready for microservices architecture  

---

## 📖 Usage Examples

### Console Application
```bash
Todo List Application
1. Add Item
2. Update Item
3. Remove Item
4. Register Progression
5. Print Items
6. Exit
Select an option: 1

Enter title: Implement authentication
Enter description: Add JWT-based authentication
Available categories: Work, Personal, Shopping, Health
Enter category: Work
✅ Item added successfully!
```

### Web API (cURL)
```bash
# Get all tasks
curl -X GET "https://localhost:7126/api/ToDoList"

# Create a task
curl -X POST "https://localhost:7126/api/ToDoList" \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "title": "Complete documentation",
    "description": "Write user guides",
    "category": "Work"
  }'

# Register progress
curl -X POST "https://localhost:7126/api/ToDoList/1/progression" \
  -H "Content-Type: application/json" \
  -d '{
    "date": "2025-11-25T14:30:00Z",
    "percentage": 25
  }'
```

### Web Client
1. Navigate to `https://localhost:7266`
2. Fill in the task form (Title, Description, Category)
3. Click "Save" to create the task
4. Use "Update" or "Delete" buttons on task cards
5. Register progress using the progression form

---

## 🔧 Configuration

### API Endpoints
- **Web API**: `https://localhost:7126`
- **Swagger UI**: `https://localhost:7126/swagger`
- **Web Client**: `https://localhost:7266`

### CORS Settings
The API accepts requests from:
- `https://localhost:7266` (Web Client)

To modify CORS settings, edit `ToDoList.WebApi/Program.cs`:
```csharp
policy.WithOrigins("https://your-domain.com")
```

### Logging Configuration
Logs are written to:
- **Console**: Real-time output
- **File**: `logs/todolist.log` (daily rotation)

To modify logging, edit `TodoList/Program.cs`:
```csharp
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/todolist.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();
```

---

## 🤝 Contributing

### Development Setup
1. Clone the repository
   ```bash
   git clone https://github.com/yourusername/TodoList.git
   cd TodoList
   ```

2. Restore dependencies
   ```bash
   dotnet restore
   ```

3. Build the solution
   ```bash
   dotnet build
   ```

4. Run tests
   ```bash
   dotnet test
   ```

### Code Style
- Follow C# naming conventions
- Use meaningful variable and method names
- Add XML documentation comments for public APIs
- Write unit tests for new features

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👤 Author

**Rosan Ivanov**

---

## 🙏 Acknowledgments

- Built with ASP.NET Core and .NET8
- Logging powered by Serilog
- API documentation with Swagger/OpenAPI
- UI styling with Bootstrap

---

## 📞 Support

For questions or issues:
1. Check the [documentation](docs/)
2. Review the [API documentation](docs/WEB_API_DOCUMENTATION.md)
3. Open an issue on GitHub

---

**⭐ If you find this project helpful, please consider giving it a star!**
