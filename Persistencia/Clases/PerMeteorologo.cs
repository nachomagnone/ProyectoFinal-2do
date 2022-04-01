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
    internal class PerMeteorologo:IPerMeteorologo
    {
        #region singleton
        private static PerMeteorologo _instancia = null;

        private PerMeteorologo() { }

        public static PerMeteorologo GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PerMeteorologo();

            return _instancia;
        }
        #endregion
        public Meteorologo Logueo(string pUsu, string pass)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn());
            string usuLog;
            string contrasena;
            string nombre_Completo;
            string telefono;
            string mail;
            Meteorologo M = null;
            
            SqlCommand oComando = new SqlCommand("Logueo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@UsuLog", pUsu);
            oComando.Parameters.AddWithValue("@contrasena", pass);

            SqlDataReader oReader;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if (oReader.Read())
                {
                    usuLog = (string)oReader["UsuLog"];
                    contrasena = (string)oReader["Contrasena"];
                    nombre_Completo = (string)oReader["Nombre_Completo"];
                    telefono = (string)oReader["Telefono"];
                    mail = (string)oReader["Mail"];
                    M = new Meteorologo(usuLog, contrasena, nombre_Completo, mail, telefono);
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
            return M;
        }//ok
        public void AgregarMeteorologo(Meteorologo M, Empleado emp)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("AltaMeteorologo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _usu = new SqlParameter("@Usu_Log", M.UsuLog);
            SqlParameter _mail = new SqlParameter("@Mail", M.Mail);
            SqlParameter _telefono = new SqlParameter("@Telefono", M.Telefono);
            SqlParameter _pass = new SqlParameter("@pass", M.Contrasena);
            SqlParameter _nombreCompleto = new SqlParameter("@nombre_comp", M.Nombre_Completo);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_usu);
            oComando.Parameters.Add(_mail);
            oComando.Parameters.Add(_telefono);
            oComando.Parameters.Add(_pass);
            oComando.Parameters.Add(_nombreCompleto);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;
            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -11 || oAfectados == -4)
                    throw new Exception("El Usuario ya existe");
                else if (oAfectados == -2)
                    throw new Exception("Error al crear el logueo en la BD");
                else if (oAfectados == -3)
                    throw new Exception("Error al crear usuario en la base de datos");
                else if (oAfectados == -4)
                    throw new Exception("Error al dar permisos al usuario");
                else if (oAfectados == -5 || oAfectados == -6)
                    throw new Exception("Error en los permisos de la base de datos");
                else if (oAfectados == -7 || oAfectados == -8)
                    throw new Exception("Error en tabla de Base de datos");
                else if (oAfectados == -9 || oAfectados == -10)
                    throw new Exception("Error al insertar en la tabla de Base de datos");

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }//ok
        public void ModificarMeteorologo(Meteorologo M, Empleado emp)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("ModificarMeteorologo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _usu = new SqlParameter("@Usu", M.UsuLog);
            SqlParameter _mail = new SqlParameter("@Mail", M.Mail);
            SqlParameter _nombreCompleto = new SqlParameter("@NombreC", M.Nombre_Completo);
            SqlParameter _telefono = new SqlParameter("@telefono", M.Telefono);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_usu);
            oComando.Parameters.Add(_mail);
            oComando.Parameters.Add(_nombreCompleto);
            oComando.Parameters.Add(_telefono);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("No existe el Meteorologo");
                if (oAfectados == -2 || oAfectados == -3)
                    throw new Exception("Error al modificar Meteorologo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }//ok
        public void ModificarPassMeteorologo(Meteorologo M, Meteorologo met)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(met));
            SqlCommand oComando = new SqlCommand("ModificarPassMeteorologo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _usu = new SqlParameter("@usu", M.UsuLog);
            SqlParameter _pass = new SqlParameter("@Pass", M.Contrasena);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_usu);
            oComando.Parameters.Add(_pass);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;
            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("No existe el Meteorologo");
                if (oAfectados == -2)
                    throw new Exception("Error al modificar Meteorologo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }//ok
        public void EliminarMeteorologo(Meteorologo M, Empleado emp)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("BajaMeteorologo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _usu = new SqlParameter("@codigoU", M.UsuLog);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_usu);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("El Meteorologo no Existe");
                else if (oAfectados == -2 || oAfectados == -3 || oAfectados == -4 || oAfectados == -5 || oAfectados == -6)
                    throw new Exception("Error al eliminar Meteorologo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }//ok
        public Meteorologo BuscarMeteorologo(string pUsulog, Usuario usu)
        {
            string usuLog;
            string contrasena;
            string nombre_Completo;
            string telefono;
            string mail;
            Meteorologo M = null;
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(usu));
            SqlCommand oComando = new SqlCommand("BuscarMeteorologoActivo", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@Usu_Log", pUsulog);

            SqlDataReader oReader;

            try
            {
                oConexion.Open();
                oReader = oComando.ExecuteReader();

                if (oReader.Read())
                {
                    usuLog = (string)oReader["UsuLog"];
                    contrasena = (string)oReader["Contrasena"];
                    nombre_Completo = (string)oReader["Nombre_Completo"];
                    telefono = (string)oReader["Telefono"];
                    mail = (string)oReader["Mail"];
                    M = new Meteorologo(usuLog, contrasena, nombre_Completo, mail, telefono);
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
            return M;
        }//ok
        internal Meteorologo BuscarTodosMeteorologo(string pUsulog)
        {
           
           
            SqlConnection _cnn = new SqlConnection(Conexion.Cnn());
            Meteorologo _unMeteorologo = null;


            SqlCommand _comando = new SqlCommand("BuscarTodosLosMeteorologos", _cnn);
            _comando.CommandType = System.Data.CommandType.StoredProcedure;
            _comando.Parameters.AddWithValue("@Usu_log", pUsulog);

            try
            {
                _cnn.Open();
                SqlDataReader _lector = _comando.ExecuteReader();
                if (_lector.HasRows)
                {
                    _lector.Read();
                    _unMeteorologo = new Meteorologo(pUsulog, (string)_lector["Contrasena"], (string)_lector["Nombre_Completo"], (string)_lector["Mail"], (string)_lector["Telefono"]);
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
            return _unMeteorologo;
        }//ok      
        public List<Meteorologo> ListarMeteorologoSinPronxAno(int pAnio, Empleado emp)
        {
            List<Meteorologo> _Lista = new List<Meteorologo>();
            SqlConnection _Conexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand _Comando = new SqlCommand("ListarMeteorologoSinPronxAno", _Conexion);
            _Comando.CommandType = CommandType.StoredProcedure;

            _Comando.Parameters.AddWithValue("@anio", pAnio);

            SqlDataReader _Reader;

            try
            {
                _Conexion.Open();
                _Reader = _Comando.ExecuteReader();

                while (_Reader.Read())
                {
                    string usuLog = (string)_Reader["UsuLog"];
                    string contrasena = (string)_Reader["Contrasena"];
                    string nombre_Completo = (string)_Reader["Nombre_Completo"];
                    string telefono = (string)_Reader["Telefono"];
                    string mail = (string)_Reader["Mail"];
                    Meteorologo m = new Meteorologo(usuLog, contrasena, nombre_Completo, mail, telefono);
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
