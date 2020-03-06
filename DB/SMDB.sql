USE Supermarket_DB

GO



CREATE TABLE Addresses(
	ID int NOT NULL identity(1,1) PRIMARY KEY,
	City CHAR(15) NOT NULL,
	Street CHAR(15) NOT NULL,
	Number CHAR(15) NOT NULL, 
)

GO

CREATE TABLE Branches(
	Branch_Name varchar(50) Primary key NOT NULL,
	Address_ID int,
    Foreign key(Address_ID) references Addresses(ID)
)

GO

CREATE TABLE Users(
	Id int identity(1,1) primary key,
	Username varchar(50) NOT NULL,
	Password varchar(50) NOT NULL,
	Email varchar(50) NULL
)

GO

CREATE TABLE Feedback(
	Id int identity(1,1) primary key,
	[User_Id] int not null foreign key  REFERENCES Users(Id),
	[Text]  varchar(max) not null
)

GO

CREATE TABLE Customers(
    [User_Id] int not null foreign key  REFERENCES Users(Id),
	[Name] varchar(50) NULL default 'unknown',
	Phone_Number varchar(24) NOT NULL,
	PRIMARY KEY([User_Id]),
	Address_ID int,
    Foreign key(Address_ID) references Addresses(ID)
)

GO

CREATE TABLE Transactions(
	Id bigint identity Primary Key NOT NULL ,
	Amount money NULL default 0,
	Status char(10) NOT NULL,
	Payment_Type char(10),
	Branch_name varchar(50) foreign key references Branches(Branch_Name)
)

GO

CREATE TABLE Workers(
	Worker_Id int foreign key references Users(Id) primary key,
	[Name] varchar(50) NOT NULL,
	Salary money NOT NULL,
	[Rank] varchar(24) NOT NULL
)

GO


CREATE TABLE Delivery_Person(
	Delivery_Id int foreign key references Workers(Worker_Id),
	Social_Id VARCHAR(24) NOT NULL unique,
	primary key(Delivery_id),
	Address_ID int,
    Foreign key(Address_ID) references Addresses(ID)
)

GO

CREATE TABLE Orders(
	Order_Id bigint not null foreign key REFERENCES Transactions(Id),
	Customer_Id int not null foreign key REFERENCES Customers(User_Id),
	Delivery_Id int not null foreign key REFERENCES Delivery_Person(Delivery_Id),

	Order_Date datetime NOT NULL,
	Order_Confirm_Date datetime  NULL,
	Planned_Delivery_Receive_Date datetime NULL,
	Address_ID int,
	[Status] varchar(50),
	[Feedback] varchar(max),
    Foreign key(Address_ID) references Addresses(ID),

	PRIMARY KEY(Order_Id)
)

GO

CREATE TABLE Suppliers(
	Company_Name varchar(50) primary key NOT NULL
)

GO

CREATE TABLE Categories(
	Parent_Category varchar(50) foreign key references Categories(Category_Name),
	Category_Name varchar(50) primary key NOT NULL
)

GO

CREATE TABLE Products(
	Barcode char(14) Primary Key NOT NULL,
	Cost_Price money NOT NULL,
	Selling_Price money NOT NULL,
    Supplier_Name varchar(50) foreign key references Suppliers(Company_Name),
	Name varchar(50) NOT NULL, 
    Category_Name varchar(50) foreign key references Categories(Category_Name)
)

GO

CREATE TABLE Warehouses(
	Warehouse_Name varchar(50) Primary key NOT NULL,
	Capacity float(24) NOT NULL,
	Address_ID int,
    Foreign key(Address_ID) references Addresses(ID)
)

GO



CREATE TABLE Workers_Branches(
 Branch_Name varchar(50) foreign key references Branches(Branch_Name),
 Worker_Id int foreign key references Workers(Worker_Id),
 Schedule varchar(100),
 PRIMARY KEY(Worker_Id,Branch_Name)
 )

GO

CREATE TABLE Workers_Warehouses(
  Warehouse_Name varchar(50) foreign key references Warehouses(Warehouse_Name),
  Worker_Id int foreign key references Workers(Worker_Id),
  Schedule varchar(100),
  PRIMARY KEY(Worker_Id,Warehouse_Name)
)

GO

CREATE TABLE Branches_Warehouses(
 Warehouse_Name varchar(50) foreign key references Warehouses(Warehouse_Name),
 Branch_Name varchar(50) foreign key references Branches(Branch_Name),
 PRIMARY KEY(Warehouse_Name, Branch_Name)
)

GO

CREATE TABLE Products_input_date (
  Product_code char(14) foreign key references Products(Barcode),
  Warehouse_name varchar(50) foreign key references Warehouses(Warehouse_Name),
  Date_ date NOT NULL,
  Quantity int NOT NULL,
  PRIMARY Key(Product_code, Warehouse_name, Date_, Quantity) 
)

GO
select * from TCT
select * from Transactions
select * from Branches
select * from Products_in_Branches
select * from Orders

insert into Orders(Order_Id,Customer_Id, Delivery_Id, Order_Date, Order_Confirm_Date, Planned_Delivery_Receive_Date, Address_ID, Status)
Values (6,1,6,'2020-03-08 12:35:29.000','2020-03-08 13:00:00.000','2020-03-08 18:30:00.000',9,'new')
CREATE TABLE Products_in_Branches (
	Product_code char(14) foreign key references Products(Barcode),
	Branch_name varchar(50) foreign key references Branches(Branch_Name),
	[Count] int, 
	PRIMARY KEY(Product_code, Branch_Name)
)

GO

	SELECT * FROM Transaction_Products

Create Table Transaction_Products(
	TransactionID bigint foreign key references Transactions(Id),
	ProductsCode char(14) foreign key references Products (Barcode),
    [Count] int NOT NULL,
	Primary Key(TransactionID,ProductsCode)
)

GO

CREATE TABLE Terminals(
	Id int identity NOT NULL,
	Branch_Name varchar(50) foreign key references Branches(Branch_Name),
	Primary Key(Id, Branch_Name)
)

GO

CREATE TABLE Cashiers(
	Cashier_Id int foreign key references Workers(Worker_Id),
	Name varchar(50) NOT NULL,
	Salary money NOT NULL,
	[Rank] varchar(24) NOT NULL,
	Amount money NOT NULL,
	Primary key(Cashier_Id)
)

GO

CREATE TABLE Suppliers_Warehouses
(
	Supplier_name varchar(50) foreign key references Suppliers (Company_Name),
	Warehouse_name varchar(50) foreign key references Warehouses (Warehouse_Name),
	[Count] int NOT NULL,
	Product_take_out_date datetime NOT NULL,
	Product_receiving_date datetime NOT NULL,

    PRIMARY KEY(Supplier_name, Warehouse_name)
)

GO

CREATE TABLE TCT(
	Transaction_Id bigint foreign key references Transactions(Id),
	Terminal_Id int,
	Branch_Name varchar(50),
	Foreign key(Terminal_ID, Branch_Name) references Terminals(Id, Branch_Name),
	Cashier_Id int foreign key references Cashiers(Cashier_Id),
	Primary Key(Transaction_Id,Terminal_Id)
)

GO

CREATE TABLE Preferences(
	Preference_Id int identity(1,1),
	Customer_Id int not null foreign key references Customers([User_Id]),
	[text] varchar(200),
	primary key(Preference_Id)
)

GO

CREATE TABLE Orders_Products(
	Order_Id bigint foreign key references Orders(Order_Id),
	Product_code char(14) foreign key references Products(Barcode),
	Quantity int not null,
	Customer_Id int not null foreign key references Customers([User_Id]),
	Id int identity(1,1) primary key
)

GO