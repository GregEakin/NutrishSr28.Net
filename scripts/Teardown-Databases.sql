USE [master]
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Nutrish')
DROP DATABASE [Nutrish]
GO
