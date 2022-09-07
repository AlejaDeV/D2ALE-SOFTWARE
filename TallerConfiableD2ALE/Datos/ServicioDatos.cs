using TallerConfiableD2ALE.Models;
using System.Data.SqlClient;
using System.Data;
namespace TallerConfiableD2ALE.Datos
{
    public class ServicioDatos
    {
        //Metodo para Listar
        public List<ServicioModel> Listar()
        {
            var oLista = new List<ServicioModel>();

            var conex = new Conexion();

            using (var conexion = new SqlConnection(conex.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarServicio", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new ServicioModel()
                        {
                            IdServicio = Convert.ToInt32(dr["idServicio"]),
                            TipoServicio = dr["tipoServicio"].ToString(),
                            Descripcion = dr["descripcion"].ToString(),
                            NivAceite = dr["nivAceite"].ToString(),
                            NivLiquidoFrenos = dr["nivLiquidoFrenos"].ToString(),
                            NivRefrigerante = dr["nivRefrigerante"].ToString(),
                            NivLiquidoDireccion = dr["nivLiquidoDireccion"].ToString(),
                            FechaCompra = Convert.ToDateTime(dr["fechaCompra"]),
                            MaestroFK = Convert.ToInt32(dr["maestroFK"]),
                            VehiculoFK = dr["vehiculoFK"].ToString(),
                            UsuarioFK  = Convert.ToInt32(dr["usuarioFK"])
                        });
                    }
                }
                return oLista;
            }
        }
        //Metodo para Obtener
        public ServicioModel Obtener(int IdServicio)
        {
            var oServicio = new ServicioModel();

            var conex = new Conexion();

            using (var conexion = new SqlConnection(conex.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerServicio", conexion);
                cmd.Parameters.AddWithValue("idServicio", IdServicio);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oServicio.IdServicio = Convert.ToInt32(dr["idServicio"]);
                        oServicio.TipoServicio = dr["tipoServicio"].ToString();
                        oServicio.Descripcion = dr["descripcion"].ToString();
                        oServicio.NivAceite = dr["nivAceite"].ToString();
                        oServicio.NivLiquidoFrenos = dr["nivLiquidoFrenos"].ToString();
                        oServicio.NivRefrigerante = dr["nivRefrigerante"].ToString();
                        oServicio.NivLiquidoDireccion = dr["nivLiquidoDireccion"].ToString();
                        oServicio.FechaCompra = Convert.ToDateTime(dr["fechaCompra"]);
                        oServicio.MaestroFK = Convert.ToInt32(dr["maestroFK"]);
                        oServicio.VehiculoFK = dr["vehiculoFK"].ToString();
                        oServicio.UsuarioFK = Convert.ToInt32(dr["usuarioFK"]);
                    }
                }                
            }
            return oServicio;
        }
        //Metodo para Guardar
        public bool Guardar (ServicioModel oservicio)
        {
            bool resp;
            try
            {
                var conex = new Conexion();

                using (var conexion = new SqlConnection(conex.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarServicio", conexion);
                    cmd.Parameters.AddWithValue("tipoServicio", oservicio.TipoServicio);
                    cmd.Parameters.AddWithValue("descripcion", oservicio.Descripcion);
                    cmd.Parameters.AddWithValue("nivAceite", oservicio.NivAceite);
                    cmd.Parameters.AddWithValue("nivLiquidoFrenos", oservicio.NivLiquidoFrenos);
                    cmd.Parameters.AddWithValue("nivRefrigerante", oservicio.NivRefrigerante);
                    cmd.Parameters.AddWithValue("nivLiquidoDireccion", oservicio.NivLiquidoDireccion);
                    cmd.Parameters.AddWithValue("fechaCompra", oservicio.FechaCompra);
                    cmd.Parameters.AddWithValue("maestroFK", oservicio.MaestroFK);
                    cmd.Parameters.AddWithValue("vehiculoFK", oservicio.VehiculoFK);
                    cmd.Parameters.AddWithValue("usuarioFK", oservicio.UsuarioFK);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                resp = true;
            }
            catch(Exception e)
            {
                string error = e.Message;
                resp = false;
            }
            return resp;
        }
        //Metodo para Editar
        public bool Editar(ServicioModel oservicio)
        {
            bool resp;
            try
            {
                var conex = new Conexion();

                using (var conexion = new SqlConnection(conex.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarServicio", conexion);
                    cmd.Parameters.AddWithValue("idServicio", oservicio.IdServicio);
                    cmd.Parameters.AddWithValue("tipoServicio", oservicio.TipoServicio);
                    cmd.Parameters.AddWithValue("descripcion", oservicio.Descripcion);
                    cmd.Parameters.AddWithValue("nivAceite", oservicio.NivAceite);
                    cmd.Parameters.AddWithValue("nivLiquidoFrenos", oservicio.NivLiquidoFrenos);
                    cmd.Parameters.AddWithValue("nivRefrigerante", oservicio.NivRefrigerante);
                    cmd.Parameters.AddWithValue("nivLiquidoDireccion", oservicio.NivLiquidoDireccion);
                    cmd.Parameters.AddWithValue("fechaCompra", oservicio.FechaCompra);
                    cmd.Parameters.AddWithValue("maestroFK", oservicio.MaestroFK);
                    cmd.Parameters.AddWithValue("vehiculoFK", oservicio.VehiculoFK);
                    cmd.Parameters.AddWithValue("usuarioFK", oservicio.UsuarioFK);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                resp = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                resp = false;
            }
            return resp;
        }
        //Metodo para Eliminar
        public bool Eliminar(int IdServicio)
        {
            bool resp;
            try
            {
                var conex = new Conexion();

                using (var conexion = new SqlConnection(conex.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarServicio", conexion);
                    cmd.Parameters.AddWithValue("IdServicio", IdServicio);                    
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                resp = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                resp = false;
            }
            return resp;
        }
    }
}
