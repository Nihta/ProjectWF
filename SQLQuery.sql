USE master
GO

USE dbProject
GO

-- Đăng nhập
CREATE OR ALTER PROCEDURE dbo.Login
    @UserName nchar(30),
    @PassWord nchar(32)
AS
SELECT UserID
FROM TableUsers
WHERE UserName = @UserName and Password = @PassWord
go

-- Mật khẩu là 123 (md5)
Exec dbo.Login "nihta", "202cb962ac59075b964b07152d234b70"
Go

-- Cập nhật thông tin tài khoản
CREATE OR ALTER PROCEDURE dbo.UpdateUser
    @UserID int,
    @FullName nchar(30),
    @PassWord nchar(32)
AS
UPDATE dbo.TableUsers
    SET
        FullName = @FullName,
        [PassWord] = @PassWord
    WHERE UserID = @UserID
go

EXEC dbo.UpdateUser @UserID = 1, @FullName = "Thìn 2", @PassWord = "202cb962ac59075b964b07152d234b70"
go

Select UserID, UserName, [PassWord], FullName
from TableUsers
go


SELECT * FROM TableUsers WHERE UserName = 'Nihta' AND UserID != 1