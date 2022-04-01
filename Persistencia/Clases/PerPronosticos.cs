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
    internal class PerPronosticos:IPerPronostico
    {
        #region singleton
        private static PerPronosticos _instancia = null;

        private PerPronosticos() { }

        public static PerPronosticos GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PerPronosticos();

            return _instancia;
        }
        #endregion

        public void AgregarPronostico(Pronosticos P, Meteorologo met)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(met));
            SqlCommand oComando = new SqlCommand("AltaPronostico", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _fecha_pub = new SqlParameter("@Fecha_pub", P.Fecha);
            SqlParameter _usu = new SqlParameter("@Usu_Log", met.UsuLog);
            SqlParameter _cod_ciu = new SqlParameter("@Cod_ciu", P.Ciudad.Codigo_Ciudad);

            SqlParameter _Retorno = new SqlParameter("@ret", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.Output;

            oComando.Parameters.Add(_fecha_pub);
            oComando.Parameters.Add(_usu);
            oComando.Parameters.Add(_cod_ciu);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;
            SqlTransaction _transaccion = null;
            List<Pronostico_Hora> pronosticos = P.ProH;
            try
            {
                oConexion.Open();
                _transaccion = oConexion.BeginTransaction();
                oComando.Transaction = _transaccion;

                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@ret"].Value;
                if (oAfectados == -1)
                    throw new Exception("El Meteorologo no existe");
                if (oAfectados == -2)
                    throw new Exception("El codigo de Ciudad no existe");

                foreach (Pronostico_Hora PH in P.ProH)
                {
                   // Pronostico_Hora pro = new Pronostico_Hora(1, "despejado", 11, 10, 10, 10, 10);
                    PerPronosticoHora.GetInstancia().AgregarPronosticoHora(oAfectados, PH, _transaccion);
                }
                _transaccion.Commit();

            }
            catch (Exception ex)
            {
                _transaccion.Rollback();
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }//ok

        public List<Pronosticos> ListarPronosticoAnio(Empleado emp)
        {
            List<Pronosticos> _Lista = new List<Pronosticos>();

            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand _Comando = new SqlCommand("ListarPronosticodelAnio", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader _Reader;

            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();

                while (_Reader.Read())
                {
                    int cod_int = (int)_Reader["Codigo_Interno"];
                    DateTime fecha_p = Convert.ToDateTime(_Reader["Fecha_P"]);
                    string usu = (string)_Reader["UsuLog"];
                    string cod_ciu = (string)_Reader["Codigo_Ciudad"];

                    Ciudades C = PerCiudades.GetInstancia().BuscarTodasCiudad(cod_ciu);
                    Meteorologo U = PerMeteorologo.GetInstancia().BuscarTodosMeteorologo(usu);
                    List<Pronostico_Hora> PH = PerPronosticoHora.GetInstancia().ListarPronosticoDiario(cod_int);

                    Pronosticos P = new Pronosticos(cod_int, fecha_p, C, U, PH);
                    _Lista.Add(P);
                }

                _Reader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Conexion.Close();
            }

            return _Lista;
        }//ok
        public List<Pronosticos> ListarPronosticodeHoy()
        {
            List<Pronosticos> _Lista = new List<Pronosticos>();

            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn());
            SqlCommand _Comando = new SqlCommand("ListarPronosticosHoy", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader _Reader;

            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();

                while (_Reader.Read())
                {
                    int cod_int = (int)_Reader["Codigo_Interno"];
                    DateTime fecha_p = Convert.ToDateTime(_Reader["Fecha_P"]);
                    string usu = (string)_Reader["UsuLog"];
                    string cod_ciu = (string)_Reader["Codigo_Ciudad"];

                    Ciudades C = PerCiudades.GetInstancia().BuscarTodasCiudad(cod_ciu);
                    Meteorologo U = PerMeteorologo.GetInstancia().BuscarTodosMeteorologo(usu);
                    List<Pronostico_Hora> PH = PerPronosticoHora.GetInstancia().ListarPronosticoDiario(cod_int);

                    Pronosticos P = new Pronosticos(cod_int, fecha_p, C, U, PH);
                    _Lista.Add(P);
                }

                _Reader.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _Conexion.Close();
            }

            return _Lista;
        }//ok
    }
}
