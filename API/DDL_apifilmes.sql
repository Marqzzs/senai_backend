CREATE DATABASE Filmes
USE Filmes

CREATE TABLE Genero
(
	IdGenero int primary key identity,
	Nome varchar(50) NOT NULL
)

CREATE TABLE Filme
(
	IdFilme int primary key identity,
	IdGenero int foreign key references Genero(IdGenero) NOT NULL,
	Nome varchar (50) NOT NULL
)

CREATE TABLE Usuario
(
	IdUsuario INT PRIMARY KEY IDENTITY,
	Email VARCHAR(50) NOT NULL UNIQUE,
	Senha VARCHAR(50) NOT NULL,
	Permissao VARCHAR(20) NOT NULL
)

INSERT INTO Genero (Nome)
VALUES ('Terror'), ('Comedia')

INSERT INTO Filme (IdGenero, Nome)
VALUES (1, 'A freira'), (2, 'As branquelas')

INSERT INTO Usuario VALUES('admin@admin.com', 'admin', 'Administrador'), ('comum@comum.com', 'comum', 'Comum')

select * from Usuario

drop table Filme
drop table Genero
drop table Usuario

/*
create database Catalogo;

use Catalogo

create table Genero
(
	IdGenero int primary key identity,
	NomeGenero varchar(20)
)

create table Filme
(
	IdFilme int primary key identity,
	IdGenero int foreign key references Genero(IdGenero),
	NomeFilme varchar(50)
)