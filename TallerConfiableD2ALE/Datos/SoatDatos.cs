using System.Data;
using System.Data.SqlClient;
using TallerConfiableD2ALE.Models;
namespace TallerConfiableD2ALE.Datos
{
    public class SoatDatos
    {
        public List<SoatModel> Listar()
        {
            var oLista = new List<SoatModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarSoat", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new SoatModel()
                        {
                            idSOAT = Convert.ToInt32(dr["idSOAT"]),
                            vehiculoFK = dr["vehiculoFK"].ToString(),
                            fechaCompra = Convert.ToDateTime(dr["fechaCompra"]),
                            fechaVencimiento = Convert.ToDateTime(dr["fechaVencimiento"])
                        });
                    }
                }
            }
            return oLista;
        }

        public SoatModel Obtener(int idSOAT)
        {
            var oSoat = new SoatModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {

                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerSoat", conexion);//Definimos que procedimiento almacenado se utilizara para obtener un usuario
                cmd.Parameters.AddWithValue("idSOAT", idSOAT);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oSoat.idSOAT = Convert.ToInt32(dr["idSOAT"]);
                        oSoat.vehiculoFK = dr["vehiculoFK"].ToString();
                        oSoat.fechaCompra = Convert.ToDateTime(dr["fechaCompra"]);
                        oSoat.fechaVencimiento = Convert.ToDateTime(dr["fechaVencimiento"]);
                    }
                }
            }
            return oSoat;
        }

        public bool Guardar(SoatModel osoat)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarSoat", conexion);
                    cmd.Parameters.AddWithValue("vehiculoFK", osoat.vehiculoFK);
                    cmd.Parameters.AddWithValue("fechaCompra", osoat.fechaCompra);
                    cmd.Parameters.AddWithValue("fechaVencimiento", osoat.fechaVencimiento);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

        public bool Editar(SoatModel osoat)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarSoat", conexion);
                    cmd.Parameters.AddWithValue("idSOAT", osoat.idSOAT);
                    cmd.Parameters.AddWithValue("fechaCompra", osoat.fechaCompra);
                    cmd.Parameters.AddWithValue("fechaVencimiento", osoat.fechaVencimiento);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }

        public bool Eliminar(int idSOAT)
        {
            bool rpta;

            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarSoat", conexion);
                    cmd.Parameters.AddWithValue("idSOAT", idSOAT);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                rpta = false;
            }
            return rpta;
        }
    }
}
