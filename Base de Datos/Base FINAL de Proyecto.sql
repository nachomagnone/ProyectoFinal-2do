use master;
go

if exists (select * from Sysdatabases where name='Proyecto')
begin
  drop database Proyecto
end
GO

CREATE DATABASE Proyecto
GO

USE Proyecto
GO

--creacion de usuario IIS para que el sitio pueda acceder a la bd-------------------------------------------
USE master
GO

CREATE LOGIN [IIS APPPOOL\DefaultAppPool] FROM WINDOWS 
GO

use proyecto
--exec sys.sp_addrolemember 'db_owner', [IIS APPPOOL\DefaultAppPool]


-- TABLAS --
create table Usuarios (
  UsuLog varchar (20) primary key,
  Contrasena varchar(9) not null check (Contrasena Like '[0-9][0-9][0-9][a-Z][a-Z][a-Z][a-Z][^a-Z0-9^][^a-Z0-9^]'),
  Nombre_Completo varchar (50) not null
)
go

CREATE TABLE Meteorologo (
  UsuLog varchar (20) primary key,
  Mail varchar (50) unique check(Mail like '_%@_%.%'),
  Telefono varchar (50) not null,
  Activo bit not null default(1)
    Foreign key (UsuLog) references Usuarios (UsuLog)
)
go

CREATE TABLE Empleado (
  UsuLog varchar (20) primary key,
  Horas_Semanales int check(Horas_Semanales <= 40)
    Foreign key (UsuLog) references Usuarios (UsuLog)
)
go

CREATE TABLE Ciudades (
  Codigo_Ciudad varchar (6) primary key check(Codigo_Ciudad Like '[a-Z][a-Z][a-Z][a-Z][a-Z][a-Z]'),
  Nombre_Pais varchar (30) not null,
  Nombre_Ciudad varchar (50) not null,
  activo bit not null default(1)
)
go

CREATE TABLE Pronosticos (
  Codigo_Interno int primary key identity (1, 1), 
  Fecha_P date not null,
  UsuLog varchar (20),
  Codigo_Ciudad varchar (6),
    Foreign key (UsuLog) references Meteorologo (UsuLog),
    Foreign key (Codigo_Ciudad) references Ciudades (Codigo_Ciudad)
)
go

CREATE TABLE Pronosticos_Hora (
  Hora_Pronostico int,
  Tipo_Cielo varchar (20) not null check(Tipo_Cielo in ('despejado', 'parcialmente nuboso', 'nuboso')),
  Temp_Max int not null,
  Temp_Min int not null,
  Probabilidad_Lluvias int not null,
  Probabilidad_Tormenta int not null,
  Velocidad_Viento int not null,
  Codigo_Interno int not null,
    Primary key (Hora_Pronostico, Codigo_Interno),
    Foreign key (Codigo_Interno) references Pronosticos (Codigo_Interno)
)
go

-----SP------

--LOGUEO--
create Procedure Logueo @UsuLog varchar(20), @Contrasena varchar(9) 
as
begin
	--si es empleado
	if exists (select * from Empleado where UsuLog = @UsuLog)
	select * from Usuarios inner join Empleado on Usuarios.UsuLog = Empleado.UsuLog where Usuarios.UsuLog = @UsuLog and Usuarios.Contrasena = @Contrasena
	-- si es meteorologo 
	if exists (select * from Meteorologo where UsuLog = @UsuLog)
	select * from Usuarios inner join Meteorologo on Usuarios.UsuLog = Meteorologo.UsuLog where Usuarios.UsuLog = @UsuLog and Usuarios.Contrasena = @Contrasena
end
go 

--ALTAS--



--Alta Ciudades --
Create procedure AltaCiudades @Cod_ciu varchar(6), @Nom_pais varchar(30), @Nom_ciu varchar(50)
as
begin
	if (exists(select * from Ciudades where Codigo_Ciudad = @Cod_ciu and activo = 1))
		return -1
	else
	begin
		Insert Ciudades(Codigo_Ciudad, Nombre_Pais, Nombre_Ciudad, activo) Values (@Cod_ciu, @Nom_pais, @Nom_ciu, 1)
		return  1
	end
	if(exists(select * from Ciudades where Codigo_Ciudad = @Cod_ciu and activo = 0))
	begin
		update Ciudades
		set activo = 1, Nombre_Pais = @Nom_pais, Nombre_Ciudad = @Nom_ciu
		where @Cod_ciu = Codigo_Ciudad
		return 1
	end
end
go

--Alta Pronostico--
create proc AltaPronostico @Fecha_pub date, @Usu_Log varchar(20), @Cod_ciu varchar(6), @ret int output
as
begin
	if not exists(select * from Meteorologo where UsuLog = @Usu_Log)
		set @ret = -1
	
	if not exists(select * from Ciudades where Codigo_Ciudad = @Cod_ciu)
		set @ret = -2
		
	insert Pronosticos(Fecha_P, UsuLog, Codigo_Ciudad)
	values(@Fecha_pub, @Usu_Log, @Cod_ciu)
	
	set @ret = @@IDENTITY
end
go 

--Alta Pronostico Hora--
create proc AltaPronostico_Horas @Hora_pro int, @Tipo_cielo varchar(20), @Temp_max int, @Temp_min int, @Probabi_lluvia int, 
		@Probabi_tormenta int, @Veloci_viento int , @Cod_int int
as
begin
	if not exists(select * from Pronosticos where Codigo_Interno = @Cod_int)
		return -1
			
	if exists(select * from Pronosticos_Hora where Hora_Pronostico = @Hora_pro and Codigo_Interno = @Cod_int)
		return -2 
		
	insert Pronosticos_Hora( Hora_Pronostico, Tipo_Cielo, Temp_Max, Temp_Min, Probabilidad_Lluvias, 
							 Probabilidad_Tormenta, Velocidad_Viento, Codigo_Interno)

	values(@Hora_pro, @Tipo_cielo, @Temp_max, @Temp_min, @Probabi_lluvia, @Probabi_tormenta, @Veloci_viento, @Cod_int)
	
	return 1
end
go

-- Buscar Meteorologo --
create procedure BuscarMeteorologoActivo @Usu_log varchar(20)
as
begin
	select * from Meteorologo inner join Usuarios on Meteorologo.UsuLog = Usuarios.UsuLog
	where Meteorologo.UsuLog = @Usu_log and activo = 1
end
go

create procedure BuscarTodosLosMeteorologos @Usu_log varchar(20)
as
begin
	select * from Meteorologo inner join Usuarios on Meteorologo.UsuLog = Usuarios.UsuLog
	where Meteorologo.UsuLog = @Usu_log
end
go

-- Buscar Empleado --
Create procedure BuscarEmpleado @Usu_log varchar(20)
as
begin
	select * from Empleado inner join Usuarios on Usuarios.UsuLog = Empleado.UsuLog
	where Empleado.UsuLog = @Usu_log
end
go

-- Buscar Ciudades --
create procedure BuscarCiudadesActivo @cod_ciu varchar(6)
as
begin
	select * from Ciudades
	where Codigo_Ciudad = @cod_ciu and activo = 1
end
go

create procedure BuscarTodasLasCiudades @cod_ciu varchar(6)
as
begin
	select * from Ciudades
	where Codigo_Ciudad = @cod_ciu
end
go



-- Listar Pronostico--

create procedure ListarPronosticosHoy
as
begin
	select * from Pronosticos	
	where day(Pronosticos.Fecha_P) = day(GETDATE())
	and month(Pronosticos.Fecha_P) = month(GETDATE())
	and year(Pronosticos.Fecha_P) = year(GETDATE())
end
go

create procedure ListarPronosticodelAnio
as
begin
	select * from Pronosticos
	where year(Fecha_P) = year(GETDATE())
end
go



create procedure ListarPronosticoDiario @codigo_interno int
as
begin
	select * from Pronosticos_Hora
	where Codigo_Interno = @codigo_interno
end
go



create procedure ListarMeteorologoSinPronxAno @anio int
as
begin
	if(@anio = 0)
	begin
	select * from Meteorologo inner join Usuarios on Usuarios.UsuLog= Meteorologo.UsuLog
	where not exists (select null from Pronosticos
						where Meteorologo.UsuLog = Pronosticos.UsuLog)
	end
	else 
	begin
	select * from Meteorologo inner join Usuarios on Usuarios.UsuLog= Meteorologo.UsuLog
	where not exists (select null from Pronosticos
						where Meteorologo.UsuLog = Pronosticos.UsuLog and year(Pronosticos.Fecha_P) = @anio)
	end
end
go
 -- MODIFICAR 
create procedure ListarCiudadSinPronxAno @anio int
as
begin
	if(@anio = 0)
	begin
	select * from Ciudades
	where not exists (select null from Pronosticos
						where Ciudades.Codigo_Ciudad = Pronosticos.Codigo_Ciudad)
	end
	else
	begin
	select * from Ciudades
	where not exists (select null from Pronosticos
						where Ciudades.Codigo_Ciudad = Pronosticos.Codigo_Ciudad and year(Pronosticos.Fecha_P) = @anio)
	end
end
go


create procedure ModificarEmpleado @Usu varchar(20), @contrasena varchar(9), @NombreC varchar(50), @Horas int, @usuLog varchar(20)
as
begin
	Declare @passold varchar(9)
	if(not exists(select * from Empleado where UsuLog = @Usu))
		return -1
	else
	begin
		begin tran
			begin
			set @passold = (select s.Contrasena from Usuarios as s where s.UsuLog = @Usu) 
				update Usuarios set Nombre_Completo = @NombreC
				where UsuLog = @Usu
				if (@@ERROR <> 0)
				Begin
					Rollback TRAN
					return -2
				end
				
				update Empleado set Horas_Semanales = @Horas 
				where UsuLog = @Usu
					if (@@ERROR <> 0)
				Begin
					Rollback TRAN
					return -3
				end
				
			end
	
		commit tran
		if (@usu = @usuLog)
		--REVISAR
		exec sp_password @passold,@contrasena,@Usu
		update Usuarios set Contrasena = @contrasena
		where UsuLog = @Usu
		if (@@ERROR <> 0)
				Begin
					Rollback TRAN
					return -2
				end
	end
	return 1
end
go

create procedure ModificarMeteorologo @Usu varchar(20),  @NombreC varchar(50), @Mail varchar(50), @telefono varchar(50) 
as
begin
	if(not exists (select * from Meteorologo where UsuLog = @Usu and activo = 1))
		return -1
	else
	begin
		begin tran	
			update Usuarios set Nombre_Completo = @NombreC 
			where UsuLog = @Usu
			if (@@ERROR <> 0)
				Begin
					Rollback TRAN
					return -2
				end
			update Meteorologo set Mail = @Mail, Telefono = @telefono
			where UsuLog = @Usu
			if (@@ERROR <> 0)
				Begin
					Rollback TRAN
					return -3
				end
			
		commit tran
	end
	return 1
end
go
Create procedure ModificarPassMeteorologo @usu Varchar(20), @Pass varchar(9)
as
BEGIN try
	begin
	if(not exists (select * from Meteorologo where UsuLog = @usu and activo = 1))
		return -1
	Declare @passold varchar(9)
	set @passold = (select s.Contrasena from Usuarios as s where s.UsuLog = @Usu) 
	exec sp_password @passold,@Pass,@usu
	update Usuarios set contrasena=@Pass
	where UsuLog = @usu
	end
END TRY
BEGIN CATCH
	return @@ERROR
END CATCH
go


create procedure ModificarCiudad @Codigo varchar(6), @NombreP varchar(30), @NombreC varchar(50)
as
begin
	if(not exists(select * from Ciudades where Codigo_Ciudad = @Codigo and activo = 1))
		return -1
	else
	begin
		update Ciudades set Nombre_Pais = @NombreP, Nombre_Ciudad = @NombreC
		where Codigo_Ciudad = @Codigo
	end
end
go


CREATE PROCEDURE BajaCiudad @Codigo Varchar(6)
AS
BEGIN
	IF(NOT EXISTS(SELECT * FROM Ciudades WHERE Codigo_Ciudad = @Codigo))
	RETURN -1
	IF(EXISTS(SELECT * FROM Pronosticos WHERE Codigo_Ciudad = @Codigo))
	BEGIN
		UPDATE Ciudades set activo = 0 where Codigo_Ciudad = @Codigo
		RETURN 1
	END
	ELSE
	BEGIN
	DELETE FROM Ciudades WHERE Codigo_Ciudad = @Codigo
	RETURN 1
	END
END
GO

CREATE PROCEDURE BajaMeteorologo @codigoU varchar(20)
as
if (not exists (select * from Meteorologo where UsuLog = @codigoU))
	return -1
BEGIN TRAN	

	if(exists(select * from Pronosticos where UsuLog = @codigoU ))
	BEGIN
	update Meteorologo set activo = 0 where UsuLog = @codigoU
	if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end 
	END
	ELSE
	begin
	delete from Meteorologo where UsuLog = @codigoU
	if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -3
		end 
	delete from Usuarios where UsuLog = @codigoU
	if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -4
		end 
	end
	Declare @VarSentencia varchar(200)
	set @VarSentencia = 'DROP LOGIN ['+ @codigoU +']'
		EXEC (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -5
		end 
		set @VarSentencia = 'DROP USER ['+ @codigoU +']'
		EXEC (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -6
		end 

	commit tran	
return 1	
	
GO

CREATE PROCEDURE BajaEmpleado @codigoE varchar(20)
as
begin 
	Declare @VarSentencia varchar(200)
	if (not exists (select * from Empleado where UsuLog = @codigoE))
	return -1

	else
	begin tran

		delete from Empleado where UsuLog = @codigoE
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end 
		delete from Usuarios where UsuLog = @codigoE
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -3
		end 
		set @VarSentencia = 'DROP LOGIN ['+ @codigoE +']'
		EXEC (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -4
		end 
		set @VarSentencia = 'DROP USER ['+ @codigoE +']'
		EXEC (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -5
		end 

	commit tran	
return 1	
end
go

CREATE PROCEDURE ListarCiudades
as
SELECT * FROM Ciudades WHERE activo=1
GO

create procedure ListarTodaslasCiudades
as
select * from Ciudades 
go
--Alta Empleado
Create procedure AltaEmpleado @Usu_Log varchar(20), @Horas_Semanales varchar(20), @pass varchar(9), @nombre_comp varchar(50) 
as
begin
	if (exists(select * from Usuarios where UsuLog = @Usu_Log))
		return -2
	Declare @VarSentencia varchar(200)
	
	Begin TRAN	
	--primero creo el usuario de logueo
		Set @VarSentencia = 'CREATE LOGIN [' +  @Usu_Log + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''')
		Exec (@VarSentencia)
		
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -4
		end	
		---segundo creo usuario bd
		Set @VarSentencia = 'Create User [' +  @Usu_Log + '] From Login [' + @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -5
		end
		---dar permiso de ejecutar SP
		Set @VarSentencia = 'GRANT EXECUTE TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -6
		end
		Set @VarSentencia ='GRANT EXECUTE ON [dbo].[BuscarTodosLosMeteorologos] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -7
		end
		Set @VarSentencia ='GRANT EXECUTE ON [dbo].[AltaMeteorologo] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -7
		end
		--quito permiso de ejecutar SP ALTA PRONOSTICO
		Set @VarSentencia ='DENY EXECUTE ON [dbo].[AltaPronostico] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -7
		end
			--quito permiso de ejecutar SP ALTA PRONOSTICO hora
		Set @VarSentencia ='Deny execute ON [dbo].[AltaPronostico_Horas] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -7
		end
			--quito permiso de ejecutar SP ALTA PRONOSTICO hora
		Set @VarSentencia ='Deny execute ON [dbo].[ListarCiudades] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -7
		end
		-- QUITO PERMISOS PUBLICOS
		--quito permiso de ejecutar SP ALTA PRONOSTICO
		Set @VarSentencia ='DENY EXECUTE ON [dbo].[Logueo] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -10
		end
		--quito permiso de ejecutar SP ALTA PRONOSTICO
		--Set @VarSentencia ='DENY EXECUTE ON [dbo].[ListarPronosticoDia] TO [' +  @Usu_Log + ']'
		--Exec (@VarSentencia)
		--if (@@ERROR <> 0)
		--Begin
		--	Rollback TRAN
		--	return -11
		--end
		--	Set @VarSentencia ='DENY EXECUTE ON [dbo].[ListarPronosticoHoy] TO [' +  @Usu_Log + ']'
		--Exec (@VarSentencia)
		--if (@@ERROR <> 0)
		--Begin
		--	Rollback TRAN
		--	return -11
		--end

		insert Usuarios(UsuLog, Contrasena, Nombre_Completo) values (@Usu_Log, @pass, @nombre_comp)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -8
		end

		Insert Empleado(UsuLog, Horas_Semanales) Values (@Usu_Log, @Horas_Semanales)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -9
		end
		else
		
		
		Commit TRAN		
	--asigno rol de servidor al usuario recien creado
		Exec sp_addsrvrolemember @loginame=@Usu_Log, @rolename='securityAdmin'	
	--asigno rol de bd al usuario recien creado
	    EXEC sp_addrolemember @rolename = 'db_securityadmin' , @membername = @Usu_Log
		EXEC sp_addrolemember @rolename = 'db_owner' , @membername = @Usu_Log
		return 1
end
go

--Alta Meteorologo--
create procedure AltaMeteorologo @Usu_Log varchar(20), @Mail varchar(50), @Telefono varchar(50), @pass varchar(9), @nombre_comp varchar(50)
as
BEGIN
	if(exists(select * from Meteorologo where UsuLog = @Usu_Log and Activo =1))
		return -1
--EL METEOROLOGO YA EXISTE Y ESTA ACTIVO
	if (exists(select * from Empleado where UsuLog = @Usu_Log))
		return -1
--YA EXISTE ESE USUARIO Y ES EMPLEADO
	BEGIN TRAN
	if (exists (select * from Meteorologo where UsuLog = @Usu_Log and activo = 0))
	--EL USUARIO YA EXISTE PERO ESTA CON BAJA LOGICA SE HACE UN UPDATE
	BEGIN 
	
		update Usuarios
		set Contrasena = @pass, Nombre_Completo = @nombre_comp
		where @Usu_Log = UsuLog
		if (@@ERROR <> 0)
		Begin
				Rollback TRAN
				return -2
		end

		update Meteorologo
		set Telefono = @Telefono, Mail = @Mail, activo = 1
		where @Usu_Log = UsuLog
		if (@@ERROR <> 0)
		begin
				Rollback TRAN
				return -2
		end
	END
	ELSE
	--NO EXISTE ESTE METEOROLOGO Y SE DA DE ALTA
	BEGIN
		insert Usuarios(UsuLog, Contrasena, Nombre_Completo) values (@Usu_Log, @pass, @nombre_comp)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end

		Insert Meteorologo(UsuLog, Mail, Telefono, activo) Values (@Usu_Log, @Mail, @Telefono, 1)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end
	END
	--DAR LOS PERMISOS EN LA BASE 
	Declare @VarSentencia varchar(200)
	Set @VarSentencia = 'CREATE LOGIN [' +  @Usu_Log + '] WITH PASSWORD = ' + QUOTENAME(@pass, '''')
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end
		---segundo creo usuario bd
		Set @VarSentencia = 'Create User [' +  @Usu_Log + '] From Login [' + @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end
		--doy permiso de ejecutar SP ESPECIFICOS
		Set @VarSentencia ='GRANT execute ON [dbo].[AltaPronostico] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end
		
		Set @VarSentencia ='GRANT execute ON [dbo].[AltaPronostico_Horas] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end
		Set @VarSentencia ='GRANT execute ON [dbo].[ListarCiudades] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end
		Set @VarSentencia ='GRANT execute ON [dbo].[BuscarCiudadesActivo] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end
		Set @VarSentencia ='GRANT execute ON [dbo].[BuscarEmpleado] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end
		Set @VarSentencia ='GRANT execute ON [dbo].[BuscarMeteorologoActivo] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end	
		Set @VarSentencia ='GRANT execute ON [dbo].[ModificarPassMeteorologo] TO [' +  @Usu_Log + ']'
		Exec (@VarSentencia)
		if (@@ERROR <> 0)
		Begin
			Rollback TRAN
			return -2
		end	
	COMMIT TRAN
	RETURN 1
END
GO


USE Proyecto
GO

CREATE USER [IIS APPPOOL\DefaultAppPool] FOR LOGIN [IIS APPPOOL\DefaultAppPool]

 GRANT execute ON [dbo].[ListarPronosticosHoy] TO [IIS APPPOOL\DefaultAppPool]
 go
 GRANT execute ON [dbo].[ListarPronosticoDiario] TO [IIS APPPOOL\DefaultAppPool]
 go
 GRANT execute ON [dbo].[Logueo] TO [IIS APPPOOL\DefaultAppPool]
 go
 GRANT execute ON [dbo].[BuscarTodasLasCiudades] TO [IIS APPPOOL\DefaultAppPool]
 go
 GRANT execute ON [dbo].[BuscarTodosLosMeteorologos] TO [IIS APPPOOL\DefaultAppPool]
 go

-----------


insert into Ciudades(Codigo_Ciudad, Nombre_Pais, Nombre_Ciudad)
values ('URUMON', 'Uruguay', 'Montevideo'),
('URUMER', 'Uruguay', 'Mercedes');


exec AltaEmpleado 'nico',20,'123aaaa//','nicolas ceb'
exec AltaMeteorologo 'robert','robert@gmail.com','234566','123aaaa//','roberto burg'
exec AltaEmpleado 'Nacho', 40, '123aaaa//', 'Nacho Magnone'

--exec AltaPronostico '14/03/2022','robert', 'URUMER', 1 
--exec AltaPronostico_Horas '19','despejado', 21,15,1,1,20,4
--select * from Empleado
--select * from Meteorologo
--select * from Usuarios
--select * from Ciudades
--select * from Pronosticos
--select * from Pronosticos_Hora