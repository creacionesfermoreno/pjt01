
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class ConfiguracionEstadosDelSocioData
	{		
        public List<ConfiguracionEstadosDelSocioDTO> Listar(ConfiguracionEstadosDelSocioDTO oConfiguracionEstadosDelSocioDTO)
		{
			List<ConfiguracionEstadosDelSocioDTO> lista = new List<ConfiguracionEstadosDelSocioDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarConfiguracionEstadosDelSocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodSede", System.Data.SqlDbType.Int)).Value = oConfiguracionEstadosDelSocioDTO.CodigoSede;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new ConfiguracionEstadosDelSocioDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")]),
                                    UsuarioCreacion = oIDataReader[oIDataReader.GetOrdinal("UsuarioCreacion")].ToString(),
                                    FechaCreacion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaCreacion")]),
                                    UsuarioEdicion = oIDataReader[oIDataReader.GetOrdinal("UsuarioEdicion")].ToString(),
                                    FechaEdicion = Convert.ToDateTime(oIDataReader[oIDataReader.GetOrdinal("FechaEdicion")]),
                                    DirigidoA = oIDataReader[oIDataReader.GetOrdinal("DirigidoA")].ToString()
                                });
                            }
                        }

                    }
                }
            }

            return lista;           
		}
		
		public ConfiguracionEstadosDelSocioDTO BuscarPorCodigoConfiguracionEstadosDelSocio(ConfiguracionEstadosDelSocioDTO oConfiguracionEstadosDelSocio)
		{
            ConfiguracionEstadosDelSocioDTO itemDTO = new ConfiguracionEstadosDelSocioDTO();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarConfiguracionEstadosDelSocioPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oConfiguracionEstadosDelSocio.Codigo;
                    
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new ConfiguracionEstadosDelSocioDTO()
                                {
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    CodigoSede = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoSede")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Color = oIDataReader[oIDataReader.GetOrdinal("Color")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])
                                };
                            }
                        }
                    }
                }
            }
			return itemDTO;
		}
		
		public void Registrar(ConfiguracionEstadosDelSocioDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarConfiguracionEstadosDelSocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar,100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Color", System.Data.SqlDbType.VarChar, 200)).Value = item.Color;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;

                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@FechaCreacion", System.Data.SqlDbType.DateTime)).Value = item.FechaCreacion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar,100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@FechaEdicion", System.Data.SqlDbType.DateTime)).Value = item.FechaEdicion;
                    
                    cmd.ExecuteNonQuery();
                }
            }          		
		}

		public void Actualizar(ConfiguracionEstadosDelSocioDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarConfiguracionEstadosDelSocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@Color", System.Data.SqlDbType.VarChar,100)).Value = item.Color;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}

		public void Eliminar(ConfiguracionEstadosDelSocioDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarConfiguracionEstadosDelSocio", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
