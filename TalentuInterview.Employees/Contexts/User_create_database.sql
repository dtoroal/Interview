USE [master]
GO

/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [talentu]    Script Date: 4/20/2024 8:04:16 PM ******/
CREATE LOGIN [talentu] WITH PASSWORD=N'aCcrFw0PjdnVsbadLuQvFnwHxSqpFWBsRZwEmmIFWRs=', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO

ALTER LOGIN [talentu] DISABLE
GO

ALTER SERVER ROLE [dbcreator] ADD MEMBER [talentu]
GO


