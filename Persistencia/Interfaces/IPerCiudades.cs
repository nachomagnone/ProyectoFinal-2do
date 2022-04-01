using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPerCiudades
    {
        void AgregarCiudad(Ciudades C, Empleado emp);
        void EliminarCiudad(Ciudades C, Empleado emp);
        void ModificarCiudad(Ciudades C, Empleado emp);
        Ciudades BuscarCiudad(string pCodigo, Usuario usu);      
        List<Ciudades> ListarCiudadSinPronxAno(int anio, Empleado emp);
        List<Ciudades> ListarCiudades(Usuario met);
        List<Ciudades> ListarTodaslasCiudades(Empleado emp);
    }
}
