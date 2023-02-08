
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class SubTipoDocumentoData
	{		
        public List<SubTipoDocumentoDTO> Listar(SubTipoDocumentoDTO oitem)
		{
			List<SubTipoDocumentoDTO> lista = new List<SubTipoDocumentoDTO>();
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSubTipoDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SubTipoDocumentoDTO()
                                {
                                    CodigoTipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoDocumento")]),
                                    DescripcionTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DescripcionTipoDocumento")].ToString(),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}
        
        public List<SubTipoDocumentoDTO> ListarPorTipoDocumento(SubTipoDocumentoDTO oitem)
        {
            List<SubTipoDocumentoDTO> lista = new List<SubTipoDocumentoDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarSubTipoDocumentoPorTipoDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oitem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@TipoDocumento", System.Data.SqlDbType.Int)).Value = oitem.CodigoTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oitem.CodigoSede;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new SubTipoDocumentoDTO()
                                {                                  
                                    CodigoTipoDocumento = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoDocumento")]),
                                    DescripcionTipoDocumento = oIDataReader[oIDataReader.GetOrdinal("DescripcionTipoDocumento")].ToString(),
                                    Codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("Codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
        }

		public SubTipoDocumentoDTO BuscarPorCodigoSubTipoDocumento(SubTipoDocumentoDTO oItem)
		{
			SubTipoDocumentoDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarSubTipoDocumentoPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoDocumento", System.Data.SqlDbType.Int)).Value = oItem.CodigoTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = oItem.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                itemDTO = new SubTipoDocumentoDTO()
                                {
                                    CodigoTipoDocumento = Convert.ToInt32(reader[reader.GetOrdinal("CodigoTipoDocumento")]),
                                    Codigo = Convert.ToInt32(reader[reader.GetOrdinal("Codigo")]),
                                    Descripcion = reader[reader.GetOrdinal("Descripcion")].ToString()                                   
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}
		
		public void Registrar(SubTipoDocumentoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarSubTipoDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoDocumento", System.Data.SqlDbType.Int)).Value = item.CodigoTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioCreacion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    
                    cmd.ExecuteNonQuery();
                }
            }
		}

		public void Actualizar(SubTipoDocumentoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarSubTipoDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoDocumento", System.Data.SqlDbType.Int)).Value = item.CodigoTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion", System.Data.SqlDbType.VarChar, 100)).Value = item.UsuarioEdicion;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;

                    cmd.ExecuteNonQuery();
                }
           }
		}

		public void Eliminar(SubTipoDocumentoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarSubTipoDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoTipoDocumento", System.Data.SqlDbType.Int)).Value = item.CodigoTipoDocumento;
                    cmd.Parameters.Add(new SqlParameter("@Codigo", System.Data.SqlDbType.Int)).Value = item.Codigo;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSerie", System.Data.SqlDbType.Int)).Value = item.CodigoSede;

                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
