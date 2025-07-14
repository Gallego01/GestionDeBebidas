using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.Json;
using Act_Bebidas.Context;
using Act_Bebidas.Models;

namespace Act_Bebidas.Services
{
    public class BebidaService
    {
        private readonly BebidasContext bebidasContext;
        CervezaService cervezaService = new CervezaService();
        VinoService vinoService = new VinoService();

        public BebidaService()
        {
            bebidasContext = new BebidasContext();
        }

        public void AgregarBebidaASQL(Bebidas bebida)
        {
            try
            {
                using (var connection = bebidasContext.GetConnection())
                {
                    connection.Open();

                    // Insertar en tabla base Bebidas y obtener el ID generado
                    string query = @"INSERT INTO Bebidas (cantidad, nombre, tipo)
                             VALUES (@cantidad, @nombre, @tipo);
                             SELECT CAST(scope_identity() AS int)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@cantidad", bebida.Cantidad);
                    command.Parameters.AddWithValue("@nombre", bebida.Nombre);
                    command.Parameters.AddWithValue("@tipo", bebida.Tipo);

                    int bebidaId = (int)command.ExecuteScalar();

                    switch (bebida.Tipo.Trim().ToLower())
                    {
                        case "cerveza":
                            cervezaService.AgregarCervezaASQL((Cerveza)bebida, bebidaId, connection);
                            break;

                        case "vino":
                            vinoService.AgregarVinoASQL((Vino)bebida, bebidaId, connection);
                            break;
                    }

                    Console.WriteLine("Bebida agregada exitosamente");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar la bebida: {ex.Message}");
            }
        }

        public void CargarBebida()
        {
            int totalProcesadas = 0;
            int totalInsertadas = 0;
            int totalOmitidas = 0;
            int totalErrores = 0;

            int opcionSeguir = 0;
            do
            {
                try
                {
                    totalProcesadas++;

                    Console.WriteLine("Ingrese la cantidad:");
                    int cantidad = GuardClause.GuardClause.ValidarOpcion(1, 10000);

                    Console.WriteLine("Ingrese el nombre:");
                    string nombre = Console.ReadLine();

                    Console.WriteLine("Ingrese el tipo de bebida (Cerveza, Vino):");
                    string tipo = GuardClause.GuardClause.MostrarMenuBebidas(false);

                    Bebidas bebida = null;

                    if (tipo.Trim().ToLower() == "cerveza")
                    {
                        bebida = cervezaService.CargarDatosDeCerveza(cantidad, nombre);

                        if (bebida is Cerveza cerveza)
                        {
                            if (!ExisteDuplicado("cerveza.txt", cerveza.Nombre, cerveza.Marca, "cerveza"))
                            {
                                GuardarBebidasComoJson(cerveza, "cerveza", "cerveza.txt");
                                LeerYMostrarBebidasDesdeJson("cerveza.txt", "cerveza");
                                totalInsertadas++;
                                AgregarBebidaASQL(cerveza);
                            }
                            else
                            {
                                Console.WriteLine("Ya existe una cerveza con ese nombre y marca. Se omitió");
                                totalOmitidas++;
                            }
                        }
                    }
                    else if (tipo.Trim().ToLower() == "vino")
                    {
                        bebida = vinoService.CargarDatosDeVino(cantidad, nombre);

                        if (bebida is Vino vino)
                        {
                            if (!ExisteDuplicado("vino.txt", vino.Nombre, vino.Marca, "vino"))
                            {
                                GuardarBebidasComoJson(vino, "vino", "vino.txt");
                                LeerYMostrarBebidasDesdeJson("vino.txt", "vino");
                                totalInsertadas++;
                                AgregarBebidaASQL(vino);
                            }
                            else
                            {
                                Console.WriteLine("Ya existe un vino con ese nombre y marca. Se omitió");
                                totalOmitidas++;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tipo de bebida inválido. Intente nuevamente.");
                        totalErrores++;
                        continue;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Error al cargar la bebida: {ex.Message}");
                    totalErrores++;
                }

                Console.WriteLine("\n¿Desea agregar otra bebida?");
                Console.WriteLine("1 - SI");
                Console.WriteLine("2 - NO");
                opcionSeguir = GuardClause.GuardClause.ValidarOpcion(1, 2);

            } while (opcionSeguir != 2);
            
            Console.WriteLine("\nRESUMEN:");
            Console.WriteLine($"- Total procesadas: {totalProcesadas}");
            Console.WriteLine($"- Insertadas: {totalInsertadas}");
            Console.WriteLine($"- Omitidas (duplicadas): {totalOmitidas}");
            Console.WriteLine($"- Errores: {totalErrores}");
        }


        public void GuardarBebidasComoJson(Bebidas nuevaBebida, string tipo, string nombreArchivo)
        {
            try
            {
                if (tipo.ToLower() == "cerveza" && nuevaBebida is Cerveza nuevaCerveza)
                {
                    List<Cerveza> cervezas = File.Exists(nombreArchivo)
                        ? JsonSerializer.Deserialize<List<Cerveza>>(File.ReadAllText(nombreArchivo)) ?? new List<Cerveza>() : new List<Cerveza>();

                    cervezas.Add(nuevaCerveza);
                    File.WriteAllText(nombreArchivo, JsonSerializer.Serialize(cervezas, new JsonSerializerOptions { WriteIndented = true }));
                }
                else if (tipo.ToLower() == "vino" && nuevaBebida is Vino nuevoVino)
                {
                    List<Vino> vinos = File.Exists(nombreArchivo)
                        ? JsonSerializer.Deserialize<List<Vino>>(File.ReadAllText(nombreArchivo)) ?? new List<Vino>() : new List<Vino>();

                    vinos.Add(nuevoVino);
                    File.WriteAllText(nombreArchivo, JsonSerializer.Serialize(vinos, new JsonSerializerOptions { WriteIndented = true }));
                }
            
                Console.WriteLine($"Bebida guardada en {nombreArchivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al guardar JSON: {ex.Message}");
            }
        }

        public void LeerYMostrarBebidasDesdeJson(string nombreArchivo, string tipo)
        {
            try
            {
                switch (tipo.Trim().ToLower())
                {
                    case "cerveza":
                        List<Cerveza> cervezas = JsonSerializer.Deserialize<List<Cerveza>>(File.ReadAllText(nombreArchivo));
                        foreach (var cerveza in cervezas)
                        {
                            Console.WriteLine($"[Cerveza] Nombre: {cerveza.Nombre}, Cantidad: {cerveza.Cantidad}%, Tipo: {cerveza.Tipo}, " +
                                              $"Alcohol: {cerveza.Alcohol}°, Marca: {cerveza.Marca}");
                        }
                        break;

                    case "vino":
                        List<Vino> vinos = JsonSerializer.Deserialize<List<Vino>>(File.ReadAllText(nombreArchivo));
                        foreach (var vino in vinos)
                        {
                            Console.WriteLine($"[Vino] Nombre: {vino.Nombre}, Cantidad: {vino.Cantidad}%, Tipo: {vino.Tipo}, " +
                                              $"Alcohol: {vino.Alcohol}°, Marca: {vino.Marca}");
                        }
                        break;
                }
            }
            catch
            {
                Console.WriteLine($"\nError al leer el archivo {nombreArchivo}");
            }
        }

        public bool ExisteDuplicado(string nombreArchivo, string nombre, string marca, string tipo)
        {
            try
            {
                if (!File.Exists(nombreArchivo))
                    return false;

                string json = File.ReadAllText(nombreArchivo);

                if (tipo.ToLower() == "cerveza")
                {
                    List<Cerveza> cervezas = JsonSerializer.Deserialize<List<Cerveza>>(json);
                    return cervezas.Any(c => c.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)
                                          && c.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase));
                }
                else if (tipo.ToLower() == "vino")
                {
                    List<Vino> vinos = JsonSerializer.Deserialize<List<Vino>>(json);
                    return vinos.Any(v => v.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)
                                       && v.Marca.Equals(marca, StringComparison.OrdinalIgnoreCase));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar duplicado: {ex.Message}");
            }

            return false;
        }
    }
}
