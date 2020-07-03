

CREATE DATABASE KIEMTRA_59132942
GO

USE KIEMTRA_59132942
GO

CREATE TABLE Tinh
(
	MaTinh VARCHAR(100) PRIMARY KEY,
	TenTinh NVARCHAR(100) NOT NULL
)
GO

CREATE TABLE ThanhVien
(
	MaTV VARCHAR(100) PRIMARY KEY,
	HoTV NVARCHAR(100) NOT NULL,
	TenTV NVARCHAR(100) NOT NULL,
	NgaySinh DATE NOT NULL,
	GioiTinh BIT DEFAULT(1) NOT NULL,
	Email NVARCHAR(100) NOT NULL,
	DiaChi NVARCHAR(100) NOT NULL,
	MaTinh VARCHAR(100) FOREIGN KEY REFERENCES dbo.Tinh(MaTinh)
    ON UPDATE CASCADE
	ON DELETE CASCADE
)
GO


INSERT INTO dbo.Tinh
        ( MaTinh, TenTinh )
VALUES  ( 'PY', N'Phú Yên'),
		('KH', N'Khánh Hòa')
GO

INSERT INTO dbo.ThanhVien
        ( MaTV , HoTV , TenTV , NgaySinh , GioiTinh , Email ,DiaChi , MaTinh )
VALUES  ( 'TV1' , N'Nguyễn' , N'Thái Tuấn' , '1999-09-01' ,1 , N'tuan.nt.59cntt@ntu.edu.vn' ,N'02 NDC' ,'PY'),
		( 'TV2' , N'Nguyễn' , N'Trúc Đào' , '1999-09-08' ,0 , N'quyen.nh.60qtdl@ntu.edu.vn' ,N'02 NDC' ,'KH')
GO
SELECT * FROM dbo.ThanhVien

DROP PROCEDURE dbo.ThanhVien_TimKiem
GO

CREATE PROCEDURE ThanhVien_TimKiem -- tạo tên thủ tục
    @MaTV NVARCHAR(100),
	@HoTen NVARCHAR(100) ,
	@NgaySinh NVARCHAR(10),
	@GioiTinh NVARCHAR(10) ,
	@Email NVARCHAR(100) ,
	@DiaChi NVARCHAR(100),
	@MaTinh NVARCHAR(100)  -- tạo tên biến tạm 
AS						   -- phải có cái này, là cái nối giữa biến tạm và cái hành động thực thi bên dưới
BEGIN					   -- 1 lệnh thì không cần begin - end, 
	SELECT * FROM dbo.ThanhVien WHERE -- từ bản thành viên, chọn ->>>>>>>>>>>>>>.
	MaTV LIKE '%' + @MaTV + '%' AND   -- @MaTV là tên biến mình khởi tạo ở trên, MaTV là thuộc tính trong bảng ThanhVien
	HoTV + '' +TenTV LIKE '%' + @HoTen + '%' AND -- nối chuỗi..... HoTV + TenTv -> @HoTen, ở dưới tương tự
	NgaySinh LIKE  '%' + @NgaySinh + '%' AND
	GioiTinh LIKE '%' + @GioiTinh + '%' AND
	Email LIKE  '%' + @Email + '%' AND
	DiaChi LIKE  '%' + @DiaChi + '%'AND
	MaTinh LIKE '%' + @MaTinh+ '%'
END
GO
EXEC ThanhVien_TimKiem @MaTV = '',@HoTen = N'Thái', @NgaySinh = '', @GioiTinh = ,@Email = N'',@DiaChi = N'',@MaTinh = '' 
-- exec là để xuất ra kết quả của thủ tục trên, EXEC đúng cái tên thủ tục cần làm
  -- cách này tìm kiếm theo tên có dấu được, ko phân biệt chữ hoa, hay thường. rồi đọc đi



