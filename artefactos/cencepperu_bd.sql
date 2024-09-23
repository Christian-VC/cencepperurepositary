
XP_CREATE_SUBDIR 'C:\Proyecto\Datos\Cencepperu'
GO

CREATE DATABASE cencepperu
ON PRIMARY
(NAME = 'Datos01', FILENAME = 'C:\Proyecto\Datos\Cencepperu\Datos01.mdf', SIZE = 50MB, FILEGROWTH = 100MB, MAXSIZE = 2GB),
(NAME = 'Datos02', FILENAME = 'C:\Proyecto\Datos\Cencepperu\Datos02.ndf', SIZE = 50MB, FILEGROWTH = 100MB, MAXSIZE = 2GB),
FILEGROUP cencepperu_personal
	(NAME = 'Datos03', FILENAME = 'C:\Proyecto\Datos\Cencepperu\Datos03.ndf', SIZE = 50MB, FILEGROWTH = 100MB, MAXSIZE = 8GB)
GO

USE cencepperu
GO

CREATE SCHEMA Silabo
GO
CREATE SCHEMA Servicio
GO
CREATE SCHEMA Persona
GO
CREATE SCHEMA Transaccion
GO

CREATE TABLE Silabo.Area (
	CodigoArea NCHAR(8),
	NombreArea NVARCHAR(100) not null,
	EstadoArea NCHAR(1) not null, -- ESTADO A: ACTIVO, I: INACTIVO
	FechaCreacionArea DATE not null,
	FechaActualizacionArea DATE not null,
	CONSTRAINT CodigoAreaPK PRIMARY KEY (CodigoArea),
	CONSTRAINT EstadoAreaCK CHECK (EstadoArea IN ('A', 'I'))
) 
ON cencepperu_personal
GO

CREATE TABLE Silabo.Grupo (
	CodigoGrupo NCHAR(8),
	CodigoAreaGrupo NCHAR(8),
	NombreGrupo NVARCHAR(100) not null,
	EstadoGrupo NCHAR(1) not null, -- ESTADO A: ACTIVO, I: INACTIVO
	FechaCreacionGrupo DATE not null,
	FechaActualizacionGrupo DATE not null,
	CONSTRAINT CodigoGrupoPK PRIMARY KEY (CodigoGrupo),
	CONSTRAINT CodigoAreaGrupoFK FOREIGN KEY (CodigoAreaGrupo) REFERENCES Silabo.Area (CodigoArea),
	CONSTRAINT EstadoGrupoCK CHECK (EstadoGrupo IN ('A', 'I'))
) 
ON cencepperu_personal
GO

CREATE TABLE Silabo.Programa(
	CodigoPrograma NCHAR(8),
	CodigoGrupoPrograma NCHAR(8),
	NombrePrograma NCHAR(100) not null,
	EstadoPrograma NCHAR(1) not null, -- ESTADO A: ACTIVO, I: INACTIVO
	FechaCreacionPrograma DATE not null,
	FechaActualizacionPrograma DATE not null,
	CONSTRAINT CodigoProgramaPK PRIMARY KEY (CodigoPrograma),
	CONSTRAINT CodigoGrupoProgramaFK FOREIGN KEY (CodigoGrupoPrograma) REFERENCES Silabo.Grupo (CodigoGrupo),
	CONSTRAINT EstadoProgramaCK CHECK (EstadoPrograma IN ('A', 'I'))
)
ON cencepperu_personal 
GO

CREATE TABLE Silabo.Modulo(
	CodigoModulo NCHAR(8),
	CodigoProgramaModulo NCHAR(8),
	NombreModulo NVARCHAR(100) not null,
	EstadoModulo NCHAR(1) not null, -- ESTADO A: ACTIVO, I: INACTIVO
	FechaCreacionModulo DATE not null,
	FechaActualizacionModulo DATE not null,
	CONSTRAINT CodigoModuloPK PRIMARY KEY (CodigoModulo),
	CONSTRAINT CodigoProgramaModuloFK FOREIGN KEY (CodigoProgramaModulo) REFERENCES Silabo.Programa (CodigoPrograma),
	CONSTRAINT EstadoModuloCK CHECK (EstadoModulo IN ('A', 'I'))
)
ON cencepperu_personal
GO

CREATE TABLE Persona.Alumno(
	CodigoAlumno NCHAR(8), 
	ApellidoPaternoAlumno NVARCHAR(100) not null,
	ApellidoMaternoAlumno NVARCHAR(100) not null,
	NombresAlumno NVARCHAR(100) not null,
	DniAlumno NCHAR(8) not null, 
	EstadoAlumno NCHAR(1) not null, -- ESTADO A: ACTIVO, I: INACTIVO
	FechaCreacionAlumno DATE not null,
	FechaActualizacionAlumno DATE not null,
	CONSTRAINT CodigoAlumno PRIMARY KEY (CodigoAlumno), 
	CONSTRAINT DniAlumnoUQ UNIQUE (DniAlumno),
	CONSTRAINT EstadoAlumnoCK CHECK (EstadoAlumno IN ('A', 'I'))
)
ON cencepperu_personal
GO

CREATE TABLE Servicio.DetalleCurso(
	CodigoAlumnoDetalleCurso NCHAR(8),
	CodigoProgramaDetalleCurso NCHAR(8),
	FechaInicioDetalleCurso DATE NOT NULL,
	FechaFinDetalleCurso DATE NOT NULL,
	FechaCreacionDetallePedido DATE NOT NULL,
	FechaActualizacionDetallePedido DATE NOT NULL,
	CONSTRAINT CodigoDetalleCursoPK PRIMARY KEY (CodigoAlumnoDetalleCurso, CodigoProgramaDetalleCurso),
	CONSTRAINT CodigoAlumnoDetalleCursoFK FOREIGN KEY (CodigoAlumnoDetalleCurso) REFERENCES Persona.Alumno (CodigoAlumno),
	CONSTRAINT CodigoProgramaDetalleCursoFK FOREIGN KEY (CodigoProgramaDetalleCurso) REFERENCES Silabo.Programa (CodigoPrograma)
)
ON cencepperu_personal
GO

CREATE TABLE Persona.Promotor(
	CodigoPromotor NCHAR(8),
	ApellidoPaternoPromotor NVARCHAR(100) not null,
	ApellidoMaternoPromotor NVARCHAR(100) not null,
	NombresPromotor NVARCHAR(100) not null,
	DniPromotor NCHAR(8) not null,
	CelularPromotor NCHAR(9) not null,
	CorreoPromotor NVARCHAR(100) not null,
	EstadoPromotor NCHAR(1) not null, -- ESTADO A: ACTIVO, I: INACTIVO
	FechaCreacionPromotor DATE not null,
	FechaActualizacionPromotor DATE not null,
	CONSTRAINT CodigoPromotorPK PRIMARY KEY (CodigoPromotor),
	CONSTRAINT DniPromotorUQ UNIQUE (DniPromotor),
	CONSTRAINT EstadoPromotorCK CHECK (EstadoPromotor IN ('A', 'I'))
)
ON cencepperu_personal
GO

CREATE TABLE Servicio.Pedido(
	CodigoPedido NCHAR(8),
	CodigoPromotorPedido NCHAR(8),
	NombrePedido NVARCHAR(100) not null,
	FechaPedido DATE not null,
	CantidadPedido INT not null,
	EstadoPedido NCHAR(1) not null,
	FechaCreacionPedido DATE NOT NULL,
	FechaActualizacionPedido DATE NOT NULL,
	CONSTRAINT CodigoPedidoPK PRIMARY KEY (CodigoPedido),
	CONSTRAINT CodigoPromotorPedidoFK FOREIGN KEY (CodigoPromotorPedido) REFERENCES Persona.Promotor (CodigoPromotor),
	CONSTRAINT EstadoPedidoCK CHECK (EstadoPedido IN ('P', 'F'))
)
ON cencepperu_personal
GO

CREATE TABLE Servicio.DetallePedido(
	CodigoPedidoDetallePedido NCHAR(8),
	CodigoAlumnoDetallePedido NCHAR(8),
	CodigoModuloDetallePedido NCHAR(8),
	FechaCreacionDetallePedido DATE NOT NULL,
	FechaActualizacionDetallePedido DATE NOT NULL,
	CONSTRAINT CodigoDetallePedidoPK PRIMARY KEY (CodigoPedidoDetallePedido, CodigoAlumnoDetallePedido, CodigoModuloDetallePedido),
	CONSTRAINT CodigoPedidoDetallePedidoFK FOREIGN KEY (CodigoPedidoDetallePedido) REFERENCES Servicio.Pedido (CodigoPedido),
	CONSTRAINT CodigoAlumnoDetallePedidoFK FOREIGN KEY (CodigoAlumnoDetallePedido) REFERENCES Persona.Alumno (CodigoAlumno),
	CONSTRAINT CodigoModuloDetallePedidoFK FOREIGN KEY (CodigoModuloDetallePedido) REFERENCES Silabo.Modulo (CodigoModulo)
)
ON cencepperu_personal
GO


--==============================================================================================
/*											ÁREA											*/
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO SELECCIONAR REGISTROS EN LA TABLA ÁREA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.ListarAreaSP
AS
	BEGIN
		SELECT 
			CodigoArea,
			NombreArea,
			EstadoArea
		FROM Silabo.Area
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO BUSCAR REGISTROS POR CÓDIGO EN LA TABLA ÁREA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.BuscarAreaCodigoSP
(
	@CodigoArea NCHAR(8)
)
AS
	BEGIN
		SELECT 
			NombreArea,
			EstadoArea
		FROM Silabo.Area WHERE CodigoArea = @CodigoArea
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO REGISTRAR EN LA TABLA ÁREA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.RegistrarAreaSP
(
	@NombreArea NVARCHAR(100)
)
AS 
	BEGIN
		-- Generar código de acuerdo a la cantidad de áreas registradas
		-- Selecciono el último código o el más grande
		DECLARE @NumeroNuevoCodigo INT;

		IF (SELECT COUNT(*) FROM Silabo.Area) < 1
			BEGIN
				SET @NumeroNuevoCodigo = 1;
			END
		ELSE
			BEGIN 
				DECLARE @UltimoCodigoArea NCHAR(8);
				SELECT @UltimoCodigoArea = MAX(CodigoArea) FROM Silabo.Area;
				SET @NumeroNuevoCodigo = CAST(SUBSTRING(@UltimoCodigoArea, 4, LEN(@UltimoCodigoArea)-4) AS INT)+1;
			END
		DECLARE @CodigoArea NCHAR(8);
		SET @CodigoArea = 'AREA' + RIGHT('0000' + CAST(@NumeroNuevoCodigo AS NVARCHAR(4)), 4);

		INSERT INTO Silabo.Area(CodigoArea, NombreArea, EstadoArea, FechaCreacionArea, FechaActualizacionArea)
		VALUES (@CodigoArea, @NombreArea, 'A', GETDATE(), GETDATE())
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO ACTUALIZAR REGISTROS EN LA TABLA ÁREA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.ActualizarAreaSP
(
	@CodigoArea NCHAR(8),
	@NombreArea NVARCHAR(100)
)
AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Silabo.Area WHERE CodigoArea = @CodigoArea)
			PRINT 'EL ÁREA ACTUALIZADA NO EXISTE'
		ELSE 
			BEGIN
				UPDATE Silabo.Area SET
					NombreArea = @NombreArea,
					FechaActualizacionArea = GETDATE()
				WHERE CodigoArea = @CodigoArea
			END
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO CAMBIAR ESTADO DE LOS REGISTROS EN LA TABLA ÁREA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.CambioEstadoAreaSP
(
	@CodigoArea NCHAR(8),
	@EstadoArea NCHAR(1)
)
AS
	BEGIN
		UPDATE Silabo.Area SET
			EstadoArea = @EstadoArea,
			FechaActualizacionArea = GETDATE()
		WHERE CodigoArea = @CodigoArea
	END
GO
--==============================================================================================


--==============================================================================================
/*											GRUPO											*/
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO SELECCIONAR REGISTROS EN LA TABLA GRUPO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.ListarGrupoSP
AS
	BEGIN
		SELECT 
			CodigoGrupo,
			CodigoAreaGrupo,
			NombreGrupo,
			EstadoGrupo
		FROM Silabo.Grupo
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO BUSCAR REGISTROS POR CÓDIGO EN LA TABLA GRUPO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.BuscarGrupoCodigoSP
(
	@CodigoGrupo NCHAR(8)
)
AS
	BEGIN
		SELECT 
			CodigoAreaGrupo,
			NombreGrupo,
			EstadoGrupo
		FROM Silabo.Grupo WHERE CodigoGrupo = @CodigoGrupo
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO REGISTRAR EN LA TABLA GRUPO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.RegistrarGrupoSP
(
	@CodigoAreaGrupo NCHAR(8),
	@NombreGrupo NVARCHAR(100)
)
AS 
	BEGIN
		-- Generar código de acuerdo a la cantidad de áreas registradas
		-- Selecciono el último código o el más grande
		DECLARE @NumeroNuevoCodigo INT;

		IF (SELECT COUNT(*) FROM Silabo.Grupo) < 1
			BEGIN
				SET @NumeroNuevoCodigo = 1;
			END
		ELSE
			BEGIN 
				DECLARE @UltimoCodigoGrupo NCHAR(8);
				SELECT @UltimoCodigoGrupo = MAX(CodigoGrupo) FROM Silabo.Grupo;
				SET @NumeroNuevoCodigo = CAST(SUBSTRING(@UltimoCodigoGrupo, 4, LEN(@UltimoCodigoGrupo)-4) AS INT)+1;
			END
		DECLARE @CodigoGrupo NCHAR(8);
		SET @CodigoGrupo = 'GRUP' + RIGHT('0000' + CAST(@NumeroNuevoCodigo AS NVARCHAR(4)), 4);

		INSERT INTO Silabo.Grupo(CodigoGrupo, CodigoAreaGrupo, NombreGrupo, EstadoGrupo, FechaCreacionGrupo, FechaActualizacionGrupo)
		VALUES (@CodigoGrupo, @CodigoAreaGrupo, @NombreGrupo, 'A', GETDATE(), GETDATE())
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO ACTUALIZAR REGISTROS EN LA TABLA GRUPO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.ActualizarGrupoSP
(
	@CodigoGrupo NCHAR(8),
	@CodigoAreaGrupo NCHAR(8),
	@NombreGrupo NVARCHAR(100)
)
AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Silabo.Grupo WHERE CodigoGrupo = @CodigoGrupo)
			PRINT 'EL ÁREA ACTUALIZADA NO EXISTE'
		ELSE 
			BEGIN
				UPDATE Silabo.Grupo SET
					CodigoAreaGrupo = @CodigoAreaGrupo,
					NombreGrupo = @NombreGrupo,
					FechaActualizacionGrupo = GETDATE()
				WHERE CodigoGrupo = @CodigoGrupo
			END
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO CAMBIAR ESTADO DE LOS REGISTROS EN LA TABLA GRUPO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.CambioEstadoGrupoSP
(
	@CodigoGrupo NCHAR(8),
	@EstadoGrupo NCHAR(1)
)
AS
	BEGIN
		UPDATE Silabo.Grupo SET
			EstadoGrupo = @EstadoGrupo,
			FechaActualizacionGrupo = GETDATE()
		WHERE CodigoGrupo = @CodigoGrupo
	END
GO
--==============================================================================================



--==============================================================================================
/*										PROGRAMA											*/
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO SELECCIONAR REGISTROS EN LA TABLA PROGRAMA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.ListarProgramaSP
AS
	BEGIN
		SELECT 
			CodigoPrograma,
			CodigoGrupoPrograma,
			NombrePrograma,
			EstadoPrograma
		FROM Silabo.Programa
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO BUSCAR REGISTROS POR CÓDIGO EN LA TABLA PROGRAMA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.BuscarProgramaCodigoSP
(
	@CodigoPrograma NCHAR(8)
)
AS
	BEGIN
		SELECT 
			CodigoGrupoPrograma,
			NombrePrograma,
			EstadoPrograma
		FROM Silabo.Programa WHERE CodigoPrograma = @CodigoPrograma
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO REGISTRAR EN LA TABLA PROGRAMA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.RegistrarProgramaSP
(
	@CodigoGrupoPrograma NCHAR(8),
	@NombrePrograma NVARCHAR(100)
)
AS 
	BEGIN
		-- Generar código de acuerdo a la cantidad de áreas registradas
		-- Selecciono el último código o el más grande
		DECLARE @NumeroNuevoCodigo INT;

		IF (SELECT COUNT(*) FROM Silabo.Programa) < 1
			BEGIN
				SET @NumeroNuevoCodigo = 1;
			END
		ELSE
			BEGIN 
				DECLARE @UltimoCodigoPrograma NCHAR(8);
				SELECT @UltimoCodigoPrograma = MAX(CodigoPrograma) FROM Silabo.Programa;
				SET @NumeroNuevoCodigo = CAST(SUBSTRING(@UltimoCodigoPrograma, 4, LEN(@UltimoCodigoPrograma)-4) AS INT)+1;
			END
		DECLARE @CodigoPrograma NCHAR(8);
		SET @CodigoPrograma = 'PROG' + RIGHT('0000' + CAST(@NumeroNuevoCodigo AS NVARCHAR(4)), 4);

		INSERT INTO Silabo.Programa(CodigoPrograma, CodigoGrupoPrograma, NombrePrograma, EstadoPrograma, FechaCreacionPrograma, FechaActualizacionPrograma)
		VALUES (@CodigoPrograma, @CodigoGrupoPrograma, @NombrePrograma, 'A', GETDATE(), GETDATE())
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO ACTUALIZAR REGISTROS EN LA TABLA PROGRAMA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.ActualizarProgramaSP
(
	@CodigoPrograma NCHAR(8),
	@CodigoGrupoPrograma NCHAR(8),
	@NombrePrograma NVARCHAR(100)
)
AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Silabo.Programa WHERE CodigoPrograma = @CodigoPrograma)
			PRINT 'EL ÁREA ACTUALIZADA NO EXISTE'
		ELSE 
			BEGIN
				UPDATE Silabo.Programa SET
					NombrePrograma = @NombrePrograma,
					CodigoGrupoPrograma = @CodigoGrupoPrograma,
					FechaActualizacionPrograma = GETDATE()
				WHERE CodigoPrograma = @CodigoPrograma
			END
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO CAMBIAR ESTADO DE LOS REGISTROS EN LA TABLA PROGRAMA
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.CambioEstadoProgramaSP
(
	@CodigoPrograma NCHAR(8),
	@EstadoPrograma NCHAR(1)
)
AS
	BEGIN
		UPDATE Silabo.Programa SET
			EstadoPrograma = @EstadoPrograma,
			FechaActualizacionPrograma = GETDATE()
		WHERE CodigoPrograma = @CodigoPrograma
	END
GO
--==============================================================================================



--==============================================================================================
/*											MODULO											*/
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO SELECCIONAR REGISTROS EN LA TABLA MODULO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.ListarModuloSP
AS
	BEGIN
		SELECT 
			CodigoModulo,
			CodigoProgramaModulo,
			NombreModulo,
			EstadoModulo
		FROM Silabo.Modulo
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO BUSCAR REGISTROS POR CÓDIGO EN LA TABLA MODULO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.BuscarModuloCodigoSP
(
	@CodigoModulo NCHAR(8)
)
AS
	BEGIN
		SELECT 
			CodigoProgramaModulo,
			NombreModulo,
			EstadoModulo
		FROM Silabo.Modulo WHERE CodigoModulo = @CodigoModulo
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO REGISTRAR EN LA TABLA MODULO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.RegistrarModuloSP
(
	@CodigoProgramaModulo NCHAR(8),
	@NombreModulo NVARCHAR(100)
)
AS 
	BEGIN
		-- Generar código de acuerdo a la cantidad de áreas registradas
		-- Selecciono el último código o el más grande
		DECLARE @NumeroNuevoCodigo INT;

		IF (SELECT COUNT(*) FROM Silabo.Modulo) < 1
			BEGIN
				SET @NumeroNuevoCodigo = 1;
			END
		ELSE
			BEGIN 
				DECLARE @UltimoCodigoModulo NCHAR(8);
				SELECT @UltimoCodigoModulo = MAX(CodigoModulo) FROM Silabo.Modulo;
				SET @NumeroNuevoCodigo = CAST(SUBSTRING(@UltimoCodigoModulo, 4, LEN(@UltimoCodigoModulo)-4) AS INT)+1;
			END
		DECLARE @CodigoModulo NCHAR(8);
		SET @CodigoModulo = 'PROG' + RIGHT('0000' + CAST(@NumeroNuevoCodigo AS NVARCHAR(4)), 4);

		INSERT INTO Silabo.Modulo(CodigoModulo, CodigoProgramaModulo, NombreModulo, EstadoModulo, FechaCreacionModulo, FechaActualizacionModulo)
		VALUES (@CodigoModulo, @CodigoProgramaModulo, @NombreModulo, 'A', GETDATE(), GETDATE())
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO ACTUALIZAR REGISTROS EN LA TABLA MODULO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.ActualizarModuloSP
(
	@CodigoModulo NCHAR(8),
	@CodigoProgramaModulo NCHAR(8),
	@NombreModulo NVARCHAR(100)
)
AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Silabo.Modulo WHERE CodigoModulo = @CodigoModulo)
			PRINT 'EL ÁREA ACTUALIZADA NO EXISTE'
		ELSE 
			BEGIN
				UPDATE Silabo.Modulo SET
					CodigoProgramaModulo = @CodigoProgramaModulo,
					NombreModulo = @NombreModulo,
					FechaActualizacionModulo = GETDATE()
				WHERE CodigoModulo = @CodigoModulo
			END
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO CAMBIAR ESTADO DE LOS REGISTROS EN LA TABLA MODULO
--==============================================================================================
CREATE OR ALTER PROCEDURE Silabo.CambioEstadoModuloSP
(
	@CodigoModulo NCHAR(8),
	@EstadoModulo NCHAR(1)
)
AS
	BEGIN
		UPDATE Silabo.Modulo SET
			EstadoModulo = @EstadoModulo,
			FechaActualizacionModulo = GETDATE()
		WHERE CodigoModulo = @CodigoModulo
	END
GO
--==============================================================================================



--==============================================================================================
/*										ALUMNO											*/
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO SELECCIONAR REGISTROS EN LA TABLA ALUMNO
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.ListarAlumnoSP
AS
	BEGIN
		SELECT 
			CodigoAlumno,
			ApellidoPaternoAlumno,
			ApellidoMaternoAlumno,
			NombresAlumno,
			DniAlumno,
			EstadoAlumno
		FROM Persona.Alumno
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO BUSCAR REGISTROS POR CÓDIGO EN LA TABLA ALUMNO
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.BuscarAlumnoCodigoSP
(
	@CodigoAlumno NCHAR(8)
)
AS
	BEGIN
		SELECT 
			ApellidoPaternoAlumno,
			ApellidoMaternoAlumno,
			NombresAlumno,
			DniAlumno,
			EstadoAlumno
		FROM Persona.Alumno WHERE CodigoAlumno = @CodigoAlumno
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO REGISTRAR EN LA TABLA ALUMNO
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.RegistrarAlumnoSP
(
	@ApellidoPaternoAlumno NVARCHAR(100),
	@ApellidoMaternoAlumno NVARCHAR(100),
	@NombresAlumno NVARCHAR(100),
	@DniAlumno NCHAR(8)
)
AS 
	BEGIN
		-- Generar código de acuerdo a la cantidad de áreas registradas
		-- Selecciono el último código o el más grande
		DECLARE @NumeroNuevoCodigo INT;

		IF (SELECT COUNT(*) FROM Persona.Alumno) < 1
			BEGIN
				SET @NumeroNuevoCodigo = 1;
			END
		ELSE
			BEGIN 
				DECLARE @UltimoCodigoAlumno NCHAR(8);
				SELECT @UltimoCodigoAlumno = MAX(CodigoAlumno) FROM Persona.Alumno;
				SET @NumeroNuevoCodigo = CAST(SUBSTRING(@UltimoCodigoAlumno, 4, LEN(@UltimoCodigoAlumno)-3) AS INT)+1;
			END
		DECLARE @CodigoAlumno NCHAR(8);
		SET @CodigoAlumno = 'ALU' + RIGHT('00000' + CAST(@NumeroNuevoCodigo AS NVARCHAR(5)), 5);

		INSERT INTO Persona.Alumno(CodigoAlumno, ApellidoPaternoAlumno, ApellidoMaternoAlumno, NombresAlumno, DniAlumno, EstadoAlumno, FechaCreacionAlumno, FechaActualizacionAlumno)
		VALUES (@CodigoAlumno, @ApellidoPaternoAlumno, @ApellidoMaternoAlumno, @NombresAlumno, @DniAlumno, 'A', GETDATE(), GETDATE())
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO ACTUALIZAR REGISTROS EN LA TABLA ALUMNO
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.ActualizarAlumnoSP
(
	@CodigoAlumno NCHAR(8),
	@ApellidoPaternoAlumno NVARCHAR(100),
	@ApellidoMaternoAlumno NVARCHAR(100),
	@NombresAlumno NVARCHAR(100),
	@DniAlumno NCHAR(8)
)
AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Persona.Alumno WHERE CodigoAlumno = @CodigoAlumno)
			PRINT 'EL ALUMNO ACTUALIZADO NO EXISTE'
		ELSE 
			BEGIN
				UPDATE Persona.Alumno SET
					ApellidoPaternoAlumno = @ApellidoPaternoAlumno,
					ApellidoMaternoAlumno = @ApellidoMaternoAlumno,
					NombresAlumno = @NombresAlumno,
					DniAlumno = @DniAlumno,
					FechaActualizacionAlumno = GETDATE()
				WHERE CodigoAlumno = @CodigoAlumno
			END
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO CAMBIAR ESTADO DE LOS REGISTROS EN LA TABLA ALUMNO
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.CambioEstadoAlumnoSP
(
	@CodigoAlumno NCHAR(8),
	@EstadoAlumno NCHAR(1)
)
AS
	BEGIN
		UPDATE Persona.Alumno SET
			EstadoAlumno = @EstadoAlumno,
			FechaActualizacionAlumno = GETDATE()
		WHERE CodigoAlumno = @CodigoAlumno
	END
GO
--==============================================================================================



--==============================================================================================
/*										PROMOTOR											*/
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO SELECCIONAR REGISTROS EN LA TABLA PROMOTOR
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.ListarPromotorSP
AS
	BEGIN
		SELECT 
			CodigoPromotor,
			ApellidoPaternoPromotor,
			ApellidoMaternoPromotor,
			NombresPromotor,
			DniPromotor,
			CelularPromotor,
			CorreoPromotor,
			EstadoPromotor
		FROM Persona.Promotor
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO BUSCAR REGISTROS POR CÓDIGO EN LA TABLA ALUMNO
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.BuscarPromotorCodigoSP
(
	@CodigoPromotor NCHAR(8)
)
AS
	BEGIN
		SELECT 
			ApellidoPaternoPromotor,
			ApellidoMaternoPromotor,
			NombresPromotor,
			DniPromotor,
			CelularPromotor,
			CorreoPromotor,
			EstadoPromotor
		FROM Persona.Promotor WHERE CodigoPromotor = @CodigoPromotor
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO REGISTRAR EN LA TABLA PROMOTOR
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.RegistrarPromotorSP
(
	@ApellidoPaternoPromotor NVARCHAR(100),
	@ApellidoMaternoPromotor NVARCHAR(100),
	@NombresPromotor NVARCHAR(100),
	@DniPromotor NCHAR(8),
	@CelularPromotor NCHAR(9),
	@CorreoPromotor NVARCHAR(100)
)
AS 
	BEGIN
		-- Generar código de acuerdo a la cantidad de áreas registradas
		-- Selecciono el último código o el más grande
		DECLARE @NumeroNuevoCodigo INT;

		IF (SELECT COUNT(*) FROM Persona.Promotor) < 1
			BEGIN
				SET @NumeroNuevoCodigo = 1;
			END
		ELSE
			BEGIN 
				DECLARE @UltimoCodigoPromotor NCHAR(8);
				SELECT @UltimoCodigoPromotor = MAX(CodigoPromotor) FROM Persona.Promotor;
				SET @NumeroNuevoCodigo = CAST(SUBSTRING(@UltimoCodigoPromotor, 4, LEN(@UltimoCodigoPromotor)-4) AS INT)+1;
			END
		DECLARE @CodigoPromotor NCHAR(8);
		SET @CodigoPromotor = 'PROM' + RIGHT('0000' + CAST(@NumeroNuevoCodigo AS NVARCHAR(4)), 4);

		INSERT INTO Persona.Promotor(CodigoPromotor, ApellidoPaternoPromotor, ApellidoMaternoPromotor, NombresPromotor, DniPromotor, CelularPromotor, CorreoPromotor, EstadoPromotor, FechaCreacionPromotor, FechaActualizacionPromotor)
		VALUES (@CodigoPromotor, @ApellidoPaternoPromotor, @ApellidoMaternoPromotor, @NombresPromotor, @DniPromotor, @CelularPromotor, @CorreoPromotor, 'A', GETDATE(), GETDATE())
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO ACTUALIZAR REGISTROS EN LA TABLA PROMOTOR
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.ActualizarPromotorSP
(
	@CodigoPromotor NCHAR(8),
	@ApellidoPaternoPromotor NVARCHAR(100),
	@ApellidoMaternoPromotor NVARCHAR(100),
	@NombresPromotor NVARCHAR(100),
	@DniPromotor NCHAR(8),
	@CelularPromotor NCHAR(9),
	@CorreoPromotor NVARCHAR(100)
)
AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Persona.Promotor WHERE CodigoPromotor = @CodigoPromotor)
			PRINT 'EL PROMOTOR ACTUALIZADO NO EXISTE'
		ELSE 
			BEGIN
				UPDATE Persona.Promotor SET
					ApellidoPaternoPromotor = @ApellidoPaternoPromotor,
					ApellidoMaternoPromotor = @ApellidoMaternoPromotor,
					NombresPromotor = @NombresPromotor,
					DniPromotor = @DniPromotor,
					CelularPromotor = @CelularPromotor,
					CorreoPromotor = @CorreoPromotor,
					FechaActualizacionPromotor = GETDATE()
				WHERE CodigoPromotor = @CodigoPromotor
			END
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO CAMBIAR ESTADO DE LOS REGISTROS EN LA TABLA PROMOTOR
--==============================================================================================
CREATE OR ALTER PROCEDURE Persona.CambioEstadoPromotorSP
(
	@CodigoPromotor NCHAR(8),
	@EstadoPromotor NCHAR(1)
)
AS
	BEGIN
		UPDATE Persona.Promotor SET
			EstadoPromotor = @EstadoPromotor,
			FechaActualizacionPromotor = GETDATE()
		WHERE CodigoPromotor = @CodigoPromotor
	END
GO
--==============================================================================================



--==============================================================================================
/*											PEDIDO											*/
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO SELECCIONAR REGISTROS EN LA TABLA PEDIDO
--==============================================================================================
CREATE OR ALTER PROCEDURE Servicio.ListarPedidoSP
AS
	BEGIN
		SELECT 
			CodigoPedido,
			CodigoPromotorPedido,
			NombrePedido,
			FechaPedido,
			CantidadPedido,
			EstadoPedido
		FROM Servicio.Pedido
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO BUSCAR REGISTROS POR CÓDIGO EN LA TABLA PEDIDO
--==============================================================================================
CREATE OR ALTER PROCEDURE Servicio.BuscarPedidoCodigoSP
(
	@CodigoPedido NCHAR(8)
)
AS
	BEGIN
		SELECT 
			CodigoPromotorPedido,
			NombrePedido,
			FechaPedido,
			CantidadPedido,
			EstadoPedido
		FROM Servicio.Pedido WHERE CodigoPedido = @CodigoPedido
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO REGISTRAR EN LA TABLA PEDIDO
--==============================================================================================
CREATE OR ALTER PROCEDURE Servicio.RegistrarPedidoSP
(
	@CodigoPromotorPedido NCHAR(8),
	@NombrePedido NVARCHAR(100),
	@FechaPedido DATE,
	@CantidadPedido INT
)
AS 
	BEGIN
		-- Generar código de acuerdo a la cantidad de áreas registradas
		-- Selecciono el último código o el más grande
		DECLARE @NumeroNuevoCodigo INT;

		IF (SELECT COUNT(*) FROM Servicio.Pedido) < 1
			BEGIN
				SET @NumeroNuevoCodigo = 1;
			END
		ELSE
			BEGIN 
				DECLARE @UltimoCodigoPedido NCHAR(8);
				SELECT @UltimoCodigoPedido = MAX(CodigoPedido) FROM Servicio.Pedido;
				SET @NumeroNuevoCodigo = CAST(SUBSTRING(@UltimoCodigoPedido, 4, LEN(@UltimoCodigoPedido)-4) AS INT)+1;
			END
		DECLARE @CodigoPedido NCHAR(8);
		SET @CodigoPedido = 'PROG' + RIGHT('0000' + CAST(@NumeroNuevoCodigo AS NVARCHAR(4)), 4);

		INSERT INTO Servicio.Pedido(CodigoPedido, CodigoPromotorPedido, NombrePedido, FechaPedido, CantidadPedido, EstadoPedido, FechaCreacionPedido, FechaActualizacionPedido)
		VALUES (@CodigoPedido, @CodigoPromotorPedido, @NombrePedido, 'A', GETDATE(), GETDATE())
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO ACTUALIZAR REGISTROS EN LA TABLA PEDIDO
--==============================================================================================
CREATE OR ALTER PROCEDURE Servicio.ActualizarPedidoSP
(
	@CodigoPedido NCHAR(8),
	@CodigoPromotorPedido NCHAR(8),
	@NombrePedido NVARCHAR(100),
	@FechaPedido DATE,
	@CantidadPedido INT
)
AS
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Servicio.Pedido WHERE CodigoPedido = @CodigoPedido)
			PRINT 'EL PEDIDO ACTUALIZADO NO EXISTE'
		ELSE 
			BEGIN
				UPDATE Servicio.Pedido SET
					CodigoPromotorPedido = @CodigoPromotorPedido,
					NombrePedido = @NombrePedido,
					FechaPedido = @FechaPedido,
					CantidadPedido = @CantidadPedido
				WHERE CodigoPedido	 = @CodigoPedido
			END
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO CAMBIAR ESTADO DE LOS REGISTROS EN LA TABLA PEDIDO
--==============================================================================================
CREATE OR ALTER PROCEDURE Servicio.CambioEstadoPedidoSP
(
	@CodigoPedido NCHAR(8),
	@EstadoPedido NCHAR(1)
)
AS
	BEGIN
		UPDATE Servicio.Pedido SET
			EstadoPedido = @EstadoPedido,
			FechaActualizacionPedido = GETDATE()
		WHERE CodigoPedido = @CodigoPedido
	END
GO
--==============================================================================================



--==============================================================================================
/*										DETALLE DE PEDIDO										*/
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO BUSCAR REGISTROS POR CÓDIGO DE PEDIDO EN LA TABLA DETALLE PEDIDO
--==============================================================================================
CREATE OR ALTER PROCEDURE Servicio.BuscarDetalleCodigoPedidoSP
(
	@CodigoPedidoDetallePedido NCHAR(8)
)
AS
	BEGIN
		SELECT 
			CodigoAlumnoDetallePedido,
			CodigoModuloDetallePedido
		FROM Servicio.DetallePedido WHERE CodigoPedidoDetallePedido = @CodigoPedidoDetallePedido
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO REGISTRAR EN LA TABLA DETALLE PEDIDO
--==============================================================================================
CREATE OR ALTER PROCEDURE Servicio.RegistrarDetallePedidoSP
(
	@CodigoPedidoDetallePedido NCHAR(8),
	@CodigoAlumnoDetallePedido NCHAR(8),
	@CodigoModuloDetallePedido NCHAR(8)
)
AS 
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Servicio.DetallePedido WHERE CodigoPedidoDetallePedido = @CodigoPedidoDetallePedido AND CodigoAlumnoDetallePedido = @CodigoAlumnoDetallePedido AND CodigoModuloDetallePedido = @CodigoModuloDetallePedido)
			BEGIN
				INSERT INTO Servicio.DetallePedido(CodigoPedidoDetallePedido, CodigoAlumnoDetallePedido, CodigoModuloDetallePedido, FechaCreacionDetallePedido, FechaActualizacionDetallePedido)
				VALUES (@CodigoPedidoDetallePedido, @CodigoAlumnoDetallePedido, @CodigoModuloDetallePedido, GETDATE(), GETDATE())
			END
		END
	END
GO
--==============================================================================================



--==============================================================================================
/*										DETALLE DE PEDIDO										*/
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO BUSCAR REGISTROS POR CÓDIGO DE PEDIDO EN LA TABLA DETALLE PEDIDO
--==============================================================================================
CREATE OR ALTER PROCEDURE Servicio.BuscarDetalleCodigoPedidoSP
(
	@CodigoPedidoDetallePedido NCHAR(8)
)
AS
	BEGIN
		SELECT 
			CodigoAlumnoDetallePedido,
			CodigoModuloDetallePedido
		FROM Servicio.DetallePedido WHERE CodigoPedidoDetallePedido = @CodigoPedidoDetallePedido
	END
GO
--==============================================================================================


-- PROCEDIMIENTO ALMACENADO REGISTRAR EN LA TABLA DETALLE PEDIDO
--==============================================================================================
CREATE OR ALTER PROCEDURE Servicio.RegistrarDetallePedidoSP
(
	@CodigoPedidoDetallePedido NCHAR(8),
	@CodigoAlumnoDetallePedido NCHAR(8),
	@CodigoModuloDetallePedido NCHAR(8)
)
AS 
	BEGIN
		IF NOT EXISTS (SELECT 1 FROM Servicio.DetallePedido WHERE CodigoPedidoDetallePedido = @CodigoPedidoDetallePedido AND CodigoAlumnoDetallePedido = @CodigoAlumnoDetallePedido AND CodigoModuloDetallePedido = @CodigoModuloDetallePedido)
			BEGIN
				INSERT INTO Servicio.DetallePedido(CodigoPedidoDetallePedido, CodigoAlumnoDetallePedido, CodigoModuloDetallePedido, FechaCreacionDetallePedido, FechaActualizacionDetallePedido)
				VALUES (@CodigoPedidoDetallePedido, @CodigoAlumnoDetallePedido, @CodigoModuloDetallePedido, GETDATE(), GETDATE())
			END
		END
	END
GO
--==============================================================================================

