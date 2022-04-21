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
### Entity relations
![alt text](https://github.com/BiserB/HRFlow/blob/main/HRFlow.Data/Diagrams/HRFlow.png)

### Functionality

The home screen shows all employees stored in the system with the corresponding line manager. You can search for an employee by name.\
There is a start date and a comparison bar for long-term employment.\
When you click on an employee, you are redirected to the employee details page.\
On Employee Details page you can:
- review full employee details: names, IBAN, current salary, line manager and hire date
- update employee details: names, IBAN, salary
- review and add comments, update comments
- the "Job" section contains the history of all positions held by the employee. There is a start and end date for the job and the corresponding salary.
- When the employee's salary is renewed, a new record is added to the work history.
- Similarly, the "departments" section contains a history of the departments in which the employee has worked.

New employees are added in the "Add Employee" page
