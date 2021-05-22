CREATE PROCEDURE [dbo].[spOrders_UpdateName]
	@Id int,
	@OrderName nvarchar(50)
AS
BEGIN
	set nocount on;
	update dbo.[Order] 
	set OrderName=@OrderName
	where Id=@Id

END
	
