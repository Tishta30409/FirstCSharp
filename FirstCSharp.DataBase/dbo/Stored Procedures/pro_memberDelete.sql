CREATE PROCEDURE [dbo].[pro_memberDelete]
	@id INT
AS
	DELETE FROM t_member WITH(ROWLOCK)
	OUTPUT deleted.*
	WHERE f_id = @id
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberDelete] TO PUBLIC
    AS [dbo];
