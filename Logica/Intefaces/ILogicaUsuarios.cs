using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaUsuarios
    {
        Usuario Logueo(string pUsu, string pass);
        void AgregarUsuario(Usuario u, Empleado emp);
        void ModificarUsuario(Usuario u, Empleado emp);
        void ModificarPassMeteorologo(Meteorologo M, Meteorologo met);
        void EliminarUsuario(Usuario u, Empleado emp);
        Usuario BuscarUsuario(string pUsulog, Usuario usu);
        List<Meteorologo> ListarMeteorologoSinPronxAno(int pAnio, Empleado emp);
    }
}
