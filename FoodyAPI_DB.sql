USE [master]
GO
/****** Object:  Database [FoodyAPI_DB]    Script Date: 3/27/2023 2:37:36 AM ******/
CREATE DATABASE [FoodyAPI_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FoodyAPI_DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLJPMR5502\MSSQL\DATA\FoodyAPI_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FoodyAPI_DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLJPMR5502\MSSQL\DATA\FoodyAPI_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [FoodyAPI_DB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FoodyAPI_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FoodyAPI_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FoodyAPI_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FoodyAPI_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FoodyAPI_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FoodyAPI_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FoodyAPI_DB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FoodyAPI_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FoodyAPI_DB] SET  MULTI_USER 
GO
ALTER DATABASE [FoodyAPI_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FoodyAPI_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FoodyAPI_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FoodyAPI_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FoodyAPI_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FoodyAPI_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FoodyAPI_DB] SET QUERY_STORE = OFF
GO
USE [FoodyAPI_DB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/27/2023 2:37:37 AM ******/
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
/****** Object:  Table [dbo].[Comments]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Content] [ntext] NOT NULL,
	[ParentId] [int] NULL,
	[UserId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](40) NOT NULL,
	[Message] [ntext] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DishComments]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DishComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DishId] [int] NOT NULL,
	[CommentId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_DishComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Dishes]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dishes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	[ImagePath] [nvarchar](max) NOT NULL,
	[Description] [ntext] NULL,
	[Price] [decimal](11, 2) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Dishes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DishIngredients]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DishIngredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DishId] [int] NOT NULL,
	[IngreId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_DishIngredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DishTypeDishes]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DishTypeDishes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DishTypeId] [int] NOT NULL,
	[DishId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_DishTypeDishes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DishTypes]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DishTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_DishTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ingredients]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ingredients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DishId] [int] NOT NULL,
	[OrderId] [int] NOT NULL,
	[Qty] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[TotalPrice] [decimal](5, 2) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](15) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 3/27/2023 2:37:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[Username] [nvarchar](25) NOT NULL,
	[Email] [nvarchar](40) NOT NULL,
	[Password] [nvarchar](300) NOT NULL,
	[ImagePath] [varchar](max) NULL,
	[Token] [nvarchar](100) NULL,
	[IsActived] [bit] NULL,
	[RoleId] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230305173214_Initial', N'7.0.2')
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([Id], [Content], [ParentId], [UserId], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (2, N'Carbonara is so tasty', NULL, 34, CAST(N'2023-03-19T13:18:38.0300000' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[Dishes] ON 

INSERT [dbo].[Dishes] ([Id], [Name], [ImagePath], [Description], [Price], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (2, N'spaghetti carbonara', N'slika.jpg', N'Bela Italiano carbonara', CAST(7.00 AS Decimal(11, 2)), CAST(N'2023-03-19T13:24:07.6100000' AS DateTime2), NULL, 0)
INSERT [dbo].[Dishes] ([Id], [Name], [ImagePath], [Description], [Price], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (3, N'Spaghetti carbonaraa', N'/images/6afb2497-0204-47a5-bde9-de069d381379_carbonara.JPG', N'Bella Italiano carbonara', CAST(6.00 AS Decimal(11, 2)), CAST(N'2023-03-19T15:40:18.7300000' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[Dishes] OFF
GO
SET IDENTITY_INSERT [dbo].[DishIngredients] ON 

INSERT [dbo].[DishIngredients] ([Id], [DishId], [IngreId], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (1, 3, 2, CAST(N'2023-03-19T17:18:12.9333333' AS DateTime2), NULL, 0)
INSERT [dbo].[DishIngredients] ([Id], [DishId], [IngreId], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (2, 3, 5, CAST(N'2023-03-19T17:19:42.9966667' AS DateTime2), NULL, 0)
INSERT [dbo].[DishIngredients] ([Id], [DishId], [IngreId], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (3, 3, 7, CAST(N'2023-03-19T17:20:01.4200000' AS DateTime2), NULL, 0)
INSERT [dbo].[DishIngredients] ([Id], [DishId], [IngreId], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (4, 3, 4, CAST(N'2023-03-19T17:20:21.0466667' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[DishIngredients] OFF
GO
SET IDENTITY_INSERT [dbo].[DishTypeDishes] ON 

INSERT [dbo].[DishTypeDishes] ([Id], [DishTypeId], [DishId], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (1, 1, 3, CAST(N'2023-03-19T17:22:40.7333333' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[DishTypeDishes] OFF
GO
SET IDENTITY_INSERT [dbo].[DishTypes] ON 

INSERT [dbo].[DishTypes] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (1, N'Pasta', CAST(N'2023-03-19T13:12:09.5366667' AS DateTime2), NULL, 0)
INSERT [dbo].[DishTypes] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (2, N'Pizza', CAST(N'2023-03-19T13:12:25.6266667' AS DateTime2), NULL, 0)
INSERT [dbo].[DishTypes] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (3, N'Cake', CAST(N'2023-03-19T13:13:23.7300000' AS DateTime2), NULL, 0)
INSERT [dbo].[DishTypes] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (4, N'', CAST(N'2023-03-19T13:14:46.8166667' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[DishTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[Ingredients] ON 

INSERT [dbo].[Ingredients] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (1, N'Tomato sauce', CAST(N'2023-03-19T13:03:33.0766667' AS DateTime2), NULL, 0)
INSERT [dbo].[Ingredients] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (2, N'Bacon', CAST(N'2023-03-19T13:05:19.3466667' AS DateTime2), NULL, 0)
INSERT [dbo].[Ingredients] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (3, N'Minced meat', CAST(N'2023-03-19T13:05:41.5066667' AS DateTime2), NULL, 0)
INSERT [dbo].[Ingredients] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (4, N'Olive oil', CAST(N'2023-03-19T13:06:50.2566667' AS DateTime2), NULL, 0)
INSERT [dbo].[Ingredients] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (5, N'Garlic cloves', CAST(N'2023-03-19T13:07:14.0966667' AS DateTime2), NULL, 0)
INSERT [dbo].[Ingredients] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (6, N'Onion', CAST(N'2023-03-19T13:07:34.1800000' AS DateTime2), NULL, 0)
INSERT [dbo].[Ingredients] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (7, N'Pecorino', CAST(N'2023-03-19T13:09:03.4166667' AS DateTime2), NULL, 0)
INSERT [dbo].[Ingredients] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (8, N'Parmesan', CAST(N'2023-03-19T13:09:13.6033333' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[Ingredients] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (1, N'Admin', CAST(N'2023-03-13T23:41:12.3966667' AS DateTime2), NULL, 0)
INSERT [dbo].[Roles] ([Id], [Name], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (2, N'Customer', CAST(N'2023-03-13T23:42:24.2800000' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [Email], [Password], [ImagePath], [Token], [IsActived], [RoleId], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (13, N'Jovan', N'Popovic', N'jovan', N'jovanpop@gmail.com', N'dXMe2HmV7jPon4kYxyVUfBmV5g86kNMqlrzQty2O1BahLmlrs9wLhTGcEPjz6gUhSJcARtbGrCYyCVSTnzdKFRijmt80sBG2yWSh2mWCmNxjCn9h', NULL, NULL, 1, 1, CAST(N'2023-03-14T00:05:00.3533333' AS DateTime2), NULL, 0)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [Email], [Password], [ImagePath], [Token], [IsActived], [RoleId], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (34, N'Moralna', N'Pobeda', N'moralnaaax', N'moralnapobeda71@gmail.com', N'dXMe2HmV7jPon4kYxyVUfBmV5g86kNMqlrzQty2O1BahLmlrs9wLhTGcEPjz6gUhSJcARtbGrCYyCVSTnzdKFRijmt80sBG2yWSh2mWCmNxjCn9h', NULL, NULL, 1, 2, CAST(N'2023-03-18T11:18:50.4000000' AS DateTime2), NULL, 0)
INSERT [dbo].[Users] ([Id], [FirstName], [LastName], [Username], [Email], [Password], [ImagePath], [Token], [IsActived], [RoleId], [CreatedAt], [UpdatedAt], [IsDeleted]) VALUES (35, N'Jovan', N'Popovic', N'popjer', N'armygamesict@gmail.com', N'+NrjIc/jjcK841TuaGokdBvA141fctF7KWiCFE+BD2CiFXFOzLjKuj1oiuJmpsjrk1wnyHzJswgavm62rZkp8qVUa6RKnajfzCNc0LYYCh2M7AcB', NULL, NULL, 1, 2, CAST(N'2023-03-18T12:51:23.4366667' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Comments_ParentId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_ParentId] ON [dbo].[Comments]
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_UserId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_Comments_UserId] ON [dbo].[Comments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DishComments_CommentId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_DishComments_CommentId] ON [dbo].[DishComments]
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DishComments_DishId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_DishComments_DishId] ON [dbo].[DishComments]
(
	[DishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DishIngredients_DishId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_DishIngredients_DishId] ON [dbo].[DishIngredients]
(
	[DishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DishIngredients_IngreId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_DishIngredients_IngreId] ON [dbo].[DishIngredients]
(
	[IngreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DishTypeDishes_DishId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_DishTypeDishes_DishId] ON [dbo].[DishTypeDishes]
(
	[DishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DishTypeDishes_DishTypeId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_DishTypeDishes_DishTypeId] ON [dbo].[DishTypeDishes]
(
	[DishTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderItems_DishId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems_DishId] ON [dbo].[OrderItems]
(
	[DishId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderItems_OrderId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems_OrderId] ON [dbo].[OrderItems]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_UserId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_Orders_UserId] ON [dbo].[Orders]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_RoleId]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_Users_RoleId] ON [dbo].[Users]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_Username_Email]    Script Date: 3/27/2023 2:37:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Username_Email] ON [dbo].[Users]
(
	[Username] ASC,
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Comments] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Contacts] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Contacts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DishComments] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DishComments] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Dishes] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Dishes] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DishIngredients] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DishIngredients] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DishTypeDishes] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DishTypeDishes] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[DishTypes] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[DishTypes] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Ingredients] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Ingredients] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[OrderItems] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[OrderItems] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActived]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_RoleId]  DEFAULT ((2)) FOR [RoleId]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Comments_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Comments] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Comments_ParentId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users_UserId]
GO
ALTER TABLE [dbo].[DishComments]  WITH CHECK ADD  CONSTRAINT [FK_DishComments_Comments_CommentId] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comments] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DishComments] CHECK CONSTRAINT [FK_DishComments_Comments_CommentId]
GO
ALTER TABLE [dbo].[DishComments]  WITH CHECK ADD  CONSTRAINT [FK_DishComments_Dishes_DishId] FOREIGN KEY([DishId])
REFERENCES [dbo].[Dishes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DishComments] CHECK CONSTRAINT [FK_DishComments_Dishes_DishId]
GO
ALTER TABLE [dbo].[DishIngredients]  WITH CHECK ADD  CONSTRAINT [FK_DishIngredients_Dishes_DishId] FOREIGN KEY([DishId])
REFERENCES [dbo].[Dishes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DishIngredients] CHECK CONSTRAINT [FK_DishIngredients_Dishes_DishId]
GO
ALTER TABLE [dbo].[DishIngredients]  WITH CHECK ADD  CONSTRAINT [FK_DishIngredients_Ingredients_IngreId] FOREIGN KEY([IngreId])
REFERENCES [dbo].[Ingredients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DishIngredients] CHECK CONSTRAINT [FK_DishIngredients_Ingredients_IngreId]
GO
ALTER TABLE [dbo].[DishTypeDishes]  WITH CHECK ADD  CONSTRAINT [FK_DishTypeDishes_Dishes_DishId] FOREIGN KEY([DishId])
REFERENCES [dbo].[Dishes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DishTypeDishes] CHECK CONSTRAINT [FK_DishTypeDishes_Dishes_DishId]
GO
ALTER TABLE [dbo].[DishTypeDishes]  WITH CHECK ADD  CONSTRAINT [FK_DishTypeDishes_DishTypes_DishTypeId] FOREIGN KEY([DishTypeId])
REFERENCES [dbo].[DishTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DishTypeDishes] CHECK CONSTRAINT [FK_DishTypeDishes_DishTypes_DishTypeId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Dishes_DishId] FOREIGN KEY([DishId])
REFERENCES [dbo].[Dishes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Dishes_DishId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_UserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles_RoleId]
GO
USE [master]
GO
ALTER DATABASE [FoodyAPI_DB] SET  READ_WRITE 
GO
