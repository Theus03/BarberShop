create database db_BarberShop
default character set utf8
default collate utf8_general_ci;

use db_BarberShop;

create table tbl_login(
	user_login varchar(50) not null,
    senha_login varchar(50) not null,
    tipo_login int
);
insert into tbl_login(user_login, senha_login, tipo_login) values("Matheus03", "123", 1);
insert into tbl_login(user_login, senha_login, tipo_login) values("Julia", "12345", 2);

select * from tbl_login;

create table tbl_cliente(
	cd_cliente int primary key auto_increment,
    nm_cliente varchar(70),
    telefone_cliente varchar(13),
    celular_cliente varchar(15),
    email_cliente varchar(50)
);

create table tbl_barbeiro(
	cd_barbeiro int primary key auto_increment,
    nm_barbeiro varchar(70) not null,
    cpf_barbeiro varchar(14),
    telefone_barbeiro varchar(13)
);

create table tbl_reserva(
	cd_reserva int primary key auto_increment,
    cd_cliente int references tbl_cliente(cd_cliente),
    cd_barbeiro int references tbl_barbeiro(cd_barbeiro),
    data_reserva varchar(15) not null,
    hora varchar(10) not null
);
select tbl_reserva.cd_reserva, tbl_reserva.data_reserva,
tbl_reserva.hora, tbl_cliente.nm_cliente,
tbl_barbeiro.nm_barbeiro from tbl_reserva, tbl_cliente,
tbl_barbeiro where tbl_reserva.cd_barbeiro = tbl_barbeiro.cd_barbeiro and tbl_reserva.cd_cliente = tbl_cliente.cd_cliente;
