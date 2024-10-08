USE [master]
GO
/****** Object:  Database [Demodb]    Script Date: 27-09-2024 20:31:23 ******/
CREATE DATABASE [Demodb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Demodb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019\MSSQL\DATA\Demodb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Demodb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQL2019\MSSQL\DATA\Demodb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Demodb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Demodb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Demodb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Demodb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Demodb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Demodb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Demodb] SET ARITHABORT OFF 
GO
ALTER DATABASE [Demodb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Demodb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Demodb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Demodb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Demodb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Demodb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Demodb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Demodb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Demodb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Demodb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Demodb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Demodb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Demodb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Demodb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Demodb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Demodb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Demodb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Demodb] SET RECOVERY FULL 
GO
ALTER DATABASE [Demodb] SET  MULTI_USER 
GO
ALTER DATABASE [Demodb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Demodb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Demodb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Demodb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Demodb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Demodb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Demodb', N'ON'
GO
ALTER DATABASE [Demodb] SET QUERY_STORE = OFF
GO
USE [Demodb]
GO
/****** Object:  Table [dbo].[tblCity]    Script Date: 27-09-2024 20:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateId] [int] NULL,
	[CityName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblState]    Script Date: 27-09-2024 20:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUserRegistration]    Script Date: 27-09-2024 20:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUserRegistration](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Phone] [varchar](15) NOT NULL,
	[Address] [varchar](250) NULL,
	[StateId] [int] NULL,
	[CityId] [int] NULL,
	[State] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblCity]  WITH CHECK ADD FOREIGN KEY([StateId])
REFERENCES [dbo].[tblState] ([Id])
GO
ALTER TABLE [dbo].[tblUserRegistration]  WITH CHECK ADD FOREIGN KEY([CityId])
REFERENCES [dbo].[tblCity] ([Id])
GO
ALTER TABLE [dbo].[tblUserRegistration]  WITH CHECK ADD FOREIGN KEY([StateId])
REFERENCES [dbo].[tblState] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[sp_DeleteUser]    Script Date: 27-09-2024 20:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_DeleteUser]
    @Id INT
AS
BEGIN
    DELETE FROM tblUserRegistration WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetAllUsers]    Script Date: 27-09-2024 20:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetAllUsers]
AS
BEGIN
    SELECT Id, Name, Email, Phone, Address, StateId, CityId FROM tblUserRegistration;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_GetUserById]    Script Date: 27-09-2024 20:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_GetUserById]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM tblUserRegistration WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[sp_InsertUserRegistration]    Script Date: 27-09-2024 20:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_InsertUserRegistration]
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Address VARCHAR(250),
    @StateId INT,
    @CityId INT
AS
BEGIN
    INSERT INTO tblUserRegistration (Name, Email, Phone, Address, StateId, CityId)
    VALUES (@Name, @Email, @Phone, @Address, @StateId, @CityId);
	    SELECT SCOPE_IDENTITY() AS Id; 

END
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateUserRegistration]    Script Date: 27-09-2024 20:31:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_UpdateUserRegistration]
    @Id INT,
    @Name VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Address VARCHAR(250),
    @StateId INT,
    @CityId INT
AS
BEGIN
    UPDATE tblUserRegistration
    SET Name = @Name, Email = @Email, Phone = @Phone, Address = @Address, StateId = @StateId, CityId = @CityId
    WHERE Id = @Id;
END
GO
USE [master]
GO
ALTER DATABASE [Demodb] SET  READ_WRITE 
GO
