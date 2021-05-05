CREATE OR ALTER PROCEDURE dbo.Login 
    @userName nchar(30),
    @passWord nchar(32) 
AS
    SELECT FullName FROM TableUsers WHERE UserName = @userName and Password = @passWord
go
