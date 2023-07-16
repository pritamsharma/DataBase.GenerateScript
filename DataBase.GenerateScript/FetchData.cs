#region [ Version Info ]
/*=============================================  
-- Author:      Pritam Sharma  
-- Create date: 29th May 2009
-- Description: Generating Script
-- ============================================= */
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;

namespace DataBase.GenerateScript
{
	/// <summary>
	/// Connects to the database and interacts with database
	/// </summary>
	public class FetchData
	{

		#region Global Variables

		/// <summary>
		/// 
		/// </summary>
		string strConnection = string.Empty;

		/// <summary>
		/// 
		/// </summary>
		DbProviderFactory objFactory;// = null;

		/// <summary>
		/// 
		/// </summary>
		DbConnection dbConnection;

		/// <summary>
		/// 
		/// </summary>
		DbCommand objCommand;

		#endregion Global Variables

		#region Sql Query

		#region strFnSplit

		private static string strFnSplit = @"

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_Split]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'
Create FUNCTION [dbo].[fn_Split](@sText varchar(8000), @sDelim varchar(20) = '' '')  

RETURNS @retArray TABLE (idx smallint Primary Key, value varchar(8000))  

AS  

BEGIN  

DECLARE @idx smallint,  

 @value varchar(8000),  

 @bcontinue bit,  

 @iStrike smallint,  

 @iDelimlength tinyint  

IF @sDelim = ''Space''  

 BEGIN  

 SET @sDelim = '' ''  

 END  

SET @idx = 0  

SET @sText = LTrim(RTrim(@sText))  

SET @iDelimlength = DATALENGTH(@sDelim)  

SET @bcontinue = 1  

IF NOT ((@iDelimlength = 0) or (@sDelim = ''Empty''))  

 BEGIN  

 WHILE @bcontinue = 1  

  BEGIN  

--If you can find the delimiter in the text, retrieve the first element and  

--insert it with its index into the return table.  

   

  IF CHARINDEX(@sDelim, @sText)>0  

   BEGIN  

   SET @value = SUBSTRING(@sText,1, CHARINDEX(@sDelim,@sText)-1)  

	BEGIN  

	INSERT @retArray (idx, value)  

	VALUES (@idx, @value)  

	END  

	 

--Trim the element and its delimiter from the front of the string.  

   --Increment the index and loop.  

SET @iStrike = DATALENGTH(@value) + @iDelimlength  

   SET @idx = @idx + 1  

   SET @sText = LTrim(Right(@sText,DATALENGTH(@sText) - @iStrike))  

	

   END  

  ELSE  

   BEGIN  

--If you can''t find the delimiter in the text, @sText is the last value in  

--@retArray.  

 SET @value = @sText  

	BEGIN  

	INSERT @retArray (idx, value)  

	VALUES (@idx, @value)  

	END  

   --Exit the WHILE loop.  

SET @bcontinue = 0  

   END  

  END  

 END  

ELSE  

 BEGIN  

 WHILE @bcontinue=1  

  BEGIN  

  --If the delimiter is an empty string, check for remaining text  

  --instead of a delimiter. Insert the first character into the  

  --retArray table. Trim the character from the front of the string.  

--Increment the index and loop.  

  IF DATALENGTH(@sText)>1  

   BEGIN  

   SET @value = SUBSTRING(@sText,1,1)  

	BEGIN  

	INSERT @retArray (idx, value)  

	VALUES (@idx, @value)  

	END  

   SET @idx = @idx+1  

   SET @sText = SUBSTRING(@sText,2,DATALENGTH(@sText)-1)  

	 

   END  

  ELSE  

   BEGIN  

   --One character remains.  

   --Insert the character, and exit the WHILE loop.  

   INSERT @retArray (idx, value)  

   VALUES (@idx, @sText)  

   SET @bcontinue = 0   

   END  

 END  

END  

RETURN  

END  



' 
END


";

		#endregion

		#region strSqlCreateSpTemplate

		private static string strSqlCreateSpTemplate = @"
-- ===================================================================
-- Author:      Pritam Sharma
-- Create date: 9th Feb 2009
-- Description:	Creating stored procedure
--              @table_name --> Table name in comma seperated format
--              @all_table  --> 'Y' Creates stored procedure of all the table present in the database
-- ===================================================================

DECLARE @name_table VARCHAR(MAX),
@all_table  CHAR(1)

SELECT @name_table ='§1',
@all_table  = '§2'

BEGIN

	SET @name_table = REPLACE(@name_table,CHAR(13)+CHAR(10),' ')
	SET @name_table = REPLACE(@name_table,'
','')

  DECLARE @table_temp TABLE (pri_col INT IDENTITY PRIMARY KEY,script_col VARCHAR(MAX))
  
  INSERT INTO @table_temp ([script_col]) VALUES('--SCRIPT DATE: '+CONVERT(VARCHAR(30),GETDATE())+'   --
')
	DECLARE
	@tbl_name VARCHAR(100),
	@date_current DATETIME

	SET @date_current = GETDATE()

	IF(@all_table = 'Y')
	BEGIN
		--Generating stored procedure for all the tables in the database.
		DECLARE curName CURSOR FOR
		SELECT [name] FROM sysobjects WHERE [type] = 'U' ORDER BY [name]

		OPEN curName
		FETCH next FROM curName INTO @tbl_name
		WHILE(@@FETCH_STATUS = 0)	
		BEGIN
--		PRINT 'Generating stored procedure for all the tables in the database'
--		EXEC sp_create_stored_proc_single @tbl_name , @date_current

DECLARE @table_name VARCHAR(MAX),
@current_date DATETIME

SELECT @table_name = @tbl_name,
@current_date = @date_current

BEGIN

	IF( EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].['+@table_name+']') AND [type] = 'U'))
	BEGIN
	
		DECLARE
		@query VARCHAR(MAX),
		@column_name VARCHAR(200),
		@datatype VARCHAR(100),
		@length VARCHAR(10),
		@xprec VARCHAR(10),
		@xscale VARCHAR(10),
		@colstat INT,
		@count INT

		SET @count = 0

		SET @query = '

-- ===========================================================================
-- Author:	    Pritam Sharma
-- Create date: '+ CONVERT(VARCHAR(40),@current_date)+'  
-- Description:	Mode:> I --> INSERT, U --> UPDATE, D --> DELETE
-- ============================================================================
CREATE PROCEDURE [dbo].[usp_'+@table_name+'] 
 @mode CHAR(1)'


DECLARE curNameChld CURSOR FOR
SELECT syscolumns.[name] ,
master..systypes.[name] AS datatype,
syscolumns.length,
syscolumns.xprec,
syscolumns.xscale
FROM syscolumns
INNER JOIN master..systypes
ON syscolumns.xtype = master..systypes.xtype
WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
--AND syscolumns.colstat <> 1
--AND master..systypes.xtype <> 189
ORDER BY syscolumns.colid

OPEN curNameChld
FETCH next FROM curNameChld INTO @column_name,@datatype,@length,@xprec,@xscale
WHILE(@@FETCH_STATUS = 0)	
BEGIN

	SET @datatype = UPPER(@datatype)

	IF(@datatype='DATETIME' OR @datatype='SMALLDATETIME' OR @datatype='INT' OR  
	@datatype='BIGINT' OR @datatype='SMALLINT' OR @datatype='TINYINT' OR
	@datatype='NUMERIC' OR @datatype='BIT' OR @datatype='SMALLMONEY' OR @datatype='MONEY' OR
	@datatype='FLOAT' OR @datatype='REAL' OR @datatype='TIMESTAMP' OR @datatype='XML'
	OR @datatype='UNIQUEIDENTIFIER')
	BEGIN
		SET @query = @query +'
,@'+@column_name +'   '+@datatype
	END
	ELSE IF(@datatype='DECIMAL')
	BEGIN 
		SET @query = @query +'
,@'+@column_name +'   '+@datatype+'('+@xprec+','+@xscale+')'
	END
	ELSE
	BEGIN
		SET @query = @query +'
,@'+@column_name +'   '+@datatype+'('+@length+')'
	END

	FETCH next FROM curNameChld INTO @column_name,@datatype,@length,@xprec,@xscale
END
CLOSE curNameChld
DEALLOCATE curNameChld

-----------------------------------------------------------

DECLARE curNameChld CURSOR FOR
SELECT syscolumns.[name] ,
master..systypes.[name] AS datatype,
syscolumns.length,
syscolumns.xprec,
syscolumns.xscale,
syscolumns.colstat
FROM information_schema.table_constraints tc
INNER JOIN information_schema.key_column_usage kcu
ON tc.constraint_name = kcu.constraint_name
INNER JOIN syscolumns
ON kcu.column_name = syscolumns.[name]
INNER JOIN master..systypes
ON syscolumns.xtype = master..systypes.xtype
WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
AND tc.table_name = @table_name
AND tc.constraint_type = 'PRIMARY KEY'
AND syscolumns.colstat <> 1
ORDER BY syscolumns.colid

OPEN curNameChld
FETCH next FROM curNameChld INTO @column_name,@datatype,@length,@xprec,@xscale,@colstat
WHILE(@@FETCH_STATUS = 0)	
BEGIN

	SET @datatype = UPPER(@datatype)

	IF(@colstat<>1)
	BEGIN

		IF(@datatype='DATETIME' OR @datatype='SMALLDATETIME' OR @datatype='INT' OR  
		@datatype='BIGINT' OR @datatype='SMALLINT' OR @datatype='TINYINT' OR
		@datatype='NUMERIC' OR @datatype='BIT' OR @datatype='SMALLMONEY' OR @datatype='MONEY' OR
		@datatype='FLOAT' OR @datatype='REAL' OR @datatype='TIMESTAMP' OR @datatype='XML'
		OR @datatype='UNIQUEIDENTIFIER')
		BEGIN
			SET @query = @query +'
,@old_'+@column_name +'   '+@datatype
		END
		ELSE IF(@datatype='DECIMAL')
		BEGIN 
			SET @query = @query +'
,@old_'+@column_name +'   '+@datatype+'('+@xprec+','+@xscale+')'
		END
		ELSE
		BEGIN
			SET @query = @query +'
,@old_'+@column_name +'   '+@datatype+'('+@length+')'
		END

	END

	FETCH next FROM curNameChld INTO @column_name,@datatype,@length,@xprec,@xscale,@colstat
END
CLOSE curNameChld
DEALLOCATE curNameChld

------------------------------------------------------------------------

--Declare columns in the stored procedure
		SET @query =@query+'

AS
BEGIN

	BEGIN TRY

		IF(@mode = ''D'')
		BEGIN

			DELETE FROM '+@table_name+' 
			WHERE'

			SET @count = 0
			DECLARE curNamePrimaryKey CURSOR FOR
	--		SELECT kcu.column_name
	--		FROM information_schema.table_constraints tc
	--		INNER JOIN information_schema.key_column_usage kcu
	--		ON tc.constraint_name = kcu.constraint_name
	--		WHERE tc.table_name = @table_name
	--		AND tc.constraint_type = 'PRIMARY KEY'
			SELECT syscolumns.[name] ,
			syscolumns.colstat
			FROM information_schema.table_constraints tc
			INNER JOIN information_schema.key_column_usage kcu
			ON tc.constraint_name = kcu.constraint_name
			INNER JOIN syscolumns
			ON kcu.column_name = syscolumns.[name]
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
			AND tc.table_name = @table_name
			AND tc.constraint_type = 'PRIMARY KEY'
			ORDER BY syscolumns.colid

			OPEN curNamePrimaryKey
			FETCH next FROM curNamePrimaryKey INTO @column_name,@colstat
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN
				

				IF(@count = 0)
				BEGIN
					IF(@colstat=1)
						SET @query = @query+' '+@column_name+' = @'+@column_name
					ELSE
						SET @query = @query+' '+@column_name+' = @old_'+@column_name
				END		
				ELSE
				BEGIN
					IF(@colstat=1)
						SET @query = @query+' 
			AND '+@column_name+' = @'+@column_name
					ELSE
						SET @query = @query+' 
			AND '+@column_name+' = @old_'+@column_name

				END	

				SET @count = @count + 1
				FETCH next FROM curNamePrimaryKey INTO @column_name,@colstat
			END
			CLOSE curNamePrimaryKey
			DEALLOCATE curNamePrimaryKey

			SET @query = @query+' 

		END
		ELSE IF(@mode = ''U'')
		BEGIN

			UPDATE '+@table_name+' SET '
			--putting value in all columns which need to be updated

			SET @count = 0
			DECLARE curNameChld CURSOR FOR
			SELECT syscolumns.[name]
			FROM syscolumns
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name)
			AND syscolumns.colstat <> 1
			AND master..systypes.xtype <> 189
			ORDER BY syscolumns.colid

			OPEN curNameChld
			FETCH next FROM curNameChld INTO @column_name
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN

				DECLARE @count_column INT

				SELECT @count_column = COUNT(syscolumns.[name])
				FROM syscolumns
				INNER JOIN master..systypes
				ON syscolumns.xtype = master..systypes.xtype
				WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name)
				AND syscolumns.colstat <> 1
				AND master..systypes.xtype <> 189

				IF(@count=@count_column-1)
					SET @query = @query +'
			'+@column_name+' = @'+@column_name
				ELSE 
					SET @query = @query +'
			'+@column_name+' = @'+@column_name+','

				SET @count = @count + 1
				
				FETCH next FROM curNameChld INTO @column_name
			END
			CLOSE curNameChld
			DEALLOCATE curNameChld

			SET @count = 0

			SET @query = @query +' 
			WHERE'

			SET @count = 0
			DECLARE curNamePrimaryKey CURSOR FOR
			SELECT syscolumns.[name] ,
			syscolumns.colstat
			FROM information_schema.table_constraints tc
			INNER JOIN information_schema.key_column_usage kcu
			ON tc.constraint_name = kcu.constraint_name
			INNER JOIN syscolumns
			ON kcu.column_name = syscolumns.[name]
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
			AND tc.table_name = @table_name
			AND tc.constraint_type = 'PRIMARY KEY'
			ORDER BY syscolumns.colid

			OPEN curNamePrimaryKey
			FETCH next FROM curNamePrimaryKey INTO @column_name,@colstat
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN
				

				IF(@count = 0)
				BEGIN
					IF(@colstat=1)
						SET @query = @query+' '+@column_name+' = @'+@column_name
					ELSE
						SET @query = @query+' '+@column_name+' = @old_'+@column_name
				END		
				ELSE
				BEGIN
					IF(@colstat=1)
						SET @query = @query+' 
			AND '+@column_name+' = @'+@column_name
					ELSE
						SET @query = @query+' 
			AND '+@column_name+' = @old_'+@column_name

				END	

				SET @count = @count + 1
				FETCH next FROM curNamePrimaryKey INTO @column_name,@colstat
			END
			CLOSE curNamePrimaryKey
			DEALLOCATE curNamePrimaryKey

			SET @query = @query +'

		END
		ELSE IF(@mode = ''I'')
		BEGIN

			INSERT INTO '+@table_name+'('
			--All the columns whose value is to be inserted
			DECLARE curNameChld CURSOR FOR
			SELECT syscolumns.[name]
			FROM syscolumns
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name)
			AND syscolumns.colstat <> 1
			AND master..systypes.xtype <> 189
			ORDER BY syscolumns.colid

			OPEN curNameChld
			FETCH next FROM curNameChld INTO @column_name
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN

				IF(@count = 0)
					SET @query = @query +'
			'+@column_name
				ELSE
					SET @query = @query +'
			,'+@column_name

				SET @count = @count + 1

				FETCH next FROM curNameChld INTO @column_name
			END
			CLOSE curNameChld
			DEALLOCATE curNameChld

			SET @count = 0

			SET @query = @query+')
			VALUES ('
			--All the values to be added to the columns
			DECLARE curNameChld CURSOR FOR
			SELECT syscolumns.[name]
			FROM syscolumns
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
			AND syscolumns.colstat <> 1
			AND master..systypes.xtype <> 189
			ORDER BY syscolumns.colid

			OPEN curNameChld
			FETCH next FROM curNameChld INTO @column_name
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN

				IF(@count = 0)
					SET @query = @query +'
			@'+@column_name
				ELSE
					SET @query = @query +'
			,@'+@column_name

				SET @count = @count + 1

				FETCH next FROM curNameChld INTO @column_name
			END
			CLOSE curNameChld
			DEALLOCATE curNameChld
			SET @query = @query+')

		END

		ELSE
		BEGIN

			RAISERROR(''Please enter proper mode to perform any operation'', 11, 1)

		END

	END TRY

	BEGIN CATCH

		DECLARE @error_message  VARCHAR(MAX),	@error_severity INT
		SELECT @error_message  = ERROR_MESSAGE(), @error_severity = ERROR_SEVERITY()
		RAISERROR(@error_message, @error_severity, 1)

	END CATCH

END

	'

		SET @query = REPLACE(@query, '''', '''''')

		PRINT '
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[usp_'+@table_name+']'') AND type in (N''P'', N''PC''))
BEGIN
	EXEC dbo.sp_executesql @statement = N'''+@query+'''
END

GO

	'

		INSERT INTO @table_temp (script_col)VALUES('
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[usp_'+@table_name+']'') AND type in (N''P'', N''PC''))
BEGIN
	EXEC dbo.sp_executesql @statement = N'''+@query+'''
END

GO

	')

	END

END

			FETCH next FROM curName INTO @tbl_name
		END
		CLOSE curName
		DEALLOCATE curName


	END
	ELSE IF(@name_table <> '')
	BEGIN
		--Generating stored procedure for all the tables passed in the comma seperated format.

		DECLARE curName CURSOR FOR
		SELECT value FROM dbo.fn_Split(@name_table,',')

		OPEN curName
		FETCH next FROM curName INTO @tbl_name
		WHILE(@@FETCH_STATUS = 0)	
		BEGIN
--		PRINT 'Generating stored procedure for all the tables passed in the comma seperated format'
--		EXEC sp_create_stored_proc_single @tbl_name , @date_current

--DECLARE @table_name VARCHAR(MAX),
--@current_date DATETIME

SELECT @table_name = @tbl_name,
@current_date = @date_current

BEGIN

	IF( EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].['+@table_name+']') AND [type] = 'U'))
	BEGIN
	
		SELECT
		@query = NULL,
		@column_name = NULL,
		@datatype = NULL,
		@length = NULL,
		@xprec = NULL,
		@xscale = NULL,
		@colstat = 0,
		@count = 0

		SET @query = '

-- ===========================================================================
-- Author:	    Pritam Sharma
-- Create date: '+ CONVERT(VARCHAR(40),@current_date)+'  
-- Description:	Mode:> I --> INSERT, U --> UPDATE, D --> DELETE
-- ============================================================================
CREATE PROCEDURE [dbo].[usp_'+@table_name+'] 
 @mode CHAR(1)'


DECLARE curNameChld CURSOR FOR
SELECT syscolumns.[name] ,
master..systypes.[name] AS datatype,
syscolumns.length,
syscolumns.xprec,
syscolumns.xscale
FROM syscolumns
INNER JOIN master..systypes
ON syscolumns.xtype = master..systypes.xtype
WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
--AND syscolumns.colstat <> 1
--AND master..systypes.xtype <> 189
ORDER BY syscolumns.colid

OPEN curNameChld
FETCH next FROM curNameChld INTO @column_name,@datatype,@length,@xprec,@xscale
WHILE(@@FETCH_STATUS = 0)	
BEGIN

	SET @datatype = UPPER(@datatype)

	IF(@datatype='DATETIME' OR @datatype='SMALLDATETIME' OR @datatype='INT' OR  
	@datatype='BIGINT' OR @datatype='SMALLINT' OR @datatype='TINYINT' OR
	@datatype='NUMERIC' OR @datatype='BIT' OR @datatype='SMALLMONEY' OR @datatype='MONEY' OR
	@datatype='FLOAT' OR @datatype='REAL' OR @datatype='TIMESTAMP' OR @datatype='XML'
	OR @datatype='UNIQUEIDENTIFIER')
	BEGIN
		SET @query = @query +'
,@'+@column_name +'   '+@datatype
	END
	ELSE IF(@datatype='DECIMAL')
	BEGIN 
		SET @query = @query +'
,@'+@column_name +'   '+@datatype+'('+@xprec+','+@xscale+')'
	END
	ELSE
	BEGIN
		SET @query = @query +'
,@'+@column_name +'   '+@datatype+'('+@length+')'
	END

	FETCH next FROM curNameChld INTO @column_name,@datatype,@length,@xprec,@xscale
END
CLOSE curNameChld
DEALLOCATE curNameChld

-----------------------------------------------------------

DECLARE curNameChld CURSOR FOR
SELECT syscolumns.[name] ,
master..systypes.[name] AS datatype,
syscolumns.length,
syscolumns.xprec,
syscolumns.xscale,
syscolumns.colstat
FROM information_schema.table_constraints tc
INNER JOIN information_schema.key_column_usage kcu
ON tc.constraint_name = kcu.constraint_name
INNER JOIN syscolumns
ON kcu.column_name = syscolumns.[name]
INNER JOIN master..systypes
ON syscolumns.xtype = master..systypes.xtype
WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
AND tc.table_name = @table_name
AND tc.constraint_type = 'PRIMARY KEY'
AND syscolumns.colstat <> 1
ORDER BY syscolumns.colid

OPEN curNameChld
FETCH next FROM curNameChld INTO @column_name,@datatype,@length,@xprec,@xscale,@colstat
WHILE(@@FETCH_STATUS = 0)	
BEGIN

	SET @datatype = UPPER(@datatype)

	IF(@colstat<>1)
	BEGIN

		IF(@datatype='DATETIME' OR @datatype='SMALLDATETIME' OR @datatype='INT' OR  
		@datatype='BIGINT' OR @datatype='SMALLINT' OR @datatype='TINYINT' OR
		@datatype='NUMERIC' OR @datatype='BIT' OR @datatype='SMALLMONEY' OR @datatype='MONEY' OR
		@datatype='FLOAT' OR @datatype='REAL' OR @datatype='TIMESTAMP' OR @datatype='XML'
		OR @datatype='UNIQUEIDENTIFIER')
		BEGIN
			SET @query = @query +'
,@old_'+@column_name +'   '+@datatype
		END
		ELSE IF(@datatype='DECIMAL')
		BEGIN 
			SET @query = @query +'
,@old_'+@column_name +'   '+@datatype+'('+@xprec+','+@xscale+')'
		END
		ELSE
		BEGIN
			SET @query = @query +'
,@old_'+@column_name +'   '+@datatype+'('+@length+')'
		END

	END

	FETCH next FROM curNameChld INTO @column_name,@datatype,@length,@xprec,@xscale,@colstat
END
CLOSE curNameChld
DEALLOCATE curNameChld

------------------------------------------------------------------------

--Declare columns in the stored procedure
		SET @query =@query+'

AS
BEGIN

	BEGIN TRY

		IF(@mode = ''D'')
		BEGIN

			DELETE FROM '+@table_name+' 
			WHERE'

			SET @count = 0
			DECLARE curNamePrimaryKey CURSOR FOR
	--		SELECT kcu.column_name
	--		FROM information_schema.table_constraints tc
	--		INNER JOIN information_schema.key_column_usage kcu
	--		ON tc.constraint_name = kcu.constraint_name
	--		WHERE tc.table_name = @table_name
	--		AND tc.constraint_type = 'PRIMARY KEY'
			SELECT syscolumns.[name] ,
			syscolumns.colstat
			FROM information_schema.table_constraints tc
			INNER JOIN information_schema.key_column_usage kcu
			ON tc.constraint_name = kcu.constraint_name
			INNER JOIN syscolumns
			ON kcu.column_name = syscolumns.[name]
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
			AND tc.table_name = @table_name
			AND tc.constraint_type = 'PRIMARY KEY'
			ORDER BY syscolumns.colid

			OPEN curNamePrimaryKey
			FETCH next FROM curNamePrimaryKey INTO @column_name,@colstat
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN
				

				IF(@count = 0)
				BEGIN
					IF(@colstat=1)
						SET @query = @query+' '+@column_name+' = @'+@column_name
					ELSE
						SET @query = @query+' '+@column_name+' = @old_'+@column_name
				END		
				ELSE
				BEGIN
					IF(@colstat=1)
						SET @query = @query+' 
			AND '+@column_name+' = @'+@column_name
					ELSE
						SET @query = @query+' 
			AND '+@column_name+' = @old_'+@column_name

				END	

				SET @count = @count + 1
				FETCH next FROM curNamePrimaryKey INTO @column_name,@colstat
			END
			CLOSE curNamePrimaryKey
			DEALLOCATE curNamePrimaryKey

			SET @query = @query+' 

		END
		ELSE IF(@mode = ''U'')
		BEGIN

			UPDATE '+@table_name+' SET '
			--putting value in all columns which need to be updated

			SET @count = 0
			DECLARE curNameChld CURSOR FOR
			SELECT syscolumns.[name]
			FROM syscolumns
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name)
			AND syscolumns.colstat <> 1
			AND master..systypes.xtype <> 189
			ORDER BY syscolumns.colid

			OPEN curNameChld
			FETCH next FROM curNameChld INTO @column_name
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN

--				DECLARE @count_column INT

				SELECT @count_column = COUNT(syscolumns.[name])
				FROM syscolumns
				INNER JOIN master..systypes
				ON syscolumns.xtype = master..systypes.xtype
				WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name)
				AND syscolumns.colstat <> 1
				AND master..systypes.xtype <> 189

				IF(@count=@count_column-1)
					SET @query = @query +'
			'+@column_name+' = @'+@column_name
				ELSE 
					SET @query = @query +'
			'+@column_name+' = @'+@column_name+','

				SET @count = @count + 1
				
				FETCH next FROM curNameChld INTO @column_name
			END
			CLOSE curNameChld
			DEALLOCATE curNameChld

			SET @count = 0

			SET @query = @query +' 
			WHERE'

			SET @count = 0
			DECLARE curNamePrimaryKey CURSOR FOR
			SELECT syscolumns.[name] ,
			syscolumns.colstat
			FROM information_schema.table_constraints tc
			INNER JOIN information_schema.key_column_usage kcu
			ON tc.constraint_name = kcu.constraint_name
			INNER JOIN syscolumns
			ON kcu.column_name = syscolumns.[name]
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
			AND tc.table_name = @table_name
			AND tc.constraint_type = 'PRIMARY KEY'
			ORDER BY syscolumns.colid

			OPEN curNamePrimaryKey
			FETCH next FROM curNamePrimaryKey INTO @column_name,@colstat
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN
				

				IF(@count = 0)
				BEGIN
					IF(@colstat=1)
						SET @query = @query+' '+@column_name+' = @'+@column_name
					ELSE
						SET @query = @query+' '+@column_name+' = @old_'+@column_name
				END		
				ELSE
				BEGIN
					IF(@colstat=1)
						SET @query = @query+' 
			AND '+@column_name+' = @'+@column_name
					ELSE
						SET @query = @query+' 
			AND '+@column_name+' = @old_'+@column_name

				END	

				SET @count = @count + 1
				FETCH next FROM curNamePrimaryKey INTO @column_name,@colstat
			END
			CLOSE curNamePrimaryKey
			DEALLOCATE curNamePrimaryKey

			SET @query = @query +'

		END
		ELSE IF(@mode = ''I'')
		BEGIN

			INSERT INTO '+@table_name+'('
			--All the columns whose value is to be inserted
			DECLARE curNameChld CURSOR FOR
			SELECT syscolumns.[name]
			FROM syscolumns
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name)
			AND syscolumns.colstat <> 1
			AND master..systypes.xtype <> 189
			ORDER BY syscolumns.colid

			OPEN curNameChld
			FETCH next FROM curNameChld INTO @column_name
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN

				IF(@count = 0)
					SET @query = @query +'
			'+@column_name
				ELSE
					SET @query = @query +'
			,'+@column_name

				SET @count = @count + 1

				FETCH next FROM curNameChld INTO @column_name
			END
			CLOSE curNameChld
			DEALLOCATE curNameChld

			SET @count = 0

			SET @query = @query+')
			VALUES ('
			--All the values to be added to the columns
			DECLARE curNameChld CURSOR FOR
			SELECT syscolumns.[name]
			FROM syscolumns
			INNER JOIN master..systypes
			ON syscolumns.xtype = master..systypes.xtype
			WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )
			AND syscolumns.colstat <> 1
			AND master..systypes.xtype <> 189
			ORDER BY syscolumns.colid

			OPEN curNameChld
			FETCH next FROM curNameChld INTO @column_name
			WHILE(@@FETCH_STATUS = 0)	
			BEGIN

				IF(@count = 0)
					SET @query = @query +'
			@'+@column_name
				ELSE
					SET @query = @query +'
			,@'+@column_name

				SET @count = @count + 1

				FETCH next FROM curNameChld INTO @column_name
			END
			CLOSE curNameChld
			DEALLOCATE curNameChld
			SET @query = @query+')

		END

		ELSE
		BEGIN

			RAISERROR(''Please enter proper mode to perform any operation'', 11, 1)

		END

	END TRY

	BEGIN CATCH

		DECLARE @error_message  VARCHAR(MAX),	@error_severity INT
		SELECT @error_message  = ERROR_MESSAGE(), @error_severity = ERROR_SEVERITY()
		RAISERROR(@error_message, @error_severity, 1)

	END CATCH

END

	'

		SET @query = REPLACE(@query, '''', '''''')

		PRINT '
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[usp_'+@table_name+']'') AND type in (N''P'', N''PC''))
BEGIN
	EXEC dbo.sp_executesql @statement = N'''+@query+'''
END

GO

	'

		INSERT INTO @table_temp (script_col)VALUES('
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].[usp_'+@table_name+']'') AND type in (N''P'', N''PC''))
BEGIN
	EXEC dbo.sp_executesql @statement = N'''+@query+'''
END

GO

	')

	END

END

			FETCH next FROM curName INTO @tbl_name
		END
		CLOSE curName
		DEALLOCATE curName

	END
	ELSE
	BEGIN
		PRINT 'Please enter proper data'
	END

	SELECT script_col FROM @table_temp

END



";

		#endregion strSqlCreateSpTemplate

		#region strSqlCreateStoredProcScript

		private static string strSqlCreateStoredProcScript = @"
-- =============================================
-- Author:		  PRITAM SHARMA
-- Create Date: 01/20/2009
-- Modified Date: 12/09/2009 Reason:Modified to create alter script
-- Description:	For Generating script
--              @name --> Name of stored procedure functions and views in comma seperated format
-- =============================================

DECLARE 
@name   VARCHAR(MAX), -- Comma seperated format
@insert_delete CHAR(1) -- If 'Y' Then it will create insert delete script, if not then it will create an alter script

SELECT 
@name          = '§1',
@insert_delete = '§2'

BEGIN
SET NOCOUNT ON

	SET @name = REPLACE(@name,CHAR(13)+CHAR(10),' ')
	SET @name = REPLACE(@name,'
','')

	DECLARE
	@ind_name VARCHAR(100)

	DECLARE @CommentText 
	TABLE (
	[LineId]	INT IDENTITY(1,1) NOT NULL,
	[Text]    VARCHAR(8000) COLLATE database_default)

	INSERT INTO @CommentText ([Text]) VALUES('--SCRIPT DATE: '+CONVERT(VARCHAR(30),GETDATE())+'   --
')

	DECLARE curName CURSOR FOR
	SELECT [value] FROM dbo.fn_Split(@name,',')

	OPEN curName
	FETCH next FROM curName INTO @ind_name
	WHILE(@@FETCH_STATUS = 0)	
	BEGIN

	DECLARE 
		@objid	    INT

		SELECT @objid = OBJECT_ID(@ind_name)

	IF(@insert_delete <> 'Y')
	BEGIN

	  IF(@objid < 0)
	  BEGIN

		IF( (SELECT COUNT(1) FROM master.sys.syscomments
			 WHERE id = @objid )>0)
		BEGIN

		  INSERT INTO @CommentText ([Text]) VALUES('
------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

')

		  INSERT INTO @CommentText 
		  ([Text])
		  SELECT [text] 
		  FROM  master.sys.syscomments 
				WHERE id = @objid
				ORDER BY number, colid 

		  INSERT INTO @CommentText ([Text]) VALUES('

GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------------------
')

		END
	  END
	  ELSE
	  BEGIN

		IF( (SELECT COUNT(1) FROM syscomments 
				   WHERE id = @objid 
				   AND encrypted = 0 )>0)
		BEGIN

		  INSERT INTO @CommentText ([Text]) VALUES('
------------------------------------------------------------------------------------------------------

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

')
	  
		  INSERT INTO @CommentText
		  ([Text]) 
		  SELECT [text] 
		  FROM syscomments 
				WHERE id = @objid 
				AND encrypted = 0
				ORDER BY number, colid

		  INSERT INTO @CommentText ([Text]) VALUES('

GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

------------------------------------------------------------------------------------------------------
')
		  
		END

	  END

	END

	ELSE
	BEGIN
------------------
		  DECLARE 
		  @objname      VARCHAR(1000),
		  @stringencode CHAR(1),
		  @print_end    CHAR(1)

		  SELECT 
		  @objname      = @ind_name,
		  @stringencode = 'Y',
		  @print_end    = 'N'

		  DECLARE @CommentTextTemp 
		  TABLE (
		  [LineId] INT,
		  [Text]   VARCHAR(8000) COLLATE database_default )

		  DECLARE 
		  @LineId     INT,
		  @SyscomText	VARCHAR(8000)

		  DELETE FROM @CommentTextTemp

		  SELECT @objid = OBJECT_ID(@objname)

		  IF @objid < 0	-- Handle system-objects
		  BEGIN

			  DECLARE ms_crs_syscom CURSOR LOCAL 
			  FOR SELECT [text],[colid] FROM master.sys.syscomments 
			  WHERE id = @objid
			  ORDER BY number, colid 
			  FOR READ ONLY

		  END
		  ELSE
		  BEGIN

			  DECLARE ms_crs_syscom  CURSOR LOCAL
			  FOR SELECT [text],[colid] FROM syscomments 
			  WHERE id = @objid 
			  AND encrypted = 0
			  ORDER BY number, colid
			  FOR READ ONLY

		  END

		  IF( EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID('[dbo].['+@ind_name+']')) )
		  BEGIN

			  SET @print_end = 'Y'

			  INSERT INTO @CommentText ([Text]) VALUES('

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']''))
  DROP VIEW [dbo].['+@ind_name+']
GO
 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']''))
BEGIN
  EXEC dbo.sp_executesql @statement = N''

')

		  END
		  ELSE IF( EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].['+@ind_name+']') AND type in ('P', 'PC')))
		  BEGIN

			  SET @print_end = 'Y'

			  INSERT INTO @CommentText ([Text]) VALUES('

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''P'', N''PC'') )
  DROP PROCEDURE [dbo].['+@ind_name+']
GO
 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''P'', N''PC''))
BEGIN
  EXEC dbo.sp_executesql @statement = N''
 
')

		  END
		  ELSE IF( EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].['+@ind_name+']') AND type in ('FN', 'IF', 'TF', 'FS', 'FT')))
		  BEGIN

			  SET @print_end = 'Y'

			  INSERT INTO @CommentText ([Text]) VALUES(' 

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''FN'', N''IF'', N''TF'', N''FS'', N''FT'') )
  DROP FUNCTION [dbo].['+@ind_name+']
GO
 
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''FN'', N''IF'', N''TF'', N''FS'', N''FT''))
BEGIN
  EXEC dbo.sp_executesql @statement = N''
 
')

		  END

		  -------------------------------------------
		  OPEN ms_crs_syscom
		  FETCH NEXT FROM ms_crs_syscom INTO @SyscomText, @LineId
		  WHILE @@FETCH_STATUS >= 0
		  BEGIN

			  INSERT @CommentTextTemp VALUES( @LineId,CASE WHEN @stringencode='Y' THEN REPLACE(@SyscomText, '''', '''''') ELSE @SyscomText END  )

			  FETCH NEXT FROM ms_crs_syscom INTO @SyscomText, @LineId
		  END

		  CLOSE  ms_crs_syscom
		  DEALLOCATE 	ms_crs_syscom

		  SET @lineId = @LineId + 1

		  IF( @print_end = 'Y' )
		  BEGIN

			  INSERT @CommentTextTemp VALUES( @LineId, '''

END

GO')

		  END

		  INSERT INTO @CommentText ([Text]) SELECT Text FROM @CommentTextTemp ORDER BY LineId

	END
		--------------------------------------------
		FETCH next FROM curName INTO @ind_name
	END
	CLOSE curName
	DEALLOCATE curName

  IF(@insert_delete <> 'Y')
  BEGIN

	UPDATE @CommentText 
	SET [Text] = REPLACE([Text], 'CREATE ', 'ALTER ')
	WHERE UPPER([Text]) LIKE '%CREATE %'

--    UPDATE @CommentText 
--    SET [Text] = REPLACE([Text], 'CREATE PROCEDURE [dbo].', 'ALTER PROCEDURE [dbo].')
--    WHERE UPPER([Text]) LIKE '%CREATE PROCEDURE [[]dbo].%'
--
--    UPDATE @CommentText 
--    SET [Text] = REPLACE([Text], 'CREATE PROC [dbo].', 'ALTER PROC [dbo].')
--    WHERE UPPER([Text]) LIKE '%CREATE PROC [[]dbo].%'
--
--    UPDATE @CommentText 
--    SET [Text] = REPLACE([Text], 'CREATE VIEW [dbo].', 'ALTER VIEW [dbo].')
--    WHERE UPPER([Text]) LIKE '%CREATE VIEW [[]dbo].%'
--
--    UPDATE @CommentText 
--    SET [Text] = REPLACE([Text], 'CREATE FUNCTION [dbo].', 'ALTER FUNCTION [dbo].')
--    WHERE UPPER([Text]) LIKE '%CREATE FUNCTION [[]dbo].%'

  END

	SELECT [Text] FROM @CommentText ORDER BY LineId

END

";
		#region Commented Code
		//       private static string strSqlCreateStoredProcScript = @"
		//
		//-- =============================================
		//-- Author:		  PRITAM SHARMA
		//-- Create date: 01/20/2009
		//-- Description:	For Generating script
		//--              @name --> Name of stored procedure functions and views in comma seperated format
		//-- =============================================
		//
		//DECLARE @name   VARCHAR(MAX) -- Comma seperated format
		//
		//SELECT @name = '§1'
		//
		//BEGIN
		//SET NOCOUNT ON
		//
		//	SET @name = REPLACE(@name,CHAR(13)+CHAR(10),' ')
		//	SET @name = REPLACE(@name,'
		//','')
		//
		//	DECLARE
		//	@ind_name VARCHAR(100)
		//
		//	DECLARE @CommentText 
		//	TABLE (
		//	[LineId]	INT IDENTITY(1,1) NOT NULL,
		//	[Text]    VARCHAR(8000) COLLATE database_default)
		//
		//	INSERT INTO @CommentText ([Text]) VALUES('--SCRIPT DATE: '+CONVERT(VARCHAR(30),GETDATE())+'   --
		//')
		//
		//	DECLARE curName CURSOR FOR
		//	SELECT [value] FROM dbo.fn_Split(@name,',')
		//
		//	OPEN curName
		//	FETCH next FROM curName INTO @ind_name
		//	WHILE(@@FETCH_STATUS = 0)	
		//	BEGIN
		//
		//    print @ind_name
		//
		//		DECLARE 
		//		@objid	    INT
		//
		//		SELECT @objid = OBJECT_ID(@ind_name)
		//
		//    print @objid
		//
		//    IF(@objid < 0)
		//    BEGIN
		//
		//      IF( (SELECT COUNT(1) FROM master.sys.syscomments
		//           WHERE id = @objid )>0)
		//      BEGIN
		//
		//        INSERT INTO @CommentText 
		//        SELECT [text] 
		//        FROM  master.sys.syscomments 
		//			  WHERE id = @objid
		//			  ORDER BY number, colid 
		//
		//      END
		//    END
		//    ELSE
		//    BEGIN
		//
		//      IF( (SELECT COUNT(1) FROM syscomments 
		//			     WHERE id = @objid 
		//			     AND encrypted = 0 )>0)
		//      BEGIN
		//    
		//        INSERT INTO @CommentText 
		//        SELECT [text] 
		//        FROM syscomments 
		//			  WHERE id = @objid 
		//			  AND encrypted = 0
		//			  ORDER BY number, colid
		//        
		//      END
		//
		//    END
		//		
		//		--------------------------------------------
		//		FETCH next FROM curName INTO @ind_name
		//	END
		//	CLOSE curName
		//	DEALLOCATE curName
		//
		//	SELECT [Text] FROM @CommentText ORDER BY LineId
		//
		//END
		//
		//";

		//       private static string strSqlCreateStoredProcScript = @"
		//-- =============================================
		//-- Author:		  PRITAM SHARMA
		//-- Create date: 01/20/2009
		//-- Description:	For Generating script
		//--              @name --> Name of stored procedure functions and views in comma seperated format
		//-- =============================================
		//
		//DECLARE @name   VARCHAR(MAX) -- Comma seperated format
		//
		//SELECT @name = '§1'
		//
		//BEGIN
		//SET NOCOUNT ON
		//
		//	SET @name = REPLACE(@name,CHAR(13)+CHAR(10),' ')
		//	SET @name = REPLACE(@name,'
		//','')
		//
		//	DECLARE
		//	@ind_name VARCHAR(100)
		//
		//	DECLARE @CommentText 
		//	TABLE (
		//	[LineId]	INT IDENTITY(1,1) NOT NULL,
		//	[Text]    VARCHAR(8000) COLLATE database_default)
		//
		//	INSERT INTO @CommentText ([Text]) VALUES('--SCRIPT DATE: '+CONVERT(VARCHAR(30),GETDATE())+'   --
		//')
		//
		//	DECLARE curName CURSOR FOR
		//	SELECT [value] FROM dbo.fn_Split(@name,',')
		//
		//	OPEN curName
		//	FETCH next FROM curName INTO @ind_name
		//	WHILE(@@FETCH_STATUS = 0)	
		//	BEGIN
		//
		//		DECLARE 
		//		@objname      VARCHAR(1000),
		//		@stringencode CHAR(1),
		//		@print_end    CHAR(1)
		//
		//		SELECT 
		//		@objname      = @ind_name,
		//		@stringencode = 'Y',
		//		@print_end    = 'N'
		//
		//		DECLARE @CommentTextTemp 
		//		TABLE (
		//		[LineId] INT,
		//		[Text]   VARCHAR(8000) COLLATE database_default )
		//
		//		DECLARE 
		//		@objid	    INT,
		//		@LineId     INT,
		//		@SyscomText	VARCHAR(8000)
		//
		//		DELETE FROM @CommentTextTemp
		//
		//		SELECT @objid = OBJECT_ID(@objname)
		//
		//		IF @objid < 0	-- Handle system-objects
		//		BEGIN
		//
		//			DECLARE ms_crs_syscom CURSOR LOCAL 
		//			FOR SELECT [text],[colid] FROM master.sys.syscomments 
		//			WHERE id = @objid
		//			ORDER BY number, colid 
		//			FOR READ ONLY
		//
		//		END
		//		ELSE
		//		BEGIN
		//
		//			DECLARE ms_crs_syscom  CURSOR LOCAL
		//			FOR SELECT [text],[colid] FROM syscomments 
		//			WHERE id = @objid 
		//			AND encrypted = 0
		//			ORDER BY number, colid
		//			FOR READ ONLY
		//
		//		END
		//
		//		IF( EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID('[dbo].['+@ind_name+']')) )
		//		BEGIN
		//
		//			SET @print_end = 'Y'
		//
		//			INSERT INTO @CommentText ([Text]) VALUES('
		//
		//IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']''))
		//	DROP VIEW [dbo].['+@ind_name+']
		//GO
		// 
		//SET ANSI_NULLS ON
		//GO
		//SET QUOTED_IDENTIFIER ON
		//GO
		//
		//IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']''))
		//BEGIN
		//	EXEC dbo.sp_executesql @statement = N''
		//
		//')
		//
		//		END
		//		ELSE IF( EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].['+@ind_name+']') AND type in ('P', 'PC')))
		//		BEGIN
		//
		//			SET @print_end = 'Y'
		//
		//			INSERT INTO @CommentText ([Text]) VALUES('
		//
		//IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''P'', N''PC'') )
		//	DROP PROCEDURE [dbo].['+@ind_name+']
		//GO
		// 
		//SET ANSI_NULLS ON
		//GO
		//SET QUOTED_IDENTIFIER ON
		//GO
		//
		//IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''P'', N''PC''))
		//BEGIN
		//	EXEC dbo.sp_executesql @statement = N''
		// 
		//')
		//
		//		END
		//		ELSE IF( EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].['+@ind_name+']') AND type in ('FN', 'IF', 'TF', 'FS', 'FT')))
		//		BEGIN
		//
		//			SET @print_end = 'Y'
		//
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//
		//IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''FN'', N''IF'', N''TF'', N''FS'', N''FT'') )
		//	DROP FUNCTION [dbo].['+@ind_name+']
		//GO
		// 
		//SET ANSI_NULLS ON
		//GO
		//SET QUOTED_IDENTIFIER ON
		//GO
		//
		//IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''FN'', N''IF'', N''TF'', N''FS'', N''FT''))
		//BEGIN
		//	EXEC dbo.sp_executesql @statement = N''
		// 
		//')
		//
		//		END
		//
		//		-------------------------------------------
		//		OPEN ms_crs_syscom
		//		FETCH NEXT FROM ms_crs_syscom INTO @SyscomText, @LineId
		//		WHILE @@FETCH_STATUS >= 0
		//		BEGIN
		//
		//			INSERT @CommentTextTemp VALUES( @LineId,CASE WHEN @stringencode='Y' THEN REPLACE(@SyscomText, '''', '''''') ELSE @SyscomText END  )
		//
		//			FETCH NEXT FROM ms_crs_syscom INTO @SyscomText, @LineId
		//		END
		//
		//		CLOSE  ms_crs_syscom
		//		DEALLOCATE 	ms_crs_syscom
		//
		//		SET @lineId = @LineId + 1
		//
		//		IF( @print_end = 'Y' )
		//		BEGIN
		//
		//			INSERT @CommentTextTemp VALUES( @LineId, '''
		//
		//END
		//
		//GO')
		//
		//		END
		//
		//		INSERT INTO @CommentText ([Text]) SELECT Text FROM @CommentTextTemp ORDER BY LineId
		//		
		//		--------------------------------------------
		//		FETCH next FROM curName INTO @ind_name
		//	END
		//	CLOSE curName
		//	DEALLOCATE curName
		//
		//	SELECT [Text] FROM @CommentText ORDER BY LineId
		//
		//END
		//";


		//        private static string strSqlCreateStoredProcScript = @"
		//-- =============================================
		//-- Author:		  PRITAM SHARMA
		//-- Create date: 01/20/2009
		//-- Description:	For Generating script
		//--              @name --> Name of stored procedure functions and views in comma seperated format
		//-- =============================================
		//
		//DECLARE @name         VARCHAR(MAX) -- Comma seperated format
		//
		//SELECT @name = '§1'
		//
		//BEGIN
		//SET NOCOUNT ON
		//
		//	SET @name = REPLACE(@name,CHAR(13)+CHAR(10),' ')
		//	SET @name = REPLACE(@name,'
		//','')
		//
		//	DECLARE
		//	@ind_name VARCHAR(100)
		//
		//	IF(@name IS NULL OR LEN(@name) =0)
		//		PRINT 'Please pass comma seperated List for generating script'
		//
		//	DECLARE @CommentText TABLE (LineId	INT IDENTITY(1,1) NOT NULL,[Text] NVARCHAR(1000) COLLATE database_default)
		//
		//	INSERT INTO @CommentText ([Text]) VALUES('--SCRIPT DATE: '+CONVERT(VARCHAR(30),GETDATE())+'   --
		//')
		//
		//	PRINT '--SCRIPT DATE: '+CONVERT(VARCHAR(30),GETDATE())+'   --'
		//
		//	DECLARE curName CURSOR FOR
		//	SELECT value FROM dbo.fn_Split(@name,',')
		//
		//	OPEN curName
		//	FETCH next FROM curName INTO @ind_name
		//	WHILE(@@FETCH_STATUS = 0)	
		//	BEGIN
		//
		//		IF( EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID('[dbo].['+@ind_name+']')) )
		//		BEGIN
		//
		//			INSERT INTO @CommentText ([Text]) VALUES('
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']''))
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('	DROP VIEW [dbo].['+@ind_name+']
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('SET ANSI_NULLS ON
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('SET QUOTED_IDENTIFIER ON
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']''))
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('BEGIN
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('	EXEC dbo.sp_executesql @statement = N''
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//
		//---------------------------------------------------------
		//
		//
		//			 DECLARE @objname      NVARCHAR(776)
		//			,@stringencode CHAR(1)
		//			,@columnname   SYSNAME
		//
		//			SELECT @objname  = @ind_name
		//			,@stringencode = 'Y'
		//			,@columnname  = NULL
		//
		//			DECLARE @CommentTextTemp TABLE (LineId	INT,Text NVARCHAR(1000) COLLATE database_default)
		//			BEGIN
		//
		//				DECLARE 
		//				 @dbname          SYSNAME
		//				,@objid	          INT
		//				,@BlankSpaceAdded INT
		//				,@BasePos         INT
		//				,@CurrentPos      INT
		//				,@TextLength      INT
		//				,@LineId          INT
		//				,@AddOnLen        INT
		//				,@LFCR            INT --lengths of line feed carriage RETURN
		//				,@DefinedLength   INT
		//				/* NOTE: Length of @SyscomText IS 4000 to replace the length of
		//				** text column in syscomments.
		//				** lengths ON @Line, #CommentText Text column AND
		//				** value for @DefinedLength are all 255. These need to all have
		//				** the same values. 255 was selected in ORDER for the max length
		//				** display using down level clients*/
		//				,@SyscomText	    NVARCHAR(4000)
		//				,@Line            NVARCHAR(1000)--(255)
		//
		//				SELECT 
		//				@DefinedLength   = 1000,--255,
		//				@BlankSpaceAdded = 0 --Keeps track of blank spaces at END of lines. Note Len function ignores trailing blank spaces
		//
		//                DELETE FROM @CommentTextTemp
		//
		//				--  Make sure the @objname IS local to the current database.
		//				SELECT @dbname = PARSENAME(@objname,3)
		//
		//				IF @dbname IS NULL
		//				BEGIN
		//
		//					SELECT @dbname = DB_NAME()
		//
		//				END
		//				ELSE IF @dbname <> DB_NAME()
		//				BEGIN
		//
		//					--RAISERROR(15250,-1,-1)
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//				END
		//
		//				--  See IF @objname exists.
		//				SELECT @objid = OBJECT_ID(@objname)
		//
		//				IF (@objid IS NULL)
		//				BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//				END
		//
		//				-- IF second parameter was given.
		//				IF ( @columnname IS NOT NULL)
		//				BEGIN
		//
		//					-- Check IF it IS a table
		//					IF (SELECT COUNT(*) FROM sys.objects WHERE OBJECT_ID = @objid AND TYPE in ('S ','U ','TF'))=0
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					-- check IF it IS a correct column name
		//					IF ((SELECT 'count'=COUNT(*) FROM sys.columns WHERE name = @columnname AND OBJECT_ID = @objid) =0)
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					IF (COLUMNPROPERTY(@objid, @columnname, 'IsComputed') = 0)
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					DECLARE ms_crs_syscom  CURSOR LOCAL
		//					FOR SELECT text FROM syscomments 
		//					WHERE id = @objid 
		//					AND encrypted = 0 
		//					AND number = (SELECT column_id FROM sys.columns 
		//												WHERE name = @columnname 
		//												AND OBJECT_ID = @objid)
		//					ORDER BY number,colid 
		//					FOR READ ONLY
		//
		//				END
		//				ELSE IF @objid < 0	-- Handle system-objects
		//				BEGIN
		//
		//					-- Check COUNT of rows with text data
		//					IF (SELECT COUNT(*) FROM master.sys.syscomments WHERE id = @objid AND text IS NOT NULL) = 0
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					DECLARE ms_crs_syscom CURSOR LOCAL 
		//					FOR SELECT text FROM master.sys.syscomments 
		//					WHERE id = @objid
		//					ORDER BY number, colid 
		//					FOR READ ONLY
		//
		//				END
		//				ELSE
		//				BEGIN
		//
		//					/*  Find out how many lines of text are coming back,
		//					**  AND RETURN IF there are none.*/
		//					IF( (SELECT COUNT(*) FROM syscomments c, sysobjects o WHERE o.xtype NOT in ('S', 'U')
		//							AND o.id = c.id AND o.id = @objid) = 0 )
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					IF (SELECT COUNT(*) FROM syscomments WHERE id = @objid AND encrypted = 0) = 0
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					DECLARE ms_crs_syscom  CURSOR LOCAL
		//					FOR SELECT text FROM syscomments 
		//					WHERE id = @objid 
		//					AND encrypted = 0
		//					ORDER BY number, colid
		//					FOR READ ONLY
		//
		//				END
		//				--  ELSE get the text.
		//
		//				SELECT @LFCR = 2
		//				SELECT @LineId = 1
		//
		//				--Opens the cursor
		//				OPEN ms_crs_syscom
		//				FETCH NEXT FROM ms_crs_syscom INTO @SyscomText
		//				WHILE @@FETCH_STATUS >= 0
		//				BEGIN
		//
		//					SELECT  @BasePos    = 1
		//					SELECT  @CurrentPos = 1
		//					SELECT  @TextLength = LEN(@SyscomText)
		//
		//					WHILE @CurrentPos  != 0
		//					BEGIN
		//
		//						--Looking for END of line followed BY carriage RETURN
		//						SELECT @CurrentPos =   CHARINDEX(CHAR(13)+CHAR(10), @SyscomText, @BasePos)
		//
		//								--IF carriage RETURN found
		//						IF @CurrentPos != 0
		//						BEGIN
		//
		//							/*IF new value for @Lines length will be > then the
		//							**SET length then insert current contents of @line
		//							**AND proceed.
		//							*/
		//							WHILE (ISNULL(LEN(@Line),0) + @BlankSpaceAdded + @CurrentPos-@BasePos + @LFCR) > @DefinedLength
		//							BEGIN
		//
		//									SELECT @AddOnLen = @DefinedLength-(ISNULL(LEN(@Line),0) + @BlankSpaceAdded)
		//
		//									INSERT @CommentTextTemp VALUES
		//									( @LineId,
		//										ISNULL(@Line, N'') + ISNULL(SUBSTRING(CASE WHEN @stringencode='Y' THEN REPLACE(@SyscomText, '''', '''''') ELSE @SyscomText END, @BasePos, @AddOnLen), N''))
		//
		//									SELECT @Line = NULL, @LineId = @LineId + 1,
		//												 @BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
		//
		//							END
		//
		//							SELECT @Line    = ISNULL(@Line, N'') + ISNULL(SUBSTRING(@SyscomText, @BasePos, @CurrentPos-@BasePos + @LFCR), N'')
		//
		//							SELECT @BasePos = @CurrentPos+2
		//
		//							INSERT @CommentTextTemp VALUES( @LineId,CASE WHEN @stringencode='Y' THEN REPLACE(@Line, '''', '''''') ELSE @Line END  )
		//
		//							SELECT @LineId = @LineId + 1
		//
		//							SELECT @Line = NULL
		//
		//						END
		//						ELSE
		//								--ELSE carriage RETURN NOT found
		//						BEGIN
		//
		//							IF @BasePos <= @TextLength
		//							BEGIN
		//								/*IF new value for @Lines length will be > then the
		//								**defined length
		//								*/
		//								WHILE (ISNULL(LEN(@Line),0) + @BlankSpaceAdded + @TextLength-@BasePos+1 ) > @DefinedLength
		//								BEGIN
		//
		//									SELECT @AddOnLen = @DefinedLength - (ISNULL(LEN(@Line),0) + @BlankSpaceAdded)
		//
		//									INSERT @CommentTextTemp VALUES
		//									( @LineId,
		//										ISNULL(@Line, N'') + ISNULL(SUBSTRING(CASE WHEN @stringencode='Y' THEN REPLACE(@SyscomText, '''', '''''') ELSE @SyscomText END , @BasePos, @AddOnLen), N''))
		//
		//									SELECT @Line = NULL, @LineId = @LineId + 1,
		//											@BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
		//
		//								END
		//
		//								SELECT @Line = ISNULL(@Line, N'') + ISNULL(SUBSTRING(@SyscomText, @BasePos, @TextLength-@BasePos+1 ), N'')
		//
		//								IF LEN(@Line) < @DefinedLength AND charindex(' ', @SyscomText, @TextLength+1 ) > 0
		//								BEGIN
		//
		//									SELECT @Line = @Line + ' ', @BlankSpaceAdded = 1
		//
		//								END
		//
		//							END
		//
		//						END
		//
		//					END
		//
		//					FETCH NEXT FROM ms_crs_syscom INTO @SyscomText
		//				END
		//
		//				IF @Line IS NOT NULL
		//						INSERT @CommentTextTemp VALUES( @LineId,CASE WHEN @stringencode='Y' THEN REPLACE(@Line, '''', '''''') ELSE @Line END  )
		//
		//				CLOSE  ms_crs_syscom
		//				DEALLOCATE 	ms_crs_syscom
		//
		//				INSERT INTO @CommentText ([Text]) SELECT Text FROM @CommentTextTemp ORDER BY LineId
		//
		//			END
		//---------------------------------------------------------
		//
		//--			INSERT INTO @CommentText ([Text]) SELECT Text FROM dbo.f_pritam_test_create_script(@ind_name,'Y',NULL) ORDER BY LineId
		//			INSERT INTO @CommentText ([Text]) VALUES('''
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('END
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//
		//			PRINT '
		//
		//IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']''))
		//	DROP VIEW [dbo].['+@ind_name+']
		//GO
		//
		//'
		//			PRINT '
		//SET ANSI_NULLS ON
		//GO
		//SET QUOTED_IDENTIFIER ON
		//GO
		//IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']''))
		//BEGIN
		//	EXEC dbo.sp_executesql @statement = N''
		//'
		//			EXEC sp_script_genereate @ind_name, 'Y'
		//			PRINT '''
		//END
		//
		//GO
		//'
		//
		//		END
		//		ELSE IF( EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].['+@ind_name+']') AND type in ('P', 'PC')))
		//		BEGIN
		//
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''P'', N''PC'') )
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('	DROP PROCEDURE [dbo].['+@ind_name+']
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('SET ANSI_NULLS ON
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('SET QUOTED_IDENTIFIER ON
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''P'', N''PC''))
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('BEGIN
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('	EXEC dbo.sp_executesql @statement = N''
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//
		//------------------------------------------------------------
		//
		//			SELECT @objname  = @ind_name
		//			,@stringencode = 'Y'
		//			,@columnname  = NULL
		//
		//			DELETE FROM @CommentTextTemp
		//
		//--			DECLARE @CommentTextTemp TABLE (LineId	INT,Text NVARCHAR(1000) COLLATE database_default)
		//			BEGIN
		//
		//				SELECT 
		//--			 @dbname          SYSNAME
		//		     @objid	          = NULL
		//				,@BlankSpaceAdded = NULL
		//				,@BasePos         = NULL
		//				,@CurrentPos      = NULL
		//				,@TextLength      = NULL
		//				,@LineId          = NULL
		//				,@AddOnLen        = NULL
		//				,@LFCR            = NULL
		//				,@DefinedLength   = NULL
		//				,@SyscomText	    = NULL
		//				,@Line            = NULL
		//
		//				SELECT 
		//				@DefinedLength   = 1000,--255,
		//				@BlankSpaceAdded = 0 --Keeps track of blank spaces at END of lines. Note Len function ignores trailing blank spaces
		//
		//				--  Make sure the @objname IS local to the current database.
		//				SELECT @dbname = PARSENAME(@objname,3)
		//
		//				IF @dbname IS NULL
		//				BEGIN
		//
		//					SELECT @dbname = DB_NAME()
		//
		//				END
		//				ELSE IF @dbname <> DB_NAME()
		//				BEGIN
		//
		//					--RAISERROR(15250,-1,-1)
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//				END
		//
		//				--  See IF @objname exists.
		//				SELECT @objid = OBJECT_ID(@objname)
		//
		//				IF (@objid IS NULL)
		//				BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//				END
		//
		//				-- IF second parameter was given.
		//				IF ( @columnname IS NOT NULL)
		//				BEGIN
		//
		//					-- Check IF it IS a table
		//					IF (SELECT COUNT(*) FROM sys.objects WHERE OBJECT_ID = @objid AND TYPE in ('S ','U ','TF'))=0
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					-- check IF it IS a correct column name
		//					IF ((SELECT 'count'=COUNT(*) FROM sys.columns WHERE name = @columnname AND OBJECT_ID = @objid) =0)
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					IF (COLUMNPROPERTY(@objid, @columnname, 'IsComputed') = 0)
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					DECLARE ms_crs_syscom  CURSOR LOCAL
		//					FOR SELECT text FROM syscomments 
		//					WHERE id = @objid 
		//					AND encrypted = 0 
		//					AND number = (SELECT column_id FROM sys.columns 
		//												WHERE name = @columnname 
		//												AND OBJECT_ID = @objid)
		//					ORDER BY number,colid 
		//					FOR READ ONLY
		//
		//				END
		//				ELSE IF @objid < 0	-- Handle system-objects
		//				BEGIN
		//
		//					-- Check COUNT of rows with text data
		//					IF (SELECT COUNT(*) FROM master.sys.syscomments WHERE id = @objid AND text IS NOT NULL) = 0
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					DECLARE ms_crs_syscom CURSOR LOCAL 
		//					FOR SELECT text FROM master.sys.syscomments 
		//					WHERE id = @objid
		//					ORDER BY number, colid 
		//					FOR READ ONLY
		//
		//				END
		//				ELSE
		//				BEGIN
		//
		//					/*  Find out how many lines of text are coming back,
		//					**  AND RETURN IF there are none.*/
		//					IF( (SELECT COUNT(*) FROM syscomments c, sysobjects o WHERE o.xtype NOT in ('S', 'U')
		//							AND o.id = c.id AND o.id = @objid) = 0 )
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					IF (SELECT COUNT(*) FROM syscomments WHERE id = @objid AND encrypted = 0) = 0
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					DECLARE ms_crs_syscom  CURSOR LOCAL
		//					FOR SELECT text FROM syscomments 
		//					WHERE id = @objid 
		//					AND encrypted = 0
		//					ORDER BY number, colid
		//					FOR READ ONLY
		//
		//				END
		//				--  ELSE get the text.
		//
		//				SELECT @LFCR = 2
		//				SELECT @LineId = 1
		//
		//				--Opens the cursor
		//				OPEN ms_crs_syscom
		//				FETCH NEXT FROM ms_crs_syscom INTO @SyscomText
		//				WHILE @@FETCH_STATUS >= 0
		//				BEGIN
		//
		//					SELECT  @BasePos    = 1
		//					SELECT  @CurrentPos = 1
		//					SELECT  @TextLength = LEN(@SyscomText)
		//
		//					WHILE @CurrentPos  != 0
		//					BEGIN
		//
		//						--Looking for END of line followed BY carriage RETURN
		//						SELECT @CurrentPos =   CHARINDEX(CHAR(13)+CHAR(10), @SyscomText, @BasePos)
		//
		//								--IF carriage RETURN found
		//						IF @CurrentPos != 0
		//						BEGIN
		//
		//							/*IF new value for @Lines length will be > then the
		//							**SET length then insert current contents of @line
		//							**AND proceed.
		//							*/
		//							WHILE (ISNULL(LEN(@Line),0) + @BlankSpaceAdded + @CurrentPos-@BasePos + @LFCR) > @DefinedLength
		//							BEGIN
		//
		//									SELECT @AddOnLen = @DefinedLength-(ISNULL(LEN(@Line),0) + @BlankSpaceAdded)
		//
		//									INSERT @CommentTextTemp VALUES
		//									( @LineId,
		//										ISNULL(@Line, N'') + ISNULL(SUBSTRING(CASE WHEN @stringencode='Y' THEN REPLACE(@SyscomText, '''', '''''') ELSE @SyscomText END, @BasePos, @AddOnLen), N''))
		//
		//									SELECT @Line = NULL, @LineId = @LineId + 1,
		//												 @BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
		//
		//							END
		//
		//							SELECT @Line    = ISNULL(@Line, N'') + ISNULL(SUBSTRING(@SyscomText, @BasePos, @CurrentPos-@BasePos + @LFCR), N'')
		//
		//							SELECT @BasePos = @CurrentPos+2
		//
		//							INSERT @CommentTextTemp VALUES( @LineId,CASE WHEN @stringencode='Y' THEN REPLACE(@Line, '''', '''''') ELSE @Line END  )
		//
		//							SELECT @LineId = @LineId + 1
		//
		//							SELECT @Line = NULL
		//
		//						END
		//						ELSE
		//								--ELSE carriage RETURN NOT found
		//						BEGIN
		//
		//							IF @BasePos <= @TextLength
		//							BEGIN
		//								/*IF new value for @Lines length will be > then the
		//								**defined length
		//								*/
		//								WHILE (ISNULL(LEN(@Line),0) + @BlankSpaceAdded + @TextLength-@BasePos+1 ) > @DefinedLength
		//								BEGIN
		//
		//									SELECT @AddOnLen = @DefinedLength - (ISNULL(LEN(@Line),0) + @BlankSpaceAdded)
		//
		//									INSERT @CommentTextTemp VALUES
		//									( @LineId,
		//										ISNULL(@Line, N'') + ISNULL(SUBSTRING(CASE WHEN @stringencode='Y' THEN REPLACE(@SyscomText, '''', '''''') ELSE @SyscomText END , @BasePos, @AddOnLen), N''))
		//
		//									SELECT @Line = NULL, @LineId = @LineId + 1,
		//											@BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
		//
		//								END
		//
		//								SELECT @Line = ISNULL(@Line, N'') + ISNULL(SUBSTRING(@SyscomText, @BasePos, @TextLength-@BasePos+1 ), N'')
		//
		//								IF LEN(@Line) < @DefinedLength AND charindex(' ', @SyscomText, @TextLength+1 ) > 0
		//								BEGIN
		//
		//									SELECT @Line = @Line + ' ', @BlankSpaceAdded = 1
		//
		//								END
		//
		//							END
		//
		//						END
		//
		//					END
		//
		//					FETCH NEXT FROM ms_crs_syscom INTO @SyscomText
		//				END
		//
		//				IF @Line IS NOT NULL
		//						INSERT @CommentTextTemp VALUES( @LineId,CASE WHEN @stringencode='Y' THEN REPLACE(@Line, '''', '''''') ELSE @Line END  )
		//
		//				CLOSE  ms_crs_syscom
		//				DEALLOCATE 	ms_crs_syscom
		//
		//				INSERT INTO @CommentText ([Text]) SELECT Text FROM @CommentTextTemp ORDER BY LineId
		//
		//			END
		//-------------------------------------------------------------------------
		//--			INSERT INTO @CommentText ([Text]) SELECT Text FROM dbo.f_pritam_test_create_script(@ind_name,'Y',NULL) ORDER BY LineId
		//			INSERT INTO @CommentText ([Text]) VALUES('''
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('END
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//
		//
		//			PRINT '
		//
		//IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''P'', N''PC'') )
		//	DROP PROCEDURE [dbo].['+@ind_name+']
		//GO
		//
		//'
		//			PRINT '
		//SET ANSI_NULLS ON
		//GO
		//SET QUOTED_IDENTIFIER ON
		//GO
		//IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''P'', N''PC''))
		//BEGIN
		//	EXEC dbo.sp_executesql @statement = N''
		//'
		//
		//			EXEC sp_script_genereate @ind_name, 'Y'
		//			PRINT '''
		//END
		//
		//GO
		//'
		//
		//		END
		//		ELSE IF( EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].['+@ind_name+']') AND type in ('FN', 'IF', 'TF', 'FS', 'FT')))
		//		BEGIN
		//
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''FN'', N''IF'', N''TF'', N''FS'', N''FT'') )
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('		DROP FUNCTION [dbo].['+@ind_name+']
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('SET ANSI_NULLS ON
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('SET QUOTED_IDENTIFIER ON
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''FN'', N''IF'', N''TF'', N''FS'', N''FT''))
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('BEGIN
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('	EXEC dbo.sp_executesql @statement = N''
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//
		//------------------------------------------------------------
		//
		//			SELECT @objname  = @ind_name
		//			,@stringencode = 'Y'
		//			,@columnname  = NULL
		//
		//			DELETE FROM @CommentTextTemp
		//
		//--			DECLARE @CommentTextTemp TABLE (LineId	INT,Text NVARCHAR(1000) COLLATE database_default)
		//			BEGIN
		//
		//				SELECT 
		//--			 @dbname          SYSNAME
		//		     @objid	          = NULL
		//				,@BlankSpaceAdded = NULL
		//				,@BasePos         = NULL
		//				,@CurrentPos      = NULL
		//				,@TextLength      = NULL
		//				,@LineId          = NULL
		//				,@AddOnLen        = NULL
		//				,@LFCR            = NULL
		//				,@DefinedLength   = NULL
		//				,@SyscomText	    = NULL
		//				,@Line            = NULL
		//
		//				SELECT 
		//				@DefinedLength   = 1000,--255,
		//				@BlankSpaceAdded = 0 --Keeps track of blank spaces at END of lines. Note Len function ignores trailing blank spaces
		//
		//				--  Make sure the @objname IS local to the current database.
		//				SELECT @dbname = PARSENAME(@objname,3)
		//
		//				IF @dbname IS NULL
		//				BEGIN
		//
		//					SELECT @dbname = DB_NAME()
		//
		//				END
		//				ELSE IF @dbname <> DB_NAME()
		//				BEGIN
		//
		//					--RAISERROR(15250,-1,-1)
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//				END
		//
		//				--  See IF @objname exists.
		//				SELECT @objid = OBJECT_ID(@objname)
		//
		//				IF (@objid IS NULL)
		//				BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//				END
		//
		//				-- IF second parameter was given.
		//				IF ( @columnname IS NOT NULL)
		//				BEGIN
		//
		//					-- Check IF it IS a table
		//					IF (SELECT COUNT(*) FROM sys.objects WHERE OBJECT_ID = @objid AND TYPE in ('S ','U ','TF'))=0
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					-- check IF it IS a correct column name
		//					IF ((SELECT 'count'=COUNT(*) FROM sys.columns WHERE name = @columnname AND OBJECT_ID = @objid) =0)
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					IF (COLUMNPROPERTY(@objid, @columnname, 'IsComputed') = 0)
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					DECLARE ms_crs_syscom  CURSOR LOCAL
		//					FOR SELECT text FROM syscomments 
		//					WHERE id = @objid 
		//					AND encrypted = 0 
		//					AND number = (SELECT column_id FROM sys.columns 
		//												WHERE name = @columnname 
		//												AND OBJECT_ID = @objid)
		//					ORDER BY number,colid 
		//					FOR READ ONLY
		//
		//				END
		//				ELSE IF @objid < 0	-- Handle system-objects
		//				BEGIN
		//
		//					-- Check COUNT of rows with text data
		//					IF (SELECT COUNT(*) FROM master.sys.syscomments WHERE id = @objid AND text IS NOT NULL) = 0
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					DECLARE ms_crs_syscom CURSOR LOCAL 
		//					FOR SELECT text FROM master.sys.syscomments 
		//					WHERE id = @objid
		//					ORDER BY number, colid 
		//					FOR READ ONLY
		//
		//				END
		//				ELSE
		//				BEGIN
		//
		//					/*  Find out how many lines of text are coming back,
		//					**  AND RETURN IF there are none.*/
		//					IF( (SELECT COUNT(*) FROM syscomments c, sysobjects o WHERE o.xtype NOT in ('S', 'U')
		//							AND o.id = c.id AND o.id = @objid) = 0 )
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					IF (SELECT COUNT(*) FROM syscomments WHERE id = @objid AND encrypted = 0) = 0
		//					BEGIN
		//
		//					INSERT @CommentTextTemp VALUES( 0, 'Error' )
		//
		//					END
		//
		//					DECLARE ms_crs_syscom  CURSOR LOCAL
		//					FOR SELECT text FROM syscomments 
		//					WHERE id = @objid 
		//					AND encrypted = 0
		//					ORDER BY number, colid
		//					FOR READ ONLY
		//
		//				END
		//				--  ELSE get the text.
		//
		//				SELECT @LFCR = 2
		//				SELECT @LineId = 1
		//
		//				--Opens the cursor
		//				OPEN ms_crs_syscom
		//				FETCH NEXT FROM ms_crs_syscom INTO @SyscomText
		//				WHILE @@FETCH_STATUS >= 0
		//				BEGIN
		//
		//					SELECT  @BasePos    = 1
		//					SELECT  @CurrentPos = 1
		//					SELECT  @TextLength = LEN(@SyscomText)
		//
		//					WHILE @CurrentPos  != 0
		//					BEGIN
		//
		//						--Looking for END of line followed BY carriage RETURN
		//						SELECT @CurrentPos =   CHARINDEX(CHAR(13)+CHAR(10), @SyscomText, @BasePos)
		//
		//								--IF carriage RETURN found
		//						IF @CurrentPos != 0
		//						BEGIN
		//
		//							/*IF new value for @Lines length will be > then the
		//							**SET length then insert current contents of @line
		//							**AND proceed.
		//							*/
		//							WHILE (ISNULL(LEN(@Line),0) + @BlankSpaceAdded + @CurrentPos-@BasePos + @LFCR) > @DefinedLength
		//							BEGIN
		//
		//									SELECT @AddOnLen = @DefinedLength-(ISNULL(LEN(@Line),0) + @BlankSpaceAdded)
		//
		//									INSERT @CommentTextTemp VALUES
		//									( @LineId,
		//										ISNULL(@Line, N'') + ISNULL(SUBSTRING(CASE WHEN @stringencode='Y' THEN REPLACE(@SyscomText, '''', '''''') ELSE @SyscomText END, @BasePos, @AddOnLen), N''))
		//
		//									SELECT @Line = NULL, @LineId = @LineId + 1,
		//												 @BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
		//
		//							END
		//
		//							SELECT @Line    = ISNULL(@Line, N'') + ISNULL(SUBSTRING(@SyscomText, @BasePos, @CurrentPos-@BasePos + @LFCR), N'')
		//
		//							SELECT @BasePos = @CurrentPos+2
		//
		//							INSERT @CommentTextTemp VALUES( @LineId,CASE WHEN @stringencode='Y' THEN REPLACE(@Line, '''', '''''') ELSE @Line END  )
		//
		//							SELECT @LineId = @LineId + 1
		//
		//							SELECT @Line = NULL
		//
		//						END
		//						ELSE
		//								--ELSE carriage RETURN NOT found
		//						BEGIN
		//
		//							IF @BasePos <= @TextLength
		//							BEGIN
		//								/*IF new value for @Lines length will be > then the
		//								**defined length
		//								*/
		//								WHILE (ISNULL(LEN(@Line),0) + @BlankSpaceAdded + @TextLength-@BasePos+1 ) > @DefinedLength
		//								BEGIN
		//
		//									SELECT @AddOnLen = @DefinedLength - (ISNULL(LEN(@Line),0) + @BlankSpaceAdded)
		//
		//									INSERT @CommentTextTemp VALUES
		//									( @LineId,
		//										ISNULL(@Line, N'') + ISNULL(SUBSTRING(CASE WHEN @stringencode='Y' THEN REPLACE(@SyscomText, '''', '''''') ELSE @SyscomText END , @BasePos, @AddOnLen), N''))
		//
		//									SELECT @Line = NULL, @LineId = @LineId + 1,
		//											@BasePos = @BasePos + @AddOnLen, @BlankSpaceAdded = 0
		//
		//								END
		//
		//								SELECT @Line = ISNULL(@Line, N'') + ISNULL(SUBSTRING(@SyscomText, @BasePos, @TextLength-@BasePos+1 ), N'')
		//
		//								IF LEN(@Line) < @DefinedLength AND charindex(' ', @SyscomText, @TextLength+1 ) > 0
		//								BEGIN
		//
		//									SELECT @Line = @Line + ' ', @BlankSpaceAdded = 1
		//
		//								END
		//
		//							END
		//
		//						END
		//
		//					END
		//
		//					FETCH NEXT FROM ms_crs_syscom INTO @SyscomText
		//				END
		//
		//				IF @Line IS NOT NULL
		//						INSERT @CommentTextTemp VALUES( @LineId,CASE WHEN @stringencode='Y' THEN REPLACE(@Line, '''', '''''') ELSE @Line END  )
		//
		//				CLOSE  ms_crs_syscom
		//				DEALLOCATE 	ms_crs_syscom
		//
		//				INSERT INTO @CommentText ([Text]) SELECT Text FROM @CommentTextTemp ORDER BY LineId
		//
		//			END
		//
		//--------------------------------------------------------------
		//
		//
		//--			INSERT INTO @CommentText ([Text]) SELECT Text FROM dbo.f_pritam_test_create_script(@ind_name,'Y',NULL) ORDER BY LineId
		//			INSERT INTO @CommentText ([Text]) VALUES('''
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('END
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES(' 
		//')
		//			INSERT INTO @CommentText ([Text]) VALUES('GO
		//')
		//
		//
		//			PRINT '
		//
		//IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''FN'', N''IF'', N''TF'', N''FS'', N''FT'') )
		//	DROP FUNCTION [dbo].['+@ind_name+']
		//GO
		//
		//'
		//			PRINT'
		//SET ANSI_NULLS ON
		//GO
		//SET QUOTED_IDENTIFIER ON
		//GO
		//IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N''[dbo].['+@ind_name+']'') AND type in (N''FN'', N''IF'', N''TF'', N''FS'', N''FT''))
		//BEGIN
		//	EXECUTE dbo.sp_executesql @statement = N''
		//'
		//			EXEC sp_script_genereate @ind_name, 'Y'
		//			PRINT '''
		//END
		//
		//GO
		//'
		//
		//		END
		//
		//		FETCH next FROM curName INTO @ind_name
		//	END
		//	CLOSE curName
		//	DEALLOCATE curName
		//
		//	SELECT [Text] FROM @CommentText ORDER BY LineId
		//
		//END
		//
		//
		//
		//";
		#endregion Commented Code

		#endregion strSqlCreateStoredProcScript

		#region strSqlCreateInsertStatement

		private static string strSqlCreateInsertStatement = @"
-- ======================================================================================  
-- Author:      Pritam Sharma  
-- Create date: 10th Feb 2009  
-- Description: Creating script for inserting value in any table.  
--              @table_name                 --> Table whose insert script is to be generated  
--              @search_by_column           --> Columns by which data is to be filtered. To be passed in comma seperated format  
--              @search_by_column_value_all --> Values of the of the columns. To be passed in comma seperated format. If multiple select criteria is to be passed it has to be seperated using '|'  
--                       EXAMPLE : 1) With multiple search criteria
--																		EXEC sp_create_insert_script_for_table 'email','email_resource_name','LABOR_TICKET_CANCELLED_NOTIFY_TECH|LABOR_TICKET_HOLD_NOTIFY_TECH|SEHEDULED_LABOR_TICKET_CLOSED|LABOR_TICKET_FOR_OPEN_MAINT_CONTRACT'
--																 2) With single search criteria but multiple search by column
--																	  EXEC sp_create_insert_script_for_table 'app_error_msg','group_cd,error_msg_id','ORDER ENTRY,837|ORDER ENTRY,861|ORDER ENTRY,1025'
--  
--              @delete_required            --> If 'N' delete statement will be added before insert  Default value is 'Y' 
--              @search_by_primary          --> Will search by all primary key if 'Y'  Default value is 'N'
--              @script_entire_table        --> Will generate insert script for the entire table content   Default value is 'N'
--              @include_identity_column    --> Will include identity column in the insert statement if 'Y'
-- ======================================================================================  


DECLARE @table_name         VARCHAR(100),  
@search_by_column           VARCHAR(MAX),  
@search_by_column_value_all VARCHAR(MAX),  
@delete_required            CHAR(1) ,  
@search_by_primary          CHAR(1) ,  
@script_entire_table        CHAR(1) ,
@include_identity_column    CHAR(1) 

SELECT @table_name           = '§1',--'app_error_msg',
@search_by_column            = '§2',
@search_by_column_value_all  = '§3',
@delete_required             = '§4',  
@search_by_primary           = '§5',  
@script_entire_table         = '§6',
@include_identity_column     = '§7'



BEGIN  
 SET NOCOUNT ON  
  
 IF( EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('[dbo].['+@table_name+']') AND [type] = 'U'))  
 BEGIN  

	DECLARE @table_temp TABLE (pri_col INT IDENTITY PRIMARY KEY,script_col VARCHAR(MAX))
	INSERT INTO @table_temp ([script_col]) VALUES('--SCRIPT DATE: '+CONVERT(VARCHAR(30),GETDATE())+'   --
')

	SET @search_by_column = REPLACE(@search_by_column,CHAR(13)+CHAR(10),' ')
	SET @search_by_column_value_all = REPLACE(@search_by_column_value_all,CHAR(13)+CHAR(10),' ')
	SET @search_by_column = REPLACE(@search_by_column,'
','')
	SET @search_by_column_value_all = REPLACE(@search_by_column_value_all,'
','')
	
  
  BEGIN   
  
   DECLARE @search_by_column_value VARCHAR(MAX)  
  
   DECLARE curNameWhereClauseValueAll CURSOR FOR  
   SELECT [value] FROM dbo.fn_Split(@search_by_column_value_all,'|')  
  
   OPEN curNameWhereClauseValueAll  
   FETCH next FROM curNameWhereClauseValueAll INTO @search_by_column_value  
   WHILE(@@FETCH_STATUS = 0)   
   BEGIN  
	--Creating Insert statement  
	----------------------------------------------------------------------------------------------  
 --   Main Body  
	DECLARE   
	@count             INT,  
	@query             VARCHAR(MAX),  
	@column_name       VARCHAR(MAX),  
	@where_clause      VARCHAR(MAX),  
	@count_where_value INT,  
	@column_value      VARCHAR(MAX)  
  
	SELECT @count = 0,  
	@query        = '',  
	@column_name  = '',  
	@where_clause = '',  
	@count_where_value = 0,  
	@column_value = ''  
	  
	----------------Delete Statement and the where clause--------------------------------------------------------------  
	SET @query = '  
DELETE FROM '+@table_name+' '  
	--WHERE'  
  
	IF(@script_entire_table = 'N')  
	BEGIN  
	  
	 IF(@search_by_primary = 'N')  
	 BEGIN  
  
	  DECLARE curNameWhereClause CURSOR FOR  
	  SELECT [value] FROM dbo.fn_Split(@search_by_column,',')  
  
	  OPEN curNameWhereClause  
	  FETCH next FROM curNameWhereClause INTO @column_name  
	  WHILE(@@FETCH_STATUS = 0)   
	  BEGIN  
  
	   SET @count_where_value = 0  
  
	   DECLARE curNameWhereClauseValue CURSOR FOR  
	   SELECT [value] FROM dbo.fn_Split(@search_by_column_value,',')  
  
	   OPEN curNameWhereClauseValue  
	   FETCH next FROM curNameWhereClauseValue INTO @column_value  
	   WHILE(@@FETCH_STATUS = 0)   
	   BEGIN  
  
		IF(@count = @count_where_value)  
		BEGIN  
  
		 IF(@count = 0)  
		   SET @where_clause = @where_clause+' '+@column_name+' = '''+REPLACE(@column_value, '''', '''''')+''''  
		 ELSE  
		  SET @where_clause = @where_clause+'   
AND '+@column_name+' = '''+REPLACE(@column_value, '''', '''''')+''''  
  
		END       
  
		SET @count_where_value = @count_where_value + 1  
		FETCH next FROM curNameWhereClauseValue INTO @column_value  
  
	   END  
	   CLOSE curNameWhereClauseValue  
	   DEALLOCATE curNameWhereClauseValue  
  
	   SET @count = @count + 1  
	   FETCH next FROM curNameWhereClause INTO @column_name  
  
	  END  
	  CLOSE curNameWhereClause  
	  DEALLOCATE curNameWhereClause  
  
	 END  
	 ELSE  
	 BEGIN  
  
	  SET @count_where_value = 0  
	  DECLARE curNamePrimaryKey CURSOR FOR  
	  SELECT syscolumns.[name]  
	  FROM information_schema.table_constraints tc  
	  INNER JOIN information_schema.key_column_usage kcu  
	  ON tc.constraint_name = kcu.constraint_name  
	  INNER JOIN syscolumns  
	  ON kcu.column_name = syscolumns.[name]  
	  INNER JOIN master..systypes  
	  ON syscolumns.xtype = master..systypes.xtype  
	  WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name )  
	  AND tc.table_name = @table_name  
	  AND tc.constraint_type = 'PRIMARY KEY'  
	  ORDER BY syscolumns.colid  
  
	  OPEN curNamePrimaryKey  
	  FETCH next FROM curNamePrimaryKey INTO @column_name  
	  WHILE(@@FETCH_STATUS = 0)   
	  BEGIN  
  
	   SET @count_where_value = 0  
  
	   DECLARE curNameWhereClauseValue CURSOR FOR  
	   SELECT [value] FROM dbo.fn_Split(@search_by_column_value,',')  
  
	   OPEN curNameWhereClauseValue  
	   FETCH next FROM curNameWhereClauseValue INTO @column_value  
	   WHILE(@@FETCH_STATUS = 0)   
	   BEGIN  
  
		IF(@count = @count_where_value)  
		BEGIN  
  
		 IF(@count = 0)  
		   SET @where_clause = @where_clause+' '+@column_name+' = '''+REPLACE(@column_value, '''', '''''')+''''  
		 ELSE  
		  SET @where_clause = @where_clause+'   
AND '+@column_name+' = '''+REPLACE(@column_value, '''', '''''')+''''  
  
		END       
  
		SET @count_where_value = @count_where_value + 1  
		FETCH next FROM curNameWhereClauseValue INTO @column_value  
	   END  
	   CLOSE curNameWhereClauseValue  
	   DEALLOCATE curNameWhereClauseValue  
  
	   SET @count = @count + 1  
	   FETCH next FROM curNamePrimaryKey INTO @column_name  
  
	  END  
	  CLOSE curNamePrimaryKey  
	  DEALLOCATE curNamePrimaryKey      
  
	 END  
	  
	END  
  
	IF(@script_entire_table = 'N' )  
	 SET @query = @query + +'  
WHERE '+@where_clause  
  
	IF(@delete_required='Y') 
		BEGIN 
			INSERT INTO @table_temp (script_col)VALUES(@query)
--            PRINT @query  
		END
  
	SET @query = ''  
	---------------Delete Complete------------------------------------------------------------------  
  
	---------------Begin Insert--------------------------------------------------------------------  
  
	DECLARE   
	@insert_template VARCHAR(MAX),  
	@select_query    VARCHAR(MAX)  
  
	SELECT  
	@insert_template = '',  
	@select_query    = ''  
  
	SET @count = 0  
  
	SET @select_query = '  
SELECT '  
  
	SET @insert_template ='  
INSERT INTO '+@table_name+'('  
	--All the columns whose value is to be inserted  
	DECLARE curNameChld CURSOR FOR  
	SELECT syscolumns.[name]  
	FROM syscolumns  
	INNER JOIN master..systypes  
	ON syscolumns.xtype = master..systypes.xtype  
	 AND syscolumns.xusertype = master..systypes.xusertype  
	WHERE id IN(SELECT id FROM sysobjects WHERE [type] IN  ( 'U', 'V' ) AND [name]= @table_name)  
	AND (syscolumns.colstat <> 1  OR @include_identity_column = 'Y')
	AND master..systypes.xtype <> 189  
	ORDER BY syscolumns.colid  
  
	OPEN curNameChld  
	FETCH next FROM curNameChld INTO @column_name  
	WHILE(@@FETCH_STATUS = 0)   
	BEGIN  
  
	 IF(@count = 0)  
	 BEGIN  
  
	  SET @insert_template = @insert_template +'  
'+@column_name  
  
	  SET @select_query = @select_query +'CONVERT( VARCHAR(MAX),(CASE WHEN '+@column_name+' IS NULL THEN ''NULL'' ELSE CONVERT(VARCHAR(MAX),'+@column_name+') END))'  
 --' CONVERT(VARCHAR(MAX),ISNULL('+@column_name+',''''))'  
  
	 END  
	 ELSE  
	 BEGIN  
  
	  SET @insert_template = @insert_template +'  
,'+@column_name  
  
	  SET @select_query = @select_query+'+' +''''+'||||'+''''+'+CONVERT( VARCHAR(MAX),(CASE WHEN '+@column_name+' IS NULL THEN ''NULL'' ELSE CONVERT(VARCHAR(MAX),'+@column_name+') END))'  
 --'+CONVERT(VARCHAR(MAX),ISNULL('+@column_name+',''''))'  
  
	 END  
  
	 SET @count = @count + 1  
	 FETCH next FROM curNameChld INTO @column_name  
  
	END  
	CLOSE curNameChld  
	DEALLOCATE curNameChld  
  
	SET @insert_template = @insert_template+')'  
	SET @select_query = @select_query+'  
FROM '+@table_name+' '  
	  
	IF(@script_entire_table = 'N')  
	 SET @select_query = @select_query+'WHERE '+@where_clause  
  
	DECLARE @temp_table TABLE     (col_val VARCHAR (MAX) NULL)  
  
	DELETE FROM @temp_table  
  
	INSERT @temp_table EXEC(@select_query)  
  
	DECLARE curNameChld CURSOR FOR  
	SELECT col_val FROM @temp_table  
	OPEN curNameChld  
	FETCH next FROM curNameChld INTO @column_value  
	WHILE(@@FETCH_STATUS = 0)   
	BEGIN  
  
	 SET @count = 0  
		
	 SET @query = @insert_template+'  
VALUES('  
	 DECLARE curNameValues CURSOR FOR  
	 SELECT [value] FROM dbo.fn_Split(@column_value,'||||')  
  
	 OPEN curNameValues  
	 FETCH next FROM curNameValues INTO @column_name  
	 WHILE(@@FETCH_STATUS = 0)   
	 BEGIN  
  
	  IF(@count=0)  
	  BEGIN  
  
	   IF(@column_name <> 'NULL')  
		SET @query = @query+'  
'''+REPLACE(@column_name, '''', '''''')+''''  
	   ELSE  
		SET @query = @query+'  
NULL'  
  
	  END  
	  ELSE  
	  BEGIN  
  
	   IF(@column_name <> 'NULL')  
		SET @query = @query+'  
,'''+REPLACE(@column_name, '''', '''''')+''''  
	   ELSE  
		SET @query = @query+'  
,NULL'  
  
	  END  
		
	  SET @count = @count + 1  
	  FETCH next FROM curNameValues INTO @column_name  
  
	 END  
	 CLOSE curNameValues  
	 DEALLOCATE curNameValues  
  
	 SET @query = @query+')  
	 '  
--        PRINT @query    
		INSERT INTO @table_temp (script_col)VALUES(@query)
  
		FETCH next FROM curNameChld INTO @column_value  
  
	END  
	CLOSE curNameChld  
	DEALLOCATE curNameChld  
  
  
   ---------------Insert Complete----------------------------------------------------------------  
   -------------------------------------------------------------------------------------------------  
  
	FETCH next FROM curNameWhereClauseValueAll INTO @search_by_column_value  
   END  
   CLOSE curNameWhereClauseValueAll  
   DEALLOCATE curNameWhereClauseValueAll  
  
  END  
	
 END  
 ELSE  
 BEGIN  
  
  RAISERROR('Table is not present in the database',11,1)  
  
 END  

	SELECT script_col FROM @table_temp
  
END
";
		#endregion strSqlCreateInsertStatement

		#endregion

		#region Constructor

		/// <summary>
		/// 
		/// </summary>
		public FetchData()
		{
			try
			{
				strConnection = ConfigurationManager.AppSettings["ConnectionString"];
				OpenConnection(strConnection);
			}
			catch { }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="strConnection"></param>
		public FetchData(string strConnection)
		{
		}

		#endregion Constructor

		#region Private Methods

		/// <summary>
		/// Opens the database connection
		/// </summary>
		/// <param name="strConnectionString"></param>
		private void OpenConnection(string strConnectionString)
		{
			objFactory = SqlClientFactory.Instance;
			dbConnection = objFactory.CreateConnection();
			objCommand = objFactory.CreateCommand();
			dbConnection.ConnectionString = strConnectionString;
			objCommand.Connection = dbConnection;
			dbConnection.Open();
		}

		/// <summary>
		/// Prepares the SQL statement before it is executed
		/// </summary>
		/// <param name="strSQL">SQL statement to be formated</param>
		/// <param name="colParam">Values to be inserted</param>
		/// <param name="bReplaceSingleQuote">true -> single quote need to be handeled </param>
		/// <returns>Formated SQL statement</returns>
		private string PrepareSQL(string strSQL, ArrayList colParam, bool bReplaceSingleQuote)
		{
			int count = 1;
			foreach (string strParam in colParam)
			{
				if (bReplaceSingleQuote)
					strSQL = strSQL.Replace("§" + count, strParam.Replace("'", "''"));
				else
					strSQL = strSQL.Replace("§" + count, strParam);
				count = count + 1;
			}
			return strSQL;
		}

		#endregion Private Methods

		#region Public Methods

		/// <summary>
		/// Validates the connection and returns false if connection fails
		/// </summary>
		/// <param name="strConnectionString">The connection string</param>
		/// <returns>True or False</returns>
		public bool ValidateConnectionString(string strConnectionString)
		{
			try { OpenConnection(strConnectionString); }
			catch { objFactory = null; return false; } return true;
		}

		/// <summary>
		/// Creates function FnSplit if not present in the database
		/// </summary>
		public void CreateFnSplitFunction()
		{
			List<Entity> retScript = new List<Entity>();
			try
			{
				objCommand.CommandTimeout = 0;
				objCommand.CommandText = strFnSplit;
				objCommand.Transaction = dbConnection.BeginTransaction();
				objCommand.ExecuteNonQuery();
				objCommand.Transaction.Commit();
			}
			catch (Exception ex)
			{
				objCommand.Transaction.Rollback();
				objFactory = null;
				throw ex;
			}
		}

		/// <summary>
		/// Gets the list of Database object like tables,views
		/// </summary>
		/// <param name="strScriptType">Any of the following combination in comma seperated format C,D,F,FN,FS,IT,P,PK,S,SQ,TF,TR,U,UQ,V</param>
		/// <returns>Entity List</returns>
		public List<Entity> GetScriptName(string strScriptType)
		{
			List<Entity> retScript = new List<Entity>();
			try
			{
				objCommand.CommandTimeout = 0;
				objCommand.CommandText = "SELECT [name] FROM sys.objects WHERE [type] IN (" + strScriptType + ") ORDER BY [name]";//strCreateSpTemplate;//strCreateSpTemplate;//strSqlCreateStoredProcScript;
				DbDataReader datareader = objCommand.ExecuteReader();
				while (datareader.Read())
				{
					Entity scrp = new Entity();
					scrp.Text = Convert.ToString(datareader["name"/*,"Text","script_col"*/] == DBNull.Value ? "" : datareader["name"/*,"Text","script_col"*/]);
					retScript.Add(scrp);
				}
				datareader.Dispose();
				datareader.Close();
			}
			catch (Exception ex)
			{
				objFactory = null;
				throw ex;
			}
			return retScript;
		}

		/// <summary>
		/// Gives the list of all tables contained in the database
		/// </summary>
		/// <returns></returns>
		public ArrayList GetTables()
		{
			ArrayList retScript = new ArrayList();
			try
			{
				objCommand.CommandTimeout = 0;
				objCommand.CommandText = "SELECT [name] FROM sysobjects WHERE [type] = 'U' ORDER BY [name]";
				DbDataReader datareader = objCommand.ExecuteReader();
				while (datareader.Read())
				{
					retScript.Add(Convert.ToString(datareader["name"] == DBNull.Value ? "" : datareader["name"]));
				}
				datareader.Dispose();
				datareader.Close();
			}
			catch (Exception ex)
			{
				objFactory = null;
				throw ex;
			}
			return retScript;
		}

		/// <summary>
		/// Gets the list of Menu or Operation depending upon the Parameter passed
		/// </summary>
		/// <param name="strType">MENU for Getting List of menu and OPERATION for getting the list of operations</param>
		/// <param name="strFilterValue">Name of the Menu by which you want to filter</param>
		/// <returns></returns>
		public List<Entity> GetMenuOperName(string strType, string strFilterValue)
		{
			List<Entity> retScript = new List<Entity>();
			try
			{
				if (strType == "MENU" || strType == "OPERATION")
				{
					objCommand.CommandTimeout = 0;

					if (strFilterValue.Trim().Length == 0)
						objCommand.CommandText = strType == "MENU" ? "SELECT menu_name [name] FROM menu ORDER BY menu_name" : "SELECT oper_name FROM operation ORDER BY oper_name";
					else
						objCommand.CommandText = strType == "MENU" ? "SELECT menu_name [name] FROM menu WHERE menu_name ='" + strFilterValue.Replace("'", "''") + "' ORDER BY menu_name" : "SELECT o.oper_name [name] FROM operation o INNER JOIN menu m ON o.menu_id = m.menu_id AND o.oper_name <> m.menu_name WHERE m.menu_name = '" + strFilterValue.Replace("'", "''") + "' ORDER BY o.oper_name";

					DbDataReader datareader = objCommand.ExecuteReader();
					while (datareader.Read())
					{
						Entity scrp = new Entity();
						scrp.Text = Convert.ToString(datareader["name"] == DBNull.Value ? "" : datareader["name"]);
						retScript.Add(scrp);
					}
					datareader.Dispose();
					datareader.Close();
				}
			}
			catch (Exception ex)
			{
				objFactory = null;
				throw ex;
			}
			return retScript;
		}

		/// <summary>
		/// Creates New Stored Procedure Template for Table
		/// </summary>
		/// <param name="strTableName">Table name in comma seperated format for which SP template is to be generated</param>
		/// <param name="strForAllDatabaseTable">If "Y" then creates SP template for all the table content of the Database</param>
		/// <returns>Entity List</returns>
		public List<Entity> CreateStoredProcTempleteForTable(string strTableName, string strForAllDatabaseTable)
		{
			List<Entity> retScript = new List<Entity>();
			try
			{
				ArrayList objArryPass = new ArrayList();
				objArryPass.Add(strTableName);
				objArryPass.Add(strForAllDatabaseTable);
				objCommand.CommandTimeout = 0;
				objCommand.CommandText = PrepareSQL(strSqlCreateSpTemplate, objArryPass, true);
				DbDataReader datareader = objCommand.ExecuteReader();
				while (datareader.Read())
				{
					Entity scrp = new Entity();
					scrp.Text = Convert.ToString(datareader["script_col"] == DBNull.Value ? "" : datareader["script_col"]);
					retScript.Add(scrp);
				}
				datareader.Dispose();
				datareader.Close();
			}
			catch (Exception ex)
			{
				objFactory = null;
				throw ex;
			}
			return retScript;
		}

		/// <summary>
		/// Creats Insert Script for a table. The filter has to be passed
		/// </summary>
		/// <param name="strTableName">Table whose insert script is to be generated</param>
		/// <param name="strSearchByColumns">Columns by which data is to be filtered. To be passed in comma seperated format  </param>
		/// <param name="strSearchByColumnValues">Values of the of the columns. To be passed in comma seperated format. If multiple select criteria is to be passed it has to be seperated using '|'  
		/// Example:"ORDER ENTRY,837|ORDER ENTRY,861|ORDER ENTRY,1025"</param>
		/// <param name="strDeleteRequired">If "N" delete statement will be added before insert  Default value is 'Y' </param>
		/// <param name="strSearchByPrimary">Will search by all primary key if "Y"</param>
		/// <param name="strScriptEntireTable">Will generate insert script for the entire table content if "Y"</param>
		/// <param name="strIncludeIdentityColumn">Will include identity column in the insert statement if "Y"</param>
		/// <returns></returns>
		public List<Entity> CreateInsertScriptForTable(string strTableName, string strSearchByColumns,
			string strSearchByColumnValues, string strDeleteRequired, string strSearchByPrimary,
			string strScriptEntireTable, string strIncludeIdentityColumn)
		{
			List<Entity> retScript = new List<Entity>();
			try
			{
				ArrayList objArryPass = new ArrayList();
				objArryPass.Add(strTableName);
				objArryPass.Add(strSearchByColumns);
				objArryPass.Add(strSearchByColumnValues);
				objArryPass.Add(strDeleteRequired);
				objArryPass.Add(strSearchByPrimary);
				objArryPass.Add(strScriptEntireTable);
				objArryPass.Add(strIncludeIdentityColumn);
				objCommand.CommandTimeout = 0;
				objCommand.CommandText = PrepareSQL(strSqlCreateInsertStatement, objArryPass, true);
				DbDataReader datareader = objCommand.ExecuteReader();
				while (datareader.Read())
				{
					Entity scrp = new Entity();
					scrp.Text = Convert.ToString(datareader["script_col"] == DBNull.Value ? "" : datareader["script_col"]);
					retScript.Add(scrp);
				}
				datareader.Dispose();
				datareader.Close();
			}
			catch (Exception ex)
			{
				objFactory = null;
				throw ex;
			}
			return retScript;
		}

		public List<Entity> GetScriptsListModifiedByDate(string strScriptType, DateTime dtFrom, DateTime dtTill)
		{
			List<Entity> retScript = new List<Entity>();
			try
			{
				objCommand.CommandTimeout = 0;
				objCommand.CommandText = @"SELECT [name] FROM sys.objects 
												WHERE [type] IN (" + strScriptType + @")
												AND modify_date >='" + Convert.ToString(dtFrom).Replace("'", "''") + @"'
												AND modify_date <='" + Convert.ToString(dtTill).Replace("'", "''") + @"'
												ORDER BY [type], [name]";
				DbDataReader dataReader = objCommand.ExecuteReader();
				while (dataReader.Read())
				{
					Entity scrp = new Entity();
					scrp.Text = Convert.ToString(dataReader["name"] == DBNull.Value ? "" : dataReader["name"]);
					retScript.Add(scrp);
				}
				dataReader.Dispose();
				dataReader.Close();
			}
			catch (Exception ex)
			{
				objFactory = null;
				throw ex;
			}
			return retScript;
		}

		/// <summary>
		/// Creating drop and insert script of already existing View,Function and Stored Procedure in the Database
		/// </summary>
		/// <param name="alParam">List of passed parameter</param>
		/// <param name="strSearchOption">"Enter Own Value","Get List Form DataBase","Modified By Date"</param>
		/// <returns>Entity List</returns>
		public List<Entity> CreateDropInsertScript(ArrayList alParam, string strSearchOption)
		{
			List<Entity> retScript = new List<Entity>();
			try
			{
				string strScriptNameList = string.Empty;
				if (alParam != null && alParam.Count > 0)
				{
					if (strSearchOption == "Enter Own Value" || strSearchOption == "Get List Form DataBase")
					{
						strScriptNameList = Convert.ToString(alParam[0]);
					}
					else if (strSearchOption == "Modified By Date")
					{
						objCommand.CommandTimeout = 0;
						objCommand.CommandText = @"SELECT [name] FROM sys.objects 
												WHERE [type] IN (" + Convert.ToString(alParam[0]) + @")
												AND modify_date >='" + Convert.ToString(alParam[2]).Replace("'", "''") + @"'
												AND modify_date <='" + Convert.ToString(alParam[3]).Replace("'", "''") + @"'
												ORDER BY modify_date";
						DbDataReader dataReader = objCommand.ExecuteReader();
						while (dataReader.Read())
						{
							if (strScriptNameList == string.Empty)
								strScriptNameList = Convert.ToString(dataReader["name"] == DBNull.Value ? "" : dataReader["name"]);
							else
								strScriptNameList = strScriptNameList + "," + Convert.ToString(dataReader["name"] == DBNull.Value ? "" : dataReader["name"]);
						}
						dataReader.Dispose();
						dataReader.Close();
					}
				}
				ArrayList objArryPass = new ArrayList();
				objArryPass.Add(strScriptNameList);
				objArryPass.Add(alParam[1]);
				objCommand.CommandTimeout = 0;
				objCommand.CommandText = PrepareSQL(strSqlCreateStoredProcScript, objArryPass, true);//"EXEC sp_generate_script_new_test '" + strScriptNameList.Replace("'", "''") + "'";//"exec sp_helptext 'sp_helptext'";//
				DbDataReader datareader = objCommand.ExecuteReader();
				while (datareader.Read())
				{
					Entity scrp = new Entity();
					scrp.Text = Convert.ToString(datareader["Text"] == DBNull.Value ? "" : datareader["Text"]);
					retScript.Add(scrp);
				}
				datareader.Dispose();
				datareader.Close();
			}
			catch (Exception ex)
			{
				objFactory = null;
				throw ex;
			}
			return retScript;
		}

		/// <summary>
		/// Returns the list of Comumn Name
		/// </summary>
		/// <param name="strTableName">Name of the table whose columns is to be fetched</param>
		/// <param name="strColumnType">"All Column" --> All the column of a table is fetched
		/// "Primary Key Column" --> Only the primary key column is fetched</param>
		/// <returns></returns>
		public List<Entity> GetColumnName(string strTableName, string strColumnType)
		{
			List<Entity> objRetEntity = new List<Entity>();
			try
			{
				string strCommand = string.Empty;
				switch (strColumnType)
				{
					case "All Column":
						strCommand = @"select [syscolumns].[name]
								from syscolumns
								inner join master..systypes
								on syscolumns.xtype = master..systypes.xtype
								inner join sysobjects
								on sysobjects.id = syscolumns.id
								and sysobjects.[type] in ( 'U','V', 'P','FN' )
								where sysobjects.id = object_id('" + strTableName + @"')
								order by syscolumns.colid";
						break;
					case "Primary Key Column":
						strCommand = @"SELECT KCU.COLUMN_NAME [name]
								FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS TC
								INNER JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE KCU ON TC.CONSTRAINT_NAME =
								KCU.CONSTRAINT_NAME
								WHERE TC.TABLE_NAME = '" + strTableName + @"'
								AND TC.CONSTRAINT_TYPE ='PRIMARY KEY'";
						break;
				}

				if (!string.IsNullOrEmpty(strCommand))
				{
					objCommand.CommandTimeout = 0;
					objCommand.CommandText = strCommand;
					DbDataReader datareader = objCommand.ExecuteReader();
					while (datareader.Read())
					{
						Entity scrp = new Entity();
						scrp.Text = Convert.ToString(datareader["name"] == DBNull.Value ? "" : datareader["name"]);
						objRetEntity.Add(scrp);
					}
					datareader.Dispose();
					datareader.Close();
				}
			}
			catch (Exception ex)
			{
				objFactory = null;
				throw ex;
			}
			return objRetEntity;
		}

		#endregion Public Methods

	}
}
