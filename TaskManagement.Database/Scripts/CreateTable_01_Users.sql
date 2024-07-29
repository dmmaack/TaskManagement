USE [TaskManagement]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 28/07/2024 20:43:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](180) NOT NULL,
	[UserName] [varchar](12) NOT NULL,
	[Password] [varchar](1000) NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[UserRule] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


