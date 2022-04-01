using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica
{
    public class Fabrica
    {
        public static ILogicaUsuarios GetLU()
        {
            return LogicaUsuarios.GetInstancia();
        }

        public static ILogicaPronosticos GetLP()
        {
            return LogicaPronosticos.GetInstancia();
        }

        public static ILogicaCiudades GetLC()
        {
            return LogicaCiudades.GetInstancia();
        }
    }
}
