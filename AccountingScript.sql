USE [master]
GO
/****** Object:  Database [accounting]    Script Date: 14.05.2023 1:41:04 ******/
CREATE DATABASE [accounting]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'accounting', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\accounting.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'accounting_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\accounting_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [accounting] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [accounting].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [accounting] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [accounting] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [accounting] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [accounting] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [accounting] SET ARITHABORT OFF 
GO
ALTER DATABASE [accounting] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [accounting] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [accounting] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [accounting] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [accounting] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [accounting] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [accounting] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [accounting] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [accounting] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [accounting] SET  DISABLE_BROKER 
GO
ALTER DATABASE [accounting] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [accounting] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [accounting] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [accounting] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [accounting] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [accounting] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [accounting] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [accounting] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [accounting] SET  MULTI_USER 
GO
ALTER DATABASE [accounting] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [accounting] SET DB_CHAINING OFF 
GO
ALTER DATABASE [accounting] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [accounting] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [accounting] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [accounting] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [accounting] SET QUERY_STORE = OFF
GO
USE [accounting]
GO
/****** Object:  Table [dbo].[Budget]    Script Date: 14.05.2023 1:41:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Budget](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_User] [int] NOT NULL,
	[Id_Finance] [int] NOT NULL,
	[Total] [money] NOT NULL,
 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Finance]    Script Date: 14.05.2023 1:41:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Finance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_User] [int] NOT NULL,
	[Id_IncomeOrExpenses] [int] NOT NULL,
	[Id_TypeIncome] [int] NULL,
	[Id_TypeExpenses] [int] NULL,
	[Date] [date] NOT NULL,
	[Sum] [money] NOT NULL,
 CONSTRAINT [PK_Finance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IncomeOrExpenses]    Script Date: 14.05.2023 1:41:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IncomeOrExpenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_IncomeOrExpenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeExpenses]    Script Date: 14.05.2023 1:41:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeExpenses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_TypeExpenses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeIncome]    Script Date: 14.05.2023 1:41:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeIncome](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TypeIncome] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 14.05.2023 1:41:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[IncomeOrExpenses] ON 

INSERT [dbo].[IncomeOrExpenses] ([Id], [Name]) VALUES (1, N'Доход')
INSERT [dbo].[IncomeOrExpenses] ([Id], [Name]) VALUES (2, N'Расход')
SET IDENTITY_INSERT [dbo].[IncomeOrExpenses] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeExpenses] ON 

INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (1, N'Еда')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (2, N'Развлечения')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (3, N'Спорт')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (4, N'Транспорт')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (5, N'Штраф')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (6, N'Обучение')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (7, N'Налоги')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (8, N'Одежда')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (9, N'Связь')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (10, N'ЖКХ')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (11, N'Благотворительность')
INSERT [dbo].[TypeExpenses] ([Id], [Name]) VALUES (12, N'Кредит')
SET IDENTITY_INSERT [dbo].[TypeExpenses] OFF
GO
SET IDENTITY_INSERT [dbo].[TypeIncome] ON 

INSERT [dbo].[TypeIncome] ([Id], [Name]) VALUES (1, N'Зарплата')
INSERT [dbo].[TypeIncome] ([Id], [Name]) VALUES (2, N'Подработка')
INSERT [dbo].[TypeIncome] ([Id], [Name]) VALUES (3, N'Премия')
INSERT [dbo].[TypeIncome] ([Id], [Name]) VALUES (4, N'Подарок')
INSERT [dbo].[TypeIncome] ([Id], [Name]) VALUES (5, N'Кредит')
INSERT [dbo].[TypeIncome] ([Id], [Name]) VALUES (6, N'Кэшбэк')
SET IDENTITY_INSERT [dbo].[TypeIncome] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Lastname], [Name], [Patronymic], [Login], [Password]) VALUES (1, N'Приказюк', N'Иван', N'Евгеньевич', N'Ivan16', N'qwerty123')
INSERT [dbo].[User] ([Id], [Lastname], [Name], [Patronymic], [Login], [Password]) VALUES (3, N'Павлов', N'Дмитрий', NULL, N'Di', N'228q')
INSERT [dbo].[User] ([Id], [Lastname], [Name], [Patronymic], [Login], [Password]) VALUES (4, N'wdrgew', N'weger', N'wgew', N'1', N'1')
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_Finance] FOREIGN KEY([Id_Finance])
REFERENCES [dbo].[Finance] ([Id])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_Finance]
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_User] FOREIGN KEY([Id_User])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_User]
GO
ALTER TABLE [dbo].[Finance]  WITH CHECK ADD  CONSTRAINT [FK_Finance_IncomeOrExpenses] FOREIGN KEY([Id_IncomeOrExpenses])
REFERENCES [dbo].[IncomeOrExpenses] ([Id])
GO
ALTER TABLE [dbo].[Finance] CHECK CONSTRAINT [FK_Finance_IncomeOrExpenses]
GO
ALTER TABLE [dbo].[Finance]  WITH CHECK ADD  CONSTRAINT [FK_Finance_TypeExpenses] FOREIGN KEY([Id_TypeExpenses])
REFERENCES [dbo].[TypeExpenses] ([Id])
GO
ALTER TABLE [dbo].[Finance] CHECK CONSTRAINT [FK_Finance_TypeExpenses]
GO
ALTER TABLE [dbo].[Finance]  WITH CHECK ADD  CONSTRAINT [FK_Finance_TypeIncome] FOREIGN KEY([Id_TypeIncome])
REFERENCES [dbo].[TypeIncome] ([Id])
GO
ALTER TABLE [dbo].[Finance] CHECK CONSTRAINT [FK_Finance_TypeIncome]
GO
ALTER TABLE [dbo].[Finance]  WITH CHECK ADD  CONSTRAINT [FK_Finance_User] FOREIGN KEY([Id_User])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[Finance] CHECK CONSTRAINT [FK_Finance_User]
GO
USE [master]
GO
ALTER DATABASE [accounting] SET  READ_WRITE 
GO
