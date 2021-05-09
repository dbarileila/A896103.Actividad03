using System;
using System.Linq;
using System.Text;
using System.Globalization;
using System.IO;

namespace A896103.ACTIVIDAD03CAI
{
    internal class PlanDeCuentas
    {

        public PlanDeCuentas(int codigo, string nombrec, string tipo)
        {
            CodigoCuenta = codigo;
            NombreCuenta = nombrec;
            Tipo = tipo;
        }

        public PlanDeCuentas()
        {

        }
        public int CodigoCuenta { get; }
        public string NombreCuenta { get; }
        public string Tipo { get; }


        internal static PlanDeCuentas Parse(string linea)
        {
            var datos = linea.Split('|');
            return new PlanDeCuentas(codigo: int.Parse(datos[0]), nombrec: datos[1], tipo: datos[2]);
        }

       


    }
}