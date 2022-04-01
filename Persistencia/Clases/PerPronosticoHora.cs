using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntidadesCompartidas;
using System.Data;
using System.Data.SqlClient;

namespace Persistencia
{
    internal class PerPronosticoHora
    {
        #region singleton
        private static PerPronosticoHora _instancia = null;

        private PerPronosticoHora() { }

        public static PerPronosticoHora GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PerPronosticoHora();

            return _instancia;
        }
        #endregion

        internal void AgregarPronosticoHora(int cod_int, Pronostico_Hora H, SqlTransaction trn)
        {
            SqlCommand oComando = new SqlCommand("AltaPronostico_Horas", trn.Connection);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _cod_int = new SqlParameter("@Cod_int", cod_int);
            SqlParameter _hora_pro = new SqlParameter("@Hora_pro", H.Hora_pronostico);
            SqlParameter _tipo_cielo = new SqlParameter("@Tipo_cielo", H.Tipo_Cielo);
            SqlParameter _temp_max = new SqlParameter("@Temp_max", H.Temp_Max);
            SqlParameter _temp_min = new SqlParameter("@Temp_min", H.Temp_Min);
            SqlParameter _prob_lluvia = new SqlParameter("@Probabi_lluvia", H.Probabilidad_Lluvias);
            SqlParameter _prob_tormenta = new SqlParameter("@Probabi_tormenta", H.Probabilidad_Tormentas);
            SqlParameter _vel_viento = new SqlParameter("@Veloci_viento", H.Velocidad_Viento);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_cod_int);
            oComando.Parameters.Add(_hora_pro);
            oComando.Parameters.Add(_tipo_cielo);
            oComando.Parameters.Add(_temp_max);
            oComando.Parameters.Add(_temp_min);
            oComando.Parameters.Add(_prob_lluvia);
            oComando.Parameters.Add(_prob_tormenta);
            oComando.Parameters.Add(_vel_viento);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;

            try
            {
                oComando.Transaction = trn;
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("EL Codigo del Pronostico no Existe");
                if (oAfectados == -2)
                    throw new Exception("Ya existe el pronotistico hora que se desea agregar");
                if (oAfectados == -3)
                    throw new Exception("Error de ejecucion");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        internal List<Pronostico_Hora> ListarPronosticoDiario(int pCodigo_int)
        {
            List<Pronostico_Hora> _Lista = new List<Pronostico_Hora>();

            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn());
            SqlCommand _Comando = new SqlCommand("ListarPronosticoDiario", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@codigo_interno", pCodigo_int);

            SqlDataReader _Reader;
            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();

                while (_Reader.Read())
                {
                    int _hpron = (int)_Reader["Hora_Pronostico"];
                    string _tipocielo = (string)_Reader["Tipo_Cielo"];
                    int _tempmax = (int)_Reader["Temp_Max"];
                    int _tempmin = (int)_Reader["Temp_Min"];
                    int _prolluvias = (int)_Reader["Probabilidad_Lluvias"];
                    int _protormentas = (int)_Reader["Probabilidad_Tormenta"];
                    int _velviento = (int)_Reader["Velocidad_Viento"];
                    Pronostico_Hora H = new Pronostico_Hora(_hpron, _tipocielo, _tempmax, _tempmin, _prolluvias, _protormentas, _velviento);
                    _Lista.Add(H);
                }
                _Reader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _Conexion.Close();
            }
            return _Lista;
        }
        
    }
}
