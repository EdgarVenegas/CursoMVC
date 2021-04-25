CREATE PROCEDURE spGetActivos
AS
BEGIN
	SELECT A.IdUsuario, A.Username, A.Estatus, A.Bloqueado,
		   B.Nombre, B.APaterno, B.AMaterno, B.Telefono, B.Email,
		   B.FechaNacimiento
	FROM Usuario A
	INNER JOIN Persona B on A.IdPersona = B.IdPersona
	WHERE A.Estatus = 1
	ORDER BY B.APaterno, B.Nombre
END