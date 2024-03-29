USE [master]
GO
/****** Object:  Database [BestBio]    Script Date: 22-03-2023 09:24:32 ******/
CREATE DATABASE [BestBio]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BestBio', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\BestBio.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BestBio_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER01\MSSQL\DATA\BestBio_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BestBio] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BestBio].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BestBio] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BestBio] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BestBio] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BestBio] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BestBio] SET ARITHABORT OFF 
GO
ALTER DATABASE [BestBio] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BestBio] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BestBio] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BestBio] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BestBio] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BestBio] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BestBio] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BestBio] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BestBio] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BestBio] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BestBio] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BestBio] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BestBio] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BestBio] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BestBio] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BestBio] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BestBio] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BestBio] SET RECOVERY FULL 
GO
ALTER DATABASE [BestBio] SET  MULTI_USER 
GO
ALTER DATABASE [BestBio] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BestBio] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BestBio] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BestBio] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BestBio] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BestBio] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'BestBio', N'ON'
GO
ALTER DATABASE [BestBio] SET QUERY_STORE = OFF
GO
USE [BestBio]
GO
/****** Object:  User [Mads]    Script Date: 22-03-2023 09:24:32 ******/
CREATE USER [Mads] FOR LOGIN [Mads] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [Mads]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [Mads]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [Mads]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [Mads]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [Mads]
GO
ALTER ROLE [db_datareader] ADD MEMBER [Mads]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [Mads]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [Mads]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [Mads]
GO
/****** Object:  Table [dbo].[Movies]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movies](
	[MovieID] [int] IDENTITY(1,1) NOT NULL,
	[MovieName] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservations]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservations](
	[ReservationID] [int] IDENTITY(1,1) NOT NULL,
	[SeatID] [int] NOT NULL,
	[MovieID] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Email] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ReservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seats]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seats](
	[SeatID] [int] IDENTITY(0,1) NOT NULL,
	[SeatRow] [varchar](255) NOT NULL,
	[SeatNumber] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SeatID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Address] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[PhoneNumber] [varchar](50) NOT NULL,
	[PasswordSalt] [varbinary](max) NULL,
	[PasswordHash] [varbinary](max) NULL,
 CONSTRAINT [PK_Users_1] PRIMARY KEY CLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Users] UNIQUE NONCLUSTERED 
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movies] ([MovieID])
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD FOREIGN KEY([SeatID])
REFERENCES [dbo].[Seats] ([SeatID])
GO
ALTER TABLE [dbo].[Reservations]  WITH CHECK ADD  CONSTRAINT [FK_Reservations_Users] FOREIGN KEY([Email])
REFERENCES [dbo].[Users] ([Email])
GO
ALTER TABLE [dbo].[Reservations] CHECK CONSTRAINT [FK_Reservations_Users]
GO
/****** Object:  StoredProcedure [dbo].[Get_User]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_User] @Email varchar(60)
AS
begin
SELECT PasswordHash, PasswordSalt FROM Users where Email = @Email
end
GO
/****** Object:  StoredProcedure [dbo].[Get_User_Data]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Get_User_Data] 
	-- Add the parameters for the stored procedure here
	@Email varchar (60)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select Name, Address, Email, PhoneNumber from Users where Email = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[GetRerservedSeats]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRerservedSeats] 
	@MovieID int, @Date datetime
AS
BEGIN
	SET NOCOUNT ON;
	SELECT SeatID from Reservations where MovieID = @MovieID and Date = @Date
END
GO
/****** Object:  StoredProcedure [dbo].[Insert_User]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Insert_User] @Name varchar(60), @Address varchar(100), @Email varchar(60), @PhoneNumber varchar(12), @PasswordHash varbinary(255), @PasswordSalt varbinary(255)
as
begin
insert into Users (name, Address, Email, PhoneNumber, PasswordHash, PasswordSalt)
values (@Name, @Address, @Email, @PhoneNumber, @PasswordHash, @PasswordSalt)
end
GO
/****** Object:  StoredProcedure [dbo].[RemoveUser]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RemoveUser] 
	@Email varchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	delete from Users where Email = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[Reserve]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Reserve] 
	@MovieID int, @SeatID int, @Date datetime, @Email varchar(50)
AS
BEGIN
	SET NOCOUNT ON;

	insert into Reservations (SeatID, MovieID, Date, Email)
	values (@SeatID, @MovieID, @Date, @Email)
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 22-03-2023 09:24:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUser]
	@Email varchar(50), @Address varchar(50), @PhoneNumber varchar(50), @Name varchar (50), @PasswordHash varbinary(max), @PasswordSalt varbinary(max)
AS
BEGIN
	SET NOCOUNT ON;
	update Users
	set Address = @Address, PhoneNumber = @PhoneNumber, Name = @Name, PasswordHash = @PasswordHash, PasswordSalt = @PasswordSalt
	where Email = @Email
END
GO
USE [master]
GO
ALTER DATABASE [BestBio] SET  READ_WRITE 
GO
