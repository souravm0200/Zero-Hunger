/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [UserId]
      ,[UserName]
      ,[UserType]
      ,[Password]
  FROM [ZeroHunger].[dbo].[User]

