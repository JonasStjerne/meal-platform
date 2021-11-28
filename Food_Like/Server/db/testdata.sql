USE foodlike;

INSERT INTO Buyer( FirstName,LastName,Email,PhoneNumber,ProfilePicture,EncryptedPassword)
VALUES('Hanne', 'Hansen', 'hanne@hansen.dk', '4560302010', LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/Hanne.png'), 'abc1234');

SET @hanne_id = LAST_INSERT_ID();

INSERT INTO Address( Line1,City )
VALUES ('BOULEVARDEN 14', '9000 Aalborg');

SET @addr_id = LAST_INSERT_ID();

INSERT INTO Seller( SellerId,AddressId,KitchenPicture)
VALUES (@hanne_id, @addr_id, LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/kitchen.png') );


INSERT INTO Meal( SellerId,Titel,Portions,PortionPrice,MealDescription,Ingridients,PickupFrom,PickupTo,MealPicture )
VALUES (@hanne_id, 'Spagetti w/chicken', 5, 20,
"I'll be making this delicious dinner. I’ve made it many times before and all the ingridients will be fresh. The chicken is marinaded in garlic and rosemary The pastas are fresh but not homemade.",
'Spaghetti,Tomato, Mozzarella, Chicken Breast,Balsamic,Garlic,Rosemary,Pesto',
'2021-1-20 19:00:00',
'2021-1-20 20:00:00',
LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/Spaghettichicken.png')
);

SET @meal_id = LAST_INSERT_ID();

INSERT INTO Category( Titel)
VALUES ('European');

INSERT INTO Category( Titel)
VALUES ('American');

INSERT INTO Category( Titel)
VALUES ('Asian');

INSERT INTO Category( Titel)
VALUES ('Italian');
SET @cat1_id = LAST_INSERT_ID();

INSERT INTO Category( Titel)
VALUES ('Danish');
SET @cat2_id = LAST_INSERT_ID();


INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal_id, @cat1_id);

INSERT INTO MealCategory( MealId,CategoryId)
VALUES(@meal_id, @cat2_id);


INSERT INTO Buyer( FirstName,LastName,Email,PhoneNumber,ProfilePicture,EncryptedPassword)
VALUES('Hans', 'Hansen', 'hans@hansen.dk', '4520302010', LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/money.png'), 'test');

SET @hans_id = LAST_INSERT_ID();

INSERT INTO Buyer( FirstName,LastName,Email,PhoneNumber,ProfilePicture,EncryptedPassword)
VALUES('Bob', 'Jensen', 'bob@jensen.dk', '4530303030', LOAD_FILE('/Users/Frederikke/Projects/P3/Food_Like/Client/wwwroot/assets/pickup.png'), 'test');

SET @bob_id = LAST_INSERT_ID();


INSERT INTO MealOrder( BuyerId,MealId,Quantity )
VALUES (@hans_id, @meal_id, 1);

INSERT INTO MealOrder( BuyerId,MealId,Quantity )
VALUES (@bob_id, @meal_id, 2);


INSERT INTO Review( BuyerId,MealId,Rating,ReviewDescription)
VALUES (@bob_id, @meal_id, 4, 'God spagetti');

INSERT INTO Review( BuyerId,MealId,Rating,ReviewDescription)
VALUES (@hans_id, @meal_id, 3, 'Mega nice');

