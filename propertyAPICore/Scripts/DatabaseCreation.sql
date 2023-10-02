CREATE TABLE [dbo].[propertyForSale](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OwnerEmail] [varchar] (50) NOT NULL FOREIGN KEY REFERENCES [dbo].[user]([email]),
	[Phone] [varchar](50) NULL,	
	[title] [varchar] (max) NULL,
	[location] [varchar] (max) NULL,
	[area] [varchar] (max) NULL,
	[description] [varchar] (max) NULL,
	[price] [int] NULL,
	[latitude] [decimal] (9, 6) NULL,
	[longitude] [decimal] (9, 6) NULL
 CONSTRAINT [ID] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

USE [PropertyDatabase]
GO

/****** Object:  Table [dbo].[user]    Script Date: 9/20/23 10:04:38 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[user](
	[email] [varchar](50) NOT NULL,
	[password] [varchar](50) NULL,
	[name] [varchar](50) NULL,
	[phone] [varchar](50) NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


