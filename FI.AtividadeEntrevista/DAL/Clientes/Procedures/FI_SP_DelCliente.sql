﻿CREATE PROC FI_SP_DelCliente
	@ID BIGINT
AS
BEGIN
	DELETE BENEFICIARIOS WHERE IDCLIENTE = @ID
	DELETE CLIENTES WHERE ID = @ID
END