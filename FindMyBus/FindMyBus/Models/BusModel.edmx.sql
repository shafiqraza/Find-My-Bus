
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/02/2020 17:18:13
-- Generated from EDMX file: C:\Users\92322\Desktop\FindMyBus\FindMyBus\Models\BusModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FindMyBus];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LoginLoginRoles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LoginRoles] DROP CONSTRAINT [FK_LoginLoginRoles];
GO
IF OBJECT_ID(N'[dbo].[FK_LoginBusBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BusBookings] DROP CONSTRAINT [FK_LoginBusBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_BusBusBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BusBookings] DROP CONSTRAINT [FK_BusBusBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_BusBusRoutes]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BusRoutes] DROP CONSTRAINT [FK_BusBusRoutes];
GO
IF OBJECT_ID(N'[dbo].[FK_BusRoutesBusBooking]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BusBookings] DROP CONSTRAINT [FK_BusRoutesBusBooking];
GO
IF OBJECT_ID(N'[dbo].[FK_BusDriver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Drivers] DROP CONSTRAINT [FK_BusDriver];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO
IF OBJECT_ID(N'[dbo].[LoginRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginRoles];
GO
IF OBJECT_ID(N'[dbo].[BusBookings]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BusBookings];
GO
IF OBJECT_ID(N'[dbo].[Buses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Buses];
GO
IF OBJECT_ID(N'[dbo].[BusRoutes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BusRoutes];
GO
IF OBJECT_ID(N'[dbo].[Drivers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Drivers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Contact] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LoginRoles'
CREATE TABLE [dbo].[LoginRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Role] nvarchar(max)  NOT NULL,
    [LoginId] int  NOT NULL
);
GO

-- Creating table 'BusBookings'
CREATE TABLE [dbo].[BusBookings] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NoOfSeats] nvarchar(max)  NOT NULL,
    [Status] nvarchar(max)  NOT NULL,
    [DateTime] datetime  NOT NULL,
    [LoginId] int  NOT NULL,
    [BusId] int  NOT NULL,
    [BusRoutesId] int  NOT NULL
);
GO

-- Creating table 'Buses'
CREATE TABLE [dbo].[Buses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Number] nvarchar(max)  NOT NULL,
    [TotalSeats] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BusRoutes'
CREATE TABLE [dbo].[BusRoutes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Depature] nvarchar(max)  NOT NULL,
    [Arrival] nvarchar(max)  NOT NULL,
    [DepatureDataTime] datetime  NOT NULL,
    [ArrivalDateTime] datetime  NOT NULL,
    [Price] nvarchar(max)  NOT NULL,
    [BusId] int  NOT NULL
);
GO

-- Creating table 'Drivers'
CREATE TABLE [dbo].[Drivers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [ContactNo] nvarchar(max)  NOT NULL,
    [BusId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoginRoles'
ALTER TABLE [dbo].[LoginRoles]
ADD CONSTRAINT [PK_LoginRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BusBookings'
ALTER TABLE [dbo].[BusBookings]
ADD CONSTRAINT [PK_BusBookings]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Buses'
ALTER TABLE [dbo].[Buses]
ADD CONSTRAINT [PK_Buses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BusRoutes'
ALTER TABLE [dbo].[BusRoutes]
ADD CONSTRAINT [PK_BusRoutes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [PK_Drivers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [LoginId] in table 'LoginRoles'
ALTER TABLE [dbo].[LoginRoles]
ADD CONSTRAINT [FK_LoginLoginRoles]
    FOREIGN KEY ([LoginId])
    REFERENCES [dbo].[Logins]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoginLoginRoles'
CREATE INDEX [IX_FK_LoginLoginRoles]
ON [dbo].[LoginRoles]
    ([LoginId]);
GO

-- Creating foreign key on [LoginId] in table 'BusBookings'
ALTER TABLE [dbo].[BusBookings]
ADD CONSTRAINT [FK_LoginBusBooking]
    FOREIGN KEY ([LoginId])
    REFERENCES [dbo].[Logins]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoginBusBooking'
CREATE INDEX [IX_FK_LoginBusBooking]
ON [dbo].[BusBookings]
    ([LoginId]);
GO

-- Creating foreign key on [BusId] in table 'BusBookings'
ALTER TABLE [dbo].[BusBookings]
ADD CONSTRAINT [FK_BusBusBooking]
    FOREIGN KEY ([BusId])
    REFERENCES [dbo].[Buses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BusBusBooking'
CREATE INDEX [IX_FK_BusBusBooking]
ON [dbo].[BusBookings]
    ([BusId]);
GO

-- Creating foreign key on [BusId] in table 'BusRoutes'
ALTER TABLE [dbo].[BusRoutes]
ADD CONSTRAINT [FK_BusBusRoutes]
    FOREIGN KEY ([BusId])
    REFERENCES [dbo].[Buses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BusBusRoutes'
CREATE INDEX [IX_FK_BusBusRoutes]
ON [dbo].[BusRoutes]
    ([BusId]);
GO

-- Creating foreign key on [BusRoutesId] in table 'BusBookings'
ALTER TABLE [dbo].[BusBookings]
ADD CONSTRAINT [FK_BusRoutesBusBooking]
    FOREIGN KEY ([BusRoutesId])
    REFERENCES [dbo].[BusRoutes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BusRoutesBusBooking'
CREATE INDEX [IX_FK_BusRoutesBusBooking]
ON [dbo].[BusBookings]
    ([BusRoutesId]);
GO

-- Creating foreign key on [BusId] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [FK_BusDriver]
    FOREIGN KEY ([BusId])
    REFERENCES [dbo].[Buses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BusDriver'
CREATE INDEX [IX_FK_BusDriver]
ON [dbo].[Drivers]
    ([BusId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------