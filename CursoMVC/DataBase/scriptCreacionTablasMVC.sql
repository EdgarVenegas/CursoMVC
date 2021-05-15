create table Persona(
IdPersona int identity(1,1) not null primary key,
Nombre nvarchar(32),
APaterno nvarchar(32),
AMaterno nvarchar(32),
Telefono nvarchar(15),
Email nvarchar(32),
FechaNacimiento datetime,
IdUsuarioCreo int,
FechaCreo datetime not null,
IdUsuarioModifico int,
FechaModifico datetime
)
GO
create table Usuario(
IdUsuario int identity(1,1) not null primary key,
IdPersona int foreign key references Persona(IdPersona) not null,
Username nvarchar(32),
Contrasena nvarchar(50),
Estatus bit,
IntentosBloqueo tinyint,
Bloqueado bit,
IdUsuarioCreo int not null,
FechaCreo datetime not null,
IdUsuarioModifico int,
FechaModifico datetime
)
GO
Create table Cliente(--Edgar
IdCliente int identity(1,1) not null primary key,
IdPersona int foreign key references Persona(IdPersona) not null,
Calle nvarchar(50),
NoExt nvarchar(50),
NoInt nvarchar(50),
Colonia nvarchar(50),
Ciudad nvarchar(50),
RFC nvarchar(13),
IdUsuarioCreo int not null,
FechaCreo datetime not null,
IdUsuarioModifico int,
FechaModifico datetime
)
Go
Create table Proveedor(--Luis
IdProveedor int identity(1,1) not null primary key,
RazonSocial nvarchar(32),
Calle nvarchar(50),
NoExt nvarchar(50),
NoInt nvarchar(50),
Colonia nvarchar(50),
Ciudad nvarchar(50),
IdUsuarioCreo int not null,
FechaCreo datetime not null,
IdUsuarioModifico int,
FechaModifico datetime
)
GO
create table Insumo(--JL
IdInsumo int identity(1,1) not null primary key,
IdProveedor int foreign key references Proveedor(IdProveedor),
Categoria nvarchar(32),
Nombre nvarchar(32),
Marca nvarchar(32),Cancelado bit,
Activo bit,
IdUsuarioCreo int not null,
FechaCreo datetime not null,
IdUsuarioModifico int,
FechaModifico datetime
)
GO
Create table Formula(--janet
IdFormula int identity(1,1) not null primary key,
Nombre nvarchar(32),
Activo bit,
IdUsuarioCreo int not null,
FechaCreo datetime not null,
IdUsuarioModifico int,
FechaModifico datetime
)
GO
create Table FormulaDetalle(--janet
IdFormulaDetalle int not null identity(1,1) primary key,
IdFromula int foreign key references Formula(IdFormula) not null,
IdInsumo int foreign key references Insumo(IdInsumo) not null,
Cantidad decimal(18,2),
Borrado bit,
IdUsuarioModifico int,
FechaModifico datetime
)
GO
create table Producto(--Jesus
IdProducto int identity(1,1) not null primary key,
IdFormula int foreign key references Formula(IdFormula) not null,
Nombre nvarchar(32),
Categoria nvarchar(32),
Precio decimal(18,4),
IdUsuarioCreo int not null,
FechaCreo datetime not null,
IdUsuarioModifico int,
FechaModifico datetime
)
GO
Create table Remision(--Aldo
IdRemision int identity(1,1) not null primary key,
IdCliente int foreign key references Cliente(IdCliente) not null,
Fecha datetime,
Total decimal(18,2),
Pagado bit,
Cancelado bit,
IdUsuarioCreo int not null,
FechaCreo datetime not null,
IdUsuarioModifico int,
FechaModifico datetime
)
GO
create Table RemisionDetalle(--Alexa
IdRemisionDetalle int not null identity(1,1) primary key,
IdRemision int foreign key references Remision(IdRemision) not null,
IdProducto int foreign key references Producto(IdProducto) not null,
Borrado bit,
IdUsuarioModifico int,
FechaModifico datetime
)
GO
create table Envios(--Jose
IdEnvio int not null primary key identity(1,1),
IdRemision int foreign key references Remision(IdRemision) not null,
FechaEnvio datetime,
Estatus char,
IdUsuarioCreo int not null,
FechaCreo datetime not null,
IdUsuarioModifico int,
FechaModifico datetime
)