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

create table brand
(
	id_brand int identity constraint PK_id_brand primary key,
	names varchar(50)
)

create table color
(
	id_color int identity constraint PK_id_color primary key,
	names varchar(50)
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
  id_brand int constraint FK_id_product_brand foreign key references brand (id_brand),
  id_color int constraint FK_id_product_brand foreign key references color (id_color),
  quantity int default 10,
  rate decimal,
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
INSERT INTO color (names) VALUES
	('Beige'),
	('Black'),
	('Blue'),
	('Cream'),
	('Gold'),
	('Green'),
	('Grey'),
	('Pink'),
	('Purple'),
	('Red'),
	('Silver'),
	('White');

--++++++++++++++++++++++++++++++++++++++++
INSERT INTO brand (names) VALUES
	('Samsung'),
	('Iphone'),
	('Oppo'),
	('Google');

--++++++++++++++++++++++++++++++++++++++++
INSERT INTO product (names, images, price, display, weights, water_resistance, operating_system, processor, battery, ram, id_brand, id_color) VALUES
	('Galaxy S22+', 'wwwroot/Images/Products/1.png', 800, '"6.6" Infinity-O FHD+ Dynamic AMOLED 2X"', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', 1, 2),
	('Galaxy S22+', 'wwwroot/Images/Products/2.png', 800, '"6.6" Infinity-O FHD+ Dynamic AMOLED 2X"', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', 1, 6),
	('Galaxy S22+', 'wwwroot/Images/Products/3.png', 800, '"6.6" Infinity-O FHD+ Dynamic AMOLED 2X"', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', 1, 8),
	('Galaxy S22+', 'wwwroot/Images/Products/4.png', 800, '"6.6" Infinity-O FHD+ Dynamic AMOLED 2X"', '228 g', 'IP68', 'Android 12', 'Snapdragon 8 Gen 1', '4, 500 mAh', '8 GB', 1, 12),
	('Galaxy S23 Ultra', 'wwwroot/Images/Products/5.png', 1000, '"6.8" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8 GB', 1, 2),
	('Galaxy S23 Ultra', 'wwwroot/Images/Products/6.png', 1000, '"6.8" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8 GB', 1, 4),
	('Galaxy S23 Ultra', 'wwwroot/Images/Products/7.png', 1000, '"6.8" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8 GB', 1, 6),
	('Galaxy S23 Ultra', 'wwwroot/Images/Products/8.png', 1000, '"6.8" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '5, 000 mAh', '8 GB', 1, 9),
	('Galaxy S23+', 'wwwroot/Images/Products/9.png', 1200, '"6.6" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', 1, 2),
	('Galaxy S23+', 'wwwroot/Images/Products/10.png', 1200, '"6.6" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', 1, 4),
	('Galaxy S23+', 'wwwroot/Images/Products/11.png', 1200, '"6.6" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', 1, 6),
	('Galaxy S23+', 'wwwroot/Images/Products/12.png', 1200, '"6.6" Dynamic AMOLED 2X Infinity-O"', '234 g', 'IP68', 'Android 13', 'Qualcomm Snapdragon 8 Gen 2', '4, 700 mAh', '8 GB', 1, 9),
	('Galaxy Z Flip4', 'wwwroot/Images/Products/13.png', 840, '"6.7" FHD + Dynamic AMOLED 2X Infinity Flex"', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', 1, 3),
	('Galaxy Z Flip4', 'wwwroot/Images/Products/14.png', 840, '"6.7" FHD + Dynamic AMOLED 2X Infinity Flex"', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', 1, 5),
	('Galaxy Z Flip4', 'wwwroot/Images/Products/15.png', 840, '"6.7" FHD + Dynamic AMOLED 2X Infinity Flex"', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', 1, 7),
	('Galaxy Z Flip4', 'wwwroot/Images/Products/16.png', 840, '"6.7" FHD + Dynamic AMOLED 2X Infinity Flex"', '187 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '3, 700 mAh', '8 GB', 1, 9),
	('Galaxy Z Fold4', 'wwwroot/Images/Products/17.png', 1000, '"7.6" QXGA+ Dynamic AMOLED 2X Infinity Flex"', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', 1, 1),
	('Galaxy Z Fold4', 'wwwroot/Images/Products/18.png', 1000, '"7.6" QXGA+ Dynamic AMOLED 2X Infinity Flex"', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', 1, 2),
	('Galaxy Z Fold4', 'wwwroot/Images/Products/19.png', 1000, '"7.6" QXGA+ Dynamic AMOLED 2X Infinity Flex"', '263 g', 'IP68', 'Android 12', 'Snapdragon 8 + Gen 1', '4, 400 mAh', '12 GB', 1, 6),
	('iPhone 13', 'wwwroot/Images/Products/20.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 2),
	('iPhone 13', 'wwwroot/Images/Products/21.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 3),
	('iPhone 13', 'wwwroot/Images/Products/22.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 6),
	('iPhone 13', 'wwwroot/Images/Products/23.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 8),
	('iPhone 13', 'wwwroot/Images/Products/24.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 10),
	('iPhone 13', 'wwwroot/Images/Products/25.png', 700, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 15 or newer', 'A15 Bionic chip with 4-core GPU', '3227 mAh,  12.41 Wh', '6 GB', 2, 12),
	('iPhone 13 Mini', 'wwwroot/Images/Products/26.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 3),
	('iPhone 13 Mini', 'wwwroot/Images/Products/27.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 6),
	('iPhone 13 Mini', 'wwwroot/Images/Products/28.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 8),
	('iPhone 13 Mini', 'wwwroot/Images/Products/29.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 10),
	('iPhone 13 Mini', 'wwwroot/Images/Products/30.png', 650, '"5.4" Super Retina XDR OLED"', '141 g', 'IP68', 'iOS 15', 'A15 Bionic chip with 4-core GPU', '2406 mAh', '6 GB', 2, 12),
	('iPhone 14', 'wwwroot/Images/Products/31.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 2),
	('iPhone 14', 'wwwroot/Images/Products/32.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 3),
	('iPhone 14', 'wwwroot/Images/Products/33.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 9),
	('iPhone 14', 'wwwroot/Images/Products/34.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 10),
	('iPhone 14', 'wwwroot/Images/Products/35.png', 800, '"6.1" Super Retina XDR OLED"', '172 g', 'IP68', 'iOS 16', 'A15 Bionic with 5-core GPU', '3279 mAh,  12.68 Wh', '6 GB', 2, 12),
	('iPhone 14 Pro ', 'wwwroot/Images/Products/36.png', 900, '"6.1" Super Retina XDR OLED"', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', 2, 2),
	('iPhone 14 Pro', 'wwwroot/Images/Products/37.png', 900, '"6.1" Super Retina XDR OLED"', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', 2, 5),
	('iPhone 14 Pro', 'wwwroot/Images/Products/38.png', 900, '"6.1" Super Retina XDR OLED"', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', 2, 9),
	('iPhone 14 Pro ', 'wwwroot/Images/Products/39.png', 900, '"6.1" Super Retina XDR OLED"', '206 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '3200 mAh,  12.38 Wh', '6 GB', 2, 11),
	('iPhone 14 Pro Max', 'wwwroot/Images/Products/40.png', 1200, '"6.7" Super Retina XDR OLED"', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', 2, 2),
	('iPhone 14 Pro Max', 'wwwroot/Images/Products/41.png', 1200, '"6.7" Super Retina XDR OLED"', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', 2, 5),
	('iPhone 14 Pro Max', 'wwwroot/Images/Products/42.png', 1200, '"6.7" Super Retina XDR OLED"', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', 2, 9),
	('iPhone 14 Pro Max', 'wwwroot/Images/Products/43.png', 1200, '"6.7" Super Retina XDR OLED"', '240 g', 'IP68', 'iOS 16', 'A16 Bionic chip', '4323 mAh,  16.68 Wh', '6 GB', 2, 11),
	('Find N2 Flip', 'wwwroot/Images/Products/44.png', 500, '"6.8" FHD+"', '191 g', '', 'Android 13', '8 cores with a maximum clock rate of 3.2GHz', '3110 mAh', '8 GB ', 3, 2),
	('Find N2 Flip', 'wwwroot/Images/Products/45.png', 500, '"6.8" FHD+"', '191 g', '', 'Android 13', '8 cores with a maximum clock rate of 3.2GHz', '3110 mAh', '8 GB ', 3, 12),
	('Find X5', 'wwwroot/Images/Products/46.png', 440, '"6.55" FHD+"', '196 g', '', 'Android 12', 'MariSilicon X Imaging NPU', '3110 mAh', '8 GB ', 3, 2),
	('Find X6', 'wwwroot/Images/Products/47.png', 440, '"6.55" FHD+"', '196 g', '', 'Android 12', 'MariSilicon X Imaging NPU', '3110 mAh', '8 GB ', 3, 12),
	('Reno8', 'wwwroot/Images/Products/48.png', 520, '"6.4" FHD"', '179 g', '', 'Android 12', 'MediaTek Dimensity 1300 8 cores', '4500 mAh', '8 GB ', 3, 2),
	('Reno8', 'wwwroot/Images/Products/49.png', 520, '"6.4" FHD"', '179 g', '', 'Android 12', 'MediaTek Dimensity 1300 8 cores', '4500 mAh', '8 GB ', 3, 12),
	('Pixel 6A', 'wwwroot/Images/Products/50.png', '600', '"6.1" FHD+ OLED at 429 PPI"', '178 g', 'IP67', '', '4 Tensor ', '4410 mAh', '6 GB ', 4, 2),
	('Pixel 6A', 'wwwroot/Images/Products/51.png', '600', '"6.1" FHD+ OLED at 429 PPI"', '178 g', 'IP67', '', '4 Tensor ', '4410 mAh', '6 GB ', 4, 6),
	('Pixel 6A', 'wwwroot/Images/Products/52.png', '600', '"6.1" FHD+ OLED at 429 PPI"', '178 g', 'IP67', '', '4 Tensor ', '4410 mAh', '6 GB ', 4, 12),
	('Pixel 7', 'wwwroot/Images/Products/53.png', 680, '"6.3" FHD+ OLED at 416 PPI"', '197 g', 'IP68', '', '4 Tensor G2', '4355 mAh', '8 GB', 4, 2),
	('Pixel 7', 'wwwroot/Images/Products/54.png', 680, '"6.3" FHD+ OLED at 416 PPI"', '197 g', 'IP68', '', '4 Tensor G2', '4355 mAh', '8 GB', 4, 6),
	('Pixel 7', 'wwwroot/Images/Products/55.png', 680, '"6.3" FHD+ OLED at 416 PPI"', '197 g', 'IP68', '', '4 Tensor G2', '4355 mAh', '8 GB', 4, 12),
	('Pixel 7 Pro', 'wwwroot/Images/Products/56.png', 720, '"6.7" QHD+ LTPO OLED at 512 PPI"', '212 g', 'IP68', '', '4 Tensor G2', '5000 mAh', '12 GB', 4, 2),
	('Pixel 7 Pro', 'wwwroot/Images/Products/57.png', 720, '"6.7" QHD+ LTPO OLED at 512 PPI"', '212 g', 'IP68', '', '4 Tensor G2', '5000 mAh', '12 GB', 4, 6),
	('Pixel 7 Pro', 'wwwroot/Images/Products/58.png', 720, '"6.7" QHD+ LTPO OLED at 512 PPI"', '212 g', 'IP68', '', '4 Tensor G2', '5000 mAh', '12 GB', 4, 12);
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
	(1, 1, '1 ');

--+++++++++++++++++++++++++++++++++++++++++++++++++++++
INSERT INTO cart (id_user_cart, id_product, quantity) VALUES ()
	--51 user



