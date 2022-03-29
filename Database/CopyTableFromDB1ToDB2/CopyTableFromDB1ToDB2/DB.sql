  --Database 1

  CREATE TABLE Persons (
    ID int IDENTITY(1,1),
    FirstName varchar(255),
    LastName varchar(255),
    Age Int
);
--Database 2
  CREATE TABLE Persons (
    ID int,
    FirstName varchar(255),
    LastName varchar(255),
    Age Int
);
