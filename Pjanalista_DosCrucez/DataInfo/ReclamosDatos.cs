using Microsoft.AspNetCore.Mvc;
using Pjanalista_DosCrucez.Models;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;

namespace Pjanalista_DosCrucez.DataInfo
{
    public class ReclamosDatos
    {
/*Funciones para Reclamos*/
        public List<ReclamosModel> listarReclamos()
        {
            var oLista = new List<ReclamosModel>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getSQLString()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_ListarReclamos", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(new ReclamosModel()
                        {
                            IdReclamo = Convert.ToInt32(dr["IdReclamo"]),
                            NombreConsumidor = dr["NombreConsumidor"].ToString(),
                            NombreProveedor = dr["NombreProveedor"].ToString(),
                            EstadoReclamo = dr["Estado"].ToString(),
                        });
                    }
                }
            }
            return oLista;
        }
        public ReclamosModel buscarReclamo(int idReclamo)
        {
            var bReclamo = new ReclamosModel();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getSQLString()))
            {
                conexion.Open();

                SqlCommand cmd = new SqlCommand("sp_BuscarReclamo", conexion);
                cmd.Parameters.AddWithValue("IdReclamo", idReclamo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        bReclamo.IdReclamo = Convert.ToInt32(dr["IdReclamo"]);
                        bReclamo.NombreProveedor = dr["NombreProveedor"].ToString();
                        bReclamo.DireccionProveedor = dr["DireccionProveedor"].ToString();
                        bReclamo.TelefonoProveedor = dr["TelefonoProveedor"].ToString();
                        bReclamo.DetalleReclamo = dr["DetalleReclamo"].ToString();
                        bReclamo.MontoReclamo = Convert.ToDouble(dr["MontoReclamo"]);
                        bReclamo.FechaIngreso = Convert.ToDateTime(dr["FechaIngreso"]);
                        bReclamo.IdEmpleado = Convert.ToInt32(dr["IdEmpleado"]);
                        bReclamo.IdConsumidor = Convert.ToInt32(dr["IdConsumidor"]);
                        bReclamo.NombreConsumidor = dr["NombreConsumidor"].ToString();
                        bReclamo.EstadoReclamo = dr["Estado"].ToString();
                    }
                }
            }
            return bReclamo;
        }

        public bool GuardarReclamo(ReclamosModel oReclamo)
        {
            bool o = false;
            int idConsumidor;

            try
            {
                var cn = new Conexion();
                DateTime dt = DateTime.Now;

                if (!VerificarConsumidor(oReclamo.DuiConsumidor))
                {
                    idConsumidor = Convert.ToInt32(RegistrarConsumidor(oReclamo));
                    using (var conexion = new SqlConnection(cn.getSQLString()))
                    {
                        conexion.Open();
                        SqlCommand cmd = new SqlCommand("sp_GuardarReclamo", conexion);
                        cmd.Parameters.AddWithValue("NombreProveedor", oReclamo.NombreProveedor);
                        cmd.Parameters.AddWithValue("DireccionProveedor", oReclamo.DireccionProveedor);
                        cmd.Parameters.AddWithValue("TelefonoProveedor", oReclamo.TelefonoProveedor);
                        cmd.Parameters.AddWithValue("DetalleReclamo", oReclamo.DetalleReclamo);
                        cmd.Parameters.AddWithValue("MontoReclamo", oReclamo.MontoReclamo);
                        cmd.Parameters.AddWithValue("FechaIngreso", dt);
                        cmd.Parameters.AddWithValue("IdConsumidor", idConsumidor);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        conexion.Close();
                    }
                    o = true;
                    
                }
                else
                {
                    idConsumidor = buscarConsumidor(oReclamo.DuiConsumidor);
                    using (var conexion = new SqlConnection(cn.getSQLString()))
                    {

                        conexion.Open();
                        SqlCommand cmd = new SqlCommand("sp_GuardarReclamo", conexion);
                        cmd.Parameters.AddWithValue("NombreProveedor", oReclamo.NombreProveedor);
                        cmd.Parameters.AddWithValue("DireccionProveedor", oReclamo.DireccionProveedor);
                        cmd.Parameters.AddWithValue("TelefonoProveedor", oReclamo.TelefonoProveedor);
                        cmd.Parameters.AddWithValue("DetalleReclamo", oReclamo.DetalleReclamo);
                        cmd.Parameters.AddWithValue("MontoReclamo", oReclamo.MontoReclamo);
                        cmd.Parameters.AddWithValue("FechaIngreso", dt);
                        cmd.Parameters.AddWithValue("IdConsumidor", idConsumidor);
                        cmd.Parameters.Add("Guardado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        conexion.Close();
                    }
                    o = true;

                }
            }
            catch (Exception e){
                string error = e.Message;
                Debug.WriteLine(error);
                o = false;
            }
            return o;
        }
        public bool ActualizarReclamo(ReclamosModel oReclamo)
        {
            bool o = false;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getSQLString()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_ActualizarReclamo", conexion);
                    cmd.Parameters.AddWithValue("IdReclamo", oReclamo.IdReclamo);
                    cmd.Parameters.AddWithValue("NombreProveedor", oReclamo.NombreProveedor);
                    cmd.Parameters.AddWithValue("DireccionProveedor", oReclamo.DireccionProveedor);
                    cmd.Parameters.AddWithValue("TelefonoProveedor", oReclamo.TelefonoProveedor);
                    cmd.Parameters.AddWithValue("DetalleReclamo", oReclamo.DetalleReclamo);
                    cmd.Parameters.AddWithValue("MontoReclamo", oReclamo.MontoReclamo);
                    cmd.Parameters.AddWithValue("FechaRevision", oReclamo.FechaRevision);
                    cmd.Parameters.AddWithValue("IdEstado", oReclamo.IdEstado);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                o = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                o = false;
            }
            return o;
        }
        public bool EliminarReclamo(int idReclamo)
        {
            bool o = false;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getSQLString()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarReclamo", conexion);
                    cmd.Parameters.AddWithValue("IdReclamo", idReclamo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                o = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                o = false;
            }
            return o;
        }
        /*Funciones Para Consumidor*/

        public bool VerificarConsumidor(string consumidorDui)
        {
            bool exists = false;
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getSQLString()))
            {
                conexion.Open();

                SqlCommand verificarConsumidor = new SqlCommand("sp_VerificarConsumidor", conexion);
                verificarConsumidor.Parameters.AddWithValue("DuiConsumidor", consumidorDui);
                verificarConsumidor.Parameters.Add("ConsumidorExists", SqlDbType.Bit).Direction = ParameterDirection.Output;
                verificarConsumidor.CommandType = CommandType.StoredProcedure;
                verificarConsumidor.ExecuteNonQuery();

                exists = Convert.ToBoolean(verificarConsumidor.Parameters["ConsumidorExists"].Value);
            }

            return exists;
        }
        public int RegistrarConsumidor(ReclamosModel oConsumidor)
        {
            int idConsumidor = 0;
            try
            {
                var cn = new Conexion();
                using (var conexion = new SqlConnection(cn.getSQLString()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_RegistrarConsumidor", conexion);
                    cmd.Parameters.AddWithValue("NombreConsumidor", oConsumidor.NombreConsumidor);
                    cmd.Parameters.AddWithValue("ApellidoConsumidor", oConsumidor.ApellidoConsumidor);
                    cmd.Parameters.AddWithValue("CorreoElectronicoConsumidor", oConsumidor.EmailConsumidor);
                    cmd.Parameters.AddWithValue("DuiConsumidor", oConsumidor.DuiConsumidor);
                    cmd.Parameters.AddWithValue("DireccionConsumidor", oConsumidor.DireccionConsumidor);
                    cmd.Parameters.Add("IdConsumidor", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                    idConsumidor = Convert.ToInt32(cmd.Parameters["IdConsumidor"].Value);

                }
            }
            catch (Exception e)
            {
                string error = e.Message;
            }

            return idConsumidor;
        }
        public int buscarConsumidor (string duiConsumidor)
        {
            var oConsumidor = new ConsumidorModel();
            var cn = new Conexion();
            int idConsumidor;

            using (var conexion = new SqlConnection(cn.getSQLString()))
            {
                conexion.Open();

                SqlCommand verificarConsumidor = new SqlCommand("sp_VerificarConsumidor", conexion);
                verificarConsumidor.Parameters.AddWithValue("DuiConsumidor", duiConsumidor);
                verificarConsumidor.Parameters.Add("ConsumidorExists", SqlDbType.Bit).Direction = ParameterDirection.Output;
                verificarConsumidor.CommandType = CommandType.StoredProcedure;
                verificarConsumidor.ExecuteNonQuery();

                idConsumidor = Convert.ToInt32(verificarConsumidor.Parameters["ConsumidorExists"].Value);
            }


            return idConsumidor;
        }

        public bool verificarUsuario(string nombreUsuario, string passwordUsuario)
        {
            bool exists = false;;
            
            var cn = new Conexion();
            int idConsumidor;

            using (var conexion = new SqlConnection(cn.getSQLString()))
            {
                conexion.Open();

                SqlCommand verificarUsuario = new SqlCommand("sp_VerificarUsuario", conexion);
                verificarUsuario.Parameters.AddWithValue("NombreUsuario", nombreUsuario);
                verificarUsuario.Parameters.AddWithValue("PasswordUsuario", passwordUsuario);
                verificarUsuario.Parameters.Add("ExistsUsuario", SqlDbType.Bit).Direction = ParameterDirection.Output;
                verificarUsuario.CommandType = CommandType.StoredProcedure;
                verificarUsuario.ExecuteNonQuery();

                exists = Convert.ToBoolean(verificarUsuario.Parameters["ExistsUsuario"].Value);
            }

            return exists;

        }

    }
}


