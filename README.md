# HRFlow
### Simple employee records management system .NET Core MVC


**Environment**:
- **Operating System**  Windows-10-10.0.19041-SP0
- **Target framework** .NET 6.0
- **IDE** Microsoft Visual Studio 2022 Version 17.0.5
- **DB Server** PostgreSQL 14
- **DB tool** pgAdmin 4 Version 5.7
- **Optional db** (Microsoft SQL Server/ SQL Server management studio)

The database can be changed to **SQL Server** simply with configuration update in AddDbContext.\
Date format in PostgreSQL is 'timestamp without timezone', in SQL Server it is datetime2(7).\

### App overview
- The first time you start the application, it seeds in demo data.
- All employees are displayed on the home page.
- The search can be performed by employee name.
- It is possible to add and edit comments to an employee record.
- The salary can be changed, the respective salary history is maintained.
- The department can be changed, the respective department history is maintained.

***
### Entity realtions
![alt text](https://github.com/BiserB/HRFlow/blob/main/HRFlow.Data/Diagrams/HRFlow.png)
