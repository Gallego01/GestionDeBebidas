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

        public Vino CargarDatosDeVino(int cantidad, string nombre)
        {
            Console.WriteLine("Ingresar el grado de alcohol:");
            int alcohol = int.Parse(Console.ReadLine());

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
