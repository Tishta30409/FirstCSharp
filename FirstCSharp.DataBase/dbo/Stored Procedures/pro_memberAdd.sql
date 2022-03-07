

CREATE PROCEDURE [dbo].[pro_memberAdd]
	@userName VARCHAR(25),
	@price DECIMAL,
	@descript VARCHAR(25)
AS
	INSERT INTO t_member (f_name, f_price, f_descript)
	OUTPUT inserted.*
	VALUES(@userName, @price, @descript)
RETURN 0
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberAdd] TO PUBLIC
    AS [dbo];
