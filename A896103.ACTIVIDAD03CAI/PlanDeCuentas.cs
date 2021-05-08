using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace A896103.ACTIVIDAD03CAI
{
    internal class PlanDeCuentas
    {

        private PlanDeCuentas(int codigo, string nombrec, string tipo)
        {
            CodigoCuenta = codigo;
            NombreCuenta = nombrec;
            Tipo = tipo;
        }

        public int CodigoCuenta { get; }
        public string NombreCuenta { get; }
        public string Tipo { get; }


        internal static PlanDeCuentas Parse(string linea)
        {
            var datos = linea.Split('|');
            return new PlanDeCuentas(codigo: int.Parse(datos[0]), nombrec: (datos[1]), tipo: (datos[2]));

        }

    }
}