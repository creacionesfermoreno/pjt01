using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class TipoDocumentoData
	{
	
		public List<TipoDocumentoDTO> Listar()
		{
			List<TipoDocumentoDTO> lista = new List<TipoDocumentoDTO>();
            
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTipoDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new TipoDocumentoDTO()
                                {
                                    codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()                                  
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}
		
		public TipoDocumentoDTO BuscarPorCodigoTipoDocumento(TipoDocumentoDTO oItem)
		{
			TipoDocumentoDTO itemDTO = null;
           
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarTipoDocumentoPorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@codigo", System.Data.SqlDbType.Int)).Value = oItem.codigo;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new TipoDocumentoDTO()
                                {
                                    codigo = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("codigo")]),
                                    Descripcion = oIDataReader[oIDataReader.GetOrdinal("Descripcion")].ToString()                                   
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}
		
		public void Registrar(TipoDocumentoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarTipoDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@codigo ", System.Data.SqlDbType.Int)).Value = item.codigo;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioCreacion ", System.Data.SqlDbType.VarChar)).Value = item.UsuarioCreacion;
                  
                    cmd.ExecuteNonQuery();
                }
            }
		}

		public void Actualizar(TipoDocumentoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarTipoDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@codigo ", System.Data.SqlDbType.Int)).Value = item.codigo;
                    cmd.Parameters.Add(new SqlParameter("@Descripcion", System.Data.SqlDbType.VarChar, 100)).Value = item.Descripcion;
                    cmd.Parameters.Add(new SqlParameter("@UsuarioEdicion ", System.Data.SqlDbType.VarChar)).Value = item.UsuarioEdicion;

                    cmd.ExecuteNonQuery();
                }
            }
		}

		public void Eliminar(TipoDocumentoDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarTipoDocumento", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@codigo ", System.Data.SqlDbType.Int)).Value = item.codigo;
                
                    cmd.ExecuteNonQuery();
                }
            }
		}
	}
}
