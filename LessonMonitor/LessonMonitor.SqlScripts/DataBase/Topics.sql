﻿CREATE TABLE [dbo].[Topics]
(
	[Id] INT NOT NULL, 
    [Theme] NVARCHAR(50) NULL, 
    CONSTRAINT [PK_Topics] PRIMARY KEY CLUSTERED ([Id] ASC),
)
