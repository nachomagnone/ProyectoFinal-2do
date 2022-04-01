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
    public class Ciudades
    {
        string codigo_Ciudad;
        string nombre_pais;
        string nombre_ciudad;

        [DataMember]
        public string Codigo_Ciudad
        {
            get
            {
                return codigo_Ciudad;
            }

            set
            {
                if (value.Length != 6 )
                    throw new Exception("El codigo de la ciudad debe ser de 6");
                codigo_Ciudad = value;
            }
        }

        [DataMember]
        public string Nombre_pais
        {
            get
            {
                return nombre_pais;
            }

            set
            {
                if (value.Trim() == string.Empty)
                    throw new Exception("Pais no puede quedar vacio");
                else if (value.Length > 30)
                    throw new Exception("El pais puede tener hasta 30 caracteres");
                nombre_pais = value;
            }
        }

        [DataMember]
        public string Nombre_ciudad
        {
            get
            {
                return nombre_ciudad;
            }

            set
            { if(value.Trim()==string.Empty)
                    throw new Exception("Ciudad no puede quedar vacio");
                else if (value.Length > 50)
                    throw new Exception("El pais puede tener hasta 50 caracteres");
                nombre_ciudad = value;
            }
        }

        public Ciudades (string pCodigo , string pPais, string pCiudad)
        {
            Codigo_Ciudad = pCodigo;
            Nombre_pais = pPais;
            Nombre_ciudad = pCiudad;
        }

        public Ciudades() { }
    }
}
