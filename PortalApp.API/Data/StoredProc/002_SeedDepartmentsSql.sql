-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SeedDepartmentsSql
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   declare @tmpDep table
	(
		[Id] int not null,
		[Key] nvarchar(max),
		[Name] nvarchar(max),
		[DisplayNameRus] nvarchar(max),
		[FromNameRus] nvarchar(max),
		[ToNameRus] nvarchar(max),
		[DisplayNameEng] nvarchar(max),
		[FromNameEng] nvarchar(max),
		[ToNameEng] nvarchar(max),
		[Priority] int not null,
		[Disabled] bit not null,
		[DisplayNameKaz] nvarchar(max),
		[FromNameKaz] nvarchar(max),
		[ToNameKaz] nvarchar(max),
		[Region] nvarchar(max)

		primary key(Id)
	)

	declare 
	@Id as int,
	@Key as nvarchar(max),
	@Name as nvarchar(max),
	@DisplayNameRus as nvarchar(max),
	@FromNameRus as nvarchar(max),
	@ToNameRus as nvarchar(max),
	@DisplayNameEng as nvarchar(max),
	@FromNameEng as nvarchar(max),
	@ToNameEng as nvarchar(max),
	@Priority as int,
	@Disabled as bit,
	@DisplayNameKaz as nvarchar(max),
	@FromNameKaz as nvarchar(max),
	@ToNameKaz as nvarchar(max),
	@Region as nvarchar(max)

	

	insert @tmpDep
		SELECT [Id]
		  ,[Key]
		  ,[Name]
		  ,[DisplayNameRus]
		  ,[FromNameRus]
		  ,[ToNameRus]
		  ,[DisplayNameEng]
		  ,[FromNameEng]
		  ,[ToNameEng]
			,[Priority]
		  ,[Disabled]
		  ,[DisplayNameKaz]
		  ,[FromNameKaz]
		  ,[ToNameKaz]
		  ,[Region]
	  FROM [Docz].[dbo].[DepartmentProfiles]

		--select * from @tmpDep

		while ((select count(*) from @tmpDep) > 0)
		BEGIN
		--------------------------------------
			select top 1 @Key = [Key], 
				@Id = Id, 
				@Name = Name,
				@DisplayNameRus = DisplayNameRus,
				@FromNameRus = FromNameRus,
				@ToNameRus = ToNameRus,
				@DisplayNameEng = DisplayNameEng,
				@FromNameEng = FromNameEng,
				@ToNameEng = ToNameEng,
				@Priority = [Priority],
				@Disabled  = [Disabled],
				@DisplayNameKaz = DisplayNameKaz,
				@FromNameKaz = FromNameKaz,
				@ToNameKaz  = ToNameKaz,
				@Region  = Region
		
			from @tmpDep
			--------------------------------------
			-- Вставляем данные в Departments
			---------------------------------------
			declare @DepartmentId uniqueidentifier
			SET @DepartmentId = null;

			SELECT @DepartmentId = Id from Departments 
				where KeyIndex = @Key
			
			if(@DepartmentId is null)
				BEGIN
					SET @DepartmentId = NEWID();
					
					INSERT INTO [dbo].[Departments]
						([Id]
						,[KeyIndex])
					VALUES
						(@DepartmentId
						,@Key)

				END
			ELSE
				BEGIN
					UPDATE [dbo].[Departments]
					   SET [KeyIndex] = @Key
						WHERE Id = @DepartmentId

				END
			

			-----------------------------------------
			-- Вставляем данные в DepartmentVs Main Office
			----------------------------------------
				-- id регионов
				--------------
			declare @RegionId uniqueidentifier
			SET @RegionId = null

			declare @RegionId_Main uniqueidentifier
			SELECT top 1 @RegionId_Main = Id from Regions
							where NameRu = 'Main Office'

			declare @RegionId_Almaty uniqueidentifier
			SELECT top 1 @RegionId_Almaty = Id from Regions
							where NameRu = 'Almaty GTA'

			declare @RegionId_Shymkent uniqueidentifier
			SELECT top 1 @RegionId_Shymkent = Id from Regions
							where NameRu = 'Shymkent GTA'

			declare @RegionId_Taraz uniqueidentifier
			SELECT top 1 @RegionId_Taraz = Id from Regions
							where NameRu = 'Taraz GTA'

			--------------------------------------------------
			IF(@Key in ('CS6', 'CS7', 'CCS-6', 'CCS-7', 'CCS-8', 'GTA Almaty'))
				SET @RegionId = @RegionId_Almaty
			ELSE IF(@Key in ('CS4', 'CCS-3', 'CCS-4', 'CCS-5', 'GTA Taraz'))
				SET @RegionId = @RegionId_Taraz
			ELSE IF(@Key in ('CS1', 'CS2', 'CCS-1', 'CCS-2', 'GTA Shymkent'))
				SET @RegionId = @RegionId_Shymkent
			ELSE SET @RegionId = @RegionId_Main

			--print @RegionId

			--------------------------------------------------
			declare @DepartmentVId uniqueidentifier
			SET @DepartmentVId = null
			--SET @DepartmentVId = NEWID()

			SELECT @DepartmentVId = Id 
				from [dbo].[DepartmentVs]
				WHERE Name = @Key

			if(@DepartmentVId is null)
				BEGIN
					SET @DepartmentVId = NEWID()

					INSERT INTO [dbo].[DepartmentVs]
					   ([Id]
					   ,[Created]
					   ,[Name]
					   ,[ShortName]
					   ,[DisplayNameRus]
					   ,[DisplayNameEng]
					   ,[DisplayNameKaz]
					   ,[FromNameRus]
					   ,[FromNameEng]
					   ,[FromNameKaz]
					   ,[ToNameRus]
					   ,[ToNameEng]
					   ,[ToNameKaz]
					   ,[Priority]
					   ,[Disabled]
					   ,[RegionId]
					   ,[DepartmentId])
				 VALUES
					   (@DepartmentVId
					   , GETDATE()
					   ,@Name
					   ,@Name
					   ,@DisplayNameRus
					   ,@DisplayNameEng
					   ,@DisplayNameKaz
					   ,@FromNameRus
					   ,@FromNameEng
					   ,@FromNameKaz
					   ,@ToNameRus
					   ,@ToNameEng
					   ,@ToNameKaz
					   ,@Priority
					   ,@Disabled
					   ,@RegionId
					   ,@DepartmentId)
				END
			ELSE 
				BEGIN
					
					UPDATE [dbo].[DepartmentVs]
					   SET [Name] = @Name
						  ,[ShortName] = @Name
						  ,[DisplayNameRus] = @DisplayNameRus
						  ,[DisplayNameEng] = @DisplayNameEng
						  ,[DisplayNameKaz] = @DisplayNameKaz
						  ,[FromNameRus] = @FromNameRus
						  ,[FromNameEng] = @FromNameEng
						  ,[FromNameKaz] = @FromNameKaz
						  ,[ToNameRus] = @ToNameRus
						  ,[ToNameEng] = @ToNameEng
						  ,[ToNameKaz] = @ToNameKaz
						  ,[Priority] = @Priority
						  ,[Disabled] = @Disabled
						  ,[RegionId] = @RegionId
						  ,[DepartmentId] = @DepartmentId
					 WHERE [Id] = @DepartmentVId


				END
			

----====================================================
			delete from @tmpDep where Id = @Id
			END
END
GO
