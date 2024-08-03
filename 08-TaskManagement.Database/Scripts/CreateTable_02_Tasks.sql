USE [TaskManagement]
GO

/****** Object:  Table [dbo].[Tasks]    Script Date: 28/07/2024 20:43:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tasks](
	[TaskIdId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](150) NOT NULL,
	[Description] [varchar](5000) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[RegisterDate] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[Priority] [int] NOT NULL,
	[UserId] [bigint] NOT NULL,
	[AssignedTo] [bigint] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskIdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users_AssignedTo] FOREIGN KEY([AssignedTo])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users_AssignedTo]
GO

ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO

ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users_UserId]
GO


