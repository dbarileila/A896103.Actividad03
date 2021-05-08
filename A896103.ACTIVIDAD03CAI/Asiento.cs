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
            Haber = haber;
        }
        public int NroAsiento { get; }
        public DateTime Fecha { get; }
        public int CodigoCuenta { get; }
        public decimal Debe { get; }
        public decimal Haber { get; }

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

                if (fecha > DateTime.Today)
                {
                    Console.WriteLine("La fecha ingresada debe ser menor a la actual.");
                    continue;
                }

                ok = true;
            }

            
            
                int codigo = 0;
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

                decimal debe = 0.0m;
                ok = false;
                while (!ok)
                {
                    Console.WriteLine();
                    Console.WriteLine("Si el código de cuenta ingresado tiene saldo deudor ingrese en el DEBE el importe, sino ingrese cero (0).");
                    Console.WriteLine(" --> Activo (+).");
                    Console.WriteLine(" --> Pasivo (-).");
                    Console.WriteLine(" --> PN     (-).");
                    Console.WriteLine();
                    Console.WriteLine("Formato válido a ingresar: decimal (ej: 100.00)");

                    var ingreso = Console.ReadLine();
                    if (!decimal.TryParse(ingreso, out debe))
                    {
                        Console.WriteLine("El importe debe ser númerico.");
                        continue;
                    }

                    if (debe < 0)
                    {
                        Console.WriteLine("El importe debe ser mayor o igual a cero.");
                        continue;
                    }

                    ok = true;
                }

                decimal haber = 0.0m;
                ok = false;
                while (!ok)
                {
                    Console.WriteLine();
                    Console.WriteLine("Si el código de cuenta ingresado tiene saldo acreedor ingrese en el HABER el importe, sino ingrese cero (0).");
                    Console.WriteLine(" --> Activo (-).");
                    Console.WriteLine(" --> Pasivo (+).");
                    Console.WriteLine(" --> PN     (+).");
                    Console.WriteLine();
                    Console.WriteLine("Formato válido a ingresar: decimal (ej: 100.00)");

                    var ingreso = Console.ReadLine();
                    if (!decimal.TryParse(ingreso, out haber))
                    {
                        Console.WriteLine("El importe debe ser númerico.");
                        continue;
                    }

                    if (haber < 0)
                    {
                        Console.WriteLine("El importe debe ser mayor o igual a cero.");
                        continue;
                    }

                    ok = true;
                }
                return new Asiento(asiento, fecha, codigo, debe, haber);


            
        }

    }
}