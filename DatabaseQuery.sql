CREATE DATABASE EmployeeMaster

USE EmployeeMaster
CREATE TABLE Employee (
    EmpId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(250) NOT NULL,
    Email NVARCHAR(250),
    Designation NVARCHAR(250),
    Mobile NVARCHAR(20)
)

USE EmployeeMaster
CREATE TABLE Employee_Details (
    DocId INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
    EmpId int FOREIGN KEY REFERENCES Employee(EmpId),
    FileName NVARCHAR(250),
    FilePath NVARCHAR(250),
	FileDesc NVARCHAR(250),
    CreatedDate Datetime
)