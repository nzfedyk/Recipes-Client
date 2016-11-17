create table Category(
	ID int not null identity(1, 1),
	ParentID int,
	Name nvarchar(64) not null,

	constraint pk_Category_ID primary key(ID),
	constraint fk_Category_ParentID_Category_ID foreign key(ParentID) references Category(ID) 
);

create table Recipe(
	ID int not null identity(1, 1),
	CategoryID int not null,
	Name nvarchar(200) not null,
	Ingredients nvarchar(1500) not null,
	CookingText nvarchar(2500) not null,
	
	constraint pk_Recipe_ID primary key(ID),
	constraint fk_Recipe_CategoryID_Category_ID foreign key(CategoryID) references Category(ID)
);
/* =================================================================================================== */

