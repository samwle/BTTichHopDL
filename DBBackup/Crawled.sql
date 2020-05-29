USE [Microsoft.eShopOnWeb.CatalogDb]
GO

/****** Object:  Table [dbo].[Crawled]    Script Date: 5/29/2020 10:09:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Crawled](
	[ID] [nvarchar](36) NOT NULL,
	[Model] [nvarchar](200) NULL,
	[Price] [nvarchar](50) NULL,
	[Link] [nvarchar](500) NULL,
	[ImageUrl] [nvarchar](500) NULL,
	[CrawledTime] [datetime] NULL
) ON [PRIMARY]

GO
