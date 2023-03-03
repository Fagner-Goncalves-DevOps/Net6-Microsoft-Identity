use Modelo

/*

clonar aplicação, rodar add-migration depois update-database
na sequencia rodar aplicação e criar um usuario, apos isso seguir passos abaixo
necessario criar um usuario admin direto no sql server, rodar comando abaixo na sequencia

*/

--Rodar 1° para criar a role admin

insert into Modelo.dbo.AspNetRoles 
select '1a0a85a3-4586-4dc3-bcf8-f6cc2a72736b','Admin','ADMIN','cc46b031-9297-48d5-83d5-5ffde38b14cc'

--Rodar 2° add usuario criado acima para role admin

insert into Modelo.dbo.AspNetUserRoles 
select '2e516d58-7de6-49ce-9cca-40cedb3cee90','1a0a85a3-4586-4dc3-bcf8-f6cc2a72736b'
