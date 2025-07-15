using Act_Bebidas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act_Bebidas.Services
{
    public class VinoService
    {
        // Metodo que inserta un vino en la tabla Vinos de la base de datos
        // Requiere el ID de la bebida general (ya insertada en la tabla Bebidas)
        // Usa una conexion activa para ejecutar la insercion
        public void AgregarVinoASQL(Vino vino, int idBebida, SqlConnection connection)
        {
            string query = @"INSERT INTO Cervezas (idBebida, alcohol, marca)
                     VALUES (@idBebida, @alcohol, @marca)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idBebida", idBebida);
                command.Parameters.AddWithValue("@alcohol", vino.Alcohol);
                command.Parameters.AddWithValue("@marca", vino.Marca);

                command.ExecuteNonQuery();
            }
        }

        // Metodo que solicita al usuario los datos especificos de un vino
        // Devuelve un objeto Vino con la informacion ingresada por consola
        public Vino CargarDatosDeVino(int cantidad, string nombre)
        {
            Console.WriteLine("Ingresar el grado de alcohol:");
            int alcohol = GuardClause.GuardClause.ValidarOpcion(1, 100);

            Console.WriteLine("Ingresar la marca:");
            string marca = Console.ReadLine();

            return new Vino
            {
                Cantidad = cantidad,
                Nombre = nombre,
                Tipo = "Vino",
                Alcohol = alcohol,
                Marca = marca
            };
        }
    }
}
