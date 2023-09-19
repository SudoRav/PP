CREATE DATABASE pp
GO

USE pp
GO

CREATE TABLE files  
(  
    problem_ID int NOT NULL ,
	ID int NOT NULL IDENTITY,
    Data varbinary(MAX) NOT NULL,
    Extension char(100) NOT NULL,
    FileName varchar(MAX) NOT NULL,  
);  

CREATE TABLE problems  
(  
	ID int NOT NULL IDENTITY,
    status bit NOT NULL,
    name varchar(30) NOT NULL,
    description text NOT NULL,  
	date_create varchar(10) NOT NULL,  
	date_ending varchar(10) NULL,  
);  

CREATE TABLE staff  
(  
    problem_ID int NOT NULL,
    user_ID int NOT NULL,  
);  

CREATE TABLE subproblems  
(  
    problem_ID int NOT NULL,
    ID int NOT NULL IDENTITY,
	status bit NOT NULL,
    name varchar(30) NOT NULL,
	date_ending varchar(10) NULL, 
);  

CREATE TABLE tab  
(  
    user_ID int NOT NULL, 
    total varchar(50) NULL,
	day1 varchar(50) NULL, 
	day2 varchar(50) NULL, 
	day3 varchar(50) NULL, 
	day4 varchar(50) NULL, 
	day5 varchar(50) NULL, 
	day6 varchar(50) NULL, 
	day7 varchar(50) NULL,
	day8 varchar(50) NULL, 
	day9 varchar(50) NULL, 
	day10 varchar(50) NULL, 
	day11 varchar(50) NULL, 
	day12 varchar(50) NULL,
	day13 varchar(50) NULL, 
	day14 varchar(50) NULL, 
	day15 varchar(50) NULL, 
	day16 varchar(50) NULL, 
	day17 varchar(50) NULL,
	day18 varchar(50) NULL, 
	day19 varchar(50) NULL, 
	day20 varchar(50) NULL, 
	day21 varchar(50) NULL, 
	day22 varchar(50) NULL,
	day23 varchar(50) NULL, 
	day24 varchar(50) NULL, 
	day25 varchar(50) NULL, 
	day26 varchar(50) NULL, 
	day27 varchar(50) NULL,
	day28 varchar(50) NULL, 
	day29 varchar(50) NULL, 
	day30 varchar(50) NULL, 
);  

INSERT INTO tab (user_ID) VALUES (0);

CREATE TABLE users  
(  
    ID int NOT NULL IDENTITY,
	login varchar(50) NOT NULL,
	password varchar(50) NOT NULL,
	F varchar(50) NOT NULL,
	I varchar(50) NOT NULL,
	O varchar(50) NULL,
	mail varchar(50) NOT NULL,
	phone varchar(50) NOT NULL,
	ier varchar(15) NOT NULL,
	toreg bit NOT NULL,
);  

INSERT INTO tab (user_ID) VALUES (1);
INSERT INTO users (login, password, F, I, O, mail, phone, ier, toreg) VALUES (1, 'C4CA4238A0B923820DCC509A6F75849B', 1, 1, 1, 1, 1, 'Администратор', 1); 
GO