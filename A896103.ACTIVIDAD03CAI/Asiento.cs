using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace A896103.ACTIVIDAD03CAI
{
    internal class Asiento
    {
        private Asiento(int nro, DateTime fecha, int codigo, decimal debe, decimal haber)
        {
            NroAsiento = nro;
            Fecha = fecha;
            CodigoCuenta = codigo;
            Debe = debe;
            Haber = debe;
        
        }
        public int NroAsiento { get; }
        public DateTime Fecha { get; }
        public int CodigoCuenta { get; }
        public decimal Debe { get; set; }
        public decimal Haber { get; set; }
 

        internal static Asiento Ingreso()
        {
            Console.WriteLine();
            Console.WriteLine("Nuevo asiento.");
            Console.WriteLine("El formato cargado debe ser NroAsiento|Fecha|CodigoCuenta|Debe|Haber.");
            bool ok = false;
            int asiento = 0;

            while (!ok)
            {
                Console.WriteLine("Ingrese el número de asiento: ");

                var ingreso = Console.ReadLine();
                if (!int.TryParse(ingreso, out asiento))
                {
                    Console.WriteLine("Debe ingresar un número de asiento válido.");
                    continue;
                }

                if (asiento < 1)
                {
                    Console.WriteLine("El número de asiento debe ser mayor a cero.");
                    continue;
                }

                ok = true;
            }

            var fecha = new DateTime(); 
            ok = false;
            while (!ok)
            {
                Console.WriteLine();
                Console.WriteLine("Ingrese la fecha que desea cargar: ");
                Console.WriteLine("Formato: MM/DD/AA.");
                var ingreso = Console.ReadLine();

                if (!DateTime.TryParse(ingreso, out fecha))
                {
                    Console.WriteLine("Debe ingresar una fecha válida.");
                    continue;
                }

                if (fecha > DateTime.Now)
                {
                    Console.WriteLine("La fecha ingresada debe ser menor a la actual.");
                    continue;
                }
                
                ok = true;
            }

            bool seguir = true;
            int codigo = 0;
            decimal debe = 0.0m;
            decimal haber = 0.0m;
            do
            {

                ok = false;
                while (!ok)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ingrese un código de cuenta según el archivo Diario.txt: ");
                    Console.WriteLine("El código debe estar entre 11 y 34");
                    var ingreso = Console.ReadLine();

                    if (!int.TryParse(ingreso, out codigo))
                    {
                        Console.WriteLine("El código ingresado no es numérico.");
                        continue;
                    }

                    if (codigo < 11 || codigo > 34)
                    {
                        Console.WriteLine("El código ingresado no se encuentra en el Diario");
                        continue;
                    }


                    ok = true;
                }
                
                ok = false;
                while (!ok)
                {
                    Console.WriteLine("Si la cuenta posee saldo deudor ingrese D, si es acreedor ingrese H? D/H: ");
                    var tecla1 = Console.ReadKey(intercept: true);
                    if (tecla1.Key == ConsoleKey.D)
                    {
                        Console.WriteLine();
                        Console.WriteLine("DEBE");
                        Console.WriteLine();
                        Console.WriteLine("Formato válido a ingresar: decimal (ej: 100.00)");

                        Console.Write("Importe: $ ");
                        var ingreso = Console.ReadLine();
                        if (!decimal.TryParse(ingreso, out debe))
                        {
                            Console.WriteLine("El importe debe ser númerico.");
                            continue;
                        }
                        else if (debe < 0)
                        {
                            Console.WriteLine("El importe debe ser mayor o igual a cero.");
                            continue;
                        }
                      

                    }
                    else if(tecla1.Key == ConsoleKey.H)
                    {
                        Console.WriteLine();
                        Console.WriteLine("HABER");
                        Console.WriteLine();
                        Console.WriteLine("Formato válido a ingresar: decimal (ej: 100.00)");
                        Console.Write("Importe: $ ");
                        var ingreso = Console.ReadLine();
                        if (!decimal.TryParse(ingreso, out haber))
                        {
                            Console.WriteLine("El importe debe ser númerico.");
                            continue;
                        }
                        else if (haber < 0)
                        {
                            Console.WriteLine("El importe debe ser mayor o igual a cero.");
                            continue;
                        }

                    }
                    ok = true;

                   

                }
                Console.WriteLine("Seleccione 'I' para cargar una otra cuenta y respetar la igualdad DEBE = HABER o cualquier tecla para seguir.");
                var tecla = Console.ReadKey(intercept: true);
                seguir = tecla.Key == ConsoleKey.I;
                if (debe != haber)
                {
                    Console.WriteLine("No se respeta la igualdad contable DEBE = HABER.");
                    continue;
                }


            } while (seguir);

            return new Asiento(asiento, fecha, codigo, debe, haber);
        }

    }
}