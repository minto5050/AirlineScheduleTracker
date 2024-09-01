## Setting-up Requirements

The RDBMS used in the project is SQL server 2022, if you don't have it installed on your local run the free container from MS

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<YourStrong@Passw0rd>" 
   -p 1433:1433 --name sql1 --hostname sql1 
   -d mcr.microsoft.com/mssql/server:2022-latest
```
Based on your local hostname , port, password etc. update the connection string in the `.\Presentation\Console.UI\Config.json` file.

> By default, while running the  `ConsoleUI` program, the program will start running the migrations and tries to insert the seed data that are in the csv files under the directory `Infrastructure/Data/Assets/`. This will be slower since each of the row will be inserted one-by-one ( The seed method was for aiding in integration test which I didn't do because of the lack of time.)
> Instead, you can press `Ctrl+C` when he ConsoleUI inserts the data and run the bulk insert by running the following TSQL statements in the server (You need to copy the files to the container if you are running the SQL Server on the container)

```sql
SET IDENTITY_INSERT flights ON;
BULK INSERT 
	Flights FROM '/home/flights.csv' 
			WITH ( FIELDTERMINATOR = ',',
			CSV ROWTERMINATOR = '\n', 
			FIRSTROW = 2
			);

```

 ## Database design

### Relationships
![[docs/Pasted image 20240901225517.png]]

### Schema

```sql
USE [AirlineScheduleDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 01/09/2024 10:56:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[flights]    Script Date: 01/09/2024 10:56:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flights](
	[flight_id] [int] IDENTITY(1,1) NOT NULL,
	[route_id] [int] NOT NULL,
	[departure_time] [datetime2](7) NOT NULL,
	[arrival_time] [datetime2](7) NOT NULL,
	[airline_id] [int] NOT NULL,
 CONSTRAINT [PK_flights] PRIMARY KEY CLUSTERED 
(
	[flight_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[routes]    Script Date: 01/09/2024 10:56:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[routes](
	[route_id] [int] IDENTITY(1,1) NOT NULL,
	[origin_city_id] [int] NOT NULL,
	[destination_city_id] [int] NOT NULL,
	[departure_date] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_routes] PRIMARY KEY CLUSTERED 
(
	[route_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[subscriptions]    Script Date: 01/09/2024 10:56:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[subscriptions](
	[agency_id] [int] NOT NULL,
	[origin_city_id] [int] NOT NULL,
	[destination_city_id] [int] NOT NULL,
	[RouteId] [int] NULL,
 CONSTRAINT [PK_subscriptions] PRIMARY KEY CLUSTERED 
(
	[agency_id] ASC,
	[origin_city_id] ASC,
	[destination_city_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[flights]  WITH NOCHECK ADD  CONSTRAINT [FK_flights_routes_route_id] FOREIGN KEY([route_id])
REFERENCES [dbo].[routes] ([route_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[flights] NOCHECK CONSTRAINT [FK_flights_routes_route_id]
GO
ALTER TABLE [dbo].[subscriptions]  WITH CHECK ADD  CONSTRAINT [FK_subscriptions_routes_RouteId] FOREIGN KEY([RouteId])
REFERENCES [dbo].[routes] ([route_id])
GO
ALTER TABLE [dbo].[subscriptions] CHECK CONSTRAINT [FK_subscriptions_routes_RouteId]
GO

```

### Overall structure of the application

The project is following Robert C. Martin's clean code concepts and is designed based on [Jason Taylor's implementation of the same](https://github.com/jasontaylordev/CleanArchitecture)

![[docs/Pasted image 20240901230553.png]]

,