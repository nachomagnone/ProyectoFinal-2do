using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPerEmpleado
    {
        Empleado Logueo(string pUsu, string pass);
        void AgregarEmpleado(Empleado E, Empleado emp);
        void ModificarEmpleado(Empleado E, Empleado emp);
        void EliminarEmpleado(Empleado E, Empleado emp);
        Empleado BuscarEmpleado(string pUsulog, Usuario emp);
    }
}
