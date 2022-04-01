using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using System.Xml;
using EntidadesCompartidas;

namespace OtroServicioWCF
{
    [ServiceContract]
    public interface IServicio
    {
        #region Ciudades

        [OperationContract]
        void AgregarCiudad(Ciudades C, Empleado emp);

        [OperationContract]
        void EliminarCiudad(Ciudades C, Empleado emp);

        [OperationContract]
        void ModificarCiudad(Ciudades C, Empleado emp);

        [OperationContract]
        Ciudades BuscarCiudad(string pCodigo, Usuario usu);

        [OperationContract]
        List<Ciudades> ListarCiudadSinPronxAno(int anio, Empleado emp);

        [OperationContract]
        List<Ciudades> ListarCiudades(Meteorologo met);

        [OperationContract]

        List<Ciudades> ListarTodasLasCiudades(Empleado emp);

        #endregion

        #region Usuarios

        [OperationContract]
        Usuario Logueo(string pUsu, string pass);
        [OperationContract]
        void AgregarUsuario(Usuario u, Empleado emp);
        [OperationContract]
        void ModificarUsuario(Usuario u, Empleado emp);
        [OperationContract]
        void ModificarPassMeteorologo(Meteorologo M, Meteorologo met);
        [OperationContract]
        void EliminarUsuario(Usuario u, Empleado emp);
        [OperationContract]
        Usuario BuscarUsuario(string pUsulog, Usuario usu);
        [OperationContract]
        List<Meteorologo> ListarMeteorologoSinPronxAno(int pAnio, Empleado emp);

        #endregion

        #region Pronostico

        [OperationContract]
        void AgregarPronostico(Pronosticos P, Meteorologo met);

        [OperationContract]
        List<Pronosticos> ListarPronosticoAnio(Empleado emp);

        [OperationContract]
        XmlElement ListarPronosticodeHoy();

        #endregion
    }
}
