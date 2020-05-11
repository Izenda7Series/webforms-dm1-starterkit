USE [WebFormsBEStandalone]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 5/9/2017 4:26:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 5/9/2017 4:26:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 5/9/2017 4:26:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 5/9/2017 4:26:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 5/9/2017 4:26:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Tenant_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tenants]    Script Date: 5/9/2017 4:26:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tenants](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Tenants] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'732E6E70-84B9-4531-A75A-E729E0F9D941', N'Employee')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'7EA0B2FF-5A45-4E70-8F80-887E100127C1', N'Manager')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'91FF0C14-7E34-4C71-974B-F2362F1BFEF6', N'VP')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'A255F9B0-F2CB-4E5B-8D2A-05CECE4889A2', N'Administrator')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'446f596a-b7ac-4d2e-9814-b96249e10796', N'7EA0B2FF-5A45-4E70-8F80-887E100127C1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'49529006-1142-40be-bacb-55793909d7dd', N'7EA0B2FF-5A45-4E70-8F80-887E100127C1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4d83f471-24c2-4f7c-8f84-f5ac195cb65a', N'732E6E70-84B9-4531-A75A-E729E0F9D941')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5de4e37e-ef25-4979-8cbe-303ea3eb2554', N'732E6E70-84B9-4531-A75A-E729E0F9D941')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'95087a84-a231-430b-a339-e72054888bb1', N'A255F9B0-F2CB-4E5B-8D2A-05CECE4889A2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a6155e91-f367-422a-9c4e-af19fe50e3ba', N'732E6E70-84B9-4531-A75A-E729E0F9D941')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'bada6fa4-2a35-4868-b6b0-7567b659f719', N'7EA0B2FF-5A45-4E70-8F80-887E100127C1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'd36d16e8-1246-481b-b8e0-fa1a0205f150', N'91FF0C14-7E34-4C71-974B-F2362F1BFEF6')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'e704cd24-271f-4edc-80de-e88e0af3b5f3', N'91FF0C14-7E34-4C71-974B-F2362F1BFEF6')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'eae7c07f-3a3d-494e-ae0f-ceb7e513dcad', N'7EA0B2FF-5A45-4E70-8F80-887E100127C1')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f6c80567-6fdc-4145-a527-a4b5a9b1aad7', N'91FF0C14-7E34-4C71-974B-F2362F1BFEF6')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'446f596a-b7ac-4d2e-9814-b96249e10796', N'manager@natwr.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'c7cb2b5c-9eeb-455a-a54a-93ddc4e306f3', NULL, 0, 0, NULL, 1, 0, N'manager@natwr.com', 3)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'49529006-1142-40be-bacb-55793909d7dd', N'manager@deldg.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'68702f74-bd3c-4d10-b8b4-a04490af978e', NULL, 0, 0, NULL, 1, 0, N'manager@deldg.com', 2)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'4d83f471-24c2-4f7c-8f84-f5ac195cb65a', N'employee@retcl.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'f8dcd354-0826-4f82-89e1-63ef0457b699', NULL, 0, 0, NULL, 1, 0, N'employee@retcl.com', 4)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'5de4e37e-ef25-4979-8cbe-303ea3eb2554', N'employee@deldg.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'561cf2a3-4e17-480c-9c12-512347d40871', NULL, 0, 0, NULL, 1, 0, N'employee@deldg.com', 2)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'693c15e5-918b-494c-8197-e4c10f4af46d', N'johndoe@izenda.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'659b633a-a151-401d-9e0f-f3efa873f5f8', NULL, 0, 0, NULL, 1, 0, N'johndoe@izenda.com', 1)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'93622e07-51e6-4a38-86d8-07e05142c28e', N'testdeldg@system.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'33b5aca3-ef60-409e-98cc-96229935c7b3', NULL, 0, 0, NULL, 1, 0, N'testdeldg@system.com', 4)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'93b5dc2d-3ed6-4a8a-93ca-ea7136418fc2', N'joedoe1@system.com', 0, N'ABBmugdCq2v//yc4a41thYzRe546tI2oMydHyaHiJ6F89r57bwtJ3Jr9ruVySeSx7w==', N'a9d23677-900b-4f5d-b4c3-83d7e9642bda', NULL, 0, 0, NULL, 1, 0, N'joedoe1@system.com', 1)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'95087a84-a231-430b-a339-e72054888bb1', N'IzendaAdmin@system.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'fce856a8-bb02-474f-a371-fc8db7e82d5e', NULL, 0, 0, NULL, 1, 0, N'IzendaAdmin@system.com', 1)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'a6155e91-f367-422a-9c4e-af19fe50e3ba', N'employee@natwr.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'1047ba84-ab9f-40ad-ac75-9e866ed01283', NULL, 0, 0, NULL, 1, 0, N'employee@natwr.com', 3)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'b723d397-2317-4b3c-822c-592e0045cc7b', N'myemail@systm.com', 0, N'ALBFBvLF6SYGzUxKv1HCDtcrTF9/3Q7MT7W7ZZPwluDp/XjLs/lWJ+eGE+DEDzy45Q==', N'60ea33da-5932-4f8e-8e5e-a4dda3bb990c', NULL, 0, 0, NULL, 1, 0, N'myemail@systm.com', 3)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'bada6fa4-2a35-4868-b6b0-7567b659f719', N'natwr@natwr.com', 0, N'AMctGcxJll3muS6d1ch65470x0Qp7LPQa3chVo3gO760jwT7lsUZgE45uYr1xcW5tg==', N'5a0bc273-efc3-4ac1-be09-2a72437d2915', NULL, 0, 0, NULL, 1, 0, N'natwr@natwr.com', 3)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'd36d16e8-1246-481b-b8e0-fa1a0205f150', N'vp@deldg.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'ac216a19-668d-4e3e-9d82-73d1b9bd29ca', NULL, 0, 0, NULL, 1, 0, N'vp@deldg.com', 2)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'e704cd24-271f-4edc-80de-e88e0af3b5f3', N'vp@retcl.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'4b27ca91-549e-4024-9b22-44f17baa33b1', NULL, 0, 0, NULL, 1, 0, N'vp@retcl.com', 4)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'eae7c07f-3a3d-494e-ae0f-ceb7e513dcad', N'manager@retcl.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'6743e1df-7571-4ca4-9004-540f3c27f280', NULL, 0, 0, NULL, 1, 0, N'manager@retcl.com', 4)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'f6c80567-6fdc-4145-a527-a4b5a9b1aad7', N'VP@natwr.com', 0, N'ADFoIj+1YECZ3N2jLRiz6urUkZObKIl2aFJDbK4ARdf8T3q2WhFpexNzbgT0o3l9fQ==', N'bcdbcbd6-74c3-4e2d-9fed-b5ae3c0d32d4', NULL, 0, 0, NULL, 1, 0, N'VP@natwr.com', 3)
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Tenant_Id]) VALUES (N'fb1dbcc2-7780-47c9-98c1-d88faa62dfab', N'joedoe@system.com', 0, N'AKbNLAQXJ5tBuXzUH4E/zsnPh7+HKzkzZQEwEHsqcxGeYZCezrCyGTQUGq3qAfmyfA==', N'b737c00f-9984-4b39-8a09-b72919811d41', NULL, 0, 0, NULL, 1, 0, N'joedoe@system.com', 1)
SET IDENTITY_INSERT [dbo].[Tenants] ON 

INSERT [dbo].[Tenants] ([Id], [Name]) VALUES (1, N'System')
INSERT [dbo].[Tenants] ([Id], [Name]) VALUES (2, N'DELDG')
INSERT [dbo].[Tenants] ([Id], [Name]) VALUES (3, N'NATWR')
INSERT [dbo].[Tenants] ([Id], [Name]) VALUES (4, N'RETCL')
SET IDENTITY_INSERT [dbo].[Tenants] OFF
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUsers_dbo.Tenants_Tenant_Id] FOREIGN KEY([Tenant_Id])
REFERENCES [dbo].[Tenants] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_dbo.AspNetUsers_dbo.Tenants_Tenant_Id]
GO
USE [master]
GO
ALTER DATABASE [WebFormsBEStandalone] SET  READ_WRITE 
GO