﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act_Bebidas.Context
{
    public class BebidasContext
    {
        // Cadena de conexion a la base de datos SQL Server
        // Incluye el servidor, la base de datos, el usuario y la contraseña
        //private readonly string connectionString = "Server=LAPTOP-8GCRCA54\\SQLEXPRESS01;Initial Catalog= dbBebidas; Integrated Security=true;";
        private readonly string connectionString = "Server=DESKTOP-IE4M0CJ\\SQLEXPRESS;Database=Licoreria;UID=sa;PWD=12345678;TrustServerCertificate=True;";

        // Metodo para obtener una nueva instancia de SqlConnection utilizando la cadena de conexión definida
        // Esta conexion debe abrirse antes de ser utilizada (con connection.Open()) y cerrarse después (con connection.Close())
        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
