CREATE TABLE [dbo].t_member
(
	[f_id]		INT	IDENTITY (1, 1)	NOT NULL,
	[f_name]	VARCHAR(25)		NOT NULL,
	[f_price]	DECIMAL(18)		NULL,
	[f_descript]	VARCHAR(25) NULL,

	CONSTRAINT [PK_member] PRIMARY KEY CLUSTERED ([f_id] ASC)
)
