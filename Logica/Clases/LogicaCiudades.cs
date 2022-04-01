using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntidadesCompartidas;
using Persistencia;
using System.Data;
using System.Data.SqlClient;

namespace Logica
{
    internal class LogicaCiudades:ILogicaCiudades
    {
        #region singleton
        private static LogicaCiudades _instancia = null;

        private LogicaCiudades() { }

        public static LogicaCiudades GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaCiudades();

            return _instancia;
        }
        #endregion

        public void AgregarCiudad(Ciudades C, Empleado emp)
        {
            Persistencia.Fabrica.GetPC().AgregarCiudad(C, emp);
        }
        public void EliminarCiudad(Ciudades C, Empleado emp)
        {
            Persistencia.Fabrica.GetPC().EliminarCiudad(C, emp);
        }
        public void ModificarCiudad(Ciudades C, Empleado emp)
        {
            Persistencia.Fabrica.GetPC().ModificarCiudad(C, emp);
        }
        public Ciudades BuscarCiudad(string pCodigo, Usuario usu)
        {
            return (Persistencia.Fabrica.GetPC().BuscarCiudad(pCodigo, usu));
        }
        public List<Ciudades> ListarCiudadSinPronxAno(int anio, Empleado emp)
        {
            return (Persistencia.Fabrica.GetPC().ListarCiudadSinPronxAno(anio, emp));
        }
        public List<Ciudades> ListarCiudades(Usuario met)
        {
            return (Persistencia.Fabrica.GetPC().ListarCiudades(met));
        }
        public List<Ciudades> ListarTodaslasCiudades(Empleado emp)
        {
            return (Persistencia.Fabrica.GetPC().ListarTodaslasCiudades(emp));
        }
    }
}
