using TallerConfiableD2ALE.Models;
using System.Data.SqlClient;
using System.Data;
using System.Numerics;
using System.Reflection;

namespace TallerConfiableD2ALE.Datos
{
    public class VehiculoDatos
    {
        public List<VehiculoModel> Listar()
        {
            var oLista = new List<VehiculoModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd =  new SqlCommand("sp_ListarVehiculo",conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new VehiculoModel(){
                            placa = dr["placa"].ToString(),
                            marca = dr["marca"].ToString(),
                            modelo = dr["modelo"].ToString(),
                            tipoVehiculo = dr["tipoVehiculo"].ToString(),
                            cilindraje = dr["cilindraje"].ToString(),
                            ciudadRegistro = dr["ciudadRegistro"].ToString(),
                            nombre = dr["nombre"].ToString(),
                            apellido = dr["apellido"].ToString()
                        });
                    }
                }
            }
            return oLista;
        }

        public VehiculoModel Obtener(string placa)
        {
            var oVehiculo = new VehiculoModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerVehiculo", conexion);
                cmd.Parameters.AddWithValue("placa",placa);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oVehiculo.placa = dr["placa"].ToString();
                        oVehiculo.marca = dr["marca"].ToString();
                        oVehiculo.modelo = dr["modelo"].ToString();
                        oVehiculo.tipoVehiculo = dr["tipoVehiculo"].ToString();
                        oVehiculo.cilindraje = dr["cilindraje"].ToString();
                        oVehiculo.ciudadRegistro = dr["ciudadRegistro"].ToString();
                        oVehiculo.identificacion = dr["identificacion"].ToString();
                    }
                }
            }
            return oVehiculo;
        }

        public bool Guardar(VehiculoModel oVehiculo)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarVehiculo", conexion);
                    cmd.Parameters.AddWithValue("placa",oVehiculo.placa);
                    cmd.Parameters.AddWithValue("marca",oVehiculo.marca);
                    cmd.Parameters.AddWithValue("modelo",oVehiculo.modelo);
                    cmd.Parameters.AddWithValue("tipoVehiculo", oVehiculo.tipoVehiculo);
                    cmd.Parameters.AddWithValue("cilindraje", oVehiculo.cilindraje);
                    cmd.Parameters.AddWithValue("ciudadRegistro", oVehiculo.ciudadRegistro);
                    cmd.Parameters.AddWithValue("identificacion", oVehiculo.identificacion);
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public bool Editar(VehiculoModel oVehiculo)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarVehiculo", conexion);
                    cmd.Parameters.AddWithValue("placa", oVehiculo.placa);
                    cmd.Parameters.AddWithValue("marca", oVehiculo.marca);
                    cmd.Parameters.AddWithValue("modelo", oVehiculo.modelo);
                    cmd.Parameters.AddWithValue("tipoVehiculo", oVehiculo.tipoVehiculo);
                    cmd.Parameters.AddWithValue("cilindraje", oVehiculo.cilindraje);
                    cmd.Parameters.AddWithValue("ciudadRegistro", oVehiculo.ciudadRegistro);
                    cmd.Parameters.AddWithValue("identificacion", oVehiculo.identificacion);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }

        public bool Eliminar(string placa)
        {
            bool respuesta;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarVehiculo", conexion);
                    cmd.Parameters.AddWithValue("placa", placa);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}
