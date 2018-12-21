
declare @tmpDep table
	(
		[Id] int not null,
		[Position] nvarchar(max),
		[DepartmentName] nvarchar(max),
		[DeputyUserName] nvarchar(max),
		[Email] nvarchar(max),
		[Roles] nvarchar(max),
		[PrefferedCulture] nvarchar(max),
		[Phone] nvarchar(max),
		[Mobile] nvarchar(max),
		[Cabinet] nvarchar(max),
		[Name] nvarchar(max),
		[DisplayNameRus] nvarchar(max),
		[FromNameRus] nvarchar(max),
		[ToNameRus] nvarchar(max),
		[DisplayNameEng] nvarchar(max),
		[FromNameEng] nvarchar(max),
		[ToNameEng] nvarchar(max),
		--[TimeStamp] [timestamp],
		[Priority] int,
		[Disabled] bit,
		[LastLogin] datetime,
		[PositionRus] nvarchar(max),
		[PositionKaz] nvarchar(max),
		[PositionEng] nvarchar(max),
		[DisplayNameKaz] nvarchar(max),
		[FromNameKaz] nvarchar(max),
		[ToNameKaz] nvarchar(max),
		[RegionString] nvarchar(max),

		primary key(Id)
	)

	declare 
		@Id as int,
		@Position as nvarchar(max),
		@DepartmentName as nvarchar(max),
		@DeputyUserName as nvarchar(max),
		@Email as nvarchar(max),
		@Roles as nvarchar(max),
		@PrefferedCulture as nvarchar(max),
		@Phone as nvarchar(max),
		@Mobile as nvarchar(max),
		@Cabinet as nvarchar(max),
		@Name as nvarchar(max),
		@DisplayNameRus as nvarchar(max),
		@FromNameRus as nvarchar(max),
		@ToNameRus as nvarchar(max),
		@DisplayNameEng as nvarchar(max),
		@FromNameEng as nvarchar(max),
		@ToNameEng as nvarchar(max),
		--@TimeStamp as [timestamp],
		@Priority as int,
		@Disabled as bit,
		@LastLogin as datetime,
		@PositionRus as nvarchar(max),
		@PositionKaz as nvarchar(max),
		@PositionEng as nvarchar(max),
		@DisplayNameKaz as nvarchar(max),
		@FromNameKaz as nvarchar(max),
		@ToNameKaz as nvarchar(max),
		@RegionString as nvarchar(max)

	

	insert @tmpDep
		SELECT [Id]
		  ,[Position]
		  ,[DepartmentName]
		  ,[DeputyUserName]
		  ,[Email]
		  ,[Roles]
		  ,[PrefferedCulture]
		  ,[Phone]
		  ,[Mobile]
		  ,[Cabinet]
		  ,[Name]
		  ,[DisplayNameRus]
		  ,[FromNameRus]
		  ,[ToNameRus]
		  ,[DisplayNameEng]
		  ,[FromNameEng]
		  ,[ToNameEng]
		  --,[TimeStamp]
		  ,[Priority]
		  ,[Disabled]
		  ,[LastLogin]
		  ,[PositionRus]
		  ,[PositionKaz]
		  ,[PositionEng]
		  ,[DisplayNameKaz]
		  ,[FromNameKaz]
		  ,[ToNameKaz]
		  ,[RegionString]
	  FROM [Docz].[dbo].[UserProfiles]


		--select * from @tmpDep

		while ((select count(*) from @tmpDep) > 0)
		BEGIN
--		--------------------------------------
			select top 1 
				@Id = Id,
				@Position = Position,
				@DepartmentName = DepartmentName,
				@DeputyUserName = DeputyUserName,
				@Email = Email,
				@Roles = Roles,
				@PrefferedCulture = PrefferedCulture,
				@Phone = Phone,
				@Mobile = Mobile,
				@Cabinet = Cabinet,
				@Name = Name,
				@DisplayNameRus = DisplayNameRus,
				@FromNameRus = FromNameRus,
				@ToNameRus = ToNameRus,
				@DisplayNameEng = DisplayNameEng,
				@FromNameEng = FromNameEng,
				@ToNameEng = ToNameEng,
				--@TimeStamp as [timestamp],
				@Priority = [Priority],
				@Disabled = [Disabled],
				@LastLogin = LastLogin,
				@PositionRus = PositionRus,
				@PositionKaz = PositionKaz,
				@PositionEng = PositionEng,
				@DisplayNameKaz = DisplayNameKaz,
				@FromNameKaz = FromNameKaz,
				@ToNameKaz = ToNameKaz,
				@RegionString = RegionString
		
			from @tmpDep
			-------------------------
			declare @tempId int
			SET @tempId = null
			--SET @DepartmentVId = NEWID()

			SELECT @tempId = Id 
				from [dbo].[UserTemps]
				WHERE Name = @Name

			IF(@tempId is null)
				BEGIN
					INSERT INTO [dbo].[UserTemps]
				   ([Position]
				   ,[DepartmentName]
				   ,[DeputyUserName]
				   ,[Email]
				   ,[Roles]
				   ,[PrefferedCulture]
				   ,[Phone]
				   ,[Mobile]
				   ,[Cabinet]
				   ,[Name]
				   ,[DisplayNameRus]
				   ,[FromNameRus]
				   ,[ToNameRus]
				   ,[DisplayNameEng]
				   ,[FromNameEng]
				   ,[ToNameEng]
				   ,[Priority]
				   ,[Disabled]
				   ,[LastLogin]
				   ,[PositionRus]
				   ,[PositionKaz]
				   ,[PositionEng]
				   ,[DisplayNameKaz]
				   ,[FromNameKaz]
				   ,[ToNameKaz]
				   ,[RegionString])
			 VALUES
				   (
				    @Position
				   ,@DepartmentName
				   ,@DeputyUserName
				   ,@Email
				   ,@Roles
				   ,@PrefferedCulture
				   ,@Phone
				   ,@Mobile
				   ,@Cabinet
				   ,@Name
				   ,@DisplayNameRus
				   ,@FromNameRus
				   ,@ToNameRus
				   ,@DisplayNameEng
				   ,@FromNameEng
				   ,@ToNameEng
				   ,@Priority
				   ,@Disabled
				   ,@LastLogin
				   ,@PositionRus
				   ,@PositionKaz
				   ,@PositionEng
				   ,@DisplayNameKaz
				   ,@FromNameKaz
				   ,@ToNameKaz
				   ,@RegionString)

				END
			ELSE
				BEGIN
					UPDATE [dbo].[UserTemps]
					   SET [Position] = @Position
						  ,[DepartmentName] = @DepartmentName
						  ,[DeputyUserName] = @DeputyUserName
						  ,[Email] = @Email
						  ,[Roles] = @Roles
						  ,[PrefferedCulture] = @PrefferedCulture
						  ,[Phone] = @Phone
						  ,[Mobile] = @Mobile
						  ,[Cabinet] = @Cabinet
						  ,[Name] = @Name
						  ,[DisplayNameRus] = @DisplayNameRus
						  ,[FromNameRus] = @FromNameRus
						  ,[ToNameRus] = @ToNameRus
						  ,[DisplayNameEng] = @DisplayNameEng
						  ,[FromNameEng] = @FromNameEng
						  ,[ToNameEng] = @ToNameEng
						  ,[Priority] = @Priority
						  ,[Disabled] = @Disabled
						  ,[LastLogin] = @LastLogin
						  ,[PositionRus] = @PositionRus
						  ,[PositionKaz] = @PositionKaz
						  ,[PositionEng] = @PositionEng
						  ,[DisplayNameKaz] = @DisplayNameKaz
						  ,[FromNameKaz] = @FromNameKaz
						  ,[ToNameKaz] = @ToNameKaz
						  ,[RegionString] = @RegionString
					 WHERE Id = @tempId

				END


------====================================================
			delete from @tmpDep where Id = @Id
			END