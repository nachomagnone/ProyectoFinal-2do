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
    internal class LogicaUsuarios : ILogicaUsuarios
    {
        #region singleton
        private static LogicaUsuarios _instancia = null;

        private LogicaUsuarios() { }

        public static LogicaUsuarios GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaUsuarios();

            return _instancia;
        }
        #endregion
        public Usuario Logueo(string pUsu, string pass)
        {
            Empleado e = (Persistencia.Fabrica.GetPE().Logueo(pUsu, pass));
            if (e == null)
                return (Persistencia.Fabrica.GetPM().Logueo(pUsu, pass));

            else
                return e;
        }
        public void AgregarUsuario(Usuario u, Empleado emp)
        {
            if (u is Empleado)
                Persistencia.Fabrica.GetPE().AgregarEmpleado((Empleado)(u), emp);
            else
                Persistencia.Fabrica.GetPM().AgregarMeteorologo((Meteorologo)(u), emp);
        }
        public void ModificarUsuario(Usuario u, Empleado emp)
        {
            if (u is Empleado)
                Persistencia.Fabrica.GetPE().ModificarEmpleado((Empleado)(u), emp);
            else
                Persistencia.Fabrica.GetPM().ModificarMeteorologo((Meteorologo)(u), emp);
        }
        public void ModificarPassMeteorologo(Meteorologo M, Meteorologo met)
        {
            Persistencia.Fabrica.GetPM().ModificarPassMeteorologo(M, met);
        }
        public void EliminarUsuario(Usuario u, Empleado emp)
        {
            if (u is Empleado)
                Persistencia.Fabrica.GetPE().EliminarEmpleado((Empleado)(u), emp);
            else
                Persistencia.Fabrica.GetPM().EliminarMeteorologo((Meteorologo)(u), emp);
        }
        public Usuario BuscarUsuario(string pUsulog, Usuario usu)
        {
            Empleado e = Persistencia.Fabrica.GetPE().BuscarEmpleado(pUsulog,usu);
            if (e == null)
            {
                Meteorologo m = Persistencia.Fabrica.GetPM().BuscarMeteorologo(pUsulog, usu);
                if (m == null)
                    return e;
                else
                    return m;
            }
            else
                return e;
             
        }
        public List<Meteorologo> ListarMeteorologoSinPronxAno(int pAnio, Empleado emp)
        {
            return (Persistencia.Fabrica.GetPM().ListarMeteorologoSinPronxAno(pAnio, emp));
        }
    }
}
