using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    public class Pronosticos
    {
        private int codigo_interno;
        private DateTime fecha;
        private Ciudades ciudad;
        private Meteorologo usu;
        private List<Pronostico_Hora> proH;

        [DataMember]
        public int Codigo_interno
        {
            get
            {
                return codigo_interno;
            }

            set
            {
                codigo_interno = value;
            }
        }

        [DataMember]
        public DateTime Fecha
        {
            get
            {
                return fecha;
            }

            set
            {            
                fecha = value;
            }
        }

        [DataMember]
        public Ciudades Ciudad
        {
            get
            {
                return ciudad;
            }

            set
            {
                if (value == null)
                    throw new Exception("debe haber una ciudad asociada");
                ciudad = value;
            }
        }

        [DataMember]
        public Meteorologo Usu
        {
            get
            {
                return usu;
            }

            set
            {
                if (value == null)
                    throw new Exception("debe haber un usuario asociado");
                usu = value;
            }
        }

        [DataMember]
        public List<Pronostico_Hora> ProH
        {
            get { return proH; }
            set
            {
              
                if (value == null)
                    throw new Exception("debe haber un pronostico");
                else if (value.Count > 24)
                    throw new Exception("El dia tiene 24 horas");
                proH = value;
            }
        }

        public Pronosticos(int pCodigo, DateTime pFecha, Ciudades pCiudad, Meteorologo pUsu, List<Pronostico_Hora> pProH)
        {
            Codigo_interno = pCodigo;
            Fecha = pFecha;
            Ciudad = pCiudad;
            Usu = pUsu;
            ProH = pProH;
        }

        public Pronosticos() { }
    }
}
