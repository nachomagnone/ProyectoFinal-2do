using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using System.ServiceModel;
using System.Runtime.Serialization;

namespace EntidadesCompartidas
{
    [DataContract]
    [KnownType(typeof(Empleado))]
    [KnownType(typeof(Meteorologo))]
    public abstract class Usuario
    {
        //ATRIBUTOS
        private string usuLog;
        private string contrasena;
        private string nombre_Completo;

        [DataMember]
        //PROPIEDADES
        public string UsuLog
        {
            get
            {
                return usuLog;
            }

            set
            {
                if (value.Trim() == string.Empty)
                    throw new Exception("Usuario no puede ser vacio");
                else if (value.Length > 20)
                    throw new Exception("El usuario tiene maximo 20 caracteres");
                usuLog = value;
            }
        }

        [DataMember]
        public string Contrasena
        {
            get
            {
                return contrasena;
            }

            set
            {
                Regex rx = new Regex("[0-9]{3}[A-Za-z]{4}[^A-Za-z0-9]{2}");
                if (!rx.IsMatch(value))
                    throw new Exception("El Pass debe estar compuesto primero por 3 numero, luego 4 letras y ultimo 2 simbolos");
                contrasena = value;
            }
        }

        [DataMember]
        public string Nombre_Completo
        {
            get
            {
                return nombre_Completo;
            }

            set
            {   if(value.Trim()== string.Empty)
                    throw new Exception("El nombre no puede ser vacio");
                else if (value.Length > 50)
                    throw new Exception("El nombre tiene maximo 50 caracteres");
                nombre_Completo = value;
            }
        }

        //CONSTRUCTOR
        public Usuario(string pUsu, string pcontrasena, string pNom)
        {
            UsuLog = pUsu;
            Contrasena = pcontrasena;
            Nombre_Completo = pNom;
        }

        public Usuario() { }
    }
}
