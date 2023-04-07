create database MobileShopping
use MobileShopping

--------------------default: insert unrequired--------------------


create table customer(
	id_customer int identity constraint PK_id_customer_customer primary key,
	email nvarchar(100),
	phone varchar(10),
	addresss nvarchar(100) ,
	city nvarchar(100),
	district nvarchar(100),
	ward nvarchar(100)
)
create table ranks(
	id_rank int identity constraint PK_id_rank primary key,
	discount int 
)
create table users
(
	id_user int identity constraint PK_id_user primary key, 
	is_admin bit default 0,
	names nvarchar(100),
	email varchar(100) , 
	passwords varchar(100), 
	avatar varchar(100),
	phone varchar(10) ,
	id_rank int constraint FK_id_users_user foreign key references ranks(id_rank)
	totalspend float
)

create table promocode 
(
	id_promo int identity constraint PK_id_promo primary key, 
	code varchar(10) unique,
	created_at datetime default getdate(), 
	started_at datetime default getdate() + 1, 
	finished_at datetime default getdate() + 15,
	discount_price int
)

create table product
(
  id_product int identity constraint PK_id_product primary key, 
  names varchar(100), 
  images varchar(100), 
  price decimal, 
  display varchar(100), 
  weights varchar(100),  
  water_resistance varchar(100),  
  operating_system varchar(100), 
  processor varchar(100), 
  battery varchar(100), 
  ram varchar(15), 
  brand varchar(20),
  color varchar(20),
  quantity int default 10,
  rate decimal,
  id_promo int constraint FK_id_promo_product foreign key references promocode(id_promo)
)

create table cart(
  id_cart int identity constraint PK_id_cart primary key,
  id_user int constraint FK_id_user_cart foreign key references users (id_user),
  id_product int constraint FK_id_product_cart foreign key references product (id_product), 
  quantity int 
)


create table orders(
  id_order int identity constraint PK_id_order primary key, 
  id_customer int constraint FK_id_customer_oders foreign key references customer(id_customer),
  payment_type bit default 0, 
  created_at datetime default getdate(), 
  started_at datetime default getdate() + 1, 
  finished_at datetime, 
  shipping_fee int , 
  total_price decimal default 0, 
  id_promo int constraint FK_id_promo_order foreign key references promocode(id_promo),
  pending bit default 1, 
  delivering bit default 0, 
  successed bit default 0,  
  canceled bit default 0,  
  paid bit default 0,
) 

create table order_item(
  id_order_item int identity constraint PK_id_order_item  primary key, 
  id_order int constraint FK_id_order_order_item foreign key references orders(id_order), 
  id_product int constraint FK_id_product_order_item foreign key references product(id_product), 
  quantity int
)


create table chat(
	id_chat int identity constraint PK_id_chat primary key, 
	id_user int constraint FK_id_user_chat foreign key references users(id_user), 
	content varchar(500), 
	create_at datetime default getdate(), 
)

create table feedbacks(
  id_feedback int indentity constraint PK_id_feedback primary key, 
  content varchar(500), 
  rate int default 0, 
  created_at datetime default getdate()
)

--------------------------------------------------------------------------------------
--INSERT INTO color (names) VALUES
--	('Beige'),
--	('Black'),
--	('Blue'),
--	('Cream'),
--	('Gold'),
--	('Green'),
--	('Grey'),
--	('Pink'),
--	('Purple'),
--	('Red'),
--	('Silver'),
--	('White');


--INSERT INTO brand (names) VALUES
--	('Samsung'),
--	('Apple'),
--	('Oppo'),
--	('Google');


--++++++++++++++++++++++++++++++++++++++++
	INSERT INTO promocode (code, discount_price ) VALUES
	('SAVE15', 15),
	('SAVE16', 16),
	('SAVE17', 17),
	('SAVE18', 18),
	('SAVE19', 19),
	('SAVE20', 20)

--++++++++++++++++++++++++++++++++++++++++
INSERT INTO product (names, images, price, display, weights, water_resistance, operating_system, processor, battery, ram, id_brand, id_color,quantity,rate,id_promo) VALUES
	('Galaxy S22+', '1.png', 800, '"6.6" Infinity-O FHD+ Dynamic AMOLED 2X"', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', 1, 2, 10, 5, null),
	('Galaxy S22+', '2.png', 800, '"6.6" Infinity-O FHD+ Dynamic AMOLED 2X"', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', 1, 6, 10, 5, null),
	('Galaxy S22+', '3.png', 800, '"6.6" Infinity-O FHD+ Dynamic AMOLED 2X"', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', 1, 8, 10, 5, null),
	('Galaxy S22+', '4.png', 800, '"6.6" Infinity-O FHD+ Dynamic AMOLED 2X"', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', 1, 12, 10, 5, null),
	('Galaxy S23 Ultra', '5.png', 1000, '"6.8" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8 GB', 1, 2, 10, 5, null),
	('Galaxy S23 Ultra', '6.png', 1000, '"6.8" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8 GB', 1, 4, 10, 5, null),
	('Galaxy S23 Ultra', '7.png', 1000, '"6.8" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8 GB', 1, 6, 10, 5, null),
	('Galaxy S23 Ultra', '8.png', 1000, '"6.8" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8 GB', 1, 9, 10, 5, null),
	('Galaxy S23+', '9.png', 1200, '"6.6" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', 1, 2, 10, 5, null),
	('Galaxy S23+', '10.png', 1200, '"6.6" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', 1, 4, 10, 5, null),
	('Galaxy S23+', '11.png', 1200, '"6.6" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', 1, 6, 10, 5, null),
	('Galaxy S23+', '12.png', 1200, '"6.6" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', 1, 9, 10, 5, null),
	('Galaxy Z Flip4', '13.png', 840, '"6.7" FHD + Dynamic AMOLED 2X Infinity Flex"', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', 1, 3, 10, 5, null),
	('Galaxy Z Flip4', '14.png', 840, '"6.7" FHD + Dynamic AMOLED 2X Infinity Flex"', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', 1, 5, 10, 5, null),
	('Galaxy Z Flip4', '15.png', 840, '"6.7" FHD + Dynamic AMOLED 2X Infinity Flex"', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', 1, 7, 10, 5, null),
	('Galaxy Z Flip4', '16.png', 840, '"6.7" FHD + Dynamic AMOLED 2X Infinity Flex"', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', 1, 9, 10, 5, null),
	('Galaxy Z Fold4', '17.png', 1000, '"7.6" QXGA+ Dynamic AMOLED 2X Infinity Flex"', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', 1, 1, 10, 5, null),
	('Galaxy Z Fold4', '18.png', 1000, '"7.6" QXGA+ Dynamic AMOLED 2X Infinity Flex"', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', 1, 2, 10, 5, null),
	('Galaxy Z Fold4', '19.png', 1000, '"7.6" QXGA+ Dynamic AMOLED 2X Infinity Flex"', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', 1, 6, 10, 5, null),
	('iPhone 13', '20.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 2, 10, 5, null),
	('iPhone 13', '21.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 3, 10, 5, null),
	('iPhone 13', '22.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 6, 10, 5, null),
	('iPhone 13', '23.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 8, 10, 5, null),
	('iPhone 13', '24.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 10, 10, 5, null),
	('iPhone 13', '25.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 12, 10, 5, null),
	('iPhone 13 Mini', '26.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 3, 10, 5, null),
	('iPhone 13 Mini', '27.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 6, 10, 5, null),
	('iPhone 13 Mini', '28.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 8, 10, 5, null),
	('iPhone 13 Mini', '29.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 10, 10, 5, null),
	('iPhone 13 Mini', '30.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 12, 10, 5, null),
	('iPhone 14', '31.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 2, 10, 5, null),
	('iPhone 14', '32.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 3, 10, 5, null),
	('iPhone 14', '33.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 9, 10, 5, null),
	('iPhone 14', '34.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 10, 10, 5, null),
	('iPhone 14', '35.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 12, 10, 5, null),
	('iPhone 14 Pro ', '36.png', 900, '"6.1" Super Retina XDR OLED"', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', 2, 2, 10, 5, null),
	('iPhone 14 Pro', '37.png', 900, '"6.1" Super Retina XDR OLED"', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', 2, 5, 10, 5, null),
	('iPhone 14 Pro', '38.png', 900, '"6.1" Super Retina XDR OLED"', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', 2, 9, 10, 5, null),
	('iPhone 14 Pro ', '39.png', 900, '"6.1" Super Retina XDR OLED"', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', 2, 11, 10, 5, null),
	('iPhone 14 Pro Max', '40.png', 1200, '"6.7" Super Retina XDR OLED"', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', 2, 2, 10, 5, null),
	('iPhone 14 Pro Max', '41.png', 1200, '"6.7" Super Retina XDR OLED"', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', 2, 5, 10, 5, null),
	('iPhone 14 Pro Max', '42.png', 1200, '"6.7" Super Retina XDR OLED"', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', 2, 9, 10, 5, null),
	('iPhone 14 Pro Max', '43.png', 1200, '"6.7" Super Retina XDR OLED"', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', 2, 11, 10, 5, null),
	('Find N2 Flip', '44.png', 500, '"6.8" FHD+"', '191 g', null, 'Android 13', '8 cores with a maximum clock rate of 3.2GHz', '3110 mAh', '8 GB ', 3, 2, 10, 5, null),
	('Find N2 Flip', '45.png', 500, '"6.8" FHD+"', '191 g', null, 'Android 13', '8 cores with a maximum clock rate of 3.2GHz', '3110 mAh', '8 GB ', 3, 12, 10, 5, null),
	('Find X5', '46.png', 440, '"6.55" FHD+"', '196 g', null, 'Android 12', 'MariSilicon X Imaging NPU', '3110 mAh', '8 GB ', 3, 2, 10, 5, null),
	('Find X6', '47.png', 440, '"6.55" FHD+"', '196 g', null, 'Android 12', 'MariSilicon X Imaging NPU', '3110 mAh', '8 GB ', 3, 12, 10, 5, null),
	('Reno8', '48.png', 520, '"6.4" FHD"', '179 g', null, 'Android 12', 'MediaTek Dimensity 1300 8 cores', '4500 mAh', '8 GB ', 3, 2, 10, 5, null),
	('Reno8', '49.png', 520, '"6.4" FHD"', '179 g', null, 'Android 12', 'MediaTek Dimensity 1300 8 cores', '4500 mAh', '8 GB ', 3, 12, 10, 5, null),
	('Pixel 6A', '50.png', '600', '"6.1" FHD+ OLED at 429 PPI"', '178 g', 'IP67', null, '4 Tensor ', '4410 mAh', '6 GB ', 4, 2, 10, 5, null),
	('Pixel 6A', '51.png', '600', '"6.1" FHD+ OLED at 429 PPI"', '178 g', 'IP67', null, '4 Tensor ', '4410 mAh', '6 GB ', 4, 6, 10, 5, null),
	('Pixel 6A', '52.png', '600', '"6.1" FHD+ OLED at 429 PPI"', '178 g', 'IP67', null, '4 Tensor ', '4410 mAh', '6 GB ', 4, 12, 10, 5, null),
	('Pixel 7', '53.png', 680, '"6.3" FHD+ OLED at 416 PPI"', '197 g', 'IP68', null, '4 Tensor G2', '4355 mAh', '8 GB', 4, 2, 10, 5, null),
	('Pixel 7', '54.png', 680, '"6.3" FHD+ OLED at 416 PPI"', '197 g', 'IP68', null, '4 Tensor G2', '4355 mAh', '8 GB', 4, 6, 10, 5, null),
	('Pixel 7', '55.png', 680, '"6.3" FHD+ OLED at 416 PPI"', '197 g', 'IP68', null, '4 Tensor G2', '4355 mAh', '8 GB', 4, 12, 10, 5, null),
	('Pixel 7 Pro', '56.png', 720, '"6.7" QHD+ LTPO OLED at 512 PPI"', '212 g', 'IP68', null, '4 Tensor G2', '5000 mAh', '12 GB', 4, 2, 10, 5, null),
	('Pixel 7 Pro', '57.png', 720, '"6.7" QHD+ LTPO OLED at 512 PPI"', '212 g', 'IP68', null, '4 Tensor G2', '5000 mAh', '12 GB', 4, 6, 10, 5, null),
	('Pixel 7 Pro', '58.png', 720, '"6.7" QHD+ LTPO OLED at 512 PPI"', '212 g', 'IP68', null, '4 Tensor G2', '5000 mAh', '12 GB', 4, 12, 10, 5, null);

--++++++++++++++++++++++++++++++++++++++++



INSERT INTO users (is_admin, email, passwords) VALUES
	(1, 'thao@gmail.com', 123)
	
