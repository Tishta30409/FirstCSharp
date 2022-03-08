

CREATE PROCEDURE [dbo].[pro_memberAddBatch]
	@type_member type_member READONLY
AS
	INSERT INTO t_member (f_name, f_price, f_descript)
	OUTPUT inserted.*
	SELECT f_name, f_price, f_descript FROM @type_member
RETURN 0
GO

GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberAddBatch] TO PUBLIC
    AS [dbo];
