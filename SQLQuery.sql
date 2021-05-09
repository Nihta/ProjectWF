CREATE OR ALTER PROCEDURE dbo.Login 
    @UserName nchar(30),
    @PassWord nchar(32) 
AS
    SELECT FullName FROM TableUsers WHERE UserName = @UserName and Password = @PassWord
go


select *
from TableUsers