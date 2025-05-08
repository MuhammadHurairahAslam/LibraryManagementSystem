1- After Cloning Project you click on (LibraryManagementSystem.sln) to run project.
2- After that set as startup project this (LibraryManagementSystem.Api)
3- Open file on path (LibraryManagementSystem/LibraryManagementSystem.Api/appsettings.json) change Server, Id and password in ConnectionStrings to connect SQL Server.Also change CorsUrl in same file with their MVC application running Url.
4- You Don't need to create DB or running SP's and seedings it run automatically.
5- If you see SQL Scripts go to this path in project (LibraryManagementSystem/LibraryManagementSystem.Infrastructure/Scripts).All SP's, Creation Table and Seeding scripts are their. 
