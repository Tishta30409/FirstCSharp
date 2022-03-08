
CREATE TYPE [dbo].[type_member] AS TABLE(
	[f_name] [varchar](25) NOT NULL,
	[f_price] [decimal](18, 0) NULL,
	[f_descript] [varchar](25) NULL
)
GO

GRANT EXECUTE
    ON TYPE::[dbo].[type_member] TO PUBLIC;
GO