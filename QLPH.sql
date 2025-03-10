USE [master]
GO
/****** Object:  Database [QuanLyPhongHoc]    Script Date: 07/01/2024 2:27:58 CH ******/
CREATE DATABASE [QuanLyPhongHoc]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QuanLyPhongHoc', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QuanLyPhongHoc.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'QuanLyPhongHoc_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\QuanLyPhongHoc_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [QuanLyPhongHoc] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QuanLyPhongHoc].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QuanLyPhongHoc] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET ARITHABORT OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QuanLyPhongHoc] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QuanLyPhongHoc] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QuanLyPhongHoc] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QuanLyPhongHoc] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET RECOVERY FULL 
GO
ALTER DATABASE [QuanLyPhongHoc] SET  MULTI_USER 
GO
ALTER DATABASE [QuanLyPhongHoc] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QuanLyPhongHoc] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QuanLyPhongHoc] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QuanLyPhongHoc] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [QuanLyPhongHoc] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [QuanLyPhongHoc] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'QuanLyPhongHoc', N'ON'
GO
ALTER DATABASE [QuanLyPhongHoc] SET QUERY_STORE = ON
GO
ALTER DATABASE [QuanLyPhongHoc] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [QuanLyPhongHoc]
GO
/****** Object:  Table [dbo].[CapPhongHoc]    Script Date: 07/01/2024 2:27:58 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CapPhongHoc](
	[MaPhong] [nvarchar](30) NOT NULL,
	[MaGiangVien] [nvarchar](30) NOT NULL,
	[TietHoc] [nvarchar](10) NOT NULL,
	[Ngay] [date] NOT NULL,
	[GhiChu] [nvarchar](100) NULL,
	[MaThietBi] [nvarchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaPhong] ASC,
	[MaGiangVien] ASC,
	[TietHoc] ASC,
	[Ngay] ASC,
	[MaThietBi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiangVien]    Script Date: 07/01/2024 2:27:58 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiangVien](
	[MaGiangVien] [nvarchar](30) NOT NULL,
	[TenGV] [nvarchar](50) NULL,
	[Khoa] [nvarchar](100) NULL,
	[NgaySinh] [datetime] NULL,
	[SoDienThoai] [nchar](50) NULL,
	[GhiChu] [nvarchar](100) NULL,
 CONSTRAINT [PK_GiangVien] PRIMARY KEY CLUSTERED 
(
	[MaGiangVien] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhongHoc]    Script Date: 07/01/2024 2:27:58 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhongHoc](
	[MaPhong] [nvarchar](30) NOT NULL,
	[TenPhong] [nvarchar](50) NULL,
	[Tang] [nvarchar](30) NULL,
	[SucChua] [nvarchar](30) NULL,
	[GhiChu] [nvarchar](100) NULL,
 CONSTRAINT [PK_PhongHoc] PRIMARY KEY CLUSTERED 
(
	[MaPhong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 07/01/2024 2:27:58 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[TenDangNhap] [nvarchar](30) NOT NULL,
	[MatKhau] [nvarchar](50) NULL,
	[HoVaTen] [nvarchar](50) NULL,
	[QuyenHan] [nvarchar](10) NULL,
	[GhiChu] [nvarchar](100) NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[TenDangNhap] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ThietBi]    Script Date: 07/01/2024 2:27:58 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ThietBi](
	[MaThietBi] [nvarchar](30) NOT NULL,
	[TenThietBi] [nvarchar](50) NULL,
	[TinhTrang] [nvarchar](30) NULL,
	[TrangThai] [nvarchar](30) NULL,
	[NgayMuon] [date] NULL,
	[NgayTra] [date] NULL,
 CONSTRAINT [PK_ThietBi] PRIMARY KEY CLUSTERED 
(
	[MaThietBi] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TKB_Phong]    Script Date: 07/01/2024 2:27:58 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TKB_Phong](
	[TietHoc] [nvarchar](10) NOT NULL,
	[ThoiGian] [nvarchar](30) NULL,
 CONSTRAINT [PK_TKB_Phong] PRIMARY KEY CLUSTERED 
(
	[TietHoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[CapPhongHoc] ([MaPhong], [MaGiangVien], [TietHoc], [Ngay], [GhiChu], [MaThietBi]) VALUES (N'PH001', N'GV001', N'Tiết 10', CAST(N'2024-01-07' AS Date), N'Nhớ Tắt Đèn', N'TB002')
INSERT [dbo].[CapPhongHoc] ([MaPhong], [MaGiangVien], [TietHoc], [Ngay], [GhiChu], [MaThietBi]) VALUES (N'PH002', N'GV002', N'Tiết 4', CAST(N'2024-01-07' AS Date), N'Nhớ Đóng Cửa', N'TB005')
INSERT [dbo].[CapPhongHoc] ([MaPhong], [MaGiangVien], [TietHoc], [Ngay], [GhiChu], [MaThietBi]) VALUES (N'PH003', N'GV004', N'Tiết 8', CAST(N'2024-01-07' AS Date), N'Tắt Quạt', N'TB006')
INSERT [dbo].[CapPhongHoc] ([MaPhong], [MaGiangVien], [TietHoc], [Ngay], [GhiChu], [MaThietBi]) VALUES (N'PH004', N'GV001', N'Tiết 3', CAST(N'2024-01-07' AS Date), N'Trật Tự', N'TB003')
GO
INSERT [dbo].[GiangVien] ([MaGiangVien], [TenGV], [Khoa], [NgaySinh], [SoDienThoai], [GhiChu]) VALUES (N'GV001', N'Nguyễn Văn Tình', N'CNTT', CAST(N'1990-01-01T00:00:00.000' AS DateTime), N'091XXXXXX                                         ', N'Trưởng Khoa')
INSERT [dbo].[GiangVien] ([MaGiangVien], [TenGV], [Khoa], [NgaySinh], [SoDienThoai], [GhiChu]) VALUES (N'GV002', N'Nguyễn Văn Chiến', N'CNPM', CAST(N'1990-01-01T00:00:00.000' AS DateTime), N'0983XXXXXX                                        ', N'Trợ Giảng')
INSERT [dbo].[GiangVien] ([MaGiangVien], [TenGV], [Khoa], [NgaySinh], [SoDienThoai], [GhiChu]) VALUES (N'GV003', N'Long Tất Thành', N'CNTP', CAST(N'1991-01-01T00:00:00.000' AS DateTime), N'02963XXXXX                                        ', N'Phó Khoa')
INSERT [dbo].[GiangVien] ([MaGiangVien], [TenGV], [Khoa], [NgaySinh], [SoDienThoai], [GhiChu]) VALUES (N'GV004', N'Tiên Hỷ Kỳ', N'CNTT', CAST(N'1997-06-01T00:00:00.000' AS DateTime), N'091XXXXXXX                                        ', N'Giáo viên mới')
INSERT [dbo].[GiangVien] ([MaGiangVien], [TenGV], [Khoa], [NgaySinh], [SoDienThoai], [GhiChu]) VALUES (N'GV005', N'Quốc Tuấn Du', N'Luật', CAST(N'1999-06-16T00:00:00.000' AS DateTime), N'097623523523                                      ', N'Giảng Viên Mới')
GO
INSERT [dbo].[PhongHoc] ([MaPhong], [TenPhong], [Tang], [SucChua], [GhiChu]) VALUES (N'PH001', N'A101', N'1', N'30', NULL)
INSERT [dbo].[PhongHoc] ([MaPhong], [TenPhong], [Tang], [SucChua], [GhiChu]) VALUES (N'PH002', N'A204', N'2', N'15', NULL)
INSERT [dbo].[PhongHoc] ([MaPhong], [TenPhong], [Tang], [SucChua], [GhiChu]) VALUES (N'PH003', N'A302', N'3', N'24', NULL)
INSERT [dbo].[PhongHoc] ([MaPhong], [TenPhong], [Tang], [SucChua], [GhiChu]) VALUES (N'PH004', N'A401', N'4', N'28', NULL)
INSERT [dbo].[PhongHoc] ([MaPhong], [TenPhong], [Tang], [SucChua], [GhiChu]) VALUES (N'PH005', N'B207', N'2', N'32', N'')
GO
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [HoVaTen], [QuyenHan], [GhiChu]) VALUES (N'Admin', N'123', N'Trường Vũ', N'admin', N'Quản Trị Viên')
INSERT [dbo].[TaiKhoan] ([TenDangNhap], [MatKhau], [HoVaTen], [QuyenHan], [GhiChu]) VALUES (N'sa1', N'123', N'Sil Phạm', N'user', N'Nhân Viên')
GO
INSERT [dbo].[ThietBi] ([MaThietBi], [TenThietBi], [TinhTrang], [TrangThai], [NgayMuon], [NgayTra]) VALUES (N'TB001', N'Quạt trần', N'Hoạt động bình thường', N'Đang hoạt động', CAST(N'2023-11-25' AS Date), CAST(N'2023-12-31' AS Date))
INSERT [dbo].[ThietBi] ([MaThietBi], [TenThietBi], [TinhTrang], [TrangThai], [NgayMuon], [NgayTra]) VALUES (N'TB002', N'Micro', N'Hoạt động bình thường', N'Đang sạc pin', CAST(N'2023-12-30' AS Date), CAST(N'2024-01-21' AS Date))
INSERT [dbo].[ThietBi] ([MaThietBi], [TenThietBi], [TinhTrang], [TrangThai], [NgayMuon], [NgayTra]) VALUES (N'TB003', N'Loa Mini để bàn', N'Đang sửa chữa', N'Đang bảo dưỡng', CAST(N'2024-01-05' AS Date), CAST(N'2024-01-05' AS Date))
INSERT [dbo].[ThietBi] ([MaThietBi], [TenThietBi], [TinhTrang], [TrangThai], [NgayMuon], [NgayTra]) VALUES (N'TB004', N'Máy tính', N'Hoạt động tốt', N'Đang kết nối', CAST(N'2024-01-01' AS Date), CAST(N'2024-03-29' AS Date))
INSERT [dbo].[ThietBi] ([MaThietBi], [TenThietBi], [TinhTrang], [TrangThai], [NgayMuon], [NgayTra]) VALUES (N'TB005', N'Chuột', N'Bảo trì', N'Hỏng hóc nhẹ', CAST(N'2024-01-05' AS Date), CAST(N'2024-01-05' AS Date))
INSERT [dbo].[ThietBi] ([MaThietBi], [TenThietBi], [TinhTrang], [TrangThai], [NgayMuon], [NgayTra]) VALUES (N'TB006', N'Dây cáp', N'Hoạt động tốt', N'Đang được sử dụng', CAST(N'2024-01-07' AS Date), CAST(N'2024-05-16' AS Date))
GO
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 1', N'(7h-7h45)')
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 10', N'(16h45-17h30)')
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 2', N'(7h45-8h30)')
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 3', N'(8h45-9h30)')
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 4', N'(9h45-10h30)')
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 5', N'(10h45-11h30)')
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 6', N'(13h-13h45)')
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 7', N'(13h45-14h30)')
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 8', N'(14h45-15h30)')
INSERT [dbo].[TKB_Phong] ([TietHoc], [ThoiGian]) VALUES (N'Tiết 9', N'(15h45-16h30)')
GO
ALTER TABLE [dbo].[CapPhongHoc]  WITH CHECK ADD  CONSTRAINT [FK_CapPhongHoc_GiangVien] FOREIGN KEY([MaGiangVien])
REFERENCES [dbo].[GiangVien] ([MaGiangVien])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CapPhongHoc] CHECK CONSTRAINT [FK_CapPhongHoc_GiangVien]
GO
ALTER TABLE [dbo].[CapPhongHoc]  WITH CHECK ADD  CONSTRAINT [FK_CapPhongHoc_PhongHoc] FOREIGN KEY([MaPhong])
REFERENCES [dbo].[PhongHoc] ([MaPhong])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CapPhongHoc] CHECK CONSTRAINT [FK_CapPhongHoc_PhongHoc]
GO
ALTER TABLE [dbo].[CapPhongHoc]  WITH CHECK ADD  CONSTRAINT [FK_CapPhongHoc_ThietBi] FOREIGN KEY([MaThietBi])
REFERENCES [dbo].[ThietBi] ([MaThietBi])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CapPhongHoc] CHECK CONSTRAINT [FK_CapPhongHoc_ThietBi]
GO
ALTER TABLE [dbo].[CapPhongHoc]  WITH CHECK ADD  CONSTRAINT [FK_CapPhongHoc_TKB_Phong] FOREIGN KEY([TietHoc])
REFERENCES [dbo].[TKB_Phong] ([TietHoc])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CapPhongHoc] CHECK CONSTRAINT [FK_CapPhongHoc_TKB_Phong]
GO
USE [master]
GO
ALTER DATABASE [QuanLyPhongHoc] SET  READ_WRITE 
GO
