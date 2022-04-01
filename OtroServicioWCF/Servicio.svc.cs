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
    
    public class Servicio : IServicio
    {
        #region Ciudades
       
        void IServicio.AgregarCiudad(Ciudades C, Empleado emp)
        {
            Logica.Fabrica.GetLC().AgregarCiudad(C, emp);
        }

        void IServicio.EliminarCiudad(Ciudades C, Empleado emp)
        {
            Logica.Fabrica.GetLC().EliminarCiudad(C, emp);
        }
       
        void IServicio.ModificarCiudad(Ciudades C, Empleado emp)
        {
            Logica.Fabrica.GetLC().ModificarCiudad(C, emp);
        }
        
        Ciudades IServicio.BuscarCiudad(string pCodigo, Usuario usu)
        {
            return (Logica.Fabrica.GetLC().BuscarCiudad(pCodigo, usu));
        }

        List<Ciudades> IServicio.ListarCiudadSinPronxAno(int anio, Empleado emp)
        {
            return (Logica.Fabrica.GetLC().ListarCiudadSinPronxAno(anio, emp));
        }

        List<Ciudades> IServicio.ListarCiudades(Meteorologo met)
        {
            return (Logica.Fabrica.GetLC().ListarCiudades(met));
        }

        List<Ciudades> IServicio.ListarTodasLasCiudades(Empleado emp)
        {
            return (Logica.Fabrica.GetLC().ListarTodaslasCiudades(emp));
        }

        #endregion

        #region Usuarios

        Usuario IServicio.Logueo(string pUsu, string pass)
        {
            return (Logica.Fabrica.GetLU().Logueo(pUsu, pass));
        }

        void IServicio.AgregarUsuario(Usuario u, Empleado emp)
        {
            Logica.Fabrica.GetLU().AgregarUsuario(u, emp);
        }
        void IServicio.ModificarUsuario(Usuario u, Empleado emp)
        {
            Logica.Fabrica.GetLU().ModificarUsuario(u, emp);
        }
        void IServicio.EliminarUsuario(Usuario u, Empleado emp)
        {
            Logica.Fabrica.GetLU().EliminarUsuario(u, emp);
        }
        Usuario IServicio.BuscarUsuario(string pUsulog, Usuario usu)
        {
            return (Logica.Fabrica.GetLU().BuscarUsuario(pUsulog, usu));
        }
        void IServicio.ModificarPassMeteorologo(Meteorologo M, Meteorologo met)
        {
            Logica.Fabrica.GetLU().ModificarPassMeteorologo(M, met);
        }
        List<Meteorologo> IServicio.ListarMeteorologoSinPronxAno(int pAnio, Empleado emp)
        {
            return (Logica.Fabrica.GetLU().ListarMeteorologoSinPronxAno(pAnio, emp));
        }

        #endregion

        #region Pronostico

        void IServicio.AgregarPronostico(Pronosticos P, Meteorologo met)
        {
            Logica.Fabrica.GetLP().AgregarPronostico(P, met);
        }

        List<Pronosticos> IServicio.ListarPronosticoAnio(Empleado emp)
        {
            return (Logica.Fabrica.GetLP().ListarPronosticoAnio(emp));
        }

        XmlElement IServicio.ListarPronosticodeHoy()
        {
            List<Pronosticos> lista = Logica.Fabrica.GetLP().ListarPronosticodeHoy();

            XmlDocument documento = new XmlDocument();
            documento.LoadXml("<?xml version='1.0' encoding='utf-8' ?> <Pronosticos> </Pronosticos>");
            //XmlNode _raiz = documento.CreateNode(XmlNodeType.Element, "Pronosticos", "");

            foreach (Pronosticos unP in lista)
            {
                XmlNode _raiz = documento.CreateNode(XmlNodeType.Element, "Pronosticos", "");

                XmlElement _ciudad = documento.CreateElement("Nombre_Ciudad");
                _ciudad.InnerText = unP.Ciudad.Nombre_ciudad.ToString();
                _raiz.AppendChild(_ciudad);

                XmlElement _pais = documento.CreateElement("Nombre_Pais");
                _pais.InnerText = unP.Ciudad.Nombre_pais.ToString();
                _raiz.AppendChild(_pais);



                foreach (Pronostico_Hora ph in unP.ProH)
                {
                    XmlElement _ph = documento.CreateElement("Pronostico_Hora");

                    XmlElement _hora = documento.CreateElement("Hora_pronostico");
                    _hora.InnerText = ph.Hora_pronostico.ToString();
                    _ph.AppendChild(_hora);

                    XmlElement _tipo_Cielo = documento.CreateElement("Tipo_Cielo");
                    _tipo_Cielo.InnerText = ph.Tipo_Cielo.ToString();
                    _ph.AppendChild(_tipo_Cielo);

                    XmlElement _temp_Max = documento.CreateElement("Temp_Max");
                    _temp_Max.InnerText = ph.Temp_Max.ToString();
                    _ph.AppendChild(_temp_Max);

                    XmlElement _temp_Min = documento.CreateElement("Temp_Min");
                    _temp_Min.InnerText = ph.Temp_Min.ToString();
                    _ph.AppendChild(_temp_Min);

                    XmlElement _probabilidad_lluvias = documento.CreateElement("Probabilidad_Lluvias");
                    _probabilidad_lluvias.InnerText = ph.Probabilidad_Lluvias.ToString();
                    _ph.AppendChild(_probabilidad_lluvias);

                    XmlElement _probabilidad_tormentas = documento.CreateElement("Probabilidad_Tormentas");
                    _probabilidad_tormentas.InnerText = ph.Probabilidad_Tormentas.ToString();
                    _ph.AppendChild(_probabilidad_tormentas);

                    XmlElement _velocidad_viento = documento.CreateElement("Velocidad_Viento");
                    _velocidad_viento.InnerText = ph.Velocidad_Viento.ToString();
                    _ph.AppendChild(_velocidad_viento);

                    _raiz.AppendChild(_ph);
                }




                documento.DocumentElement.AppendChild(_raiz);
            }





            return documento.DocumentElement;
        }

        #endregion
    }
}
