/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

if not exists (select * from dbo.Food)

begin
    insert into dbo.Food(Title,[Description],Price) 
    values('Food One','This is for test one',7.98),
    ('Food Two','This is for test two',7.98),
    ('Food Three','This is for test three',7.98),
    ('Food Four','This is for test four',7.98)
end
