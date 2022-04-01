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
    internal class PerCiudades:IPerCiudades
    {
        #region singleton
        private static PerCiudades _instancia = null;

        private PerCiudades() { }

        public static PerCiudades GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PerCiudades();

            return _instancia;
        }
        #endregion
        public void AgregarCiudad(Ciudades C, Empleado emp)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("AltaCiudades", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _codigo = new SqlParameter("@Cod_ciu", C.Codigo_Ciudad);
            SqlParameter _nombre_pais = new SqlParameter("@Nom_pais", C.Nombre_pais);
            SqlParameter _nombre_ciudad = new SqlParameter("@Nom_ciu", C.Nombre_ciudad);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_codigo);
            oComando.Parameters.Add(_nombre_pais);
            oComando.Parameters.Add(_nombre_ciudad);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("El Codigo de la Ciudad ya existe");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }// OK

        public void EliminarCiudad(Ciudades C, Empleado emp)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("BajaCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _codigo = new SqlParameter("@Codigo", C.Codigo_Ciudad);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_codigo);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("La Ciudad no existe - No se elimina");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }     
        }//OK

        public void ModificarCiudad(Ciudades C, Empleado emp)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("ModificarCiudad", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _codigo = new SqlParameter("@Codigo", C.Codigo_Ciudad);
            SqlParameter _nom_pais = new SqlParameter("@NombreP", C.Nombre_pais);
            SqlParameter _nom_ciudad = new SqlParameter("NombreC", C.Nombre_ciudad);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_codigo);
            oComando.Parameters.Add(_nom_pais);
            oComando.Parameters.Add(_nom_ciudad);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("No existe la Ciudad - No se modifico");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }     

        }//OK

        public Ciudades BuscarCiudad(string pCodigo, Usuario usu)
        {
            string _codigo;
            string _nombrepais;
            string _nombreciudad;
            Ciudades c = null;

            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(usu));
            SqlCommand oComando = new SqlCommand("BuscarCiudadesActivo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@cod_ciu", pCodigo);

            SqlDataReader oReader;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if (oReader.Read())
                {
                    _codigo = (string)oReader["Codigo_Ciudad"];
                    _nombrepais = (string)oReader["Nombre_Pais"];
                    _nombreciudad = (string)oReader["Nombre_Ciudad"];
                    c = new Ciudades(_codigo, _nombrepais, _nombreciudad);
                }

                oReader.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                oConexion.Close();
            }
            return c;
        }//OK

        internal Ciudades BuscarTodasCiudad(string pCodigo)
        {
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn());
            Ciudades _unaCiudad = null;

            SqlCommand _comando = new SqlCommand("BuscarTodasLasCiudades", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@cod_ciu", pCodigo);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    _unaCiudad = new Ciudades(pCodigo, (string)_lector["Nombre_Pais"], (string)_lector["Nombre_Ciudad"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _cnn.Close();
            }
            return _unaCiudad;
        }//OK

        public List<Ciudades> ListarCiudadSinPronxAno(int anio, Empleado emp)
        {
            List<Ciudades> _Lista = new List<Ciudades>();
            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand _Comando = new SqlCommand("ListarCiudadSinPronxAno", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@anio", anio);

            SqlDataReader _Reader;

            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();

                while (_Reader.Read())
                {
                    string _cod = (string)_Reader["Codigo_Ciudad"];
                    string _nom_Pais = (string)_Reader["Nombre_Pais"];
                    string _nom_Ciudad = (string)_Reader["Nombre_Ciudad"];
                    Ciudades c = new Ciudades(_cod, _nom_Pais, _nom_Ciudad);
                    _Lista.Add(c);
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
        }//OK

        public List<Ciudades>ListarCiudades(Usuario met)
        {
            string codigoInterno;
            string nombre_Ciudad;
            string nombre_Pais;
            
            List<Ciudades> _Lista = new List<Ciudades>();

            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn(met));
            SqlCommand _Comando = new SqlCommand("ListarCiudades", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader _Reader;
            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();

                while (_Reader.Read())
                {
                    codigoInterno = (string)_Reader["Codigo_Ciudad"];
                    nombre_Pais = (string)_Reader["Nombre_Pais"];
                    nombre_Ciudad = (string)_Reader["Nombre_Ciudad"];

                    Ciudades m = new Ciudades(codigoInterno, nombre_Pais, nombre_Ciudad);
                    _Lista.Add(m);
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

        public List<Ciudades> ListarTodaslasCiudades(Empleado emp)
        {
            string codigoInterno;
            string nombre_Ciudad;
            string nombre_Pais;

            List<Ciudades> _Lista = new List<Ciudades>();

            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand _Comando = new SqlCommand("ListarTodaslasCiudades", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            SqlDataReader _Reader;
            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();

                while (_Reader.Read())
                {
                    codigoInterno = (string)_Reader["Codigo_Ciudad"];
                    nombre_Pais = (string)_Reader["Nombre_Pais"];
                    nombre_Ciudad = (string)_Reader["Nombre_Ciudad"];

                    Ciudades m = new Ciudades(codigoInterno, nombre_Pais, nombre_Ciudad);
                    _Lista.Add(m);
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
