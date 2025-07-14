# Gestión de Bebidas (Cerveza y Vino)

Este proyecto de consola en C# permite cargar bebidas (Cerveza y Vino), almacenarlas en archivos JSON y en una base de datos SQL Server, y mostrar información relevante como un resumen de carga y filtrado por tipo o si son alcohólicas.

## Funcionalidades principales

- Carga interactiva de bebidas desde consola.
- Validación de datos antes de guardar (nombre, cantidad, tipo, marca, grado de alcohol).
- Evita duplicados según combinación **Nombre + Marca**.
- Almacenamiento en archivos JSON separados para Cerveza y Vino.
- SQL Server con tablas separadas (**Bebidas**, **Cervezas**, **Vinos**).

## Resumen final luego de la carga:

- Total de bebidas procesadas.
- Cantidad insertada.
- Cantidad omitida por duplicados.
- Errores por formato u otros.

## Ejecutar query de SQL para un buen funcionamiento

```sql
CREATE TABLE Bebidas (
    id INT PRIMARY KEY IDENTITY(1,1),
    nombre NVARCHAR(100),
    cantidad INT,
    tipo NVARCHAR(50)
);
CREATE TABLE Cervezas (
    idBebida INT PRIMARY KEY,
    alcohol INT,
    marca NVARCHAR(100)
);
CREATE TABLE Vinos (
    idBebida INT PRIMARY KEY,
    alcohol INT,
    marca NVARCHAR(100)
);

ALTER TABLE Cervezas
ADD CONSTRAINT FK_Cervezas_Bebidas
FOREIGN KEY (idBebida) REFERENCES Bebidas(id);

ALTER TABLE Vinos
ADD CONSTRAINT FK_Vinos_Bebidas
FOREIGN KEY (idBebida) REFERENCES Bebidas(id);
```
