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
    public class Meteorologo : Usuario
    {
        //ATRIBUTOS
        private string mail;
        private string telefono;

        [DataMember]
        public string Mail
        {
            get
            {
                return mail;
            }

            set
            {
                if (value.Trim() == string.Empty)
                    throw new Exception("El Mail no puede ser vacio");
                else if (value.Length > 50)
                    throw new Exception("Mail hasta 50 caracteres");
                Regex rx = new Regex("[a-z0-9]{1,18}[@][a-z0-9]{1,7}[.][a-z0-9]{1,3}");
                if (!rx.IsMatch(value))
                    throw new Exception("El formato del Mail es incorrecto ");
                mail = value;
            }
        }

        [DataMember]
        public string Telefono
        {
            get
            {
                return telefono;
            }

            set
            {
                Regex rx = new Regex("[0-9]");
                if (!rx.IsMatch(value))
                    throw new Exception("Debe ser numerico");
                else if (value.Trim() == string.Empty)
                    throw new Exception("El telefono no puede ser nulo");
                else if (value.Length > 50)
                    throw new Exception("Telefono hasta 50 caracteres");
                telefono = value;
            }
        }

        public Meteorologo (string pUsu, string pConstrasena, string pNom, string pMail, string pTel): base(pUsu,pConstrasena,pNom)
        {
            Mail = pMail;
            Telefono = pTel;
        }

        public  Meteorologo():base() { }
    }
}
