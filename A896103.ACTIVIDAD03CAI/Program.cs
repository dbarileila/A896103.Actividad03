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
        static Dictionary<int, PlanDeCuentas> plan = new Dictionary<int, PlanDeCuentas>();
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
                Console.WriteLine("c. Generar (guardar cambios realizados en Diario.txt).");
                Console.WriteLine("d. Actualizar.");
                Console.WriteLine("e. Terminar.");

                var tecla = Console.ReadKey(intercept: true);
                if (tecla.Key == ConsoleKey.A)
                {
                    LeerPlanCuentas();
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
                    GenerarOActualizar();
                }
                else if (tecla.Key == ConsoleKey.E)
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

        private static void LeerPlanCuentas()
        {

            string ruta;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Ingrese la ruta para PlandeCuentas.txt: ");
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
                        var pl = PlanDeCuentas.Parse(linea);
                        if (plan.ContainsKey(pl.CodigoCuenta))
                        {
                            plan[pl.CodigoCuenta] = pl;
                        }
                        else
                        {
                            Console.WriteLine($"{pl.CodigoCuenta}|{pl.NombreCuenta}|{pl.Tipo}");
                        }

                    }
                }
            }
        }

        private static void GenerarOActualizar()
        {
            Console.WriteLine("Ingrese la ruta del Diario.txt para generar el asiento en el archivo.");
            string ruta = Console.ReadLine();
            if (File.Exists(ruta))
            {
                Console.WriteLine("El archivo ya existe. ¿Desea sobreescribirlo? [S/N]");
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

            Console.WriteLine("El asiento se ha cargado exitosamente.");

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
    

