using TallerConfiableD2ALE.Models;
using System.Data.SqlClient;
using System.Data;
namespace TallerConfiableD2ALE.Datos
{
    public class UsuarioDatos
    {
        public List<UsuarioModel> Listar()
        {
            var oLista = new List<UsuarioModel>();

            var cn = new Conexion();//Guardamos toda la información de la cadena de conexión en la variable

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarUsuario",conexion);//Definimos que procedimiento almacenado se utilizara para listar los usuarios
                cmd.CommandType = CommandType.StoredProcedure;//Definimos que vamos a trabajar con un procedimiento almacenado

                using(var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //var temp = Convert.ToDateTime(dr["fechaNacimiento"]);//Agregamos esta línea para pasar del objeto a Datetime

                        oLista.Add(new UsuarioModel() { 
                            idUsuario = Convert.ToInt32(dr["idUsuario"]),
                            nombres = dr["nombres"].ToString(),
                            apellidos = dr["apellidos"].ToString(),
                            identificacion = dr["identificacion"].ToString(),
                            telefono = dr["telefono"].ToString(),
                            correo = dr["correo"].ToString(),
                            contrasena = dr["contrasena"].ToString(),
                            fechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"]),//Aquí guardamos en fechaNacimiento (en datetime porque no se puede pasar a dateonly <pero le damos formato en la vista>)
                            rol = dr["rol"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        public UsuarioModel Obtener(int idUsuario)
        {
            var oUsuario = new UsuarioModel();

            var cn = new Conexion();//Guardamos toda la información de la cadena de conexión en la variable

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerUsuario", conexion);//Definimos que procedimiento almacenado se utilizara para obtener un usuario
                cmd.Parameters.AddWithValue("idUsuario", idUsuario);
                cmd.CommandType = CommandType.StoredProcedure;//Definimos que vamso a trabajar con un procedimiento almacenado

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oUsuario.idUsuario = Convert.ToInt32(dr["idUsuario"]);
                        oUsuario.nombres = dr["nombres"].ToString();
                        oUsuario.apellidos = dr["apellidos"].ToString();
                        oUsuario.identificacion = dr["identificacion"].ToString();
                        oUsuario.telefono = dr["telefono"].ToString();
                        oUsuario.correo = dr["correo"].ToString();
                        oUsuario.contrasena = dr["contrasena"].ToString();
                        oUsuario.fechaNacimiento = Convert.ToDateTime(dr["fechaNacimiento"]);
                        oUsuario.rolFK = Convert.ToInt32(dr["rolFK"]);
                    }
                }
            }
            return oUsuario;
        }

        public bool Guardar(UsuarioModel oUsuario)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();//Guardamos toda la información de la cadena de conexión en la variable

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarUsuario", conexion);//Definimos que procedimiento almacenado se utilizara
                    cmd.Parameters.AddWithValue("nombres", oUsuario.nombres);
                    cmd.Parameters.AddWithValue("apellidos", oUsuario.apellidos);
                    cmd.Parameters.AddWithValue("identificacion", oUsuario.identificacion);
                    cmd.Parameters.AddWithValue("telefono", oUsuario.telefono);
                    cmd.Parameters.AddWithValue("correo", oUsuario.correo);
                    cmd.Parameters.AddWithValue("contrasena", oUsuario.contrasena);
                    cmd.Parameters.AddWithValue("fechaNacimiento", oUsuario.fechaNacimiento);
                    cmd.Parameters.AddWithValue("rolFK", oUsuario.rolFK);
                    cmd.CommandType = CommandType.StoredProcedure;//Definimos que vamso a trabajar con un procedimiento almacenado
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta=false;
            }

            return respuesta;
        }        
        public bool Editar(UsuarioModel oUsuario)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();//Guardamos toda la información de la cadena de conexión en la variable

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarUsuario", conexion);//Definimos que procedimiento almacenado se utilizara
                    cmd.Parameters.AddWithValue("idUsuario", oUsuario.idUsuario);
                    cmd.Parameters.AddWithValue("nombres", oUsuario.nombres);
                    cmd.Parameters.AddWithValue("apellidos", oUsuario.apellidos);
                    cmd.Parameters.AddWithValue("identificacion", oUsuario.identificacion);
                    cmd.Parameters.AddWithValue("telefono", oUsuario.telefono);
                    cmd.Parameters.AddWithValue("correo", oUsuario.correo);
                    cmd.Parameters.AddWithValue("contrasena", oUsuario.contrasena);
                    cmd.Parameters.AddWithValue("fechaNacimiento", oUsuario.fechaNacimiento);
                    cmd.Parameters.AddWithValue("rolFK", oUsuario.rolFK);
                    cmd.CommandType = CommandType.StoredProcedure;//Definimos que vamso a trabajar con un procedimiento almacenado
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta=false;
            }

            return respuesta;
        }
        public bool Eliminar(int idUsuario)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();//Guardamos toda la información de la cadena de conexión en la variable

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarUsuario", conexion);//Definimos que procedimiento almacenado se utilizara
                    cmd.Parameters.AddWithValue("idUsuario", idUsuario);
                    cmd.CommandType = CommandType.StoredProcedure;//Definimos que vamso a trabajar con un procedimiento almacenado
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta=false;
            }

            return respuesta;
        }
    }
}
