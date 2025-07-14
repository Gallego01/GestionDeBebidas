using Act_Bebidas.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Act_Bebidas.Services
{
    public class CervezaService
    {
        public void AgregarCervezaASQL(Cerveza cerveza, int idBebidas, SqlConnection connection)
        {
            string query = @"INSERT INTO Cervezas (idBebida, alcohol, marca)
                     VALUES (@idBebida, @alcohol, @marca)";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@idBebida", idBebidas);
                command.Parameters.AddWithValue("@alcohol", cerveza.Alcohol);
                command.Parameters.AddWithValue("@marca", cerveza.Marca);

                command.ExecuteNonQuery();
            }
        }

        public Cerveza CargarDatosDeCerveza(int cantidad, string nombre)
        {
            Console.WriteLine("Ingresar el grado de alcohol:");
            int alcohol = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingresar la marca:");
            string marca = Console.ReadLine();

            return new Cerveza
            {
                Cantidad = cantidad,
                Nombre = nombre,
                Tipo = "Cerveza",
                Alcohol = alcohol,
                Marca = marca
            };
        }
    }
}
