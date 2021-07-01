CREATE DATABASE Parking

USE Parking
go

DROP TABLE Operator;
DROP TABLE Payment;
DROP TABLE Report;
DROP TABLE Place;
DROP TABLE Rate;

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
	status nchar(9) default('Ñâîáîäíî') not null,
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
	id int identity(1,1) not null,
	departure smalldatetime not null,
	pay smallmoney not null,
	car nchar(10) not null,
	CONSTRAINT Prim_ID_Payment PRIMARY KEY (id),
)
GO

CREATE TABLE Rate(
	id int identity(1,1) not null,
	ddate date not null,
	pay smallmoney not null,
	CONSTRAINT Prim_ID_Rate PRIMARY KEY (id),
)
GO

--INSERT INTO Report(car, place, arrival) values('1234 as-2', 1, ());
--Go

INSERT INTO Place(status) values('Ñâîáîäíî');
INSERT INTO Place(status) values('Ñâîáîäíî');
INSERT INTO Place(status) values('Ñâîáîäíî');
INSERT INTO Place(status) values('Ñâîáîäíî');
INSERT INTO Place(status) values('Ñâîáîäíî');
INSERT INTO Place(status) values('Ñâîáîäíî');
INSERT INTO Place(status) values('Ñâîáîäíî');
INSERT INTO Place(status) values('Ñâîáîäíî');
INSERT INTO Place(status) values('Ñâîáîäíî');
GO

INSERT INTO Report(car, place, arrival) values ('9876 as-4', 8, '20-06-2021')

INSERT INTO Rate(ddate, pay) values('12-05-2021', 20);
GO

INSERT INTO Payment(departure, pay, car) values('30-06-2021', 123, '8564 sa-7');
INSERT INTO Payment(departure, pay, car) values('30-06-2021', 231, '2564 sa-7');
GO

INSERT INTO Payment(departure, pay, car) values('01-07-2021', 123, '8564 se-7');
INSERT INTO Payment(departure, pay, car) values('01-07-2021', 133, '8214 se-7');
GO

INSERT INTO Operator(name, surname, pass) values('Àäëåğ', 'Õâÿö', '1234');
GO

INSERT INTO Operator(name, surname, pass) values('f', 'f', 'f');
GO


SET DATEFORMAT DMY;
GO

UPDATE Place SET status = 'Çàíÿòî' where id = 2;

select * from Rate where id = max(id)

SELECT * FROM Operator
SELECT * FROM Place
SELECT * FROM Report
SELECT * FROM Payment
SELECT * FROM Rate

select sum(pay) from Payment where departure >= '01-07-2021' and departure <= '02-07-2021'
