using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Act_Bebidas.GuardClause
{
    public class GuardClause
    {
        // Valida si una opcion ingresada por el usuario esta dentro del rango especificado
        // Solicita repetidamente un numero hasta que sea valido
        // Devuelve la opcion validada
        public static int ValidarOpcion(int minimo, int maximo)
        {
            bool pudo = false;
            int opcion = 0;
            while (!pudo)
            {
                pudo = int.TryParse(Console.ReadLine(), out opcion);
                if (!pudo || opcion < minimo || opcion > maximo)
                {
                    pudo = false;
                    Console.WriteLine(string.Concat("Solo numeros entre ", minimo, " y ", maximo, ".\nIntente nuevamente: "));
                }
            }
            return opcion;
        }

        // Metodo que muestra un menu por consola para seleccionar el tipo de bebida
        // Si el parametro incluirTodos es true, agrega una opcion adicional "Todos"
        // Devuelve el nombre de la bebida seleccionada como string
        public static string MostrarMenuBebidas(bool incluirTodos = true)
        {
            List<string> tiposDeVehiculos = new List<string> { "Vino", "Cerveza"};

            if (incluirTodos)
            {
                tiposDeVehiculos.Add("Todos");
            }

            for (int i = 0; i < tiposDeVehiculos.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {tiposDeVehiculos[i]}");
            }

            Console.Write("Seleccionar Bebida: ");
            int opcion = ValidarOpcion(1, tiposDeVehiculos.Count);

            return tiposDeVehiculos[opcion - 1];
        }
    }
}
