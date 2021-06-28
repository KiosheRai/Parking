CREATE DATABASE Parking

USE Parking
go

DROP TABLE Operator;
DROP TABLE Payment;
DROP TABLE Report;
DROP TABLE Place;

CREATE TABLE Operator(
	id int identity(1,1),
	name nchar(15) not null,
	surname nchar(15) not null,
	pass nchar(18) not null,
	CONSTRAINT Prim_ID_Admin PRIMARY KEY (id),
)  
GO

CREATE TABLE Place(
	id int identity(1,1),
	status nchar(9) default('Свободно') not null,
	CONSTRAINT Prim_ID_Place PRIMARY KEY (id),
)
GO

CREATE TABLE Report(
	id int identity(1,1),
	car nchar(10) not null,
	place int not null,
	arrival smalldatetime not null,
	CONSTRAINT Prim_ID_Report PRIMARY KEY (id),
	CONSTRAINT Fore_Place_Report FOREIGN KEY (place) REFERENCES Place([id])
)
GO

CREATE TABLE Payment(
	id int not null,
	departure smalldatetime not null,
	pay smallmoney not null,
	CONSTRAINT Fore_ID_Report FOREIGN KEY (id) REFERENCES Report([id])
)
GO

INSERT INTO Place(status) values('Свободно');
GO

SELECT * FROM Place