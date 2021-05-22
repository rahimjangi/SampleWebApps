CREATE PROCEDURE [dbo].[spOrders_Delete]
	@Id int

As
BEGIN
	set nocount on;
	delete dbo.[Food]
	where id=@id
END
