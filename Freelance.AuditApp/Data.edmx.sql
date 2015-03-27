
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/30/2014 00:33:27
-- Generated from EDMX file: J:\Git\Freelance.AuditApp\Freelance.AuditApp\Data.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db7d6755988d704c83a223a33a00b36d5a];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__UserProje__UserI__145C0A3F]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProjects] DROP CONSTRAINT [FK__UserProje__UserI__145C0A3F];
GO
IF OBJECT_ID(N'[dbo].[FK__AspectIte__Proje__22AA2996]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspectItems] DROP CONSTRAINT [FK__AspectIte__Proje__22AA2996];
GO
IF OBJECT_ID(N'[dbo].[FK__ProjectAs__Proje__1920BF5C]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ProjectAspects] DROP CONSTRAINT [FK__ProjectAs__Proje__1920BF5C];
GO
IF OBJECT_ID(N'[dbo].[FK__Projects__Create__0EA330E9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK__Projects__Create__0EA330E9];
GO
IF OBJECT_ID(N'[dbo].[FK__UserProje__Proje__1367E606]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserProjects] DROP CONSTRAINT [FK__UserProje__Proje__1367E606];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserProjects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserProjects];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[AspectItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspectItems];
GO
IF OBJECT_ID(N'[dbo].[ProjectAspects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjectAspects];
GO
IF OBJECT_ID(N'[dbo].[Actions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Actions];
GO
IF OBJECT_ID(N'[dbo].[Results]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Results];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserProjects'
CREATE TABLE [dbo].[UserProjects] (
    [UserProjectId] int IDENTITY(1,1) NOT NULL,
    [ProjectId] int  NULL,
    [UserId] int  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [Username] varchar(125)  NULL,
    [UserPassword] varchar(125)  NULL,
    [UserRole] varchar(125)  NULL
);
GO

-- Creating table 'AspectItems'
CREATE TABLE [dbo].[AspectItems] (
    [AspectItemsID] int IDENTITY(1,1) NOT NULL,
    [ProjectAspectID] int  NULL,
    [AspectItem1] varchar(2000)  NULL
);
GO

-- Creating table 'ProjectAspects'
CREATE TABLE [dbo].[ProjectAspects] (
    [ProjectAspectsID] int IDENTITY(1,1) NOT NULL,
    [ProjectID] int  NULL,
    [Aspect] varchar(1400)  NULL
);
GO

-- Creating table 'Actions'
CREATE TABLE [dbo].[Actions] (
    [ActionId] int IDENTITY(1,1) NOT NULL,
    [ProjectId] int  NULL,
    [CreatedBy] int  NULL,
    [Closed] bit  NULL,
    [Priority] varchar(10)  NULL,
    [DueDate] datetime  NULL,
    [CreatedDate] datetime  NULL,
    [UploadedFile] varbinary(50)  NULL,
    [ActionDescription] varchar(max)  NULL,
    [ClosedReason] varchar(max)  NULL,
    [ClosedDate] datetime  NULL,
    [ClosedFile] varbinary(50)  NULL,
    [RaisedBy] varchar(max)  NULL
);
GO

-- Creating table 'Results'
CREATE TABLE [dbo].[Results] (
    [ResultID] int IDENTITY(1,1) NOT NULL,
    [ProjectId] int  NULL,
    [AspectItem] int  NULL,
    [ObservationalComment] varchar(max)  NULL,
    [Satisfactory] varchar(max)  NULL,
    [ConductedBy] varchar(200)  NULL,
    [Auditees] varchar(max)  NULL,
    [WeatherObservations] varchar(max)  NULL,
    [AuditGuid] varchar(max)  NULL,
    [DateRecorded] datetime  NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [ProjectId] int IDENTITY(1,1) NOT NULL,
    [ProjectName] varchar(200)  NULL,
    [CreatedBy] int  NULL,
    [RosterWork] varchar(200)  NULL,
    [Location] varchar(200)  NULL,
    [ProjectNumber] varchar(200)  NULL,
    [RosterBreak] nchar(10)  NULL,
    [StartDate] datetime  NULL,
    [Duration] varchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserProjectId] in table 'UserProjects'
ALTER TABLE [dbo].[UserProjects]
ADD CONSTRAINT [PK_UserProjects]
    PRIMARY KEY CLUSTERED ([UserProjectId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [AspectItemsID] in table 'AspectItems'
ALTER TABLE [dbo].[AspectItems]
ADD CONSTRAINT [PK_AspectItems]
    PRIMARY KEY CLUSTERED ([AspectItemsID] ASC);
GO

-- Creating primary key on [ProjectAspectsID] in table 'ProjectAspects'
ALTER TABLE [dbo].[ProjectAspects]
ADD CONSTRAINT [PK_ProjectAspects]
    PRIMARY KEY CLUSTERED ([ProjectAspectsID] ASC);
GO

-- Creating primary key on [ActionId] in table 'Actions'
ALTER TABLE [dbo].[Actions]
ADD CONSTRAINT [PK_Actions]
    PRIMARY KEY CLUSTERED ([ActionId] ASC);
GO

-- Creating primary key on [ResultID] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [PK_Results]
    PRIMARY KEY CLUSTERED ([ResultID] ASC);
GO

-- Creating primary key on [ProjectId] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([ProjectId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'UserProjects'
ALTER TABLE [dbo].[UserProjects]
ADD CONSTRAINT [FK__UserProje__UserI__145C0A3F]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__UserProje__UserI__145C0A3F'
CREATE INDEX [IX_FK__UserProje__UserI__145C0A3F]
ON [dbo].[UserProjects]
    ([UserId]);
GO

-- Creating foreign key on [ProjectAspectID] in table 'AspectItems'
ALTER TABLE [dbo].[AspectItems]
ADD CONSTRAINT [FK__AspectIte__Proje__22AA2996]
    FOREIGN KEY ([ProjectAspectID])
    REFERENCES [dbo].[ProjectAspects]
        ([ProjectAspectsID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__AspectIte__Proje__22AA2996'
CREATE INDEX [IX_FK__AspectIte__Proje__22AA2996]
ON [dbo].[AspectItems]
    ([ProjectAspectID]);
GO

-- Creating foreign key on [ProjectID] in table 'ProjectAspects'
ALTER TABLE [dbo].[ProjectAspects]
ADD CONSTRAINT [FK__ProjectAs__Proje__1920BF5C]
    FOREIGN KEY ([ProjectID])
    REFERENCES [dbo].[Projects]
        ([ProjectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__ProjectAs__Proje__1920BF5C'
CREATE INDEX [IX_FK__ProjectAs__Proje__1920BF5C]
ON [dbo].[ProjectAspects]
    ([ProjectID]);
GO

-- Creating foreign key on [CreatedBy] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [FK__Projects__Create__0EA330E9]
    FOREIGN KEY ([CreatedBy])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__Projects__Create__0EA330E9'
CREATE INDEX [IX_FK__Projects__Create__0EA330E9]
ON [dbo].[Projects]
    ([CreatedBy]);
GO

-- Creating foreign key on [ProjectId] in table 'UserProjects'
ALTER TABLE [dbo].[UserProjects]
ADD CONSTRAINT [FK__UserProje__Proje__1367E606]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Projects]
        ([ProjectId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK__UserProje__Proje__1367E606'
CREATE INDEX [IX_FK__UserProje__Proje__1367E606]
ON [dbo].[UserProjects]
    ([ProjectId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------