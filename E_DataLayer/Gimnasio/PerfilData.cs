using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class PerfilData
	{
		public List<PerfilDTO> Listar()
		{
			List<PerfilDTO> lista = new List<PerfilDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGListarPerfil", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PerfilDTO()
                                {
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Estado = Convert.ToBoolean(oIDataReader[oIDataReader.GetOrdinal("Estado")])                                   
                                });
                            }
                        }

                    }
                }
            }
            return lista;           
		}

        public List<PerfilDTO> uspListarConfiguracionPerfil()
		{			
            List<PerfilDTO> lista = new List<PerfilDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarConfiguracionPerfil", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new PerfilDTO()
                                {
                                    CodigoPerfil = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoPerfil")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString(),
                                    Check = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Estado")]) == 0 ? "" : "checked"                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;             
		}
        
		public PerfilDTO BuscarPorCodigoPerfil(PerfilDTO oitem)
		{
			PerfilDTO itemDTO = null;
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGBuscarPerfilPorCodigoPerfil", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = oitem.CodigoPerfil;
                    
                    cmd.Parameters.AddWithValue("@NumeroRegistros", 0).Direction = System.Data.ParameterDirection.Output;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new PerfilDTO()
                                {
                                    CodigoPerfil = Convert.ToInt32(reader[reader.GetOrdinal("CodigoPerfil")]),
                                    Descripcion = reader[reader.GetOrdinal("Descripcion")].ToString(),
                                    Estado = Convert.ToBoolean(reader[reader.GetOrdinal("Estado")].ToString())                                
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;           
		}
		
		public int Registrar(PerfilDTO item)
		{
		   int ? campoRetorno = 0;
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGRegistrarPerfil", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CodigoPerfil", 0).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar,100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar,50)).Value = item.UsuarioCreacion;
                   
                    cmd.ExecuteNonQuery();
                    campoRetorno = Convert.ToInt32(cmd.Parameters["@CodigoPerfil"].Value);
                }
            }
          
		  return Convert.ToInt32(campoRetorno);
		}

		public void Actualizar(PerfilDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGActualizarPerfil", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@Estado", System.Data.SqlDbType.Bit)).Value = item.Estado;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 50)).Value = item.UsuarioCreacion;

                    cmd.ExecuteNonQuery();
                }
            }          
		}

		public void Eliminar(PerfilDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("SEGEliminarPerfil", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoPerfil", System.Data.SqlDbType.Int)).Value = item.CodigoPerfil;
                   
                    cmd.ExecuteNonQuery();
                }
            }
		}

	}
}
