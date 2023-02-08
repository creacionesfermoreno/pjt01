using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
    public class AgendaPostVentaData
    {
        public void Registrar(AgendaPostVentaDTO item)
        {
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarAgendaPostVentaTodos", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@TipoAgenda", System.Data.SqlDbType.Int)).Value = item.TipoAgenda;

                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCalificacion", System.Data.SqlDbType.Int)).Value = item.CodigoCalificacion;
                    cmd.Parameters.Add(new SqlParameter("@Mensaje", System.Data.SqlDbType.VarChar, 100)).Value = item.Mensaje;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoInicioSesion", System.Data.SqlDbType.Int)).Value = item.TK_ID;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AgendaPostVentaDTO> uspListarAgendaPostVentaSeguimiento_Paginacion(AgendaPostVentaDTO oAgendaPostVentaDTO, Paging paging)
        {
            List<AgendaPostVentaDTO> lista = new List<AgendaPostVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaPostVentaSeguimiento_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCalificacion", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoCalificacion;
                    cmd.Parameters.Add(new SqlParameter("@TipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.TipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oAgendaPostVentaDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAgendaPostVentaDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar)).Value = oAgendaPostVentaDTO.Nombre;
                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaPostVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    TipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]),
                                    DesTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("DesTipoAgenda")].ToString(),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoCalificacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCalificacion")]),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    ColorCalificacion = oIDataReader[oIDataReader.GetOrdinal("ColorCalificacion")].ToString(),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                    Nombre = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Telefono = oIDataReader[oIDataReader.GetOrdinal("Telefono")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public AgendaPostVentaDTO uspListarAgendaPostVentaSeguimiento_NumeroRegistros(AgendaPostVentaDTO oAgendaPostVentaDTO)
        {
            AgendaPostVentaDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaPostVentaSeguimiento_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoCalificacion", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoCalificacion;
                    cmd.Parameters.Add(new SqlParameter("@TipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.TipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oAgendaPostVentaDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAgendaPostVentaDTO.FechaFinal;
                    cmd.Parameters.Add(new SqlParameter("@Nombre", System.Data.SqlDbType.VarChar,100)).Value = oAgendaPostVentaDTO.Nombre;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new AgendaPostVentaDTO()
                                {
                                    CantidadTotalFilas = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }

        public List<AgendaPostVentaDTO> uspListarCalificacionPostVenta()
        {
            List<AgendaPostVentaDTO> lista = new List<AgendaPostVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarCalificacionPostVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaPostVentaDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    ColorCalificacion = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public List<AgendaPostVentaDTO> uspListarTipoAgendaPostVenta()
        {
            List<AgendaPostVentaDTO> lista = new List<AgendaPostVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTipoAgendaPostVenta", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaPostVentaDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    ColorCalificacion = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;
        }

        public List<AgendaPostVentaDTO> uspListarAgendaPostVentaSeguimientoSocios_Mensajes(AgendaPostVentaDTO oAgendaPostVentaDTO)
        {
            List<AgendaPostVentaDTO> lista = new List<AgendaPostVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaPostVentaSeguimientoSocios_Mensajes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.TipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSocio;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaPostVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    TipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]),
                                    DesTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("DesTipoAgenda")].ToString(),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoCalificacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCalificacion")]),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    ColorCalificacion = oIDataReader[oIDataReader.GetOrdinal("ColorCalificacion")].ToString(),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public List<AgendaPostVentaDTO> uspListarAgendaPostVentaSeguimientoReferido_Mensajes(AgendaPostVentaDTO oAgendaPostVentaDTO)
        {
            List<AgendaPostVentaDTO> lista = new List<AgendaPostVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaPostVentaSeguimientoReferido_Mensajes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.TipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaPostVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    TipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]),
                                    DesTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("DesTipoAgenda")].ToString(),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoCalificacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCalificacion")]),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    ColorCalificacion = oIDataReader[oIDataReader.GetOrdinal("ColorCalificacion")].ToString(),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public List<AgendaPostVentaDTO> uspListarAgendaPostVentaSeguimientoProspectos_Mensajes(AgendaPostVentaDTO oAgendaPostVentaDTO)
        {
            List<AgendaPostVentaDTO> lista = new List<AgendaPostVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaPostVentaSeguimientoProspectos_Mensajes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.TipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaPostVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    TipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]),
                                    DesTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("DesTipoAgenda")].ToString(),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoCalificacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCalificacion")]),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    ColorCalificacion = oIDataReader[oIDataReader.GetOrdinal("ColorCalificacion")].ToString(),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public List<AgendaPostVentaDTO> uspListarAgendaPostVentaSeguimientoLlamadaEntrante_Mensajes(AgendaPostVentaDTO oAgendaPostVentaDTO)
        {
            List<AgendaPostVentaDTO> lista = new List<AgendaPostVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaPostVentaSeguimientoLlamadaEntrante_Mensajes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.TipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaPostVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    TipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]),
                                    DesTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("DesTipoAgenda")].ToString(),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoCalificacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCalificacion")]),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    ColorCalificacion = oIDataReader[oIDataReader.GetOrdinal("ColorCalificacion")].ToString(),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public List<AgendaPostVentaDTO> uspListarAgendaPostVentaSeguimientoInvitado_Mensajes(AgendaPostVentaDTO oAgendaPostVentaDTO)
        {
            List<AgendaPostVentaDTO> lista = new List<AgendaPostVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaPostVentaSeguimientoInvitado_Mensajes", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@TipoAgenda", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.TipoAgenda;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaPostVentaDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    TipoAgenda = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("TipoAgenda")]),
                                    DesTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("DesTipoAgenda")].ToString(),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    CodigoCalificacion = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoCalificacion")]),
                                    DesCalificacion = oIDataReader[oIDataReader.GetOrdinal("DesCalificacion")].ToString(),
                                    ColorCalificacion = oIDataReader[oIDataReader.GetOrdinal("ColorCalificacion")].ToString(),
                                    Mensaje = oIDataReader[oIDataReader.GetOrdinal("Mensaje")].ToString(),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

        public List<AgendaPostVentaDTO> uspListarCantidadSeguimiento(AgendaPostVentaDTO oAgendaPostVentaDTO)
        {
            List<AgendaPostVentaDTO> lista = new List<AgendaPostVentaDTO>();
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarCantidadSeguimiento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaPostVentaDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oAgendaPostVentaDTO.FechaInicio;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAgendaPostVentaDTO.FechaFinal;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaPostVentaDTO()
                                {
                                    DesTipoAgenda = oIDataReader[oIDataReader.GetOrdinal("Tipo")].ToString(),
                                    Cantidad_Excelente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Excelente")]),
                                    Cantidad_Bueno = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Bueno")]),
                                    Cantidad_Regular = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Regular")]),
                                    Cantidad_Malo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Malo")])
                                });
                            }
                        }

                    }
                }
            }
            return lista;

        }

    }
}
