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

create table rom
(
	id_rom int identity constraint PK_id_rom primary key,
	size varchar(10)
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
  id_rom int constraint FK_id_product_rom foreign key references rom (id_rom),
  id_brand int constraint FK_id_product_brand foreign key references brand (id_brand),
  id_color int constraint FK_id_product_brand foreign key references color (id_color),
  quantity int default 10,
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
INSERT INTO cart (id_user_cart, id_product, quantity) VALUES ()
	--51 user

select * from product
select distinct names, color from product
select distinct names, price from product


