using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    public class Fabrica
    {
        public static IPerCiudades GetPC()
        {
            return PerCiudades.GetInstancia();
        }

        public static IPerEmpleado GetPE()
        {
            return PerEmpleado.GetInstancia();
        }

        public static IPerMeteorologo GetPM()
        {
            return PerMeteorologo.GetInstancia();
        }

        public static IPerPronostico GetPP()
        {
            return PerPronosticos.GetInstancia();
        }

  
    }
}
