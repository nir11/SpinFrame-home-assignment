# Car Management System
![car-management-system](https://github.com/nir11/car-management-system/assets/23150023/35268826-f351-4323-a2fb-dc91968a9114)

This readme provides instructions on how to run the server and client projects of the **car-management-system**
project.

Project structure:

- `server` directory (.NET Core 6.0 Web API)

- `client` directory (Reac app)

- `CarsManagement.bak` (SQL Server backup file)

## Server

The server is built using .NET Core 6.0 and runs on port 7176.

### Requirements

- .NET Core 6.0 SDK must be installed on your machine.
- SQL Server with the **CarsManagement** database (Use the provided **CarsManagement.bak** file to restore the database).

### Instructions

1. Restore the SQL Server database `CarsManagement.bak`.

2. Navigate to the `server` directory.

3. Open the file named `appsettings.json` and change the `ConnectionStrings.DefaultConnection` accordingly to your machine connection string;

4. Open a terminal and run the following command to build and run the server:

   ```
   dotnet run
   ```

5. The server will start running on port 7176.

## Client

The client directory of the project is a React application that runs on port 3000.

### Requirements

- Node.js v18 or later must be installed on your machine.

### Instructions

1. Open a new terminal and navigate to the `client` directory.

2. Install the required dependencies by running the following command:
   ### `npm install`
3. Create a `.env` file at the root of the client directory and add the following line:
   ```
   REACT_APP_BASE_URL=https://localhost:7176/api
   ```
   If the port of the server is different from 7176 on your machine, update the URL accordingly.
4. Run the following command to start the client:

   ### `npm start`

5. Open [http://localhost:3000](http://localhost:3000) to view it in the browser.
