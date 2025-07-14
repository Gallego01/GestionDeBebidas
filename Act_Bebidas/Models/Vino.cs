using System;

namespace Act_Bebidas.Models
{
    public class Vino : Bebidas
    {
        public Vino(int id, int cantidad, string nombre, int alcohol, string marca)
            : base(id, cantidad, nombre, "Vino")
        {
            Alcohol = alcohol;
            Marca = marca;
        }

        public int IdBebida { get; set; }
        public int Alcohol { get; set; }
        public string Marca { get; set; }

        public Vino() { }
    }
}
