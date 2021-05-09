using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A896103.ACTIVIDAD03CAI
{
    class Program
    {

        static List<PlanDeCuentas> plan = new List<PlanDeCuentas>();
        static Dictionary<int, Asiento> asientos = new Dictionary<int, Asiento>();

        /*A partir de los asientos ingresados por el usuario, una aplicación debe generar/actualizar un
               archivo de texto llamado “Diario.txt” con el siguiente formato por línea:
               NroAsiento|Fecha|CodigoCuenta|Debe|Haber
               Utilizará el archivo de plan de cuentas adjunto para validar los datos ingresados (recuerde
               especialmente: Debe = Haber y Activo = Pasivo + PN). 
           */
        static void Main(string[] args)
        {
            while (true)
            {
               
                Console.WriteLine("Bienvenido. Seleccione la opción que desea realizar:");
                Console.WriteLine();
                Console.WriteLine("a. Visualizar el plan de cuentas.");
                Console.WriteLine("b. Ingreso de nuevo asiento.");
                Console.WriteLine("c. Generar/Actualizar (guardar cambios realizados en Diario.txt).");
                Console.WriteLine("d. Terminar.");

                var tecla = Console.ReadKey(intercept: true);
                if (tecla.Key == ConsoleKey.A)
                {
                    ConsultarPlanCuentas();

                }
                else if (tecla.Key == ConsoleKey.B)
                {
                    IngresoNuevoAsiento();
                }
                else if (tecla.Key == ConsoleKey.C)
                {
                    GenerarOActualizar();
                }
                else if (tecla.Key == ConsoleKey.D)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("No es una opción en la lista.");
                   
                }
              
                Console.WriteLine();

            }

            Console.WriteLine("El ejercicio ha finalizado.");
            Console.ReadKey(intercept: true);

        }

        private static void ConsultarPlanCuentas()
        {

            string ruta;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Ingrese la ruta para PlandeCuentas.txt: ");
                Console.WriteLine();
                ruta = Console.ReadLine();
                if (!File.Exists(ruta))
                {
                    Console.WriteLine("El archivo no exite.");
                }

            } while (!File.Exists(ruta));


            using (var archivo = File.OpenRead(ruta))
            {
                using (var reader = new StreamReader(archivo))
                {
                    while (!reader.EndOfStream)
                    {
                        var linea = reader.ReadLine();
                        if (linea == "Codigo|Nombre|Tipo")
                        {
                            continue;
                        }
                        else
                        {
                            var pl = PlanDeCuentas.Parse(linea);
                            plan.Add(pl);
                            Console.WriteLine($"{pl.CodigoCuenta}|{pl.NombreCuenta}|{pl.Tipo}");
                        }

                    }
                    reader.Close();
                }
            }
            Console.WriteLine();
            Console.ReadKey(intercept: true);


        }
        private static void GenerarOActualizar()
        {

            Console.WriteLine("Se cargaran los asientos en el archivo Diario.txt");
            string ruta = @"Diario.txt";
            if (File.Exists(ruta))
            {
                Console.WriteLine("¿Desea sobreescribirlo? [S/N]");
                if (Console.ReadKey(intercept: true).Key == ConsoleKey.S)
                {
                    File.Delete(ruta);
                }
            }

            using (var writer = File.AppendText(ruta))
            {
                foreach (var asiento in asientos)
                {
                    writer.WriteLine($"{asiento.Value.NroAsiento}|{asiento.Value.Fecha}|{asiento.Value.CodigoCuenta}|" +
                        $"{asiento.Value.Debe}|{asiento.Value.Haber}");
                }
            }

            Console.WriteLine("El asiento se ha cargado exitosamente en el archivo Diario.txt");

        }


        private static void IngresoNuevoAsiento()
        {
            bool seguir = true;
            while (seguir)
            {
                var asiento = Asiento.Ingreso();
                if (asientos.ContainsKey(asiento.NroAsiento))
                {
                    Console.WriteLine("El número de asiento ingresado ya exite.");
                }
                else
                {
                    asientos.Add(asiento.NroAsiento, asiento);
                    Console.WriteLine($"Se ha ingresado un nuevo asiento con el número: {asiento.NroAsiento}");
                    Console.Write("¿Desea ingresar otro [S/N]: ");
                    var tecla = Console.ReadKey(intercept: true);
                    seguir = tecla.Key == ConsoleKey.S;

                }
            }
        }





    }
}
    

