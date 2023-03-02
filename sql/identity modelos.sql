use Modelo


select * from Modelo.dbo.AspNetRoleClaims
select * from Modelo.dbo.AspNetRoles
select * from Modelo.dbo.AspNetUserClaims
select * from Modelo.dbo.AspNetUserLogins
select * from Modelo.dbo.AspNetUserRoles
select * from Modelo.dbo.AspNetUsers
select * from Modelo.dbo.AspNetUserTokens


select * from Modelo.dbo.AspNetUsers
select * from Modelo.dbo.AspNetRoles
select * from Modelo.dbo.AspNetUserRoles



select * from sysalerts
sp_help 'AspNetUserRoles'
user id = 1a0a852a-4586-4dc3-bcf8-f6cc2a72736b

insert into Modelo.dbo.AspNetRoles 
select '','Admin','',''

insert into Modelo.dbo.AspNetUserRoles 
select '2e516d58-7de6-49ce-9cca-40cedb3cee90','1a0a85a3-4586-4dc3-bcf8-f6cc2a72736b'


update t
	set
		t.Id  = '1a0a85a3-4586-4dc3-bcf8-f6cc2a72736b',
		t.Name = 'Admin',
		t.NormalizedName = 'ADMIN',
		T.ConcurrencyStamp = 'cc46b031-9297-48d5-83d5-5ffde38b14cc'
from	
	Modelo.dbo.AspNetRoles t