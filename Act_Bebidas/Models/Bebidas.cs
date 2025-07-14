using System;

namespace Act_Bebidas.Models
{
    public class Bebidas
    {
        public int Id { get; set; }
        public int Cantidad { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }

        public Bebidas() { }

        public Bebidas(int id, int cantidad, string nombre, string tipo)
        {
            Id = id;
            Cantidad = cantidad;
            Nombre = nombre;
            Tipo = tipo;
        }

        public void Tomarse(int cuantoBebio)
        {
            Cantidad -= cuantoBebio;
        }

        public void MaxRecomendado()
        {
            Console.WriteLine("El máximo son 5 cervezas");
        }
    }
}
