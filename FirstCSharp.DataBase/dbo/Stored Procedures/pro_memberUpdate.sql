CREATE PROCEDURE [dbo].[pro_memberUpdate]
	@id INT,--傳入值
	@userName VARCHAR(25),
	@price DECIMAL,
	@descript VARCHAR(25)
AS
	UPDATE t_member WITH(ROWLOCK)
	SET 
	f_name = @userName, 
	f_price = @price, 
	f_descript = @descript 
	OUTPUT inserted.*
	WHERE f_id = @id
RETURN 0
GO
GRANT EXECUTE
    ON OBJECT::[dbo].[pro_memberUpdate] TO PUBLIC
    AS [dbo];
