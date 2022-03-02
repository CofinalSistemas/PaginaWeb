
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/30/2016 11:24:51
-- Generated from EDMX file: D:\Ingenio.PortalWebRepo - 1\Ingenio.PortalWebRepo\Ingenio.DAL\IngenioModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Ingenio];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Permisos_Modulos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Permisos] DROP CONSTRAINT [FK_Permisos_Modulos];
GO
IF OBJECT_ID(N'[dbo].[FK_RolesModulos_Modulos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolesModulos] DROP CONSTRAINT [FK_RolesModulos_Modulos];
GO
IF OBJECT_ID(N'[dbo].[FK_RolesModulos_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RolesModulos] DROP CONSTRAINT [FK_RolesModulos_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuariosPermisos_Permisos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuariosPermisos] DROP CONSTRAINT [FK_UsuariosPermisos_Permisos];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuariosPermisos_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuariosPermisos] DROP CONSTRAINT [FK_UsuariosPermisos_Usuarios];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuariosRoles_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuariosRoles] DROP CONSTRAINT [FK_UsuariosRoles_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuariosRoles_Usuarios]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuariosRoles] DROP CONSTRAINT [FK_UsuariosRoles_Usuarios];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Modulos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Modulos];
GO
IF OBJECT_ID(N'[dbo].[Permisos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Permisos];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[RolesModulos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RolesModulos];
GO
IF OBJECT_ID(N'[dbo].[Sliders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sliders];
GO
IF OBJECT_ID(N'[dbo].[Usuarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Usuarios];
GO
IF OBJECT_ID(N'[dbo].[UsuariosPermisos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuariosPermisos];
GO
IF OBJECT_ID(N'[dbo].[UsuariosRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuariosRoles];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Modulos'
CREATE TABLE [dbo].[Modulos] (
    [Id] int  NOT NULL,
    [Nombre] varchar(max)  NOT NULL
);
GO

-- Creating table 'Permisos'
CREATE TABLE [dbo].[Permisos] (
    [Id] int  NOT NULL,
    [Descripcion] varchar(max)  NOT NULL,
    [Id_Modulo] int  NOT NULL
);
GO

-- Creating table 'UsuariosPermisos'
CREATE TABLE [dbo].[UsuariosPermisos] (
    [Id] int  NOT NULL,
    [Id_Usuario] int  NOT NULL,
    [Id_Permiso] int  NOT NULL
);
GO

-- Creating table 'UsuariosRoles'
CREATE TABLE [dbo].[UsuariosRoles] (
    [Id] int  NOT NULL,
    [Id_Usuario] int  NOT NULL,
    [Id_Rol] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(max)  NOT NULL,
    [Activo] bit  NOT NULL
);
GO

-- Creating table 'RolesModulos'
CREATE TABLE [dbo].[RolesModulos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Id_Rol] int  NOT NULL,
    [Id_Modulo] int  NOT NULL
);
GO

-- Creating table 'Usuarios'
CREATE TABLE [dbo].[Usuarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] varchar(max)  NOT NULL,
    [Password] varchar(max)  NOT NULL,
    [FechaCreacion] datetime  NOT NULL,
    [FechaUltimoAcceso] datetime  NOT NULL,
    [Nombre] varchar(max)  NOT NULL,
    [Telefono] varchar(max)  NOT NULL,
    [Identificacion] varchar(max)  NOT NULL,
    [Cargo] varchar(max)  NOT NULL
);
GO

-- Creating table 'Sliders'
CREATE TABLE [dbo].[Sliders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Imagen] varchar(max)  NULL,
    [Titulo] varchar(max)  NULL,
    [Descripcion] varchar(max)  NULL,
    [Url] varchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Modulos'
ALTER TABLE [dbo].[Modulos]
ADD CONSTRAINT [PK_Modulos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [PK_Permisos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsuariosPermisos'
ALTER TABLE [dbo].[UsuariosPermisos]
ADD CONSTRAINT [PK_UsuariosPermisos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsuariosRoles'
ALTER TABLE [dbo].[UsuariosRoles]
ADD CONSTRAINT [PK_UsuariosRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RolesModulos'
ALTER TABLE [dbo].[RolesModulos]
ADD CONSTRAINT [PK_RolesModulos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Usuarios'
ALTER TABLE [dbo].[Usuarios]
ADD CONSTRAINT [PK_Usuarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sliders'
ALTER TABLE [dbo].[Sliders]
ADD CONSTRAINT [PK_Sliders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_Modulo] in table 'Permisos'
ALTER TABLE [dbo].[Permisos]
ADD CONSTRAINT [FK_Permisos_Modulos]
    FOREIGN KEY ([Id_Modulo])
    REFERENCES [dbo].[Modulos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Permisos_Modulos'
CREATE INDEX [IX_FK_Permisos_Modulos]
ON [dbo].[Permisos]
    ([Id_Modulo]);
GO

-- Creating foreign key on [Id_Permiso] in table 'UsuariosPermisos'
ALTER TABLE [dbo].[UsuariosPermisos]
ADD CONSTRAINT [FK_UsuariosPermisos_Permisos]
    FOREIGN KEY ([Id_Permiso])
    REFERENCES [dbo].[Permisos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuariosPermisos_Permisos'
CREATE INDEX [IX_FK_UsuariosPermisos_Permisos]
ON [dbo].[UsuariosPermisos]
    ([Id_Permiso]);
GO

-- Creating foreign key on [Id_Modulo] in table 'RolesModulos'
ALTER TABLE [dbo].[RolesModulos]
ADD CONSTRAINT [FK_RolesModulos_Modulos]
    FOREIGN KEY ([Id_Modulo])
    REFERENCES [dbo].[Modulos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolesModulos_Modulos'
CREATE INDEX [IX_FK_RolesModulos_Modulos]
ON [dbo].[RolesModulos]
    ([Id_Modulo]);
GO

-- Creating foreign key on [Id_Rol] in table 'RolesModulos'
ALTER TABLE [dbo].[RolesModulos]
ADD CONSTRAINT [FK_RolesModulos_Roles]
    FOREIGN KEY ([Id_Rol])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RolesModulos_Roles'
CREATE INDEX [IX_FK_RolesModulos_Roles]
ON [dbo].[RolesModulos]
    ([Id_Rol]);
GO

-- Creating foreign key on [Id_Rol] in table 'UsuariosRoles'
ALTER TABLE [dbo].[UsuariosRoles]
ADD CONSTRAINT [FK_UsuariosRoles_Roles]
    FOREIGN KEY ([Id_Rol])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuariosRoles_Roles'
CREATE INDEX [IX_FK_UsuariosRoles_Roles]
ON [dbo].[UsuariosRoles]
    ([Id_Rol]);
GO

-- Creating foreign key on [Id_Usuario] in table 'UsuariosPermisos'
ALTER TABLE [dbo].[UsuariosPermisos]
ADD CONSTRAINT [FK_UsuariosPermisos_Usuarios]
    FOREIGN KEY ([Id_Usuario])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuariosPermisos_Usuarios'
CREATE INDEX [IX_FK_UsuariosPermisos_Usuarios]
ON [dbo].[UsuariosPermisos]
    ([Id_Usuario]);
GO

-- Creating foreign key on [Id_Usuario] in table 'UsuariosRoles'
ALTER TABLE [dbo].[UsuariosRoles]
ADD CONSTRAINT [FK_UsuariosRoles_Usuarios]
    FOREIGN KEY ([Id_Usuario])
    REFERENCES [dbo].[Usuarios]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuariosRoles_Usuarios'
CREATE INDEX [IX_FK_UsuariosRoles_Usuarios]
ON [dbo].[UsuariosRoles]
    ([Id_Usuario]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------