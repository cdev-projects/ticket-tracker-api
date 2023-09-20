
<h2 align="center"><p>Ticket Tracker API</p></h2>

# Context

This is a sample mini-project built to demonstrate my understanding of different software development practices and object oriented principles.

For this project, I built an API that is designed to track the creation & status of service tickets along with the history of the interaction between the customer & IT staff. 

Customers can:
- Create a ticket - `POST /tickets`
- Get the status of a ticket - `GET /tickets/{ticketId}`
- Close a ticket - `PUT /tickets/{ticketId}/status`
- Interact with the IT staff - `POST /tickets/{ticketId}/interactions`

IT Staff can:
- Assign the ticket to other IT staff - `PUT /tickets/{ticketId}/assignment`
- Change the status of the ticket - `PUT /tickets/{ticketId}/status`
- Add technical notes to the ticket - `POST /tickets/{ticketId}/notes`
- Interact with the customer - `POST /tickets/{ticketId}/interactions`

# Technology Stack

- Language: C#
- Framework: .NET Core
- ORM: Entity Framework
- Database: SQLite

# Technical Concepts

- REST API Design
- Dependency Injection
- Repository Pattern
- ORM Integration
- Code First Approach in EF Core

# Folder Structure

```bash
    ├── TicketTracker.Api               # .NET Core Web API Project
    │   ├── Tickets Controller           
    │   ├── Users Controller             
    ├── TicketTracker.Application       # Service application layer, contains handlers used by API endpoints
    │   ├── Ticket Handlers      
    │   ├── User Handlers        
    ├── TicketTracker.Domain            # Domain layer containing business logic & all invariants
    │   ├── Tickets Domain Model        
    │       ├── Ticket   
    │       ├── Ticket Note   
    │       ├── Ticket Interaction   
    │   ├── Users Domain Model    
    │       ├── User   
    │       ├── Role   
    │       ├── Permission   
    ├── TicketTracker.Repositories      # Data layer containing the db context, repositories, and entities
    │   ├── Tickets Repository   
    │       ├── Ticket Entity   
    │       ├── Ticket Note Entity   
    │       ├── Ticket Interaction Entity 
    │   ├── Users Repository     
    │       ├── User Entity   
    │       ├── Role Entity  
    │       ├── Permission Entity   
    │   ├── Ticket Tracker DB Context   # DB Context used by Entity Framework - currently mapped using Code First approach
    └── ...
```

# Getting Started

## Running Locally

First, download this repository to your local machine and open the solution within your Visual Studios IDE.

Next, make sure that the `TicketTracker.Api` project is set as your startup project and then select `IIS Express` as your runtime server host.

![](public/images/startup.jpg)

From here, open your package manager console within Visual Studios under `View > Other Windows > Package Manager Console`.

Within the package manager console, set the default project to `TicketTracker.Repositories` since this is where the DB Context resides and run the command `Update-Database`.

![](public/images/package-manager.jpg)

This command will run the migrations & data seeding required to construct the database tables and populate seed data for the first time.

Once the command completes, the database should be configured and the API should now be ready to start. Proceed by clicking on the `IIS Express` button at the top to run the API.

Clicking `IIS Express` should open a screen that displays a `Swagger` API with all of the endpoints shown similar to the image below.

![](public/images/main.jpg)

# Endpoints

Within this API, there are endpoints that support the functionality outlined in the `Context` section at the top.

Feel free to interact with these APIs and observe the responses shown in Swagger. 

`GET /tickets` - Return all tickets created within the system

```bash
Sample Request URL

https://localhost:44369/tickets
```

`GET /tickets/{ticketId}` - Find a specific ticket by its corresponding Id

```bash
Sample Request URL

https://localhost:44369/tickets/-1
```

`POST /tickets` - Create a new ticket / returns newly created ticket id
```bash
Sample Request Body

{
  "ticket": {
    "title": "New enhancement required to support customer onboarding",
    "description": "Build a web app to allow sales to import customers into the system",
    "createdByUserId": 2
  }
}
```

`PUT /tickets/{ticketId}/status` - Change the status on a specific ticket
```bash
Sample Request Body

{
  "status": 2,
  "changedByUserId": 3
}
```

`PUT /tickets/{ticketId}/assignment` - Change the IT staff assigned to a specific ticket
```bash
Sample Request Body

{
  "assignedByUserId": 3,
  "assignedToUserId": 4
}
```

`POST /tickets/{ticketId}/notes` - Create a new ticket note / returns newly created ticket note id
```bash
Sample Request Body

{
  "ticketNote": {
    "message": "Finished building web app to support customer onboarding",
    "createdByUserId": 3
  }
}
```

`POST /tickets/{ticketId}/interactions` - Create a new ticket interaction between a Customer and a IT Staff / returns newly created ticket interaction id
```bash
Sample Request Body

{
  "ticketInteraction": {
    "message": "Hey Tom, the web app looks great! This should really help with the customer onboarding!",
    "sentByUserId": 1,
    "receivedByUserId": 3
  }
}
```

`GET /users` - Return all users created within the system

```bash
Sample Request URL

https://localhost:44369/users
```

`GET /users/{userId}` - Find a specific user by its corresponding Id

```bash
Sample Request URL

https://localhost:44369/users/4
```

Note: there are data validations within the API that may display in the response whenever any of the domain invariants are violated. See the `Domain Restrictions` section for more information on what type of API actions are permitted.

In addition, make sure to read the `Data Context` section below to find more information about how the seed data was configured and what the possible input options are for each API endpoint. 

# Domain Restrictions

This sections outlines the rules or invariants that are protected by the domain. When any of these rules are violated, an error message will be communicated in the API response.

1. Users (Customers & IT Staff) are only permitted to perform an action if their role permits it. See `Seed Data` section below to see what permissions are applied to each role.

2. Only Customers are allowed to create a new ticket.

3. Only IT Staff are allowed to assign a ticket, and the ticket may only be assigned to other IT Staff.

4. Both Customers & IT Staff are allowed to change the status of a ticket but only Customers are allowed to close a ticket.

5. Only IT Staff are allowed to add a note to a ticket.

6. Both Customers & IT Staff are allowed to add interactions to a ticket.

7. All ticket interactions must always be between a Customer and an IT Staff (no Customer to Customer, or IT Staff to IT Staff interactions).

# Data Context

This section outlines all of the important data setup details required to interact with the API.

## Enumerations

```bash
enum TicketStatus
{
    New = 0,
    Assigned = 1,
    InProgress = 2,
    Completed = 3,
    Closed = 4
}
```

```bash
enum PermissionType
{
    CreateTicket = 1,
    AssignTicket = 2,
    ChangeTicketStatus = 3,
    CloseTicket = 4,
    AddTicketNote = 5,
    AddTicketInteraction = 6
}
```

## Seed Data 

```bash
Ticket

{
    Id = -1,
    Title = "Application requires update to support new customers",
    Description = "Customer onboarding screen needs to be developed so that customers can be dynamically added into the system.",
    Status = "Assigned",
    CreatedTimestamp = DateTime.UtcNow,
    CreatedBy = "Bob",
    LastModifiedTimestamp = DateTime.UtcNow,
    LastModifiedBy = "Bob",
}
```

```bash
Users

{ Id = 1, Name = "Bob",      Role = "Customer" },
{ Id = 2, Name = "Jane",     Role = "Customer" },
{ Id = 3, Name = "Tom",      Role = "IT Staff" },
{ Id = 4, Name = "Samantha", Role = "IT Staff" }
```

```bash
Roles

{ Id = 1, Name = "Customer" }
{ Id = 2, Name = "IT Staff" }
```

```bash
Permissions

{ Id = 1, Name = "Create Ticket" }
{ Id = 2, Name = "Assign Ticket" }
{ Id = 3, Name = "Change Ticket Status" }
{ Id = 4, Name = "Close Ticket" }
{ Id = 5, Name = "Add Ticket Note" }
{ Id = 6, Name = "Add Ticket Interaction" }
```

```bash
Role Permissions

{ Role = "Customer", Permissions = ["Create Ticket", "Change Ticket Status", "Close Ticket", "Add Ticket Interaction"] }
{ Role = "IT Staff", Permissions = ["Assign Ticket", "Change Ticket Status", "Add Ticket Note", "Add Ticket Interaction"] }
```

# Expected Result

Below is a sample output of what the data could look like after interacting with all of the endpoints.

```bash
{
  "id": 1,
  "title": "New enhancement required to support customer onboarding",
  "description": "Build a web app to allow sales to import customers into the system",
  "status": 4,
  "assignedTo": {
    "id": 3,
    "name": "Tom",
    "role": null
  },
  "createdTimestamp": "2023-09-19T23:04:42.146008",
  "createdBy": {
    "id": 2,
    "name": "Jane",
    "role": null
  },
  "lastModifiedTimestamp": "2023-09-19T23:08:29.7746393",
  "lastModifiedBy": {
    "id": 4,
    "name": "Samantha",
    "role": null
  },
  "closedTimestamp": "2023-09-19T23:07:30.2497389",
  "closedBy": {
    "id": 1,
    "name": "Bob",
    "role": null
  },
  "notes": [
    {
      "id": 1,
      "message": "Assigned ticket to Tom",
      "createdTimestamp": "2023-09-19T23:09:13.429763",
      "createdBy": {
        "id": 4,
        "name": "Samantha",
        "role": null
      }
    },
    {
      "id": 2,
      "message": "Finished building web app to support customer onboarding",
      "createdTimestamp": "2023-09-19T23:10:10.7732853",
      "createdBy": {
        "id": 3,
        "name": "Tom",
        "role": null
      }
    }
  ],
  "interactions": [
    {
      "id": 1,
      "message": "Bob, the web app has been completed. Please verify.",
      "sentTimestamp": "2023-09-19T23:10:38.6758636",
      "sentBy": {
        "id": 3,
        "name": "Tom",
        "role": null
      },
      "receivedBy": {
        "id": 1,
        "name": "Bob",
        "role": null
      }
    },
    {
      "id": 2,
      "message": "Hey Tom, the web app looks great! This should really help with the customer onboarding!",
      "sentTimestamp": "2023-09-19T23:11:04.198499",
      "sentBy": {
        "id": 1,
        "name": "Bob",
        "role": null
      },
      "receivedBy": {
        "id": 3,
        "name": "Tom",
        "role": null
      }
    }
  ]
}
```