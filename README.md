# ECommerce Project

Simple, clean-architecture-based E-Commerce backend API.

## 💻 About The Project

ECommerce is a modular monolith e-commerce backend built with Clean Architecture and modern .NET best practices.  
Features include: CQRS (MediatR), Soft Delete, Role-based Auth, Basket & Order Management, Discount Codes, AutoMapper, FluentValidation, and more.

## ⚙️ Getting Started

To get a local copy up and running, follow these simple steps.

### Prerequisites

- .NET 9 SDK

### Installation

1. **Clone the repo**
    ```bash
    git clone https://github.com/youruseremreinanname/E-Commerce.git
    ```

2. **Configure `appsettings.json`**
   - Open `src/WebAPI/appsettings.json` and set your database connection string.

3. **Apply database migrations**
   - Open **Package Manager Console** in WebAPI and run:
     ```
     Update-Database
     ```
## 🚀 Usage

1. **Run the WebAPI project**
    ```bash
    dotnet run --project src/ecommerce/WebAPI
    ```

## 🔍 Analysis

1. Restore dotnet tools if needed:
    ```bash
    dotnet tool restore
    ```
2. Run code analysis:
    ```bash
    dotnet roslynator analyze
    ```

## 🤝 Contributing

Contributions are welcome!

- Fork the repo and clone your local copy
- Create a feature branch (`git checkout -b feat/<FeatureName>`)
- Commit using [semantic commit messages](https://www.conventionalcommits.org/)
- Push and open a Pull Request

## ⚖️ License

Distributed under the MIT License.  
See `LICENSE` for more information.

## 📧 Contact

Developed by Emre İnan  
Contact: emreinannn@gmail.com

