create database desafio_dev_01;
use desafio_dev_01;

create table pessoa(
id int primary key auto_increment,
nome varchar(200) not null,
cpf_cnpj varchar(18) not null,
email varchar(70) not null,
logradouro varchar(200) not null,
logradouro_numero varchar(10) not null,
logradouro_complemento varchar(20),
bairro varchar(40) not null,
cep varchar(9) not null,
cidade varchar(70) not null,
estado varchar(2) not null,
CONSTRAINT pessoa_cpf_cnpj_unico UNIQUE (cpf_cnpj)
);

create table usuario(
id int primary key auto_increment,
nome varchar(200) not null,
cpf_cnpj varchar(14) not null,
email varchar(70) not null,
login_usuario varchar(50) not null,
login_senha varchar(100) not null,
logradouro varchar(200) not null,
logradouro_numero varchar(10) not null,
logradouro_complemento varchar(20),
bairro varchar(40) not null,
cep varchar(9) not null,
cidade varchar(70) not null,
estado varchar(2) not null,
CONSTRAINT usuario_cpf_cnpj_unico UNIQUE (cpf_cnpj)
);
