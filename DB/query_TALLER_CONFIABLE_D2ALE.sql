CREATE DATABASE PRUEBA
--DROP DATABASE PRUEBA
USE PRUEBA

CREATE TABLE rol 
(
    idRol INT PRIMARY KEY IDENTITY,
    nombreRol VARCHAR(20),
)

create table usuario
(
    idUsuario INT PRIMARY KEY IDENTITY,
    identificacion VARCHAR(20) NOT NULL unique,--agregamos unique a la identificación de los usuarios--
    nombres VARCHAR(50) NOT NULL,
    apellidos VARCHAR(50) NOT NULL,
    telefono VARCHAR(15) NOT NULL,
    correo VARCHAR(50) not NULL,
    contrasena varchar(100) not null,
    fechaNacimiento DATE,
    rolFK int FOREIGN KEY(rolFK)
    REFERENCES Rol(idRol)
)

CREATE TABLE vehiculo
(
    placa varchar(10) PRIMARY KEY NOT NULL,
    marca VARCHAR(20) NOT NULL,
    modelo VARCHAR(50) NOT NULL,
    tipoVehiculo VARCHAR(50) NOT NULL,
    cilindraje VARCHAR(50) NOT NULL,
    ciudadRegistro VARCHAR(50) NOT NULL, 
    usuarioFK int FOREIGN KEY(usuarioFK)
    REFERENCES usuario(idUsuario)
)

CREATE TABLE soat 
(
    idSOAT int PRIMARY KEY IDENTITY,
    fechaCompra DATE,
    fechaVencimiento DATE,
    vehiculoFK VARCHAR(10) FOREIGN KEY (vehiculoFK)
    REFERENCES vehiculo(placa)

)

CREATE TABLE repuesto 
(
    idRepuesto INT PRIMARY KEY IDENTITY,
    nombreRepuesto VARCHAR(50) NOT NULL,
    precio int NOT NULL
)

CREATE TABLE servicio
(
    idServicio INT PRIMARY KEY IDENTITY,
    tipoServicio VARCHAR(20) NOT NULL ,
    descripcion VARCHAR(255) NOT NULL,
    nivAceite VARCHAR(20) NOT NULL,
    nivLiquidoFrenos VARCHAR(20) NOT NULL,
    nivRefrigerante VARCHAR(20) NOT NULL,
    nivLiquidoDireccion VARCHAR(20) NOT NULL,
    fechaCompra DATETIME,
    vehiculoFK VARCHAR(10) FOREIGN KEY (vehiculoFK)
    REFERENCES vehiculo(placa),
    mecanicoFK INT FOREIGN KEY (mecanicoFK) 
    REFERENCES usuario(idUsuario)
)

CREATE table maestro_detalle_rep
(
idMaestro int PRIMARY KEY IDENTITY,
repuestoFK int FOREIGN KEY(repuestoFK)
references repuesto(idRepuesto),
servicioFK int FOREIGN KEY(servicioFK)
REFERENCES servicio(idServicio)
)
insert into rol(nombreRol) VALUES ('Cliente');
insert into rol(nombreRol) VALUES ('Auxiliar');
insert into rol(nombreRol) VALUES ('Jefe de Operaciones');
insert into rol(nombreRol) VALUES ('Mecánico');

GO
--Stored Procedures ROL--

CREATE PROCEDURE sp_ListarRol
AS
BEGIN
    SELECT * FROM rol
END
GO

CREATE PROCEDURE sp_ObtenerRol(@idRol int)
AS
BEGIN
    SELECT * FROM rol WHERE idRol=@idRol 
END

GO
insert into usuario(identificacion, nombres, apellidos, telefono, correo, contrasena, fechaNacimiento,rolFK) VALUES (123,'Soy un','Cliente','3203019435','cliente@gmail.com','1234', '1998-05-15',1);
insert into usuario(identificacion, nombres, apellidos, telefono, correo, contrasena, fechaNacimiento,rolFK) VALUES (1234,'Soy un','Auxiliar','3203019435','auxiliar@gmail.com','1234', '1998-05-15',2);
insert into usuario(identificacion, nombres, apellidos, telefono, correo, contrasena, fechaNacimiento,rolFK) VALUES (12345,'Soy un','Jefe de Operaciones','3203019435','jefeOperaciones@gmail.com','1234', '1998-05-15',3);
insert into usuario(identificacion, nombres, apellidos, telefono, correo, contrasena, fechaNacimiento,rolFK) VALUES (123456,'Soy un','Mecánico','3203019435','Mecanico@gmail.com','1234', '1998-05-15',4);
GO
--Stored Procedures Usuario--

CREATE PROCEDURE sp_ListarUsuario
AS
BEGIN
    SELECT idUsuario, identificacion, nombres, apellidos, telefono, correo, contrasena, fechaNacimiento, roles.nombreRol 'rol'
    FROM usuario usuarios
    INNER JOIN rol roles
    ON roles.idRol = usuarios.rolFK
END
GO

CREATE PROCEDURE sp_ObtenerUsuario(@idUsuario int)
AS
BEGIN
    SELECT * FROM usuario WHERE idUsuario = @idUsuario
END
GO

CREATE PROCEDURE sp_GuardarUsuario(
    @nombres VARCHAR(50),
    @apellidos VARCHAR(50),
    @identificacion VARCHAR(20),
    @telefono VARCHAR(15),
    @correo VARCHAR(50),
    @contrasena varchar(100),
    @fechaNacimiento DATE,
    @rolFK int
)
AS
BEGIN
    INSERT INTO usuario(nombres, apellidos, identificacion, telefono, correo, contrasena, fechaNacimiento, rolFK) VALUES (@nombres, @apellidos,@identificacion,@telefono,@correo,@contrasena,@fechaNacimiento,@rolFK)
END
GO

CREATE PROCEDURE sp_EditarUsuario(
    @idUsuario int,
    @nombres VARCHAR(50),
    @apellidos VARCHAR(50),
    @identificacion VARCHAR(20),
    @telefono VARCHAR(15),
    @correo VARCHAR(50),
    @contrasena varchar(100),
    @fechaNacimiento DATE,
    @rolFK int
)
AS
BEGIN
    UPDATE usuario SET nombres = @nombres, apellidos = @apellidos, identificacion = @identificacion, telefono = @telefono, correo = @correo, contrasena = @contrasena, fechaNacimiento = @fechaNacimiento, rolFK = @rolFK WHERE idUsuario = @idUsuario
END
GO

CREATE PROCEDURE sp_EliminarUsuario(@idUsuario INT)
AS
BEGIN
    DELETE FROM usuario WHERE idUsuario = @idUsuario
END
GO
--Stored Procedures Servicio--

CREATE PROCEDURE sp_ListarServicio AS
BEGIN
    SELECT * FROM servicio
END
GO

CREATE PROCEDURE sp_ObtenerServicio (@idServicio int) AS
BEGIN
    SELECT * FROM servicio WHERE idServicio = @idServicio
END
GO

CREATE PROCEDURE sp_GuardarServicio 
(
    @tipoServicio VARCHAR (20) ,
    @descripcion VARCHAR(255),
    @nivAceite VARCHAR (20),
    @nivLiquidoFrenos VARCHAR(20),
    @nivLiquidoDireccion VARCHAR(20),
    @nivRefrigerante VARCHAR(20),
    @fechaCompra DATETIME,
    @vehiculoFK VARCHAR(50),
    @mecanicoFK INT
) AS
BEGIN
    INSERT INTO servicio 
    (
        tipoServicio, descripcion, nivAceite, nivLiquidoFrenos, 
        nivLiquidoDireccion, nivRefrigerante, fechaCompra, vehiculoFK, mecanicoFK
    ) 
    VALUES 
    (
        @tipoServicio , @descripcion , @nivAceite, @nivLiquidoFrenos,
        @nivLiquidoDireccion, @nivRefrigerante, @fechaCompra , @vehiculoFK,
        @mecanicoFK
    )
END
GO

CREATE PROCEDURE sp_EditarServicio
(
    @idServicio int,
    @tipoServicio VARCHAR (20) ,
    @descripcion VARCHAR(255),
    @nivAceite VARCHAR (20),
    @nivLiquidoFrenos VARCHAR(20),
    @nivLiquidoDireccion VARCHAR(20),
    @nivRefrigerante VARCHAR(20),
    @fechaCompra DATETIME,
    @vehiculoFK VARCHAR(50),
    @mecanicoFK INT
) AS
BEGIN
    UPDATE servicio SET
        tipoServicio = @tipoServicio, descripcion = @descripcion, 
        nivAceite = @nivAceite, nivLiquidoFrenos = @nivLiquidoFrenos, 
        nivLiquidoDireccion = @nivLiquidoDireccion, nivRefrigerante = @nivRefrigerante, 
        fechaCompra = @fechaCompra , vehiculoFK = @vehiculoFK, mecanicoFK = @mecanicoFK
    WHERE idServicio = @idServicio
END
GO

CREATE PROCEDURE sp_EliminarServicio (@idServicio int) AS
BEGIN
    DELETE FROM servicio WHERE idServicio = @idServicio
END
GO
--Stored Procedures Soat--

CREATE PROCEDURE sp_ListarSoat
AS
BEGIN
     SELECT * FROM soat
END
GO

CREATE PROCEDURE sp_ObtenerSoat (@idSOAT int)
AS
BEGIN
     SELECT * FROM soat WHERE idSOAT=@idSOAT
END
GO

CREATE PROCEDURE sp_GuardarSoat(
@vehiculoFK VARCHAR(10),
@fechaCompra DATE,
@fechaVencimiento DATE
)
AS
BEGIN
      INSERT INTO soat(vehiculoFK,fechaCompra,fechaVencimiento) values(@vehiculoFK,@fechaCompra,@fechaVencimiento)
END
GO

CREATE PROCEDURE sp_EditarSoat(
@idSOAT int,
@fechaCompra DATE,
@fechaVencimiento DATE
)
AS
BEGIN
        UPDATE soat SET  fechaCompra=@fechaCompra,
          fechaVencimiento=@fechaVencimiento  WHERE idSOAT=@idSOAT
END
GO

CREATE PROCEDURE sp_EliminarSoat(
    @idSOAT INT
)
AS 
BEGIN
    DELETE FROM soat WHERE idSOAT=@idSOAT
END
GO
--Stored Procedures Vehiculo--

CREATE PROCEDURE sp_ListarVehiculo
AS
BEGIN
    SELECT placa, marca, modelo, tipoVehiculo, cilindraje, ciudadRegistro, usuarios.nombres 'nombre',usuarios.apellidos 'apellido'
    FROM vehiculo vehiculos
    INNER JOIN usuario usuarios
    ON usuarios.idUsuario = vehiculos.usuarioFK
END
GO

CREATE PROCEDURE sp_ObtenerVehiculo(@placa varchar(10))
AS
BEGIN
    --SELECT * FROM vehiculo WHERE placa = @placa

    SELECT placa, marca, modelo, tipoVehiculo, cilindraje, ciudadRegistro, usuario.identificacion
    FROM vehiculo
    INNER JOIN usuario
    ON usuario.idUsuario = vehiculo.usuarioFK WHERE vehiculo.placa = @placa
END
GO

CREATE PROCEDURE sp_GuardarVehiculo(
@placa VARCHAR(10),
@marca VARCHAR(20),
@modelo VARCHAR(50),
@tipoVehiculo VARCHAR(50),
@cilindraje VARCHAR(50),
@ciudadRegistro VARCHAR(50),
@identificacion VARCHAR(20)
)
AS
BEGIN
    DECLARE @usuarioFK int = 
    (SELECT usuario.idUsuario FROM usuario WHERE usuario.identificacion = @identificacion)

    INSERT INTO vehiculo(placa, marca, modelo, tipoVehiculo, cilindraje, ciudadRegistro, usuarioFK)VALUES(@placa, @marca, @modelo, @tipoVehiculo, @cilindraje, @ciudadRegistro, @usuarioFK)
END
GO

CREATE PROCEDURE sp_EditarVehiculo(
    @placa VARCHAR(10),
    @marca VARCHAR(20),
    @modelo VARCHAR(50),
    @tipoVehiculo VARCHAR(50),
    @cilindraje VARCHAR(50),
    @ciudadRegistro VARCHAR(50),
    @identificacion VARCHAR(20)
)
AS
BEGIN
    DECLARE @usuarioFK int = (SELECT usuario.idUsuario FROM usuario WHERE usuario.identificacion = @identificacion)

    UPDATE vehiculo SET marca = @marca, modelo = @modelo, tipoVehiculo = @tipoVehiculo, cilindraje = @cilindraje, ciudadRegistro = @ciudadRegistro, usuarioFK = @usuarioFK WHERE placa = @placa
END
GO

--IMPORTANTE
--¿Creamos procedimiento para editar la placa?

CREATE PROCEDURE sp_EliminarVehiculo(@placa VARCHAR(10))
AS
BEGIN
    DELETE FROM vehiculo WHERE placa = @placa
END
GO
--drop PROCEDURE sp_ObtenerVehiculo

--Stored Procedures Repuesto--
CREATE PROCEDURE sp_ListarRepuesto AS
BEGIN   
    SELECT * FROM repuesto
END
GO

CREATE PROCEDURE sp_ObtenerRepuesto (@IdRepuesto INT) AS
BEGIN   
     SELECT * FROM repuesto WHERE idRepuesto = @IdRepuesto
END
GO

CREATE PROCEDURE sp_GuardarRepuesto 
(
    @IdRepuesto INT,
    @NombreRepuesto VARCHAR(50),
    @Precio INT
) AS BEGIN
    INSERT INTO repuesto
    (
        idRepuesto, nombreRepuesto, precio
    )
    VALUES
    (
        @IdRepuesto, @NombreRepuesto, @Precio
    )
END
GO

CREATE PROCEDURE sp_EditarRepuesto
(
    @IdRepuesto INT,
    @NombreRepuesto VARCHAR(50),
    @Precio INT
)AS 
BEGIN
    UPDATE repuesto SET
    nombreRepuesto = @NombreRepuesto, precio = @Precio
    WHERE idRepuesto = @IdRepuesto
END
GO

CREATE PROCEDURE sp_EliminarRepuesto (@IdRepuesto INT) AS
BEGIN 
    DELETE FROM repuesto WHERE idRepuesto = @IdRepuesto
END
GO

EXECUTE sp_ListarUsuario
--DROP DATABASE PRUEBA

--PRIMER PROCEDIMIENTO ALMACENADO PARA EL LOGIN, EN ESTE SE OBTINE EL id DEL USUARIO
CREATE PROCEDURE sp_ValidarUsuario(
    @identificacion VARCHAR (20),
    @contrasena VARCHAR(100)
)
as 
BEGIN
    IF(exists(select * from usuario where identificacion=@identificacion and contrasena=@contrasena))
        SELECT idUsuario FROM usuario WHERE identificacion=@identificacion and contrasena=@contrasena
    ELSE
        select '0' 
        
    END
GO

--SEGUNDO PROCEDIMIENTO ALMACENADO PARA LOGIN, EN ESTE SE OBTIENE EL rol
create PROCEDURE sp_ValidarUsuarioRol(
    @identificacion VARCHAR (20),
    @contrasena VARCHAR(100)
)
as 
BEGIN
    IF(exists(select * from usuario where identificacion=@identificacion and contrasena=@contrasena))
        SELECT rolFK FROM usuario WHERE identificacion=@identificacion and contrasena=@contrasena 
    ELSE
        select '0' 
        
    END
GO
