drop database concesionario;
CREATE database concesionario;
use concesionario;
create table fichaauto(
	id int auto_increment primary key,
	marca varchar(16),
    	modelo int(4),
    	color varchar(8),
    	tipo varchar(16),
        cantidad int(3)
	);
INSERT INTO fichaauto (marca, modelo, color, tipo,cantidad) VALUES 
	('mazda',2020,'rojo','automovil',3),
	('toyota',2021,'blanca','camioneta',5),
	('renault',2022,'verde','van',2),
	('kenworth',2023,'azul','tractocamion',6),
	('chevrolet',2024,'blanco','camion',7);
ALTER TABLE fichaauto ADD fecha_compra datetime;
