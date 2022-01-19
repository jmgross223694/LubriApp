create database LubriApp
GO

use LubriApp
GO

create table TiposProducto(
	ID bigint primary key identity (1,1) not null,
	Descripcion varchar(50) unique not null
)
GO

create table MarcasProducto(
	ID bigint primary key identity (1,1) not null,
	Descripcion varchar(50) unique not null,
	Estado bit not null default (1)
)
GO

create table Proveedores(
	ID bigint primary key identity (1,1) not null,
	CUIT varchar(11) unique not null,
	RazonSocial varchar(100) unique not null,
	Estado bit not null default (1)
)
GO

create table Inventario(
	ID bigint not null primary key identity (1,1),
	EAN bigint unique not null,
	Descripcion varchar(60) not null,
	IdTipo bigint not null foreign key references TiposProducto(ID),
	IdMarca bigint not null foreign key references MarcasProducto(ID),
	IdProveedor bigint not null foreign key references Proveedores(ID),
	FechaCompra date not null check (FechaCompra <= getdate()),
	FechaVencimiento date null, check (FechaVencimiento >= getdate()),
	Costo money not null check (Costo >= 0),
	PrecioVenta money not null,
	Stock int not null check (Stock >= 0),
	Estado bit not null default (1)
)
GO

INSERT INTO TiposProducto(Descripcion) values('Lubricante')
INSERT INTO TiposProducto(Descripcion) values('Aceite')
INSERT INTO TiposProducto(Descripcion) values('Líquido de frenos')
INSERT INTO TiposProducto(Descripcion) values('Agua destilada')
INSERT INTO TiposProducto(Descripcion) values('Líquido refrigerante')
INSERT INTO TiposProducto(Descripcion) values('Líquido de dirección')
GO

INSERT INTO Proveedores(CUIT, RazonSocial) values('11111111111', 'ABC S.A.')
INSERT INTO Proveedores(CUIT, RazonSocial) values('22222222222', 'DEF S.R.L.')
INSERT INTO Proveedores(CUIT, RazonSocial) values('33333333333', 'GHI S.C.')
GO

INSERT INTO MarcasProducto(Descripcion) values('YPF')
INSERT INTO MarcasProducto(Descripcion) values('Castrol')
INSERT INTO MarcasProducto(Descripcion) values('BioFair')
INSERT INTO MarcasProducto(Descripcion) values('Bosch')
INSERT INTO MarcasProducto(Descripcion) values('sTp')
INSERT INTO MarcasProducto(Descripcion) values('AcDelco')
GO

INSERT INTO Inventario(EAN, Descripcion, IdTipo, IdMarca, IdProveedor, FechaCompra, FechaVencimiento, Costo, PrecioVenta, Stock) values(7798030610445, 
'Lubricante muy bueno', 1, 2, 1, '2021-05-15', '2023-09-15', 10, 20, 5)
GO
INSERT INTO Inventario(EAN, Descripcion, IdTipo, IdMarca, IdProveedor, FechaCompra, FechaVencimiento, Costo, PrecioVenta, Stock) values(7798030610446,
'Aceite 15W40', 2, 1, 1, '2021-05-15', '2023-09-15', 10, 20, 5)
GO
INSERT INTO Inventario(EAN, Descripcion, IdTipo, IdMarca, IdProveedor, FechaCompra, FechaVencimiento, Costo, PrecioVenta, Stock) values(7798030610447,
'Líquido refrigerante concentrado', 5, 6, 2, '2021-05-15', '2023-09-15', 10, 20, 5)
GO
INSERT INTO Inventario(EAN, Descripcion, IdTipo, IdMarca, IdProveedor, FechaCompra, FechaVencimiento, Costo, PrecioVenta, Stock) values(7798030610448,
'Agua destilada', 4, 3, 2, '2021-05-15', '2023-09-15', 10, 20, 5)
GO
INSERT INTO Inventario(EAN, Descripcion, IdTipo, IdMarca, IdProveedor, FechaCompra, FechaVencimiento, Costo, PrecioVenta, Stock) values(7798030610449,
'Líquido de frenos', 3, 4, 3, '2021-05-15', '2023-09-15', 10, 20, 5)
GO
INSERT INTO Inventario(EAN, Descripcion, IdTipo, IdMarca, IdProveedor, FechaCompra, FechaVencimiento, Costo, PrecioVenta, Stock) values(7798030610450,
'Líquido de dirección hidráulica rojo', 6, 5, 3, '2021-05-15', '2023-09-15', 10, 20, 5)
GO

--(select BulkColumn 
--FROM Openrowset(Bulk 'C:\Users\Juanma\Desktop\GitHub\TPC_GROSS_LAINO_CHAPARRO\TPC_GROSS_LAINO_CHAPARRO\img\Catalogo\SinImagen.jpg', Single_Blob) as NombreImagen

create table TiposCliente(
	ID smallint primary key identity (1,1),
	Descripcion varchar(30) unique check 
	(Descripcion = 'Empresa' or Descripcion = 'Particular' or 
	Descripcion = 'Monotributista' or Descripcion = 'Estatal')
)
GO

create table Clientes(
	ID bigint primary key identity (1,1) not null,
	IDTipo smallint not null foreign key references TiposCliente(ID),
	CUIT_DNI varchar(11) unique not null,
	RazonSocial varchar(100) null default(null),
	ApeNom varchar(100) null default(null),
	FechaAlta date default (getdate()) not null,
	Mail varchar(100) not null,
	Telefono varchar(50) not null,
	TotalVehiculosRegistrados int default (0) check (TotalVehiculosRegistrados >= 0),
	Estado bit default (1)
)
GO

create table TiposUsuario(
	ID int primary key identity (1,1),
	Descripcion varchar(30) unique check (Descripcion = 'Administrador' or Descripcion = 'Jefe' or Descripcion = 'Empleado'),
	Estado bit default (1)
)
GO

create table Usuarios(
	ID int primary key identity (1,1) not null,
	TipoUser int not null foreign key references TiposUsuario(ID),
	Usuario varchar(50) unique not null,
	Pass varchar(50) not null,
	Mail varchar(100) unique not null,
	FechaAlta date default (getdate()),
	Estado bit not null default (1)
)
GO

INSERT INTO TiposUsuario(Descripcion) VALUES('Empleado')
INSERT INTO TiposUsuario(Descripcion) VALUES('Jefe')
INSERT INTO Usuarios(TipoUser, Usuario, Pass, Mail) VALUES(1, 'test', 'test', 'test@test.com')
INSERT INTO Usuarios(TipoUser, Usuario, Pass, Mail) VALUES(2, 'admin', 'admin', 'admin@admin.com')
INSERT INTO Usuarios(TipoUser, Usuario, Pass, Mail) VALUES(1, 'empleado', 'empleado', 'empleado@hotmail.com')
GO

create table Empleados(
	ID bigint primary key identity (1,1) not null,
	Legajo varchar(6) unique not null,
	CUIL varchar(13) unique not null,
	ApeNom varchar(100) not null,
	FechaAlta date not null,
	FechaNacimiento date null,
	Mail varchar(100) unique not null,
	Telefono varchar(50) not null,
	TotalServiciosRealizados int not null default (0) check (TotalServiciosRealizados >= 0),
	Estado bit not null default (1)
)
GO

create table MarcasVehiculo(
	ID bigint primary key identity (1,1) not null,
	Descripcion varchar(50) unique not null
)
GO

INSERT INTO MarcasVehiculo(Descripcion) values('Volkswagen')
INSERT INTO MarcasVehiculo(Descripcion) values('Chevrolet')
INSERT INTO MarcasVehiculo(Descripcion) values('Ford')
INSERT INTO MarcasVehiculo(Descripcion) values('Honda')
INSERT INTO MarcasVehiculo(Descripcion) values('Renault')
GO

create table Vehiculos(
	ID bigint identity(1,1) primary key not null,
	Patente varchar(7) unique not null,
	IdMarca bigint not null foreign key references MarcasVehiculo(ID),
	Modelo varchar(50) not null,
	AnioFabricacion int not null,
	FechaAlta date not null default (getdate()),
	IdCliente bigint not null foreign key references Clientes(ID),
	Estado bit not null default (1)
)
GO

create table TiposServicio(
	ID int primary key identity (1,1) not null,
	Descripcion varchar(100) unique,
	Estado bit not null default(1)
)
GO

insert into TiposServicio(Descripcion) values('Aceite')
insert into TiposServicio(Descripcion) values('Filtros')
insert into TiposServicio(Descripcion) values('Aceite y Filtros')
insert into TiposServicio(Descripcion) values('Sistema refrigeración')
insert into TiposServicio(Descripcion) values('Frenos')
insert into TiposServicio(Descripcion) values('Revisión general')
GO

create table Servicios(
	ID bigint primary key identity (1,1) not null,
	FechaRealizacion datetime not null default(getdate()),
	PatenteVehiculo varchar(7) not null foreign key references Vehiculos(Patente),
	IdTipo int not null foreign key references TiposServicio(ID),
	Comentarios varchar(400) null,
	IdCliente bigint not null foreign key references Clientes(ID),
	IdEmpleado bigint not null foreign key references Empleados(ID),
	Estado varchar(10) not null default('Pendiente') check(Estado = 'Pendiente' OR Estado = 'Completado')
)
GO

create table HistoricoServicios(
	ID bigint primary key identity (1,1) not null,
	IdServicioOriginal bigint not null,
	FechaRealizacion datetime not null,
	PatenteVehiculo varchar(7) not null,
	IdTipo int not null,
	Comentarios varchar(400) null,
	IdCliente bigint not null,
	IdEmpleado bigint not null,
	Estado varchar(19) not null,
	FechaHoraCambio datetime not null
)
GO

create trigger TR_INSERT_HISTORICO_SERVICIOS on Servicios
after insert
as
begin
	declare @IdServicioOriginal bigint = (select ID from inserted)
	declare @FechaRealizacion datetime = (select FechaRealizacion from inserted)
	declare @PatenteVehiculo varchar(7) = (select IdCliente from inserted)
	declare @IdTipo int = (select IdTipo from inserted)
	declare @Comentarios varchar(400) = (select Comentarios from inserted)
	declare @IdCliente bigint = (select IdCliente from inserted)
	declare @IdEmpleado bigint = (select IdEmpleado from inserted)
	declare @Estado varchar(10) = (select Estado from inserted)

	declare @FechaHoraCambio datetime = (select getdate())

	insert into HistoricoServicios(IdServicioOriginal, FechaRealizacion, PatenteVehiculo, IdTipo, 
	Comentarios, IdCliente, IdEmpleado, Estado, FechaHoraCambio)

	values(@IdServicioOriginal, @FechaRealizacion, @PatenteVehiculo, @IdTipo, 
	@Comentarios, @IdCliente, @IdEmpleado, @Estado, @FechaHoraCambio)
end
GO

create trigger TR_UPDATE_HISTORICO_SERVICIOS on Servicios
after update
as
begin
	declare @IdServicioOriginal bigint = (select ID from inserted)
	declare @FechaRealizacion datetime = (select FechaRealizacion from inserted)
	declare @PatenteVehiculo varchar(7) = (select IdCliente from inserted)
	declare @IdTipo int = (select IdTipo from inserted)
	declare @Comentarios varchar(400) = (select Comentarios from inserted)
	declare @IdCliente bigint = (select IdCliente from inserted)
	declare @IdEmpleado bigint = (select IdEmpleado from inserted)
	declare @Estado varchar(19) = (select Estado from inserted)

	declare @FechaHoraCambio datetime = (select getdate())

	update HistoricoServicios set FechaRealizacion = @FechaRealizacion,
								  PatenteVehiculo  = @PatenteVehiculo,
								  IdTipo		   = @IdTipo,
								  Comentarios	   = @Comentarios,
								  IdCliente		   = @IdCliente,
								  IdEmpleado	   = @IdEmpleado,
								  Estado		   = @Estado,
								  FechaHoraCambio  = @FechaHoraCambio
	where IdServicioOriginal = @IdServicioOriginal
end
GO

create trigger TR_DELETE_HISTORICO_SERVICIOS on Servicios
after delete
as
begin
	declare @IdServicioOriginal bigint = (select ID from deleted)

	declare @FechaHoraCambio datetime = (select getdate())

	update HistoricoServicios set Estado = 'Cancelado/Eliminado', 
	FechaHoraCambio = @FechaHoraCambio where IdServicioOriginal = @IdServicioOriginal
end
GO

create view ExportHistoricoServicios
as
	select ID as ID, IdServicioOriginal as IdOriginal, FechaRealizacion as FechaRealizacion, 
	(select V.ID from Vehiculos V where V.Patente = PatenteVehiculo) as IdVehiculo, PatenteVehiculo as Patente,
	IdTipo as IdTipo, (select Descripcion from TiposServicio TS where TS.ID = IdTipo) as TipoServicio, 
	Comentarios as Comentarios, IdCliente as IdCliente, (select isnull(C.ApeNom, C.RazonSocial) from Clientes C 
	where C.ID = IdCliente) as Cliente, IdEmpleado as IdEmpleado, (select E.ApeNom from Empleados E 
	where E.ID = IdEmpleado) as Empleado, Estado as Estado,
	convert(varchar(10),FechaHoraCambio,105) as FechaModificado, convert(varchar(8),FechaHoraCambio,108) as HoraModificado
	from HistoricoServicios
GO

create procedure SP_INSERTAR_PRODUCTO(
	@EAN bigint,
	@Descripcion varchar(60),
	@IdTipo bigint,
	@IdMarca bigint,
	@IdProveedor bigint,
	@FechaCompra date,
	@FechaVencimiento date,
	@Costo money,
	@PrecioVenta money,
	@Stock int,
	@Estado bit
)
as
begin
	INSERT INTO Inventario(EAN, Descripcion, IdTipo, IdMarca, IdProveedor, FechaCompra, FechaVencimiento, Costo, PrecioVenta, Stock, Estado)
	VALUES(@EAN, @Descripcion, @IdTipo, @IdMarca, @IdProveedor, @FechaCompra, @FechaVencimiento, @Costo, @PrecioVenta, @Stock, @Estado)
end
GO

create procedure SP_ACTUALIZAR_PRODUCTO(
	@ID bigint,
	@EAN bigint,
	@Descripcion varchar(60),
	@IdTipo bigint,
	@IdMarca bigint,
	@IdProveedor bigint,
	@FechaCompra date,
	@FechaVencimiento date,
	@Costo money,
	@PrecioVenta money,
	@Stock int,
	@Estado bit
)
as
begin
	UPDATE Inventario SET Descripcion=@Descripcion, IdTipo=@IdTipo, IdMarca=@IdMarca, IdProveedor=@IdProveedor, 
	FechaCompra=@FechaCompra, FechaVencimiento=@FechaVencimiento, Costo=@Costo, PrecioVenta=@PrecioVenta, Stock=@Stock, Estado=@Estado
	WHERE ID=@ID AND EAN=@EAN
end
GO

create view ExportTiposProducto
as
select TP.ID as ID, TP.Descripcion as Descripcion
from TiposProducto TP
GO

create procedure SP_INSERTAR_TIPO_PRODUCTO(
	@Descripcion varchar(60)
)
as
begin
	INSERT INTO TiposProducto(Descripcion) VALUES(@Descripcion)
end
GO

create procedure SP_ELIMINAR_TIPO_PRODUCTO(
	@ID bigint
)
as
begin
	DELETE FROM TiposProducto WHERE ID = @ID
end
GO

CREATE view ExportEmpleados
as
select E.ID as ID, E.Legajo as Legajo, E.Cuil as Cuil, E.ApeNom, CONVERT(VARCHAR(10),E.FechaAlta,105) as FechaAlta, 
CONVERT(VARCHAR(10),E.FechaNacimiento,105) as FechaNacimiento, E.Mail as Mail, E.Telefono as Telefono, E.TotalServiciosRealizados as ServiciosRealizados
from Empleados as E
GO

Create or alter view ExportServicios
as 
select ID as ID, FechaRealizacion as FechaHora, CONVERT(VARCHAR(10),s.FechaRealizacion,105) as Fecha, s.PatenteVehiculo as Patente,
(select V.ID from Vehiculos V where V.Patente = PatenteVehiculo) as IdVehiculo, IdTipo as IdTipo,
(Select ts.Descripcion from TiposServicio ts where ts.id = s.IdTipo) as TiposServicio, s.Comentarios as Comentarios,
(select isnull(c.ApeNom,c.RazonSocial) from Clientes c where c.ID = s.IdEmpleado) as Cliente,
(select C.CUIT_DNI from Clientes C where C.ID = IdCliente) as CUIT_DNI,
IdCliente as IdCliente, (select E.ApeNom from Empleados E where E.ID = IdEmpleado) as Empleado, IdEmpleado as IdEmpleado,
CONVERT(VARCHAR(5),s.FechaRealizacion,108) as Hora, Estado as Estado
from Servicios s
GO

insert into Empleados (Legajo,CUIL,ApeNom,FechaAlta,FechaNacimiento,Mail,Telefono) values ('333','20123456788','Homero Simpson','10-10-2000','1-1-1980','asdasd@asd.com','1234567890')
insert into Empleados (Legajo,CUIL,ApeNom,FechaAlta,FechaNacimiento,Mail,Telefono) values ('222','88765432102','Marge Simpson','10-10-2000','1-1-1980','abcd@abcd.com','0123456789')
GO

create procedure SP_INSERTAR_EMPLEADO(
    @Legajo varchar(6),
    @CUIL varchar(13),
    @ApeNom varchar(100),
    @FechaAlta date,
    @FechaNacimiento date,
    @Mail varchar(100),
    @Telefono varchar (50)
)
as
begin
    INSERT INTO Empleados(Legajo, CUIL, ApeNom, FechaAlta, FechaNacimiento, Mail, Telefono )
    VALUES(@Legajo, @CUIL, @ApeNom, @FechaAlta, @FechaNacimiento, @Mail, @Telefono)
end
GO

create procedure SP_INSERTAR_USUARIO(
	@User varchar(50),
	@Pass varchar(50),
	@Mail varchar(100),
	@TipoUsuario int
)
as
begin	
		INSERT INTO Usuarios(TipoUser, Usuario, Pass, Mail)
		VALUES (@TipoUsuario, @User, @Pass, @Mail)	
end
go

create view ExportClientes
as
	select C.ID, C.CUIT_DNI as 'CUITDNI', isnull(C.RazonSocial,'-') as RazonSocial, isnull(C.ApeNom,'-') as ApeNom, T.ID as 'IdTipo', T.Descripcion as 'TipoCliente',
	CONVERT(VARCHAR(10),C.FechaAlta,105) as FechaAlta, C.Mail, C.Telefono, (select isnull(count(*), 0) from Vehiculos V where V.IdCliente = C.ID) as TotalVehiculosRegistrados, C.Estado
	from Clientes C
	inner join TiposCliente T on IdTipo = T.ID
GO

insert into TiposCliente (Descripcion) values('Empresa')
insert into TiposCliente (Descripcion) values('Particular')
insert into TiposCliente (Descripcion) values('Monotributista')
insert into TiposCliente (Descripcion) values('Estatal')
GO

insert into Clientes (IDTipo, CUIT_DNI, ApeNom, Mail, Telefono)
values(2, 15326856, 'Roberto Villalobos', 'asdasd@gmail.com', '1123456789')
insert into Clientes (IDTipo, CUIT_DNI, RazonSocial, Mail, Telefono)
values(1, 35343323214, 'Salamanca SA', 'salamancasa@gmail.com', '1101234567')
GO

create view ExportProveedores
as
select P.ID ID, P.CUIT CUIT, P.RazonSocial RazonSocial, (select isnull(count(I.ID), 0) from Inventario I where I.IdProveedor = P.ID) Asignaciones, P.Estado Estado
from Proveedores P
GO

create procedure SP_INSERTAR_PROVEEDOR(
	@CUIT varchar(11),
	@RazonSocial varchar(100)
)as
begin
	INSERT INTO Proveedores (CUIT, RazonSocial)
				Values (@CUIT, @RazonSocial)
end
GO

create procedure SP_ACTUALIZAR_CLIENTE(
	@ID bigint,
	@IdTipo smallint,
	@CUIT_DNI varchar(11),
	@RazonSocial varchar(100),
	@ApeNom varchar(100),
	@FechaAlta date,
	@Mail varchar(100),
	@Telefono varchar(50),
	@Estado bit
)as
begin
	UPDATE Clientes Set IDTipo = @IdTipo, CUIT_DNI = @CUIT_DNI, RazonSocial = @RazonSocial,
	ApeNom = @ApeNom, FechaAlta = @FechaAlta, Mail = @Mail,
	Telefono = @Telefono, Estado = @Estado WHERE ID = @ID
end
GO

create procedure SP_AGREGAR_CLIENTE_DNI(
	@IdTipo smallint,
	@CUIT_DNI varchar(11),
	@ApeNom varchar(100),
	@Mail varchar(100),
	@Telefono varchar(50)
)as
begin
	INSERT INTO Clientes(CUIT_DNI, ApeNom, IDTipo, Mail, Telefono)
	VALUES(@CUIT_DNI, @ApeNom, @IdTipo, @Mail, @Telefono)
end
GO

create procedure SP_AGREGAR_CLIENTE_CUIT(
	@IdTipo smallint,
	@CUIT_DNI varchar(11),
	@RazonSocial varchar(100),
	@Mail varchar(100),
	@Telefono varchar(50)
)as
begin
	INSERT INTO Clientes(CUIT_DNI, RazonSocial, IDTipo, Mail, Telefono)
	VALUES(@CUIT_DNI, @RazonSocial, @IdTipo, @Mail, @Telefono)
end
GO

create table HorariosLunesViernes(
    ID int primary key identity (1,1) not null,
    LunesViernes varchar(5) unique not null
)
GO

create table HorariosSabado(
    ID int primary key identity (1,1) not null,
    Sabado varchar(5) unique not null
)
GO

insert INTO HorariosLunesViernes(LunesViernes) values('08:00')
insert INTO HorariosLunesViernes(LunesViernes) values('08:30')
insert INTO HorariosLunesViernes(LunesViernes) values('09:00')
insert INTO HorariosLunesViernes(LunesViernes) values('09:30')
insert INTO HorariosLunesViernes(LunesViernes) values('10:00')
insert INTO HorariosLunesViernes(LunesViernes) values('10:30')
insert INTO HorariosLunesViernes(LunesViernes) values('11:00')
insert INTO HorariosLunesViernes(LunesViernes) values('11:30')
insert INTO HorariosLunesViernes(LunesViernes) values('12:00')
insert INTO HorariosLunesViernes(LunesViernes) values('12:30')
insert INTO HorariosLunesViernes(LunesViernes) values('13:00')
insert INTO HorariosLunesViernes(LunesViernes) values('13:30')
insert INTO HorariosLunesViernes(LunesViernes) values('14:00')
insert INTO HorariosLunesViernes(LunesViernes) values('14:30')
insert INTO HorariosLunesViernes(LunesViernes) values('15:00')
insert INTO HorariosLunesViernes(LunesViernes) values('15:30')
insert INTO HorariosLunesViernes(LunesViernes) values('16:00')
insert INTO HorariosLunesViernes(LunesViernes) values('16:30')
insert INTO HorariosLunesViernes(LunesViernes) values('17:00')
insert INTO HorariosLunesViernes(LunesViernes) values('17:30')
GO

insert INTO HorariosSabado(Sabado) values('08:00')
insert INTO HorariosSabado(Sabado) values('08:30')
insert INTO HorariosSabado(Sabado) values('09:00')
insert INTO HorariosSabado(Sabado) values('09:30')
insert INTO HorariosSabado(Sabado) values('10:00')
insert INTO HorariosSabado(Sabado) values('10:30')
insert INTO HorariosSabado(Sabado) values('11:00')
insert INTO HorariosSabado(Sabado) values('11:30')
insert INTO HorariosSabado(Sabado) values('12:00')
insert INTO HorariosSabado(Sabado) values('12:30')
GO

create table Turnos(
    ID bigint primary key not null identity(1,1),
	IdTipoServicio int not null,
	IdCliente bigint not null foreign key references Clientes(ID),
	IdVehiculo bigint not null foreign key references Vehiculos(ID),
	Dia varchar(9) not null check (Dia <> 'Domingo' OR Dia <> 'Sunday'),
    FechaHora datetime unique not null check ((DATENAME(WEEKDAY, FechaHora)) <> 'Domingo'),
	IDHorario int not null foreign key references HorariosLunesViernes(ID), --(del 1 al 20)
	Estado varchar(10) not null default('Pendiente') check (Estado = 'Pendiente' OR Estado = 'Completado' OR Estado = 'Cancelado')
)
GO

create table HistoricoTurnos(
    ID bigint primary key not null identity(1,1),
	IdTurnoOriginal bigint not null,
	TipoServicio varchar(100) not null,
	Cliente varchar(100) not null,
	PatenteVehiculo varchar(7) not null,
	Dia varchar(9) not null,
    FechaHora datetime not null,
	Estado varchar(19) not null,
	FechaHoraCambio datetime not null
)
GO

create trigger TR_INSERT_TURNOS_GENERAL on Turnos
after insert
as
begin
	declare @IdTurnoOriginal bigint = (select ID from inserted)
	declare @IdTipoServicio int = (select IdTipoServicio from inserted)
	declare @IdCliente bigint = (select IdCliente from inserted)
	declare @IdVehiculo bigint = (select IdVehiculo from inserted)
	declare @Dia varchar(9) = (select Dia from inserted)
	declare @FechaHora datetime = (select FechaHora from inserted)
	declare @Estado varchar(19) = (select Estado from inserted)

	declare @TipoServicio varchar(100) = (select Descripcion from TiposServicio where ID = @IdTipoServicio)
	declare @Cliente varchar(100) = (select isnull(RazonSocial, ApeNom) from Clientes where ID = @IdCliente)
	declare @Patente varchar(7) = (select Patente from Vehiculos where ID = @IdVehiculo)

	declare @FechaHoraCambio datetime = (select getdate())

	insert into HistoricoTurnos(IdTurnoOriginal, TipoServicio, Cliente, PatenteVehiculo, Dia, FechaHora, Estado, FechaHoraCambio)
	values(@IdTurnoOriginal, @TipoServicio, @Cliente, @Patente, @Dia, @FechaHora, @Estado, @FechaHoraCambio)
end
GO

create trigger TR_UPDATE_TURNOS_GENERAL on Turnos
after update
as
begin
	declare @IdTurnoOriginal bigint = (select ID from inserted)
	declare @IdTipoServicio int = (select IdTipoServicio from inserted)
	declare @IdCliente bigint = (select IdCliente from inserted)
	declare @IdVehiculo bigint = (select IdVehiculo from inserted)
	declare @Dia varchar(9) = (select Dia from inserted)
	declare @FechaHora datetime = (select FechaHora from inserted)
	declare @Estado varchar(19) = (select Estado from inserted)

	declare @TipoServicio varchar(100) = (select Descripcion from TiposServicio where ID = @IdTipoServicio)
	declare @Cliente varchar(100) = (select isnull(RazonSocial, ApeNom) from Clientes where ID = @IdCliente)
	declare @Patente varchar(7) = (select Patente from Vehiculos where ID = @IdVehiculo)

	declare @FechaHoraCambio datetime = (select getdate())

	update HistoricoTurnos set IdTurnoOriginal = @IdTurnoOriginal,
							 TipoServicio = @TipoServicio,
							 Cliente = @Cliente,
							 PatenteVehiculo = @Patente,
							 Dia = @Dia,
							 FechaHora = @FechaHora,
							 Estado = @Estado,
							 FechaHoraCambio = @FechaHoraCambio
	where IdTurnoOriginal = @IdTurnoOriginal
end
GO

create or alter trigger TR_DELETE_TURNOS_GENERAL on Turnos
after delete
as
begin
	declare @CantidadTurnosCliente int = (select isnull(count(*), 0) from deleted)
	
	while(@CantidadTurnosCliente > 0)
		begin
			declare @IdTurnoOriginal bigint = (select top 1 ID from deleted)

			declare @FechaHoraCambio datetime = (select getdate())

			update HistoricoTurnos set Estado = 'Cancelado/Eliminado', FechaHoraCambio = @FechaHoraCambio where IdTurnoOriginal = @IdTurnoOriginal

			set @CantidadTurnosCliente = @CantidadTurnosCliente - 1
		end
end
GO

create view ExportHistoricoTurnos
as
select ID as ID, IdTurnoOriginal as 'ID Tabla Turnos', Dia as Dia, CONVERT(VARCHAR(10),FechaHora,105) as Fecha, CONVERT(VARCHAR(5),FechaHora,108) as Hora,
Cliente as Cliente, PatenteVehiculo as Vehiculo, TipoServicio as 'Servicio', Estado as Estado, CONVERT(VARCHAR(10),FechaHoraCambio,105) as FechaCambio,
CONVERT(VARCHAR(10),FechaHoraCambio,108) as HoraCambio
from HistoricoTurnos
GO

create or alter view ExportTurnos
as
select ID as ID, Dia as Dia, FechaHora, CONVERT(VARCHAR(10),FechaHora,105) as Fecha, CONVERT(VARCHAR(5),FechaHora,108) as Hora,
isnull((select C.ApeNom from Clientes C where ID = T.IdCliente),(select C.RazonSocial from Clientes C where ID = T.IdCliente)) as Cliente, 
(select C.CUIT_DNI from Clientes C where ID = T.IdCliente) as CUIT_DNI, (select V.Patente from Vehiculos V where ID = T.IdVehiculo) as Patente,
IDHorario as IDHorario, IdTipoServicio as IdTipoServicio, (select TS.Descripcion from TiposServicio TS where ID = IdTipoServicio) as TipoServicio,
Estado as Estado
from Turnos T
GO

create procedure SP_AGREGAR_TURNO(
    @FechaHora datetime,
	@IDHorario int,
	@Dia varchar(9),
	@IdCliente bigint,
	@IdVehiculo bigint,
	@IdTipoServicio int
)as
begin
    INSERT INTO Turnos(Dia, FechaHora, IDHorario, IdCliente, IdVehiculo, IdTipoServicio) values(@Dia, @FechaHora, @IDHorario, @IdCliente, @IdVehiculo, @IdTipoServicio)
end
GO

create or alter procedure SP_TURNOS_SELECCIONADOS(
	@Fecha date
)as
begin
	SELECT IDHorario as ID FROM Turnos 
	WHERE year(FechaHora) = year(@Fecha)
	and month(FechaHora) = month(@Fecha)
	and day(FechaHora) = day(@Fecha)
end
GO

create procedure SP_AGREGAR_VEHICULO(
	@Patente varchar(7),
	@IdMarca bigint,
	@Modelo varchar(50),
	@AnioFabricacion int,
	@IdCliente bigint
)as
begin
	INSERT INTO Vehiculos(Patente, IdMarca, Modelo, AnioFabricacion, IdCliente)
	Values (@Patente, @IdMarca, @Modelo, @AnioFabricacion, @IdCliente)
end
GO

EXEC SP_AGREGAR_VEHICULO 'AAD123', 2, 'Corsita', 2006, 1
GO

EXEC SP_AGREGAR_VEHICULO 'KTJ262', 2, 'Classic', 2011, 2
GO

insert into Turnos(IdTipoServicio, IdCliente, IdVehiculo, Dia, FechaHora, IDHorario)
values (1, 1, 1, 'Sábado', '2022-09-24 09:30:00.000', 
(select ID from HorariosLunesViernes where LunesViernes LIKE '%09:00%'))
GO

insert into Turnos(IdTipoServicio, IdCliente, IdVehiculo, Dia, FechaHora, IDHorario)
values (3, 2, 2, 'Viernes', '2022-09-23 10:30:00.000', 
(select ID from HorariosLunesViernes where LunesViernes LIKE '%09:00%'))
GO

create view ExportVehiculos
as
	SELECT V.ID as ID, V.Patente as Patente, (select M.ID from MarcasVehiculo M 
	Where M.ID = V.IdMarca) as IdMarca, (select M.Descripcion from MarcasVehiculo M 
	Where M.ID = V.IdMarca) as Marca, V.Modelo as Modelo, V.AnioFabricacion as 'Año de fabricación',
	CONVERT(VARCHAR(10),V.FechaAlta,105) as 'Fecha de alta', (select C.CUIT_DNI from Clientes C Where C.ID = V.IdCliente) 
	as CUITDNI, (select isnull(C.ApeNom, C.RazonSocial) from Clientes C Where C.ID = V.IdCliente)
	as Cliente, Estado as Estado from Vehiculos V
GO

create view ExportUsuarios
as
	SELECT U.ID as ID, (select T.Descripcion as TipoUser from TiposUsuario T where U.TipoUser = T.ID) as TipoUser,
	U.Usuario as Usuario, U.Pass as Pass, U.Mail as Mail, CONVERT(VARCHAR(10),U.FechaAlta,105) as FechaAlta,
	Estado as Estado from Usuarios U
GO

create trigger TR_ELIMINAR_TIPO_SERVICIO on TiposServicio
instead of delete
as
begin
	declare @IdTipoServicio int = (select ID from deleted)
	declare @Estado bit = (select Estado from deleted)

	if @Estado = 0
		begin
			update TiposServicio set Estado = 1 where ID = @IdTipoServicio
		end

		else

		begin
			update TiposServicio set Estado = 0 where ID = @IdTipoServicio
		end
end
GO

create procedure UPDATE_VEHICULO(
	@ID bigint,
	@Patente varchar(7),
	@IdMarca bigint,
	@Modelo varchar(50),
	@AñoFabricacion int,
	@Estado bit
)as
begin
	update Vehiculos set Patente = @Patente, IdMarca = @IdMarca, Modelo = @Modelo, 
	AnioFabricacion = @AñoFabricacion, Estado = @Estado where ID = @ID
end
GO

create trigger TR_INSERT_SERVICIO on Servicios
after insert
as
begin
	declare @IdEmpleado bigint = (select IdEmpleado from inserted)
	declare @cantServiciosActual int = (select TotalServiciosRealizados from Empleados where ID = @IdEmpleado)

	update Empleados set TotalServiciosRealizados = @cantServiciosActual + 1 where ID = @IdEmpleado

	declare @EstadoServicio varchar(10) = (select Estado from inserted)
	if (@EstadoServicio = 'Completado')
		begin
			insert into AvisosServicios(IdCliente, IdTipoServicio, IdServicio, Patente, FechaAviso, FechaRealizado)
			select IdCliente, IdTipo, ID, PatenteVehiculo, 
			CONCAT(year(FechaRealizacion)+1,'/',month(FechaRealizacion),'/',day(FechaRealizacion)-7),
			FechaRealizacion from inserted
		end
end
GO

insert into servicios(PatenteVehiculo, IdTipo, comentarios, idcliente, IdEmpleado) values('AAD123', 2, 'Cliente conforme', 1, 1)
GO
insert into servicios(PatenteVehiculo, IdTipo, comentarios, idcliente, IdEmpleado) values('KTJ262', 4, 'Cliente disconforme', 2, 2)
GO

create procedure UPDATE_SERVICIO(
	@ID bigint,
	@FechaHora datetime,
	@Patente varchar(7),
	@Comentarios varchar(400),
	@Estado varchar(10),
	@IdTipo int,
	@IdCliente bigint,
	@IdEmpleado bigint
)as
begin
	update Servicios set FechaRealizacion = @FechaHora, PatenteVehiculo = @Patente,
	IdTipo = @IdTipo, Comentarios = @Comentarios, IdCliente = @IdCliente, IdEmpleado = @IdEmpleado,
	Estado = @Estado WHERE ID = @ID
end
GO

create procedure INSERT_SERVICIO(
	@FechaHora datetime,
	@Patente varchar(7),
	@Comentarios varchar(400),
	@Estado varchar(10),
	@IdTipo int,
	@IdCliente bigint,
	@IdEmpleado bigint
)as
begin
	insert into Servicios(FechaRealizacion, PatenteVehiculo, IdTipo, Comentarios, IdCliente, IdEmpleado, Estado)
	values(@FechaHora, @Patente, @IdTipo, @Comentarios, @IdCliente, @IdEmpleado, @Estado)
end
GO

create table CredencialesMail(
	ID int not null identity(1,1),
	Usuario varchar(200) unique not null,
	Clave varchar(200) not null
)
GO

insert into CredencialesMail(Usuario, Clave)
values ('pruebalubriapp@gmail.com','lubriAppGLC')
GO

create table AvisosServicios(
	ID bigint not null identity(1,1),
	IdCliente bigint not null,
	IdServicio bigint not null,
	IdTipoServicio int not null,
	Patente varchar(7) not null,
	FechaRealizado date not null,
	FechaAviso date not null,
	MailEnviado bit not null default(0)
)
GO

create view Export_AvisosServicios
as
select ID as ID,
(select isnull(EC.RazonSocial, EC.ApeNom) from ExportClientes EC where EC.ID = Avs.IdCliente) as Cliente,
(select EC.Mail from ExportClientes EC where EC.ID = Avs.IdCliente) as Mail,
(select TS.Descripcion from TiposServicio TS where TS.ID = AvS.IdTipoServicio) as TipoServicio,
AvS.Patente as Patente,
CONVERT(varchar,AvS.FechaRealizado,105) as FechaRealizado,
CONVERT(varchar,AvS.FechaAviso,105) as FechaAviso,
MailEnviado as Enviado
from AvisosServicios AvS
where AvS.FechaAviso = CONVERT(varchar,getdate(),105)
GO

create trigger TR_UPDATE_SERVICIO on Servicios
after update
as
begin
	declare @IdServicio bigint = (select ID from inserted)
	declare @IdCliente bigint = (select IdCliente from inserted)
	declare @Patente varchar(7) = (select PatenteVehiculo from inserted)
	declare @IdTipoServicio int = (select IdTipo from inserted)
	declare @FechaRealizado date = (select FechaRealizacion from inserted)
	declare @Estado varchar(20) = (select Estado from inserted)
	declare @TipoServicio varchar(100) = (select Descripcion from TiposServicio where ID = @IdTipoServicio)

	if(	@Estado = 'Completado' AND
		@TipoServicio = 'Revisión de filtros' OR 
		@TipoServicio = 'Revisión de aceite y filtros' OR 
		@TipoServicio = 'Revisión de aceite')
	begin
		update AvisosServicios set IdCliente = @IdCliente, 
		IdTipoServicio = @IdTipoServicio, Patente = @Patente, 
		FechaRealizado = @FechaRealizado
		where IdServicio = @IdServicio
	end
	else if (@Estado <> 'Completado')
	begin
		delete from AvisosServicios where IdServicio = @IdServicio
	end

	declare @IdEmpleadoAnterior bigint = (select IdEmpleado from deleted)
	declare @IdEmpleadoNuevo bigint = (select IdEmpleado from inserted)
	declare @CantServiciosEmpleadoAnterior int = (select TotalServiciosRealizados from Empleados where ID = @IdEmpleadoAnterior)
	declare @CantServiciosEmpleadoNuevo int = (select TotalServiciosRealizados from Empleados where ID = @IdEmpleadoNuevo)

	if (@IdEmpleadoAnterior <> @IdEmpleadoNuevo)
	begin
		update Empleados set TotalServiciosRealizados = @CantServiciosEmpleadoAnterior - 1 where ID = @IdEmpleadoAnterior
		update Empleados set TotalServiciosRealizados = @CantServiciosEmpleadoNuevo + 1 where ID = @IdEmpleadoNuevo
	end

end
GO

create table ImagenesInventario(
	ID bigint primary key not null identity(1,1),
	Imagen varbinary(max) not null,
	EAN bigint not null
)
GO

create view ExportInventario
as
select I.ID as ID, I.EAN as EAN, I.Descripcion as Descripción, 
isnull((select IV.Imagen from ImagenesInventario IV WHERE I.EAN = IV.EAN), CONVERT(varbinary(max), 'VACIO')) as Imagen,
IdTipo, TP.Descripcion as TipoProducto, 
IdMarca, M.Descripcion as Marca, IdProveedor, P.RazonSocial as Proveedor, 
CONVERT(VARCHAR(10),I.FechaCompra,105) as 'Fecha de Compra', CONVERT(VARCHAR(10),I.FechaVencimiento,105) as 'Fecha de Vencimiento',
I.Costo as Costo, I.PrecioVenta as PrecioVenta, I.Stock as Stock, I.Estado as Estado, M.Estado as EstadoMarca, P.Estado as EstadoProveedor 
from Inventario as I
inner join TiposProducto as TP on I.IdTipo = TP.ID
inner join MarcasProducto as M on I.IdMarca = M.ID
inner join Proveedores as P on I.IdProveedor = P.ID
GO

create trigger TR_DELETE_SERVICIO on Servicios
after delete
as
begin
	declare @IdEmpleado bigint = (select IdEmpleado from deleted)
	declare @cantServiciosActual int = (select TotalServiciosRealizados from Empleados where ID = @IdEmpleado)

	update Empleados set TotalServiciosRealizados = @cantServiciosActual - 1 where ID = @IdEmpleado
end
GO

create trigger TR_DELETE_IMAGES_PRODUCT on Inventario
after delete
as
begin
	declare @EAN bigint = (select EAN from deleted)

	delete from ImagenesInventario where EAN = @EAN
end
GO

--Elimina los turnos (si es que hay) asociados al clientes que se eliminó
create or alter trigger TR_ELIMINAR_TURNOS_CLIENTES on Clientes
instead of delete
as
begin
	declare @IdCliente bigint = (select ID from deleted)
	declare @CantidadTurnos int = (select isnull(count(*), 0) Turnos from Turnos where IdCliente = @IdCliente)

	if (@CantidadTurnos = 0)
		begin
			update Clientes set Estado = 0 where ID = @IdCliente
		end
	else
		begin
			delete from Turnos where IdCliente = @IdCliente
			
			update Clientes set Estado = 0 where ID = @IdCliente
		end
end
GO

--Elimina los turnos asociados al cliente, si se actualiza su estado a '0'
create or alter trigger TR_CAMBIO_ESTADO_CLIENTE on Clientes
after update
as
begin
	declare @IdCliente bigint = (select ID from deleted)
	declare @EstadoNuevo int = (select Estado from inserted)

	if (@EstadoNuevo = 0)
		begin
			delete from Turnos where ID = @IdCliente
		end
end
GO