using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Act_Bebidas.Models;
using System.IO;
using Act_Bebidas.Services;
namespace Act_Bebidas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BebidaService service = new BebidaService();
            /*Cerveza cerveza = new Cerveza(500, "Cerveza Rubia", 5, "Marca A");
            string cervezaJson = JsonSerializer.Serialize(cerveza);
            File.WriteAllText("cerveza.txt", cervezaJson);
            string cevezaDesJson = File.ReadAllText("cerveza.txt");
            Cerveza cervezaDes = JsonSerializer.Deserialize<Cerveza>(cevezaDesJson);
            Console.WriteLine($"Nombre: {cervezaDes.nombre}, Cantidad: {cervezaDes.cantidad}, Alcohol: {cervezaDes.alcohol}, Marca: {cervezaDes.marca}");¨*/

            service.CargarBebida();
        }
    }
}
