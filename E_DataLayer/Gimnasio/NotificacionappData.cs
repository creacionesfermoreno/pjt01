using E_DataModel;
using E_DataModel.Common;
using E_DataModel.Gimnasio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace E_DataLayer
{
    public class NotificacionappData
    {
        public List<NotificacionDTO> Listado (NotificacionDTO oFiltro)
        {
            List<NotificacionDTO> lista = new List<NotificacionDTO>();
            
                using (var conn = new SqlConnection(Helper.Conexion()))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("uspListarNotificacionesApp", conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoUnidadNegocio;
                        cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oFiltro.CodigoSede;
                        using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                        {
                            if (oIDataReader.HasRows)
                            {
                                while (oIDataReader.Read())
                                {
                                    lista.Add(new NotificacionDTO()
                                    {
                                        CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                        CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                        CodigoNotificacionesApp = oIDataReader[oIDataReader.GetOrdinal("CodigoNotificacionesApp")].ToString(),
                                        TipoEnvio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoEnvio")]),
                                        Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                        FechaHoraEnvio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraEnvio")]),
                                        DesFechaHoraEnvio = oFiltro.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraEnvio")])),
                                        Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                        Recurrente = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Recurrente")]),
                                        Enviado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Enviado")]),
                                        Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                        GrupoPersonas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("GrupoPersonas")]),
                                        UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                        //FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                    });
                                }
                            }

                        }
                    }
                }
            
     
            return lista;
        }

        public NotificacionDTO BuscarPorCodigoNotificacionApp(NotificacionDTO oNotificacionDTO)
        {
            NotificacionDTO itemDTO = new NotificacionDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarNotificacionesApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oNotificacionDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oNotificacionDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar)).Value = oNotificacionDTO.CodigoNotificacionesApp;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new NotificacionDTO()
                                {                                
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoNotificacionesApp = oIDataReader[oIDataReader.GetOrdinal("CodigoNotificacionesApp")].ToString(),
                                    TipoEnvio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoEnvio")]),
                                    Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                    FechaHoraEnvio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraEnvio")]),
                                    DesFechaHoraEnvio = oNotificacionDTO.DateParse(Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaHoraEnvio")])),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                    Recurrente = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Recurrente")]),
                                    Enviado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Enviado")]),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    GrupoPersonas = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("GrupoPersonas")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),

                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;
        }

        public void Registrar(NotificacionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarNotificacionesApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.Int)).Value = item.CodigoNotificacionesApp;
                    cmd.Parameters.Add(new SqlParameter("@TipoEnvio", System.Data.SqlDbType.Int)).Value = item.TipoEnvio;
                    cmd.Parameters.Add(new SqlParameter("@Asunto", System.Data.SqlDbType.VarChar)).Value = item.Asunto;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraEnvio", System.Data.SqlDbType.DateTime)).Value = item.@FechaHoraEnvio;
                    cmd.Parameters.Add(new SqlParameter("@Mensaje", System.Data.SqlDbType.VarChar)).Value = item.Mensaje;
                    cmd.Parameters.Add(new SqlParameter("@Recurrente", System.Data.SqlDbType.Bit)).Value = item.Recurrente;
                    cmd.Parameters.Add(new SqlParameter("@GrupoPersonas", System.Data.SqlDbType.Int)).Value = item.GrupoPersonas;
                    if (!string.IsNullOrEmpty(item.UsuarioCreacion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    }
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(NotificacionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarNotificacionesApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar)).Value = item.CodigoNotificacionesApp;
                    cmd.Parameters.Add(new SqlParameter("@TipoEnvio", System.Data.SqlDbType.Int)).Value = item.TipoEnvio;
                    cmd.Parameters.Add(new SqlParameter("@Asunto", System.Data.SqlDbType.VarChar)).Value = item.Asunto;
                    cmd.Parameters.Add(new SqlParameter("@FechaHoraEnvio", System.Data.SqlDbType.DateTime)).Value = item.@FechaHoraEnvio;
                    cmd.Parameters.Add(new SqlParameter("@Mensaje", System.Data.SqlDbType.VarChar)).Value = item.Mensaje;
                    cmd.Parameters.Add(new SqlParameter("@Recurrente", System.Data.SqlDbType.Bit)).Value = item.Recurrente;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@GrupoPersonas", System.Data.SqlDbType.Int)).Value = item.GrupoPersonas;
                    if (!string.IsNullOrEmpty(item.UsuarioEdicion))
                    {
                        cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    }
                    cmd.ExecuteNonQuery();
                }
            }

        }

        public void Eliminar(NotificacionDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarNotificacionesApp", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoNotificacionesApp", System.Data.SqlDbType.VarChar)).Value = item.CodigoNotificacionesApp;

                    cmd.ExecuteNonQuery();
                }
            }
        }


    }
}
