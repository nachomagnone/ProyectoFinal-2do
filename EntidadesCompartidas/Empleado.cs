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
    public class Empleado : Usuario
    {
        private int horas_semanales;
       // private Usuario unUsu;

        [DataMember]
        public int Horas_semanales
        {
            get
            {
                return horas_semanales;
            }

            set
            {
                if (value > 40)
                    throw new Exception("No puede ser mayora a 40 horas semanales");
                else if (value <= 0)
                    throw new Exception("La cantidad de horas no puede ser 0 ni negativa");
                horas_semanales = value;
            }
        }

        public Empleado(string pUsu, string pConstrasena, string pNom, int pHoras) : base(pUsu, pConstrasena, pNom)
        {
            Horas_semanales = pHoras;
        }

        public Empleado():base() { }
    }
}
