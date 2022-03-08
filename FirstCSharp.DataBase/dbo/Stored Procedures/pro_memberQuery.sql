CREATE PROCEDURE [dbo].[pro_memberQuery]
AS
	SELECT f_id, f_name, f_price, f_descript  FROM t_member WITH(NOLOCK) 
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberQuery] TO PUBLIC
    AS [dbo];
