CREATE TABLE [dbo].[Fansubs] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Acronym]          NVARCHAR (450)   NOT NULL,
    [Name]             NVARCHAR (450)   NOT NULL,
    [Webpage]          NVARCHAR (MAX)   NULL,
    [CreationDate]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ModificationDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Fansubs] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE TABLE [dbo].[Permission] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Grant]            INT              NOT NULL,
    [CreationDate]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ModificationDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Permission] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE TABLE [dbo].[Users] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Auth0ID]          NVARCHAR (450)   NOT NULL,
    [Name]             NVARCHAR (450)   NOT NULL,
    [CreationDate]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ModificationDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE TABLE [dbo].[Animes] (
    [ID]                    UNIQUEIDENTIFIER NOT NULL,
    [KitsuID]               INT              NOT NULL,
    [Slug]                  NVARCHAR (MAX)   NOT NULL,
    [Name]                  NVARCHAR (MAX)   NOT NULL,
    [Season]                INT              NOT NULL,
    [Status]                INT              NOT NULL,
    [StartDate]             DATETIME2 (7)    NOT NULL,
    [EndDate]               DATETIME2 (7)    NULL,
    [Synopsis]              NVARCHAR (MAX)   NULL,
    [CoverImages_Tiny]      NVARCHAR (MAX)   NULL,
    [CoverImages_Small]     NVARCHAR (MAX)   NULL,
    [CoverImages_Original]  NVARCHAR (MAX)   NULL,
    [PosterImages_Tiny]     NVARCHAR (MAX)   NULL,
    [PosterImages_Small]    NVARCHAR (MAX)   NULL,
    [PosterImages_Original] NVARCHAR (MAX)   NULL,
    [CreationDate]          DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ModificationDate]      DATETIME2 (7)    NULL,
    [AniDBID]               INT              NULL,
    [AniListID]             INT              NULL,
    [MyAnimeListID]         INT              NULL,
    CONSTRAINT [PK_Animes] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE TABLE [dbo].[Subtitles] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Status]           INT              NOT NULL,
    [Format]           INT              NOT NULL,
    [Url]              NVARCHAR (MAX)   NOT NULL,
    [EpisodeID]        UNIQUEIDENTIFIER NOT NULL,
    [CreationDate]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ModificationDate] DATETIME2 (7)    NULL,
    [MembershipID]     UNIQUEIDENTIFIER DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    [Language]         INT              DEFAULT (0) NOT NULL,
    CONSTRAINT [PK_Subtitles] PRIMARY KEY CLUSTERED ([EpisodeID] ASC, [MembershipID] ASC),
    CONSTRAINT [FK_Subtitles_Episodes_EpisodeID] FOREIGN KEY ([EpisodeID]) REFERENCES [dbo].[Episodes] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Subtitles_Memberships_MembershipID] FOREIGN KEY ([MembershipID]) REFERENCES [dbo].[Memberships] ([ID]) ON DELETE CASCADE
);


GO

CREATE TABLE [dbo].[DataProtectionKeys] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [FriendlyName] NVARCHAR (MAX) NULL,
    [Xml]          NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_DataProtectionKeys] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO

CREATE TABLE [dbo].[Memberships] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [UserID]           UNIQUEIDENTIFIER NOT NULL,
    [RoleID]           UNIQUEIDENTIFIER NOT NULL,
    [FansubID]         UNIQUEIDENTIFIER NOT NULL,
    [CreationDate]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ModificationDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Memberships] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Memberships_FansubRoles_RoleID_FansubID] FOREIGN KEY ([RoleID], [FansubID]) REFERENCES [dbo].[FansubRoles] ([ID], [FansubID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Memberships_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID]) ON DELETE CASCADE
);


GO

CREATE TABLE [dbo].[FansubRoles] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Name]             NVARCHAR (MAX)   NOT NULL,
    [FansubID]         UNIQUEIDENTIFIER NOT NULL,
    [CreationDate]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ModificationDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_FansubRoles] PRIMARY KEY CLUSTERED ([ID] ASC, [FansubID] ASC),
    CONSTRAINT [FK_FansubRoles_Fansubs_FansubID] FOREIGN KEY ([FansubID]) REFERENCES [dbo].[Fansubs] ([ID]) ON DELETE CASCADE
);


GO

CREATE TABLE [dbo].[Bookmarks] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [AnimeID]          UNIQUEIDENTIFIER NOT NULL,
    [UserID]           UNIQUEIDENTIFIER NOT NULL,
    [CreationDate]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ModificationDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Bookmarks] PRIMARY KEY CLUSTERED ([AnimeID] ASC, [UserID] ASC),
    CONSTRAINT [FK_Bookmarks_Animes_AnimeID] FOREIGN KEY ([AnimeID]) REFERENCES [dbo].[Animes] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookmarks_Users_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([ID]) ON DELETE CASCADE
);


GO

CREATE TABLE [dbo].[Episodes] (
    [ID]               UNIQUEIDENTIFIER NOT NULL,
    [Number]           INT              NOT NULL,
    [Name]             NVARCHAR (MAX)   NULL,
    [Aired]            DATETIME2 (7)    NULL,
    [Duration]         INT              NULL,
    [AnimeID]          UNIQUEIDENTIFIER NOT NULL,
    [CreationDate]     DATETIME2 (7)    DEFAULT (getutcdate()) NOT NULL,
    [ModificationDate] DATETIME2 (7)    NULL,
    CONSTRAINT [PK_Episodes] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Episodes_Animes_AnimeID] FOREIGN KEY ([AnimeID]) REFERENCES [dbo].[Animes] ([ID]) ON DELETE CASCADE
);


GO

CREATE TABLE [dbo].[FansubRolePermission] (
    [PermissionsID]       UNIQUEIDENTIFIER NOT NULL,
    [FansubRolesID]       UNIQUEIDENTIFIER NOT NULL,
    [FansubRolesFansubID] UNIQUEIDENTIFIER DEFAULT ('00000000-0000-0000-0000-000000000000') NOT NULL,
    CONSTRAINT [PK_FansubRolePermission] PRIMARY KEY CLUSTERED ([PermissionsID] ASC, [FansubRolesID] ASC, [FansubRolesFansubID] ASC),
    CONSTRAINT [FK_FansubRolePermission_FansubRoles_FansubRolesID_FansubRolesFansubID] FOREIGN KEY ([FansubRolesID], [FansubRolesFansubID]) REFERENCES [dbo].[FansubRoles] ([ID], [FansubID]) ON DELETE CASCADE,
    CONSTRAINT [FK_FansubRolePermission_Permission_PermissionsID] FOREIGN KEY ([PermissionsID]) REFERENCES [dbo].[Permission] ([ID]) ON DELETE CASCADE
);


GO

CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
);


GO

CREATE NONCLUSTERED INDEX [IX_Subtitles_MembershipID]
    ON [dbo].[Subtitles]([MembershipID] ASC);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Memberships_UserID_FansubID]
    ON [dbo].[Memberships]([UserID] ASC, [FansubID] ASC);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Fansubs_Name]
    ON [dbo].[Fansubs]([Name] ASC);


GO

CREATE NONCLUSTERED INDEX [IX_FansubRoles_FansubID]
    ON [dbo].[FansubRoles]([FansubID] ASC);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Name]
    ON [dbo].[Users]([Name] ASC);


GO

CREATE NONCLUSTERED INDEX [IX_FansubRolePermission_FansubRolesID_FansubRolesFansubID]
    ON [dbo].[FansubRolePermission]([FansubRolesID] ASC, [FansubRolesFansubID] ASC);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Fansubs_Acronym]
    ON [dbo].[Fansubs]([Acronym] ASC);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Animes_KitsuID]
    ON [dbo].[Animes]([KitsuID] ASC);


GO

CREATE NONCLUSTERED INDEX [IX_Memberships_RoleID_FansubID]
    ON [dbo].[Memberships]([RoleID] ASC, [FansubID] ASC);


GO

CREATE NONCLUSTERED INDEX [IX_Bookmarks_UserID]
    ON [dbo].[Bookmarks]([UserID] ASC);


GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Auth0ID]
    ON [dbo].[Users]([Auth0ID] ASC);


GO

CREATE NONCLUSTERED INDEX [IX_Episodes_AnimeID]
    ON [dbo].[Episodes]([AnimeID] ASC);


GO

