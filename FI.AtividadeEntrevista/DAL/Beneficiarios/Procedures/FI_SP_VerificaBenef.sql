﻿CREATE PROC FI_SP_VerificaBenef
	@CPF VARCHAR(11),
	@ID_CLIENTE BIGINT
AS
BEGIN
	SELECT 1 FROM BENEFICIARIOS WHERE CPF = @CPF AND IDCLIENTE = @ID_CLIENTE
END