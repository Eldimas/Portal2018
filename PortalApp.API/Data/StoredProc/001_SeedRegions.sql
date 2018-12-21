USE [Portal2018_userv]
GO

/****** Object:  StoredProcedure [dbo].[SeedRegions]    Script Date: 20.12.2018 9:58:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SeedRegions] 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

INSERT INTO [dbo].[Regions]([Id],[NameRu],[NameEn],[NameKz])
     VALUES (NEWID(),'Main Office','Main Office','Main Office')

INSERT INTO [dbo].[Regions]([Id],[NameRu],[NameEn],[NameKz])
     VALUES (NEWID(),'Almaty GTA','Almaty GTA','Almaty GTA')

INSERT INTO [dbo].[Regions]([Id],[NameRu],[NameEn],[NameKz])
     VALUES (NEWID(),'Shymkent GTA','Shymkent GTA','Shymkent GTA')

INSERT INTO [dbo].[Regions]([Id],[NameRu],[NameEn],[NameKz])
     VALUES (NEWID(),'Taraz GTA','Taraz GTA','Taraz GTA')



END

GO


