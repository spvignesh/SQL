USE [Customer]

CREATE TABLE [dbo].[Customers](
	[Id] [bigint] NOT NULL,
	[First Name] [varchar](50) NULL,
	[Code] [int] NULL,
	[City] [varchar](50) NULL,
	[Last Name] [varchar](50) NULL,
	[Country] [varchar](50) NULL
) ON [PRIMARY]

Insert into Customers Values (1,'Peter',123,'Los Angeles','Hein','America')
Insert into Customers Values (2,'Francois',456,'Paris','Gilles','France')
Insert into Customers Values (3,'Kevin',789,'London','Peterson','United Kingdom')
Insert into Customers Values (4,'Mark',134,'Toronto','Henry','Canada')
Insert into Customers Values (5,'Sakurai',341,'Tokyo','Shoko','Japan')
Insert into Customers Values (6,'Jim',734,'Shanghai','Lee','China')
Insert into Customers Values (7,'Karl',946,'Queenstown','Max','New Zealand')
Insert into Customers Values (8,'James',627,'Manila','Max','Philippines')
Insert into Customers Values (9,'Mohammad',639,'Karachi','Khan','Pakistan')


ALTER DATABASE Customer SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE;


--Testing purpose
Insert into Customers Values (10,'Rashid',295,'Barisal','Akmal','Bangladesh')
DELETE FROM Customers where Id=10
Update Customers set [Last Name]='Wood' where Id=7
