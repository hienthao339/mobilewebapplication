create database MobileShopping
use MobileShopping

--------------------default: insert unrequired--------------------
create table users
(
	id_user int identity constraint PK_id_user primary key, 
	is_admin bit default 0,
	email varchar(100), 
	passwords varchar(100), 
	avatar varchar(100),
	names varchar(100), 
	phone varchar(10), 
	addresss varchar(500), 
	ranks int	--in (1,2,3,4)
	---1: Copper
	---2: Silver
	---3: Golden
	---4: Diamond
)

create table promocode 
(
	id_promo int identity constraint PK_id_promo primary key, 
	code varchar(10),
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
  rom varchar(10), --<radio button - checklist>
  brand varchar(20), --<radio button - checklist>
  color varchar(20), --<radio button - checklist>
  quantity int,
  rate decimal default 0,
  discount_price decimal
)

create table cart(
  id_user_cart int constraint PK_id_user_cart primary key constraint FK_id_user_cart references users(id_user),
  id_product int constraint FK_id_product_cart foreign key references product (id_product), 
  quantity int 
)

create table orders(
  id int identity constraint PK_id_order primary key, 
  id_user int constraint FK_id_user_order foreign key references users(id_user), 
  payment_type bit default 0, 
  created_at datetime default getdate(), 
  started_at datetime default getdate() + 1, 
  finished_at datetime, 
  shipping_fee int default 10, 
  total_price decimal default 0, 
  discount_price decimal default 0,
  pending bit default 1, 
  delivering bit default 0, 
  successed bit default 0,  
  canceled bit default 0,  
  paid bit default 0
) 

create table order_item(
  id_order_item int identity constraint PK_id_order_item  primary key, 
  id_order int constraint FK_id_order foreign key references orders(id), 
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
  id_feedback_order_item int constraint PK_id_feedback_order_item primary key constraint FK_id_feedback_order_item foreign key references order_item(id_order_item), 
  content varchar(500), 
  rate int default 0, 
  created_at datetime default getdate()
)


--------------------------------------------------------------------------------------
INSERT INTO  product (names, price, display, weights, water_resistance, operating_system, processor, battery, ram, rom, brand, color, quantity)
VALUES ('iPhone 14 Pro Max', '1099', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '128gb', 'Iphone', ' Deep Purple', 20), 
('iPhone 14 Pro Max', '1199', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '256gb', 'Iphone', 'Deep Purple', 20), 
('iPhone 14 Pro Max', '1399', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '512gb', 'Iphone', 'Deep Purple', 20), 
('iPhone 14 Pro Max', '1599', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '1tb', 'Iphone', 'Deep Purple', 20), 

('iPhone 14 Pro Max', '1099', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '128gb', 'Iphone', 'Space Black', 20), 
('iPhone 14 Pro Max', '1199', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '256gb', 'Iphone', 'Space Black', 20), 
('iPhone 14 Pro Max', '1399', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '512gb', 'Iphone', 'Space Black', 20), 
('iPhone 14 Pro Max', '1599', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '1tb', 'Iphone', 'Space Black', 20), 

('iPhone 14 Pro Max', '1099', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '128gb', 'Iphone', 'Silver', 20), 
('iPhone 14 Pro Max', '1199', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '256gb', 'Iphone', 'Silver', 20), 
('iPhone 14 Pro Max', '1399', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '512gb', 'Iphone', 'Silver', 20), 
('iPhone 14 Pro Max', '1599', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '1tb', 'Iphone', 'Silver', 20), 

('iPhone 14 Pro Max', '1099', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '128gb', 'Iphone', 'Gold', 20), 
('iPhone 14 Pro Max', '1199', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '256gb', 'Iphone', 'Gold', 20), 
('iPhone 14 Pro Max', '1399', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '512gb', 'Iphone', 'Gold', 20), 
('iPhone 14 Pro Max', '1599', '6.7" Super Retina XDR OLED', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', '1tb', 'Iphone', 'Gold', 20), 
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('iPhone 14 Pro ', '999', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '128gb', 'Iphone', 'Deep Purple', 20), 
('iPhone 14 Pro ', '1099', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '256gb', 'Iphone', 'Deep Purple', 20), 
('iPhone 14 Pro ', '1299', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '512gb', 'Iphone', 'Deep Purple', 20), 
('iPhone 14 Pro ', '1499', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '1tb', 'Iphone', 'Deep Purple', 20), 

('iPhone 14 Pro ', '999', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '128gb', 'Iphone', 'Space Black', 20), 
('iPhone 14 Pro ', '1099', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '256gb', 'Iphone', 'Space Black', 20), 
('iPhone 14 Pro ', '1299', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '512gb', 'Iphone', 'Space Black', 20), 
('iPhone 14 Pro ', '1499', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '1tb', 'Iphone', 'Space Black', 20), 

('iPhone 14 Pro', '999', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '128gb', 'Iphone', 'Silver', 20), 
('iPhone 14 Pro', '1099', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '256gb', 'Iphone', 'Silver', 20), 
('iPhone 14 Pro', '1299', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '512gb', 'Iphone', 'Silver', 20), 
('iPhone 14 Pro', '1499', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '1tb', 'Iphone', 'Silver', 20), 

('iPhone 14 Pro', '999', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '128gb', 'Iphone', 'Gold', 20), 
('iPhone 14 Pro', '1099', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '256gb', 'Iphone', 'Gold', 20), 
('iPhone 14 Pro', '1299', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '512gb', 'Iphone', 'Gold', 20), 
('iPhone 14 Pro', '1499', '6.1" Super Retina XDR OLED', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', '1tb', 'Iphone', 'Gold', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('iPhone 14', '799', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '128gb', 'Iphone', ' Blue', 20), 
('iPhone 14', '899', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '256gb', 'Iphone', 'Blue', 20), 
('iPhone 14', '1099', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '512gb', 'Iphone', 'Blue', 20), 

('iPhone 14', '799', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '128gb', 'Iphone', 'Midnight', 20), 
('iPhone 14', '899', '6.1 " Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '256gb', 'Iphone', 'Midnight', 20), 
('iPhone 14', '1099', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '512gb', 'Iphone', 'Midnight', 20), 

('iPhone 14', '799', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '128gb', 'Iphone', 'Purple', 20), 
('iPhone 14', '899', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '256gb', 'Iphone', 'Purple', 20), 
('iPhone 14', '1099', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '512gb', 'Iphone', 'Purple', 20), 

('iPhone 14', '799', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '128gb', 'Iphone', 'Red', 20), 
('iPhone 14', '899', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '256gb', 'Iphone', 'Red', 20), 
('iPhone 14', '1099', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '512gb', 'Iphone', 'Red', 20), 

('iPhone 14', '799', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '128gb', 'Iphone', 'Starlight', 20), 
('iPhone 14', '899', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '256gb', 'Iphone', 'Starlight', 20), 
('iPhone 14', '1099', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', '512gb', 'Iphone', 'Starlight', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('iPhone 13', '699', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPUz', '3227 mAh,  12.41 Wh', '6 GB', '128gb', 'Iphone', 'Midnight', 20), 
('iPhone 13', '799', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '256gb', 'Iphone', 'Midnight', 20), 
('iPhone 13', '999', '6.1" Super Retina XDR OLED', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '512gb', 'Iphone', 'Midnight', 20), 

('iPhone 13', '699', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '128gb', 'Iphone', 'Blue', 20), 
('iPhone 13', '799', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '256gb', 'Iphone', 'Blue', 20), 
('iPhone 13', '999', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '512gb', 'Iphone', 'Blue', 20), 

('iPhone 13', '699', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '128gb', 'Iphone', 'Green', 20), 
('iPhone 13', '799', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '256gb', 'Iphone', 'Green', 20), 
('iPhone 13', '999', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '512gb', 'Iphone', 'Green', 20), 

('iPhone 13', '699', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '128gb', 'Iphone', 'Pink', 20), 
('iPhone 13', '799', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '256gb', 'Iphone', 'Pink', 20), 
('iPhone 13', '999', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '512gb', 'Iphone', 'Pink', 20), 

('iPhone 13', '699', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '128gb', 'Iphone', 'Red', 20), 
('iPhone 13', '799', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '256gb', 'Iphone', 'Red', 20), 
('iPhone 13', '999', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '512gb', 'Iphone', 'Red', 20), 

('iPhone 13', '699', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '128gb', 'Iphone', 'Starlight', 20), 
('iPhone 13', '799', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '256gb', 'Iphone', 'Starlight', 20), 
('iPhone 13', '999', '6.1" Super Retina XDR OLED', '174 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', '512gb', 'Iphone', 'Starlight', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('iPhone 13 Mini', '599', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '128gb', 'Iphone', 'Blue', 20), 
('iPhone 13 Mini', '699', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '256gb', 'Iphone', 'Blue', 20), 
('iPhone 13 Mini', '899', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '512gb', 'Iphone', 'Blue', 20), 

('iPhone 13 Mini', '599', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '128gb', 'Iphone', 'Green', 20), 
('iPhone 13 Mini', '699', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '256gb', 'Iphone', 'Green', 20), 
('iPhone 13 Mini', '899', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '512gb', 'Iphone', 'Green', 20), 

('iPhone 13 Mini', '599', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '128gb', 'Iphone', 'Pink', 20), 
('iPhone 13 Mini', '699', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '256gb', 'Iphone', 'Pink', 20), 
('iPhone 13 Mini', '899', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '512gb', 'Iphone', 'Pink', 20), 

('iPhone 13 Mini', '599', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '128gb', 'Iphone', 'Red', 20), 
('iPhone 13 Mini', '699', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '256gb', 'Iphone', 'Red', 20), 
('iPhone 13 Mini', '899', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '512gb', 'Iphone', 'Red', 20), 

('iPhone 13 Mini', '599', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '128gb', 'Iphone', 'Starlight', 20), 
('iPhone 13 Mini', '699', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '256gb', 'Iphone', 'Starlight', 20), 
('iPhone 13 Mini', '899', '5.4" Super Retina XDR OLED', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', '512gb', 'Iphone', 'Starlight', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Galaxy S23 Ultra', '1199.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '256gb', 'Samsung', 'Black', 20), 
('Galaxy S23 Ultra', '1379.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '512gb', 'Samsung', 'Black', 20), 
('Galaxy S23 Ultra', '1619.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '1tb', 'Samsung', 'Black', 20), 

('Galaxy S23 Ultra', '1199.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '256gb', 'Samsung', 'Cream', 20), 
('Galaxy S23 Ultra', '1379.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '512gb', 'Samsung', 'Cream', 20), 
('Galaxy S23 Ultra', '1619.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '1tb', 'Samsung', 'Cream', 20), 

('Galaxy S23 Ultra', '1199.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '256gb', 'Samsung', 'Green', 20), 
('Galaxy S23 Ultra', '1379.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'AQualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '512gb', 'Samsung', 'Green', 20), 
('Galaxy S23 Ultra', '1619.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '1tb', 'Samsung', 'Green', 20), 

('Galaxy S23 Ultra', '1199.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '256gb', 'Samsung', 'Lavender', 20), 
('Galaxy S23 Ultra', '1379.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '512gb', 'Samsung', 'Lavender', 20), 
('Galaxy S23 Ultra', '1619.99', '6.8” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8/12 GB', '1tb', 'Samsung', 'Lavender', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Galaxy S23+', '999.99', '6.6” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', '256gb', 'Samsung', 'Black', 20), 
('Galaxy S23+', '1119.99', '6.6” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', '512gb', 'Samsung', 'Black', 20), 

('Galaxy S23+', '999.99', '6.6” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', '256gb', 'Samsung', 'Cream', 20), 
('Galaxy S23+', '1119.99', '6.6” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', '512gb', 'Samsung', 'Cream', 20), 

('Galaxy S23+', '999.99', '6.6” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', '256gb', 'Samsung', 'Green', 20), 
('Galaxy S23+', '1119.99', '6.6” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'AQualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', '512gb', 'Samsung', 'Green', 20), 

('Galaxy S23+', '999.99', '6.6” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', '256gb', 'Samsung', 'Lavender', 20), 
('Galaxy S23+', '1119.99', '6.6” Dynamic AMOLED 2X Infinity-O', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', '512gb', 'Samsung', 'Lavender', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Galaxy S22+', '819.99', '6.6” Infinity-O FHD+ Dynamic AMOLED 2X', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', '128gb', 'Samsung', 'Phantom black', 20), 
('Galaxy S22+ ','869.99', '6.6” Infinity-O FHD+ Dynamic AMOLED 2X', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', '256gb', 'Samsung', 'Phantom black', 20), 

('Galaxy S22+',  '819.99', '6.6” Infinity-O FHD+ Dynamic AMOLED 2X', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', '128gb', 'Samsung', 'Pink gold', 20), 
('Galaxy S22+',  '869.99', '6.6” Infinity-O FHD+ Dynamic AMOLED 2X', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', '256gb', 'Samsung', 'Pink gold', 20), 

('Galaxy S22+', '819.99', '6.6” Infinity-O FHD+ Dynamic AMOLED 2X', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', '125gb', 'Samsung', 'Green', 20), 
('Galaxy S22+', '869.99', '6.6” Infinity-O FHD+ Dynamic AMOLED 2X', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', '256gb', 'Samsung', 'Green', 20), 

('Galaxy S22+',  '819.99', '6.6” Infinity-O FHD+ Dynamic AMOLED 2X', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', '128gb', 'Samsung', 'Phantom white', 20), 
('Galaxy S22+',  '869.99', '6.6” Infinity-O FHD+ Dynamic AMOLED 2X', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', '256gb', 'Samsung', 'Phantom white', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Galaxy Z Flip4',  '839.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '128gb', 'Samsung', 'Pink gold', 20), 
('Galaxy Z Flip4',  '899.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '256gb', 'Samsung', 'Pink gold', 20), 
('Galaxy Z Flip4',  '1019.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '512gb', 'Samsung', 'Pink gold', 20), 

('Galaxy Z Flip4',  '839.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '128gb', 'Samsung', 'Graphite', 20), 
('Galaxy Z Flip4',  '899.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '256gb', 'Samsung', 'Graphite', 20), 
('Galaxy Z Flip4',  '1019.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '512gb', 'Samsung', 'Graphite', 20), 

('Galaxy Z Flip4',  '839.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '125gb', 'Samsung', 'Blue', 20), 
('Galaxy Z Flip4',  '899.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '256gb', 'Samsung', 'Blue', 20), 
('Galaxy Z Flip4',  '1019.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '512gb', 'Samsung', 'Blue', 20), 

('Galaxy Z Flip4',  '839.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '128gb', 'Samsung', 'Bora purple', 20), 
('Galaxy Z Flip4',  '899.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '256gb', 'Samsung', 'Bora purple', 20), 
('Galaxy Z Flip4',  '1019.99', '6.7” FHD + Dynamic AMOLED 2X Infinity Flex', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', '512gb', 'Samsung', 'Bora purple', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Galaxy Z Fold4',  '1464.99', '7.6” QXGA+ Dynamic AMOLED 2X Infinity Flex', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', '256gb', 'Samsung', 'Beige', 20), 
('Galaxy Z Fold4',  '1584.99', '7.6” QXGA+ Dynamic AMOLED 2X Infinity Flex', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', '512gb', 'Samsung', 'Beige', 20), 
('Galaxy Z Fold4',  '1824.99', '7.6” QXGA+ Dynamic AMOLED 2X Infinity Flex', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', '1tb', 'Samsung', 'Beige', 20), 

('Galaxy Z Fold4',  '1464.99', '7.6” QXGA+ Dynamic AMOLED 2X Infinity Flex', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', '256gb', 'Samsung', 'Phantom black', 20), 
('Galaxy Z Fold4',  '1584.99', '7.6” QXGA+ Dynamic AMOLED 2X Infinity Flex', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', '512gb', 'Samsung', 'Phantom black', 20), 
('Galaxy Z Fold4',  '1824.99', '7.6” QXGA+ Dynamic AMOLED 2X Infinity Flex', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', '1tb', 'Samsung', 'Phantom black', 20), 

('Galaxy Z Fold4', '1464.99', '7.6” QXGA+ Dynamic AMOLED 2X Infinity Flex', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', '256gb', 'Samsung', 'Graygreen', 20), 
('Galaxy Z Fold4', '1584.99', '7.6” QXGA+ Dynamic AMOLED 2X Infinity Flex', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', '512gb', 'Samsung', 'Graygreen', 20), 
('Galaxy Z Fold4', '1824.99', '7.6” QXGA+ Dynamic AMOLED 2X Infinity Flex', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', '1tb', 'Samsung', 'Graygreen', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Pixel 7', '599', '6.3" FHD+ OLED at 416 PPI', '197 g', 'IP68', '', 'Google Tensor G2', '4355 mAh', '8 GB LPDDR5', '128gb', 'Google','Lemongrass',  20), 
('Pixel 7', '699', '6.3" FHD+ OLED at 416 PPI', '197 g', 'IP68', '', 'Google Tensor G2', '4355 mAh', '8 GB LPDDR5', '256gb', 'Google','Lemongrass' , 20), 

('Pixel 7',  '599', '6.3" FHD+ OLED at 416 PPI', '197 g', 'IP68', '', 'Google Tensor G2', '4355 mAh', '8 GB LPDDR5', '128gb', 'Google', 'Obsidian', 20), 
('Pixel 7', '699', '6.3" FHD+ OLED at 416 PPI', '197 g', 'IP68', '', 'Google Tensor G2', '4355 mAh', '8 GB LPDDR5', '256gb', 'Google', 'Obsidian', 20), 

('Pixel 7','599', '6.3" FHD+ OLED at 416 PPI', '197 g', 'IP68', '', 'Google Tensor G2', '4355 mAh ', '8 GB LPDDR5', '128gb', 'Google', 'Snow', 20), 
('Pixel 7','699', '6.3" FHD+ OLED at 416 PPI', '197 g', 'IP68', '', 'Google Tensor G2', '4355 mAh ', '8 GB LPDDR5', '256gb', 'Google', 'Snow', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Pixel 7 Pro',  '899', '6.7" QHD+ LTPO OLED at 512 PPI', '212 g', 'IP68', '', 'Google Tensor G2', '5000 mAh', '12 GB LPDDR5', '128gb', 'Google', 'Hazel', 20), 
('Pixel 7 Pro',  '999', '6.7" QHD+ LTPO OLED at 512 PPI', '212 g', 'IP68', '', 'Google Tensor G2', '5000 mAh', '12 GB LPDDR5', '256gb', 'Google', 'Hazel', 20), 
('Pixel 7 Pro',  '1099', '6.7" QHD+ LTPO OLED at 512 PPI', '212 g', 'IP68', '', 'Google Tensor G2', '5000 mAh', '12 GB LPDDR5', '512gb', 'Google', 'Hazel', 20), 

('Pixel 7 Pro',  '899', '6.7" QHD+ LTPO OLED at 512 PPI', '212 g', 'IP68', '', 'Google Tensor G2', '5000 mAh', '12 GB LPDDR5', '128gb', 'Google', 'Obsidian', 20), 
('Pixel 7 Pro', '999', '6.7" QHD+ LTPO OLED at 512 PPI', '212 g', 'IP68', '', 'Google Tensor G2', '5000 mAh', '12 GB LPDDR5', '256gb', 'Google', 'Obsidian', 20), 
('Pixel 7 Pro',  '1099', '6.7" QHD+ LTPO OLED at 512 PPI', '212 g', 'IP68', '', 'Google Tensor G2', '5000 mAh', '12 GB LPDDR5', '512gb', 'Google', 'Obsidian', 20), 

('Pixel 7 Pro','899', '6.7" QHD+ LTPO OLED at 512 PPI', '212 g', 'IP68', '', 'Google Tensor G2', '5000 mAh ', '12 GB LPDDR5', '128gb', 'Google', 'Snow', 20), 
('Pixel 7 Pro','999', '6.7" QHD+ LTPO OLED at 512 PPI', '212 g', 'IP68', '', 'Google Tensor G2', '5000 mAh ', '12 GB LPDDR5', '256gb', 'Google', 'Snow', 20), 
('Pixel 7 Pro','1099', '6.7" QHD+ LTPO OLED at 512 PPI', '212 g', 'IP68', '', 'Google Tensor G2', '5000 mAh', '12 GB LPDDR5', '512gb', 'Google', 'Snow', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Pixel 6A',  '449', '6.1" FHD+ OLED at 429 PPI', '178 g', 'IP67', '', 'Google Tensor ', '4410 mAh', '6 GB LPDDR5', '128gb', 'Google', 'Chalk', 20), 
('Pixel 6A',  '449', '6.1" FHD+ OLED at 429 PPI', '178 g', 'IP67', '', 'Google Tensor ', '4410 mAh', '6 GB LPDDR5', '128gb', 'Google', 'Charcoal', 20), 
('Pixel 6A','449', '6.1" FHD+ OLED at 429 PPI', '178 g', 'IP67', '', 'Google Tensor ', '4410 mAh ', '6 GB LPDDR5', '128gb', 'Google', 'Sage', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Xiaomi 13 Pro',  '1299', '6.73" WQHD+ AMOLED', '229 g', 'IP68', 'MIUI 14,  android 13', 'Snapdragon® 8 Gen 2 4nm chip Up to 3.2GH', '4820 mAh', '12 GB ', '256gb', 'Xiaomi', 'Ceramic black', 20), 
('Xiaomi 13 Pro',  '1399', '6.73" WQHD+ AMOLED', '229 g', 'IP68', 'MIUI 14,  android 13', 'Snapdragon® 8 Gen 2 4nm chip Up to 3.2GH', '4820 mAh', '12 GB ', '512gb', 'Xiaomi', 'Ceramic black', 20), 

('Xiaomi 13 Pro',  '1299', '6.73" WQHD+ AMOLED', '229 g', 'IP68', 'MIUI 14,  android 13', 'Snapdragon® 8 Gen 2 4nm chip Up to 3.2GH', '4820 mAh ', '12 GB ', '256gb', 'Xiaomi', 'Ceramic white', 20), 
('Xiaomi 13 Pro',  '1399', '6.73" WQHD+ AMOLED', '229 g', 'IP68', 'MIUI 14,  android 13', 'Snapdragon® 8 Gen 2 4nm chip Up to 3.2GH', '4820 mAh', '12 GB ', '512gb', 'Xiaomi', 'Ceramic white', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Xiaomi 12T',  '395', '6.67” AMOLED', '202 g', '', 'MIUI 13,  Android 12', '6.67” AMOLED', '5000 mAh', '8 GB ', '128gb', 'Xiaomi', ' Black', 20), 
('Xiaomi 12T',  '425', '6.67” AMOLED', '202 g', '', 'MIUI 13,  Android 12', '6.67” AMOLED', '5000 mAh', '8 GB ', '256gb', 'Xiaomi', ' Black', 20), 

('Xiaomi 12T',  '395', '6.67” AMOLED', '202 g', '', 'MIUI 13,  Android 12', '6.67” AMOLED', '5000 mAh ', '8 GB ', '128gb', 'Xiaomi', 'Silver', 20), 
('Xiaomi 12T',  '425', '6.67” AMOLED', '202 g', '', 'MIUI 13,  Android 12', '6.67” AMOLED', '5000 mAh', '8 GB ', '256gb', 'Xiaomi', 'Silver ', 20), 

('Xiaomi 12T',  '395', '6.67” AMOLED', '202 g', '', 'MIUI 13,  Android 12', '6.67” AMOLED', '5000 mAh ', '8 GB ', '128gb', 'Xiaomi', 'Blue', 20), 
('Xiaomi 12T',  '425', '6.67” AMOLED', '202 g', '', 'MIUI 13,  Android 12', '6.67” AMOLED', '5000 mAh', '8 GB ', '256gb', 'Xiaomi', 'Blue', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Oppo Find N2 Flip',  '899.99', '6.8" FHD+', '191 g', '', 'Android 13', '8 cores with a maximum clock rate of 3.2GHz', '3110 mAh', '8 GB ', '256gb', 'Oppo', 'Black ', 20), 
('Oppo Find N2 Flip',  '899.99', '6.8" FHD+', '191 g', '', 'Android 13', '8 cores with a maximum clock rate of 3.2GHz', '3110 mAh', '8 GB ', '256gb', 'Oppo', 'While', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Oppo Find X5',  '799.99', '6.55" FHD+', '196 g', '', 'Android 12', 'MariSilicon X Imaging NPU', '3110 mAh', '8 GB ', '256gb', 'Oppo', 'Black ', 20), 
('Oppo Find X5',  '799.99', '6.55" FHD+', '196 g', '', 'Android 12', 'MariSilicon X Imaging NPU', '3110 mAh', '8 GB ', '256gb', 'Oppo', 'While', 20), 
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
('Oppo Reno8',  '319.99', '6.4" FHD', '179 g', '', 'Android 12', 'MediaTek Dimensity 1300 8 cores', '4500 mAh', '8 GB ', '256gb', 'Oppo', 'Black ', 20), 
('Oppo Reno8',  '319.99', '6.4" FHD', '179 g', '', 'Android 12', 'MediaTek Dimensity 1300 8 cores', '4500 mAh', '8 GB ', '256gb', 'Oppo', 'While', 20)


--++++++++++++++++++++++++++++++++++++++++

INSERT INTO users (is_admin, email , passwords, names , phone , addresss ) VALUES
	(0, 'ande223@gmail.com', 456, 'Anderson', '6929963841', '21, Kalbadevi Road, Mumbai-2.  '),
	(0, 'john125@gmail.com', 456, 'John', '1456032778', '49 Princess Street, Mumbai-2.  '),
	(0, 'beiley267@gmail.com', 456, 'Beiley', '6284691582', '89 Princess Street, R. No. 45, Mumbai-2.  '),
	(0, 'mark137@gmail.com', 456, 'Mark', '2130077630', 'R.No. 3A,2/22/Babu Genu Road, Princess Street, Mumbai.  '),
	(0, 'maria246@gmail.com', 456, 'Maria', '6321222854', '11 Magaldas Rd, P.Street, Mumbai-2.  '),
	(0, ' ', ' ', 'Carma', '6321232854', '78/80 Souri Bldg. B.G. Road, Mumbai.  '),
	(0, ' ', ' ', 'Cheryl', '2561058706', 'Ldb Mangaldas Bldg. No.1 P. Street, Mumbai-2.  '),
	(0, ' ', ' ', 'Davis', '2398362872', 'Bhagwati, Kalbadevi Rd, Mumbai.  '),
	(0, ' ', ' ', 'Jerry', '1421478556', '144, C.P. Tank Road, Mumbai-4.  '),
	(0, ' ', ' ', 'Edwards', '4421478556', '199 B-Shamaldas Gandhi Marg, Mumbai-2.  '),
	(0, ' ', ' ', 'Dena', '2999195601', 'Gala No.8 & 9, Amir Ind. Estate, Lower Parel, Mumbai -13.  '),
	(0, ' ', ' ', 'Golpher', '6886271934', 'Gr. Floor, Villa Jofona Bldg., Sitaladevi Temple Rd., Mahim, Mumbai-16.  '),
	(0, ' ', ' ', 'James', '7222810176', '5Th Floor, West Block, Room No.1,2,3,4, Mahim, Mumbai-16.  '),
	(0, ' ', ' ', 'Heinz', '1119831027', '206/2, Citizen Soc., V.S. Marg, Mahim, Mumbai-16.  '),
	(0, ' ', ' ', 'Sara', '1604415093', '189, Jodia Mansion, G.K. Marg, Lower Parel, Mumbai-13.  '),
	(0, ' ', ' ', 'Stacey', '1864496526', 'B D Petit Parsee Gen. Hosp., B D Petit Rd., Cumballa Hill, Mumbai-36.  '),
	(0, ' ', ' ', 'Lucy', '1363238416', 'St. Elizabeth’S Hosp., Walkeshwar, Mumbai- 6  '),
	(0, ' ', ' ', 'Martinez', '6624409250', 'Shop No.14, H.N. Hospital Bldg., 103, R.M.R. Rd., Girgaon, Mumbai-04.  '),
	(0, ' ', ' ', 'Frank', '9575271422', 'Shop No.5, Gr. Fl., Old Bhavani House, 94, Eep Gawalia Tank Rd., Mumbai-36.  '),
	(0, ' ', ' ', 'Robert', '9112869100', 'Shop No.7, Himalaya Chs, R.D. Thandani Marg, Worli Sea Face, Worli, Mumbai-18.  '),
	(0, ' ', ' ', 'Jennifer', '9802220047', 'Bhatia Hospital, Tardeo, Mumbai-400 007.  '),
	(0, ' ', ' ', 'Mahesh', '3882223406', 'Dalvi Hosp. Bldg., 38, N.S.Patkar Marg, Gamdevi, Mumbai-400 007.  '),
	(0, ' ', ' ', 'Alex', '4186059811', '15/17, M.K. Marg, Opp. Charni Rd, Rly. Station, Mumbai -400 004.  '),
	(0, ' ', ' ', 'Apollo', '5402396312', 'S.S.No.3122 To 3127, Old House No. 163, Ravivar Peth, Phaltan, Dist. Satara.  '),
	(0, ' ', ' ', 'Sam', '3279631324', 'S.S.No.1138, Upper Ground Floor Gala, Pratapgajgpeth, Radhika Chouk, Satara City, Satara.  '),
	(0, ' ', ' ', 'Steven', '3147055899', 'S.S.No.805/2, S.No.730/1A, Ground Floor, Patil Hoslpt. A/P Dahivadi, Tal. Man, Dist. Satara.  '),
	(0, ' ', ' ', 'Tiffani', '8214041514', 'M.No.512, 513, Saraswatichandra Padali  '),
	(0, ' ', ' ', 'Ariana', '3848461627', 'Station, Satara Rd., Tal. Koregaon, Dist. Satara.  '),
	(0, ' ', ' ', 'Justin', '2054557060', 'M.No.2/2, Gala No.5, A/P. Vaduj, Tal. Khaur, Dist. Satara.  '),
	(0, ' ', ' ', 'Hailey', '5233118891', 'M.No.2053, Gala No.1, Neeraj Complex, Gr. Floor, Near Krushna Hospital, A/P. Malkapur, Tal. Karad, Dist. Satara.  '),
	(0, ' ', ' ', 'Bella', '5330957390', 'M.No.825, Gala No.2, A/P. Maralhaveli, Tal. Patna, Dist. Satara.  '),
	(0, ' ', ' ', 'Bob', '9169910803', '154/2, Main Road, A/P. Pachgani, Tal. Mahabaleshwar, Dist. Satara.  '),
	(0, ' ', ' ', 'Karina', '6554637512', '425/A, Gala No.2, Ganpatiali, A/P. Tal. Wai, Dist, Satara.  '),
	(0, ' ', ' ', 'Dora', '7605125645', 'S.S.No.467/7A/1/42, Room No.1, Gr. Floor, Satara.  '),
	(0, ' ', ' ', 'Jessie', '6785967790', '22, 23 & 24 Vartak Nagar, Shopkeeper Soc. Vartaknagar Naka, Thane (W).  '),
	(0, ' ', ' ', 'Kate', '8080670669', 'Shivaji Nagar, Sane Guruji Path, Naupada, Thane (W).  '),
	(0, ' ', ' ', 'Kris', '9199628314', '5, Ramchandra Chs. Lokmanya Nagar, Pada  '),
	(0, ' ', ' ', 'Layla', '1237318462', 'No.2, Near Devendra Indl. Estate, Thane (W).  '),
	(0, ' ', ' ', 'Lili', '1828394198', '15, Ameet Anand Society, Pipe Line Road, Panch, Pakhadi, Thane (W).  '),
	(0, ' ', ' ', 'Mary', '1601971362', '2, Sainath View, Behind Anand Cinema, Kopri Colony, Thane (W).  '),
	(0, ' ', ' ', 'Rosia', '7691731675', 'Shop No.687, St. Baptist School Compound, Old Mumbai Road, Opp. Brahman Vidalaya, Thane (W).  '),
	(0, ' ', ' ', 'Sophia', '6850309362', 'Chatrapati Shivaji Maharaj Hospital, T.M.C. Kalwa, Dist. Thane.  '),
	(0, ' ', ' ', 'Veronica', '7835095243', '2, Prathamesh Apt., Gen, A.K. Vaidya North, Panchpakhadi, Thane.  '),
	(0, ' ', ' ', 'Sunny', '7983499576', 'R. No.1, Gr. Flr, Voltas Comp., Enclave,  '),
	(0, ' ', ' ', 'Stella', '3580943062', 'R. No.1, Basement, Voltas Comp., Enclave, Express Highway, Service Road, Thane (W).  '),
	(0, ' ', ' ', 'Miley', '7108216178', 'No.25, Amrapali Arcade, Pokharan Road No.2, Vasant Vihar, Thane (W).  '),
	(0, ' ', ' ', 'Hank', '8046047798', 'House No.581B, Shop No.1, Khanna Apt., Kumbhar Wada, Tal. Uran.  '),
	(0, ' ', ' ', 'Henry', '7108216178', 'Gala No.3, Mid Town Herritage, Piramal Developer, Dist. Khopoli, Tal. Khalapur.  '),
	(0, ' ', ' ', 'Jane', '8046047798', 'Amira Chs, Gr. Flr, Gala No.5, At Post Tal. Karjat, Dist. Raigad.  '),
	(0, ' ', ' ', 'Scott', '7023664117', 'House No.1902, Ward No.17, Tal. Shrivardhan, Dist. Raigad.  '),
	(0, ' ', ' ', 'Ben', '9236217384', 'House No.54, Ward No.17, Post Murud-Janjira, Tal. Murud, Dist. Raigad.  '),
	(1, 'nghiadang@gmail.com', 123, ' ', ' ', ' '),
	(1, 'ptchau@gmail.com', 123, ' ', ' ', ' '),
	(1, 'ththao@gmail.com', 123, ' ', ' ', ' '),
	(1, 'nqnhat@gmail.com', 123, ' ', ' ', ' ')

--+++++++++++++++++++++++++++++++++++++++++++++++++++++
INSERT INTO promocode (code, discount_price ) VALUES
	('SAVE15', 15),
	('SAVE16', 16),
	('SAVE17', 17),
	('SAVE18', 18),
	('SAVE19', 19),
	('SAVE20', 20)

--+++++++++++++++++++++++++++++++++++++++++++++++++++++
INSERT INTO orders (id_user, payment_type, created_at, started_at, finished_at , total_price , pending , delivering , successed , canceled , paid ) VALUES
	(3, 0, '8/3/2023', '9/3/2023', '10/3/2023', '2298', 1, 1, 1, 0, 1);

--+++++++++++++++++++++++++++++++++++++++++++++++++++++
INSERT INTO order_item(id_order , id_product , quantity) VALUES
	('1', '1', '1 ');

--+++++++++++++++++++++++++++++++++++++++++++++++++++++
INSERT INTO cart (id_user_cart, id_product, quantity) VALUES
	(1, 115, 1),
	(2, 94, 1),
	(3, 129, 1),
	(4, 32, 1),
	(5, 95, 1),
	(6, 20, 1),
	(7, 56, 1),
	(8, 55, 1),
	(9, 121, 1),
	(10, 54, 1),
	(11, 83, 1),
	(12, 60, 1),
	(13, 113, 1),
	(14, 62, 1),
	(15, 22, 1),
	(16, 144, 1),
	(17, 51, 1),
	(18, 58, 1),
	(19, 13, 1),
	(20, 64, 1),
	(21, 121, 1),
	(22, 161, 1),
	(23, 48, 1),
	(24, 117, 1),
	(25, 75, 1),
	(26, 136, 1),
	(27, 100, 1),
	(28, 103, 1),
	(29, 46, 1),
	(30, 115, 1),
	(31, 19, 1),
	(32, 103, 1),
	(33, 37, 1),
	(34, 105, 1),
	(35, 105, 1),
	(36, 124, 1),
	(37, 24, 1),
	(38, 117, 1),
	(39, 108, 1),
	(40, 116, 1),
	(41, 61, 1),
	(42, 31, 1),
	(43, 68, 1),
	(44, 35, 1),
	(45, 105, 1),
	(46, 136, 1),
	(47, 77, 1),
	(48, 133, 1),
	(49, 150, 1),
	(50, 116, 1),
	(51, 129, 1)

