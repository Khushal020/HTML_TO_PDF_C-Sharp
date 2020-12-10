CREATE TABLE [dbo].[Table]
(
	[Bill_ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [GST_NO] VARCHAR(20) NOT NULL, 
    [Address] NTEXT NOT NULL,
	[Date] DATE NOT NULL,
    [Product_Title] VARCHAR(50) NOT NULL, 
    [Qauntity] INT NOT NULL, 
    [Total] FLOAT NOT NULL 
)
