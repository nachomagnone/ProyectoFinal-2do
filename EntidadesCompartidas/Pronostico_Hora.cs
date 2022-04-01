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
    public class Pronostico_Hora
    {
        int hora_pronostico;
        string tipo_Cielo;
        int temp_Max;
        int temp_Min;
        int probabilidad_Lluvias;
        int probabilidad_Tormentas;
        int velocidad_Viento;

        [DataMember]
        public int Hora_pronostico
        {
            get
            {
                return hora_pronostico;
            }

            set
            {
                if (value > 23)
                    throw new Exception("El dia tiene 24 horas");
                else if (value < 0)
                    throw new Exception("no existe hora en negativo ");
                hora_pronostico = value;
            }
        }

        [DataMember]
        public string Tipo_Cielo
        {
            get
            {
                return tipo_Cielo;
            }

            set
            {
                if (value.ToString().Trim() == "despejado" || value.ToString().Trim() == "parcialmente nuboso" || value.ToString().Trim() == "nuboso")
                    tipo_Cielo = value;
                else
                    throw new Exception("El cielo solo puede ser Despejado, parcialmente nuboso o nuboso");
            }
        }

        [DataMember]
        public int Temp_Max
        {
            get
            {
                return temp_Max;
            }

            set
            {
                if (value > 100 || value < -100 )
                    throw new Exception("No esta en el rango de temperaturas permitido");
                temp_Max = value;
            }
        }

        [DataMember]
        public int Temp_Min
        {
            get
            {
                return temp_Min;
            }

            set
            {
                if (value > 100 || value < -100)
                    throw new Exception("No esta en el rango de temperaturas permitido");
                else if (value > temp_Max)
                    throw new Exception("La temperatura minima no puede ser mayor a la maxima");
                temp_Min = value;
            }
        }

        [DataMember]
        public int Probabilidad_Lluvias
        {
            get
            {
                return probabilidad_Lluvias;
            }

            set
            {
                if (value > 100 || value < 0)
                    throw new Exception("las probaliidades van de 0 a 100");
                probabilidad_Lluvias = value;
            }
        }

        [DataMember]
        public int Probabilidad_Tormentas
        {
            get
            {
                return probabilidad_Tormentas;
            }

            set
            {
                if (value > 100 || value < 0)
                    throw new Exception("las probaliidades van de 0 a 100");
                probabilidad_Tormentas = value;
            }
        }

        [DataMember]
        public int Velocidad_Viento
        {
            get
            {
                return velocidad_Viento;
            }

            set
            { if (value < 0)
                    throw new Exception("El viento no puede ser negativo ");
                velocidad_Viento = value;
            }
        }

        public Pronostico_Hora (int pHora, string pCielo, int pTempMax, int pTempMin,int pProLLuvia, int pProTorm, int pVelocidadV)
        {
            Hora_pronostico = pHora;
            Tipo_Cielo = pCielo;
            Temp_Max = pTempMax;
            Temp_Min = pTempMin;
            Probabilidad_Lluvias = pProLLuvia;
            Probabilidad_Tormentas = pProTorm;
            Velocidad_Viento = pVelocidadV;
        }

        public Pronostico_Hora() { }
    }
}
