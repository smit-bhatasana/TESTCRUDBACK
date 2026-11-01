using APIProject.Models;

//Microsoft.Data.SqlClient(Latest)




//ng new employee- management - ui
//ng generate service services/api 
//using APIProject.Models;

//CREATE DATABASE EmployeeManagementDB;
//GO

//USE EmployeeManagementDB;
//GO

//-- TABLE 1: Departments
//CREATE TABLE Departments
//(
//    DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
//    DepartmentName VARCHAR(100) NOT NULL,
//    Description VARCHAR(250) NULL
//);
//GO

//-- TABLE 2: Employees
//CREATE TABLE Employees
//(
//    EmployeeId INT IDENTITY(1,1) PRIMARY KEY,
//    EmployeeName VARCHAR(100) NOT NULL,
//    Email VARCHAR(150) NOT NULL UNIQUE,
//    Salary DECIMAL(10,2) NOT NULL,
//    DepartmentId INT NOT NULL,

//    CONSTRAINT FK_Employees_Departments
//        FOREIGN KEY (DepartmentId)
//        REFERENCES Departments(DepartmentId)
//);
//GO

//-- TABLE 3: EmployeeDetails
//CREATE TABLE EmployeeDetails
//(
//    EmployeeDetailId INT IDENTITY(1,1) PRIMARY KEY,
//    EmployeeId INT NOT NULL,
//    Phone VARCHAR(15) NULL,
//    Address VARCHAR(250) NULL,
//    JoiningDate DATE NOT NULL,

//    CONSTRAINT FK_EmployeeDetails_Employees
//        FOREIGN KEY (EmployeeId)
//        REFERENCES Employees(EmployeeId)
//);
//GO