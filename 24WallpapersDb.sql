USE [master]
GO
/****** Object:  Database [EnticingWallpaperDb]    Script Date: 13-Oct-20 5:51:29 PM ******/
CREATE DATABASE [EnticingWallpaperDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EnticingWallpaperDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EnticingWallpaperDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EnticingWallpaperDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EnticingWallpaperDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [EnticingWallpaperDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EnticingWallpaperDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EnticingWallpaperDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EnticingWallpaperDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EnticingWallpaperDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [EnticingWallpaperDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EnticingWallpaperDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET RECOVERY FULL 
GO
ALTER DATABASE [EnticingWallpaperDb] SET  MULTI_USER 
GO
ALTER DATABASE [EnticingWallpaperDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EnticingWallpaperDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EnticingWallpaperDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EnticingWallpaperDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EnticingWallpaperDb] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EnticingWallpaperDb', N'ON'
GO
ALTER DATABASE [EnticingWallpaperDb] SET QUERY_STORE = OFF
GO
USE [EnticingWallpaperDb]
GO
/****** Object:  Table [dbo].[AdminLogin]    Script Date: 13-Oct-20 5:51:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminLogin](
	[AdminLoginId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailId] [nvarchar](max) NULL,
	[AdminPwd] [nvarchar](max) NULL,
	[IsActive] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminLoginId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 13-Oct-20 5:51:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](500) NULL,
	[CategoryDescription] [nvarchar](max) NULL,
	[CategoryImagePath] [nvarchar](max) NULL,
	[CategoryTypeId] [bigint] NULL,
	[CategoryTypeName] [nvarchar](max) NULL,
	[SEOCategoryName] [nvarchar](max) NULL,
	[SEOCategoryTypeName] [nvarchar](max) NULL,
	[MetaCatTitle] [nvarchar](max) NULL,
	[MetaCatKeywords] [nvarchar](max) NULL,
	[MetaCatDescription] [nvarchar](max) NULL,
	[Review] [bigint] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK__Category__19093A0B03317E3D] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoryType]    Script Date: 13-Oct-20 5:51:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryType](
	[CategoryTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[CategoryTypeName] [nvarchar](max) NULL,
	[SEOCategoryTypeName] [nvarchar](max) NULL,
	[CategoryTypeBannerImage] [nvarchar](max) NULL,
	[MetaCTTitle] [nvarchar](max) NULL,
	[MetaCTKeywords] [nvarchar](max) NULL,
	[MetaCTDescriptions] [nvarchar](max) NULL,
	[CategoryTypeContent] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK__Category__7B30E7A31273C1CD] PRIMARY KEY CLUSTERED 
(
	[CategoryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactEnquiry]    Script Date: 13-Oct-20 5:51:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactEnquiry](
	[ContactEnquiryId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[MobileNumber] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[EnquiryMessage] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ContactEnquiryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WallFaq]    Script Date: 13-Oct-20 5:51:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WallFaq](
	[FaqId] [bigint] IDENTITY(1,1) NOT NULL,
	[FaqTitle] [nvarchar](max) NULL,
	[FaqDesc] [nvarchar](max) NULL,
	[AutoGFaqId] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK__Faq__9C741C433C69FB99] PRIMARY KEY CLUSTERED 
(
	[FaqId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wallpaper]    Script Date: 13-Oct-20 5:51:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wallpaper](
	[WallpaperId] [bigint] IDENTITY(1,1) NOT NULL,
	[MainCategoryId] [bigint] NULL,
	[MainCategoryName] [nvarchar](max) NULL,
	[CatId] [bigint] NULL,
	[CatName] [nvarchar](500) NULL,
	[WallpaperName] [nvarchar](max) NULL,
	[SEOMainCategoryName] [nvarchar](max) NULL,
	[SEOCatName] [nvarchar](max) NULL,
	[SEOWallpaperName] [nvarchar](max) NULL,
	[WallpaperImagePath] [nvarchar](max) NULL,
	[WallpaperDetail] [nvarchar](max) NULL,
	[MetaWallTitle] [nvarchar](max) NULL,
	[MetaWallKeywords] [nvarchar](max) NULL,
	[MetaWallDescriptions] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK__Wallpape__B3196F9407020F21] PRIMARY KEY CLUSTERED 
(
	[WallpaperId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WallpaperHomePageContent]    Script Date: 13-Oct-20 5:51:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WallpaperHomePageContent](
	[WHPContentId] [bigint] IDENTITY(1,1) NOT NULL,
	[TrendingThisWeek] [nvarchar](max) NULL,
	[TrendingThisWeekContent] [nvarchar](max) NULL,
	[FeaturedCategory] [nvarchar](max) NULL,
	[FeaturedCategoryContent] [nvarchar](max) NULL,
	[TrendingWallpaperAlbums] [nvarchar](max) NULL,
	[TrendingWallpaperAlbumsContent] [nvarchar](max) NULL,
	[MostViewWallpaperAlbums] [nvarchar](max) NULL,
	[MostViewWallpaperAlbumsContent] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK__Wallpape__50F7C67E34C8D9D1] PRIMARY KEY CLUSTERED 
(
	[WHPContentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContactEnquiry] ADD  DEFAULT (getdate()) FOR [CreatedOn]
GO
/****** Object:  StoredProcedure [dbo].[sp_crud_category]    Script Date: 13-Oct-20 5:51:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[sp_crud_category](
@MainCategoryId bigint=null,
@CategoryTypeId  bigint=null,
@CategoryTypeName nvarchar(max)=null,
@SEOCategoryTypeName nvarchar(max)=null,

@CategoryId bigint=null,
@CategoryName nvarchar(max)=null,
@SEOCategoryName nvarchar(max)=null,
@action nvarchar(500)=null
)
as 
begin
if @action='getwallpaperalbums'
begin
select top(8) COUNT(wal.CatId) as TotalSubCategory,wal.CatName,cc.CategoryImagePath,wal.MainCategoryId,wal.MainCategoryName,wal.SEOMainCategoryName,cc.SEOCategoryName,cc.Review from Category as cc
inner join Wallpaper as wal on cc.CategoryId=wal.CatId 
group by wal.CatId,wal.CatName,cc.CategoryImagePath,wal.MainCategoryId,wal.MainCategoryName,wal.SEOMainCategoryName,cc.SEOCategoryName,cc.Review
end
if @action ='getmostviewwallpaperalbums'
begin
select top(8) COUNT(wal.CatId) as TotalSubCategory,wal.CatName,cc.CategoryImagePath,wal.MainCategoryId,wal.MainCategoryName,wal.SEOMainCategoryName,cc.SEOCategoryName,cc.Review from Category as cc
inner join Wallpaper as wal on cc.CategoryId=wal.CatId 
group by wal.CatId,wal.CatName,cc.CategoryImagePath,wal.MainCategoryId,wal.MainCategoryName,wal.SEOMainCategoryName,cc.SEOCategoryName,cc.Review order by cc.Review desc
end
if @action='getsubcategorygrouplikeglobal'
begin
select COUNT(wal.CatId) as TotalSubCategory,wal.CatName,cc.CategoryImagePath,wal.MainCategoryId,wal.MainCategoryName,wal.SEOMainCategoryName,cc.SEOCategoryName,cc.Review from Category as cc
inner join Wallpaper as wal on cc.CategoryId=wal.CatId where wal.SEOMainCategoryName like '%'+@SEOCategoryTypeName+'%' or wal.SEOCatName like '%'+@SEOCategoryTypeName+'%'
group by wal.CatId,wal.CatName,cc.CategoryImagePath,wal.MainCategoryId,wal.MainCategoryName,wal.SEOMainCategoryName,cc.SEOCategoryName,cc.Review
end

if @action='getsubcategorygroupbyid'
begin
select COUNT(wal.CatId) as TotalSubCategory,wal.CatName,cc.CategoryImagePath,wal.MainCategoryId,wal.MainCategoryName,wal.SEOMainCategoryName,cc.SEOCategoryName,cc.Review from Category as cc
inner join Wallpaper as wal on cc.CategoryId=wal.CatId where wal.MainCategoryId=@MainCategoryId
group by wal.CatId,wal.CatName,cc.CategoryImagePath,wal.MainCategoryId,wal.MainCategoryName,wal.SEOMainCategoryName,cc.SEOCategoryName,cc.Review
end


if @action='updatecategorytypebyId'
begin 
update Category set CategoryTypeName=@CategoryTypeName,SEOCategoryTypeName=@SEOCategoryTypeName where CategoryTypeId=@CategoryTypeId
update Wallpaper set MainCategoryName=@CategoryTypeName,SEOMainCategoryName=@SEOCategoryTypeName where MainCategoryId=@CategoryTypeId
end
if @action='updatecategorybyid'
begin
update Wallpaper set CatName=@CategoryName,SEOCatName=@SEOCategoryName where CatId=@CategoryId
end

if @action='deletecategorytypebyid'
begin
Delete from Category Where CategoryTypeId=@CategoryTypeId
Delete from Wallpaper Where MainCategoryId=@CategoryTypeId 
end

if @action='deletecategorybyid'
begin
Delete from Wallpaper where CatId=@CategoryId
end

if @action='updatereviewbyseocategoryname'
begin
update Category set Review=Review+1 where SEOCategoryName=@SEOCategoryName
end
end
GO
USE [master]
GO
ALTER DATABASE [EnticingWallpaperDb] SET  READ_WRITE 
GO
