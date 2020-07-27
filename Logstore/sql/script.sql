USE [Logstore]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 7/27/2020 1:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Street] [varchar](100) NOT NULL,
	[Number] [varchar](10) NOT NULL,
	[Complement] [varchar](40) NULL,
	[Neighborhood] [varchar](40) NOT NULL,
	[City] [varchar](40) NOT NULL,
	[PostalCode] [varchar](9) NOT NULL,
	[UF] [varchar](2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Flavor]    Script Date: 7/27/2020 1:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Flavor](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Price] [numeric](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 7/27/2020 1:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[DateCreate] [datetime] NOT NULL,
	[Total] [numeric](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderAddressDelivery]    Script Date: 7/27/2020 1:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderAddressDelivery](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
	[Street] [varchar](100) NOT NULL,
	[Number] [varchar](10) NOT NULL,
	[Complement] [varchar](40) NULL,
	[Neighborhood] [varchar](40) NOT NULL,
	[City] [varchar](40) NOT NULL,
	[PostalCode] [varchar](9) NOT NULL,
	[UF] [varchar](2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 7/27/2020 1:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItemFlavor]    Script Date: 7/27/2020 1:27:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItemFlavor](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderItemId] [bigint] NOT NULL,
	[FlavorId] [bigint] NOT NULL,
	[Value] [numeric](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_ORDER_CUSTOMER_CUSTOMERID] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_ORDER_CUSTOMER_CUSTOMERID]
GO
ALTER TABLE [dbo].[OrderAddressDelivery]  WITH CHECK ADD  CONSTRAINT [FK_ORDERADDRESSDELIVERY_ORDER_ORDERID] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderAddressDelivery] CHECK CONSTRAINT [FK_ORDERADDRESSDELIVERY_ORDER_ORDERID]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_ORDERITEM_ORDER_ORDERID] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_ORDERITEM_ORDER_ORDERID]
GO
ALTER TABLE [dbo].[OrderItemFlavor]  WITH CHECK ADD  CONSTRAINT [FK_ORDERITEMFLAVOR_FLAVOR_FLAVORID] FOREIGN KEY([FlavorId])
REFERENCES [dbo].[Flavor] ([Id])
GO
ALTER TABLE [dbo].[OrderItemFlavor] CHECK CONSTRAINT [FK_ORDERITEMFLAVOR_FLAVOR_FLAVORID]
GO
ALTER TABLE [dbo].[OrderItemFlavor]  WITH CHECK ADD  CONSTRAINT [FK_ORDERITEMFLAVOR_ORDERITEM_ORDERITEMID] FOREIGN KEY([OrderItemId])
REFERENCES [dbo].[OrderItem] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItemFlavor] CHECK CONSTRAINT [FK_ORDERITEMFLAVOR_ORDERITEM_ORDERITEMID]
GO
