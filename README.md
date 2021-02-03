# Customer.Api

Go-Logs customer management.

## Setting up

### Cloning

1. Clone this repository:  
   `git clone <url>`.
2. Update submodules:    
    1. `cd <repo_dir>`.
    2. `git submodule update --init --recursive`.
3. Install latest dotnet-format:  
   `dotnet tool install --global dotnet-format --version 5.0.0-alpha.335`
4. Restore packages. **Must** be executed manually **inside directory containing project that uses GoLogs.Framework.Core**:  
    `dotnet restore`.

### Database

1. Create an instance of PostgreSQL locally.
2. Execute scripts in `Customer.Api/database` directory in ascending order.
