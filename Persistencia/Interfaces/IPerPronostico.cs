using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPerPronostico
    {
        void AgregarPronostico(Pronosticos P, Meteorologo met);
        List<Pronosticos> ListarPronosticoAnio(Empleado emp);
        List<Pronosticos> ListarPronosticodeHoy();
    }
}
