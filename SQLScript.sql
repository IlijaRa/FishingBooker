USE [master]
GO
/****** Object:  Database [FishingBookerDB]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE DATABASE [FishingBookerDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FishingBookerDB', FILENAME = N'D:\SQLServer\MSSQL15.SQLEXPRESS\MSSQL\DATA\FishingBookerDB.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FishingBookerDB_log', FILENAME = N'D:\SQLServer\MSSQL15.SQLEXPRESS\MSSQL\DATA\FishingBookerDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 COLLATE SQL_Latin1_General_CP1_CI_AS
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FishingBookerDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FishingBookerDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FishingBookerDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FishingBookerDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FishingBookerDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FishingBookerDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FishingBookerDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FishingBookerDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FishingBookerDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FishingBookerDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FishingBookerDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FishingBookerDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FishingBookerDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FishingBookerDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FishingBookerDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FishingBookerDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FishingBookerDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FishingBookerDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FishingBookerDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FishingBookerDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FishingBookerDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FishingBookerDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FishingBookerDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FishingBookerDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FishingBookerDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FishingBookerDB] SET  MULTI_USER 
GO
ALTER DATABASE [FishingBookerDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FishingBookerDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FishingBookerDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FishingBookerDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FishingBookerDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FishingBookerDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FishingBookerDB] SET QUERY_STORE = OFF
GO
/****** Object:  Login [NT SERVICE\Winmgmt]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE LOGIN [NT SERVICE\Winmgmt] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLWriter]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE LOGIN [NT SERVICE\SQLWriter] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLTELEMETRY$SQLEXPRESS]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE LOGIN [NT SERVICE\SQLTELEMETRY$SQLEXPRESS] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT Service\MSSQL$SQLEXPRESS]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE LOGIN [NT Service\MSSQL$SQLEXPRESS] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT AUTHORITY\SYSTEM]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [DESKTOP-2JTNAL1\rakoc]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE LOGIN [DESKTOP-2JTNAL1\rakoc] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [BUILTIN\Users]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE LOGIN [BUILTIN\Users] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyTsqlExecutionLogin##]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE LOGIN [##MS_PolicyTsqlExecutionLogin##] WITH PASSWORD=N'ooBlpcNOGVOR01qp0X0Mcn2dpNsgmMSuTm+Qftl1/3w=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyTsqlExecutionLogin##] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyEventProcessingLogin##]    Script Date: 9/6/2022 7:59:32 PM ******/
CREATE LOGIN [##MS_PolicyEventProcessingLogin##] WITH PASSWORD=N'syjTgF0pMKneCIt5ecbM7q9j/rxZDqFG1VmTVqQz+aM=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyEventProcessingLogin##] DISABLE
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\Winmgmt]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLWriter]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT Service\MSSQL$SQLEXPRESS]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [DESKTOP-2JTNAL1\rakoc]
GO
USE [FishingBookerDB]
GO
GRANT VIEW ANY COLUMN ENCRYPTION KEY DEFINITION TO [public] AS [dbo]
GO
GRANT VIEW ANY COLUMN MASTER KEY DEFINITION TO [public] AS [dbo]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ContextKey] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[AdditionalServices]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdditionalServices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdventureReservations]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdventureReservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Place] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CreationTime] [datetime] NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndDate] [smalldatetime] NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[ValidityPeriodDate] [datetime2](7) NULL,
	[ValidityPeriodTime] [time](7) NULL,
	[dayOfWeek] [int] NULL,
	[Month] [int] NULL,
	[Year] [int] NULL,
	[MaxNumberOfPeople] [int] NOT NULL,
	[AdditionalServices] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Discount] [decimal](18, 0) NOT NULL,
	[IsReserved] [bit] NOT NULL,
	[ClientsEmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReservationType] [int] NOT NULL,
	[AdventureId] [int] NOT NULL,
	[InstructorId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_AdventureFastReservations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Adventures]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adventures](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PromotionDescription] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rating] [float] NOT NULL,
	[RatingSum] [float] NOT NULL,
	[RatingCount] [float] NOT NULL,
	[BehaviourRules] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdditionalServices] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Pricelist] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Price] [decimal](18, 0) NULL,
	[MaxNumberOfPeople] [int] NOT NULL,
	[FishingEquipment] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CancellationPolicy] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[InstructorId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK__Adventur__3214EC0766791AAF] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdventureSubscribers]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdventureSubscribers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientEmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[AdventureId] [int] NOT NULL,
 CONSTRAINT [PK_AdventureSubscribers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClientComplaints]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClientComplaints](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OwnerId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnerName] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnerSurname] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnerEmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClientsEmailAddress] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActionTitle] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Reason] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Status] [int] NULL,
	[ConcurrencyToken] [timestamp] NULL,
 CONSTRAINT [PK_ClientComplaints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CottageReservations]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CottageReservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CottageName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndDate] [smalldatetime] NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[ValidityPeriodDate] [smalldatetime] NOT NULL,
	[ValidityPeriodTime] [time](7) NOT NULL,
	[MaxNumberOfPeople] [int] NOT NULL,
	[AdditionalServices] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Discount] [decimal](18, 0) NOT NULL,
	[IsReserved] [bit] NOT NULL,
	[ClientsEmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReservationType] [int] NOT NULL,
	[CottageId] [int] NOT NULL,
	[OwnerId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_CottageReservations_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cottages]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cottages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PromotionDescription] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Rating] [float] NOT NULL,
	[RatingSum] [int] NOT NULL,
	[RatingCount] [int] NOT NULL,
	[BehaviourRules] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdditionalServices] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Pricelist] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[NumberOfRooms] [int] NOT NULL,
	[BedsPerRoom] [int] NOT NULL,
	[OwnerId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Cottages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeactivationRequests]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeactivationRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserSurname] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Reason] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Status] [int] NOT NULL,
	[ConcurrencyToken] [timestamp] NOT NULL,
 CONSTRAINT [PK_DeactivationRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Images]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Image] [image] NOT NULL,
	[AdventureId] [int] NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoyaltyProgram]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoyaltyProgram](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PointsAfterSuccResClient] [int] NOT NULL,
	[PointsAfterSuccResOwner] [int] NOT NULL,
 CONSTRAINT [PK_LoyaltyProgram] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoyaltyScale]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoyaltyScale](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoyaltyProgramId] [int] NOT NULL,
	[ScaleName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClientsBenefits] [int] NOT NULL,
	[OwnerBenefits] [int] NOT NULL,
	[MinEarnedPoints] [int] NOT NULL,
	[PickedColor] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_LoyaltyScale] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MoneyFlow]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MoneyFlow](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Percentage] [int] NOT NULL,
	[TotalIncome] [float] NOT NULL,
 CONSTRAINT [PK_MoneyFlow] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OwnerAvailabilitiesUnavailabilities]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OwnerAvailabilitiesUnavailabilities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromDate] [smalldatetime] NOT NULL,
	[FromTime] [time](7) NOT NULL,
	[ToDate] [smalldatetime] NOT NULL,
	[ToTime] [time](7) NOT NULL,
	[OwnerId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Type] [int] NOT NULL,
	[Text] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_InstructorsAvailabilities] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OwnerAvailabilityStandardReservations]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OwnerAvailabilityStandardReservations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[FromDate] [smalldatetime] NOT NULL,
	[FromTime] [time](7) NOT NULL,
	[ToDate] [smalldatetime] NOT NULL,
	[ToTime] [time](7) NOT NULL,
	[InstructorId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_OwnerAvailabilityStandardReservations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Records]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Records](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientsEmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[InstructorsEmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Comment] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ImpressionType] [int] NOT NULL,
	[ClientId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[InstructorId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Records] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegUsers]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegUsers](
	[Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Surname] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumber] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Password] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Type] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[City] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Country] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Biography] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TotalScalePoints] [real] NOT NULL,
	[Rating] [real] NOT NULL,
	[RatingSum] [real] NOT NULL,
	[RatingCount] [real] NOT NULL,
	[Penalties] [int] NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.RegUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReservationHistory]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReservationHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientsEmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActionTitle] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndDate] [smalldatetime] NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[OwnerId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClientPercentage] [int] NULL,
	[OwnerPercentage] [int] NULL,
	[MoneyFlowPercentage] [int] NULL,
 CONSTRAINT [PK_ReservationHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Revisions]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Revisions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientsEmailAddress] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EntityTitle] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnersEmailAddress] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ActionRating] [int] NOT NULL,
	[OwnerInstructorRating] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[ConcurrencyToken] [timestamp] NOT NULL,
 CONSTRAINT [PK_Revisions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Name] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipReservations]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipReservations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ShipName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StartDate] [smalldatetime] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndDate] [smalldatetime] NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[ValidityPeriodDate] [smalldatetime] NOT NULL,
	[ValidityPeriodTime] [time](7) NOT NULL,
	[MaxNumberOfPeople] [int] NOT NULL,
	[AdditionalServices] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[Discount] [decimal](18, 0) NOT NULL,
	[IsReserved] [bit] NOT NULL,
	[ClientsEmailAddress] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ReservationType] [int] NOT NULL,
	[ShipId] [int] NOT NULL,
	[OwnerId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ShipReservations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ships]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ships](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Address] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PromotionDescription] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Rating] [float] NULL,
	[RatingSum] [int] NULL,
	[RatingCount] [int] NULL,
	[BehaviourRules] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdditionalServices] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Pricelist] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Price] [decimal](18, 0) NOT NULL,
	[FishingEquipment] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[NavigationEquipment] [nvarchar](256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CancellationPolicy] [int] NOT NULL,
	[SpecificationId] [int] NOT NULL,
	[OwnerId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_Ships] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipSpecifications]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipSpecifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Length] [float] NOT NULL,
	[EngineNumber] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EnginePower] [float] NOT NULL,
	[MaxSpeed] [float] NOT NULL,
	[PeopleCapacity] [int] NOT NULL,
	[ShipId] [int] NOT NULL,
 CONSTRAINT [PK_ShipSpecifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 9/6/2022 7:59:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ClaimType] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimValue] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_dbo.UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 9/6/2022 7:59:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProviderKey] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 9/6/2022 7:59:33 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RoleId] [nvarchar](128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 9/6/2022 7:59:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[RegUsers]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 9/6/2022 7:59:33 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Roles]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 9/6/2022 7:59:33 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UserClaim]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 9/6/2022 7:59:33 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UserLogin]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_RoleId]    Script Date: 9/6/2022 7:59:33 PM ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserId]    Script Date: 9/6/2022 7:59:33 PM ******/
CREATE NONCLUSTERED INDEX [IX_UserId] ON [dbo].[UserRole]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RegUsers] ADD  CONSTRAINT [DF__RegUsers__TotalS__02284B6B]  DEFAULT ((0)) FOR [TotalScalePoints]
GO
ALTER TABLE [dbo].[RegUsers] ADD  CONSTRAINT [DF__RegUsers__Rating__031C6FA4]  DEFAULT ((0)) FOR [Rating]
GO
ALTER TABLE [dbo].[RegUsers] ADD  CONSTRAINT [DF__RegUsers__Rating__041093DD]  DEFAULT ((0)) FOR [RatingSum]
GO
ALTER TABLE [dbo].[RegUsers] ADD  CONSTRAINT [DF__RegUsers__Rating__0504B816]  DEFAULT ((0)) FOR [RatingCount]
GO
ALTER TABLE [dbo].[RegUsers] ADD  CONSTRAINT [DF__RegUsers__Penalt__19AACF41]  DEFAULT ((0)) FOR [Penalties]
GO
ALTER TABLE [dbo].[AdventureReservations]  WITH CHECK ADD  CONSTRAINT [FK_AdventureFastReservations_Adventures] FOREIGN KEY([AdventureId])
REFERENCES [dbo].[Adventures] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AdventureReservations] CHECK CONSTRAINT [FK_AdventureFastReservations_Adventures]
GO
ALTER TABLE [dbo].[AdventureReservations]  WITH CHECK ADD  CONSTRAINT [FK_AdventureReservations_RegUsers] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[RegUsers] ([Id])
GO
ALTER TABLE [dbo].[AdventureReservations] CHECK CONSTRAINT [FK_AdventureReservations_RegUsers]
GO
ALTER TABLE [dbo].[Adventures]  WITH CHECK ADD  CONSTRAINT [FK_Adventures_RegUsers] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[RegUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Adventures] CHECK CONSTRAINT [FK_Adventures_RegUsers]
GO
ALTER TABLE [dbo].[Cottages]  WITH CHECK ADD  CONSTRAINT [FK_Cottages_RegUsers] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[RegUsers] ([Id])
GO
ALTER TABLE [dbo].[Cottages] CHECK CONSTRAINT [FK_Cottages_RegUsers]
GO
ALTER TABLE [dbo].[LoyaltyScale]  WITH CHECK ADD  CONSTRAINT [FK_LoyaltyScale_LoyaltyProgram] FOREIGN KEY([LoyaltyProgramId])
REFERENCES [dbo].[LoyaltyProgram] ([Id])
GO
ALTER TABLE [dbo].[LoyaltyScale] CHECK CONSTRAINT [FK_LoyaltyScale_LoyaltyProgram]
GO
ALTER TABLE [dbo].[OwnerAvailabilitiesUnavailabilities]  WITH CHECK ADD  CONSTRAINT [FK_InstructorsAvailabilities_InstructorsAvailabilities] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[RegUsers] ([Id])
GO
ALTER TABLE [dbo].[OwnerAvailabilitiesUnavailabilities] CHECK CONSTRAINT [FK_InstructorsAvailabilities_InstructorsAvailabilities]
GO
ALTER TABLE [dbo].[Records]  WITH CHECK ADD  CONSTRAINT [FK_Records_RegUsers] FOREIGN KEY([ClientId])
REFERENCES [dbo].[RegUsers] ([Id])
GO
ALTER TABLE [dbo].[Records] CHECK CONSTRAINT [FK_Records_RegUsers]
GO
ALTER TABLE [dbo].[Records]  WITH CHECK ADD  CONSTRAINT [FK_Records_RegUsers1] FOREIGN KEY([InstructorId])
REFERENCES [dbo].[RegUsers] ([Id])
GO
ALTER TABLE [dbo].[Records] CHECK CONSTRAINT [FK_Records_RegUsers1]
GO
ALTER TABLE [dbo].[RegUsers]  WITH CHECK ADD  CONSTRAINT [FK_RegUsers_RegUsers] FOREIGN KEY([Id])
REFERENCES [dbo].[RegUsers] ([Id])
GO
ALTER TABLE [dbo].[RegUsers] CHECK CONSTRAINT [FK_RegUsers_RegUsers]
GO
ALTER TABLE [dbo].[ReservationHistory]  WITH CHECK ADD  CONSTRAINT [FK_ReservationHistory_ReservationHistory] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[RegUsers] ([Id])
GO
ALTER TABLE [dbo].[ReservationHistory] CHECK CONSTRAINT [FK_ReservationHistory_ReservationHistory]
GO
ALTER TABLE [dbo].[Ships]  WITH CHECK ADD  CONSTRAINT [FK_Ships_RegUsers] FOREIGN KEY([OwnerId])
REFERENCES [dbo].[RegUsers] ([Id])
GO
ALTER TABLE [dbo].[Ships] CHECK CONSTRAINT [FK_Ships_RegUsers]
GO
ALTER TABLE [dbo].[ShipSpecifications]  WITH CHECK ADD  CONSTRAINT [FK_ShipSpecifications_Ships] FOREIGN KEY([ShipId])
REFERENCES [dbo].[Ships] ([Id])
GO
ALTER TABLE [dbo].[ShipSpecifications] CHECK CONSTRAINT [FK_ShipSpecifications_Ships]
GO
ALTER TABLE [dbo].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[RegUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaim] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[RegUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[RegUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
USE [master]
GO
ALTER DATABASE [FishingBookerDB] SET  READ_WRITE 
GO
