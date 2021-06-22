USE [FlightBooking]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([id], [first_name], [last_name], [address], [phone]) VALUES (1, N'Anup', N'Mehta', N'Pune', N'7531597894')
GO
INSERT [dbo].[Customer] ([id], [first_name], [last_name], [address], [phone]) VALUES (2, N'Vikas', N'Dubey', N'Delhi', N'987463579')
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Passenger] ON 
GO
INSERT [dbo].[Passenger] ([id], [first_name], [last_name]) VALUES (1, N'Roshan', N'Shah')
GO
INSERT [dbo].[Passenger] ([id], [first_name], [last_name]) VALUES (2, N'Rajesh', N'Dewangan')
GO
SET IDENTITY_INSERT [dbo].[Passenger] OFF
GO
SET IDENTITY_INSERT [dbo].[Booking] ON 
GO
INSERT [dbo].[Booking] ([Id], [flight_id], [customer_id], [seats], [seats_type], [fare], [status], [pnr], [Passenger_id]) VALUES (1, 1, 1, N'1', N'Economic', CAST(2500.00 AS Decimal(10, 2)), N'Booked', N'PNR111', 1)
GO
INSERT [dbo].[Booking] ([Id], [flight_id], [customer_id], [seats], [seats_type], [fare], [status], [pnr], [Passenger_id]) VALUES (2, 2, 2, N'1', N'Premium', CAST(5000.00 AS Decimal(10, 2)), N'Check-in', N'PNR222', 2)
GO
SET IDENTITY_INSERT [dbo].[Booking] OFF
GO
SET IDENTITY_INSERT [dbo].[Inventory] ON 
GO
INSERT [dbo].[Inventory] ([ID], [flight_id], [economy_seats], [premium_economy_seats], [business_seats], [first_class_seats]) VALUES (1, 1, 40, 20, 15, 15)
GO
INSERT [dbo].[Inventory] ([ID], [flight_id], [economy_seats], [premium_economy_seats], [business_seats], [first_class_seats]) VALUES (2, 2, 40, 20, 15, 15)
GO
SET IDENTITY_INSERT [dbo].[Inventory] OFF
GO
