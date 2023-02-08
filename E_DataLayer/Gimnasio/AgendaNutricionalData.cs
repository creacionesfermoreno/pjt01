
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class AgendaNutricionalData
	{

        public List<AgendaNutricionalDTO> uspValidarHorariosOcupadosCitasNutricionales(AgendaNutricionalDTO oAgendaNutricionalDTO)
        {
            List<AgendaNutricionalDTO> lista = new List<AgendaNutricionalDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspValidarHorariosOcupadosCitasNutricionales", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaNutricionalDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaNutricionalDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = oAgendaNutricionalDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = oAgendaNutricionalDTO.HoraInicio;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaNutricionalDTO()
                                {                                  
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),                                   
                                    strFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]).ToString("HH:mm")                                   
                                });
                            }
                        }

                    }
                }
            }

            return lista;
        }


        public List<AgendaNutricionalDTO> uspListarAgendaNutricionalGeneral_Paginacion(AgendaNutricionalDTO oAgendaNutricionalDTO, Paging paging)
        {
            List<AgendaNutricionalDTO> lista = new List<AgendaNutricionalDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaNutricionalGeneral_Paginacion", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaNutricionalDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaNutricionalDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = oAgendaNutricionalDTO.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oAgendaNutricionalDTO.FechaInicio_Filtro;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAgendaNutricionalDTO.FechaFin_Filtro;
                    cmd.Parameters.Add(new SqlParameter("@TipoActividad", System.Data.SqlDbType.Int)).Value = oAgendaNutricionalDTO.TipoActividad;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAgendaNutricionalDTO.Buscador;

                    cmd.Parameters.Add(new SqlParameter("@PaginaActual", System.Data.SqlDbType.Int)).Value = paging.PageNumber;
                    cmd.Parameters.Add(new SqlParameter("@TamanioPagina", System.Data.SqlDbType.Int)).Value = paging.PageRecords;
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaNutricionalDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    strFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    Nombres = oIDataReader[oIDataReader.GetOrdinal("Nombres")].ToString(),
                                    Apellidos = oIDataReader[oIDataReader.GetOrdinal("Apellidos")].ToString(),
                                    Celular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString(),
                                    Correo = oIDataReader[oIDataReader.GetOrdinal("Correo")].ToString(),
                                    EstadoCelular = oIDataReader[oIDataReader.GetOrdinal("Celular")].ToString() == "" ? "none" : "block",
                                    Seguimiento_Cliente = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Seguimiento")]),
                                    DesTipoActividad = oIDataReader[oIDataReader.GetOrdinal("DesTipoActividad")].ToString(),
                                    UrlIconoTipoActividad = oIDataReader[oIDataReader.GetOrdinal("UrlIconoTipoActividad")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            
            return lista;
        }

        public AgendaNutricionalDTO uspListarAgendaNutricionalGeneral_NumeroRegistros(AgendaNutricionalDTO oAgendaNutricional)
        {
            AgendaNutricionalDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaNutricionalGeneral_NumeroRegistros", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaNutricional.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaNutricional.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = oAgendaNutricional.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaInicio", System.Data.SqlDbType.DateTime)).Value = oAgendaNutricional.FechaInicio_Filtro;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAgendaNutricional.FechaFin_Filtro;
                    cmd.Parameters.Add(new SqlParameter("@Buscador", System.Data.SqlDbType.VarChar)).Value = oAgendaNutricional.Buscador;
                    cmd.Parameters.Add(new SqlParameter("@TipoActividad", System.Data.SqlDbType.Int)).Value = oAgendaNutricional.TipoActividad;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new AgendaNutricionalDTO()
                                {
                                    CantidadRegistros = Convert.ToInt32(reader[reader.GetOrdinal("CantidadRegistros")])
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
        }

        public List<AgendaNutricionalDTO> uspListarAgendaNutricionalPorCliente(AgendaNutricionalDTO oAgendaNutricionalDTO)
        {
            List<AgendaNutricionalDTO> lista = new List<AgendaNutricionalDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarAgendaNutricionalPorCliente", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaNutricionalDTO.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oAgendaNutricionalDTO.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = oAgendaNutricionalDTO.CodigoSocio;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new AgendaNutricionalDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSocio")]),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    HoraInicio = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("HoraInicio")]),
                                    Asunto = oIDataReader[oIDataReader.GetOrdinal("Asunto")].ToString(),
                                    Estado = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    strFechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]).ToString("dd/MM/yyyy"),
                                    DesTipoActividad = oIDataReader[oIDataReader.GetOrdinal("DesTipoActividad")].ToString()
                                });
                            }
                        }

                    }
                }
            }

            return lista;            
        }
        
        public AgendaNutricionalDTO uspBuscarAgendaNutricionalPorCodigo(AgendaNutricionalDTO oAgendaNutricional)
		{
            AgendaNutricionalDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarAgendaNutricionalPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oAgendaNutricional.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@Fecha", System.Data.SqlDbType.DateTime)).Value = oAgendaNutricional.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@FechaFin", System.Data.SqlDbType.DateTime)).Value = oAgendaNutricional.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@Vendedores", System.Data.SqlDbType.VarChar, 100)).Value = oAgendaNutricional.CodigoSocio;
                   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new AgendaNutricionalDTO()
                                {
                                    CodigoUnidadNegocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoUnidadNegocio")]),
                                    CodigoSede = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSede")]),
                                    CodigoSocio = Convert.ToInt32(reader[reader.GetOrdinal("CodigoSocio")]),
                                    Codigo = Convert.ToInt32(reader[reader.GetOrdinal("Codigo")]),
                                    HoraInicio = Convert.ToDateTime(reader[reader.GetOrdinal("HoraInicio")]),
                                    Asunto = reader[reader.GetOrdinal("Asunto")].ToString(),
                                    Estado = Convert.ToInt32(reader[reader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = reader[reader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(reader[reader.GetOrdinal("FechaCreacion")])                                    
                                };
                            }
                        }
                    }
                }
            }

            return itemDTO;
		}
				
		public void Registrar(AgendaNutricionalDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarAgendaNutricional", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;

                    cmd.Parameters.Add(new SqlParameter("@HoraInicio", System.Data.SqlDbType.DateTime)).Value = item.HoraInicio;
                    cmd.Parameters.Add(new SqlParameter("@Asunto", System.Data.SqlDbType.VarChar,200)).Value = item.Asunto;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Int)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@TipoActividad", System.Data.SqlDbType.Int)).Value = item.TipoActividad;

                    cmd.ExecuteNonQuery();
                }
            }            
		}
        
		public void Actualizar(AgendaNutricionalDTO item)
		{
		  
		}
        
		public void Eliminar(AgendaNutricionalDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarAgendaNutricional", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSocio", System.Data.SqlDbType.Int)).Value = item.CodigoSocio;
                    
                    cmd.ExecuteNonQuery();
                }
            }            
		}
	}
}
