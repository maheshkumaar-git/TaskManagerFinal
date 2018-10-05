Create Database FSD_CAPSULE;
USE FSD_CAPSULE;

Create Table Task
(
Task_ID int PRIMARY KEY identity(1,1) ,
Parent_ID int,
Task varchar(50),
StartDate datetime,
EndDate datetime,
[Priority] int,
IsTaskEnded int
);


Create Table ParentTask
(
Id  int primary key identity(1,1),
Parent_ID int, 
Parent_Task varchar(50)
)

