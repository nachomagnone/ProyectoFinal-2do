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
    internal class PerEmpleado:IPerEmpleado
    {
        #region singleton
        private static PerEmpleado _instancia = null;

        private PerEmpleado() { }

        public static PerEmpleado GetInstancia()
        {
            if (_instancia == null)
                _instancia = new PerEmpleado();

            return _instancia;
        }
        #endregion
        public Empleado Logueo(string pUsu, string pass)
        {
            string usuLog;
            string contrasena;
            string nombre_Completo;
            int horas_semanales;
            Empleado E = null;

            SqlConnection oConexion= new SqlConnection(Conexion.Cnn());
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
                    contrasena = (string)oReader["contrasena"];
                    nombre_Completo = (string)oReader["Nombre_Completo"];
                    horas_semanales = (int)oReader["Horas_Semanales"];
                    E = new Empleado(usuLog, contrasena, nombre_Completo, horas_semanales);
                }

                oReader.Close();
            }
            catch (Exception ex)
            {
                E = null;
                return E;
            }
            finally
            {
                oConexion.Close();
            }
            return E;
        }
        public void AgregarEmpleado(Empleado E, Empleado emp)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("AltaEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _usu = new SqlParameter("@Usu_Log", E.UsuLog);
            SqlParameter _horas = new SqlParameter("@Horas_Semanales", E.Horas_semanales);
            SqlParameter _pass = new SqlParameter("@pass", E.Contrasena);
            SqlParameter _nombreCompleto = new SqlParameter("@nombre_comp", E.Nombre_Completo);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_usu);
            oComando.Parameters.Add(_horas);
            oComando.Parameters.Add(_pass);
            oComando.Parameters.Add(_nombreCompleto);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;
            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -2)
                    throw new Exception("El Usuario ya existe");
                else if (oAfectados == -4)
                    throw new Exception("Error al crear el logueo en la BD");
                else if (oAfectados == -5)
                    throw new Exception("Error al crear usuario en la base de datos");
                else if (oAfectados == -6)
                    throw new Exception("Error al dar permisos al usuario");
                else if (oAfectados == -7 || oAfectados == -10 || oAfectados ==-11)
                    throw new Exception("Error en los permisos de la base de datos");
                else if (oAfectados == -8)
                    throw new Exception("Error en tabla de Base de datos");
                else if (oAfectados == -9)
                    throw new Exception("Error en tabla de Base de datos");

            }
            catch (Exception ex)
            {
                throw new Exception ( ex.Message);
            }
            finally
            {
                oConexion.Close();
            }
        }
        public void ModificarEmpleado(Empleado E, Empleado emp)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("ModificarEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _usu = new SqlParameter("@Usu", E.UsuLog);
            SqlParameter _pass = new SqlParameter("@contrasena", E.Contrasena);
            SqlParameter _nombreCompleto = new SqlParameter("@NombreC", E.Nombre_Completo);
            SqlParameter _horas = new SqlParameter("@Horas", E.Horas_semanales);
            SqlParameter _UsuLog = new SqlParameter("@UsuLog", emp.UsuLog);

            SqlParameter _Retorno = new SqlParameter("@Retorno", SqlDbType.Int);
            _Retorno.Direction = ParameterDirection.ReturnValue;

            oComando.Parameters.Add(_usu);
            oComando.Parameters.Add(_pass);
            oComando.Parameters.Add(_nombreCompleto);
            oComando.Parameters.Add(_horas);
            oComando.Parameters.Add(_UsuLog);
            oComando.Parameters.Add(_Retorno);

            int oAfectados = -1;

            try
            {
                oConexion.Open();
                oComando.ExecuteNonQuery();
                oAfectados = (int)oComando.Parameters["@Retorno"].Value;
                if (oAfectados == -1)
                    throw new Exception("No existe el Empleado");
                if (oAfectados == -2|| oAfectados==-3)
                    throw new Exception("Error al modificar Empleado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }
        public void EliminarEmpleado(Empleado E, Empleado emp)
        {
            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("BajaEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            SqlParameter _usu = new SqlParameter("@codigoE", E.UsuLog);

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
                    throw new Exception("El Empleado no Existe");
                else if (oAfectados == -2 || oAfectados == -3 || oAfectados == -4 || oAfectados == -5)
                    throw new Exception("Error al eliminar Empleado");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                oConexion.Close();
            }
        }
        public Empleado BuscarEmpleado(string pUsulog, Usuario emp)
        {
            string usuLog;
            string contrasena;
            string nombre_Completo;
            int horas_semanales;
            Empleado E = null;

            SqlConnection oConexion = new SqlConnection(Conexion.Cnn(emp));
            SqlCommand oComando = new SqlCommand("BuscarEmpleado", oConexion);
            oComando.CommandType = CommandType.StoredProcedure;

            oComando.Parameters.AddWithValue("@Usu_log", pUsulog);

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
                    horas_semanales = (int)oReader["Horas_Semanales"];
                    E = new Empleado(usuLog, contrasena, nombre_Completo, horas_semanales);
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
            return E;
        }
    }
}
