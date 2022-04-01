using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntidadesCompartidas;

namespace Persistencia
{
    public interface IPerMeteorologo
    {
        Meteorologo Logueo(string pUsu, string pass);
        void AgregarMeteorologo(Meteorologo M, Empleado emp);
        void ModificarMeteorologo(Meteorologo M, Empleado emp);
        void ModificarPassMeteorologo(Meteorologo M, Meteorologo met);
        void EliminarMeteorologo(Meteorologo M, Empleado emp);
        Meteorologo BuscarMeteorologo(string pUsulog, Usuario usu);       
        List<Meteorologo> ListarMeteorologoSinPronxAno(int pAnio, Empleado emp);
        
    }
}
