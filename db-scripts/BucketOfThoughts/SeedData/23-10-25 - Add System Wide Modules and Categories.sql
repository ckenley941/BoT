IF NOT EXISTS (SELECT * FROM ThoughtModule WHERE Description = 'Other')
BEGIN
	INSERT INTO ThoughtModule (Description)
	VALUES ('Other')

	INSERT INTO ThoughtCategory (Description, ThoughtModuleId, SortOrder)
	SELECT 'Other', @@IDENTITY, 1
END
GO

IF NOT EXISTS (SELECT * FROM ThoughtModule WHERE Description = 'Random')
BEGIN
	INSERT INTO ThoughtModule (Description)
	VALUES ('Random')

	INSERT INTO ThoughtCategory (Description, ThoughtModuleId, SortOrder)
	SELECT 'Random', @@IDENTITY, 1
END
GO

IF NOT EXISTS (SELECT * FROM ThoughtModule WHERE Description = 'Music')
BEGIN
	INSERT INTO ThoughtModule (Description)
	VALUES ('Music')

	INSERT INTO ThoughtCategory (Description, ThoughtModuleId, SortOrder)
	SELECT 'Music', @@IDENTITY, 1
END
GO

IF NOT EXISTS (SELECT * FROM ThoughtModule WHERE Description = 'Outdoors')
BEGIN
	INSERT INTO ThoughtModule (Description)
	VALUES ('Outdoors')

	INSERT INTO ThoughtCategory (Description, ThoughtModuleId, SortOrder)
	SELECT 'Outdoors', @@IDENTITY, 1
END
GO

IF NOT EXISTS (SELECT * FROM ThoughtModule WHERE Description = 'Travel')
BEGIN
	INSERT INTO ThoughtModule (Description)
	VALUES ('Travel')

	
	INSERT INTO ThoughtCategory (Description, ThoughtModuleId, SortOrder)
	SELECT 'Travel', @@IDENTITY, 1
END
GO

IF NOT EXISTS (SELECT * FROM ThoughtModule WHERE Description = 'Entertainment')
BEGIN
	INSERT INTO ThoughtModule (Description)
	VALUES ('Entertainment')

	INSERT INTO ThoughtCategory (Description, ThoughtModuleId, SortOrder)
	SELECT 'Entertainment', @@IDENTITY, 1
END
GO

IF NOT EXISTS (SELECT * FROM ThoughtModule WHERE Description = 'Sports')
BEGIN
	INSERT INTO ThoughtModule (Description)
	VALUES ('Sports')

	INSERT INTO ThoughtCategory (Description, ThoughtModuleId, SortOrder)
	SELECT 'Sports', @@IDENTITY, 1
END
GO

IF NOT EXISTS (SELECT * FROM ThoughtModule WHERE Description = 'History')
BEGIN
	INSERT INTO ThoughtModule (Description)
	VALUES ('History')

	INSERT INTO ThoughtCategory (Description, ThoughtModuleId, SortOrder)
	SELECT 'History', @@IDENTITY, 1
END
GO
