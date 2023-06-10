# SpinFrame Home Assignment

This readme provides instructions on how to run the server and client projects of the **SpinFrame-home-assignment**
project.

Project structure:

- `server` directory

- `client` directory

- `SpinFrae.bak` (SQL Server backup file)

## Server

The server is built using .NET Core 6.0 and runs on port 7176.

### Requirements

- .NET Core 6.0 SDK must be installed on your machine.
- SQL Server with the **SpinFrame** database (Use the provided **SpinFrame.bak** file to restore the database).

### Instructions

1. Restore the SQL Server database `SpinFrame.bak`.

2. Open a terminal and navigate to the `server` directory.

3. Run the following command to build and run the server:

   ```
   dotnet run
   ```

4. The server will start running on port 7176.

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
