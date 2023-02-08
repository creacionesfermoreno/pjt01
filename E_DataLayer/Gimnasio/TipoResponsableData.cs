
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_DataModel.Gimnasio;
using E_DataModel.Common;
using System.Data.SqlClient;

namespace E_DataLayer.Gimnasio
{
	public class TipoResponsableData
	{
		
        public List<TipoResponsableDTO> Listar(TipoResponsableDTO oItem)
		{
			 List<TipoResponsableDTO> lista = new List<TipoResponsableDTO>();

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspListarTipoResponsables", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                   
                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                lista.Add(new TipoResponsableDTO()
                                {
                                    CodigoTipoResponsable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoResponsable")]),
                                    NombreResponsable = oIDataReader[oIDataReader.GetOrdinal("NombreResponsable")].ToString()                                    
                                });
                            }
                        }

                    }
                }
            }
            return lista;            
		}
		
		public TipoResponsableDTO BuscarPorCodigoTipoResponsable(TipoResponsableDTO oItem)
		{
			TipoResponsableDTO itemDTO = null;

            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspBuscarTipoResponsablePorCodigo", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio", System.Data.SqlDbType.Int)).Value = oItem.CodigoUnidadNegocio;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede", System.Data.SqlDbType.Int)).Value = oItem.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoResponsable", System.Data.SqlDbType.Int)).Value = oItem.CodigoTipoResponsable;

                    using (SqlDataReader oIDataReader = cmd.ExecuteReader())
                    {
                        if (oIDataReader.HasRows)
                        {
                            while (oIDataReader.Read())
                            {
                                itemDTO = new TipoResponsableDTO()
                                {
                                    CodigoTipoResponsable = Convert.ToInt32(oIDataReader[oIDataReader.GetOrdinal("CodigoTipoResponsable")]),
                                    NombreResponsable = oIDataReader[oIDataReader.GetOrdinal("NombreResponsable")].ToString()                                
                                };
                            }
                        }
                    }
                }
            }
            return itemDTO;            
		}
		
		public void Registrar(TipoResponsableDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspRegistrarTipoResponsable", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoResponsable ", System.Data.SqlDbType.Int)).Value = item.CodigoTipoResponsable;
                    cmd.Parameters.Add(new SqlParameter("@NombreResponsable", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreResponsable;                   
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede ", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.ExecuteNonQuery();
                }
            }            
		}

		public void Actualizar(TipoResponsableDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspActualizarTipoResponsable", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoResponsable ", System.Data.SqlDbType.Int)).Value = item.CodigoTipoResponsable;
                    cmd.Parameters.Add(new SqlParameter("@NombreResponsable", System.Data.SqlDbType.VarChar, 100)).Value = item.NombreResponsable;
                    cmd.Parameters.Add(new SqlParameter("@CodigoSede ", System.Data.SqlDbType.Int)).Value = item.CodigoSede;
                    cmd.Parameters.Add(new SqlParameter("@CodigoUnidadNegocio ", System.Data.SqlDbType.Int)).Value = item.CodigoUnidadNegocio;
                    cmd.ExecuteNonQuery();
                }
            }            
		}

		public void Eliminar(TipoResponsableDTO item)
		{
            using (var conn = new SqlConnection(Helper.Conexion()))
            {
                conn.Open();
                using (var cmd = new SqlCommand("uspEliminarTipoResponsable", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@CodigoResponsable ", System.Data.SqlDbType.Int)).Value = item.CodigoTipoResponsable;
                    cmd.ExecuteNonQuery();
                }
            }
            
		}
	}
}
