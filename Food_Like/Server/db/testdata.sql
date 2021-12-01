USE foodlike;

-- Buyer to seller


INSERT INTO Buyer( FirstName,LastName,Email,PhoneNumber,ProfilePicture,EncryptedPassword)
VALUES('Hanne', 'Hansen', 'hanne@hansen.dk', '4560302010', LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/Hanne.png'), 'abc1234');

SET @hanne_id = LAST_INSERT_ID();

INSERT INTO Address( Line1,City )
VALUES ('BOULEVARDEN 14', '9000 Aalborg');

SET @addr_id = LAST_INSERT_ID();

INSERT INTO Seller( SellerId,AddressId,KitchenPicture)
VALUES (@hanne_id, @addr_id, LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/kitchen.png') );


-- Meal1 italien


INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Spagetti w/chicken', 5, 20,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, Chicken Breast,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/Spaghettichicken.png')
);

SET @meal1_id = LAST_INSERT_ID();

-- Meal2 italien

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Mushroom Carbonara', 2, 10,
"Mushrooms take the place of the traditional cured pork in this super-satisfying vegetarian carbonara recipe.",
'Mushrooms,parsley, egg, shallots,parmesan,Garlic,pasta',
'2021-1-20 18:00:00',
'2021-1-20 18:30:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/MushroomCarbonara.png')
);

SET @meal2_id = LAST_INSERT_ID();

-- Meal3 italien

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Rigatoni With Fennel and Anchovies', 3, 30,
"Something truly magical happens when fennel, garlic, and anchovies get caramelized together.",
'Pasra,Fennel bulbs, anchovy, mint leaves,orange,Garlic,lemon,chicken',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/PastaAnchovies.png')
);

SET @meal3_id = LAST_INSERT_ID();

-- Meal4 italien

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Spagetti w/beef', 2, 30,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, beef,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/Spaghettibeef.png')
);

SET @meal4_id = LAST_INSERT_ID();

-- Meal5 italien

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Spagetti w/meatballs', 6, 40,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, mealballs,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/SpaghettiMeatballs.png')
);

SET @meal5_id = LAST_INSERT_ID();

-- Meal6 asian

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Chicken & tofu noodle soup', 5, 20,
"Fragrant and colourful, also referred to as bean curd, tofu is a good source of protein, calcium and phosphorus, all of which are great for bones. It’s also packed with manganese and thiamin.",
'shallots,garlic, ginger, Chicken thighs,star anise,rice noodles,coriander,tofu',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/Chickentofu.png')
);

SET @meal6_id = LAST_INSERT_ID();

-- Meal7 asian

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'South Asian chicken curry', 4, 25,
"This curry over-delivers on big, bold flavour. Served with fluffy rice to soak up the lovely creamy coconut base, plus a good hit of protein from the tender chicken.",
'pebers,garlic, ginger, Chicken thighs,star anise,rice noodles,coriander,tofu',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/Asianchicken.png')
);

SET @meal7_id = LAST_INSERT_ID();

-- Meal8 danish

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Frikadeller w/potatoes', 4, 34,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, Chicken Breast,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/frikadeller.png')
);

SET @meal8_id = LAST_INSERT_ID();

-- Meal9 danish

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Boller i karry', 7, 20,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, Chicken Breast,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/bollerkarry.png')
);

SET @meal9_id = LAST_INSERT_ID();

-- Meal10 american

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Burger', 2, 15,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, Chicken Breast,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/burger.png')
);

SET @meal10_id = LAST_INSERT_ID();

-- Meal11 american

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Tacos', 6, 50,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, Chicken Breast,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/tacos.png')
);

SET @meal11_id = LAST_INSERT_ID();

-- Meal12 european

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Paella', 8, 22,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, Chicken Breast,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/paella.png')
);

SET @meal12_id = LAST_INSERT_ID();

-- Meal13 european

INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Meat pie', 3, 25,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, Chicken Breast,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/pie.png')
);

SET @meal13_id = LAST_INSERT_ID();




-- Categories

INSERT INTO Category( Titel)
VALUES ('Italian');
SET @cat1_id = LAST_INSERT_ID();

INSERT INTO Category( Titel)
VALUES ('Danish');
SET @cat2_id = LAST_INSERT_ID();

INSERT INTO Category( Titel)
VALUES ('European');
SET @cat3_id = LAST_INSERT_ID();

INSERT INTO Category( Titel)
VALUES ('American');
SET @cat4_id = LAST_INSERT_ID();

INSERT INTO Category( Titel)
VALUES ('Asian');
SET @cat5_id = LAST_INSERT_ID();


-- Meal Category

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal1_id, @cat1_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal2_id, @cat1_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal3_id, @cat1_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal4_id, @cat1_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal5_id, @cat1_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal6_id, @cat5_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal7_id, @cat5_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal8_id, @cat2_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal9_id, @cat2_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal10_id, @cat4_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal11_id, @cat4_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal12_id, @cat3_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal13_id, @cat3_id);


-- Buyer


INSERT INTO Buyer( FirstName,LastName,Email,PhoneNumber,ProfilePicture,EncryptedPassword)
VALUES('Hans', 'Hansen', 'hans@hansen.dk', '4520302010', LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/hans.png'), 'test');

SET @hans_id = LAST_INSERT_ID();

INSERT INTO Buyer( FirstName,LastName,Email,PhoneNumber,ProfilePicture,EncryptedPassword)
VALUES('Bob', 'Jensen', 'bob@jensen.dk', '4530303030', LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/bob.png'), 'test');

SET @bob_id = LAST_INSERT_ID();


-- Orders

INSERT INTO MealOrder( BuyerId,MealId,Quantity )
VALUES (@hans_id, @meal1_id, 1);

INSERT INTO MealOrder( BuyerId,MealId,Quantity )
VALUES (@bob_id, @meal1_id, 2);


-- Reviews

INSERT INTO Review( BuyerId,MealId,Rating,ReviewDescription)
VALUES (@bob_id, @meal1_id, 4, 'Good spagetti');

INSERT INTO Review( BuyerId,MealId,Rating,ReviewDescription)
VALUES (@hans_id, @meal1_id, 3, 'Mega nice');

INSERT INTO Review( BuyerId,MealId,Rating,ReviewDescription)
VALUES (@bob_id, @meal2_id, 4, 'Good taste');

INSERT INTO Review( BuyerId,MealId,Rating,ReviewDescription)
VALUES (@hans_id, @meal3_id, 2, 'Nice seller');

INSERT INTO Review( BuyerId,MealId,Rating,ReviewDescription)
VALUES (@bob_id, @meal3_id, 5, 'Nice seller and good taske');

INSERT INTO Review( BuyerId,MealId,Rating,ReviewDescription)
VALUES (@hans_id, @meal4_id, 4, 'Big portions');

