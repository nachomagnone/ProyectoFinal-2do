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
    internal class LogicaPronosticos:ILogicaPronosticos
    {
        #region singleton
        private static LogicaPronosticos _instancia = null;

        private LogicaPronosticos() { }

        public static LogicaPronosticos GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaPronosticos();

            return _instancia;
        }
        #endregion

        public void AgregarPronostico(Pronosticos P, Meteorologo met)
        {
            DateTime fehcahoy = DateTime.Now.Date;
            if (P.Fecha < fehcahoy)
                throw new Exception("La fecha debe ser mayor a la acutal");
            else
            Persistencia.Fabrica.GetPP().AgregarPronostico(P, met);
        }
        public List<Pronosticos> ListarPronosticoAnio(Empleado emp)
        {
            return (Persistencia.Fabrica.GetPP().ListarPronosticoAnio(emp));
        }
        public List<Pronosticos> ListarPronosticodeHoy()
        {
            return (Persistencia.Fabrica.GetPP().ListarPronosticodeHoy());
        }
    }
}
