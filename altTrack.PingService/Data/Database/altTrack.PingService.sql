SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VehicleStatus](
	[Id] [uniqueidentifier] NOT NULL,
	[VehicleId] [nvarchar](20) NOT NULL,
	[TraceTime] [datetimeoffset](7) NOT NULL,
	[TraceMessage] [nvarchar](50) NOT NULL
)
GO
