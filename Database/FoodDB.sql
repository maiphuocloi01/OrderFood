CREATE DATABASE FoodAppDb
GO
USE FoodAppDb
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [float] NOT NULL,
	[Qty] [int] NOT NULL,
	[TotalAmount] [float] NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[OrderTotal] [float] NOT NULL,
	[OrderPlaced] [datetime2](7) NOT NULL,
	[IsOrderCompleted] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Detail] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Price] [float] NOT NULL,
	[IsPopularProduct] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShoppingCartItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Price] [float] NOT NULL,
	[Qty] [int] NOT NULL,
	[TotalAmount] [float] NOT NULL,
	[ProductId] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
 CONSTRAINT [PK_ShoppingCartItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

create table Advertisement
(
	ID int primary key identity(1,1),
	Image varchar(500),
)
go

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[Role] [nvarchar](max) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Orders_OrderId]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products_ProductId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users_UserId]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories_CategoryId]
GO
ALTER TABLE [dbo].[ShoppingCartItems]  WITH CHECK ADD  CONSTRAINT [FK_ShoppingCartItems_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShoppingCartItems] CHECK CONSTRAINT [FK_ShoppingCartItems_Products_ProductId]
GO

SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([Id], [Name], [ImageUrl]) VALUES (2, N'Chicken Fried', N'chicken.png')
INSERT [dbo].[Categories] ([Id], [Name], [ImageUrl]) VALUES (3, N'Fries', N'french_fries.png')
INSERT [dbo].[Categories] ([Id], [Name], [ImageUrl]) VALUES (4, N'Burgers', N'hamburger.png')
INSERT [dbo].[Categories] ([Id], [Name], [ImageUrl]) VALUES (5, N'Hot Dog', N'hot_dog.png')
INSERT [dbo].[Categories] ([Id], [Name], [ImageUrl]) VALUES (6, N'Pizza', N'pizza.png')
INSERT [dbo].[Categories] ([Id], [Name], [ImageUrl]) VALUES (7, N'Popcorn', N'popcorn.png')
INSERT [dbo].[Categories] ([Id], [Name], [ImageUrl]) VALUES (8, N'Drinks', N'drink.png')

SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (1, N'Masala Fried Chicken', N'The vibrant flavor and color of this spicy, finger-licking-good fried chicken comes from an abundance of ground dried spices, so be sure to use the freshest ones you can find. If a jar has been sitting in your cabinet for more than six months, it???s probably time to re-up. Trust us, it???ll make all the difference in the world. This recipe is from Chintan Pandya, executive chef of Adda in New York.', N'chicken1.jpg', 8, 1, 2)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (2, N'Crispy Fried Chicken', N'Southern fried chicken is a dish consisting of chicken pieces that have been coated with seasoned flour or batter and pan-fried, deep fried, pressure fried, or air fried. The breading adds a crisp coating or crust to the exterior of the chicken while retaining juices in the meat.', N'chicken2.jpg', 5, 0, 2)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (3, N'Buttermilk Fried Chicken', N'Soaking chicken or other meats in buttermilk make the meat tender. While harsher acids like lemon juice or vinegar can tenderize, they can also dry out the meat. But soaking chicken in buttermilk helps the chicken stay juicy while tenderizing the meat.', N'chicken3.jpg', 10, 0, 2)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (4, N'Chicken Fried Steak With White Gravy', N'Chicken Fried Steak with Sawmill Gravy is a true Texas meal. Delicious cube steak in a crunchy, flavorful breading and delicious white gravy!', N'chicken4.jpg', 10, 1, 2)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (5, N'Keto Fried Chicken', N'To make a Keto Friendly fried chicken we skipped the flour and went for pork rinds and Parmesan. Almond flour helps adhere everything together as well for a perfectly crisp chicken.', N'chicken5.jpg', 6, 0, 2)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (7, N'French Fries', N'A thin strip of potato, usually cut 3 to 4 inches in length and about 1/4 to 3/8 inches square that are deep fried until they are golden brown and crisp textured on the outside while remaining white and soft on the inside', N'french_fries1.jpg', 5, 1, 3)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (8, N'Zinger Burger', N'Our simple, succulent 100% chicken breast Fillet burger, plus spine-tingling Zing. With regular fries and a regular drink.', N' hamburger1.jpg', 6, 1, 4)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (9, N'Hot Dog Bar', N'A hot dog is a food consisting of a grilled or steamed sausage served in the slit of a partially sliced bun. The term hot dog can also refer to the sausage itself.', N'hot_dog1.jpg', 7, 1, 5)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (10, N'Neapolitan Pizza', N'Also known as pizza Napolitana or Naples-style pizza, Neapolitan is a traditional type of pizza that originated in Naples, Italy. It features a thin crust and basic, but very fresh topping. This makes for a delicious, harmonized taste that melts on the palette without various different flavors competing with one another.', N'pizza1.jpg', 10, 1, 6)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (11, N'Popcorn', N'Popcorn is a variety of corn kernel which expands and puffs up when heated; the same names are also used to refer to the foodstuff produced by the expansion.', N'popcorn1.jpg', 4, 1, 7)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (12, N'Cocacola', N'Coca-Cola, or Coke, is a carbonated soft drink manufactured by The Coca-Cola Company.', N' drink1.jpg', 3, 1, 8)

INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (6, N'Crinkle-Cut Fries', N'This wavy cut is a result of two-step process that involves pushing pre-soaked potatoes lengthwise on a mandolin outfitted with a special cutter. Resulting in a product that is crisper, and perhaps tastier', N'french_fries2.jpg', 5, 1, 3)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (13, N'Matchstick Fries', N'It???s the matchstick shape and high temperature that helps them cook faster and get that golden brown crisp.', N'french_fries3.jpg', 5, 0, 3)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (14, N'Cheese Fries', N'A dish consisting of French fries covered in cheese, with the possible addition of various other toppings. Cheese fries are generally served as a lunch- or dinner-time meal. They can be found in fast-food locations, diners, and grills around the globe.', N'french_fries4.jpg', 5, 0, 3)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (15, N'Sweet Potato Fries', N'Fried sweet potato features in a variety of dishes and cuisines including the popular sweet potato fries, a variation of French fries using sweet potato instead of potato. Fried sweet potatoes are known as patates in Guinean cuisine, where they are more popular than potatoes and more commonly used to make fries.', N'french_fries5.jpg', 5, 0, 3)

INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (16, N'Double CheeseBurger', N'Features two 100% pure beef burger patties seasoned with just a pinch of salt and pepper. It is topped with tangy pickles, chopped onions, ketchup, mustard and two slices of melty American cheese. ', N' hamburger2.jpg', 7, 0, 4)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (17, N'Shrimp Burger', N'This easy shrimp burger brings together crispy shrimp patties and melted cheese wrapped around buttery brioche buns to give a burst of flavor in every bite.', N' hamburger3.jpg', 7, 0, 4)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (18, N'Double Hamburger', N'contains two beef patties, sauce, lettuce, cheese, pickles, and onions on a three-piece sesame seed bun.', N' hamburger4.jpg', 8, 1, 4)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (19, N'Fish Burger', N'This burger has fish sourced from sustainably managed fisheries, on melty American cheese and topped with creamy tartar sauce, all served on a soft, steamed bun.', N' hamburger5.jpg', 6, 0, 4)

INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (20, N'Chicago-style Hot Dog', N'Started with a steamed poppy seed bun and an all-beef frankfurter. Then it is topped with yellow mustard, bright green relish, fresh chopped onions, juicy red tomato wedges, a kosher-style pickle spear, a couple of spicy sport peppers and finally, a dash of celery salt.', N'hot_dog2.jpg', 6, 0, 5)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (21, N'Completo', N' served with ingredients such as chopped tomatoes, avocados, mayonnaise, sauerkraut, salsa Americana, aj?? pepper and green sauce. It can be twice the size of an American hot dog.', N'hot_dog3.jpg', 6, 0, 5)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (22, N'Danger Dog', N'Bacon-wrapped grilled hot dogs topped with mustard, ketchup, mayo, grilled onions and peppers, fresh pico de gallo, and often topped with a grilled jalape??o pepper.', N'hot_dog4.jpg', 6, 0, 5)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (23, N'Bagel Dog', N'A simple yeast dough is wrapped around hot dogs. The dough wrapped dogs are then briefly dipped in boiling water, brushed with egg and then baked in the oven.', N'hot_dog5.jpg', 6, 0, 5)

INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (24, N'Chigaco Pizza', N'The pan in which it is baked gives the pizza its characteristically high edge which provides ample space for large amounts of cheese and a chunky tomato sauce. Chicago-style pizza may be prepared in deep-dish style and as a stuffed pizza.', N'pizza2.jpg', 10, 0, 6)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (25, N'New York-style Pizza', N'A pizza made with a characteristically large hand-tossed thin crust, often sold in wide slices to go. The crust is thick and crisp only along its edge, yet soft, thin, and pliable enough beneath its toppings to be folded in half to eat. Traditional toppings are simply tomato sauce and shredded mozzarella cheese.', N'pizza3.jpg', 12, 1, 6)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (26, N'Sicilian Pizza', N'Traditional Sicilian pizza is often thick crusted and rectangular, but can also be round and similar to the Neapolitan pizza. It is often topped with onions, anchovies, tomatoes, herbs and strong cheese such as caciocavallo and tomato. Other versions do not include cheese.', N'pizza4.jpg', 11, 0, 6)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (27, N'Greek Pizza', N'the pizza is proofed and cooked in a metal pan rather than stretched to order and baked on the floor of the pizza oven. A shallow pan is used, unlike the deep pans used in Sicilian, Chicago, or Detroit-styled pizzas. Its crust is typically spongy, airy, and light, like focaccia but not as thick.', N'pizza5.jpg', 10, 0, 6)

INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (28, N'Caramel Popcorn', N'Popcorn cover with a layer of caramelized sugar', N'popcorn2.jpg', 4, 0, 7)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (29, N'Cheese Popcorn', N'Popcorn cover with a layer of cheese dust', N'popcorn3.jpg', 4, 0, 7)

INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (30, N'Pepsi', N'A cool and refreshing drink to complement any meal', N'drink2.jpg', 3, 0, 8)
INSERT [dbo].[Products] ([Id], [Name], [Detail], [ImageUrl], [Price], [IsPopularProduct], [CategoryId]) VALUES (31, N'Fanta', N'Sweet and fruity with a class of Fanta', N'drink3.jpg', 3, 0, 8)


SET IDENTITY_INSERT [dbo].[Products] OFF

insert into Advertisement values('img01.jpg')
insert into Advertisement values('img02.jpg')
insert into Advertisement values('img03.jpg')

GO

