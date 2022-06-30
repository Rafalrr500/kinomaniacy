DROP PROCEDURE ClientCreate, StaffCreate, AdminCreate, ClientGetPassword, StaffGetPassword, AdminGetPassword, ClientExists, StaffExists, AdminExists
GO
CREATE PROCEDURE [dbo].[ClientCreate]
@pfirstName NVARCHAR(50),
@plastName NVARCHAR(50),
@puserName NVARCHAR(32),
@ppassword CHAR(64)
AS
  INSERT INTO Client (firstName, lastName, userName, password)
  VALUES (@pfirstName, @plastName,@puserName, @ppassword);

GO
CREATE PROCEDURE [dbo].[StaffCreate]
@pjobPosition NVARCHAR(50),
@puserName NVARCHAR(32),
@ppassword CHAR(64)
AS
  INSERT INTO Staff (jobPosition, userName, password)
  VALUES (@pjobPosition, @puserName, @ppassword);

GO
CREATE PROCEDURE [dbo].[AdminCreate]
@puserName NVARCHAR(32),
@ppassword CHAR(64)
AS
  INSERT INTO Admin (userName, password)
  VALUES (@puserName, @ppassword);


GO
CREATE PROCEDURE [dbo].[ClientGetPassword]
( @pUserName NVARCHAR(32) )
AS
  SELECT password
  FROM [Client]
  WHERE userName = @pUserName;

GO
CREATE PROCEDURE [dbo].[StaffGetPassword]
( @pUserName NVARCHAR(32) )
AS
  SELECT password
  FROM [Staff]
  WHERE userName = @pUserName;

GO
CREATE PROCEDURE [dbo].[AdminGetPassword]
( @pUserName NVARCHAR(32) )
AS
  SELECT password
  FROM [Admin]
  WHERE userName = @pUserName;

GO
CREATE PROCEDURE [dbo].[ClientExists]
( @pUserName NVARCHAR(32) )
AS
  SELECT 1
  FROM [Client]
  WHERE userName = @pUserName;

GO
CREATE PROCEDURE [dbo].[StaffExists]
( @pUserName NVARCHAR(32) )
AS
  SELECT 1
  FROM [Staff]
  WHERE userName = @pUserName;

GO
CREATE PROCEDURE [dbo].[AdminExists]
( @pUserName NVARCHAR(32) )
AS
  SELECT 1
  FROM [Admin]
  WHERE userName = @pUserName;