using TallerConfiableD2ALE.Models;
using System.Data.SqlClient;
using System.Data;
namespace TallerConfiableD2ALE.Datos
{
    public class RepuestoDatos
    {
       //Metodo para Listar
       public List<RepuestoModel> Listar()
       {
            var oLista = new List<RepuestoModel>();

            var conex = new Conexion();

            using ( var conexion = new SqlConnection(conex.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarRepuesto", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new RepuestoModel()
                        {
                            IdRepuesto = Convert.ToInt32(dr["idRepuesto"]),
                            NombreRepuesto = dr["nombreRepuesto"].ToString(),
                            Precio = Convert.ToInt32(dr["precio"])
                        });
                    }
                }               
                return oLista;
            }
            
       }
   
        //Metodo para Obtener
        public RepuestoModel Obtener(int IdRepuesto)
        {
            var oRepuesto = new RepuestoModel();

            var conex = new Conexion();

            using (var conexion = new SqlConnection(conex.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerRepuesto", conexion);
                cmd.Parameters.AddWithValue("idRepuesto", IdRepuesto);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oRepuesto.IdRepuesto = Convert.ToInt32(dr["idRepuesto"]);
                        oRepuesto.NombreRepuesto = dr["nombreRepuesto"].ToString();
                        oRepuesto.Precio = Convert.ToInt32(dr["Precio"]);
                    }
                }
            }
            return oRepuesto;
        }
        //Metodo para Guardar
        public bool Guardar (RepuestoModel orepuesto)
        {
            bool resp;
            try
            {
                var conex = new Conexion();

                using (var conexion = new SqlConnection(conex.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarRepuesto", conexion);                    
                    cmd.Parameters.AddWithValue("nombreRepuesto", orepuesto.NombreRepuesto);
                    cmd.Parameters.AddWithValue("precio", orepuesto.Precio);
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
        public bool Editar (RepuestoModel oRepuesto)
        {
            bool resp;
            try
            {
                var conex = new Conexion();

                using (var conexion = new SqlConnection(conex.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarRepuesto", conexion);
                    cmd.Parameters.AddWithValue("idRepuesto", oRepuesto.IdRepuesto);
                    cmd.Parameters.AddWithValue("nombreRepuesto", oRepuesto.NombreRepuesto);
                    cmd.Parameters.AddWithValue("precio", oRepuesto.Precio);
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
        public bool Eliminar (int IdRepuesto)
        {
            bool resp;
            try
            {
                var conex = new Conexion();

                using (var conexion = new SqlConnection(conex.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarRepuesto", conexion);
                    cmd.Parameters.AddWithValue("idRepuesto", IdRepuesto);
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
