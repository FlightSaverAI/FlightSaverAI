# Flight Saver API

### Prerequisites

To run this API, ensure the following are installed:

- [.NET SDK (8.0 or later)](https://dotnet.microsoft.com/download)

---

### Setup Instructions

1. **Clone the Repository**

   ```bash
   git clone <repository-url>
   cd FlightSaverApi/backend/FlightSaverApi
   ```

2. **Install Dependencies**

   The dependencies are specified in the `.csproj` file and will automatically be restored on build, but to be sure, run:

   ```bash
   dotnet restore
   ```

### Running the API

To start the API locally:

```bash
dotnet run
```

Once the API is running, it should be available at `https://localhost:<PORT>/`.

> **Note**: Replace `<PORT>` with the port number shown in the console output (typically 5001 for HTTPS, or 5000 for HTTP).

### Testing the API

#### Swagger UI

Open your browser and go to `https://localhost:<PORT>/swagger` to view the Swagger UI, which documents all API endpoints.

#### Postman or any HTTP Client

If you prefer Postman, import the following endpoints to test API requests.

### API Endpoints

| Endpoint           | Method | Description                         | Authorization |
|--------------------|--------|-------------------------------------|---------------|
| `/Aircrafts`       | GET    | Retrieves all aircraft              | User          |
| `/Aircrafts/{id}`  | GET    | Retrieves a single aircraft by ID   | User          |
| `/Aircrafts`       | POST   | Adds a new aircraft                 | Admin         |
| `/Aircrafts/{id}`  | PUT    | Updates an aircraft by ID           | Admin         |
| `/Aircrafts/{id}`  | DELETE | Deletes an aircraft by ID           | Admin         |
| `/Auth/Register`   | POST   | Registers a new user                | Open          |
| `/Auth/Login`      | POST   | Logs in a user and returns a token  | Open          |

### User Roles

The API supports role-based access for **Users** and **Admins**.

- **User**: Can view aircraft details.
- **Admin**: Can manage aircraft data.

### Troubleshooting

- **Port Conflicts**: If `https://localhost:5001` is in use, try specifying a different port by adding `--urls`:

  ```bash
  dotnet run --urls https://localhost:5002
  ```

  ```bash
  dotnet tool update --global dotnet-ef
  ```

- **JWT Token Issues**: Ensure your `appsettings.json` includes a valid JWT Secret for token generation. Replace it with your desired secret, and ensure it's a secure base64-encoded string.
