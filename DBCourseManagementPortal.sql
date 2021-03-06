USE [master]
GO
/****** Object:  Database [DBCourseManagementPortal]    Script Date: 25.02.2022 17:22:35 ******/
CREATE DATABASE [DBCourseManagementPortal]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBCourseManagementPortal1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DBCourseManagementPortal1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBCourseManagementPortal1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\DBCourseManagementPortal1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DBCourseManagementPortal] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBCourseManagementPortal].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBCourseManagementPortal] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBCourseManagementPortal] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBCourseManagementPortal] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBCourseManagementPortal] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBCourseManagementPortal] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DBCourseManagementPortal] SET  MULTI_USER 
GO
ALTER DATABASE [DBCourseManagementPortal] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBCourseManagementPortal] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBCourseManagementPortal] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBCourseManagementPortal] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBCourseManagementPortal] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBCourseManagementPortal] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [DBCourseManagementPortal] SET QUERY_STORE = OFF
GO
USE [DBCourseManagementPortal]
GO
/****** Object:  Table [dbo].[tblCourse]    Script Date: 25.02.2022 17:22:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Duration] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[ModificationTime] [datetime] NULL,
 CONSTRAINT [PK_tblCourse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOngoingCourses]    Script Date: 25.02.2022 17:22:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOngoingCourses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CourseId] [int] NOT NULL,
	[TeacherId] [int] NOT NULL,
	[PlannedEndDate] [date] NOT NULL,
	[PlannedStartDate] [date] NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK_tblOngoingCourses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblOngoingCourseStudents]    Script Date: 25.02.2022 17:22:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblOngoingCourseStudents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OngoingCourseId] [int] NOT NULL,
	[LessonName] [nvarchar](max) NOT NULL,
	[StudentId] [int] NOT NULL,
	[IsInLesson] [nvarchar](50) NOT NULL,
	[LessonDate] [date] NOT NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_tblOngoingCourseStudents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblStudent]    Script Date: 25.02.2022 17:22:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblStudent](
	[Id] [int] IDENTITY(1000,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[CreationTime] [datetime] NOT NULL,
	[ModificationTime] [datetime] NULL,
 CONSTRAINT [PK_tblStudent] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTeacher]    Script Date: 25.02.2022 17:22:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTeacher](
	[Id] [int] IDENTITY(100,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Profession] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblTeacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTeacherCourse]    Script Date: 25.02.2022 17:22:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTeacherCourse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TeacherId] [int] NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_tblTeacherCourse] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC,
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblCourse] ON 

INSERT [dbo].[tblCourse] ([Id], [Name], [Duration], [Price], [CreationTime], [ModificationTime]) VALUES (27, N'C#', 6, 100, CAST(N'2022-02-08T23:23:00.997' AS DateTime), NULL)
INSERT [dbo].[tblCourse] ([Id], [Name], [Duration], [Price], [CreationTime], [ModificationTime]) VALUES (28, N'Mathematics', 5, 50, CAST(N'2022-02-08T23:23:42.460' AS DateTime), NULL)
INSERT [dbo].[tblCourse] ([Id], [Name], [Duration], [Price], [CreationTime], [ModificationTime]) VALUES (30, N'Logic', 5, 40, CAST(N'2022-02-09T19:15:09.710' AS DateTime), NULL)
INSERT [dbo].[tblCourse] ([Id], [Name], [Duration], [Price], [CreationTime], [ModificationTime]) VALUES (31, N'English', 6, 75, CAST(N'2022-02-09T19:15:28.090' AS DateTime), NULL)
INSERT [dbo].[tblCourse] ([Id], [Name], [Duration], [Price], [CreationTime], [ModificationTime]) VALUES (32, N'Front - end Programming', 6, 200, CAST(N'2022-02-09T19:15:49.387' AS DateTime), CAST(N'2022-02-09T19:30:37.117' AS DateTime))
INSERT [dbo].[tblCourse] ([Id], [Name], [Duration], [Price], [CreationTime], [ModificationTime]) VALUES (33, N'Full stack Programming', 10, 300, CAST(N'2022-02-09T19:16:24.170' AS DateTime), CAST(N'2022-02-09T19:30:24.787' AS DateTime))
INSERT [dbo].[tblCourse] ([Id], [Name], [Duration], [Price], [CreationTime], [ModificationTime]) VALUES (35, N'Back - end Programming', 6, 250, CAST(N'2022-02-09T19:17:18.130' AS DateTime), CAST(N'2022-02-09T19:21:20.643' AS DateTime))
INSERT [dbo].[tblCourse] ([Id], [Name], [Duration], [Price], [CreationTime], [ModificationTime]) VALUES (38, N'Physics', 5, 70, CAST(N'2022-02-09T19:25:16.370' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tblCourse] OFF
GO
SET IDENTITY_INSERT [dbo].[tblOngoingCourses] ON 

INSERT [dbo].[tblOngoingCourses] ([Id], [Name], [CourseId], [TeacherId], [PlannedEndDate], [PlannedStartDate], [StartDate], [EndDate]) VALUES (4, N'C# group - 1', 27, 105, CAST(N'2022-09-01' AS Date), CAST(N'2022-03-01' AS Date), CAST(N'2022-02-25' AS Date), CAST(N'2022-08-25' AS Date))
SET IDENTITY_INSERT [dbo].[tblOngoingCourses] OFF
GO
SET IDENTITY_INSERT [dbo].[tblOngoingCourseStudents] ON 

INSERT [dbo].[tblOngoingCourseStudents] ([Id], [OngoingCourseId], [LessonName], [StudentId], [IsInLesson], [LessonDate], [Note]) VALUES (7, 4, N'Intro to C#', 1000, N'In Lesson', CAST(N'2022-02-25' AS Date), N'good')
SET IDENTITY_INSERT [dbo].[tblOngoingCourseStudents] OFF
GO
SET IDENTITY_INSERT [dbo].[tblStudent] ON 

INSERT [dbo].[tblStudent] ([Id], [Name], [Surname], [DateOfBirth], [CreationTime], [ModificationTime]) VALUES (1000, N'Ilkin', N'Shahaliyev', CAST(N'2002-09-15' AS Date), CAST(N'2022-02-08T13:19:09.433' AS DateTime), CAST(N'2022-02-08T13:52:35.877' AS DateTime))
INSERT [dbo].[tblStudent] ([Id], [Name], [Surname], [DateOfBirth], [CreationTime], [ModificationTime]) VALUES (1004, N'Shahali', N'Shahaliyev', CAST(N'2004-10-20' AS Date), CAST(N'2022-02-08T23:24:15.803' AS DateTime), NULL)
INSERT [dbo].[tblStudent] ([Id], [Name], [Surname], [DateOfBirth], [CreationTime], [ModificationTime]) VALUES (1005, N'Samir', N'Qurbanov', CAST(N'2001-07-11' AS Date), CAST(N'2022-02-09T19:21:46.403' AS DateTime), NULL)
INSERT [dbo].[tblStudent] ([Id], [Name], [Surname], [DateOfBirth], [CreationTime], [ModificationTime]) VALUES (1006, N'Tural', N'Mammadli', CAST(N'2002-08-06' AS Date), CAST(N'2022-02-09T19:28:10.300' AS DateTime), NULL)
INSERT [dbo].[tblStudent] ([Id], [Name], [Surname], [DateOfBirth], [CreationTime], [ModificationTime]) VALUES (1007, N'Cabir', N'Ahmadov', CAST(N'2003-03-12' AS Date), CAST(N'2022-02-09T19:28:26.667' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[tblStudent] OFF
GO
SET IDENTITY_INSERT [dbo].[tblTeacher] ON 

INSERT [dbo].[tblTeacher] ([Id], [Name], [Surname], [DateOfBirth], [Profession]) VALUES (105, N'Nurlan', N'Valizada', CAST(N'1993-06-06' AS Date), N'C#')
INSERT [dbo].[tblTeacher] ([Id], [Name], [Surname], [DateOfBirth], [Profession]) VALUES (106, N'Fatima', N'Cabbarova', CAST(N'1996-03-22' AS Date), N'Mathematics')
INSERT [dbo].[tblTeacher] ([Id], [Name], [Surname], [DateOfBirth], [Profession]) VALUES (107, N'Samira', N'Aliyeva', CAST(N'1991-03-22' AS Date), N'English')
SET IDENTITY_INSERT [dbo].[tblTeacher] OFF
GO
SET IDENTITY_INSERT [dbo].[tblTeacherCourse] ON 

INSERT [dbo].[tblTeacherCourse] ([Id], [TeacherId], [CourseId]) VALUES (2, 105, 27)
INSERT [dbo].[tblTeacherCourse] ([Id], [TeacherId], [CourseId]) VALUES (6, 106, 28)
INSERT [dbo].[tblTeacherCourse] ([Id], [TeacherId], [CourseId]) VALUES (10, 107, 31)
SET IDENTITY_INSERT [dbo].[tblTeacherCourse] OFF
GO
ALTER TABLE [dbo].[tblOngoingCourses]  WITH CHECK ADD  CONSTRAINT [FK_tblOngoingCourses_CourseId_tblCourse_Id] FOREIGN KEY([CourseId])
REFERENCES [dbo].[tblCourse] ([Id])
GO
ALTER TABLE [dbo].[tblOngoingCourses] CHECK CONSTRAINT [FK_tblOngoingCourses_CourseId_tblCourse_Id]
GO
ALTER TABLE [dbo].[tblOngoingCourses]  WITH CHECK ADD  CONSTRAINT [FK_tblOngoingCourses_TeacherId_tblTeacher_Id] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[tblTeacher] ([Id])
GO
ALTER TABLE [dbo].[tblOngoingCourses] CHECK CONSTRAINT [FK_tblOngoingCourses_TeacherId_tblTeacher_Id]
GO
ALTER TABLE [dbo].[tblOngoingCourseStudents]  WITH CHECK ADD  CONSTRAINT [FK_tblOngoingCourseStudents_OngoingCourseId_tblOngoingCourses_Id] FOREIGN KEY([OngoingCourseId])
REFERENCES [dbo].[tblOngoingCourses] ([Id])
GO
ALTER TABLE [dbo].[tblOngoingCourseStudents] CHECK CONSTRAINT [FK_tblOngoingCourseStudents_OngoingCourseId_tblOngoingCourses_Id]
GO
ALTER TABLE [dbo].[tblOngoingCourseStudents]  WITH CHECK ADD  CONSTRAINT [FK_tblOngoingCourseStudents_StudentId_tblStudent_Id] FOREIGN KEY([StudentId])
REFERENCES [dbo].[tblStudent] ([Id])
GO
ALTER TABLE [dbo].[tblOngoingCourseStudents] CHECK CONSTRAINT [FK_tblOngoingCourseStudents_StudentId_tblStudent_Id]
GO
ALTER TABLE [dbo].[tblTeacherCourse]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherCourse_CourseId_tblCourse_Id] FOREIGN KEY([CourseId])
REFERENCES [dbo].[tblCourse] ([Id])
GO
ALTER TABLE [dbo].[tblTeacherCourse] CHECK CONSTRAINT [FK_tblTeacherCourse_CourseId_tblCourse_Id]
GO
ALTER TABLE [dbo].[tblTeacherCourse]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherCourse_TeacherId_tblTeacher_Id] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[tblTeacher] ([Id])
GO
ALTER TABLE [dbo].[tblTeacherCourse] CHECK CONSTRAINT [FK_tblTeacherCourse_TeacherId_tblTeacher_Id]
GO
USE [master]
GO
ALTER DATABASE [DBCourseManagementPortal] SET  READ_WRITE 
GO
