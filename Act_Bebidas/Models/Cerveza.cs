using Act_Bebidas.Interfaces;
using System;

namespace Act_Bebidas.Models
{
    public class Cerveza : Bebidas, IBebidaAlcoholica
    {
        public Cerveza(int id, int cantidad, string nombre, int alcohol, string marca)
            : base(id, cantidad, nombre, "Cerveza") // El tipo se establece directamente desde acá
        {
            Alcohol = alcohol;
            Marca = marca;
        }
        public int IdBebida { get; set; }
        public int Alcohol { get; set; }
        public string Marca { get; set; }

        public Cerveza() { }
    }
}
