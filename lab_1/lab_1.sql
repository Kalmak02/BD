create type status_enum as enum ('Totally fine', 'Minor repair required', 'Reconstruction required');
create type type_enum as enum ('Mobile devices', 'Computer electronics', 'Household appliances');
create type condition_enum as enum ('New', 'Little shabby', 'Replacement required');

create table Storage(
	StorageID serial not null,
	Area varchar(10) not null,
	Owner varchar(40) not null,
	Status status_enum not null,
	constraint storage_primary_key primary key (StorageID)
);

create table Zone(
	ZoneID serial not null,
	StorageID integer not null,
	Capacity varchar(40) not null,
	Fullness varchar(40) not null,
	constraint zone_primary_key primary key (ZoneID),
	constraint storage_foreign_key foreign key (StorageID)
		references Storage (StorageID) match simple
		on update cascade
		on delete cascade
);

create table Stuff(
	StuffID serial not null,
	ZoneID integer not null,
	Type type_enum not null,
	Name varchar(40) not null,
	Price integer not null,
	constraint stuff_primary_key primary key (StuffID),
	constraint zone_foreign_key foreign key (ZoneID)
		references Zone (ZoneID) match simple
		on update cascade
		on delete cascade
);

create table Employee(
	EmployeeID serial not null,
	Full_Name varchar(40) not null,
	Salary integer not null,
	constraint employee_primary_key primary key (EmployeeID)
);

create table Locker(
	LockerID serial not null,
	Condition condition_enum not null,
	constraint locker_primary_key primary key (LockerID)
);

create table Employee_Locker(
	EmployeeID integer not null,
	LockerID integer not null,
	constraint employee_foreign_key foreign key (EmployeeID)
		references Employee (EmployeeID) match simple
		on update cascade
		on delete cascade,
	constraint locker_foreign_key foreign key (LockerID)
		references Locker (LockerID) match simple
		on update cascade
		on delete cascade
);

create table Zone_Employee(
	ZoneID integer not null,
	EmployeeID integer not null,
	constraint zone_foreign_key foreign key (ZoneID)
		references Zone (ZoneID) match simple
		on update cascade
		on delete cascade,
	constraint employee_foreign_key foreign key (EmployeeID)
		references Employee (EmployeeID) match simple
		on update cascade
		on delete cascade
);

insert into Storage (Area, Owner, Status) values
('650 m^2', 'I. V. Hrushevskiy', 'Totally fine'),
('800 m^2', 'M. A. Zubenko', 'Minor repair required');

insert into Zone (StorageID, Capacity, Fullness) values
((select StorageID from Storage where Storage.Area = '650 m^2'), '100 tons', '65%'),
((select StorageID from Storage where Storage.Area = '650 m^2'), '150 tons', '80%'),
((select StorageID from Storage where Storage.Area = '650 m^2'), '50 tons', '10%'),
((select StorageID from Storage where Storage.Area = '650 m^2'), '180 tons', '90%'),
((select StorageID from Storage where Storage.Area = '800 m^2'), '200 tons', '85%'),
((select StorageID from Storage where Storage.Area = '800 m^2'), '200 tons', '90%'),
((select StorageID from Storage where Storage.Area = '800 m^2'), '100 tons', '95%');

insert into Stuff (ZoneID, Type, Name, Price) values
((select ZoneID from Zone where Zone.ZoneID = 1), 'Mobile devices', 'Samsung Galaxy S9', '14000'),
((select ZoneID from Zone where Zone.ZoneID = 2), 'Computer electronics', 'Intel i7-12900KF', '16000'),
((select ZoneID from Zone where Zone.ZoneID = 3), 'Household appliances', 'Samsung RB38T600ESA', '21000'),
((select ZoneID from Zone where Zone.ZoneID = 4), 'Household appliances', 'Samsung WW60A4S00CE', '18000'),
((select ZoneID from Zone where Zone.ZoneID = 5), 'Computer electronics', 'Nvidia RTX 3090 Ti', '60000'),
((select ZoneID from Zone where Zone.ZoneID = 6), 'Computer electronics', 'MSI Pulse GL66', '50000'),
((select ZoneID from Zone where Zone.ZoneID = 7), 'Mobile devices', 'Google Pixel 4 XL', '8000');

insert into Employee (Full_Name, Salary) values
('Dubchenko Ivan Oleksandrovich', '24000'),
('Kvitka Igor Andriyovich', '50000'),
('Yaremenko Oksana Ivanivna', '20000'),
('Bondarenko Irina Volodimirivna', '25000'),
('Holkina Olena Igorivna', '23000'),
('Vukolov Danyil Oleksandrovich', '28000'),
('Mospanova Maria Mykolayivna', '52000'),
('Doroshenko Oleksandr Ivanovich', '26000');

insert into Locker (Condition) values
('New'),
('New'),
('New'),
('Little shabby'),
('Little shabby'),
('Replacement required'),
('Replacement required'),
('Replacement required');

insert into Zone_Employee (ZoneID, EmployeeID) values
(1,3), (2,1), (3,2), (3,7), (4,5), (5,6), (6,8), (6,4), (7,2), (7,7);

insert into Employee_Locker (EmployeeID, LockerID) values
(1,2), (2,7), (3,1), (4,6), (5,4), (6,5), (7,3), (8,8);