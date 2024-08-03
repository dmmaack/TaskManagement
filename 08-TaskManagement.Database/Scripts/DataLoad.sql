USE [TaskManagement]
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [Name], [Email], [UserName], [Password], [RegisterDate], [IsActive], [UserRule]) VALUES (1, N'Primeiro Nome', N'primeironome@email.com', N'primnome', N'P@ssw0rD', CAST(N'2024-07-27T18:42:25.940' AS DateTime), 1, 1)
GO
INSERT [dbo].[Users] ([UserId], [Name], [Email], [UserName], [Password], [RegisterDate], [IsActive], [UserRule]) VALUES (2, N'Segundo Nome', N'segundonome@email.com', N'segnome', N'P@ssw0rD', CAST(N'2024-07-27T18:43:14.563' AS DateTime), 1, 2)
GO
INSERT [dbo].[Users] ([UserId], [Name], [Email], [UserName], [Password], [RegisterDate], [IsActive], [UserRule]) VALUES (4, N'Outro Nome', N'outro@email.com', N'outronome', N'P@ssw0rD', CAST(N'2024-07-27T18:43:56.387' AS DateTime), 1, 2)
GO
INSERT [dbo].[Users] ([UserId], [Name], [Email], [UserName], [Password], [RegisterDate], [IsActive], [UserRule]) VALUES (5, N'Terceiro Nome', N'terceironome@email.com', N'ternome', N'P@ssw0rD', CAST(N'2024-07-27T18:44:25.393' AS DateTime), 1, 2)
GO
INSERT [dbo].[Users] ([UserId], [Name], [Email], [UserName], [Password], [RegisterDate], [IsActive], [UserRule]) VALUES (6, N'criado agora', N'criadoagora@email.com', N'criadomais', N'Ccd3#__ddd', CAST(N'2024-07-27T20:58:00.000' AS DateTime), 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
SET IDENTITY_INSERT [dbo].[Tasks] ON 
GO
INSERT [dbo].[Tasks] ([TaskIdId], [Title], [Description], [StartDate], [EndDate], [RegisterDate], [Status], [Priority], [UserId], [AssignedTo]) VALUES (1, N'Titulo da taqrefa 0001', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley', CAST(N'2024-07-28T21:17:20.000' AS DateTime), CAST(N'2024-07-29T21:17:20.000' AS DateTime), CAST(N'2024-07-27T21:17:20.000' AS DateTime), 1, 1, 2, 1)
GO
INSERT [dbo].[Tasks] ([TaskIdId], [Title], [Description], [StartDate], [EndDate], [RegisterDate], [Status], [Priority], [UserId], [AssignedTo]) VALUES (2, N'Titulo da taqrefa 0002', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry''s standard dummy text ever since the 1500s, when an unknown printer took a galley', CAST(N'2024-07-28T21:17:20.000' AS DateTime), CAST(N'2024-07-29T21:17:20.000' AS DateTime), CAST(N'2024-07-27T21:17:20.000' AS DateTime), 1, 1, 2, 1)
GO
INSERT [dbo].[Tasks] ([TaskIdId], [Title], [Description], [StartDate], [EndDate], [RegisterDate], [Status], [Priority], [UserId], [AssignedTo]) VALUES (4, N'Titulo da taqrefa 0004', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys', CAST(N'2024-07-29T13:09:06.740' AS DateTime), CAST(N'2024-07-29T13:09:06.740' AS DateTime), CAST(N'2024-07-28T13:09:06.740' AS DateTime), 2, 2, 1, 2)
GO
INSERT [dbo].[Tasks] ([TaskIdId], [Title], [Description], [StartDate], [EndDate], [RegisterDate], [Status], [Priority], [UserId], [AssignedTo]) VALUES (6, N'Titulo da taqrefa 0006', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley', CAST(N'2024-07-29T13:09:06.753' AS DateTime), CAST(N'2024-07-29T13:09:06.753' AS DateTime), CAST(N'2024-07-28T13:09:06.753' AS DateTime), 1, 1, 1, 2)
GO
INSERT [dbo].[Tasks] ([TaskIdId], [Title], [Description], [StartDate], [EndDate], [RegisterDate], [Status], [Priority], [UserId], [AssignedTo]) VALUES (7, N'Titulo da taqrefa 0003', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley', CAST(N'2024-07-29T13:09:49.010' AS DateTime), CAST(N'2024-07-29T13:09:49.010' AS DateTime), CAST(N'2024-07-28T13:09:49.010' AS DateTime), 1, 1, 4, 1)
GO
INSERT [dbo].[Tasks] ([TaskIdId], [Title], [Description], [StartDate], [EndDate], [RegisterDate], [Status], [Priority], [UserId], [AssignedTo]) VALUES (8, N'Titulo da taqrefa 0005', N'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industrys standard dummy text ever since the 1500s, when an unknown printer took a galley', CAST(N'2024-07-29T13:09:54.340' AS DateTime), CAST(N'2024-07-29T13:09:54.340' AS DateTime), CAST(N'2024-07-28T13:09:54.340' AS DateTime), 1, 1, 4, 2)
GO
SET IDENTITY_INSERT [dbo].[Tasks] OFF
GO